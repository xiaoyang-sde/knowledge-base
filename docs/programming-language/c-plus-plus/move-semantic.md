# Move Semantic

## Value Categories

Each C++ expression, such as an operator with its operands, a literal, or a variable name, is characterized with type and value categories. Each expression has some non-reference type, and each expression belongs to one of the three value categories: prvalue, xvalue, and lvalue.

- glvalue: glvalue ("generalized" lvalue) is an expression whose evaluation determines the identity of an object or function.

- prvalue: prvalue ("pure" rvalue) is an expression whose evaluation computes the value of an operand of a built-in operator or initializes an object.

- xvalue: xvalue (an "expiring" value) is a glvalue that denotes an object whose resources can be reused.

- lvalue: lvalue is a glvalue that is not an xvalue.

- rvalue: rvalue is a prvalue or an xvalue.

### lvalue

The lvalue is an expression that refers to an object or variable that occupies a specific memory location, which can be used as the operand of the address-of operator (`&`) to obtain its address or appear on the left-hand side of an assignment operator (`=`) to assign a new value to the object or variable. The lvalue could initialize an lvalue reference.

Although an expression consisting of the name of a variable is an lvalue expression, such expression might be move-eligible if it appears as the operand of a `return` statement, a `co_return` statement, or a `throw` expression. If an expression is move-eligible, it is treated as an rvalue for the purpose of overload resolution, thus it might select the move constructor.

- The name of a variable, a function, a template parameter object, or a data member (even if the variable's type is rvalue reference, the expression consisting of its name is an lvalue expression)

- The function call or the overloaded operator expression, whose return type is lvalue reference, such as `++it` or `str1 = str2`

- The built-in assignment and compound assignment expression, such as `a = b` or `a += b`

- The pre-increment and pre-decrement expression, such as `++a`

- The built-in indirection expression, such as `*p`

- The built-in subscript expression, such as `a[n]`, where one operand in `a[n]` is an lvalue

- The member of object expression, such as `a.m`, except where `m` is a member enumerator or a non-static member function, or where `a` is an rvalue and `m` is a non-static data member of object type

- The member of pointer expression, such as `p->m`, except where `m` is a member enumerator or a non-static member function

- The string literal, such as `"Hello, world!"`

- The cast expression to lvalue reference type, such as `static_cast<int&>(x)`

- The function call or an overloaded operator expression, whose return type is rvalue reference to function

- The cast expression to rvalue reference to function type, such as `static_cast<void(&&)(int)>(x)`

### prvalue

The rvalue is an expression that represents a value rather than a location, such as literals or temporary objects created when evaluating expressions. The rvalue could initialize an rvalue reference, in which case the lifetime of the object is extended until the scope of the reference ends.

- The literal, such as `42` or `nullptr`

- The function call or an overloaded operator expression, whose return type is non-reference, such as `str.substr(1, 2)` or `it++`

- The post-increment and post-decrement expression, such as `a++`

- The built-in arithmetic, logical, and comparison expression

- The built-in address-of expression, such as `&a`

- The member of object expression, such as `a.m`, where `m` is a member enumerator or a non-static member function

- The member of pointer expression, such as `p->m`, where `m` is a member enumerator or a non-static member function

- The cast expression to non-reference type, such as `static_cast<double>(x)`

- The `this` pointer

- The lambda expression

### xvalue

- The member of object expression, such as `a.m`, where `a` is an rvalue and `m` is a non-static data

- The function call or an overloaded operator expression, whose return type is rvalue reference to object, such as `std::move(x)`

- The cast expression to rvalue reference to object type, such as `static_cast<char&&>(x)`

## Class Member Function

Each class member function could define a version that accepts a `const` lvalue reference (`const T&`) and an rvalue reference (`T&&`). The `const` lvalue version copies the object and the rvalue version moves the object.

To ensure that the left-hand operand (object that `this` points to) is an lvalue or rvalue, a reference qualifier (`&` or `&&`) could be placed after the parameter list. If a member function has a reference qualifier, all the versions of that member with the same parameter list must have reference qualifiers.

## `std::move` and `std::forward`

Although an rvalue reference can't be bound to an lvalue, the `move` function could cast an lvalue to its corresponding rvalue reference type. The call to `move` promises that the moved object should not be used again except to assign to it.

```cpp
int lvalue = 1;
int&& rvalue_reference = move(lvalue);
```

Since an rvalue reference is an lvalue, it will be passed to another function. The `std::forward<T>` function converts an rvalue reference to an rvalue, and leaves an lvalue reference as is.

```cpp
template<typename T>
void g(T&& x) {}

template<typename T>
void f(T&& x) {
  g(std::forward<T>(x));
}
```
