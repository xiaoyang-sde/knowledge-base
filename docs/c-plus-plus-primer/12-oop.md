# Object-Oriented Programming

## Overview

### Inheritance

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

## Virtual Function

Dynamic binding happens when a virtual member function is called through a reference or a pointer to a base-class type. The compiler generates code to decide at run time which function to call. The function that is called corresponds to the dynamic type of the object.

## Abstract Base Class

The virtual function is pure if its declaration ends with `= 0`. The pure virtual function does not have to be defined. The class with a pure virtual function is an abstract base class, which defines an interface for subsequent classes to override. Create an object of an abstract base class is an error.

## Access Control and Inheritance

The class uses `protected` for those members that it is willing to share with its derived classes but wants to protect from general access. The derived class member or friend could access the protected members of the base class through a derived object. The derived class has no special access to the protected members of base-class objects.

- Public inheritance makes public members of the base class public in the derived class, and the protected members of the base class remain protected in the derived class.
- Protected inheritance makes the public and protected members of the base class protected in the derived class.
- Private inheritance makes the public and protected members of the base class private in the derived class.

The derived-to-base conversion is the operation that assigns a derived class to a a base-class reference or pointer. If a public member of the base class is accessible, then the derived-to-base conversion is accessible, and not otherwise.

## Class Scope

Each class defines its own scope within which its members are defined. Under inheritance, the scope of a derived class is nested inside the scope of its base classes. If a name is unresolved within the scope of the derived class, the enclosing base-class scopes are searched for a definition of the name.

The static type of an object, reference, or pointer determines which members of that object are visible. The static type determines what members can be used. The name in the inner scope could override the name in the outer scope. However, the member function in the inner scope could not overload the function in the outer scope.

## Constructor and Operator

### Virtual Destructor

The base class should define a virtual destructor to allow objects of the derived classes to be dynamically allocated. The destructor is called when a pointer to the dynamically allocated object is deleted. If that pointer points to an object of a derived class, it is possible that the static type of the pointer might differ from the dynamic type of the object being destroyed.
