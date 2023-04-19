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

The class template is a blueprint to generate classes. The compiler can't deduce the template parameter types for a class template. Each member function is instantiated if it's used. To inform the compiler that a name depending on a template parameter represents a type, the explicit `typename` keyword is required.

```cpp
template <typename T>
class Blob {
public:
  Blob(std::initializer_list<T> il);

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
template int compare(const int&, const int&);
```
