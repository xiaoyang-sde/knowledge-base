# Process Management

## Process Scheduling

The process scheduler is the kernel subsystem that divides the finite resource of processor time among a system's processes. The scheduler creates an illusion that multiple processes are executing at the same time.

The runnable process is a process that is not blocked, such as sleeping or waiting for I/O from the kernel. The process that interacts with users, files, or network events tend to spend a lot of time blocked. The scheduler decides which runnable process should be run next.

Linux implements the preemptive multitasking, where the scheduler decides when one process is to stop running and a different process is to resume. The length of time a process is allowed to run before the scheduler preempts it is known as the process's timeslice.

- Processes that consume all of their available timeslices are considered processor-bound, ushc as an infinite loop.
- Processes that spend more time blocked waiting for some resource than executing are I/O-bound.

## The Completely Fair Scheduler

The Completely Fair Scheduler (CFS) introduces a quite different algorithm called fair scheduling that eliminates timeslices as the unit of allotting access to the processor. Instead of timeslices, CFS assigns each process a proportion of the processor's time.

CFS assigns $N$ processes each $\frac{1}{N}$ of the processor's time. CFS then adjusts the allotment with a nice value of each process.

- Processes with the default nice value of zero have a weight of one, so their proportion is unchanged.
- Processes with a smaller nice value (higher priority) receive a larger weight, increasing their fraction of the processor.
- Processes with a larger nice value (lower priority) receive a smaller weight, decreasing their fraction of the processor.

The CFS has a target latency variable, which represents the scheduling latency of the system. The scheduler divides the target latency based on the weight of each process and runs a process for its fraction. To reduce the overhead of context switching, a process must run for at least the minimum granularity or until it blocks.

## Process Priorities

Each process has a nice value, which represents the weighted proportion of processor allotted to a process. Legal nice values range from `−20` to `19` inclusive, with a default value of `0`. The lower the nice value, the higher a process's priority.

## I/O Priorities

Each process has an I/O priority, which affects the relative priority of the process's I/O requests. The kernel’s I/O scheduler services requests originating from processes with higher I/O priorities before requests from processes with lower I/O priorities. The default value is the process's nice value.

## Processor Affinity

Linux supports multiple processors in a single system. On a multiprocessing machine, the process scheduler must decide which processes run on each CPU.

- The scheduler must uses all of the system's processors because it is inefficient for one CPU to sit idle while a process is waiting to run.
- Once a process has been scheduled on one CPU, the process scheduler should aim to schedule it on the same CPU in the future. Most of the caches associated with each processor are separate and distinct. If a process moves, the data in the old CPU's cache can become stale, so the cost of moving is significant.

Processor affinity refers to the likelihood of a process to be scheduled consistently on the same processor. The Linux scheduler attempts to schedule the same processes on the same processors for as long as possible, migrating a process from one CPU to another in situations of extreme load imbalance. Processes inherit the CPU affinities of their parents.

## System Call

- `sched_yield()` results in suspension of the running process, after which the process scheduler selects a new process to run. If there's no other process, the calling process will be resumed execution. However, the kernel knows better than an application to decide when to preempt, so there are few legitimate use case.

```c
#include <sched.h>

int sched_yield(void);
```

- `nice()` increments a process's nice value with `inc` and returns the updated value. The `inc` value can be negative when the process has the `CAP_SYS_NICE` capability.

- `getpriority()` returns the highest priority of the specified processes. The value of which must be one of `PRIO_PROCESS`, `PRIO_PGRP`, or `PRIO_USER`, in which case who specifies a pid, pgid, or uid.

- `setpriority()` sets the priority of the specified processes to `prio`.

```c
#include <unistd.h>
#include <sys/time.h>
#include <sys/resource.h>

int nice(int inc);

int getpriority(int which, int who);
int setpriority(int which, int who, int prio);

int ioprio_get(int which, int who)
int ioprio_set(int which, int who, int ioprio)
```

- `sched_getaffinity()` retrieves the CPU affinity of the process `pid` and stores it in the `cpu_set_t` type, which is accessed with special macros. The `setsize` parameter is the size of the `cpu_set_t` type. The `cpu_set_t` is a mask that represents which CPU the process can be scheduled on.
- `sched_setaffinity()` sets the CPU affinity of the process `pid` with the `cpu_set_t` type.

```c
#include <sched.h>

typedef struct cpu_set_t;

size_t CPU_SETSIZE;

void CPU_SET(unsigned long cpu, cpu_set_t *set);
void CPU_CLR(unsigned long cpu, cpu_set_t *set);
int CPU_ISSET(unsigned long cpu, cpu_set_t *set);
void CPU_ZERO(cpu_set_t *set);

int sched_getaffinity(pid_t pid, size_t setsize, cpu_set_t *set);
int sched_setaffinity(pid_t pid, size_t setsize, const cpu_set_t *set);
```
