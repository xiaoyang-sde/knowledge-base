# Constructor and Operator

## Copy

The class provides copy constructor, copy-assignment operator, move constructor, move-assignment operator, and destructor to control how an object of the class type is copied, moved, assigned, and destroyed.

- The copy and move constructors define what happens when an object is initialized from another object of the same type.
- The copy- and move-assignment operators define what happens when an object of a class type is assigned to another object of that same class type.
- The destructor defines what happens when an object of the type ceases to exist.

```cpp
class Foo {
public:
  Foo(); // default constructor
  Foo(const Foo&); // copy constructor
  Foo& operator=(const Foo&); // copy assignment operator
  ~Foo(); // destructor
};
```

To prevent a constructor or an operator, it could be defined as `delete`, which declares a function and prevents its usage. However, if the destructor is deleted, objects of that type can't be destructed, which is an error.

### The Copy Constructor

The copy constructor's first parameter is a reference to the class type. The synthesized copy constructor copies the members of its argument into the object being created. Members of class type are copied by the copy constructor for that class. Members of built-in type are copied directly.

```cpp
Foo foo_1;
Foo foo_2 = foo_1; // copy initialization
Foo foo_3(foo_1); // copy initialization
```

Copy initialization uses the copy constructor. Copy initialization also happens when passing an object as a parameter of non-reference type, returning an object from a function that has a non-reference return type, or brace initializing the elements of a container class.

### The Copy-Assignment Operator

The synthesized copy-assignment operator assigns each non-static member of the right-hand object to the corresponding member of the left-hand object using the copy-assignment operator for the type of that member.

### The Destructor

The destructor frees the resources of an object and destructs the non-static data members of the object. Members of class type are destroyed by running the member's own destructor.

The synthesized destructor has an empty function body, since the class members are destroyed as part of the implicit destruction phase that follows the destructor.

## Move

Copies are made in a few cases. In some of cases, an object is immediately destroyed after it is copied. In those cases, moving, rather than copying, the object can provide a significant performance boost. Some object (such as `unique_ptr`) have a resource that can't be shared.

```cpp
class Foo {
public:
  Foo(); // default constructor
  Foo(Foo&&); // move constructor
  Foo& operator=(Foo&&); // move assignment operator
  ~Foo(); // destructor
};
```

### RValue Reference

The rvalue reference is a reference that must be bound to an rvalue, which is an object that is about to be destroyed, such as literals or temporary objects created when evaluating expressions. Therefore, the resource could be moved from an rvalue reference to another object. The rvalue reference is obtained by using `&&`. The rvalue reference variable is an lvalue.

- Functions that return lvalue references, assignment, subscript, dereference, and prefix increment or decrement operators, all yield lvalues.
- Functions that return a non-reference type, arithmetic, relational, bitwise, and postfix increment or decrement operators, all yield rvalues. Either an lvalue reference to `const` or an rvalue reference could be bound to such expressions.

### `std::move` and `std::forward`

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

### Move Constructor and Move-Assignment Operator

The move constructor and move-assignment operator could take over the allocated objects of the moved-from object. The move constructor and operator must ensure that the moved-from object is left in a state such that destroying that object will be harmless, since after an object is moved from, that object continues to exist. Move constructors and move-assignment operators that won't throw exceptions should be marked as `noexcept`, which informs the compiler that the constructor is safe to use.

The compiler will synthesize a move constructor or a move-assignment operator if the class doesn't define copy-control members and if all non-static data member of the class can be moved. The compiler can move members of built-in type and members of class type if the member's class has the corresponding move operation. If the class doesn't have a move constructor or operator, both lvalues and rvalues will be copied.

### Member Function

Each member function could define a version that accepts a `const` lvalue reference (`const T&`) and an rvalue reference (`T&&`). The `const` lvalue version copies the object and the rvalue version moves the object.

To ensure that the left-hand operand (object that `this` points to) is an lvalue or rvalue, a reference qualifier (`&` or `&&`) could be placed after the parameter list. If a member function has a reference qualifier, all the versions of that member with the same parameter list must have reference qualifiers.
