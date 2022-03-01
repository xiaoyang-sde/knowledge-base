# ZooKeeper

ZooKeeper is a service for coordinating processes of distributed applications. ZooKeeper provides a simple and high performance kernel for building more complex coordination primitives at the client.

- ZooKeeper implements an API that manipulates **wait-free** data objects organized hierarchically as in file systems.
- ZooKeeper guarantees **linearizable writes**.
  - ZooKeeper uses Zab, a leader-based atomic broadcast protocol, to replicate write operations on the majority of the cluster.
  - ZooKeeper processes read operations locally to improve throughput.
- ZooKeeper guarantees **FIFO client order** of all operations. The client could submit operations asynchronously.

## The ZooKeeper Service

The data model of ZooKeeper is a file system with a simplified API and full data reads and writes. ZooKeeper provides to its clients the abstraction of a set of **znodes** that stores data.

- The **sequential** flag attaches a monotonically increasing counter to the name of the node created.
- The **watch** flag let the server to send notifications of changes to the client when the information returned has changed.

The ZooKeeper client library forwards client requests to ZooKeeper service and manages the network connection. ZooKeeper maintains a session for each connected client and deletes it when the client disconnected or failed.

### Client API

- `create(path, data, flags)` creates a znode with path name `path`, stores `data` in it, and returns the name of the new znode.
- `delete(path, version)` deletes the znode path if that znode is at the expected version.
- `exists(path, watch)` returns `true` if the znode
with path name path exists, and returns `false` otherwise.
- `getData(path, watch)` returns the data and meta-data (e.g. version) of the znode `path`.
- `setData(path, data, version)` writes `data` to znode `path` if the version number is the current version of the znode.
- `getChildren(path, watch)` returns the set of names of the children of a znode `path`.
- `sync(path)` waits for updates pending at the start of the operation to propagate to the server that the client is connected to.

## Implementation

ZooKeeper provides high availability by replicating the
ZooKeeper data on each server that composes the service. The cluster uses an agreement protocol to replicate write requests and each server commits changes to the database.

The replicated database is an in-memory database containing the entire data tree. Each znode in the tree stores a maximum of 1MB of data by default. The requests are written to disk before they are applied to the database and periodic fuzzy snapshots of the database are generated. The fuzzy snapshots could have applied some subset of the state changes committed during the generation of the snapshot.

The replicated transactions are idempotent. When the leader receives a write request, it calculates the state of the system if the request is applied and creates a transaction that captures the new state.

## Client-Server Interaction

When a server processes a write request, it sends out and clears notifications relative to watches that correspond to the update.

The read request are handled locally at each server. Each read request is processed and tagged with a **zxid** that corresponds to the last transaction seen by the server, which defines the partial order of the read requests with respect to the write requests.

The application could call the `sync` primitive followed by the read request to avoid stale values. The FIFO order guarantee of client operations and the global guarantee of sync enable the result of the read operation to reflect the changes that happened before `sync` was issued.

ZooKeeper server uses timeouts to detect client session failures. The client sends a heartbeat messsage to the server and connects to a different ZooKeeper server to re-establish its session when the message fails.
