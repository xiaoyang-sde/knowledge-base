# Atomic

## `std::atomic`

`std::atomic` is a template that defines an atomic type. If different threads are reading and writing to an atomic object, the behavior is well-defined. Accessing to atomic objects could establish inter-thread synchronization with `std::memory_order`.

## Volatile

The object might have a type that is `volatile`-qualified. Each access, such as write operation or member function call, made through a glvalue expression of volatile-qualified type is treated as a visible side-effect for the purposes of optimization. Within a single thread of execution, volatile accesses can't be optimized out or reordered with visible side effect that is sequenced-before or sequenced-after the volatile access.

`volatile` implies that accessing the variable might introduce unobserved side-effects that might not related to the program, such as memory-mapped I/O. For example, if `p` is not `volatile`-qualified, the compiler might dereference `p` once and save the value in a register, optimizing out subsequence dereferences.

```cpp
volatile int *p = /* ... */;
int a = *p;
int b = *p;
```

`volatile` is not intended to resolve issues involving multi-threading. Although the compiler will not reorder statements involving `volatile`-qualified variables, the processor might still reorder instructions.
