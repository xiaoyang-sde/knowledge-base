# Thread

## `std::thread`

The `std::thread` class takes a top-level function and executes it in a new thread. The return value of the function is ignored and `std::terminate` is called if there's an unhandled exception.

The `std::thread` class is either joinable or unjoinable. Each joinable `std::thread` object represents a thread of execution that is or could be running. Each unjoinable `std::thread` object is either default-constructed, moved from, joined, or detached. If the destructor of a joinable `std::thread` is invoked, the program is terminated.

## `std::jthread`

The `std::jthread` class is an RAII wrapper around `std::thread` such that it joins the thread when it's destructor is invoked. It holds a `std::stop_source` to maintain a shared stop-state. The `std::jthread` constructor accepts a function that takes a `std::stop_token` as its first argument, which allows the function to check if stop has been requested during its execution.

- `std::stop_source` provides a mechanism to issue a stop request. The stop request made for a `std::stop_source` object is visible to all `std::stop_source` objects and `std::stop_token` objects of the same associated stop-state.
- `std::stop_token` is a thread-safe view of the stop-state. The `std::stop_token::stop_requested` member function checks if its stop-state has received a stop request.

## Future

### `std::async`

The `std::async` class takes a top-level function and might execute it in a new thread. It returns a `std::future` that will hold the result of the function. The default launch policies are `std::launch::async | std::launch::deferred`.

- `std::launch::async`: The task is executed on a different thread.
- `std::launch::deferred`: The task is executed on the calling thread the first time its result is requested. The task will block the thread.

```cpp
const auto task = []() -> int {
  std::this_thread::sleep_for(std::chrono::seconds(1));
  return 0;
};

std::future<int> future = std::async(std::launch::async, task);
std::cout << future.get() << std::endl;
```

### `std::promise`

The `std::promise` class provides a mechanism to store a value or an exception that is later acquired through an `std::future` object.

The promise is the producer of the promise-future communication channel. The operation that stores a value in the shared state synchronizes with the successful return from the function that is waiting on the shared state.

```cpp
std::promise<int> promise;
promise.set_value(0);

std::future<int> future = promise.get_future();
```

### `std::future`

The `std::future` class provides a mechanism to access the result of asynchronous operations. Each asynchronous operation created through `std::async`, `std::packaged_task`, or `std::promise` could provide a `std::future` object to its creator and write the result of the operation to the shared state of `std::future`. The creator could wait for or extract a value from the `std::future`.

The shared state is a reference-counted heap-based object that stores the value generated from the promise. The destructor for the last future referring to a shared state for a non-deferred task launched through `std::async` blocks until the task completes.

### `std::packaged_task`

The `std::packaged_task` class wraps a callable object such that its return value or exception thrown is stored in a shared state which can be accessed through `std::future objects`.

## Mutex

- `std::mutex` is synchronization primitive that can be used to protect shared data from data races.
- `std::recursive_mutex` is identical to `std::mutex` with the exception that it can be locked and unlocked multiple times in a recursive pattern.
- `std::shared_mutex` is a read-write lock.
- `std::scoped_lock` is an RAII wrapper that owns zero or more locks for the duration of a scoped block.
- `std::unique_lock` is an RAII wrapper that owns a lock. It supports manual locking and unlocking.
- `std::shared_lock` is identical to `std::unique_lock` with the exception that it's designed to support shared access to `std::shared_mutex`.

## Condition Variable

The `std::condition_variable` class is a synchronization primitive used with a `std::mutex` to block one or more threads until a separated thread both modifies a shared variable and notifies the `std::condition_variable`.

- The thread that intends to change the shared variable must acquire a `std::mutex`, change the shared variable, and call `notify_one` or `notify_all` on the `std::condition_variable`, which can be done after releasing the lock.
- The thread that intends to wait on a a `std::condition_variable` must acquire a `std::unique_lock<std::mutex>` to protect the shared variable and invokes the predicated `wait` on the `std::condition_variable`.

```cpp
std::condition_variable condition;
std::mutex mutex;
bool flag = false;

std::thread thread([&condition, &mutex, &flag]() -> void {
  std::unique_lock<std::mutex> lock(mutex);
  condition.wait(lock, [&flag]() -> bool { return flag; });
  std::cout << 1 << std::endl;
});

{
  std::scoped_lock<std::mutex> _(mutex);
  flag = true;
}
condition.notify_one();
thread.join();
```

## Semaphore

The `std::counting_semaphore` class is a lightweight synchronization primitive that controls access to a shared resource. Unlike a `std::mutex`, a `std::counting_semaphore` allows multiple concurrent access to the same resource. It's initialized with an internal counter, which is incremented with `release()` and decremented with `acquire()`. When the counter is zero, `acquire()` blocks until the counter is incremented.

```cpp
std::counting_semaphore semaphore(4);
std::vector<std::thread> thread_list;
for (int i = 0; i < 128; ++i) {
  thread_list.emplace_back([&semaphore, i]() {
    semaphore.acquire();
    std::this_thread::sleep_for(std::chrono::seconds(1));
    semaphore.release();
  });
}
```

## Implementation

### `spin_lock`

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
