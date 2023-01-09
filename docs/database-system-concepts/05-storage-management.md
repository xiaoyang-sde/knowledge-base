# Storage Management

## Overview

The disk-oriented database management system stores its data on a non-volatile disk. The database file is divided into pages, with the first page being the directory page. The buffer pool manager moves pages between disk and memory. The execution engine relies on the buffer pool to access pages. The design goal is to maximize sequential access. Even though the DBMS could use `mmap` to store the contents of a file into the address space of a program, most implementaions decide to avoid it.

## Data Representaion

- File storage: The DBMS stores a database as one or more files on disk in a proprietary format. The storage manager organizes the files as a collection of pages and schedules for reads and writes to improve spatial and temporal locality of pages.
  - Page: The database page is a fixed-size block of data with a unique identifier, which contains tuples, meta-data, indexes, and log records. The DBMS maps page identifiers to physical locations, such as file offsets. The DBMS could store pages in a heap, a tree, sequential, or hashing file organization.
  - Page header: Each page contains a header of metadata about the page's contents, such as page size, checksum, version, and transaction visiblities.
  - Heap file: The heap is an unsorted collection of pages where tuples are stored in random order. The DBMS maintains special pages that tracks the metadata of data pages, such as location and number of free slots.
- Tuple storage: The DBMS stores tuples in a page in different methods.
  - Tuple-oriented: The page contains a few slots for tuples and a slot list that maps slots to the tuples' starting position offsets. The header keeps track of the number of used slots and the offset of the last slot used. This approach could cause fragmentation and random disk I/O.
  - Log-structured: The page contains a chronological sequence of log records that contain changes to tuples. Each log record contains the tuple's unique identifier. The DBMS needs to compact pages to reduce wasted space. To read a tuple with a given identifier, the DBMS finds the latest log record with an index. This approach could use sequential disk I/O.
- Tuple representation: Each tuple is a sequence of bytes, which is prefixed with a header that contians meta-data. Each tuple is assigned a unique record identifier.
  - Overflow page: To store values that are larger than a page, the DBMS uses separate overflow storage pages.
  - External storage: To store values that can't fit in a page, the DBMS uses external files. The external files don't have transaction protections.
- System catalog: The DBMS stores meta-data about databases in its internal catalogs, such as tables, columns, indexes, users, permissions, and internal statistics. The catalog is stores as an internal table of the database.

## Storage Model

### Database Worklaod

- Online Transaction Processing (OLTP): Fast operations that read or write a small amount of data each time
- Online Analytical Processing (OLAP): Complex queries that read a lot of data to compute aggregates
- Hybrid Transaction and Analytical Processing (HTAP): OLTP and OLAP on the same database instance

### N-Ary Storage Model

The DBMS stores all attributes for a single tuple in a page. The model is optimized for OLTP workload, as it supports fast inserts, updates, and deletes. It is ideal because it takes one fetch to be able to get all of the attributes for a single tuple. It's not good for scanning large portions of the table to fetch a subset of the attributes.

### Decomposition Storage Model

The DBMS stores all values of a single attribute for all tuples in a page. The model is optimized for OLAP workload, as it supports fast scans over a subset of the table's attributes. To map the tuple to the attribute value, the DBMS could use fixed-length attribute value offset or embedded tuple identifiers.

### Database Compression

The DBMS could compress the data because disk I/O is the main bottleneck. Real-world data sets tend to have skewed distributions and attributes in a tuple might have high correlations. The goal of compression is to maintain lossless and postpone decompression for as long as possible during queries.

The naive compression uses general purpose algorithm that provides lower compression ratio in exchange for faster compress or decompress. Since accessing data requires decompression of compressed data, this limits the scope of the compression scheme. The naive approach also don't consider the high-level meaning or semantics of the data.

## Physical Storage

The hard disk drive contains the disk head and a few platters. The platters contains a few tracks. The track contains a few sectors or blocks. Data is transferred in the unit of disk block to amortize high access delay.

- Seek time: The time to move the disk head between tracks. The average time is 10ms.
- Rotational delay: For a disk with $n$ RPM, the average rotational delay is $\frac{1}{2} \cdot \frac{60}{n} \cdot 1000$ ms.
- Transfer time: For a disk with $n$ RPM and $k$ sectors per track, the time to read a sector is $\frac{1}{k} \cdot \frac{60}{n} \cdot 1000$ ms.
- Transfer rate: For a disk with $n$ RPM, $k$ sectors per track, and $m$ bytes per sector, the burst transfer rate is $\frac{n}{60} \cdot k \cdot m$.

