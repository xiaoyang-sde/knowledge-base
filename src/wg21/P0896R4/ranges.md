# Ranges

## Concepts

### `range`

```cpp
template <typename T>
concept range = requires(T &t) {
  ranges::begin(t);
  ranges::end(t);
};
```

The `range` concept specifies the requirements for a type `T` to be considered a range. `T` must provide an iterator and a sentinel, representing the begin and end of the range. Both `ranges::begin(T)` and `ranges::end(T)` must be constant and amortized constant time operations.

### `sized_range`

```cpp
template <typename T>
concept sized_range = ranges::range<T> && requires(T &t) { ranges::size(t); };

template <typename> inline constexpr bool disable_sized_range = false;
```

The `sized_range` concept specifies the requirements of a `range` type that knows its size in constant time with the `ranges::size` function.

However, there are types that provide a `size` function but don't model `sized_range`. For instance, a pre-C++11 `std::list` has a linear-time `size` method. The `disable_sized_range` variable template exists to allow such types to opt out of being considered as `sized_range`.

### `borrowed_range`

```cpp
template <typename R>
concept borrowed_range =
    ranges::range<R> &&
    (std::is_lvalue_reference_v<R> || ranges::enable_borrowed_range<std::remove_cvref_t<R>>);
```

The `borrowed_range` concept specifies the requirements of a `range` type that it doesn't own its data, so a function can take it and return iterators obtained from it without danger of dangling. All rvalue ranges are assumed to be non-borrowing, except for these non-owning types, such as `std::string_view`, `std::span`, and `ranges::subrange`.

When an algorithm that returns an iterator or a `subrange` of a range that takes a particular rvalue range argument that doesn't model `borrowed_range`, the `ranges::dangling` placeholder type will be returned to avoid returning dangling iterators. `ranges::dangling` can't be dereferenced.

```cpp
// `it` is an iterator
std::vector<int> v;
auto it = ranges::find(v, 99);

// `it` is `ranges::dangling`
auto it = ranges::find(std::vector<int>(), 99);

// `it` is an iterator
std::vector<int> v;
auto it = ranges::find(std::span(v), 99);
```

### `view`

```cpp
template <typename T>
concept view = ranges::range<T> && std::movable<T> && ranges::enable_view<T>;
```

The `view` concept specifies the requirements of a `range` type that has suitable semantic properties for use in constructing range adaptor pipelines. `T` models `view` if its move operations and destruction are constant, such as `std::string_view`, `std::span`, and `ranges::subrange`.

The `enable_view<T>` variable template is used to indicate whether a range is a view. It evaluates to `true` if either `T` is derived from `view_base` or `T` has a public base class `view_interface<U>` and doesn't have base classes of type `view_interface<V>`.

### `viewable_range`

```cpp
template <typename T>
concept viewable_range =
    ranges::range<T> && ((ranges::view<std::remove_cvref_t<T>> &&
                          std::constructible_from<std::remove_cvref_t<T>, T>) ||
                         (!ranges::view<std::remove_cvref_t<T>> &&
                          (std::is_lvalue_reference_v<T> ||
                           (std::movable<std::remove_reference_t<T>> &&
                            !/*is-initializer-list*/<T>))));
```

The `viewable_range` concept specifies the requirements of a `range` type that can be converted into a `view` through `ranges::view_all`.

### `common_range`

```cpp
template <typename T>
concept common_range =
    ranges::range<T> &&
    std::same_as<ranges::iterator_t<T>, ranges::sentinel_t<T>>;
```

The `common_range` concept specifies the requirements of a `range` type for which `ranges::begin()` and `ranges::end()` return the same type.

## Access

The proposal introduces several utilities that retrieve certain properties from a range.

- `ranges::begin` returns an iterator to the beginning of a range.
- `ranges::end` returns a sentinel indicating the end of a range.
- `ranges::rbegin` returns a reverse iterator to a range.
- `ranges::rend` returns a reverse end iterator to a range.
- `ranges::size` returns an integer equal to the size of a range.
- `ranges::data` obtains a pointer to the beginning of a contiguous range.

These functions are implemented as customization point objects, which will attempt to find user-provided customized implementations before falling back to a default implementation. For example, for an element `t` with type `T`, `ranges::size(t)` will attempt to invoke `t.size()` and `size(t)`. If these invocations are not available, it will attempt to calculate `ranges::end(t) - ranges::begin(t)` and cast the result to an unsigned integer-like type. Therefore, to customize the behavior of `ranges::size`, `T` can provide a `size` function that is either a member or a non-member. The following code snippet is a simplified implementation of `ranges::size`:

