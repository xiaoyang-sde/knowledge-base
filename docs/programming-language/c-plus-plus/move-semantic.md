# Move Semantic

## LValue and RValue

- The lvalue is an expression that refers to an object or variable that occupies a specific memory location, which can be used as the operand of the address-of operator (`&`) to obtain its memory address, and it can also appear on the left-hand side of an assignment operator (`=`) to assign a new value to the object or variable. Functions that return lvalue references, assignment, subscript, dereference, and prefix increment or decrement operators, all yield lvalues.
- The rvalue is an expression that represents a value rather than a memory location, such as literals or temporary objects created when evaluating expressions. Functions that return a non-reference type, arithmetic, relational, bitwise, and postfix increment or decrement operators, all yield rvalues. Either an lvalue reference to `const` or an rvalue reference could be bound to such expressions.

## Reference

- The lvalue reference is a reference that must be bound to an lvalue. However, it is possible to bind a const lvalue reference to an rvalue expression.

- The rvalue reference is a reference that must be bound to an rvalue. Therefore, the resource could be moved from an rvalue reference to another object. The rvalue reference is obtained by using `&&`. The rvalue reference variable is an lvalue.

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
