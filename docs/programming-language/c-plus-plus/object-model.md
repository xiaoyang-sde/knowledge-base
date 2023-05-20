# Object Model

## The Semantic of Constructor

### Default Constructor Construction

The default constructor is a constructor which can be called with no arguments. If no user-declared constructors are provided for the class, the compiler will declare a default constructor as an `inline public` member of its class.

The default constructor for class `T` is trivial if the constructor is not user-provided, `T` has no virtual member functions, `T` has no virtual base classes, `T` has no non-static members with default initializers, all direct bases of `T` has a trivial default constructor, and `T` has no non-static members of `class` type with a non-trivial default constructor. The trivial default constructor performs no action, which is similar to the construction of `struct` in C.

#### Order of Construction

- The virtual base class constructors must be invoked in a left-to-right, depth-first search of the inheritance chain.
  - If the class is listed within the member initialization list, the explicit arguments must be passed.
  - The constructors shouldn't be invoked unless the class object represents the most-derived class.
- The immediate base class constructors must be invoked in the order of base class declaration.
  - If the class is listed within the member initialization list, the explicit arguments must be passed.
  - If a base class is not listed within the member initialization list, the default constructor must be invoked.
  - The a base class is a subsequent base class, the `this` pointer must be adjusted.
- The `vptr` is initialized with the address of the virtual table of the class.
- The member initialization list is expanded in the order of member declaration.
  - If a member class object is not present in the member initialization list but has an associated default constructor, that default constructor must be invoked.
- The user-defined constructor is executed.

#### Member Class Object with Default Constructor

If a class contains a member object of a class with a default constructor, the compiler will augment each constructor with code that invokes the default constructors of the member objects prior to the execution of the user-defined constructor. The language requires that the constructor of member objects be invoked in the order of member declaration within the class.

#### Base Class with Default Constructor

If a class is derived from a base class containing a default constructor, the compiler augments each constructor with the code to invoke all required default constructors of the base classes. If member class objects with default constructors are also present, these default constructors are also invoked after the invocation of all base class constructors.

#### Class with a Virtual Function

If a class either declares or inherits a virtual function or its derived from an inheritance chain in which one or more base classes are virtual, the compiler inserts code to initialize the `vptr` for each object with the address of the appropriate virtual table. The code is added after the invocation of base class constructors but before execution of user-supplied code.

Within each class object, a pointer member `vptr` is synthesized to hold the address of the virtual table. The `vptr` is set after invoking the constructors of immediate base classes, which disables polymorphism for virtual functions invoked in a constructor.

#### Class with a Virtual Base Class

If a class is derived from a virtual base class, the compiler makes the virtual base class location within each derived class object available at runtime. For each constructor the class defines, the compiler inserts code that permits runtime access of each virtual base class.

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

### Destructor

The destructor of class `T` is a special member function that is called when the lifetime of an object ends.

The destructor for class `T` is trivial if the destructor is not user-provided, the destructor is not virtual, the direct base classes of `T` have trivial destructors, and non-static data members of class type have trivial destructors.

#### Order of Destruction

- The `vptr` is reset with the address of the virtual table of the class.
- The user-defined destructor is executed.
- The destructors of the member class objects, if exist, are invoked in the reverse order of their declaration.
- The immediate base class destructors must be invoked in the reverse order of base class declaration.
- The virtual base class destructors must be invoked in the reverse order of their construction. The constructors shouldn't be invoked unless the class object represents the most-derived class.

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

## The Semantic of Data

The C++ object model representation for non-static data members stores the data members with each class object to optimize for space and access time. The rule applies to the inherited non-static data members of both virtual and non-virtual base classes, although the ordering is undefined.

The size of an object is determined with the size of each non-static data member, the overhead of supporting language features (such as virtual function), and alignment requirements of the architecture. Static data members are maintained within the global data segment of the program and do not affect the size of individual objects.

The non-static data members are set down in the order of their declaration within each class object. The compiler might reorder the data members based on their access levels within a class and insert synthesized data members such as the `vptr` pointer.

### Access of Data Member

Non-static data members can't be accessed except through an explicit or implicit class object. The implicit class object `this` is present when the program accesses a non-static data member within a member function.

```cpp
Point3d::translate(const Point3d &pt) {
  x += pt.x;
  y += pt.y;
  z += pt.z;
}

// Internal augmentation of member function
Point3d::translate(Point3d *const this, const Point3d &pt) {
  this->x += pt.x;
  this->y += pt.y;
  this->z += pt.z;
}
```

