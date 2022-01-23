# Raft Consensus Algorithm

Raft is a consensus algorithm for managing a replicated log. Consensus algorithms allow a collection of machines to work as a coherent group that can survive the failures of some members.

- **Strong leader**: Raft requires log entires only flow from the leader to other servers.
- **Leader election**: Raft uses randomized timers to elect leaders.
- **Membership changes**: Raft uses a joint consensus approach where the majorities of two configurations overlap during transitions to change the set of servers in the cluster.

## Replicated state machines

Replicated state machines compute identical copies of the same state from a deterministic replicated log. Each server stores a log containing a series of commands, which its state machine executes in order.

The consensus algorithm keeps the replicated log consistent across the cluster. Each server communicates with other servers to ensure that their logs eventually contain the same requests in the same order.

## Implementation

- **Election safety**: At most one leader could be elected in a given term.
- **Leader append-only**: The leader never overwrites or deletes entries in its log.
- **Log matching**: If two logs contain an entry with the same index and term, then the logs are identical in all entries up through the given index.
- **Leader completeness**: If a log entry is committed in a given term, then that entry will be present in the logs of the leaders for all higher-number terms.
- **State machine safety**: If a server has applied a log entry at a given index to its state machine, no other server will apply a different log entry for the same index.

### Leader election

Each server in a Raft cluster is in one of three states: **leader**, **follower**, or **candidate**. There is one leader and other servers are followers. The leader handles all client requests. Raft divides time into **terms** that are numbered with consecutive integers. Each term begins with an election to elect a candidate to become the leader.

The leader sends periodic heartbeats to followers to maintain its authority. If a follower receives no communication until the election timeout, it begins a election to choose a new leader. To begin an election, a follower increments its term and transits to candidate state. It votes for itself and issues `RequestVote` RPCs in parallel to other servers. Each server will vote for at most one candidate in a term.

- If a candidate receives votes from a majority of the servers, it wins the election and sends heartbeat message to establish its authority.
- If a candidate receives a heartbeat from another server and its term is larger or equal to the candidate's term, then the candidate recognizes the leader as legitimate.
- If no candidate obtains a majority, each candidate will increment its term and start a new election. Raft uses randomized election timeouts to ensure that split votes are rare.

### Log replication

The leader handles client requests that contains a command to be executed. The leader appends the command to its log, and issues `AppendEntries` RPCs to the servers to replicate the entry. The leader then applies the entry to its state machine and returns the result to the client. If followers crash, the leader retries the RPC indefinitely until all folowers store all log entires. The log entry is commited if the leader has replicated it on a majority of the servers.

To ensure log consistency, the leader sends the highest index of the commited entry and the index and term of the entry in its log that immediately precedes the new entries in the `AppendEntries` RPC. If the follower doesn't find an entry with the same index and term in its log, it refuses the new entries.

The leader maintains a `nextIndex` for each follower, which is the index of the next log entry the leader will send to that follower. The value is initialized to the index after the last one in the leader's log. If a `AppendEntries` RPC is rejected because of inconsistency, the value is decremented until it reaches the point that the logs diverge.
Therefore, subsequent `AppendEntries` RPC will success and overwrite the conflicting entires.

### Safety

Raft uses the election restriction to ensure the leader contains all committed entries from previous leaders. In the voting process, a candidate must contact a majority of the cluster in order to be elected, thus all committed entries must be present in at least one of these servers. If the candidate's log is less up-to-date than a specific server, the server will deny its vote.

The leader won't commit log entries from previous term by counting replicas because these entries could still be overwritten by a future leader. When an entry in the current term is committed, all prior entries are committed indirectly.

If the follower and candidate crashes, future RPCs will fail and be retried indefinitely until the crashed server restarts. Because the RPCs are idempotent, duplicated RPCs won't change the state of the server.
