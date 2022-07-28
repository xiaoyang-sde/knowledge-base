# Partitioning

For large datasets and high throughput, replication is not sufficient. The data should be broken up into partitions, and each piece of data belongs to one partition. For queries that operate on a single partition, each node could execute the queries for its own partition.

## Partitioning and Replication

Partitioning is usually combined with replication so that copies of each partition are stored on multiple nodes. Each node could store more than one partition. Each partition's leader is assigned to one node, and its followers are assigned to other nodes. Each node could be the leader for some partitions and a follower for other partitions. The choice of partitioning scheme is independent of the choice of replication scheme.

## Key-Value Data

If the partitioning is unfair, so that some partitions have more data or queries than others. The presence of skew makes partitioning much less effective. The simplest approach would be to assign records to random nodes, but it's impossible to track which node a particular record is stored in.

### Partitioning with Range

Each partition handles a continuous range of keys. The partition of a given key could be located with the range boundaries. The partition boundaries might be chosen manually by an administrator, or the database can choose them automatically. Within each partition, the keys could be stored in sorted order, which makes range queries easier to implement. However, certain access patterns could lead to hot spots. If the database partitions time series data based on the timestamp, all the writes will be forwarded to the partition with the current timestamp.

### Partitioning with Hash

The hash function takes skewed data and makes it uniformly distributed. Each partition handles a range of hashes. The partition boundaries could be evenly spaced, or could be chosen pseudorandomly with consistent hashing. Consistent hashing is a method of distributing load across an internet-wide system of caches. It uses randomly chosen partition boundaries to avoid the need for central control or distributed consensus.

However, partitioning by hash of key makes range queries inefficient. The concatenated index approach declares a compound primary key consisting of several columns. The first part of the key is hashed to determine the partition, and the other columns are used as a concatenated index for sorting the data. For example, `(user_id, update_timestamp)` enables the partition of user data and efficient range queries of the feed updates of each user.

## Secondary Index

The secondary index doesn't identify a record uniquely but rather is a method to search for occurrences of a particular value. The problem with the secondary index is that it doesn't map to partitions.

- Partition with document: Each partition of the primary key index maintains a set of local secondary indexes. Reading from a document-paritioned index requires forwarding the queries to all partitions and merging the results. This approach is prone to tail latency amplification.
- Partition with term: The secondary indexes are partitioned based on their term or the hash of the term. The term is represented as `index:value`. Writing to a term-partitioned index requires a distributed transaction across all partitions, which makes the update asynchronous.

## Rebalancing Partitions

The process of moving load from one node in the cluster to another is called rebalancing. The rebalacing process requires that the database could continue accepting reads and writes and no more data than necessary could be moved between nodes.

## Strategies for Rebalancing

- **hash mod N**: It's best to divide the possible hashes into ranges and assign each range to a partition. However, if the number of nodes `N` changes, most of the keys will need to be moved from one node to another.
- **Fixed number of partitions**: Create more partitions than the number of nodes, and assign several partitions to each node. The number of partitions is usually fixed when the database is first set up and not changed afterward. If a node is added to the cluster, the new node could fetch a few partitions from the existing nodes until partitions are distributed. However, choosing the right number of partitions is difficult if the total size of the dataset is variable.
- **Dynamic partitioning**: When a partition grows to exceed a configured size, it is split into two partitions so that approximately half of the data ends up on each side of the split, and the new partitions could be transferred to another node to balance the load. If lots of data is deleted and a partition shrinks below some threshold, it can be merged with an adjacent partition. However, when the dataset is small, all writes have to be processed with a single node while the other nodes sit idle.
- **Proportional to nodes**: Create a fixed number of partitions per node. The size of each partition grows proportionally to the dataset size while the number of nodes remains unchanged. When a new node joins the cluster, it randomly chooses a fixed number of existing partitions to split, and then takes ownership of one half of each of those split partitions while leaving the other half of each partition in place.

## Request Routing

As partitions are rebalanced, the assignment of partitions to nodes changes. Figuring out which node a request should be forwarded to is an instance of a more general problem called service discovery, which isn’t limited to just databases.

- Allow clients to contact any node through a round-robin load balancer. If that node coincidentally owns the partition to which the request applies, it can handle the request directly; otherwise, it forwards the request to the appropriate node, receives the reply, and passes the reply along to the client.
- Send all requests from clients to a routing tier first, which determines the node that should handle each request and forwards it accordingly. This routing tier does not itself handle any requests; it only acts as a partition-aware load balancer.
- Require that clients be aware of the partitioning and the assignment of partitions to nodes. In this case, a client can connect directly to the appropriate node, without any intermediary.

The distributed data system relies on a separate coordination service such as ZooKeeper to keep track of the cluster metadata. Each node registers itself in ZooKeeper, and ZooKeeper maintains the authoritative mapping of partitions to nodes. Whenever a partition changes ownership, or a node is added or removed, ZooKeeper notifies the routing tier so that it can keep its routing information up to date.
