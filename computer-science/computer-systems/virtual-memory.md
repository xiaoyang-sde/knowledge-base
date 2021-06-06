# Virtual Memory

Process in a system share the main memory with other processes, which poses some special challenges. Virtual memory is an abstraction of the main memory that provides each process with a large, uniform, and private address space.

- It uses main memory efficiently by treating it as a cache for an address space stored on disk.
- It simplifies memory management by providing each process with a uniform address space.
- It protects the address space of each process from corruption by other processes.

## Physical and Virtual Addressing

The main memory is organized as an array of M contiguous byte-size cells starting from the address of 0. With physical addressing, the CPU accesses main memory with a physical address.

With virtual addressing, the CPU accesses main memory with a virtual address, which is converted to the physical address by address translation. The memory management unit (MMU) on the CPU chip translates virtual addresses with a lookup table stored in main memory.

## Address Spaces

The linear address space is an ordered set of consecutive non-negative integer addresses: `{0, 1, 2, ...}`.

- Virtual address space: The set of $$ N = 2^n $$ (n-bit) addresses `{0, 1, 2, ..., N − 1}`.
- Physical address space: The set of $$ M = 2^m $$ addresses (M bytes of physical memory) `{0, 1, 2, ..., M − 1}`.

The concept of address space makes a distinction between data objects (bytes) and their attributes (addresses). Each data object could have multiple independent addresses, each chosen from a different address space. (e.g. an address from virtual and another from physical)

## VM as a Tool for Caching

A virtual memory is organized as an array of N contiguous byte-size cells stored on disk. Each byte has a unique virtual address that serves as an index into the array. The contents of the array on disk are cached in main memory.

The virtual memory is partitioned into fixed-size blocks (virtual pages) of $$ P = 2^p $$ size. They are the transfer units between the disk and the main memory. The physical memory is also partitioned into physical pages of $$ P = 2^p $$.

The set of virtual pages is partitioned into 3 subsets:

- **Unallocated**: Pages that have not yet been allocated by the VM system.
- **Cached**: Allocated pages that are currently cached in physical memory.
- **Uncached**: Allocated pages that are not cached in physical memory.

### DRAM Cache Organization

DRAM cache refers to the VM system’s cache that caches virtual pages in main memory.

- Virtual pages tend to be large (4 KB to 2 MB) because of the large miss penalty and the expense of accessing the first byte.
- DRAM caches are fully associative that any virtual page can be placed in any physical page.
- DRAM caches always use write-back instead of write-through because of the large access time of disk.

### Page Tables

The page table is an array of page table entries stored in the physical memory. Each page in the virtual address space has a PTE at a fixed offset in the page table. Each PTE consists of a valid bit and an n-bit address field.

- **Unallocated**: The valid bit is not set and the address field is null.
- **Uncached**: The valid bit is not set and the address field points to the start of the virtual page on disk.
- **Cached**: The valid bit is set and the address field indicates the start of the physical page in DRAM.

### Page Hits

The address translation hardware uses the virtual address as an index to locate the PTE and read it from memory. Since the valid bit is set, it uses the physical memory address in the PTE to construct the physical address of the word.

### Page Faults

Page fault is a DRAM cache miss. The address translation hardware reads a PTE from memory, and infers from the valid bit that the virtual page is uncached, so it triggers a page fault exception.

The page fault exception invokes a page fault exception handler in the kernel, which selects a victim page and modifies its PTE. If the victim page has been modified, then the kernel copies it back to disk.

The kernel copies the requested virtual page from disk to the physical address of the victim page, updates its PTE, and then recovers the faulting instruction.

### Allocating Pages

The operating system allocates a new virtual page by creating room on disk and updating PTE to point to the newly created page on disk.

### Locality to the Rescue Again

Virtual memory is efficient because of locality. The principle of locality promises that at any point in time they will tend to work on a smaller set of active pages known as the working set or resident set. After an initial overhead where the working set is paged into memory, subsequent references to the working set result in hits, with no additional disk traffic.

If the working set size exceeds the size of physical memory, then the program can produce an unfortunate situation known as thrashing, where pages are swapped in and out continuously.

## VM as a Tool for Memory Management

