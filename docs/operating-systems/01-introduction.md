# Introduction

The operating system virtualizes physical resources (CPU, memory, disk, etc.), handles concurrency, and stores files persistently.

- **Virtualization**: The operating system virtualizes the CPU to allow multiple programs to run at once and virtualizes the physical memory to create a private virtual address space for each process.
- **Concurrency**: The multi-threaded programs that access and modify shared memories could result in unexpected behaviors if their instructions do not execute atomically.
- **Persistence**: The operating system manages the disk with the file system, and it's responsible for storing files the user creates in a persistent and efficient manner on the disks. The operating system enables different processes to share information in files.

## Design Goal

- Abstraction: Build up abstractions to make the system convenient and easy to use
- Performance: Minimize the overheads (extra time and space costs) of the operating system
- Protection: Isolate processes to prevent malicious programs to harm other programs or the operating system
- Reliability: Build a reliable operating system that could run non-stop
