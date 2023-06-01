# Smart Pointer

C++ provides smart pointer acts like a regular pointer with the important exception that it could delete the object to which it points. The `shared_ptr` allows multiple pointers to refer to the same object, and the `unique_ptr` which owns the object to which it points. The `weak_ptr` that is a weak reference to an object managed by a `shared_ptr`.

## `unique_ptr`

`unique_ptr` embodies exclusive ownership semantics. Moving a `unique_ptr` transfers ownership from the source pointer to the destination pointer. Upon destruction, a non-null `unique_ptr` destructs its resource. The `unique_ptr` template accepts an argument representing a custom deleter.

## `shared_ptr`

`shared_ptr` embodies shared ownership semantics. The default initialized smart pointer holds a null pointer. Dereferencing a smart pointer returns the object to which the pointer points.

Each `shared_ptr<T>` contains a pointer to an object of type `T` and a pointer to a control block that contains the reference count, weak count, the custom deleter, and the custom allocator.

When a `shared_ptr` is copied or assigned, the pointer to the control block is copied, and the reference count is updated. If the reference count reaches `0`, the object is deallocated.

- `make_shared<T>()` initializes an object and creates a control block through a single allocation call.
- `shared_ptr(unique_ptr&&)` creates a control block because the `unique_ptr` doesn't have a control block.
- `shared_ptr(T*)` creates a control block because the raw pointer doesn't have a control block.

`enable_shared_from_this` allows an object that is managed with a `shared_ptr` named `t` to generate `shared_ptr` instances `t_1`, ..., `t_n` that all share ownership with `t`. The `shared_from_this()` member function returns a `shared_ptr` that shares ownership of the object with `t`, which won't duplicate the control block.

```cpp
class derived_class : public std::enable_shared_from_this<derived_class> {
  std::shared_ptr<derived_class> get_shared_ptr() {
    return shared_from_this();
  }
};
```

## `weak_ptr`

`weak_ptr` is a weak reference to an object that a `shared_ptr` keeps track of. `weak_ptr` won't increase the reference counter and couldn't be dereferenced.  `weak_ptr` prevents leak in eference cycle .

- `expired()` returns `true` when the object is freed
- `lock()` returns a `shared_ptr` to the object or a `nullptr` if the object has been freed

## Allocation

- `new`: The compiler calls `operator new`, which allocates raw, untyped space on heap for the object, and the compiler runs the constructor of the object with the initializers.
- `delete`: The compiler runs the destructor of the object and calls `operator delete`, which deallocates the space on heap.

Applications can define `operator new` and `operator delete` functions in the global scope or as `static` member functions. If the compiler finds a user-defined version, it uses that function to execute the new or delete expression.

```cpp
void *operator new(size_t);
void *operator new[](size_t);
void *operator delete(void *) noexcept;
void *operator delete[](void *) noexcept;

void *operator new(size_t, nothrow_t &) noexcept;
void *operator new[](size_t, nothrow_t &) noexcept;
void *operator delete(void *, nothrow_t &) noexcept;
void *operator delete[](void *, nothrow_t &) noexcept;
```

The placement `new` has the form `new(place_address) type`, which accepts an address and initializes the object at the given address.
