# Template

Template is the foundation for generic programming. Each template is a blueprint or formula for creating classes or functions. The compiler generates concrete classes or functions during compile time.

Each template definition starts with the keyword `template` with a template parameter list, which is a comma-separated list of one or more template parameters bracketed with `<>` tokens. Template parameters represent types or values used in the definition of a class or function. The actual type of the parameters are determined at compile time.

- Type parameter is a type specifier, which can be used to name the return type or a function parameter type, and for variable declarations or casts inside the function.
- Non-type parameter represents a value rather than a type, which can be an integral type, a pointer or lvalue reference to an object with static lifetime or to a function type, or constant expression.

Both function and class templates accept default template arguments. The template parameter could have a default argument if all of the parameters to its right also have default arguments.

## Function Template

The function template is a blueprint to generate functions. When invoking a function template, the compiler deduces the type parameters based on the argument of the call and instantiates a specific version of the function. Most compilation errors are detected during instantiation.

```cpp
template <typename T, typename F = std::less<T>>
int compare(const T &v1, const T &v2, F f = F()) {
  if (f(v1, v2))
    return -1;
  if (f(v2, v1))
    return 1;
  return 0;
}

cout << compare(1, 0) << endl;
```

## Class Template

The class template is a blueprint to generate classes. The compiler can't deduce the template parameter types for a class template. Each member function is instantiated if it's used.

C++ assumes that a name accessed through the scope operator is not a type. To inform the compiler that a name depending on a template parameter represents a type, the explicit `typename` keyword is required, such as `typename T::value_type()`.

```cpp
template <typename T> class Blob {
public:
  Blob(std::initializer_list<T> initializer_list);

private:
  std::unique_ptr<T> blob;
};

template <typename T>
Blob<T>::Blob(std::initializer_list<T> il) : blob(std::make_unique<T>(il)) {}

Blob<int> blob;
```

Either a regular class or a template class can have a member function that is itself a template. Member templates can't be virtual. To instantiate a member template of a class template, the arguments for the template parameters for both the class and the function templates are required.

## Instantiation

When two or more separated source files use the same template with the same template arguments, there is an instantiation of that template in each of those files. The overhead can be avoided with an explicit instantiation. When the compiler sees an `extern` template declaration, it will not generate code for that instantiation in that file. Declaring an instantiation as `extern` is a promise that there will be a non-`extern` use of that instantiation elsewhere in the program.

```cpp
extern template class Blob<string>;
extern template int compare(const int&, const int&);
```

## Template Argument Deduction

The compiler uses the arguments in a call to determine the template parameters for a function template. Function parameters whose type uses a template type parameter have special initialization rules, such that limited conversions are applied. Rather than converting the arguments, the compiler generates a new instantiation. Other conversions, such as the arithmetic conversions, derived-to-base, and user-defined conversions, are not performed.

- `const` conversion: Function parameter that is a reference or pointer to an object `const`-qualified type can be passed a reference or pointer to an object of non-`const`-qualified type.
- Array or function conversion: The array argument will be converted to a pointer to its first element. The function argument will be converted to a pointer to the function's type.

If a template type parameter is used as the type of multiple function parameters, the deduced types, after limited conversions, must be identical. The function template can be invoked with explicit template arguments. Normal conversion applies to explicit template arguments.

```cpp
template <typename T1, typename T2, typename T3>
auto sum(T2 a, T3 b) -> T1 {
  return a + b;
}

sum<long long>(1, 2);
sum<long long, long long, long long>(1, 2);
```

### Reference

- When a function parameter is a template type parameter `T`, it could accept either an lvalue or an rvalue, which will be copied or moved, and the `const` qualification will be dropped.
- When a function parameter is an lvalue reference to a template type parameter `T&`, it could accept an lvalue, and the `const` qualification will be preserved.
- When a function parameter is a `const` lvalue reference to a template type parameter `const T&`, it could accept a `const` lvalue or an rvalue.
- When a function parameter is an rvalue reference to a template type parameter `T&&`, it could accept either an lvalue or an rvalue, and the `const` qualification will be preserved.
  - When an lvalue `U` is passed to `T&&`, the compiler deduces the template type parameter `T` as the argument's lvalue reference type `U&`.
  - When an rvalue `U` is passed to `T&&`, the compiler deduces the template type parameter `T` as the argument's  type `U`.
  - The reference collapsing rule states that `X& &`, `X& &&`, and `X&& &` collapse to `X&` and `X&& &&` collapses to `X&&`. Therefore, the function parameter `U& &&` collapses to an lvalue reference `U&`.

```cpp
template <typename T> auto move(T &&t) -> std::remove_reference_t<T &&> {
  return static_cast<std::remove_reference_t<T> &&>(t);
}
```

### Forward

Function parameter is an lvalue expression. Therefore, it will be treated as an lvalue when it's passed to other functions. When used with a function parameter that is an rvalue reference to template type parameter `T&&`, `std::forward<T>` preserves the value categories of an argument's type.

## Overloading

Function templates can be overloaded with function templates or non-template functions. The candidate functions for a call include function-template instantiation for which template argument deduction succeeds. The candidate function templates are sorted with the conversions required to make the call. The candidate with the best match is selected. If there's a tie, non-template functions and specialized function templates are preferred. Otherwise, the call is ambiguous.

## Variadic Template

The variadic template is a template function or class that can take a varying number of parameters. The template parameter pack represents several template parameters and the function parameter pack represents several function parameters.

```cpp
template <typename T>
std::ostream &print(std::ostream &ostream, const T &t) {
  return ostream << t;
}

template <typename T, typename... Args>
std::ostream &print(std::ostream &ostream, const T &t, const Args &...rest) {
  ostream << t << ' ';
  return print(ostream, rest...);
}

print(std::cout, 1, 2, 3, 4, '\n');
```

## Template Specialization

Template specialization is a separate definition of the template in which one or more template parameters are specified to have particular types.

```cpp
template <typename T>
int compare(const T& a, const T& b) {
 return std::less(a, b);
}

template <>
int compare(const int& a, const int& b) {
  return a < b;
}
```

Partial specialization is allowed for class templates. Each partial specialization is itself a template. The specialization's template parameter list includes each template parameter whose type is not fixed.

```cpp
template <typename T> struct remove_reference {
  using type = T;
};

template <typename T> struct remove_reference<T &> {
  using type = T;
};

template <typename T> struct remove_reference<T &&> {
  using type = T;
};
```
