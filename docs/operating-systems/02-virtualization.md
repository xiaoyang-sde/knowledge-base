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