The operating system provides separate page table and virtual address space for each process. Multiple virtual pages can be mapped to the same shared physical page. The notion of mapping a set of contiguous virtual pages to an arbitrary location in an arbitrary file is known as memory mapping.

- **Linking**: A separate address space allows each process to use the same basic format for its memory image. The uniformity simplifies the design and implementation of linkers.
- **Loading**: The Linux loader allocates virtual pages for the code and data segments, marks them as not cached, and points their page table entries to the appropriate locations in the object file. The loader never copies any data from disk into memory. The data are paged in automatically and on demand by the virtual memory system the first time each page is referenced.
- **Sharing**: To share code and data between processes (e.g. C standard library), the operating system maps different virtual pages in different processes to the same physical pages.
- **Memory allocation**: Virtual memory provides a simple mechanism for allocating additional memory to user processes. The operating system allocates an appropriate number contiguous virtual memory pages, and maps them to arbitrary physical pages located anywhere in physical memory.

## VM as a Tool for Memory Protection

The address translation mechanism provides access control with additional permission bits in the PTE.

- `SUP`: whether processes must be running in kernel (supervisor) mode to access the page
- `READ`: whether process could read from the page
- `WRITE`: whether process could write to the page

If an instruction violates these permissions, the CPU triggers a general protection fault that transfers control to an exception handler in the kernel, which kills the process and raises a "segmentation fault".

## Address Translation

Address translation is a mapping between the elements of an N-element virtual address space (VAS) and an M-element physical address space (PAS).

- **PTBR**: The page table base register that points to the current page table
- The n-bit virtual address
  - **VPO**: a p-bit virtual page offset
  - **VPN**: an (n − p)-bit virtual page number
- The m-bit physical address
  - **PPO**: a p-bit physical page offset (same as VPO)
  - **PPN**: an (m − p)-bit physical page number

1. The processor generates a virtual address and sends it to the MMU.
2. The MMU uses the VPN to select the appropriate PTE. (e.g. VPN 0 selects PTE 0)
3. The MMU constructs the physical address by concatenating the physical page number (PPN) from the page table entry and the VPO from the virtual address.
4. The data word is returned with the steps of [Page Hits](#page-hits) or [Page Faults](#page-faults)

### Integrating Caches and VM

Most systems use physical addresses to access the SRAM cache because it is straightforward for multiple processes to have blocks in the cache at the same time and to share blocks from the same virtual pages. The page table entries can be cached, just like any other data words.

### Speeding Up Address Translation with a TLB

Many systems eliminate the cost of retrieving the PTE from memory by including a small cache of PTEs in the MMU called a translation lookaside buffer (TLB).

- **TLB**: A small, virtually addressed cache with a high degree of associativity where each line holds a block consisting of a single PTE. The TLB has $$ T = 2^t $$ sets.
- **TLB Index (TLBI)**: The t least significant bits of the VPN.
- **TLB Tag (TLBT)**: The remaining bits in the VPN.
- **VPO**: The virtual page offset in the PTE.

When there is a TLB miss, then the MMU must fetch the PTE from the L1 cache or the memory and store it in the TLB.

### Multi-Level Page Tables

The hierarchy of page tables could reduce the size of page table in the memory. The TLB reduces the cost of accessing multiple page tables by caching the PTEs at different levels.

- The level 1 table is responsible for mapping a 4 MB chunk of the virtual address space, where each chunk consists of 1,024 contiguous pages. If every page in chunk `i` is unallocated, then level 1 PTE `i` is null. If at least one page in chunk i is allocated, then level 1 PTE `i` points to the base of a level 2 page table
- The level 2 table is responsible for mapping a 4-KB page of virtual memory to the physical address.

If a PTE in the level 1 table is `null`, then the corresponding level 2 page table does not even have to exist. Only the level 1 table needs to be in main memory at all times. The level 2 page tables can be created and paged in and out by the VM system as they are needed.

If a system has `k` levels of page tables, the virtual address is partitioned into `k` VPNs and a VPO. The `i`-th VPN is an index into a page table at level `i`. Each PTE in a level `j` table (`j < k`), points to the base of a page table at level `j + 1`. Each PTE in a level k table contains either the PPN of some physical page or the address of a disk block.
