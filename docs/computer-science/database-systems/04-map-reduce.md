# MapReduce

MapReduce is a programming model and an associated implementation for processing and generating large
data sets. The program written in the functional style are parallelized and executed on a large cluster of machines. The run-time system partitions the input data, schedules the execution, handles machine failures, and manages inter-machine communication.

## Programming Model

The computation takes a set of input key-value pairs, and
produces a set of output key-value pairs.

- The `map(k1, v1) => Array<[k2, v2]>` function processes a key-value pair to generate a set of intermediate key-value pairs.
- The `reduce(k2, Array<v2>) => Arary<v2>` function merges all intermediate values associated with the same intermediate key.

## Implementation

The `map()` invocations are distributed across multiple
machines by automatically partitioning the input data into a set of `M` splits. The `reduce()` invocations are distributed by partitioning the intermediate key
space into `R` pieces using a partitioning function.

1. The MapReduce library splits the input files into `M` pieces. The MapReduce program is copied to a master machine and a cluster of workers machine. The master stores the state (idle, in-progress, completed) for each task and the identify of the worker machine.
2. The master picks idle workers and assigns a map tasks or a reduce task.
3. The map worker parses the key-value pairs from the input split, and passes each pair to the `map()` function.
4. The result of the map are partitioned into `R` regions by the partitioning function, and the location is forwarded by the master to the reduce workers.
5. The reduce worker reads the partitioned data from the map workers. It sorts the data by the intermediate keys, iterates over the result, and for each unique key encountered, it passes the key and the set of values to the `reduce()` function.
6. When all map and reduce tasks have been completed, the master returns the result back to the user code.

When a MapReduce operation is close to completion, the master schedules backup executions of the remaining in-progress tasks. The tasks is marked as completed whenever either the primary or the backup execution completes.

## Fault Tolerance

The master writes periodic checkpoints of the progress. If the master task dies, a new copy could be started from the last checkpointed state. The master pings each worker periodically, and if no response is received, the master marks the worker as failed.

- The completed map tasks are re-executed because their output is stored on the local disk of the failed machine.
- The completed reduce tasks don't need to be re-executed because their output is stored in a global file system.
