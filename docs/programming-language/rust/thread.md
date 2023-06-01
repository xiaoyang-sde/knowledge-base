# Thread

## `std::thread`

The program starts with the main thread, which will execute the `main` function and can be used to spawn more threads if requested. In Rust, threads are spawned using the `std::thread::spawn` function, which takes a function and execute it.

Returning from `main` will exit the entire program, even if other threads are still running. The `JoinHandle` returned from the `spawn` function can be used to wait for a thread to finish.

```rs
use std::thread;

fn main() {
  let thread_handle = thread::spawn(|| {
    let thread_id = thread::current().id();
    println!("thread id: {:?}", thread_id);
  });
  thread_handle.join().unwrap();
}
```

The `spawn` function has a `'static` lifetime bound on its argument type, thus it accepts functions that live forever. Therefore, if a closure is passed to `spawn`, it should take the ownership of the captured items, because the spawned thread might live longer than other threads.

```rs
let number_list = vec![1, 2, 3];
thread::spawn(move || println!("{:?}", number_list)).join().unwrap();
```

The spawned thread could return a value through the closure, and the return value could be obtained from the `Result` returned from the `join` method.

## `std::thread::scope`

The `std::thread::scope` function creates a scope for spawning scoped threads. The function passed to scope will be provided a `Scope` object, through which scoped threads can be spawned. These threads can borrow non-`'static` data, as the scope guarantees all threads will be joined at the end of the scope.

```rs
let number_list = vec![1, 2, 3];
thread::scope(|scope| {
  scope.spawn(|| println!("{:?}", number_list));
  scope.spawn(|| println!("{}", number_list.len()));
});
```

## Shared Ownership and Reference Counting

When sharing data between two threads where neither thread is guaranteed to outlive the other, neither of them can be the owner of that data. There are several methods to create a value that outlives the longest living thread.

- `static`: The program owns `static` items. The `static` item has a constant initializer, is never dropped, and exists before the `main` function starts.

- `Box::leak`: The `Box::leak` function releases the ownership of a `Box`, which makes it live forever. However, the item won't be deallocated.

```rs
let number_list: &'static _ = Box::leak(Box::new([1, 2, 3]));
thread::spawn(move || println!("{:?}", number_list));
```

- `Rc<T>`: `Rc<T>` represents a value with shared ownership. Cloning an `Rc<T>` will increment the counter and dropping an `Rc<T>` will decrement the counter. `Rc<T>` drops its owned data when the count is `0`. However, `Rc<T>` is not thread-safe.

- `Arc<T>`: `Arc<T>` is identical to `Rc<T>`, except it guarantees that modifications to the reference counter are indivisible atomic operations, making it safe to use it with multiple threads.

```rs
let number_list = Arc::new([1, 2, 3]);
thread::spawn({
  let number_list = number_list.clone();
  move || println!("{:?}", number_list)
});
println!("{:?}", number_list);
```

## Borrowing and Data Race

Data races are situations where one thread is mutating data while another thread is accessing it. Rust's borrow checking system prevent data races.

- Immutable borrowing: Borrowing something with `&` gives an shared reference. Such a reference can be copied. Access to the data it references is shared between all copies of such a reference.

- Mutable borrowing: Borrowing something with `&mut` gives a exclusive reference. Such a reference guarantees it's the unique active borrow of the data.

The basic borrowing rules are simple, but can be quite limited when multiple threads are involved, because no data that's accessible from multiple threads can be mutated.

The Rust compiler could make useful assumptions based on the borrowing rules. For example, `a` and `b` can't refer to the same address in Rust, thus the Rust compiler can omit the invocation of `x()`.

```rs
fn f(a: &i32, b: &mut i32) {
  let before = *a;
  *b += 1;
  let after = *a;
  if before != after {
    x();
  }
}
```

## Interior Mutability

The data types with interior mutability bend the borrowing rules. Under certain conditions, these types allow mutation through an immutable reference.

- `UnsafeCell<T>`: `UnsafeCell<T>` is a wrapper of `T` and is the primitive building block for interior mutability. It doesn't enforce borrowing rules and its `get()` method returns a raw pointer to the value it wraps.

- `Cell<T>`: `Cell<T>` is a wrapper of `T` that allows mutation through a shared reference. To avoid undefined behavior, it allows copying the value out if `T` is `Copy` or replacing the value with another value. `Cell<T>` is not thread-safe.

```rs
use std::cell::Cell;

fn f(v: &Cell<Vec<i32>>) {
  let mut v2 = v.take();
  v2.push(1);
  v.set(v2);
}
```

- `RefCell<T>`: `RefCell<T>` is a wrapper of `T` that allows mutation through a shared reference. It allows to borrow its value at runtime cost and holds a counter that keeps track of outstanding borrows. It panics if the program attempts to create a exclusive borrow more than once. `RefCell<T>` is not thread-safe.

```rs
use std::cell::RefCell;

fn f(v: &RefCell<Vec<i32>>) {
  v.borrow_mut().push(1);
}
```

- `RwLock<T>`: `RwLock<T>` is the concurrent version of a `RefCell<T>`. It allows to borrow its value at runtime cost and holds a counter that keeps track of outstanding borrows. Rather than panicking, it blocks the current thread while waiting for conflicting exclusive borrows to disappear.

- `Mutex<T>`: `Mutex<T>` is similar to `RwLock<T>`. The difference is that it doesn't allow shared borrows.

- Atomic: The atomic type is the concurrent version of a fixed size `Cell<T>`. There are specific atomic types such as `AtomicU32` and `AtomicPtr<T>`.