Access of a nonstatic data member requires the addition of the beginning address of the class object with the offset location of the data member. For example, the address of `&object.member_` is equivalent to `&object + (&Class::member_ - 1)`. The pointer-to-data-member syntax will return a pointer with an offset of `1`, which permits the compiler to distinguish between a pointer to the first data member and a pointer to data member that is addressing no member.

The offset of each non-static data member is known at compile time, even if the member belongs to a base class subobject derived through an inheritance chain. Access of a non-static data member is equivalent in performance to that of a C `struct` member.

Virtual inheritance introduces a level of indirection in the access of its members through a base class subobject. Accessing a non-static data member through a pointer of a virtual base class is expensive, because the type of the object that the pointer points to can't be determined at compile time.

### Inheritance

Under the C++ inheritance model, a derived class object is represented as the concatenation of its members with those of its base classes. The actual ordering of the derived and base class parts is undefined, but the compiler would place the base class members in the front, except in the case of a virtual base class. The padding of the base classes are preserved to enable reinterpreting an object of the derived class as an object of the base clase.

#### Virtual Function

When a class declares virtual functions, the compiler generates a virtual table to hold the address of each virtual function. The table contains virtual functions and slots to support runtime type identification.

- The compiler inserts a `vptr` in the front of each class object to provide the runtime link for an object to find its associated virtual table. When invoking a function through a pointer of the base class, the compiler will use a fixed offset to find the function from the virtual table.
- The compiler augments the constructor to initialize the object's `vptr` to the virtual table of the class.
- The compiler augments the destructor to reset the object's `vptr` to the virtual table of the class.

```cpp
struct A {
  int a_;
  virtual void f_0() {}
  virtual void f_1() {}
};

struct B : public A {
  int b_;
  virtual void f_0() override {}
};
```

- The virtual table of `A` contains `A::f_0()` and `A::f_1()`.
- The virtual table of `B` contains `B::f_0()`, which overrides `A::f_0()`, and `A::f_1()`.

```txt
struct A
 object                                            A VTable
     0 - vptr_A -------------------------------->  +-----------------+
     8 - int a_                                    |offset_to_top (0)|
sizeof(A): 16    align: 8                          +-----------------+
                                                   |    RTTI for A   |
                                                   +-----------------+
                                                   |      A::f0()    |
                                                   +-----------------+
                                                   |      A::f1()    |
                                                   +-----------------+

struct B
 object
     0 - struct A                                  B VTable
     0 -   vptr_A ------------------------------>  +-----------------+
     8 -   int a_                                  |offset_to_top (0)|
    12 - int b_                                    +-----------------+
sizeof(A): 16    align: 8                          |    RTTI for B   |
                                                   +-----------------+
                                                   |      B::f0()    |
                                                   +-----------------+
                                                   |      A::f1()    |
                                                   +-----------------+
```

#### Multiple Inheritance

When a class inherits multiple base classes, there are multiple `vptr` fields to point to different virtual tables, because the base classes doesn't have relationship.

- The `offset_to_top` represents the offset of each base class in the inherited class, which can be used when casting a pointer of the inherited class to a base class.

- The `Thunk` is a small piece of code that adjusts the calling convention of virtual functions. When assigning a derived class to a pointer to the base class, the compiler will adjust the pointer to `base_address + offset_to_top` to support the non-static members of the base class. When calling a virtual function through the pointer, the `Thunk` will adjust the pointer to `base_address` to support the virtual functions of the derived class.

```cpp
struct A {
  int a_;
  virtual void f_0() {}
};

struct B {
  int b_;
  virtual void f_1() {}
};

struct C : public A, public B {
  int c_;
  void f_0() override {}
  void f_1() override {}
};
```

```txt
                                                C Vtable
                                                +--------------------+
struct C                                        | offset_to_top (0)  |
object                                          +--------------------+
    0 - struct A (primary base)                 |     RTTI for C     |
    0 -   vptr_A -----------------------------> +--------------------+
    8 -   int a_                                |       C::f_0()     |
   16 - struct B                                +--------------------+
   16 -   vptr_B ----------------------+        |       C::f_1()     |
   24 -   int b_                       |        +--------------------+
   28 - int c_                         |        | offset_to_top (-16)|
sizeof(C): 32    align: 8              |        +--------------------+
                                       |        |     RTTI for C     |
                                       +------> +--------------------+
                                                |    Thunk C::f_1()  |
                                                +--------------------+
```

