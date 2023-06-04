# System Call

The system call is an interface between the hardware and user-space processes. With the kernel acting as a middleman between system resources and user-space, the kernel can arbitrate access based on permissions, users, and other criteria. In Linux, system calls are the means user-space has of interfacing with the kernel.

The kernel keeps a list of all registered system calls in the system call table `sys_call_table`, which is defined for each architecture. Each system call is assigned a syscall number. When a user-space process executes a system call, the syscall number identifies which system call was executed.

## System Call Handler

The user-space process stores the syscall number and parameters in specific registers and signals to the kernel through a software interrupt. The interrupt incurs an exception. The system will switch to kernel mode and execute the exception handler, which is the `system_call()` function. `system_call()` invokes the specific system call, which is a function pointer stored in the `sys_call_table`. When the system call returns, the control is return to the user-space and the user process continues its execution.

## System Call Context

The kernel is in process context during the execution of a system call. The `current` pointer points to the current task, which is the process that issued the syscall.

In process context, the kernel is capable of sleeping and is preemptible. Preemptible implies that another process might be executed when handling the system call. Therefore, the system call should be reentrant.
