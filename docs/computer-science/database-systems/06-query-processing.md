# Query Processing

## Join Operation

### Nested-Loop Join

The nested-loop join algorithm consists of a pair of nested for loops. To compute $r \Join_{\theta} s$ of two relations $r$ and $s$, the algorithm examines each pair of the records in the two relations, and test if they satisfy the join condition $\theta$.

Let $M$ be the number of disk blocks the buffer could store. Let the number of pairs of records be $n_r$ and $n_s$, and let the number of disk blocks be $b_r$ and $b_s$. For each record or disk block in $r$, the algorithm scans all records in $s$.

- The algorithm scans all records in $s$ for each record in $r$, the cost is $b_r + n_r \cdot b_s$.
- The algorithm scans all records in $s$ for each disk block in $r$, the cost is $b_r + b_r \cdot b_s$.
- The algorithm scans all records in $s$ for $M - 2$ disk blocks in $r$, the cost is $b_r + \lceil \frac{b_r}{M - 2} \rceil \cdot b_s$.
  - The input of $r$ reserves $M - 2$ blocks in the buffer.
  - The input of $s$ reserves a block in the buffer.
  - The result reserves a block in the buffer.

### Index Join

The index join algorithm replaces the inner loop of the nested-loop join algorithm with index lookups. For each record $t_r$, the index is used to look up records in $s$ that satisfies the join condition. The buffer stores up to $M - 3$ disk blocks of index to optimize the performance.

- The cost of scanning the relation $r$ is $b_r$.
- Let $f$ be the fraction of the index stored in the buffer. The cost of looking up the index is $f \cdot n_r$.
- Let $k$ be the number of matched records in $s$ for each record in $r$. The cost of reading matched records is $k \cdot n_r$.

### Hash Join

The hash join algorithm partitions the records of each relation into sets that have the same hash value on the join attributes. Let $h$ be the hash function that maps the join attributes to $0, 1, \dots, n_h$. Let $r_0, \dots, r_{n_h}$ and $s_0, \dots, s_{n_h}$ be the partitions of $r$ and $s$. The records in $r_i$ should be compared with records in $s_i$.

- Hash stage: The algorithm reads all records in $r$ and $s$ and hashes them into $k = M - 1$ buckets. The cost of the hash stage is $2 \cdot b_r + 2 \cdot b_s$.
- Join stage: The algorithm reads all blocks in the partitioned relations. The cost of the join stage is $b_r + b_s$.

If the size of the partitioned block is larger than $M - 2$, the algorithm will recursively partition the records with $\lceil log_{M - 1} \frac{b_R}{M - 2} \rceil$. The cost of the hash stage is $\lceil log_{M - 1} \frac{b_R}{M - 2} \rceil \cdot 2 \cdot (b_r + b_s)$.

### Sort-Merge Join

The sort-merge join algorithm sorts $r$ and $s$ on their common attributes, and join the relations similar to the merge stage in the merge-sort algorithm.

- Sort stage: The algorithm reads $M$ disk blocks from $s$ or $r$ to the buffer, sort the batch, and write the result to the disk.
  - The cost of sorting $r$ is $2 \cdot b_r \cdot (\lceil log_{M - 1} \frac{b_r}{M} \rceil + 1)$.
  - The cost of sorting $s$ is $2 \cdot b_s \cdot (\lceil log_{M - 1} \frac{b_s}{M} \rceil + 1)$.
- Join stage: The algorithm joins the two relations by maintaining two pointers to blocks $r_i$ and $s_i$, and move the pointers similar to the merge-sort algorithm. The cost of the join stage is $b_r + b_s$.
