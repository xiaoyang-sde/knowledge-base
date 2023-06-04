# Synchronization

The kernel must ensure that shared resources are protected from concurrent access from a task in kernel, an interrupt handler, a softirq, a tasklet, or a kernel thread. The Linux kernel's synchronization methods enable developers to write efficient and race-free code.

Because the kernel is preemptive, a process in the kernel can stop running at an instant to enable a process of higher priority to run, which implies that a process can begin running in the same critical region as a process that was preempted. To prevent this, the kernel preemption code uses spin-locks as markers of non-preemptive regions.

## Atomic

`atomic_t` and `atomic64_t` are signed atomic integer types, which provide several atomic methods to read or write the integer, such as `atomic_read()`, `atomic_inc()`, and `atomic_inc_return()`.

Atomic operations ensure that the operations on these variables are indivisible and can't be interrupted or interleaved through concurrent threads or interrupt handlers.

## Spin Lock

The spin lock is the most common lock in the kernel. If a thread of execution attempts to acquire a spin lock while it is held, which is called contended, the thread loops to wait for the lock to become available. It is not wise to hold a spin lock for a long time because it wastes resources.

```c
DEFINE_SPINLOCK(lock);

spin_lock(&lock);
spin_unlock(&lock);
```

If a lock is used in an interrupt handler, local interrupts on the current processor should be disabled before acquiring the lock. Otherwise, it is possible for an interrupt handler to interrupt kernel code while the lock is held and attempt to reacquire the lock.

```c
DEFINE_SPINLOCK(lock);
unsigned long flags;

spin_lock_irqsave(&lock, flags);
spin_unlock_irqrestore(&lock, flags);
```

## Semaphore

The semaphore is the sleeping lock in the kernel. When a process attempts to acquire a semaphore that is unavailable, the semaphore places the process onto a wait queue and puts the process to sleep. When the semaphore becomes available, one of the processes on the wait queue is awakened so that it can then acquire the semaphore. Semaphore provides better processor utilization, but it introduces the overhead of context switching. Semaphore should be obtained in process context because interrupt context is not schedulable.

The number of permissible simultaneous holders of semaphores can be set at declaration time. `down_interruptible()` attempts to acquire a semaphore. If the semaphore is unavailable, it places the calling process to sleep in the `TASK_INTERRUPTIBLE` state. `up()` releases a semaphore.

```c
struct semaphore name;
sema_init(&name, count);
```

## Barrier

Modern processors and compilers might reorder instructions to optimize for performance. The kernel provides barriers to ensure the load and store ordering between multiple processors.

- `rmb()` ensure that no loads are reordered across the `rmb()` call.
- `wmb()` ensure that no stores are reordered across the `wmb()` call.
- `mb()` ensure that no loads and stores are reordered across the `wmb()` call.
