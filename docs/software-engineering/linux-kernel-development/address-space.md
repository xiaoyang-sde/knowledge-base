# Address Space

The process address space consists of the virtual memory addressable in a process and the addresses within the virtual memory that the process is allowed to use. Each process is given a flat 32-bit or 64-bit address space.

The process can access a memory address only in a valid memory area. Memory areas have associated permissions, such as readable, writable, and executable, that the associated process must respect. If a process accesses a memory address not in a valid memory area or if it accesses a valid area in an invalid manner, the kernel kills the process with the segmentation fault.

Each process contains multiple memory areas, such as a memory map of the executable file's code, a memory map of the `.data` and `.bss` sections, and memory mapped files. The memory areas do not overlap.

## The Memory Descriptor

The kernel represents a process's address space with a `struct mm_struct` structure. The `mm` field of `task_struct` is a pointer to the `mm_struct` of the process. The process could share its address space with a child process with the `CLONE_VM` flag of `clone()`, which creates a thread.

The `mm_struct` structures are organized into a linked list, where the initial element in the list is the `init_mm` memory descriptor, which describes the address space of the `init` process.

- The `mm_users` field is the number of threads using the address space.
- The `mm_count` field is the reference count for `mm_struct`.
- The `mmap` field is a linked list of the `vm_area_struct` in the address space sorted with address in ascending order.
- The `mm_rb` field is a red-black tree of the `vm_area_struct` in the address space.

When a process is scheduled, its process address space is loaded and the `active_mm` field of `task_struct` refers to the address space. The kernel thread doesn't have a process address space, so it doesn't have its page table. To prevent allocation, it reuses the address space of the previous process. The kernel thread uses the information about the kernel space in the `mm_struct`, which is the same for all processes.

## Virtual Memory Area

The kernel represents a memory area with the `struct vm_area_struct` structure, which describes a single memory area over a contiguous interval in an address space. Each memory area possesses certain properties, such as permissions and a set of associated operations.

- The `vm_start` field is the inclusive start of the area.
- The `vm_end` field is the exclusive end of the area.
- The `vm_flags` field contains properties about the pages contained in the area. For example, `VM_READ` means that the pages could be read from and `VM_SEQ_READ` is a hint that the application is performing sequential reads in the mapping.
- The `vm_ops` field points to the table of operations associated with the area, which the kernel could invoke to manipulate the area.

```c
struct vm_operations_struct {
  void open(struct vm_area_struct *area);
  void close(struct vm_area_struct *area);
  int fault(struct vm_area_sruct *area, struct vm_fault *vmf);
  int page_mkwrite(struct vm_area_sruct *area, struct vm_fault *vmf);
  int access(
    struct vm_area_struct *vma, unsigned long address,
    void *buffer, int length, int write
  );
};
```

- `find_vma()` searches for the area where a specific address resides.
- `find_vma_prev()` searches for the area such that it preceeds a specific address.
- `find_vma_intersection()` searches for the area such that it overlapds a specific interval.
- `do_mmap()` creates a linear address interval in a specific process address space that is either merged with an existing area or created as a new area. The function maps an optional file at a specific offset for a specific length. The `flags` parameter specifies properties and permissions.
- `do_munmap()` removes a linear address interval from a specific process address space.

## Page Table

When an application accesses a virtual address, it must be converted to a physical address. Performing the lookup is done through page tables. The kernel uses the three-level page table, which enables a sparse address space. Each virtual address is split into chunks, where each chunk is used as an index into a table. The table points to either a low-level table or a physical page.

Each process has a page table. The `pgd` field of `mm_struct` points to the address's first-level page table. Manipulating and traversing page tables requires the `page_table_lock`.