#### Virtual Inheritance

When a class is inherited through multiple paths, there might be multiple copies of the base class subobjects in a derived class. When a base class is marked as `virtual` during inheritance, it becomes a virtual base class for all derived classes that inherit from it. Virtual inheritance ensures that there is a unique shared instance of the virtual base class among all the derived classes. Virtual inheritance affects the representation of the derived class.

- The `vbase_offset` represents the offset of the base class subobject in the virtual inherited class. For a pointer to the virtual inherited class, the compiler can't determine the offset of its base class subobject.

- The `vcall_offset` represents the offset that the pointer to the base class should be adjusted when calling a function.

```cpp
struct A {
  int a_;
  virtual void f_0() {}
  virtual void bar() {}
};

struct B : virtual public A {
  int b_;
  virtual void f_0() override {}
};

struct C : virtual public A {
  int c_;
  virtual void f_0() override {}
};

struct D : public B, public C {
  int d_;
  virtual void f_0() override {}
};
```

```txt
                                          B VTable
                                          +---------------------+
                                          |   vbase_offset(16)  |
                                          +---------------------+
                                          |   offset_to_top(0)  |
struct B                                  +---------------------+
object                                    |      RTTI for B     |
    0 - vptr_B -------------------------> +---------------------+
    8 - int b_                            |       B::f_0()      |
   16 - struct A                          +---------------------+
   16 -   vptr_A --------------+          |   vcall_offset(0)   |---------+
   24 -   int a_               |          +---------------------+         |
                               |          |   vcall_offset(-16) |-----+   |
                               |          +---------------------+     |   |
                               |          |  offset_to_top(-16) |     |   |
                               |          +---------------------+     |   |
                               |          |      RTTI for B     |     |   |
                               +--------> +---------------------+     |   |
                                          |     Thunk B::f_0()  |-----+   |
                                          +---------------------+         |
                                          |       A::bar()      |---------+
                                          +---------------------+

                                          D VTable
                                          +---------------------+
                                          |   vbase_offset(32)  |
                                          +---------------------+
struct D                                  |   offset_to_top(0)  |
object                                    +---------------------+
    0 - struct B (primary base)           |      RTTI for D     |
    0 -   vptr_B  ----------------------> +---------------------+
    8 -   int b_                          |       D::f_0()      |
   16 - struct C                          +---------------------+
   16 -   vptr_C  ------------------+     |   vbase_offset(16)  |
   24 -   int c_                    |     +---------------------+
   28 - int d_                      |     |  offset_to_top(-16) |
   32 - struct A (virtual base)     |     +---------------------+
   32 -   vptr_A --------------+    |     |      RTTI for D     |
   40 -   int a_               |    +---> +---------------------+
sizeof(D): 48    align: 8      |          |       D::f_0()      |
                               |          +---------------------+
                               |          |   vcall_offset(0)   |---------+
                               |          +---------------------+         |
                               |          |   vcall_offset(-32) |-----+   |
                               |          +---------------------+     |   |
                               |          |  offset_to_top(-32) |     |   |
                               |          +---------------------+     |   |
                               |          |      RTTI for D     |     |   |
                               +--------> +---------------------+     |   |
                                          |    Thunk D::f_0()   |-----+   |
                                          +---------------------+         |
                                          |       A::bar()      |---------+
                                          +---------------------+
```

## The Semantic of Function

### Non-static Member Function

C++ design criterion states that a non-static member function must be as efficient as a non-member function. The member function is transformed to be equivalent to the non-member instance.

- The compiler inserts an implicit argument `T* this` to the member function that provides access to the invoking class object. If the member function is `const`, the implicit `this` pointer is `T* const this`.

- The compiler rewrites each direct access of a non-static data member of the class to access through the `this` pointer.

- The member function is moved into an external function with a mangled name.

### Virtual Member Function

The compiler transforms an invocation of a virtual member function through a reference or pointer to an object of class `T` from `object->virtual_function()` to `(*object->vptr[offset])(object)`, where `vptr` is a pointer to the virtual table of class `T` and `offset` is the function's assigned slot in the virtual table. The compiler will treat an invocation of a virtual member function through a concrete object as the same as an invocation of a non-static member function, because the concrete object doesn't support polymorphism.

Each virtual function is assigned a fixed index in the virtual table. The index remains associated with the particular virtual function throughout the inheritance chain. Therefore, each virtual function in the base class and the overridden function in the derived class share the same offset in their virtual tables.
