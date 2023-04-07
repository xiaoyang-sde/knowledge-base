# Move Semantic

## `std::move` and `std::forward`

- `std::move` casts its argument to an rvalue, which informs the compiler that the object is eligible to be moved from.. It applies `std::remove_reference` to `T`, thus ensuring that `&&` is applied to a type that isn't a reference.

- `std::forward` casts its argument to an rvalue if the argument was initialized with an rvalue.

```cpp
template<typename T>
decltype(auto) move(T&& param) {
  return static_cast<remove_reference_t<T>&&>(param);
}
```
