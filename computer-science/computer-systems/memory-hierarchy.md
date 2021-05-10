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
