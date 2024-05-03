# Customization Point Object

Customization point object (CPO) is a `const` function object with a literal `semiregular` class type (eligible for `constexpr`) that interacts with program-defined types while enforcing semantic requirements on that interaction.

```cpp
template <typename T>
concept semiregular = std::copyable<T> && std::default_initializable<T>;
```

The goal of CPO is to wrap the ADL two-step idiom in `std` implementation. For example, the simplest form of CPO is a lambda expression, because it's an implicit function object and its type is a literal type. Because the compiler won't perform ADL for function objects, invoking `std_impl::swap(a, b)` is equivalent to `swap(a, b)`.

```cpp
#include <utility>

namespace std_impl {
inline constexpr auto swap = [](auto &a, auto &b) {
  using std::swap;
  swap(a, b);
};
} // namespace std_impl

using namespace std_impl;

auto main() -> std::int32_t {
  int a = 0;
  int b = 1;
  swap(a, b);
}
```

In `libc++`'`ranges` implementation, CPO is defined as an explicit function object with a `constexpr operator()`. It defines an `inline constexpr` instance of the function object in the `__cpo` inline namespace.

Suppose there's a class in `std::ranges` that defines a hidden friend `all_of()`. It's not a member function, so it's injected as a function in the `std::ranges` namespace. If the `all_of` CPO is also defined in the `std:ranges` namespace, there will be a compile error, since function objects don't support overloading. Defining the CPO in the `__cpo` inline namespace solves this issue.

```cpp
namespace ranges {
namespace __all_of {

struct __fn {
  template <
      input_range _Range, class _Proj = identity,
      indirect_unary_predicate<projected<iterator_t<_Range>, _Proj>> _Pred>
  _LIBCPP_NODISCARD_EXT _LIBCPP_HIDE_FROM_ABI constexpr bool
  operator()(_Range &&__range, _Pred __pred, _Proj __proj = {}) const {
    // ...
  }
};

} // namespace __all_of

inline namespace __cpo {
inline constexpr auto all_of = __all_of::__fn{};
} // namespace __cpo

} // namespace ranges
```
