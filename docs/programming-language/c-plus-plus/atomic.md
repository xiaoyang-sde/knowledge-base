# Atomic

## `std::atomic<T>`

`std::atomic<T>` is a template that defines an atomic type. If different threads are reading and writing to an atomic object, the behavior is well-defined. Accessing to atomic objects could establish inter-thread synchronization with `std::memory_order`.

- `store()` replaces the value of the atomic object with a non-atomic argument in an atomic operation.
- `load()` obtains the value of the atomic object in an atomic operation.
- `compare_exchange_strong()` compares the value of the atomic object with non-atomic argument and performs atomic exchange if equal or atomic load if not.
- `compare_exchange_weak()` is identical to `compare_exchange_strong()`, with the exception that it might fail even if the value of the atomic object is equal to the argument. When invoked in a loop, `compare_exchange_weak()` will perform better in some platforms.
- `wait()` takes a value and blocks until the atomic object no longer contains the given value.
- `notify_one()` notifies at least one thread waiting on the atomic object.
- `notify_all()` notifies all threads waiting on the atomic object.

If `T` is an integral type, the template implements special methods such as `fetch_add()`, `fetch_sub()`, `fetch_and()`, `fetch_or()`, `fetch_xor()`.

## Ordering

`std::memory_order` specifies how memory accesses, including regular, non-atomic memory accesses, are to be ordered around an atomic operation. Without constraints on a multi-core system, when multiple threads read and write to several variables, one thread can observe the values change in an order different from the order another thread wrote them.

```cpp
enum class memory_order {
  relaxed,
  consume,
  acquire,
  release,
  acq_rel,
  seq_cst
};
```

## Volatile

The object might have a type that is `volatile`-qualified. Each access, such as write operation or member function call, made through a glvalue expression of volatile-qualified type is treated as a visible side-effect for the purposes of optimization. Within a single thread of execution, volatile accesses can't be optimized out or reordered with visible side effect that is sequenced-before or sequenced-after the volatile access.

`volatile` implies that accessing the variable might introduce unobserved side-effects that might not related to the program, such as memory-mapped I/O. For example, if `p` is not `volatile`-qualified, the compiler might dereference `p` once and save the value in a register, optimizing out subsequence dereferences.

```cpp
volatile int *p = /* ... */;
int a = *p;
int b = *p;
```

`volatile` is not intended to resolve issues involving multi-threading. Although the compiler will not reorder statements involving `volatile`-qualified variables, the processor might still reorder instructions.
