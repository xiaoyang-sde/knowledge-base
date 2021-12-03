# Storage Management

## Index

### Ordered Index

The ordered index stores the values of the search keys in sorted order and associates with each search key the records that contain it.

- Primary index: The index whose search key includes the unique primary key and is guaranteed not to contain duplicates.
- Secondary index: The index that is not a primary index and could contain duplicates.

The index entry consists of a search-key value and pointers to one or more records. The pointer contains the identifier of the disk block and an offset within the disk block to identify the record.

- Dense index: The index entry appears for every search-key values in the file.
- Sparse index: The index entry appears for some of the search-key values in the file. Sparse index could be used only if the index is a primary index.

#### Multi-level Index

The multi-level index is an index with two or more levels. Searching for records with a multi-level index requires significantly fewer I/O operations than does searching for records by binary search.

#### Index Update

##### Insertion

- Dense index
  - If the search-key value does not appear in the index, the system inserts an index entry with the search-key value in the index at the appropriate position.
  - If the index entry stores pointers to all records with the same search-key value, the system adds a pointer to the new record in the index entry.
  - If the index entry stores a pointer to only the first record with the search-key value, the system places the record being inserted after the other records with the same search-key values.
- Sparse index: If the system creates a new block, it inserts the first search-key value appearing in the new block into the index. If the new record has the least search-key value in its block, the system updates the index entry pointing to the block.

##### Deletion

- Dense index
  - If the deleted record was the only record with its particular search-key value, the system deletes the corresponding index entry from the index.
  - If the index entry stores pointers to all records with the same search-key value, the system deletes the pointer to the deleted record from the index entry.
  - If the index entry stores a pointer to only the first record with the search-key value and the deleted record was the first record with the search-key value, the system updates the index entry to point to the next record.
- Sparse index
  - If the index does not contain an index entry with the search-key value of the deleted record, nothing needs to be done to the index.
  - If the deleted record was the only record with its search key, the system replaces the corresponding index record with an index record for the next search-key value.
  - If the index entry for the search-key value points to the record being deleted, the system updates the index entry to point to the next record with the same search-key value.

#### Secondary Index

Secondary index improves the performance of queries that use keys other than the search key of the clustering index, but it impose a overhead on modification of the database. Secondary index must be dense, with an index entry for every search-key value, and a pointer to every record in the file. If the search key of a secondary index is not a candidate key, it should contain the pointers to all records for each unique search-key value.

### Hash Index

#### Static Hashing

Let $K$ denote the set of all search-key values, and let $B$ denote the set of all bucket addresses. The hash function $h$ is a function from $K$ to $B$. The $h(K_i)$ is the address of the bucket for the record $K_i$.

Hash index could efficiently handle equality queries, but it could not handle range queries, since the value in a certain range are scattered across multiple buckets.

- Insertion: Locate the bucket $h(K_i)$ and add the index entry for the record to the bucket. If the bucket doesn't have enough space, the system provides an overflow bucket and insert the record to it. The overflow buckets of a given bucket are chained together in a linked list.
- Deletion: Locate the bucket $h(K_i)$ and delete the index entry for the record from the bucket.

The set of buckets is fixed at the time the index is created. If the relation grows far beyond the expected size, hash index could be inefficient due to long overflow chains. The system could rebuild the index with a larger number of buckets, which could cause significant disruption to normal processing with large relations.

#### Extendable Hashing

Extendable hashing copes with changes in database size by splitting and merging buckets as the database grows and shrinks. Extendable hashing requires an additional level of indirection, since the system must access the bucket address table before accessing the bucket itself.

##### Data Structure

The hash function generates values over $b$-bit binary integers, and the first $i$ bits are used by the table of bucket address. The value of $i$ grows and shrinks with the size of the database. Several consecutive table entires could point to the same bucket, thus each bucket $j$ has an integer $i_j$ that represents the length of the common hash prefix of its records.

##### Query and Update

To locate the bucket containing search-key value $K_l$, the system takes the first $i$ of $h(K_l)$, and locates the bucket $j$.

- If the bucket will overflow, the system increments $i_j$, moves records in the bucket to new buckets based on the hash values.
  - If $i = i_j$, the system increments $i$ and copy existing pointers to double the size of the address table.
  - If $i > i_j$, the system updates the pointer in the table to points to the new buckets.
- If the bucket will not overflow, the system inserts the record in the bucket.

To delete a record with the search-key value $K_l$, the system locates the bucket $j$, and removes the search key from the bucket.

- The bucket $j$ could be merged with other buckets if $i_j$ and the first $j - 1$ bits of its records is equal to other buckets.
- The table could shrink if $i_j$ for each bucket $j$ in the table is smaller than $i$.
