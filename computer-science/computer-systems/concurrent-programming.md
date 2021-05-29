# Concurrent Programming

Modern operating systems provide three basic approaches for building concurrent programs.

- Process: Each logical control flow is a process that is scheduled and maintained by the kernel.
- I/O multiplexing: Applications explicitly schedule their own logical flows in the context of a single process.
- Threads: Threads are logical flows that run in the context of a single process and are scheduled by the kernel.
