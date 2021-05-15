# The Memory Hierarchy

## Storage Technologies

### Random Access Memories

- Dynamic RAM (DRAM): The main memory and the frame buffer of a graphics system.
- Static RAM (SRAM): The cache memories. Faster and significantly more expensive than dynamic RAM.

DRAM and SRAM are volatile memories, thus they will lose information if powered off.

#### Static RAM

SRAM stores each bit in a bistable memory cell. Each cell is implemented with a six-transistor circuit. This circuit could stay indefinitely in either of two different voltage state, and any other state will be unstable. Therefore, a cell will retain its value indefinitely, as long as it's kept powered.

#### Dynamic RAM

DRAM stores each bit as charge on a capacitor, which could be made very dense that each cell consists of a capacitor and a single access transistor. The DRAM memory cell is sensitive to disturbance, thus the memory system must periodically refresh every bit of memory by reading it out and then rewriting it.

#### Conventional DRAMs

The cells in a DRAM chip are partitioned into d supercells, each consisting of w DRAM cells, which could store dw bits of information. The supercells are organized as a rectangular array with r rows and c columns, and each supercell has an address of the form `(i, j)`.

Each DRAM chip is connected to the memory controller, that could transfer w bits at a time to and from each DRAM chip. To read the contents of supercell `(i, j)`:

1. The controller sends `i` to the DRAM through the `addr` pins.
2. The DRAM copies the entire row of supercells into its internal row buffer.
3. The controller sends `j` to the DRAM.
3. The DRAM responds by sending the contents back to the controller through the `data` pins.

#### Nonvolatile Memories

Nonvolatile memories retain value even if powered off.

- Fireware programs stored in a ROM (BIOS, disk controller, etc.)
- Solid state disks
- Disk cache

#### Memory Read Transaction

```s
movq A, %rax
```

1. CPU places address A on the memory bus.
2. Main memory reads A from the memory bus, retrieves word x, and places it on the bus.
3. CPU reads word x from the bus and copies it into a register.

#### Memory Write Transaction

```s
movq %rax, A
```

1. CPU places address A on the bus.
2. Main memory reads it and waits for the corresponding data word to arrive.
3. CPU places data word y on the bus.
4. Main memory reads data word y from the bus and stores it at address A.

### Disk Storage

- Disks consist of **platters**, each with two surfaces.
- Each surface consists of concentric rings called **tracks**.
- Each track consists of **sectors** separated by gaps.
- Aligned tracks of multiple platters form a cylinder.

#### Capacity

Disk capacity is the maximum number of bits that can be sotred. Vendors express capacity in units of gigabytes, where 1 GB = 10e9 bytes.

Disks partition tracks into disjoint subsets called recording zones. Each track in a zone has the same number of sectors, and each zone has a different number of sectors or tracks.

#### Access Time

Average time to access some target sector approximated by: Access = Average seek + Average rotation + Average transfer

- Seek time: Time to position heads over cylinder containing the target sector.
- Rotational latency: Time waiting for first bit of target sector to pass under r/w head.
- Transfer time: Time to read the bits in the target sector.

#### Logical Blocks

- Modern disks present a simpler abstract view of the complex sector geometry. The set of available sectors is modeled as a sequence of b-sized logical blocks.
- The disk controller maintains a mapping between logical blocks and physical sectors. It allows controller to set aside spare cylinders for each zone that are reserved for failures.

#### Sector Read Transaction

1. CPU writes a command, logical block number, and destination memory address to a port associated with disk controller.
2. Disk controller reads the sector and performs a direct memory access transfer into main memory.
3. The disk controller notifies the CPU with an interrupt when the transfer completes.

#### Solid State Disks

Solid State Disks are built on flask memory instead of rotating platters. The blocks in SSD contains 32 to 128 pages, and each page has a size of 4 KB to 512 KB. Data are read and written in units of pages, and pages can be written only after its block has been erased.

- Sequential access is faster than random access.
- Random writers are more slower since erasing a block takes a long time.

## Locality

Principle of Locality: Programs tend to use data and instructions with addresses near or equal to those they have used recently.

- Temporal locality: Recently referenced items are likely to be referenced again in the near future.
- Spatial locality: Items with nearby addresses tend to be referenced close together in time.

```cpp
int sum = 0;
for (int i = 0; i < n; i++) {
  sum += a[i];
}
return sum;
```

- Reference array elements in succession (Spatial locality)
- Reference instructions in sequence (Spatial locality)
- Reference variable `sum` in each iteration (Temporal locality)
- Cycle through loop repeatedly (Temporal locality)

```cpp
// Good locality
int sum_array_rows_1(int a[M][N]) {
  int i, j, sum = 0;
  for (i = 0; i < M; i++) {
    for (j = 0; j < N; j++) {
      sum += a[i][j];
    }
  }
}

// Bad locality (Jump through different memory locations)
int sum_array_rows_2(int a[M][N]) {
  int i, j, sum = 0;
  for (j = 0; j < N; j++) {
    for (i = 0; i < M; i++) {
      sum += a[i][j];
    }
  }
}
```

## Memory Hierarchies

The approach of organizing memory and storage systems is known as a memory hierarchy.

- Fast storage technologies cost more per byte, have less capacity, and require more power.
- The gap between CPU and main memory is widening.
- Well-written programs tend to exhibit good locality.

### Example

