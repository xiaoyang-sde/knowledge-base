# Concurrency

## Introduction

The **thread** is an abstraction in single running process that is a point of execution in a process. The threads in a process share the same address space, and each thread reserves its own stack (thread-local storage). The system performs a context switch while switching between threads.

The **critical section** is a piece of code that accesses a shared resource. The **race condition** (data race) arises if multiple threads of execution enter the critical section at the same time and attempt to update the shared structure. If a program consists of at least one race conditions, its outcome is not deterministic. To avoid race conditions, threads implement **mutual exclusion** primitives to guarantee that only a single thread could enter a critical section.

## Thread API

### Thread Creation

The `pthread_create` function starts a new thread in the calling process. The new thread starts execution by invoking the `start_routine` function with the single argument `arg`. The `attr` specifies attributes of the thread, which is initialized with `pthread_attr_init()`.

```c
#include <pthread.h>

int pthread_create(
  pthread_t *thread,
  const pthread_attr_t *attr,
  void *(*start_routine)(void*),
  void *arg
);
```

### Thread Completion

The `pthread_join` function waits for a thread to complete. The `value_ptr` argument is a pointer to the return value of the thread.

```c
int pthread_join(
  pthread_t thread,
  void **value_ptr
);
```

### Mutex Lock

The lock provides mutual exclusion to a critical section.  If no other thread holds the lock when `pthread_mutex_lock` is called, the thread will acquire the lock and enter the critical section. The lock variable could be initialized with `pthread_mutex_init` and destroyed with `pthread_mutex_destroy`.

```c
int pthread_mutex_lock(pthread_mutex_t *mutex);

int pthread_mutex_unlock(pthread_mutex_t *mutex);
```

### Condition Variable

Condition variable is useful when a thread must wait for another to do something before it can continue. The condition variable has to be associated with a lock, which should be held while calling these routines. `pthread_cond_wait` puts the calling thread to sleep and waits for other thread to signal it.

```c
int pthread_cond_wait(
  pthread_cond_t *cond,
  pthread_mutex_t *mutex
);

int pthread_cond_signal(
  pthread_cond_t *cond
);
```

## Lock

The lock ensures that a critical section executes as a single atomic instruction. The lock variable holds the state of the lock and is either available or acquired. The thread that calls the routine `lock()` acquires the lock, enters the critical section, and calls the routine `unlock()` to free the lock.

### Controlling Interrupt

The earliest solution used to provide mutual exclusion is to disable interrupts for critical sections. If the thread is running on a single-processor system and the code inside the critical section could not be interrupted, the critical section will be executed as if it is atomic.

- The approach requires the thread to perform a privileged operation. Malicious programs could never enable the interrupt to prevent the system to regain control.
- The approach doesn't work on multiprocessors since turning off the interrupts on a core doesn't affect other cores.
- The approach turns off interrupts for extended periods of time, which could lead to interrupts becoming lost.
- The approach uses the instruction that masks or unmasks interrupts is inefficient in moderm CPUs.

### `test-and-set` and `compare-and-swap`

The `test-and-set` approach uses a single flag variable to indicate wheher a thread has possession of a lock. The first thread that enters the critical section tests whether the flag is `1` and sets the flag to `1` to indicate that the lock is acquired with the atomic `test-and-set` or `compare-and-swap` instruction. When finished the critical section, the thread clears the flag to release the lock. Other threads enter the critical section will **spin-wait** in the while loop until the first thread clears the flag.

- The approach doesn't guarantee fairness and could led to starvation.
- The approach introduces significant performance overhead on single CPU. If $N$ threads contending for a lock, $N - 1$ time slices might be wasted because the threads are spinning.

```c
int test_and_set(
  int *old_ptr,
  int new
) {
  int old = *old_ptr;
  *old_ptr = new;
  return old;
}

int compare_and_swap(
  int *ptr,
  int expected,
  int new,
) {
  int original = *ptr;
  if (original == expected) {
    *ptr = new;
  }
  return original;
}
```

### Queue

The queue approach maintains a queue data structure to store the threads that will acquire the lock. The `guard` is a `test_and_set` lock that protects the queue and `flag` variables. When a thread can't acquire the lock, it will add itself to the queue and yield the CPU.

- `park()`: put the calling thread to sleep
- `unpark(thread_id)`: wake the thread with id `thread_id`

```c
typedef struct __lock_t {
  int flag;
  int guard;
  queue_t *q;
} lock_t;

void lock_init(lock_t *m) {
  m->flag = 0;
  m->guard = 0;
  queue_init(m->q);
}

void lock(lock_t *m) {
  while (test_and_set(&m->guard, 1) == 1) {}
  if (m->flag == 0) {
    m->flag = 1;
    m->guard = 0;
  } else {
    queue_add(m->q, gettid());
    m->guard = 0;
    park();
  }
}

void unlock(lock_t *m) {
  while (test_and_set(&m->guard, 1) == 1) {}
  if (queue_empty(m->q)) {
    m->flag = 0;
  } else {
    unpark(queue_remove(m->q));
  }
  m->guard = 0;
}
```

## Lock-based Concurrent Data Structures

### Concurrent Counter

- The simple concurrent counter maintains a counter for all the CPU cores. The thread acquires the lock, increments the counter, and release the lock.
- The scalable concurrent counter maintains a local counter for each CPU core and a global counter. The thread acquires the lock, increments the local counter, and releases the lock. When the local lock reaches a threshold $S$, it transfers its value to the global counter and reset itself to $0$.

### Concurrent Linked List

- The simple concurrent linked list maintains a lock for all the nodes in the list. The thread acquires the lock, modifies the list, and releases the lock.
- The scalable concurrent linked list uses the hand-over-hand locking strategy, which maitnains a lock for each node of the list. When traversing the list, the thread acquires the next node's lock and then releases the current node's lock.

### Concurrent Queue

- The simple concurrent queue maintains a lock for all the elements in the queue. The thread acquires the lock, modifies the queue, and releases the lock.
- The scalable concurrent queue maintains a lock for the head and a lock for the tail to enable enqueue and dequeue operations.
