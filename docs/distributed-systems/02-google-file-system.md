# Google File System

The Google File System is a scalable distributed file system for distributed data-intensive applications. The design has been driven by key observations of the application workloads and environment.

## Architecture

The GFS cluster consists of a single master and multiple chunkservers and is accessed by multiple clients. Each file is divided into fixed-size chunks, and each chunk is identified by a unique chunk handle. Each chunk has three replicas in different chunkservers.

- **master**: The master maintains the file system metadata, which includes the namespace, access control, the mapping from files to chunks, and the locations of chunks. The master controls chunk lease management, garbage collection, and chunk migration. The master communicates with each chunkserver in `HeartBeat` messages.
  - The master doesn't keep a persistent record of the locations of chunks. It periodically retrieves the information from the chunkservers.
  - The master persists the record of critical metadata changes in the replicated operation log. The master checkpoints its state in a B-tree to reduce the log size. The master recovers its file system state by reading the latest checkpoint and replaying the operation log.
- **chunkserver**: Each chunkserver stores chunks on local disk and reads or writes chunk data specified by a chunk handle and byte range.
- **client**: The client communicates with the master for metadata operations, and directly communicates with the chunkserver for data operations. The client caches the metadata.

### Consistency Model

- The file namespace mutations are atomic because of the namespace lock in the master.
- The file data mutations depends on the status of the operation. The region is **consistent** if the clients will see the same data across all replicas. The region is **defined** if the clients will see consistent data and what the mutation writes in its entirety across all replicas.
  - Successful serial write: defined
  - Successful concurrent write: consistent and undefined (mixed fragments from multiple mutations)
  - Successful record append: defined interspersed with inconsistent (padding or duplicate regions bewteen consistent records)
  - Failed write or record append: inconsistent and undefined

## Implementation

### Chunk Lease

The mutation is an operation that changes the contents or metadata of a chunk. The master grants a chunk lease to the primary replica, and it picks a serial order for all mutations to the chunk. The other replicas follow the order when applying mutations. The lease has an initial timeout of 60 seconds, but the primary could request an extension.

- **write**: The data is written at application-specified file offset.
- **append**: The data is appended atomically at least once even in the presence of concurrent mutations.

### Write Operation

- The client retrieves the chunkserver that holds the current lease for the chunk and the locations of other replicas. The master grants the lease if it doesn't exist.
- The client pushes the data to all replicas, and each chunkserver stores the data in a LRU buffer cache
- The client sends a write request to the primary. The primary assigns serial numbers to all the mutations it receives and applies the mutation to its own local state.
- The primary forwards the write request to all secondary replicas. Each secondary replica applies mutations in the same serial order and reports to the primary. The primary reports the success mutation to the client.

### Read Operation

- The client translates the file name and byte offset specified by the application into a chunk index within the file and sends a request to the master.
- The master replies with the cooresponding chunk handle and locations of the replicas.
- The client caches the locations using the file name and chunk index as the key.
- The client sends the chunk handle and byte range to the closest replica to read the data.

### Snapshot Operation

The snapshot operation makes a copy of a file or a directory tree instantaneously.

- The master revokes the lease on the chunks in the file
- The master logs the operation to disk and duplicates the metadata that point to the same chunks as the source file.
- When a client sends a mutation request to a duplicated chunk `C`, the master picks a new chunk and ask each chunkserver that has a replica of `C` to create a new chunk `C'` to handle the request.

### Chunk Replication

- Creation: The master creates the replicas of a chunk on specific chunkservers based on these factors: disk space utilization, the number of recent transactions, and the rack location.
- Re-replication: The master re-replicates a chunk when the number of available replicas falls below a specified goal. It prioritizes the chunks that are frequently accessed or distant from the replication goal.
- Rebalance: The master examines the replica distribution and moves replicas for better disk space and load balancing.

### Garbage Collection

- The application deletes a file, and the master logs the deletion and rename the file to a hidden name.
- During the master's regular scan of the file system namespace, it removes the metadata of hidden files if they have existed for more than 3 days.
- During the master's regular scan of the chunk namespace, the master erases the metadata of orphaned chunks, and notify the chunkserver to delete these chunks.

### Stale Replica Deletion

The chunk replica could become stale if a chunkserver fails and misses mutations to the chunk. The master maintains a chunk version number for each chunk.

When the master grants a new lease on a chunk, it increases its version number. The replicas record the version number before writing to the chunk. The master could detect stale chunks when a chunkserver restarts and reports its chunk status and remove them with garbage collection.

## Fault Tolerance

### High Availability

- Fast recover: The master and the chunkserver could restore their state and start in seconds if they are terminated.
- Chunk replication: Each chunk is replicated on multiple chunkservers.
- Master replication: The operation log and checkpoints are replicated on multiple machines. The mutation to the state is committed after the log record has been flushed to disk on all master replicas.

### Data Integrity

Each chunkserver uses checksumming to detect corruption of stored data. Each chunk is broken up into 64 KB blocks, and each block has a corresponding 32 bit checksum stored in the memory and log. The chunkserver verifies the checksum of data blocks before sending them to the client or another chunkserver. If the block doesn't match the checksum, the master will clone the chunk from another replica.
