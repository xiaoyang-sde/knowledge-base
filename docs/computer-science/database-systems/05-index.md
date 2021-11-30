# Index

## Ordered Index

The ordered index stores the values of the search keys in sorted order and associates with each search key the records that contain it.

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
