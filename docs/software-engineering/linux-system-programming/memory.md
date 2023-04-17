# Memory

## The Process Address Space

Linux virtualizes its physical resource of memory. The kernel associates each process with a unique virtual address space, which is linear, with addresses starting at zero, increasing to some maximum value.

For the purposes of memory management, a page is the smallest addressable unit of memory that the memory management unit (MMU) can manage. The machine architecture determines the page size. Each valid page is associated with an actual page of data, either in physical memory (RAM) or on secondary storage, such as a swap partition or file on disk.

- When a process attempts to access an invalid page, the kernel terminates the process with a segmentation fault error.
- When a process attempts to access a page on secondary storage, the memory management unit generates a page fault, and the kernel moves the data from secondary storage to physical memory.

Multiple pages of virtual memory, even in different virtual address spaces, could map to a single physical page. The shared data could be readable, writable, or both readable and writable. When a process writes to a shared writable page, either the kernel modifies the page or the kernel clones the page for the writing process and modifies it, which is the approach of copy-on-write.

The kernel arranges pages into memory regions that share certain properties, such as access permissions. Certain types of memory regions can be found in a process, such as the text, the stack, the heap, and the data segment, which contains uninitialized global variables.

## Allocating Dynamic Memory

The foundation of the memory management system is the allocation, use, and eventual return of dynamic memory. Dynamic memory is allocated at runtime, not compile time, in sizes that could be unknown until the moment of allocation. Memory leak and use-after-free are common issues in a C program.

- `malloc()` in `glibc` allocates `size` bytes of memory and returns a pointer to the start of the allocated region. The content of the memory are undefined and might not be zeroed.
- `calloc()` in `glibc` allocates `nr * size` bytes of memory and returns a pointer to the start of the allocated region. The content of the memory are zeroed.

```c
#include <stdlib.h>

void* malloc(size_t size);
void* calloc(size_t nr, size_t size);
```

- `realloc()` in `glibc` resizes the memory region at `ptr` to a new size of `size` bytes. Instead of growing the region in place, the function might reallocate a new region, clone the content, and free the old region.

```c
void* realloc(void* ptr, size_t size);
```

- `free()` in `glibc` frees the memory region allocated with `malloc()`, `calloc()`, or `realloc()`. The `ptr` must be point to the start of a memory region.

```c
void free(void* ptr);
```

- `mallopt()` in `glibc` sets memory allocation parameters. The list of parameters are documented in [mallopt(3)](https://man7.org/linux/man-pages/man3/mallopt.3.html).

```c
#include <malloc.h>

int mallopt(int param, int value);
```

## Anonymous Memory Mapping

Memory allocation in `glibc` uses a combination of the heap segment and memory mappings. The heap segment is managed with buddy memory allocation algorithm, which results in internal and external fragmentation. `glibc` adjusts the size of the heap segment based on the usage.

- `brk()` sets the end of the heap segment to the address at `end`.
- `sbrk()` increments the heap segment with `increment` bytes, which might be positive or negative.

```c
#include <unistd.h>

int brk(void* end);
void* sbrk(intptr_t increment);
```

When allocating a large chunk of memory region, `glibc` does not use the heap because it might lead to fragmentation and prevent the heap from shrinking. Instead, `glibc` creates an anonymous memory mapping, which is mapped to a zero-filled block of memory. The default threshold is 128 KB.

- `mmap()` creates a mapping in the virtual address space. If the `MAP_ANONYMOUS` flag is set, the mapping is not based on a file, thus the `fd` and `offset` arguments are ignored. The content of the memory are zeroed.

- `munmap()` frees the memory region allocated with `mmap()`.

```c
#include <sys/mman.h>

void* mmap(
  void* start, size_t length,
  int prot, int flags,
  int fd, off_t offset
);
int munmap(void* start, size_t length);

mmap(
  NULL, 512 * 1024, PROT_READ | PROT_WRITE,
  MAP_ANONYMOUS | MAP_PRIVATE, −1, 0
)
```

## Manipulating Memory

- `memset()` sets the `n` bytes starting at `s` to the byte `c` and returns `s`.

```c
#include <string.h>

void* memset(void* s, int c, size_t n);
```

- `memcmp()` compares two chunks of memory for equivalence, which is similar to `strcmp()`.

```c
int memcmp(const void* s1, const void* s2, size_t n);
```

- `memmove()` copies the first `n` bytes of `src` to `dst`, returning `dst`. `src` and `dst` can overlap.

```c
void* memmove(void* dst, const void* src, size_t n);
```

- `memcpy()` copies the first `n` bytes of `src` to `dst`, returning `dst`. `src` and `dst` can't overlap.

```c
void* memcpy(void* dst, const void* src, size_t n);
```

## Locking Memory

Linux implements demand paging, which means that pages are paged in from disk as needed and paged out to disk when no longer needed. The secondary storages provide the illusion of an infinite amount of physical memory.

However, applications with timing constraints require deterministic behavior. If memory accesses result in page faults, which incur disk I/O operations, applications might overrun their timing needs. Furthermore, if secrets are kept in memory, the secrets can be paged out and stored unencrypted on disk.

- `mlock()` locks the virtual memory starting at `addr` and extending for `len` bytes into physical memory. It locks all pages that contain the requested memory region.

- `mlockall()` locks a process's entire address space into physical memory.
  - `MCL_CURRENT` instructs `mlockall()` to lock all current pages.
  - `MCL_FUTURE` instructs `mlockall()` to lock future pages.

```c
#include <sys/mman.h>

int mlock(const void* addr, size_t len);
int mlockall(int flags);
```

- `munlock()` unlocks the virtual memory starting at `addr` and extending for `len` bytes from physical memory.
- `munlockall()` unlocks a process's entire address space from physical memory.

```c
#include <sys/mman.h>

int munlock (const void* addr, size_t len);
int munlockall(void);
```

## Opportunistic Allocation

Linux employs an opportunistic allocation strategy. When a process requests additional memory from the kernel, the kernel commits to the memory without providing physical storage. When the process writes to the newly allocated memory, the kernel converts the commitment for memory to a physical allocation of memory. The kernel does this on a page-by-page basis, performing demand paging and copy-on-writes as needed. Therefore, the amount of committed memory can exceed the amount of physical memory and swap space available.

When overcommitment results in insufficient memory to fulfill a committed request, the OOM killer picks a process to terminate.
