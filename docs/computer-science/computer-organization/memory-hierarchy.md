# Memory Hierarchy

The memory of a computer is implemented as a hierarchy, which consists of multiple levels of memory with difference speeds and sizes. The principle of locality states that programs access a relatively small portion of their address space at any instant of time.

- Temporal locality: If an item is referenced, it will tend to be referenced again soon. Memory hierarchies keep more recently accessed data items closer to the processor.
- Spatial locality: If an item is referenced, items whose address are close by will tend to be referenced soon. Memory hierarchies move blocks consisting of multiple contiguous words in memory to upper levels of the hierarchy.

The hit rate is the fraction of memory accesses found in a specific hierarchy level, which is a measure of the performance of the memory hierarchy.

- The time required to access a level of the memory hierarchy, including the time needed to determine whether the access is a hit or a miss.

- The time required to fetch a block into a level of the memory hierarchy from the lower level, including the time to access the block, transmit it from one level to the other, insert it in the level that experienced the miss, and then pass the block to the requestor.

## Memory Technology

- SRAM (static random access memory) is a type of random access memory that retains data bits in its memory as long as power is being supplied. The access time is close to the cycle time.
- DRAM (dynamic random access memory) is a type of random access memory that stores data bits as a charge in a capacitor, so the value needs to be periodically refreshed. It's organized in banks, where each bank contains a series of rows. To improve performance, it buffers rows for repeated access. It uses a two-level decoding structure to refresh an entire row with a read cycle followed by a write cycle.
  - Synchronous DRAM (SDRAM) uses a clock to eliminate the time for the memory and processor to synchronize. In a DDR SDRAM, data transfers on both the rising and falling edge of the clock.
- Flash memory is a type of electrically erasable programmable read-only memory. Writes can wear out flash memory bits, thus most flash products include a controller to spread the writes by remapping blocks that have been written many times to less trodden blocks.
- Disk memory is a type of magnetic hard disk that consists of a collection of platters, which rotate on a spindle at 5400 to 15000 revolutions per minute. The metal platters are covered with magnetic recording material on both sides. The movable arm with a electromagnetic coil is responsible for reading and writing information on the disk.

## Cache

### Direct-Mapped Cache

The direct-mapped cache indicates that each address maps to a unique block in the cache. Each address is divided into a tag field, which is used to compare with the value of the tag field of the cache block, and a cache index field, which is used to select the block. The valid bit of the cache block indicates whether or not the value is meaningful.

The total number of bits needed for a cache is a function of the cache size and the address size, because the cache includes both the storage for the data and the tags. For a 64-bit address, if the cache size is $2^n$ blocks and the block size is $2^m$ words ($2^{m + 2}$ bytes), the size of the tag field is $64 - (n + m + 2)$.

When a request for data from the cache can't be filled because the data is not present in the cache, a cache miss occurs and the cache controller must fetch the requested data from a lower-level cache. The processing of a cache miss creates a pipeline stall.

- Write-through is a scheme in which writes update both the cache and the lower-level cache. This scheme has significant impact on the performance because each write takes a long time to complete.
- Write-back is a scheme in which writes update the cache. The modified block is written to the lower-level cache when it's replaced.

### Set-Associative Cache

The set-associative cache indicates that each address maps to a fixed number of blocks in the cache. The $n$-way set-associative cache consists of a number of sets, each of which consists of $n$ blocks. Each address is divided into a tag field, which is used to compare with the value of the tag field of the cache block, and a set index field, which is used to select the set. When accessing the cache, all the tags of all the elements of the set must be searched.

When a miss occurs and a block needs to be replaced, the set-associative cache has different strategies to determine the candidate. In an LRU scheme, the block replaced is the one that has been unused for the longest time.
