# Memory Virtualization

## The Abstraction: Address Spaces

The abstraction of address space is the running program's view of the memory in the system. The address space contains all of the memory state of the running program, including the code, the stack, and the heap. The system virtualizes memory to create the illusion of a large, isolated, and sparse memory space for each process and achieve transparency (invisible to the running program), efficiency, and protection.

## Interlude: Memory API

- **Stack**: The allocation and deallocation of the stack memories are managed by the compiler. The variable declarations in a function allocates memories in the stack, when the function returns, these memories are deallocated.
- **Heap**: The allocation and deallocation of the heap memories requires explicit `malloc` API calls. The address of the memories allocated by `malloc` are stored in the stack.

The `malloc()` call takes a parameter `size_t size` which describes the number of bytes to be allocated and returns a pointer with the `void` type that points to the allocated heap memory. The pointer needs to be casted to the desired type. The `sizeof()` operator returns the amount of space for specifc type.

The `free()` call deallocates the memory allocated by `malloc()`.

```c
int *x = (int *) malloc(10 * sizeof(int));
free(x);
```

## Mechanism: Address Translation

The address translation mechanism in the hardware translates the virtual address in each memory access instruction to a physical address.

The base and bounds technique (dynamic relocation) technique specifies a base register and a bounds register in the memory management unit. The base register holds the start of the physical address of the current process, and the bounds register holds the size of the address space. The technique could cause internal fragmentation because the space inside the fixed-size allocated unit is not all used and thus wasted.

$$\text{physical\_address} = \text{virtual\_address} + \text{base}$$

- When a process is created, the system searches the free list data structure to find space for its address space.
- When a process is terminated, the system puts its memory back on the free list and cleans up associated data structures.
- When a context switch occurs, the system saves the base and bounds registers of the current process into its process control block, and restores their values for the next process.
- When the CPU raises an exception (out-of-bound access, etc.), the system terminates the process to protect the machine.

## Segmentation

The segmentation technique divides the address space into segments (code, stack, and heap) and maintains the base, the size, the direction of growth, and the protection bits for each segment. The technique could cause external fragmentation because there are unallocated spaces between segments.

$$\text{physical\_address} = \text{segment\_offset} + \text{base}$$

The system extracts the first few bits of a virtual address to determine which segment it is referring to. The system shares certain memory segments between address spaces with the protection bits, which specifies whether or not a process could read, write, or execute the segment.

## Paging: Introduction

The paging technique divides the virtual address space into fixed sized pages. The physical memory is an array of fixed-sized page frames, and each frame contains a single virtual-memory page. To translate the virtual address into the physical address, the system splits the virtual address into the **virtual page number** and the **offset** within the page, and then replaces the virtual page number with the **physical frame number** based on the page table. The technique could introduce extra memory accesseses and fill the memory with large page tables.

The **linear page table** is an array which maps a virtual page number to a page-table entry. The page-table entry contains a few bits to indicate the state of the page.

- **Valid bit**: whether the particular translation is valid
- **Protection bit**: whether the page could be read, written, or executed
- **Present bit**: whether the page is in physical memory or on disk (**swapped out**)
- **Dirty bit**: whether the page has been modified since it was brought into memory
- **Reference bit**: whether the page has been accessed

## Paging: Faster Translation (TLB)

The translation-lookaside buffer (TLB) inside the MMU is a hardware cache of virtual-to-physical address translations. The system extracts the virtual page number from the virtual address and check if the TLB holds the translation. If there's a TLB miss, the hardware raises an exception to raise the priviledge level to kernel mode and jumps to a trap handler that retrieves the translation from the page table and updates the TLB, and then the system resumes execution at the instruction that caused trap.

The TLB could hold translations from different processes at the same time since each entry contains an **address space identifier** that identifies the process that it belongs to. The TLB could use LRU or random cache replacement policy if the entries reach its capacity.

- **Valid bit**: whether the entry has a valid translation
- **Protection bit**: whether the page could be read, written, or executed
- **Dirty bit**: whether the page has been written to
- **Address space identifier**: the identifier of the process that could use the translation

## Paging: Multi-level Page Table

The multi-level page table technique divides the page table into page-sized units and tracks the address of each page of the page table with the page directory structure. The multi-level page table allocates page-table space in proportion to the amount of address space the process is using to compact the size of the page table.

The technique divides the virtual memory number into the **page-directory index** and the **page table index**. The system locates the page directory entry with the page-directory index and extracts the page-frame number. The system uses the page-frame number to retrieve a specific page of the page table and locates the page-table entry that holds the physical address with the page table index.
