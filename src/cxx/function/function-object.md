# Function Object

A function object is an object for which the function call operator `operator()` is defined.

## Invocation

The `INVOKE(f, arg_0, arg_1, ..., arg_n)` operation is defined as follows, where `T` is `std::remove_cvref_t<decltype(arg_0)>`:

- If `f` is a pointer to a member function of class `C`, then the operation is equivalent to:
  - If `std::is_same_v<C, T> || std::is_base_of<C, T>` is `true`, invoke the member function on the object.
  - If `T` is a specialization of `std::reference_wrapper`, invoke the member function on the referred object
  - Otherwise, invoke the member function on the dereferenced object.
- If `n == 0` and `f` is a pointer to a data member of class `C`, then the operation is equivalent to:
  - If `std::same_as_v<C, T> || std::is_base_of<C, T>` is `true`, access the data member of the object.
  - If `T` is a specialization of `std::reference_wrapper`, access the data member of the referred object.
  - Otherwise, access the data member of the dereferenced object.
- Otherwise, the operation is equivalent to `f(arg_0, arg_1, ..., arg_n)`.

The `INVOKE<R>(f, arg_0, arg_1, ..., arg_n)` operation is defined as executing `INVOKE(f, arg_0, arg_1, ..., arg_n)` and converting the result to `R`. If the conversion operation binds `R` to a temporary object, the operation is ill-formed.

## `invoke` and `invoke_r`

`invoke` and `invoke_r` can invoke a callable object with given arguments based on the rules of `INVOKE` and `INVOKE<R>`. `invoke_result_t` represents the return type of an `INVOKE` operation deduced at compile time.

```cpp
template <class F, class... Args>
  requires std::is_invocable_v<F, Args...>
std::invoke_result_t<F, Args...>
invoke(F &&f, Args &&...args) noexcept(std::is_nothrow_invocable_v<F, Args...>);

template <class R, class F, class... Args>
  requires std::is_invocable_r_v<R, F, Args...>
constexpr R invoke_r(F &&f, Args &&...args) noexcept(std::is_nothrow_invocable_r_v<R, F, Args...>);
```
