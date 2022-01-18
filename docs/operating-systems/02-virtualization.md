# Virtualization

## The Abstraction: The Process

The process is the operating system abstraction of a running program. The process could be described by its state: the contents of memory in its address space, the contents of CPU registers, and information about I/O. The system maintains a process list data structure that contains information about all processes in the system. Each entry is a process control block, which contains information about a specific process.

### Process Creation

- The system loads the code and static data into the address space of the process.
- The system allocates memory for the program's stack (local variable, function parameter, return address) and heap (dynamically-allocated data).
- The system initializes input/output related tasks. For example, each process in UNIX systems has three open file descriptors for `stdin`, `stdout`, and `stderr`.

### Process State

- **Running**: The process is executing instructions on a processor.
- **Ready**: The process is ready to run but the system has chosen not to run it. The process could be moved between the ready and running states at the discretion of the system.
- **Blocked**: The process has performed operations (such as an I/O request) that make it not ready to run until some other event takes place.

## Process API

The UNIX shell calls `fork()` to create a new child process to run the command, calls `exec()` to run the command, and calls `wait()` to wait for the command to complete.

The separation of `fork()` and `exec()` lets the shell alter the environment of the program after the call to `fork()` and before the call to `exec()`. It enables features such as input/output redirection and pipes.

### The `fork()` System Call

The `fork()` system call is used to create a new process. The creator is called the parent, and the created process is called the child. The child process and the parent process run in separate memory spaces, and both memory spaces initially have the same content. The parent and child processes continue to run from the `fork()`.

- The `fork()` in the parent process returns the PID (process identifier) of the child process.
- The `fork()` in the child process returns `0`.

### The `wait()` System Call

The `wait()` system call is used to wait for a process to change state. The change of state includes termination, stopped by a signal, or resumed by a signal.

### The `exec()` System Call

The `exec()` system call is used to run a program that is different from the calling program. The process loads the code from the executable, re-initializes the memory space, and overwrites the current code segment with it. The system runs the program without creating a new process, thus the successful `exec()` call never returns.

## Mechanism: Limited Direct Execution

The limited direct execution mechanism runs the program directly on the CPU with a few limitations to restrict unexpected behaviors and virtualize the CPU with time sharing.

### Restricted Operation

- User mode: The code that runs in the user mode is restricted from certain instructions, such as issuing I/O requests. The code should execute a system call to perform the privileged operations.
- Kernel mode: The opearting system runs in the kernel mode could execute privileged operations and restricted insturctions.

The kernel sets up a trap table at boot time to define the handlers of certain events (system call, keyboard interrupt, etc.) and inform the hardware of the locations of these trap handlers.

The kernel initializes the environment (allocate memory, etc.) and use a `return-from-trap` instruction to switch to the user mode and start the execution of the process. The process could specify the system-call number and execute a `trap` instruction to execute the system call. The instruction jumps into the kernel and raises the privilege level to kernel mode. The system performs privileged operations, and calls the `return-from-trap` instruction to return to the calling user program.

- The `trap` instruction informs the hardware to push the program counter, flags, and other registers to the per-process kernel stack.
- The `return-from-trap` instruction pops these values off the kernel stack and resume execution of the user-mode program.

### Switching Between Process

The operating system regains control of the CPU to switch between processes with either of these two approaches:

- Cooperative approach (Legacy): The system regains control by waiting for a system call or an illegal operation to take place.
- Non-cooperative approach: The system regains control by defining a timer device that periodically raises a timer interrupt to halt the running process.

The timer interrupt instructs the hardware to save the registers to the kernel stack of the running process A, and enters the kernel. The scheduler makes the decision to continue the current process or execute the context switch routine. The context switch saves current reigster values into the process structure of process A, and restores the registers of another process B from its process structure, and then changes the stack pointer. The `return-from-trap` instruction instructs the hardware to restore the registers of process B and starts running it.

## Scheduling: Introduction

- Response time: $T_\text{response} = T_\text{first\_run} - T_\text{arrival}$
- Turnaround time: $T_\text{turnaround} = T_\text{completion} - T_\text{arrival}$

The trade-off exists between these two metrics: The system could run shorter processes to completion to improve turnaround time, or switch between processes to improve response time.

- **FIFO** (first in, first out): The system queues the processes in the order that they arrive.
- **SJF** (shortest job first): The system queues the processes in the order of their execution time.
- **STCF** (shortest time-to-completion first): The system queues the processes in the order of **remaining** execution time, and preempts the running process if it receives a process that has shorter **remaining** execution time. (Optimal turnaround time)
- **Round Robin**: The system runs each process for a time slice and then switches to the next process in the queue. The length of the time slice presents a trade-off that it should long enough to amortize the cost of context switching without making the system not responsive. (Optimal response time)

The system could incorporate I/O by treating each CPU burst as a new job. While the interactive jobs are performing I/O, other CPU-intensive jobs could be scheduled to improve processor utilization.

## Scheduling: The Multi-Level Feedback Queue

The multi-level feedback queue is a scheduling approach that optimizes turnaround time and response time without the knowledge of how long a job will run for. The MLFQ has a few distinct queues, each assigned a different priority level. The MLFQ varies the priority of a job based on its observed behavior, and it prioritizes short or I/O-intensive interactive jobs.

- If $\text{Priority(A)} > \text{Priority(B)}$, $A$ runs.
- If $\text{Priority(A)} = \text{Priority(B)}$, $A$ and $B$ run in round robin.
- When a job enters the system, it is placed at the highest
priority.
- If a job uses up its time allotment at a given level (regardless of how many times it has given up the CPU), its priority is reduced.
- After some time period $S$, move all the jobs in the system to the topmost queue to avoid the problem of starvation.