## Memory Management

The DBMS aims to optimize for spacial control, in which it keeps pages that are used together as close as possible on disk, and temporal control, in which it reduces the number of times to read data from disk.

### Buffer Pool Manager

The buffer pool is a memory region of fixed-size pages, in which each entry is called a frame. When the DBMS requests a page, the page is placed into one of these frames. Modified pages are buffered and not written to disk at once.

The page table keeps track of pages that are in the buffer pool. It maintains meta-data of each page, such as a modified flag and a reference counter.

- Lock protects the database's logical contents from other transactions.
- Latch protects the critical sections of the DBMS's internal data structure from other threads.

#### Optimization

- Multiple buffer pool: The DBMS could use a buffer pool for each database to reduce latch contention. Each buffer pool can adopt local policies tailored for its data. The DBMS could map each page to their buffer pool with a separate object identifier or hashing.
- Pre-fetching: The DBMS could pre-fetch pages for a query plan that involves sequential reads.
- Scan sharing: The DBMS could reuse data retrieved from storage or operator computations. It allows multiple queries to attach to a single cursor that scans a table.
- Buffer pool bypass: The sequential scan opeartor might not store fetched pages in the buffer pool to avoid overhead. It works well if the operator needs to read a large sequence of pages that are continuous on disk.

### Replacement Policies

When the DBMS needs to free up a frame to make room for a new page, it must decide which page to evict from the buffer pool. The goal is to keep correctness, accuracy, speed, and reduce meta-data overhead. When a modified page is evicted, the page will be written to disk. The DBMS could use background writing, which scans the page table and write modified pages to disk.

- The clock assigns a reference bit to each page. When a page is accessed, the bit is set to `1`. When the clock moves past a page, the bit is set from `1` to `0` or the page is evicted. The clock remembers position between evictions.
- The LRU assigns a single timestamp of when each page was last accessed. When the DBMS needs to evict a page, it selects the one with the oldest timestamp. It keeps the pages in sorted order to reduce the search time on eviction. However, in some workloads such as sequential scans, the recent pages are the most unneeded, which pollute the buffer pool.
- The LRU-K tracks the last `K` references to each page and compute the interval between subsequent accesses. The DBMS estimates the next time that the page could be accessed.

## Index

### Ordered Index

The index stores the values of the search keys in sorted order and associates with each search key the records that contain it.

- Primary index: The index whose search key includes the unique primary key and is guaranteed not to contain duplicates.
- Secondary index: The index that is not a primary index and could contain duplicates.

The index entry consists of a search-key value and pointers to one or more records. The pointer contains the identifier of the disk block and an offset within the disk block to identify the record.

- Dense index: The index entry appears for every search-key values in the file.
- Sparse index: The index entry appears for some of the search-key values in the file. Sparse index could be used only if the index is a primary index.

### Multi-level Index

The multi-level index is an index with two or more levels. Searching for records with a multi-level index requires significantly fewer I/O operations than does searching for records by binary search.

### Index Update

#### Insertion

- Dense index
  - If the search-key value does not appear in the index, the system inserts an index entry with the search-key value in the index at the appropriate position.
  - If the index entry stores pointers to all records with the same search-key value, the system adds a pointer to the new record in the index entry.
  - If the index entry stores a pointer to only the first record with the search-key value, the system places the record being inserted after the other records with the same search-key values.
- Sparse index: If the system creates a new block, it inserts the first search-key value appearing in the new block into the index. If the new record has the least search-key value in its block, the system updates the index entry pointing to the block.

#### Deletion

- Dense index
  - If the deleted record was the only record with its particular search-key value, the system deletes the corresponding index entry from the index.
  - If the index entry stores pointers to all records with the same search-key value, the system deletes the pointer to the deleted record from the index entry.
  - If the index entry stores a pointer to only the first record with the search-key value and the deleted record was the first record with the search-key value, the system updates the index entry to point to the next record.
- Sparse index
  - If the index does not contain an index entry with the search-key value of the deleted record, nothing needs to be done to the index.
  - If the deleted record was the only record with its search key, the system replaces the corresponding index record with an index record for the next search-key value.
  - If the index entry for the search-key value points to the record being deleted, the system updates the index entry to point to the next record with the same search-key value.

### Secondary Index

Secondary index improves the performance of queries that use keys other than the search key of the clustering index, but it impose a overhead on modification of the database. Secondary index must be dense, with an index entry for every search-key value, and a pointer to every record in the file. If the search key of a secondary index is not a candidate key, it should contain the pointers to all records for each unique search-key value.

## Data Structure

