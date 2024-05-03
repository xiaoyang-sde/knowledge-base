# Argument-Dependent Lookup

Argument-Dependent Lookup (ADL) is the set of rules for looking up the unqualified function names in function-call expressions, including implicit function calls to overloaded operators. These function names are looked up in the namespaces of their arguments and fall back to the usual unqualified name lookup.

```cpp
namespace lib {
class lib_t {};

auto id(lib_t t) -> lib_t { return t; }
} // namespace lib

auto main() -> std::int32_t {
  lib::id(lib::lib_t());

  // Argument-Dependent Lookup
  id(lib::lib_t());
  return 0;
}
```

For example, the operator `<<` is not defined in the global namespace, but ADL examines the `std` namespace because the argument `std::cout` is defined in `std` and finds `std::operator<<(std::ostream&, const char*)`.

```cpp
auto main() -> std::int32_t {
  std::operator<<(std::cout, "Hello");
  // Argument-Dependent Lookup
  operator<<(std::cout, "Hello");
  // Argument-Dependent Lookup
  std::cout << "Hello";
  return 0;
}
```

## Lookup Set

ADL is not performed if an unqualified lookup finds a declaration of a class member, a declaration of a function at block scope, or a declaration that is not a function or a function template, such as a function object. If ADL is performed, for each argument in the function call expression its type is examined to determine the associated set of namespaces and classes that it will add to the lookup. (The rules are non-exhaustive.)

- For an argument of a class type, the class itself, all of its direct and indirect base classes, and its innermost enclosing namespace are added to the set.
- For an argument of a class template specialization, the types of all template arguments provided for type template parameters are examined and their associated set of classes and namespaces are added to the set.
- For an argument of an enumeration type, its innermost enclosing namespace and its enclosing class are added to the set.
- For an argument of a function type, the function parameter types and the function return type are examined and their associated set of classes and namespaces are added to the set.
- If a namespace in the associated set of classes and namespaces is an inline namespace, its enclosing namespace is also added to the set.
- If a namespace in the associated set of classes and namespaces encloses an inline namespace, that inline namespace is added to the set.

After the associated set of classes and namespaces is determined, all declarations found in classes of this set are discarded for further ADL processing, except namespace-scoped friend functions and function templates. (i.e. ADL can find a friend function that is defined within a class or class template even if it's not declared at the namespace level.)

## Two-Step Idiom

### Motivation

It's a common practice to specialize a template in `std` for a specific type, given that specialization is more efficient than the default implementation. For example, before the introduction of move semantics in C++11, a class that manages heap-allocated resources could provide a specialized `std::swap` that swaps the pointers instead of the resources. There are a few methods to specialize such function templates:

- Reopen the `std` namespace and define the template specialization in it, which is a bad practice

```cpp
class lib_t {};

namespace std {
template <> void swap<::lib_t>(::lib_t &a, ::lib_t &b) noexcept {
  // ...
}
} // namespace std
```

- Define the template specialization in the global namespace

```cpp
class lib_t {};

template <> void std::swap<::lib_t>(::lib_t &a, ::lib_t &b) noexcept {
  // ...
}
```

However, this approach doesn't work if the type is a class template, such as `lib_t<T>`, because function templates, such as `std::swap`, don't support partial specialization. (Unlike function templates, class templates support partial specialization. For example, `std::hash` is a class template with an `operator()` overload instead of a function template, allowing for partial specialization, such as `std::hash<std::optional<T>>`.) A workaround would be to introduce an overload for `lib_t<T>`, but overloading a function template in the `std` namespace is undefined behavior.

```cpp
template <typename T> class lib_t {};

// Compile Error: Function template partial specialization is not allowed
template <typename T>
void std::swap<::lib_t<T>>(::lib_t<T> &a, ::lib_t<T> &b) noexcept {
  // ...
}

// Undefined Behavior
namespace std {
template <typename T> void swap(::lib_t<T> &a, ::lib_t<T> &b) noexcept {
  // ...
}
} // namespace std
```

Another workaround is known as the two-step idiom, which exploits argument-dependent lookup. For example, `lib_t<T>` can provide a friend function `swap`. When swapping instances of `lib_t<T>`, instead of invoking `std::swap`, the example first brings `std::swap` into the current scope, and then calls the unqualified `swap` function. In this case, ADL finds the friend function `swap` defined in `lib_t<T>`, while the usual unqualified name lookup finds `std::swap`. Overload resolution prefers the friend function `swap` over `std::swap` because it's more specialized. Because `lib_t<T>` defines `swap` as a friend function, this function can't be found without ADL. (However, if `swap` is defined as a standalone function, it will be a function template, which implies that template argument deduction won't consider implicit conversions to `lib_t<T>`.)

```cpp
#include <utility>

template <typename T> class lib_t {
  friend void swap(lib_t& a, lib_t& b) {
    // ...
  }
};

// template <typename T> void swap(lib_t<T> &a, lib_t<T> &b) {}

auto main() -> std::int32_t {
  lib_t<std::nullptr_t> a;
  lib_t<std::nullptr_t> b;

  using std::swap;
  swap(a, b);
  return 0;
}
```

## Implication

Because of ADL, `std` authors must use qualified names for functions, including internal helper functions. For example, `std_impl::distance` invokes a helper function `std_impl::__distance_impl`. If the helper function is unqualified, the compiler will perform ADL, searching the namespace where `box<incomplete_t>*` is defined. In this case, there will be a compilation error because `incomplete_t` is an incomplete type.

```cpp
namespace std_impl {
template <typename T> auto __distance_impl(T first, T last) {}

template <typename T> auto distance(T first, T last) {
  // return __distance_impl(first, last);
  return std_impl::__distance_impl(first, last);
}
} // namespace std_impl

class incomplete_t;

template <typename T> class box {
public:
  T value;
};

auto main() -> std::int32_t {
  box<incomplete_t> *ptr = nullptr;
  std_impl::distance(ptr, ptr);
  return 0;
}
```
