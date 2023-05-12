# Ordering

## Reordering

Processors and compilers perform all sorts of tricks to make programs run as fast as possible. The processor might determine that two particular consecutive instructions in a program will not affect each other and execute them out of order. While one instruction is blocked on fetching some data, several of the following instructions might be executed and finished before the first instruction finishes, as long as that wouldn't change the behavior of the program.

When working with atomics, the ordering of atomic operations matters, since the processor's and compiler's usual logic ignores interactions between threads and might allow for optimizations that do change the result of the program.

The set of ordering options is defined in the `std::sync::atomic::Ordering` enum. The orderings are abstract and do not reflect the actual compiler and processor mechanisms involved, such as instruction reordering.

- Relaxed ordering: `Ordering::Relaxed`
- Release and acquire ordering: `Ordering::{Release, Acquire, AcqRel}`
- Sequential consistent ordering: `Ordering::SeqCst`

## Happens-Before Relationship

Rust's memory model defines the order in which operations happen in terms of happens-before relationships, which are situations where one thing is guaranteed to happen before another thing.

The basic happens-before rule is that all things that happens within the same thread happens in order. If a thread is executing `f(); g();`, then `f()` happens before `g()`. Between threads, happens-before relationships occur in a few specific cases, such as when spawning and joining a thread, unlocking and locking a mutex, and through atomic operations that use non-relaxed memory ordering.

### Spawning and Joining

- Spawning a thread creates a happens-before relationship between what happened before the `spawn()` call, and the new thread.
- Joining a thread creates a happens-before relationship between the joined thread and what happens after the `join()` call.

```rs
static X: AtomicI32 = AtomicI32::new(0);

fn main() {
  X.store(1, Relaxed);

  let t = thread::spawn(f);
  X.store(2, Relaxed);
  t.join().unwrap();

  X.store(3, Relaxed);
}

fn f() {
  let x = X.load(Relaxed);
  assert!(x == 1 || x == 2);
}
```

## Relaxed Ordering

The atomic operation using relaxed memory ordering do not provide happens-before relationship, but it guarantees a total modification order of each individual atomic variable. It means that all modifications of the same atomic variable happen in an order that is the same from the perspective of all threads.

In the following example, the variables $a$, $b$, $c$, and $d$ must follow the condition $a \le b \le c \le d$.

```rs
static X: AtomicI32 = AtomicI32::new(0);

fn a() {
  X.fetch_add(5, Relaxed);
  X.fetch_add(10, Relaxed);
}

fn b() {
  let a = X.load(Relaxed);
  let b = X.load(Relaxed);
  let c = X.load(Relaxed);
  let d = X.load(Relaxed);
  println!("{a} {b} {c} {d}");
}
```

## Release and Acquire Ordering

Release and acquire ordering are used in a pair to form a happens-before relationship between threads. The happens-before relationship is formed when an acquire-load operation observes the result of a release-store operation. The store and operations before it happened before the load and operations after it.

- `Acquire` ordering applies to load operations. When using `Acquire` for a fetch or compare-and-exchange operation, it applies to the part of the operation that loads the value.
- `Release` ordering applies to store operations. When using `Release` ordering applies to store operations. When using `Acquire` for a fetch or compare-and-exchange operation, it applies to the part of the operation that stores the value.
- `AcqRel` is used to represent the combination of `Acquire` and `Release`, which causes both the load to use acquire ordering, and the store to use release ordering.

For example, the operations before the store operation with `Release` ordering are visible after the load operation with `Acquire` ordering.

```rs
use std::sync::atomic::Ordering::{Acquire, Release};

static DATA: AtomicU64 = AtomicU64::new(0);
static READY: AtomicBool = AtomicBool::new(false);

fn main() {
  thread::spawn(|| {
    DATA.store(123, Relaxed);
    READY.store(true, Release);
  });
  while !READY.load(Acquire) {
    thread::sleep(Duration::from_millis(100));
  }
  println!("{}", DATA.load(Relaxed));
}
```

## Consume Ordering

Consume ordering is an efficient variant of the acquire ordering, whose synchronizing effects are limited to things that depend on the loaded value.

If an atomic variable with a release-stored value `x` is consume loaded, the store happened before the evaluation of dependent expressions like `*x` or `table.lookup(x + 1)`, but not other independent operations like reading another variable that doesn't depend on `x`.

However, dependencies are hard to track because of compiler optimization, thus compilers treat the consume ordering as acquire ordering.

## Sequential Consistent Ordering

Sequential consistent ordering is the strongest ordering, which includes all the guarantees of acquire ordering for loads and release ordering for stores, and guarantees a consistent order of operations. Each single operation using sequential consistent ordering within a program is part of a single total order that all threads agree on, which is consistent with the total modification order of each individual variable.

While it might seem like the easiest memory ordering to reason about, sequential consistent ordering is almost never necessary in practice.

## Fence

The `std::sync::atomic::fence` function represents an atomic fence that accepts an `Acquire`, `Release`, `AcqRel`, or `SeqCst` ordering. The fence is useful when the program needs an ordering on multiple operations because it's not tied to an atomic variable. In general, the fence behaves like an implitic atomic variable with a specific ordering.

- The release semantics on store operations ensure that all previous writes are visible to other threads before the store operation takes place. The release fence ensures that all previous writes are visible to other threads before the fence is passed, similar to the release-store operation. Therefore, a release-store can be split into a release fence and a relaxed store.

- The acquire semantics on load operations ensure that all subsequent reads and writes occur after the load operation. The acquire fence ensures that all subsequent reads and writes occur after the fence, similar to the acquire-load operation. Therefore, an acquire-load can be split into a relaxed load and an acquire fence.

- The sequential consistent fence is both a release fence and an acquire fence, but also part of the single total order of sequential consistent operations. The fence is part of the total order, but not the atomic operations before or after it.