### Static Hashing

- Linear Probe Hashing: The data structure is a table of slots which could hold elements. When hash collision occurs, it search for the next free slot in the table. To determine whether an element is present, hash to a location in the index and scan for it.
- Robin Hood Hashing: The data structure is the same as the linear probe hashing. When hash collision occurs, it search for the next free slot in the table. If a visited element's distance from the original index is smaller than the current element, it moves the visited element instead of the inserted element.
- Cuckoo Hashing: The data structure has multiple tables with different hasing function seeds. On insert, check all tables and pick a table that has a free slot. If no table has a free slot, evict the element from one of them and re-hash it to find a new location.

### Chained Hashing

Let $K$ denote the set of all search-key values, and let $B$ denote the set of all bucket addresses. The hash function $h$ is a function from $K$ to $B$. The $h(K_i)$ is the address of the bucket for the record $K_i$.

Hash index could efficiently handle equality queries, but it could not handle range queries, since the value in a certain range are scattered across multiple buckets.

- Insertion: Locate the bucket $h(K_i)$ and add the index entry for the record to the bucket. If the bucket doesn't have enough space, the system provides an overflow bucket and insert the record to it. The overflow buckets of a given bucket are chained together in a linked list.
- Deletion: Locate the bucket $h(K_i)$ and delete the index entry for the record from the bucket.

The set of buckets is fixed at the time the index is created. If the relation grows far beyond the expected size, hash index could be inefficient due to long overflow chains. The system could rebuild the index with a larger number of buckets, which could cause significant disruption to normal processing with large relations.

### Extendable Hashing

Extendable hashing copes with changes in database size by splitting and merging buckets as the database grows and shrinks. Extendable hashing requires an additional level of indirection, since the system must access the bucket address table before accessing the bucket itself.

The hash function generates values over $b$-bit binary integers, and the first $i$ bits are used by the table of bucket address. The value of $i$ grows and shrinks with the size of the database. Several consecutive table entires could point to the same bucket, thus each bucket $j$ has an integer $i_j$ that represents the length of the common hash prefix of its records.

#### Query and Update

To locate the bucket containing search-key value $K_l$, the system takes the first $i$ of $h(K_l)$, and locates the bucket $j$.

- If the bucket will overflow, the system increments $i_j$, moves records in the bucket to new buckets based on the hash values.
  - If $i = i_j$, the system increments $i$ and copy existing pointers to double the size of the address table.
  - If $i > i_j$, the system updates the pointer in the table to points to the new buckets.
- If the bucket will not overflow, the system inserts the record in the bucket.

To delete a record with the search-key value $K_l$, the system locates the bucket $j$, and removes the search key from the bucket.

- The bucket $j$ could be merged with other buckets if $i_j$ and the first $j - 1$ bits of its records is equal to other buckets.
- The table could shrink if $i_j$ for each bucket $j$ in the table is smaller than $i$.

### Linear Hashing

The hash table maintains a pointer that tracks the next bucket to split. ple hashes to find the right bucket for a given key. No matter whether this pointer is pointing to a bucket that overflowed, the DBMS splits. The overflow criterion is left up to the implementation.

- When any bucket overflows, split the bucket at the pointer location by adding a new slot entry, and create a new hash function.
- If the hash function maps to slot that has previously been pointed to by pointer, apply the new hash function.
- When the pointer reaches last slot, delete original hash function and replace it with a new hash function.

### B+ Tree

The B+ tree is a self-balancing tree data structure that keeps data sorted and allows searches, sequential access, insertions, and deletions in $O(log n)$. It's optimized for systems that read and write large blocks of data. Each node could have more than 2 children. The B+ tree is an $M$-degree search tree with the following properties:

- The tree is balanced. Each leaf node is at the same depth in the tree.
- Each node contains a map, where the value is differ based on whether the node is an inner node or a leaf node. The leaf node values could be record idenfiers or actual tuple data.
- Each node other than the root is at least half-full.
- Each inner node with $k$ elements has $k + 1$ non-null children.

#### Insert

- Find the leaf node $L$.
- Insert the data into $L$ in sorted order.
- If $L$ doesn't have enough space, split $L$ into $L$ and a new $L_2$, and then redistribute entries and push the middle key up.
- To handle duplicated elements, it could append record idenfier to element to make them unique, or overflow the leaf node.

#### Delete

- Find the leaf node $L$.
- Remove the element from $L$.
- If $L$ is not half-full, redistribute elements with its adjacent siblings withe the same parent.
- If redistribution fails, merge $L$ and sibling, and then delete the node element from parent of $L$.