## `Send` and `Sync`

- `Send`: The ownership of a value of a type with `Send` trait can be transferred to another thread. The `thread::spawn` function requires its argument to be `Send`, and a closure is `Send` if all of its captures are `Send`.

- `Sync`: The value of a type with `Sync` trait can be shared with another thread. `T` is `Sync` if its shared reference `&T` is `Send`.

Primitive types such as `i32`, `bool`, and `str` are both `Send` and `Sync`. Algebraic data types are `Send` and `Sync` if all fields are `Send` and `Sync`. Raw pointers (`*const T` and `*mut T`) are neither `Send` nor `Sync`.

- The `unsafe impl` block can be used to opt in to either of these traits. `unsafe` implies that the compiler won't check for the correctness.
- The `PhantomData<T>` can be used to opt out of either of these traits, which is a zero-sized type that make the compiler interpret it as `T`.

```rs
struct X {
  p: *mut i32,
}

unsafe impl Send for X {}
unsafe impl Sync for X {}
```

## Locking

### `std::sync::Mutex<T>`

The most common tool for sharing mutable data between threads is a mutex, which ensures threads have exclusive access to some data and blocks other threads that attempt to access it at the same time. When a thread attempts to lock a locked mutex, it will be put to sleep until the mutex is unlocked. Unlocking will cause one of the waiting threads to be woken up.

`std::sync::Mutex<T>` protects a data of type `T: Send`, which provides a safe interface that can guarantee all threads will uphold the agreement. Its `lock()` method returns a `MutexGuard`, which behaves like an exclusive reference through the `DerefMut` trait, giving the exclusive access to the data the mutex protects. The `Drop` implementation of `MutexGuard` will unlock the mutex.

```rs
fn main() {
    let n = Mutex::new(0);
    thread::scope(|s| {
        for _ in 0..10 {
            s.spawn(|| {
                let mut guard = n.lock().unwrap();
                for _ in 0..100 {
                    *guard += 1;
                }
            });
        }
    });
    assert_eq!(n.into_inner().unwrap(), 1000);
}
```

The mutex gets marked as poisoned when a thread panics while holding the lock. When that happens, the mutex will no longer be locked, but calling its lock method will result in an `Err` to indicate it has been poisoned. It indicates that the data in the mutex might be an inconsistent state, and other threads should either handle it or propagates the panic.

### `std::sync::RwLock<T>`

The read-write lock is a more compliated version of a mutex that understands the difference between exclusive and shared access. It allows multiple threads to read the data at the same time.

`std::sync::RwLock<T>` protects a data of type `T: Send + Sync`, which provides a safe interface that can guarantee all threads will uphold the agreement. Its `read()` method locks as a reader and returns a `RwLockReadGuard`, which behaves like a shared reference through the `DerefMut` trait. Its `write()` method locks as a writer and returns a `RwLockWriteGuard`, which behaves like an exclusive reference through the `DerefMut` trait

## Parking and Condition Variable

### `std::thread::park()`

While a mutex allows threads to wait until it becomes unlocked, it's not able to wait for other conditions.

`std::thread::park()` blocks the current thread until the current thread's token is made available. Other threads could invoke the `unpark()` method of the parked thread's handle to resume the thread. `park()` doesn't guarantee that it will return because of a matching `unpark()`.

```rs
let queue = Mutex::new(VecDeque::new());

thread::scope(|scope| {
  let consumer = scope.spawn(|| loop {
    let item = queue.lock().unwrap().pop_front();
    if let Some(item) = item {
      println!("{:?}", item);
    } else {
      thread::park();
    }
  });

  for i in 0.. {
    queue.lock().unwrap().push_back(i);
    consumer.thread().unpark();
    thread::sleep(Duration::from_millis(100));
  }
})
```

### `std::sync::Condvar`

The condition variable are common option for waiting for something to happen to data in a mutex. The thread can wait on a condition variable, after which it can be woken up when another thread notifies the same condition variable. Multiple threads can wait on the same condition variable, and notifications can either be sent to one waiting thread, or to all of them.

`std::sync::Condvar` has a `wait` method that takes a `MutexGuard` that proves the thread has been locked. It first unlocks the mutex and goes to sleep. When woken up, it relocks the mutex and returns a new `MutexGuard`. The `wait_timeout` method takes a `Duration` and waits for a notification or a timeout.

## Implementation

## `spin_lock`

The implementation of the `spin_lock` primitive leverages an `atomic_bool` to represent the state of the lock, which follows the [`Mutex` named requirement](https://en.cppreference.com/w/cpp/named_req/Mutex) of C++.

- The `lock()` method waits on `load` rather than `exchange`, because `exchange` might claim exclusive write access to the cache line where the lock is stored.

- The `try_lock()` method first checks if the lock is free before attempting to acquire it to prevent claiming redundant exclusive write access.

```cpp
#include <atomic>
#include <thread>

class spin_lock {
public:
  auto lock() noexcept -> void {
    while (lock_.exchange(true, std::memory_order_acquire)) {
      while (lock_.load(std::memory_order_relaxed)) {
        std::this_thread::yield();
      }
    }
  }

  auto try_lock() noexcept -> bool {
    return !lock_.load(std::memory_order_relaxed) &&
           !lock_.exchange(true, std::memory_order_acquire);
  }

  auto unlock() noexcept -> void {
    lock_.store(false, std::memory_order_release);
  }

private:
  std::atomic_bool lock_{false};
};
```