```cpp
template <typename T>
concept size_enabled =
    !ranges::disable_sized_range<std::remove_cvref_t<T>>;

template <typename T>
concept member_size = size_enabled<T> && requires(T &&t) {
  { auto(t.size()) } -> std::integral;
};

template <typename T>
concept unqualified_size = size_enabled<T> && requires(T &&t) {
  { auto(size(t)) } -> std::integral;
};

template <typename T>
concept iter_difference =
    !member_size<T> && !unqualified_size<T> && requires(T &&t) {
      { ranges::begin(t) } -> std::forward_iterator;
      {
        ranges::end(t)
      } -> std::sized_sentinel_for<decltype(ranges::begin(std::declval<T>()
        ))>;
    };

struct size_fn {
  // `auto(t.size())` is a valid expression
  template <typename T>
    requires member_size<T>
  [[nodiscard]] constexpr auto static operator()(T &&t
  ) noexcept(noexcept(auto(t.size()))) {
    return auto(t.size());
  }

  // `auto(size(t))` is a valid expression
  template <typename T>
    requires unqualified_size<T>
  [[nodiscard]] constexpr auto static operator()(T &&t
  ) noexcept(noexcept(auto(size(t)))) {
    return auto(size(t));
  }

  // default implementation
  template <typename T>
    requires iter_difference<T>
  [[nodiscard]] constexpr auto static operator()(T &&t
  ) noexcept(noexcept(auto(size(t)))) {
    return static_cast<std::uint64_t>(
        ranges::end(t) - ranges::begin(t)
    );
  }

  /* ... */
};

inline constexpr auto size = size_fn{};
```

## Utilities

### `view_interface`

```cpp
template <typename D>
  requires std::is_class_v<D> && std::same_as<D, std::remove_cv_t<D>>
class view_interface;
```

The `view_interface` CRTP-based class template defines a view interface. The derived class is required to define a `begin` member function and an `end` member function, and `view_interface` will provide several other functions using these member functions. The following code snippet is a simplified implementation of `view_interface`:

```cpp
template <typename D>
  requires std::is_class_v<D> && std::same_as<D, std::remove_cv_t<D>>
class view_interface {
private:
  constexpr D &derived() noexcept { return static_cast<D &>(*this); }

public:
  constexpr bool empty()
    requires std::ranges::sized_range<D> || std::ranges::forward_range<D>
  {
    if constexpr (std::ranges::sized_range<D>) {
      return std::ranges::size(derived()) == 0;
    } else {
      return std::ranges::begin(derived()) == std::ranges::end(derived());
    }
  }

  /* ... */
};
```

### `subrange`

```cpp
enum class subrange_kind : bool { unsized, sized };

template <
    std::input_or_output_iterator I, std::sentinel_for<I> S = I,
    ranges::subrange_kind K = std::sized_sentinel_for<S, I>
                                  ? ranges::subrange_kind::sized
                                  : ranges::subrange_kind::unsized>
  requires(K == ranges::subrange_kind::sized || !std::sized_sentinel_for<S, I>)
class subrange : public ranges::view_interface<subrange<I, S, K>>;
```

The `subrange` class template combines together an iterator and a sentinel into a single `view`. The `subrange` is a `sized_range` whenever the final template parameter is `subrange_kind​::​sized`. The size record is needed to be stored if `std::sized_sentinel_for<S, I>` is `false` and `K` is `subrange_kind::sized`. The constraint prohibits the scenario when `K` is `subrange_kind::unsized` and `std::sized_sentinel_for<S, I>` is `true`.

```cpp
auto main() -> std::int32_t {
  std::vector<std::uint8_t> buf(512);
  std::ranges::subrange buf_view(buf);
  static_assert(sizeof(buf_view) == 16);
  return 0;
}
```

## Adaptors

### `ranges::ref_view`

```cpp
template <ranges::range R>
  requires std::is_object_v<R>
class ref_view : public ranges::view_interface<ref_view<R>>;
```

The `ref_view` class template is a `view` of the elements of some range. It wraps a reference to that range.

### `ranges::owning_view`

```cpp
template <ranges::range R>
  requires std::movable<R> && (!/*is-initializer-list*/<R>)
class owning_view : public ranges::view_interface<owning_view<R>>;
```

The `owning_view` class template is a `view` that has unique ownership of a range. It is movable and stores the range within it. Its constructor accepts an rvalue reference and will move it into the stored range.

### `views::all`

The `views::all` range adaptor closure object returns a `view` that includes all elements of its range argument.

- If `std::decay_t<T>` models `view`, `ranges::view_all` will convert the range into a `std::decay_t<T>` prvalue.
- If `T` is an lvalue reference, `ranges::view_all` will return a `ref_view` that refers to the range.
- If `T` is a movable, `ranges::view_all` will return an `owning_view` that takes ownership of the range.
