# `io_uring`

`io_uring` is an asynchronous I/O api for Linux with low performance overheads. It uses ring buffers as the main interface for kernel-user space communication and has less number of system calls than [`epoll(7)`](https://man7.org/linux/man-pages/man7/epoll.7.html) and [`aio(7)`](https://man7.org/linux/man-pages/man7/aio.7.html).

## Interface

- There's a submission queue and a completion queue, which are shared between kernel and user space. The `io_uring_steup()` system call creates the buffers and `mmap()` maps them to user space.

- The user program creates I/O requests (read or write a file, accept client connections, etc.) and pushes submission queue entries to the submission queue. The `io_uring_enter()` system call informs the kernel about recent entries and could wait for a number of requests to be processed.

- The kernel processes requests and pushes completion queue entries to the completion queue.

- The user program reads completion queue entries from the completion queue.
