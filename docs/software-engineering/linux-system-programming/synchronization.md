# Synchronization

Threading is the creation and management of multiple units of execution within a single process. Each process contains one or more threads. Virtualized memory is unique to each process, but shared among threads within the same process. Virtualized processor is unique to threads, allowing a single process to perform multiple tasks at the same time.

In machines with multiple processors, threads achieves true parallelism. Multiple threads run on multiple processors at the same time, improving a system's throughput. The cost of switching from one thread to a different thread within the same process is cheaper than process-to-process context switching, since there's no need to switch page tables and refresh the TLB.

## Pthreads

POSIX.1 specifies a set of interfaces for threaded programming known as Pthreads. Each process can contain multiple threads, all of which are executing the same program. These threads share the same global data and heap segments, but each thread has its own stack.

NTPL (Native POSIX Thread Library) is the standard Linux Pthread implementation, which provides 1:1 threading based around the `clone()` system call and the kernel's model that threads are just like process.

### Thread

- `pthread_create()` launches a new thread, which executes the `start_routine` function and passes the sole argument `arg`. The function will store the tid in the `thread` pointer, which points to a `pthread_t` structure. The `pthread_attr_t` object is used to change the default thread attributes of the created thread. Thread attributes let programs change some aspects of threads, such as their stack size, schedulizing parameters, and initial detached state.

```c
#include <pthread.h>

int pthread_create(
  pthread_t *thread,
  const pthread_attr_t *attr,
  void *(*start_routine) (void *),
  void *arg
);
```

- `pthread_self()` returns the tid of the current thread, which is an opaque type `pthread_t`.
- `pthread_equal()` returns a non-zero value if two `pthread_t` are equal. Because `pthread_t` might not be an arithmetic type, there is no guarantee that the `=` operator will work.

```c
pthread_t pthread_self(void);

int pthread_equal(pthread_t t1, pthread_t t2);
```

- `pthread_exit()` terminates itself, which is the thread equivalent of `exit()`. `retval` will be passed to all threads waiting for the current thread's termination.
- `pthread_cancel()` sends a cancellation request to another thread. Whether and when a thread is cancellable depends on its cancellation state and cancellation type.
  - Each thread's cancellation state is either enabled or disabled. The default for new threads is enabled. If a thread has disabled cancellation, the cancel request is queued until it is enabled.
  - Each thread's cancellation type is either asynchronous or deferred. The default for new threads is deferred. With asynchronous cancellation, a thread might be killed at some point after the issuing of a cancel request. With deferred cancellation, a thread will be killed at specific cancellation points, which are a set of guaranteed-reentrant functions in `glibc`.
- `pthread_setcancelstate()` sets its cancellation state to be `PTHREAD_CANCEL_ENABLE` or `PTHREAD_CANCEL_DISABLE`.
- `pthread_setcanceltype()` sets its cancellation type to be `PTHREAD_CANCEL_ASYNCHRONOUS` or `PTHREAD_CANCEL_DEFERRED`.

```c
void pthread_exit(void *retval);
int pthread_cancel(pthread_t thread);

int pthread_setcancelstate(int state, int *oldstate);
int pthread_setcanceltype(int type, int *oldtype);
```

- `pthread_join()` allows one thread to block while waiting for the termination of another thread. The `retval` is a pointer to the return value of the thread, which is a pointer to the `void` type.
- `pthread_detach()` makes a thread no longer joinable. The operating system will reclaim the resources when a detached thread terminates. Either `pthread_join()` or `pthread_detach()` should be called on each thread in a process so that resources are released when the thread terminates.

```c
int pthread_join(pthread_t thread, void **retval);

int pthread_detach(pthread_t thread);
```

### Mutex

- `pthread_mutex_init()` initializes a `pthread_mutex_t`. It takes an argument of type `pthread_mutexattr_t` to configure the properties of the mutex.
- `pthread_mutex_destroy()` destructs a`pthread_mutex_t`.
- `pthread_mutex_lock()` locks the mutex.
- `pthread_mutex_unlock()` unlocks the mutex.

```cpp
pthread_mutex_t mutex;
pthread_mutex_init(&mutex, nullptr);
pthread_mutex_lock(&mutex);
pthread_mutex_unlock(&mutex);
pthread_mutex_destroy(&mutex);
```

- `pthread_rwlock_init()` initializes a `pthread_rwlock_t`.
- `pthread_rwlock_destroy()` destructs a `pthread_rwlock_t`.
- `pthread_rwlock_rdlock()` acquires the read-lock.
- `pthread_rwlock_wrlock()` acquires the write-lock.
- `pthread_rwlock_unlock()` unlocks either the read-lock or the write-lock.

### Condition Variable

- `pthread_cond_init()` initializes a `pthread_cond_t`.
- `pthread_cond_destroy()` destructs a `pthread_cond_t`.
- `pthread_cond_timedwait()` waits for a condition variable.
- `pthread_cond_signal()` or `pthread_cond_broadcast()` wakes up either a waiting thread or all waiting threads.

## Futex

The `futex` system call on Linux implements various operations that operate on a 32-bit atomic integer, such as `FUTEX_WAIT` and `FUTEX_WAKE`. The pthread synchronization primitives are implemented with the `futex` system call.

- The wait operation puts a thread to sleep, which takes an argument which specifies the value we expect the atomic variable to have and will refuse to block if it doesn't match.
- The wake operation on the same atomic variable wakes the thread up again. Instead of changing the atomic integer, the kernel records which threads are waiting on which address to allow a wake operation to wake up the threads.

```c
int futex(
  int* uaddr, int futex_op, int val,
  const struct timespec* timeout,
  int* uaddr2, int val3
);
```

- `FUTEX_WAIT` takes an atomic variable, the expected value, and a pointer to a `timespec` representing the maximum time to wait. If the value of the atomic variable matches the expected value, the wait operation blocks. The wait operation might wake up without a corresponding wake operation.
- `FUTEX_WAKE` takes an atomic variable and the number of threads to wake up. It returns the number of awoken threads.
- `FUTEX_WAIT_BITSET` takes the atomic variable, the expected value, a pointer to a `timespec` representing the maximum time to wait, and a 32-bit bitset. It ignores the signal from a `FUTEX_WAKE_BITSET` if the wait bitset and the wake bitset don't have a common bit.
- `FUTEX_WAKE_BITSET` takes an atomic variable, the number of threads to wake up, and a 32-bit bitset. It returns the number of awoken threads.
- `FUTEX_REQUEUE` wakes up a given number of waiting threads and requeues a given number of remaining waiting threads to wait on another atomic variable.

The `FUTEX_PRIVATE_FLAG` informs the kernel that all relevant futex operations on the same atomic variable come from threads of the same process. Therefore, the kernel might improve the performance.
