# Processor

## Processor Instruction

When compiling software written in a compiled language like Rust or C, the code is translated into machine instructions that can execute on the processor. The instructions are specific to the processor architecture.

After compiling from a language like Rust, most of the structure of the original source code is gone. Depending on optimization level, functions and function calls might still be recognizable. However, types such as structs or enums have been reduced to bytes and addresses, and loops and conditionals have been reduced to a flat structure with basic jump or branch instructions.

### Load and Store

The compiled instruction for the load and store operations on an `&AtomicI32` is identical to the operations on a regular `&mut i32`, because the `mov` and `str` instructions are atomic. The difference between `&mut i32` and `&AtomicI32` is relavent for the compiler checks and optimizations.

```rs
pub fn load(x: &AtomicI32) -> i32 {
  x.load(Relaxed)
}

pub fn store(x: &AtomicI32) {
  x.store(0, Relaxed);
}
```

```asm
load:
  ldr w0, [x0]
  ret

store:
  str wzr, [x0]
  ret
```

### Compare and Exchange

The non-atomic fetch operation complies to multiple instructions on a RISC architecture like `aarch64` and a single instruction on a CISC architecture like `x86_64`.

```rs
pub fn a(x: &mut i32) {
  *x += 10;
}
```

```asm
a:
  ldr w8, [x0]
  add w8, w8, #10
  str w8, [x0]
  ret
```

The `add` instruction on `x86_64` will be splitted into several microinstructions behind the scenes, with separate steps for loading the value and storing the result. Although switching a processor core between threads happens between instructions, multiple cores might execute instructions in parallel.

```asm
a:
  add dword ptr [rdi], 10
  ret
```

To support multi-core systems, Intel introduced an instruction prefix called `lock`. It is used as a modifier to instructions like `add` to make their operation atomic. The lock prefix can be applied to a limited number of instructions, including `add`, `sub`, `and`, `not`, `or`, and `xor`.

The `add` instruction doesn't retrieve the original value. The `xadd` (exchange and add) variant can put the original value into a register. However, other than `xadd` and `xchg`, none of the other lock-prefixable instructions have such a variant. Therefore, operations such as `fetch_max` and `fetch_min` have no corresponding `x86_64` instruction.

```rs
pub fn a(x: &AtomicI32) -> i32 {
  x.fetch_add(10, Relaxed)
}
```

```asm
a:
  mov eax, 10
  lock xadd dword ptr [rdi], eax
  ret
```

The `cmpxchg` (compare and exchange) instruction can be used to implement operations that can't be represented with a single `x86_64` instruction. On `x86_64`, there is no difference between `compare_exchange` and `compare_exchange_weak`. Both compile down to a `lock cmpxchg` instruction.

```rs
pub fn a(x: &AtomicI32) -> i32 {
  x.fetch_or(10, Relaxed)
}
```

```rs
pub fn a(x: &AtomicI32) -> i32 {
  let mut current = x.load(Relaxed);
  loop {
    let new = current | 10;
    match x.compare_exchange(current, new, Relaxed, Relaxed) {
      Ok(v) => return v,
      Err(v) => current = v,
    }
  }
}
```

```asm
a:
  mov eax, dword ptr [rdi]
.L1:
  mov ecx, eax
  or ecx, 10
  lock cmpxchg dword ptr [rdi], ecx
  jne .L1
  ret
```

### Load-Linked and Store-Conditional

The closest thing to a compare-and-exchange loop on a RISC architecture is a load-linked or store-conditional loop. It involves a load-linked instruction, which behaves like a regular load instruction, and a store-conditional instruction, which behaves like a regular store instruction. The store-conditional instruction refuses to store to an address if other thread has overwritten that address since the load-linked instruction. These two instructions allow the processor to load a value from an address, update it, and store the new value back if there's no thread that has overwritten the value since the processor loaded it.

For an efficient implementation, there's a single address per core that can be tracked at a time and the store-conditional is allowed to fail to store even though nothing has changed that address. Therefore, the processor could track the address not per byte, but per chunk of 64 bytes, per kilobyte, or even the entire address space.

The load-linked and store-conditional instructions of `aarch64` are `ldxr` (load exclusive register) and `stxr` (store exclusive register). There's no need to perform special instructions such as `xadd` in `x86_64` to retrieve the old value.

```rs
pub fn a(x: &AtomicI32) {
  x.fetch_add(10, Relaxed);
}
```

```asm
a:
.L1:
  ldxr w8, [x0]
  add w9, w8, #10
  stxr w10, w9, [x0]
  cbnz w10, .L1
  ret
```

The `compare_exchange_weak` can be represented with a pair of `ldxr` and `stxr` instructions. The `ldxr` instruction loads the value, which is then compared with the `cmp` instruction to the expected value. If the result is `true`, the `stxr` instruction stores the new value. If the result is `false`, the `clrex` instruction aborts the load-linked or store-conditional pattern.

