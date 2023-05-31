# Smart Pointer

C++ provides smart pointer acts like a regular pointer with the important exception that it could delete the object to which it points. The `shared_ptr` allows multiple pointers to refer to the same object, and the `unique_ptr` which owns the object to which it points. The `weak_ptr` that is a weak reference to an object managed by a `shared_ptr`.

## `shared_ptr`

`shared_ptr` is a template. The default initialized smart pointer holds a null pointer. Dereferencing a smart pointer returns the object to which the pointer points.

```cpp
shared_ptr<string> string_1;
cout << *string_1 << endl;
```

`make_shared` allocates a dynamic memory. The function allocates and initializes an object in dynamic memory and returns a `shared_ptr` that points to that object.

```cpp
shared_ptr<string> string_2 = make_shared<string>(10, 'a');
shared_ptr<string> string_3 = make_shared<string>("114514");
```

When a `shared_ptr` is copied or assigned, it tracks the number of `shared_ptr`s point to the same object, which is the reference count. When the count goes to 0, the object it tracks is freed.

- `get()` returns the raw pointer
- `use_count()` returns the reference count
- `unique()` returns `true` if `use_count() == 1` (deprecated)

## `weak_ptr`

`weak_ptr` is a weak reference to an object that a `shared_ptr` keeps track of. `weak_ptr` won't increase the reference counter and couldn't be dereferenced.  `weak_ptr` prevents leak in eference cycle .

- `expired()` returns `true` when the object is freed
- `lock()` returns a `shared_ptr` to the object or a `nullptr` if the object has been freed

## `unique_ptr`

`shared_ptr` behaves like `unique_ptr` and the difference is that there's at most one `unique_ptr` that points to the same object. `unique_ptr` can't be copied or assigned, but the object could be transferred to another `unique_ptr` with `std::move`.

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
