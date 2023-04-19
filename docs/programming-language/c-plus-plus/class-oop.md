# Class and Object-Oriented Programming

## Class

### Member Function

The member function is similar to a regular function. The function should be declared in the class, but its content could be either inside or outside the class. The member function could access the object through an implicit parameter named `this`, which means that `this` could be ignored. The type of `this` is a `const` pointer to the non-`const` version of the class type.

```cpp
struct sales_data {
  string book_no;
  string get_book_no() const {
    return book_no; // equivalent to `return this->book_no;`
  };
};
```

### `const` Member Function

The `const` follows the parameter list of a member function modifies the type of the implicit `this` to a `const` pointer to the `const` version of the class type. Therefore, it allows `this` to bind to a `const` object. The `const` member function can't write to the object.

### Type Member

The class can define its own local names for types. Type names are subject to the same access controls as any other member and may be either public or private.

```cpp
class Screen {
public:
  typedef string::size_type index;
  // equivalent to `using index = string::size_type;`
};
```

### `mutable` Data Member

If a data member is declared as `mutable`, then a `const` member function could change its value.

```cpp
class sales_data {
  mutable int book_no;
  void increment_book_no() const {
    book_no += 1;
  };
};
```

### `static` Class Member

The `static` class member is associated with the class itself instead of the object. `static` member could be `private` or `public`. The `static` member function doesn't have access to `this` pointer, which means that it can't access non-`static` members. The `static` member could be accessed through the scope operator `::`, object, reference, or pointer of the class type.

The `static` member can't be initialized in the constructor. However, `static` member with integral type could accept a constant expression as an initializer.

```cpp
// declaration
struct Foo;

struct S {
  static int n;
  static int a[];
  static Foo x;
  static S s;
};

// definition
int S::n = 1;
int S::a[10];
struct Foo {};
Foo S::x;
S S::s;
```

### Return `*this`

To return the object as an lvalue, the member function could return `*this` as a reference to the object itself. It allows the program to chain multiple operations on the same object.

If the member function is `const`, then it returns a `const` reference to the object, which prevents further operations even if the object is not defined as `const`. Therefore, it's better to overload the member function with a non-`const` clone.

```cpp
class sales_data {
  const &sales_data print() const {
    cout << "test" << endl;
    return *this;
  };

  &sales_data print() {
    cout << "test" << endl;
    return *this;
  };
}
```

## Access Control and Encapsulation

C++ has access specifiers to enforce encapsulation. The difference between `class` and `struct` is the access level. For the `struct` keyword, the members defined before the first access specifier are public. For the `class` keyword, the members are private.

- Members defined after a `public` specifier are accessible to all parts of the program. The public members define the interface to the class.
- Members defined after a `private` specifier are accessible to the member functions of the class but are not accessible to code that uses the class. The private sections encapsulate the implementation.

```cpp
class sales_data {
public:
  sales_data() = default;
private:
};
```

### Friend

The class can allow another function, class, or class member function to access its non-public members by making that class or function a friend. Friendship is not transitive.

```cpp
class sales_data {
friend class sales_manager; // friend class
friend class sales_manager::report(); // friend member function
friend sales_data add(const sales_data&, const sales_data&);
friend istream &read(istream&, sales_data&);
};
```

### Class Scope

Outside the class scope, data and function members could be accessed through an object, a reference, or a pointer using a member access operator `->`. To define a member function outside the class, the class name is required, such as `sales_data::clear()`.

Member function definitions are processed after the compiler processes all of the declarations in the class, which makes it easier to organize class code. However, the name used in class declaration, including the name used for the return type and type in the parameter list, must be seen before it's used.

## Constructor

Each class defines how objects of its type can be initialized. The class controls object initialization by defining one or more special member functions known as constructors. The constructor has the same name as the class and doesn't have a return type.

The synthesized default constructor takes no arguments and default initialize all members unless there's an in-class initializer. The compiler won't generate the default constructor if there's other constructors.

```cpp
struct sales_data {
  sales_data() = default; // explicit default initializer
  sales_data(string& book_no): bookNo(s) {};
};
```

Each class provides copy constructor, copy-assignment operator, move constructor, move-assignment operator, and destructor to control how an object of the class type is copied, moved, assigned, and destroyed.

- The copy and move constructors define what happens when an object is initialized from another object of the same type.
- The copy- and move-assignment operators define what happens when an object of a class type is assigned to another object of that same class type.
- The destructor defines what happens when an object of the type ceases to exist.

```cpp
class Foo {
public:
  Foo(); // default constructor
  Foo(const Foo&); // copy constructor
  Foo(Foo&&); // move constructor
  Foo& operator=(const Foo&); // copy assignment operator
  Foo& operator=(Foo&&); // move assignment operator
  ~Foo(); // destructor
};
```

To prevent a constructor or an operator, it could be defined as `delete`, which declares a function and prevents its usage. However, if the destructor is deleted, objects of that type can't be destructed, which is an error.

### Constructor Initializer List

The  constructor initializer list specifies initial values for one or more data members of the object being created. When a member is omitted from the constructor initializer list, it is initialized using the same process as the synthesized default constructor.

When a field is initialized with the initializer list, the constructor will be called once and the object will be constructed and initialized in one operation. However, for a field that is assigned in the constructor, the field will be first default initialized and then reassigned.

Members that are `const` or references must be initialized. Classes without the default constructor must be initialized. Members are initialized in the order of the class definition.

### Delegating Constructor

The delegating constructor uses another constructor from its own class to perform its initialization. In a delegating constructor, the member initializer list has a single item that is the name of the class itself.

