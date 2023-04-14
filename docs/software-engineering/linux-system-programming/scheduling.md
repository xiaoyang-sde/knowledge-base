# Scheduling

## Scheduling Policies

The scheduler is the kernel component that decides which runnable thread will be executed next. Each thread has an associated scheduling policy and a static scheduling priority, `sched_priority`. The scheduler makes its decisions based on  knowledge of the scheduling policy and static priority of all threads on the system.

- For threads scheduled under one of the normal scheduling policies(`SCHED_OTHER`, `SCHED_IDLE`, `SCHED_BATCH`), `sched_priority` is not used in scheduling decisions (it must be specified as `0`).
- For threads scheduled under one of the real-time policies (`SCHED_FIFO`, `SCHED_RR`), `sched_priority` has a value in the range `1` (low) to `99` (high). Real-time threads have higher priority than normal threads.

The scheduler maintains a list of runnable threads for each possible `sched_priority` value.  In order to determine which thread runs next, the scheduler looks for the nonempty list with the highest static priority and selects the thread at the head of this list. The thread's policy determines where it will be inserted into the list of threads with equal static priority and how it will move inside this list.

The scheduler is preemptive. If a thread with a higher static priority becomes runnable, the running thread will be preempted and returned to the wait list for its static priority level. The scheduling policy determines the ordering only within the list of runnable threads with equal static priority.

### `SCHED_FIFO`

`SCHED_FIFO` is a simple scheduling algorithm without time slicing, which can be used for threads with a static priority higher than `0`.

- When a `SCHED_FIFO` thread blocks, the scheduler removes it from the list of runnable threads.
- When a `SCHED_FIFO` thread becomes runnable, it is inserted at the end of the list of threads for its priority.
- When a `SCHED_FIFO` thread is preempted, it will keep its position at the head of the list for its priority.
- When a `SCHED_FIFO` thread invokes `sched_yield()`, it will be moved to the end of the list for its priority.

### `SCHED_RR`

`SCHED_RR` is an enhancement of `SCHED_FIFO`. Each thread is allowed to run for a maximum time quantum. If a `SCHED_RR` thread has been running for a time period equal to or longer than the time quantum, it will be put at the end of the list for its priority. If a `SCHED_RR` thread has been preempted by a higher priority thread, it will complete the unexpired portion of its round-robin time quantum after it becomes runnable.

### `SCHED_DEADLINE`

`SCHED_DEADLINE` is a deadline scheduling policy. A sporadic task is one that has a sequence of jobs, where each job is activated at most once per period.  Each job also has a relative deadline, before which it should finish execution, and a computation time, which is the CPU time necessary for executing the job.

The thread with `SCHED_DEADLINE` policy has three parameters: `sched_runtime`, `sched_deadline`, and `sched_period`. `sched_runtime` is the worst-case execution time, `sched_deadline` is the relative deadline, and `sched_period` is the period of the task. These fields express values in nanoseconds and should follow the pattern: sched_runtime $\le$ sched_deadline $\le$ sched_period.

To ensure deadline scheduling guarantees, the kernel must prevent situations where the set of `SCHED_DEADLINE` threads is not schedulable within the given constraints. `SCHED_DEADLINE` threads are the highest priority threads in the system. if a `SCHED_DEADLINE` thread is runnable, it will preempt other threads scheduled under other policies.

### `SCHED_OTHER`

`SCHED_OTHER` is the default Linux time-sharing scheduling policy, which can be used for threads with a static priority of `0`. The thread to run is chosen from the static priority `0` list based on a dynamic priority that is determined only inside this list. The dynamic priority is based on the nice value and is increased for each time quantum the thread is runnable but not selected for running, which ensures fair progress among all `SCHED_OTHER` threads.

Each `SCHED_OTHER` thread has a nice value, which represents the weighted proportion of processor allotted to the thread. Legal nice values range from `−20` to `19` inclusive, with a default value of `0`. The lower the nice value, the higher a thread's priority.

### `SCHED_BATCH`

`SCHED_BATCH` is a scheduling policy for non-interactive threads, which can be used for threads with a static priority of `0`. This policy will cause the scheduler to assume that the thread is CPU-intensive and optimize for throughput. Therefore, the scheduler will apply a small scheduling penalty with respect to wakeup behavior, so that this thread is disfavored in scheduling decisions.

### `SCHED_IDLE`

`SCHED_IDLE` is a scheduling policy for threads with a priority lower than a nice value of `19` in `SCHED_OTHER`, which can be used for threads with a static priority of `0`.

## The Completely Fair Scheduler