Because `stxr` is allowed to have false negatives, it will be used to perform `compare_exchange_weak`. For `compare_exchange`, there's an extra branch to restart the operation if it failed.

```rs
pub fn a(x: &AtomicI32) {
  x.compare_exchange_weak(5, 6, Relaxed, Relaxed);
}
```

```asm
a:
  ldxr w8, [x0]
  cmp w8, #5
  b.ne .L1
  mov w8, #6
  stxr w9, w8, [x0]
  ret
.L1:
  clrex
  ret
```

## Caching

Processors implement caching to avoid interacting with the memory as much as possible. In most processor architectures, the cache reads and writes memory in blocks of 64 bytes, even if a single byte was requested.

- If an instruction needs to read something from memory, the processor will ask its cache for that data. If it is cached, the cache will respond with the cached data, avoiding interacting with main memory.
- When an instruction wants to write something to memory, the cache could decide to hold on to the modified data without writing it to main memory. Subsequent read requests for the same address will then get the modified data, ignoring the outdated data in main memory. It would write the data back to main memory when the modified data needs to be dropped from the cache to make space.

### Cache Coherence

In modern processors, there are multiple layers of cache from L1 to L3 or L4. In a multi-core system, each processor core usually has its own L1 cache, while the L2 or L3 caches are shared with some or all of the other cores. In a naive implementation, if one cache would accept a write and mark some cache line as modified without informing the other caches, the state of the caches could become inconsistent.

The cache coherence protocol is used to define how the caches operate and communicate with each other to maintain a consistent state.

- Write-through protocol: In caches that implement the write-through cache coherence protocol, writes are not cached but sent through to the next layer. The communication channel between cache layers is shared between caches. When a cache observes a write for an address it has cached, it either drops or updates its own cache line. In this protocol, caches never contain cache lines in a modified state.

- MESI protocol: In caches that implement the MSEI protocol, each cache line has either of the modified, exclusive, shared, or invalid state. Caches that use this protocol communicate with all the other caches at the same level and send each other updates and requests to ensure a consistent state.
  - Modified state is used for cache lines that contain data that has been modified but not written back.
  - Exclusive state is used for cache lines that contain unmodified data that's not cached in other cache at the same level.
  - Shared state is used for unmodified cache lines that might appear in one or more of the other caches at the same level.
  - Invalid state is used for unused cache lines, which do not contain useful data.

### Impact on Performance

Caching behavior can have significant effects on the performance of the atomic operations. For example, a store operation requires exclusive access to a cache line, which slows down subsequent load operations on other cores that no longer share the cache line.

## Reordering

Modern processors implement optimizations that can have a big impact on correctness, at least when multiple threads are involved.

- Store buffer: Since writes can be slow, even with caching, processor cores might store write operations in a buffer to allow it to continue with the instructions that follow. In the background, the write operation is performed on the L1 cache. The processor doesn't have to wait while the cache coherence protocol jumps into action to get exclusive access to the relevant cache line. Although the processor makes sure that subsequent read operations from the same address won't be affected, the write operation is not visible to other cores, resulting in an inconsistent view.

- Invalidation queue: Caches that operate in parallel need to process invalidation requests, which are instructions to drop a specific cache line because it's about to be modified and become invalid. It's common for these requests to be queued for later processing. Although there's no impact on the single threaded program, the write operation is not visible to other cores, resulting in an inconsistent view.

- Pipelining: The processor might execute consecutive instructions in parallel. Modern processors can often start the execution of quite a few instructions in series while the first one is still in progress. When there's no dependencies between instructions, the execution time is not predictable.

Architectures that allow for instruction reordering provide special instructions to preventing reordering, such as forcing the processor to flush its store buffer or  executing pipelined instructions before continuing.

## Ordering

The ordering requirement informs the compiler to generate instructions for the processor to prevent it from reordering instructions that would break the rules.

- The acquire operation might not get reordered with operations that follow it.
- The release operation might not get reordered with operations that precede it.
- The relaxed operation allows for all types of reordering.
- The sequential consistent operation doesn't aollow reordering.

On an `x86_64` processor, a load operation will never appear to have happened after an operation that follows, and a store operation will never appear to have happened before a preceding operation. Therefore, a relaxed load is the same as an acquire load, and a relaxed store is the same as a release store.

On an `aarch64` processor, an acquire load operation will be compiled to the `ldar` instruction, and a release store operation will be compiled to the `stlr` instruction.

## Fence

The fence instruction is used to represent a `std::sync::atomic::fence`. The fence instruction prevents certain types of instructions from being reordered past it.

- The acquire fence must prevent preceding load operations from getting reordered with following operations.
- The release fence must prevent subsequent store operations from getting reordered with preceding operations.