```cpp
class Sales_data {
public:
  Sales_data(std::string s, unsigned cnt, double price): bookNo(s), units_sold(cnt), revenue(cnt*price) {}
  Sales_data(): Sales_data("", 0, 0) {}
};
```

### Conversion Constructor

The conversion constructor is a constructor with a single argument, which is an implicit conversion from the argument type to the class type. The compiler supports implicit one-step conversion. For example, converting string literal to `string`, and then to class type is a two-step converesion. The `explicit` keyword before the constructor declaration could disable the implicit conversion.

```cpp
sales_data book_1 = "test"; // implicit
sales_data book_1("test"); // explicit
sales_data book_1 = sales_data("test"); // explicit (equivalent to `static_cast`)
```

### Copy Constructor

The copy constructor's first parameter is a reference to the class type. The synthesized copy constructor copies the members of its argument into the object being created. Members of class type are copied by the copy constructor for that class. Members of built-in type are copied directly.

```cpp
Foo foo_1;
Foo foo_2 = foo_1; // copy initialization
Foo foo_3(foo_1); // copy initialization
```

Copy initialization uses the copy constructor. Copy initialization also happens when passing an object as a parameter of non-reference type, returning an object from a function that has a non-reference return type, or brace initializing the elements of a container class.

### Copy-Assignment Operator

The synthesized copy-assignment operator assigns each non-static member of the right-hand object to the corresponding member of the left-hand object using the copy-assignment operator for the type of that member.

### Destructor

The destructor frees the resources of an object and destructs the non-static data members of the object. Members of class type are destroyed by running the member's own destructor.

The synthesized destructor has an empty function body, since the class members are destroyed as part of the implicit destruction phase that follows the destructor.

### Move Constructor and Move-Assignment Operator

The move constructor and move-assignment operator could take over the allocated objects of the moved-from object. The move constructor and operator must ensure that the moved-from object is left in a state such that destroying that object will be harmless, since after an object is moved from, that object continues to exist. Move constructors and move-assignment operators that won't throw exceptions should be marked as `noexcept`, which informs the compiler that the constructor is safe to use.

The compiler will synthesize a move constructor or a move-assignment operator if the class doesn't define copy-control members and if all non-static data member of the class can be moved. The compiler can move members of built-in type and members of class type if the member's class has the corresponding move operation. If the class doesn't have a move constructor or operator, both lvalues and rvalues will be copied.

## Inheritance

The base class defines the members that are common to the derived classes. The base class defines `virtual` functions, which it expects its derived classes to define for themselves. Each derived class specifies its base classes in the class derivation list and defines the members that are specific to the derived class itself. The derived class must override (with the `override` keyword) the `virtual` functions in the base class.
When a virtual function is called on a reference to the base class, the decision of which derived class to run depends on the type of the argument.

```cpp
class Quote {
  public:
    string isbn() const;
    virtual double net_price(size_t n) const;
};

class Bulk_quote : public Quote {
  double net_price(std::size_t) const override;
};
```

The derived class inherits the members defined in its base class. The `protected` access specifier in the base class allows derived classes to access certain members while prohibiting public access.

The derived object contains members that it inherits from its base, and it must uses the base class's constructor to initialize these members. The derived-class constructor uses its constructor initializer list to pass arguments to a base-class constructor. Otherwise, the base class will be default initialized.

The reference or pointer to a base-class type could be bound to an object of the derived class. The base can't be converted to derived at compile time even when a base pointer or reference is bound to a derived object. However,`static_cast` could override the compiler and `dynamic_cast` could perform the conversion at runtime. Furthermore, if an object of the derived class is assigned to an object of the base class, the derived part of the object is ignored.

### Access Control and Inheritance

The class uses `protected` for those members that it is willing to share with its derived classes but wants to protect from general access. The derived class member or friend could access the protected members of the base class through a derived object. The derived class has no special access to the protected members of base-class objects.

- Public inheritance makes public members of the base class public in the derived class, and the protected members of the base class remain protected in the derived class.
- Protected inheritance makes the public and protected members of the base class protected in the derived class.
- Private inheritance makes the public and protected members of the base class private in the derived class.

The derived-to-base conversion is the operation that assigns a derived class to a a base-class reference or pointer. If a public member of the base class is accessible, then the derived-to-base conversion is accessible, and not otherwise.

### Class Scope and Inheritance

Each class defines its own scope within which its members are defined. Under inheritance, the scope of a derived class is nested inside the scope of its base classes. If a name is unresolved within the scope of the derived class, the enclosing base-class scopes are searched for a definition of the name.

The static type of an object, reference, or pointer determines which members of that object are visible. The static type determines what members can be used. The name in the inner scope could override the name in the outer scope. However, the member function in the inner scope could not overload the function in the outer scope.

## Virtual Function

Dynamic binding happens when a virtual member function is called through a reference or a pointer to a base-class type. The compiler generates code to decide at run time which function to call. The function that is called corresponds to the dynamic type of the object.

## Abstract Base Class

The virtual function is pure if its declaration ends with `= 0`. The pure virtual function does not have to be defined. The class with a pure virtual function is an abstract base class, which defines an interface for subsequent classes to override. Create an object of an abstract base class is an error.

The base class should define a virtual destructor to allow objects of the derived classes to be dynamically allocated. The destructor is called when a pointer to the dynamically allocated object is deleted. If that pointer points to an object of a derived class, it is possible that the static type of the pointer might differ from the dynamic type of the object being destroyed.
