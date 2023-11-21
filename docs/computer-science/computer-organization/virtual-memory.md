# Virtual Memory

Virtual memory allows efficient and safe sharing of memory among several programs and uses the main memory as a cache for secondary storage. Each program is compiled into its own address space, which is a separated range of memory locations accessible to this program. Virtual memory implements the translation of a program's address space to physical addresses, which enforces protection of a program's address space from other virtual machines.

Each virtual memory block is a page, and a virtual memory miss is called a page fault. With virtual memory, the processor produces a virtual address, which is translated by a combination of hardware and software to a physical address.

## Sv39 Page Table

The page table records the mapping between virtual page number and physical page number. Each program has its own page table, which maps the virtual address space of that program to main memory. The hardware contains a page table base register (PTBR) that stores the address of the page table.

## Translation

The virtual address is broken into a virtual page number and a page offset.

```rs
/// The `PageSegment` struct represents a consecutive range of pages,
/// which are mapped to frames in the same method (`Identical` or `Framed`)
/// and have the same permissions.
#[derive(Clone)]
pub struct PageSegment {
    page_range: PageRange,
    frame_map: BTreeMap<PageNumber, Arc<FrameTracker>>,
    map_type: MapType,
    map_permission: MapPermission,
}

pub struct PageSet {
    page_table: PageTable,
    segment_list: Vec<PageSegment>,
}
```
