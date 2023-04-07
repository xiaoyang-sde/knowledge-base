# Replication

Replication means keeping a copy of the same data on multiple machines that are connected via a network.

- To keep data geographically close to the users
- To allow the system to continue working even if some of its parts have failed
- To scale out the number of machines that can serve read queries

## Leaders and Followers

Each node that stores a copy of the database is called a replica. To ensure that the replicas contain the same data, they have to process every write to the database. The leader-based replication is the most common solution.

- Leader replica handles the writes to the database, and sends the data change to all followers as part of a replication log.
- Follower replica takes the log from the leader and update its local copy of the database.
- The client could write to the leader replica or read from any replica.

### Synchronous and Asynchronous Replication

- **Synchronous replication**: The leader waits until the followers have confirmed the update before reporting success to the user.
  - The follower is guaranteed to be consistent with the leader.
  - If the follower doesn't respond, the leader must block all writes.
- **Asynchronous replication**: The leader sends the update to followers, but don't wait for their response.
  - The leader could process writes even if all followers have fallen behind.
  - If the leader fails and is not recoverable, any writes that have not been replicated to followers are lost.
- **Semi-synchronous replication**: One of the followers is synchronous, and the others are asynchronous. If the synchronous follower becomes unavailable, another follower is made synchronous.

### Setting Up New Followers

1. Take a consistent snapshot of the leader's database.
2. Copy the snapshot to the new follower node.
3. The follower connects to the leader and requests all data changes that have happened since the snapshot was taken.
4. After the follower has processed the backlog of data changes since the snapshot, it can continue to process data changes from the leaders.

### Follower failure (Catch-up recovery)

Each follower keeps a log of the data changes it has received from the leader. If the follower crashes and is restarted, the follower could know the last transaction that was processed, and connect to the leader to request all dta changes that occurred during the down time.

### Leader failure (Failover)

1. Determine that the leader has failed: If the leader doesn't respond for some period of time, it's assumed to be dead.
2. Choose a new leader: The new leader is chosen through a election process. The best candidate is the replica with the most up-to-date data changes from the old leader.
3. Reconfigure the system: The write requests should be routed to the new leader. The old leader should become a follower and recognizes the new leader.

### Implementation of Replication Logs

#### Statement-based replication

In the relational database, the leader could log write requests that it executes and send the SQL statement logs to the followers.

- Statement that calls a nondeterministic function, such as `RAND()` will generate different value on each replica
- Statement could have side effects (e.g. triggers, stored procedures)
- STatement that depends on the existing data in the database must be executed in the same order on each replica

#### Write-ahead log (WAL) shipping

The leader maintains a log that is an append-only sequence of bytes containing all writes to the database and sends it to the followers. When the follower process the log, it builds a copy of the exact same data structure as the leader.

#### Logical (row-based) log replication

The logical log for a relational database is usually a sequence of records describing writes to database tables at the granularity of a row.

- Inserted row: The new values of all columns
- Deleted row: The primary key or old values of all columns
- Updated row: The primary key and the new values of all columns

#### Trigger-based replication

The trigger in the database is executed when a data change occurs. The trigger could log the change into a separate table, which could be read by an external process. The external process could apply any transformation and replicate the data change to another system.

### Problems with Replication Lag

In the read-scaling architecture, the capacity for serving read-only requests could be increased by adding more followers with asynchronous replication. The **eventual consistency** is a guarantee that when an update is made in the leader, the update will eventually be reflected in all followers. However, when the lag is large, the inconsistencies become a real problem for applications.

- **Read-after-write** is a guarantee that if the user reloads the page, they will always see the update they submitted.
  - When reading the data the user may have modified, read it from the leader.
  - Track the time of the last update, and for a specific amount of time after the last update, make all reads from the leader.
  - The client remembers the timestamp of its most recent write, and the system ensures that the replica serving any reads for that user reflects updates until that timestamp.
- **Monotic reads** is a guarantee that if the user makes several reads in sequence, they will not read older data after having previously read newer data. The system could implement this guarantee by choosing the replica based on a hash of the user ID.
- **Consistent prefix reads** is a guarantee that if a sequence of writes happens in a certain order, then anyone reading those writes will see them appear in the same order.

## Multi-Leader Replication

The multi-leader configuration allows more than one node to accept writes. Each leader also acts as a follower to the other leaders.

### Multi-datacenter operation

For a database with replicas in several datacenters, the leader could exist in each datacenter. Within each datacenter, regular leader-follower replication is used. Between datacenters, each datacenter's leader replicates its changes to other leaders.

- Performance: Every write can be processed in the local datacenter and is replicated asynchronously to the other datacenters.
- Tolerance of outages: Each datacenter can operate independently of the others, and replication catches up when the failed datacenter comes back online.
- Tolerance of network problem: Temporary network interruption doesn't prevent writes being processed.

