# Object Model

## The Semantic of Constructor

### Default Constructor Construction

The default constructor is a constructor which can be called with no arguments. If no user-declared constructors are provided for the class, the compiler will declare a default constructor as an `inline public` member of its class.

The default constructor for class `T` is trivial if the constructor is not user-provided, `T` has no virtual member functions, `T` has no virtual base classes, `T` has no non-static members with default initializers, all direct bases of `T` has a trivial default constructor, and `T` has no non-static members of `class` type with a non-trivial default constructor. The trivial default constructor performs no action, which is similar to the construction of `struct` in C.

To prevent synthesizing multiple default constructors for different translation units, the compiler defines the synthesized default constructor as `inline`. If the function is too complex to be inlined, an explicit non-inline static instance is synthesized.

There are multiple scenarios when the compiler will synthesize a default constructor, which are discussed in the following sections.

#### Member Class Object with Default Constructor

If a class contains a member object of a class with a default constructor, the compiler will augment each constructor with code that invokes the default constructors of the member objects prior to the execution of the user-defined constructor. The language requires that the constructor of member objects be invoked in the order of member declaration within the class.

If the class doesn't have a constructor, the compiler needs to synthesize a default constructor for the class. The synthesis takes place if the constructor needs to be invoked.

#### Base Class with Default Constructor

If a class is derived from a base class containing a default constructor, the compiler augments each constructor with the code to invoke all required default constructors of the base classes. If member class objects with default constructors are also present, these default constructors are also invoked after the invocation of all base class constructors.

If the class doesn't have a constructor, the compiler needs to synthesize a default constructor for the class. To a derived class, the synthesized constructor appears no different than that of an user-defined default constructor.

#### Class with a Virtual Function

If a class either declares or inherits a virtual function or its derived from an inheritance chain in which one or more base classes are virtual, the compiler inserts code to initialize the `vptr` for each object with the address of the appropriate virtual table, which contains the addresses of the active virtual functions for that class. Within each class object, an additional pointer member `vptr` is synthesized to hold the address of the associated class `vtbl`.

If the class doesn't have a constructor, the compiler needs to synthesize a default constructor for the class.

#### Class with a Virtual Base Class

If a class is derived from a virtual base class, the compiler makes the virtual base class location within each derived class object available at runtime. For each constructor the class defines, the compiler inserts code that permits runtime access of each virtual base class.

If the class doesn't have a constructor, the compiler needs to synthesize a default constructor for the class.

### Copy Constructor Construction

The copy constructor of class `T` is a non-template constructor whose first parameter is `T&‍`, `const T&‍`, `volatile T&‍`, or `const volatile T&‍`, and either there are no other parameters, or the rest of the parameters all have default values.

The copy constructor is called whenever an object is initialized from another object of the same type, which includes:

- initialization: `T a = b` or `T a(b)`
- function argument passing: `f(a)`
- function return: `return a` inside a function such as `T f()`, where `a` is of type `T`, which has no move constructor

#### Default Memberwise Initialization

If the class doesn't provide an explicit copy constructor, each class object will be initialized with another object with default memberwise initialization. Default memberwise initialization copies the value of each built-in or derived data member. The memberwise initialization is applied to the member class objects.

The compiler will synthesize a non-trivial copy constructor if the class doesn't exhibit the bitwise copy semantic.

- When the class contains a member object of a class for which a copy constructor exists (either user-declared or synthesized), the compiler needs to insert invocations of the member copy constructors inside the synthesized copy constructor.

- When the class is derived from a base class for which a copy constructor exists (either user-declared or synthesized), the compiler needs to insert invocations of the base copy constructors inside the synthesized copy constructor.

- When the class declares one or more virtual functions, each object contains a `vptr` that points to the `vtable` of its class. The compiler needs to ensure that the correct `vptr` is used if object slicing occurs, such as an object of a base class is initialized with an object of a derived class.

- When the class is derived from an inheritance chain in which one or more base classes are virtual, each object contains the information about the location of the object of its virtual base class. The compiler needs to initialize the virtual base class pointer or offsest if object slicing occurs, such as an object of a base class is initialized with an object of a derived class.

### Program Transformation Semantic

- Given the definition `X x_0` and `X x1 = x0`, the compiler will transform the program such that each definition is rewritten with the initialization stripped out and an invocation of the class copy constructor is inserted.

- Passing a class object as an argument to a function is equivalent to `X formal = actual`, where `formal` is the formal argument or return value and `actual` is the actual argument. The compiler could construct the object in the location where the callee expects the actual parameter.

- Returning a value in a function is equivalent to creating a copy of the returned object using the copy constructor.

#### Copy Elision

Copy elision rules enable the compiler to omit copy and move constructors, resulting in zero-copy pass-by-value semantics.The object will be constructed into the storage where it would otherwise be copied/moved to. The copy or move constructors need not be present or accessible. Multiple copy elisions may be chained to eliminate multiple copies.

- Return value optimization (RVO): In a return statement, when the operand is a prvalue of the same class type (ignoring cv-qualification) as the function return type, the copy is omitted.

```cpp
T f() {
  return T();
}
```

- In the initialization of an object, when the initializer expression is a prvalue of the same class type (ignoring cv-qualification) as the variable type, the copy is omitted.

```cpp
T x = T(T(f()));
```

- Named return value optimization (NRVO): In a return statement, when the operand is the name of a non-volatile object with automatic storage duration, which isn't a function parameter or a catch clause parameter, and which is of the same class type (ignoring cv-qualification) as the function return type, the copy might be omitted.

### Member Initialization List

Member initialization list in the constructor is required when initializing a `const` or reference member (in-class initialization) or invoking a base or member class constructor with a set of arguments. The member initialization list could avoid the need for default initialization of non-trivial objects.

The compiler iterates over the initialization list, inserting the initializations in the proper order within the constructor prior to explicit user code. The order in which the list entries are initialized follows the declaration order of the members within the class declaration, not the order within the initialization list.
