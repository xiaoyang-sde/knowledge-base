# Class

## Introduction

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

## Constructor

Each class defines how objects of its type can be initialized. The class controls object initialization by defining one or more special member functions known as constructors. The constructor has the same name as the class and doesn't have a return type.

The synthesized default constructor takes no arguments and default initialize all members unless there's an in-class initializer. The compiler won't generate the default constructor if there's other constructors.

```cpp
struct sales_data {
  sales_data() = default; // explicit default initializer
  sales_data(string& book_no): bookNo(s) {};
};
```

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

## Class Scope

Outside the class scope, data and function members could be accessed through an object, a reference, or a pointer using a member access operator `->`. To define a member function outside the class, the class name is required, such as `sales_data::clear()`.

Member function definitions are processed after the compiler processes all of the declarations in the class, which makes it easier to organize class code. However, the name used in class declaration, including the name used for the return type and type in the parameter list, must be seen before it's used.

## `static` Class Member

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