The Completely Fair Scheduler (CFS) introduces an algorithm called fair scheduling that eliminates timeslices as the unit of allotting access to the processor. Instead of timeslices, CFS assigns each thread a proportion of the processor's time.

CFS assigns $N$ threads each $\frac{1}{N}$ of the processor's time. CFS then adjusts the allotment with a nice value of each thread.

- Threads with the default nice value of zero have a weight of one, so their proportion is unchanged.
- Threads with a smaller nice value (higher priority) receive a larger weight, increasing their fraction of the processor.
- Threads with a larger nice value (lower priority) receive a smaller weight, decreasing their fraction of the processor.

The CFS has a target latency variable, which represents the scheduling latency of the system. The scheduler divides the target latency based on the weight of each thread and runs a thread for its fraction. To reduce the overhead of context switching, a thread must run for at least the minimum granularity or until it blocks.

## processor Affinity

Linux supports multiple processors in a single system. On a multithreading machine, the thread scheduler must decide which threads run on each CPU.

- The scheduler must uses all of the system's processors because it is inefficient for one CPU to sit idle while a thread is waiting to run.
- Once a thread has been scheduled on one CPU, the thread scheduler should aim to schedule it on the same CPU in the future. Most of the caches associated with each processor are separate and distinct. If a thread moves, the data in the old CPU's cache can become stale, so the cost of moving is significant.

Processor affinity refers to the likelihood of a thread to be scheduled consistently on the same processor. The Linux scheduler attempts to schedule the same threads on the same processors for as long as possible, migrating a thread from one CPU to another in situations of extreme load imbalance. threads inherit the CPU affinities of their parents.

## Resource Limit

The Linux kernel imposes several resource limits on processes. These resource limits place hard ceilings on the amount of kernel resources that a process can consume, such as the number of open files, pending signals, and so on. The kernel enforces soft resource limits on processes, but a process can change its soft limit to up to the hard limit. However, an unprivileged process can never raise its hard limit. Child process inherits the limits of its parent.

## System Call

- `sched_yield()` results in suspension of the running thread, after which the thread scheduler selects a new thread to run. If there's no other thread, the calling thread will be resumed execution. However, the kernel knows better than an application to decide when to preempt, so there are few legitimate use case.

```c
#include <sched.h>

int sched_yield(void);
```

- `nice()` increments a thread's nice value with `inc` and returns the updated value. The `inc` value can be negative when the thread has the `CAP_SYS_NICE` capability.

- `getpriority()` returns the highest priority of the specified threads. The value of which must be one of `PRIO_thread`, `PRIO_PGRP`, or `PRIO_USER`, in which case who specifies a pid, pgid, or uid.

- `setpriority()` sets the priority of the specified threads to `prio`.

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

- `sched_getaffinity()` retrieves the CPU affinity of the thread `pid` and stores it in the `cpu_set_t` type, which is accessed with special macros. The `setsize` parameter is the size of the `cpu_set_t` type. The `cpu_set_t` is a mask that represents which CPU the thread can be scheduled on.
- `sched_setaffinity()` sets the CPU affinity of the thread `pid` with the `cpu_set_t` type.

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

- `sched_getscheduler()` returns the scheduling policy of a specific thread, such as `SCHED_FIFO` or `SCHED_OTHER`.
- `sched_setscheduler()` sets the scheduling policy of a specific thread.
- `sched_getparam()` returns the scheduling parameters of a specific thread.
- `sched_setparam()` sets the scheduling parameters of a specific thread.
- `sched_get_priority_min()` returns the minimum static priority value of a specific policy.
- `sched_get_priority_max()` returns the maximum static priority value of a specific policy.

```c
#include <sched.h>

struct sched_param {
  /* ... */
  int sched_priority;
  /* ... */
};

int sched_getscheduler(pid_t pid);
int sched_setscheduler(
  pid_t pid, int policy,
  const struct sched_param *sp
);

int sched_getparam(pid_t pid, struct sched_param *sp);
int sched_setparam(pid_t pid, const struct sched_param *sp);

int sched_get_priority_min(int policy);
int sched_get_priority_max(int policy);
```

- `getrlimit()` places the hard and soft limits on the resource in a `rlimit` structure.
- `setrlimit()` sets the hard and soft limits of a resource to the values in a `rlimit` structure.

```c
#include <sys/time.h>
#include <sys/resource.h>

struct rlimit {
  rlim_t rlim_cur;  /* soft limit */
  rlim_t rlim_max;  /* hard limit */
};

int getrlimit(int resource, struct rlimit *rlim);
int setrlimit(int resource, const struct rlimit *rlim);
```
