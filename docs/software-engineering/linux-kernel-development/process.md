# Process

## Process Descriptor

The kernel stores the list of processes in a circular  linked list called the task list. Each element in the task list is a process descriptor of the type `struct task_struct`, which is defined in `<linux/sched.h>`.

`task_struct` contains the data that describes the executing program, such as open files, address space, pending signals, the state. The kernel assigns a unique process identifier of type `pid_t` to each process.

`task_struct` is allocated with the slab allocator to provide object reuse and cache coloring. Each thread has a `thread_info` structure that is allocated at the end of its kernel stack. `thread_info` contains a pointer to the `task_struct` of the process associated with the thread.

### Process State

The `state` field of `task_struct` describes the current condition of the process.

- `TASK_RUNNING`: The process is runnable. It is either running or on a runqueue waiting to run.
- `TASK_INTERRUPTIBLE`: The process is sleeping, waiting for some condition to exist. When this condition exists, the kernel sets the process's state to `TASK_RUNNING`. The process also awakes and becomes runnable if it receives a signal.
- `TASK_UNINTERRUPTIBLE`: This state is identical to `TASK_INTERRUPTIBLE` except that it doesn't wake up and become runnable if it receives a signal.
- `__TASK_TRACED`: The process is being traced with another process, such as `strace`.
- `__TASK_STOPPED`: The process has stopped and is not eligible to run.

### Process Context

When a program executes a system call or triggers an exception, it enters kernel-space. The kernel is executing on behalf of the process and is in process context. When in process context, the `current` macro is valid, which retrieves the `task_struct` of the current process.

### Process Tree

Each process has a parent and zero or more children. The relationship between processes is stored in the process descriptor. Each `task_struct` has a pointer to the parent's `task_struct` and a list of children.

Each `task_struct` has a pointer to its node in the task list. Therefore, it's possible to iterate through the task list starting with the `init_task`, which is a static `task_struct` of the `init` process.

### Process Creation

Each process is created with a pair of `fork()` and `exec()`. `fork()` is implemented with COW, so it won't duplicate the pages in the address space. Therefore, the overhead of `fork()` is the duplication of the parent's page table entries and the creation of a `task_struct` for the child.

`clone()` is a system call that creates a child process. The `fork()`, `vfork()`, and `__clone()` calls all invoke `clone()` with the requisite flags. `clone()` invokes `do_fork()` to handle forking.

1. It calls `dup_task_struct()`, which creates a kernel stack, `thread_info`, and `task_struct` for the new process. The values are identical to those of the current process.
2. The child needs to differentiate itself from its parent. Various members of the process descriptor are cleared or set to initial values.
3. It sets the child's state to `TASK_UNINTERRUPTIBLE` and updates the `flags` member of `task_struct`.
4. It calls `alloc_pid()` to assign an available process identifier.
5. Depending on the flags passed to `clone()`, it either duplicates or shares open files, filesystem information, signal handlers, process address space, and namespace.
6. The child is woken up and run.

The `vfork()` system call has the same effect as `fork()`, except that the page table entries of the parent process are not duplicated. Instead, the child executes as the sole thread in the parent's address space, and the parent is blocked until the child either calls `exec()` or exits. The child is not allowed to write to the address space.

### Process Termination

When a process terminates, the kernel releases the resources and notifies its parent. `do_exit()` handles the process termination.

1. It sets the `PF_EXITING` flag in the flags member of the `task_struct`.
2. It calls `del_timer_sync()` to remove kernel timers.
3. It calls `exit_mm()` to release the `mm_struct`.
4. It calls `exit_sem()`. If the process is queued waiting for an IPC semaphore, it is dequeued here.
5. It calls `exit_files()` and `exit_fs()` to decrement the usage count of file descriptors and filesystem data.
6. It sets the `exit_code` member of the `task_struct`.
7. It calls `exit_notify()` to send signals to the process's parent, reparents the process's children to a thread in their thread group or the `init` process, and sets the process's exit state, stored in `exit_state` in the `task_struct` structure, to `EXIT_ZOMBIE`.
8. It calls `schedule()` to switch to a new process.

After the process terminates, some resources, such as the kernel stack, `task_struct`, and `thread_info`, still exist, but the process is a zombie and is unable to run. The parent could invoke `wait()` to obtain the information of the child process and inform the kernel to deallocate these resources.

## Thread

Linux implements a thread as a standard process that shares certain resources, such as a shared address space. It does not provide scheduling semantics or data structures to represent threads. Each thread has a unique `task_struct`.

### Thread Creation

Threads are created with the `clone()` system call. Flags are passed to share the address space, filesystem resources, file descriptors, and signal handlers.

```c
clone(CLONE_VM | CLONE_FS | CLONE_FILES | CLONE_SIGHAND, 0);
```

### Kernel Thread

The kernel use kernel threads to perform background operations. Kernel threads run in the kernel space and don't have an address space. Kernel threads are schedulable and preemptible.
