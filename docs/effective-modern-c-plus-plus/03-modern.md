# Moving to Modern C++

## Distinguish between `()` and `{}` when creating objects

- Brace initialization specifies the initial contents of a container.
- Brace initialization specifies the default initialization values for non-static data members.
- Brace initialization prohibits implicit narrowing conversions among built-in types. For example, assigninig a `double` to `int` is allowed, but brace-initializing a `int` with `double` is prohibited.
- Brace initialization prevents vexing parse. For example, `Widget w1()` will be parsed as a function, but `Widget w1{}` will call the default constructor.
- Brace initialization prefers a constructor taking a `std::initializer_list` than other constructors even if the type doesn't match.

```cpp
vector<int> v1(10, 20); // use non-`std::initializer_list` constructor, which creates a `vector` with 10 elements of `20`
vector<int> v2{10, 20};; // use `std::initializer_list` constructor, which creates a `vector` with 2 elemtns of `10` and `20`
```

## Prefer `const_iterator` to `iterator`

The `const_iterator` is the STL equivalent of pointers-to-const. The standard practice of using `const` whenever possible dictates that `const_iterators` should be preferred. C++ 14 offers non-member versions of `cbegin`, `cend`, `crbegin`, and `crend`, which could be used to obtain `const_iterator` of a container that doesn't support these members.

## `noexcept`

In C++11, unconditional `noexcept` is for functions that guarantee not to emit exceptions. Whether a function is `noexcept` is as important a piece of information. `noexcept` permits compilers to generate better object code. In a `noexcept` function, the optimizer need not keep the runtime stack in an unwindable state if an exception would propagate out of the function, and it won't ensure that objects in a `noexcept` function are destroyed in the inverse order of construction should an exception leave the function.

- Some functions in STL such as `std::vector::push_back` offer the strong exception safety guarantee, because if an exception was thrown during the copying of the elements, the state of the container remained unchanged. In C++11, a natural optimization would be to replace the copying of elements with moves. To prevents the risk of violating the strong exception safety, these functions move the object if it's move constructor is `noexcept`.
- `std::swap` of a container is `noexcept` if the `swap` on the elements is `noexcept`
