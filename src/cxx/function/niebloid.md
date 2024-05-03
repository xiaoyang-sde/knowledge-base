# Niebloid

Niebloid is an function object that doesn't require explicit template argument lists during invocation, invisible to ADL, and disables ADL if it's called with normal unqualified lookup.

The algorithms in `std::ranges` are implemented as Niebloids, which means that the overloads in `std` won't be selected when an algorithm from `std::ranges` is called. In `libc++`, these algorithms are implemented as customization point objects.

For example, when `move` is invoked, the compiler will find `std::ranges::move` from the unqualified name lookup. If `std::ranges::move` is implemented as a normal function, the compiler will perform ADL. If `decltype(buf_in.begin())` is defined as a type in `std`, such as `__gnu_cxx::__normal_iterator`, the compiler will find `std::move` with ADL. In this case, the compiler will select `std::move` because it's more specialized than `std::ranges::move`. However, if `std::ranges::move` is implemented as a Niebloid, the compiler won't perform ADL and will select `std::ranges::move`.

```cpp
namespace std {
// std::move
template <typename InputIt, typename OutputIt>
constexpr OutputIt move(InputIt first, InputIt last, OutputIt d_first);

// std::ranges::move
namespace ranges {
template <
    std::input_iterator I, std::sentinel_for<I> S, std::weakly_incrementable O>
  requires std::indirectly_movable<I, O>
constexpr move_result<I, O> move(I first, S last, O result);
}
} // namespace std

auto main() -> std::int32_t {
  std::vector<std::uint8_t> buf_in;
  std::vector<std::uint8_t> buf_out;

  using namespace std::ranges;
  move(buf_in.begin(), buf_in.end(), buf_out.begin());
}
```
