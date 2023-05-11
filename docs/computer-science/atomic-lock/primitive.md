# Primitive

## Spin-Lock

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
