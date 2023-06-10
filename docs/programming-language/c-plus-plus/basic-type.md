# Variable and Basic Type

## Basic Type

### Arithmetic Type

- `bool`: implementation-defined
- `char`: 1 byte
- `wchar_t`: 2 bytes
- `char16_t`: 2 bytes
- `char32_t`: 4 bytes
- `short`: 2 bytes
- `int`: 2 bytes
- `long`: 4 bytes
- `long long`: 8 bytes
- `float`: 6 significant digits
- `double`: 10 significant digits
- `long double`: 10 significant digits

Except for `bool` and the extended character types, the integral types are either signed or unsigned, such as `unsigned int`. While `unsigned int` could hold `0` to `255`, `signed int` could hold `-128` to `127`.

### Type Conversion

- Assign non-`bool` arithmetic type to a `bool` type: `false` if the value is `0`
- Assign `bool` to other arithmetic types: `1` if the `bool` is `true` and `0` if the `bool` is `false`
- Assign a floating-point value to an object of integral type: the value after the decimal point is truncated
- Assign an integral value to an object of floating-point type: the fractional part is `0`
- Assign an out-of-range value to an object to unsigned type: the remainder of the value modulo the number of values the target type can hold
- Assign an out-of-range value to an object to signed type: undefined behavior

### Literal

Self-evident value, such as `42` is a literal. The form and value of a literal determine its type.

- Integral literals that begin with `0` are interpreted as octal, and those that begin with `0x` are interpreted as hexadecimal. Decimal literal has the smallest type of `int`, `long`, or `long long` in which the literal's value fits.
- String literals are zero or more characters enclosed in double quotes, which have the type of array of constant `char`s that ends with `\0`.

Specific prefix or suffix could override the default type of an integer, floating-point, or character literal. For example, `1LL` has the type of `long long`.

- Prefix
  - `u`: `char16_t`
  - `U`: `char32_t`
  - `L`: `wchar_t`
  - `u8`: `char`
- Suffix
  - `U`: `unsigned`
  - `L`: `long`
  - `LL`: `long long`
  - `F`: `float`
  - `L`: `long double`

## Variable

The simple variable definition consists of a type specifier with a list of one or more variable names separated with commas, with optional initial values. For example, `int sum = 0;`.

The variable that is initialized gets the specified value at the moment it's created. **Initialization is not assignment.** The variable without an initializer is default initialized. Built-in variables defined outside functions are initialized to zero, and those defined inside functions are uninitialized with undefined value. Classes could provide default initializers.

The scope is the part of the program in which a name has a particular meaning. The same name could refer to different entities in different scopes. Names defined outside blocks have global scope. Names defined inside blocks have block scope.

## Compound Type

### Reference

The reference defines an alternative name for an object. For example, `int &ref_val = val` defines an reference for `val`. All operations on the reference are operations on the object to which the reference is bound.

### Pointer

The pointer is a compound type that points to another type. Pointers are used for indirect access to other objects. Different from reference, a pointer is an object, which can be undefined, assigned, and copied. A single pointer can point to several objects over its lifetime.

Each pointer holds an address of another object, which is retrieved with the address-of operator `&`: `int *p = &val`. The dereference operator `*` could be used to access the object.

The null pointer has the value of `nullptr` or `0`, which doesn't point to an object. It's illegal to assign an integer to a pointer. The `void*` type is a special pointer type that can hold the address of all objects. The type of the object at that address is unknown.

## `const` Qualifier

The `const` variable can't be assigned after initialization, such as `const int buf_size = 512`. If the value of the constant variable is a compile-time constant, the compiler will replace uses of the variable with its value.

Constant variables are defined as local to the file. To share a constant variable across multiple files, the `extern` keyword could be used on both its definition and declaractions.

### Constant Reference and Pointer

- Reference to `const` is a reference that refers to a `const` type. The reference can't be used to change the object to which the reference is bound. For example, `const int &r1 =  ci` is a `const` reference to `ci`. `const` reference can refer to an object that is not `const`.
- Pointer to `const` is a pointer that points to a `const` type. The pointer can't be used to change the object to which the pointer points. `const` pointer can point to an object that is not `const`.

```cpp
double pi = 3.14;
double *regular_pointer = &pi; // regular pointer
const double *const_ptr = &pi; // pointer to const type (the pointer itself could be modified)
double *const const_ptr = &pi; // const pointer (the object it points to could be modified)
const double *const const_ptr = &pi; // const pointer to const type
```

- Top-level const indicates that the object itself is a `const`. Top-level const is ignored when the object is copied.
- Low-level const indicates that the base type of the compound types (pointers or references) is a `const`.

### Constant Expression

The constant expression is an expression whose value can't change and that can be evaluated at compile time, such as a literal or a `const` object that is initialized from a constant expression. The `constexpr` declaration let the compiler check if the variable is a constant expression.

```cpp
const int soft_limit = 20; // implicit constant expression
constexpr hard_limit = soft_limit * 2; // explicit constant expression
```

## Type Deduction

### `auto`

The compiler could deduce the type of variables defined with the `auto` type specifier from their initializers. `auto` follows the same rule as the template argument deduction.
If the placeholder type specifier is `decltype(auto)`, the deduced type is `decltype(expr)`, where `expr` is the initializer.

For example, given `const auto& i = expr`, the type of `i` is the type of the argument `u` in a template `template<class U> void f(const U& u)` if the function call `f(expr)` was compiled. Therefore, `auto&&` might be deduced either as an lvalue reference or rvalue reference according to the initializer.

```cpp
double val_1 = 1.0;
double val_2 = 2.0;
auto val_3 = val_1 + val_2; // val_3 is a double
const auto const_val_3 = val_1 + val_2; // const_val_3 is a const double
```

### `decltype`

The compiler could deduce the type of variables defined with the `decltype` type specifier from the parameter of `decltype`. If the parameter is an expression instead of a variable, it returns the type that the expression returns. `decltype` returns a reference type for an expression that returns an lvalue, such as `*p`. `decltype((variable))` is guaranteed to return a reference type because `(variable)` is an expression that returns an lvalue.

`decltype(auto)` deduces a type from its initializer, but it performs the type deduction using the `decltype` rules, instead of the `auto` rules.

```cpp
decltype(1) val_1 = 0; // val_1 is an integer
decltype(1 + 1) val_2 = 0; // val_2 is an integer
decltype(max()) val_3 = 0; // val_3 is the return type of max
decltype(val_3) val_4 = 0; // val_4 has the same type as val_3
```
