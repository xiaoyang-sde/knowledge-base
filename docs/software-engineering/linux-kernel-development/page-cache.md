# Page Cache

The kernel implements the page cache to minimize disk I/O. The page cache consists of physical pages in RAM, the contents of which correspond to physical blocks on a disk. The size of the page cache is dynamic. Whenever the kernel begins a read operation, it checks if the requisite data is in the page cache, and the kernel won't access the disk if there's a cache hit.

For write operations, the kernel implements the write-back approach, where processes perform write operations on the page cache. The pages that are written to will be written back to the disk in a `writeback` process.

The kernel evicts the clean pages to make room for more relevant cache entries or to shrink the cache. If insufficient clean pages are in the cache, the kernel forces a writeback to make more clean pages available. The kernel implements the two-list approach to determine the cache entries to be evicted. The active list contains the pages that are not available for eviction. The inactive list contains the pages that are available for eviction. Both lists are maintained in a pseudo-LRU manner. If the active list becomes much larger than the inactive list, items from the active list's head are moved to the inactive list, making them available for eviction.
