# Transaction Management

## Transaction

The transaction is a unit of program execution that accesses and updates various data items. The transaction is delimited by statements of the form **begin transaction** and **end transaction**.

- Atomicity: The transaction must either executes in its entirety or not at all.
- Consistency: The transaction must preserve the consistency of the database.
- Isolation: The transaction must executes without interference from concurrently executing database statements.
- Durability: The transaction must persist its changes even if there are system failures.

### Storage Structure

- Volatile storage: Information residing in volatile storage does not survive system crashes. Access to volatile storage is extremely fast. (e.g. main memory, cache memory)
- Non-volatile storage: Information residing in non-volatile storage survives system crashes. Non-volatile storage is slower than volatile storage. (e.g. flash storage, magnetic disk)
- Stable storage: Information residing in stable storage is never lost.

### Transaction Isolation

- Dirty read: The transaction A accesses data updated by the uncommitted transaction B.
- Non-repeatable read: The transaction A attempts to access the same data twice, and the transaction B modifies the data between the transaction A's read attempts.
- Phantom: The transaction A attempts to retrieve a set of rows satisfying a given condition, and the transaction B inserts or updates a row that meets the condition between the transaction A's read attempts.

| Isolation Level | Dirty Read | Non-Repeatable Read | Phantom |
| - | - | - | - |
| `READ UNCOMMITTED` | Support | Support | Support |
| `READ COMMITTED` | Not support | Support | Support |
| `REPEATABLE READ` | Not support | Not support | Support |
| `SERIALIZABLE` | Not support | Not support | Not support |

### Log-Based Recovery

1. The system generates a log record before start, after end, and for each modification made by the transaction.
2. Before the transaction is committed, all log records until its commit must be flushed to the disk.
3. Before modified tuple is written back to disk, all log records through the tuple modification must be flushed to disk first.
4. The `ROLLBACK` statement reverts the records to the old state with the log records
5. The system recovers the data after a crash by re-executing all actions in the log file from the beginning, and rolling back all actions from non-committed transactions in the reverse order