However, the multi-leader replication is considered dangerous and should be avoided if possible.

### Clients with offline operation

For applciations that need to continue to work while it is disconnected from the internet, the multi-leader replication is appropriate.

Each decive has a local database that acts as a leader, and tere's an asynchronous multi-leader replication process between the replicas in the devices. Each device could be viewed as a "datacenter".

### Collaborative editing

Real-time collaborative editing applications allow multiple people to edit a document simultaneously. When a user edits the document, the changes are applied to their local replica and replicated to the server and other users.

### Handling Write Conflicts

Write conflicts could occur in multi-leader replication. Since multi-leader replication couldn't handle conflicts well, it's recommended to avoid conflicts. The application could ensure that all writes for a particular record route through the same leader, then conflicts can't occur.

#### Converging toward a consistent state

The database must resolve the conflict in a convergent way, which means that all replicas must arrive at the same final value.

- Give each write a unique ID, pick the write with the highest ID, and throw away the other writes.
- Give each replica a unique ID, and let writes that originated at a higher-numbered replica take precedence.
- Record the conflict in an explicit data structure that preserves all information, and prompt the user to resolve the conflict.

### Custom conflict resolution logic

Most multi-leader replication tools allows the developer to write conflict resolution logic using application code.

- On write: The database detects a conflict in the log of replicated changes, it calls the conflict handler.
- On read: The database stores the conflicting writes, and resolve the conflict when the data is read.

### Multi-Leader Replication Topologies

The replication topology describes the communication paths along which writes are propagated from one node to another. The fault tolerance of a densely connected topology is better because it allows messages to avoid a single point of failure.

- All-to-all: Each leader sends its writes to the other leaders. Due to network congestion, some replication emssages could overtake others.
- Circular: Each leader receives writes from one node and forwards to another leader. Each node is given a unique identifier to prevent infinite replication loops.
- Star: One designated root leader forwards writes to all of the other nodes.

## Leaderless Replication

The leaderless replication abandons the concept of a leader and allows replica to directly accept writes from clients. The client sends its writes to several replicas or a coordinator node, which doesn't enforce an ordering of writes.

### Writing to the Database When a Node Is Down

In a leaderless configuration, failover doesn't exist. The client sends the write to all replicas in parallel, and ignores the fact that one of the replicas missed the write.

When the unavailable node comes back, any writes that happened while the node was down are missing from the node. When a client reads from the database, it reads from several nodes in parallel. If the client get different responses, version numbers are used to determine which value is newer.

### Read repair and anti-entropy

The replication system ensures that eventually all the data is copied to every replica. In the Dynamo-style datastores, two mechanisms are used to let node catch up on missed writes.

- Read repair: When a client makes a read from several nodes in parallel, it could detect the stale response and update it with the latest value.
- Anti-entropy process: Background processes constantly look for differences between replicas and copies missing data from one replica to another.

### Quorum Consistency

If there are $n$ replicas, the reads and writes are sent to all $n$ replicas in parallel. Each write must be confirmed by $w$ nodes to be considered successful, and $r$ nodes must return a result for each read, otherwise the operation return an error.

If $w + r > n$, the set of nodes must overlap, thus the client is expected to read at least one update-to-date value. $n$ could be an odd number and $w = r = (n + 1) / 2$.

### Sloppy Quorums and Hinted Handoff

Databases with appropriately configured quorums can tolerate the failure of individual nodes without the need for failover. However, in a large cluster, it's likely that the client could connect to some nodes during the network interruption, but not to the nodes that it needs to assemble a quorum for a particular value.

The sloppy quorum requires $w$ and $r$ successful responses, but those could include nodes that are not in the $n$ home nodes for a value. Once the network interruption is fixed, the writes that a node temporarily accepted are sent to the appropriate nodes, which is called hinted handoff. It increases write availability, but it could not ensure the client will read the latest value until the hinted handoff has completed.

### Detecting Concurrent Writes

Dynamo-style databases allow several clients to concurrently write to the same key. Events may arrive in different order at different nodes due to network delays or partial failures.

- The operation A happens before operation B if B knows about A or depends on A, thus B should overwrite A.
- Two operation are concurrent if neither happens before the other, then the database should resolve the conflict.

#### Last write wins

Last write wins is an approach to achieve eventual convergence, which ensures that each replica need only store the most recent value. The client could attach a timestamp to each write, and the node accepts the write with the largest timestamp.

#### Capturing the happens-before relationship

- The server maintains a version number for every key and increment it each time the key is written.
- The client reads all values that haven't been overwritten and the latest version number.
- The client writes a key by merging all values that it received and attaching the version number from the prior read.
- The server receives a write with particular version number, then it could overwrite all values with same or lower version number, but it must keep all values with a higher version number.
