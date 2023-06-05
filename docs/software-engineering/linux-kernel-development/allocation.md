# Allocation

## Page

The kernel treats physical pages as the basic unit of memory management. The memory management unit is the hardware that manages memory and performs virtual to physical address translations. The kernel represents each physical page with a `struct page` structure, since it needs to know whether a page is free and who owns the page.

- The `flags` field stores the status of the page, such as whether it's written to or it's locked.
- The `_count` field stores the usage count of the page. Each page can be used as page cache, private data, or a mapping in a process's page table.
- The `virtual` field is the page's virtual address.

## Zone

Because of hardware limitations, the kernel can't treat all pages as identical. The kernel uses the zones to group pages of similar properties. The kernel represents each zone with a `struct zone` structure.

- `ZONE_DMA` contains the pages that supports DMA.
- `ZONE_DMA_32` contains the pages that supports DMA and are accessible with a 32-bit device.
- `ZONE_NORMAL` contains normal pages.
- `ZONE_HIGHMEM` contains pages that can't mapped into the kernel's address space due to the limited address range of a 32-bit device.

## Slab

The kernel needs to allocate and deallocate fixed-size objects, such as `task_struct` or `inode`. Instead of using a general allocator, the kernel implements a slab allocator. It divides different objects into caches, where each of which stores a different type of object. The kernel represents each cache with a `struct kmem_cache` structure.

The caches are divided into slabs, which are composed of one or more contiguous pages. The kernel represents each slab with a `struct slab` structure. The slab allocator creates slabs with a low-level kernel page allocator, such as `alloc_pages()`.

Each slab contains some number of objects, which are the data structures being cached. When the kernel requests a new object, the request is satisfied from a partial slab. If there exists no partial slab available, one is created.

- `kmem_cache_create()` creates a cache that could hold objects with a specific size and returns a pointer to the created cache.
- `kmem_cache_alloc()` allocates an object from the cache and returns a pointer to the object.
- `kmem_cache_free()` deallocates an object and returns it to its slab.

## Page Allocation

- `alloc_pages()` allocates contiguous $2^{\text{order}}$ pages and returns a pointer to the first page's `page` structure. The allocated page contains garbage data and should be cleaned.
- `page_address()` returns a pointer to the logical address of the physical page.
- `__free_pages()` frees $2^{\text{order}}$ pages.

```c
struct page *alloc_pages(gfp_t gfp_mask, unsigned int order);
void *page_address(struct page *page);
void __free_pages(struct page *page, unsigned int order);
```

## `kmalloc()` and `kfree()`

- `kmalloc()` allocates `size` bytes and returns a pointer to the first byte. It guarantees that the allocated chunk of bytes are contiguous in both the physical address space and the virtual address space. It accepts flags that represent access modifiers, zone modifiers, and types.
- `kfree()` deallocates the bytes allocated with `kmalloc()`.

```c
void *kmalloc(size_t size, gfp_t flags);
void kfree(const void* ptr);
```

- The access modifier specifies how the kernel is supposed to allocate the requested bytes. For example, `__GFP_WAIT` indicates that the allocator could sleep.
- The zone modifier specifies which zone the allocation should originate. For example, `__GFP_DMA` indicates that the allocator should allocate from `ZONE_DMA`.
- The type specifies the required action and zone modifiers to fulfill a particular type of transaction. For example, `GFP_KERNEL` should be used in process context when it's safe to sleep, which indicates that the allocation is normal and might block.

## `vmalloc()`

- `vmalloc()` allocates `size` contiguous bytes and returns a pointer to the first bytes. It guarantees that the allocated chunk of bytes are contiguous in the virtual address space.
- `vfree()` deallocates the bytes allocated with `vmalloc()`.

```c
void *vmalloc(unsigned long size);
void vfree(const void *addr);
```
