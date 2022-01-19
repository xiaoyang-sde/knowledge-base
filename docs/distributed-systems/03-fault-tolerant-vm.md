# Fault-Tolerant Virtual Machine

The VMware vSphere 4.0 platform implements fault-tolerant virtual machines based on the approach of replicating the execution of a primary virtual machine via a backup virtual machine on another server. The replication process is implemented with a state-machine approach and the hypervisor is able to capture the information about non-deterministic operations.

## Basic Design

The primary and backup VMs run on different physical server that are kept in sync and execute identically with a small time lag. The virtual disks for the VMs are on shared storage and accessible to the primary and backup VM for input and output. The inputs received by the primary VM is sent to the backup VM via a network connection known as the logging channel. The outputs of the primary VM is returned to the client, and the outputs of the backup VM are dropped by the hypervisor.

To detect if the primary or backup VM has failed, the system uses a combination of heartbeating between the servers and monitoring of the traffic on the logging channel.

### Deterministic Replay Implementation

Deterministic replay records the input of the VM and all possible non-determinism in a stream of log entires written to a log file. For non-deterministic operations, sufficient information is logged to allow the operation to be reproduced with the same state change and output. For non-deterministic events such as I/O completion interrupts, the exact instruction at which the event occurred is recorded.

### Fault-Tolerance Protocol

If the backup VM takes over after a failure of the primary, the backup VM will continue executing that is entirely consistent with all outputs that the primary VM has sent to the external world.

The execution of the primary VM is recorded with the deterministic replay and sent to the backup VM via the logging channel. The backup VM replays the entires in real time. The primary VM could not send an output to the external world until the backup VM has received and ackowledged the log entry associated with the operation producing the output.

### Failure Detection

The failure is declared if heartbeating or logging traffic has stopped for longer than a specific timeout. If the backup VM fails, the primary VM will go live and leave the record mode. If the primary VM fails, the backup VM will continue to replay the log entires until it has consumed the last entry and be promoted to the primary VM.

To avoid split-brain problems, the VM executes an atomic test-and-set operation on the shared storage when it wants to go live. If the operation fails, the current VM halts itself.

## Practical Implementation

### Start and restart VM

The VMotion of vSphere clones a VM to a remote host with minimal disruption. The VMotion also set up a logging channel, inform the primary VM to enter the record mode, and inform the backup VM to enter the replay mode.

### Manage the logging channel

The hypervisor maintains buffers for logging entires in the primary and backup VMs. The primary VM produces log entires to its log buffer, and the backup VM consumes log entires from its buffer. The backup sends acknowledgements back to the primary when it reads log entires from the network to its log buffer. The backup sends information to determine the real-time execution lag, and the hypervisor could reduce the CPU limit of the primary VM to avoid significant execution lag.