- L0: Regs
- L1: L1 cache (SRAM)
- L2: L2 cache (SRAM)
- L3: L3 cache (SRAM)
- L4: Main memory (DRAM)
- L5: Local secondary storage (local disks)
- L6: Remote secondary storage (web servers, etc.)

### Cache

- Cache is a smaller, faster storage device that acts as a staging area for a subset of the data in a larger, slower device.
- For each level `k`, the faster, smaller device at level `k` serves as a cache for the larger, slower device at level `k + 1`.
- Because of locality, programs tend to access the data at level `k` more often than they access the data at level `k + 1`.

#### General Concepts

- **Hit**: Requested data is in the cache.
- **Miss**: Requested data is not in the cache.
- - **Cold miss**: The cache is empty.
- - **Conflict miss**: The cache is large enough, but multiple data objects all map to the same block.
- - **Capacity miss**: The active cache blocks is larger than the cache.

## Cache Memories

### Generic Cache Memory Organization

In general, a cache's organization can be represented by `(S, E, B, m)`. The capacity of cache is `C = S x E x B`.

- Suppose the memory address has `m` bits that form `M = 2 ^ m` unique addresses.
- The cache is organized as an array of `S = 2 ^ s` cache sets.
- Each set has `E` cache lines.
- Each line has a data block of `B = 2 ^ b` bytes, a valid bit that indicates whether the line contains meaningful information, and `t = m - (b + s)` tag bits that identify the block in the cache line.

To find whether an address is in the cache, the `m` address bits is partitioned into three fields:

1. Tag: `t` bits
2. Set index: `s` bits
3. Block offset: `b` bits

### Direct-Mapped Caches

The direct-mapped cache refers to a cache with exactly one line per set.

- Set selection: The cache extracts the `s` set index bits from the address for `w` as an unsigned interger for the set number.
- Line matching: The cache determines whether the valid bit for this line (the only line in the set) is set and the tag in the line matches the tag in the address of `w`.
- Word selection: The block offset bits indicate the offset of the first byte in the desired word.
- Line replacement: If the cache misses, it has to retrieve the requested block from the next level of hierarchy. If the set is full of valid lines, one of them must be evicted.

### Set Associative Caches

If each set holds more than one cache line, the cache is defined as the set associative cache. A cache with `1 < E < C/B` is called E-way set associative cache.

- Set selection: Identical to direct-mapped cache.
- Line matching and word selection: The cache checks multiple line to find matched tag and whether the line is valid.
- Line replacement: The cache uses least frequently used (LFU) policy to replace the line that has been referenced the fewest times or least recently used (LRU) policy to replace the line that was last accessed in the past.

### Fully Associative Caches

If there's only one set in a cache that contains all of the cache lines, the set is defined as the fully associative cache.

- Set selection: There's only one set, so there are no set index bits in the address.
- Line matching and word selection: Identical to set associative cache. Since the cache has to search for many tags in parallel, fully associative caches are only suitable for small caches.

### Issues with Writes

- Write hit
- - Write-through: Immediately write `w`'s cache block to the next lower level
- - Write-back: Write the cache block to the next level only when it's evicted from the cache by replacement algorithm
- Write miss
- - No-write-allocate: Bypasses the cache and writes the word directly to the next level
- - Write-allocate: Loads the corresponding block from the next lower level into the cache and updates the cache block

### Performance Impact of Cache Parameters

- Miss rate: The fraction of memory refrences that miss. (`#misses / #references`)
- Hit rate: `1 - Miss rate`
- Hit time: The time to deliver a word in the cache to the CPU, including set selection, line identification, and word selection.
- Miss penalty: Any additional line required because of a miss. The penalty for L1 misses served from L2 is on the order of 10 cycles.

#### Impact of Cache Size

- Higher hit rate
- Higher hit time, since it's harder to make large memories run faster

#### Impact of Block Size

- Smaller hit rate in program with more temporal locality
- Higher miss penalty, since it takes more time to retrieve the block from the next level of cache

### Impact of Associativity

- Decrease the vulnerability of the cache to thrashing due to conflict misses
- Higher hit time, since it takes time to find the matched tag
- Higher miss penalty, since it takes time to decide which block should be evicted
- Difficult to implement

## Writing Cache-Friendly Code

In general, if a cache has a block size of `B` bytes, then a stride-k reference pattern results in an average of `min(1, (word size * k) / B)` misses per loop iteration.

- Repeated references to local variables (cache them in the register file)
- Stride-1 reference patterns (all levels of memory hierarchy store data as contiguous blocks)

```cpp
int sumarrayrows(int a[M][N]) {
  int i, j, sum = 0;
  for (i = 0; i < M; i++) {
    for (j = 0; j < N; j++) {
      sum += a[i][j];
    }
  }
  return sum;
}
```

## Impact of Caches on Program Performance

### The Memory Mountain

Read throughput (bandwith) is defined as the rate that a program reads data from the memory system. (MB/s)

The memory mountain characterizes the capabilities of its memory system. It's a two-dimensional function of read throughput versus temporal (size of working set) and spatial locality (number of strides).

- Perpendicular to the `size` axis are four ridges that refers to the temporal locality where the working set fits entirely in the L1, L2, L3 cache, and main memory.
- On each of the memory ridges, there's a slope of spatial locality that falls downhill as the stride increases. Once the stride reaches 8, every read request misses and therefore the slope becomes flat.
