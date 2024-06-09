# Lifetime

Each object and reference has a lifetime. There is a point of execution of a program when its lifetime begins, and there is a moment when it ends. The lifetime of an object begins when the storage for its type is obtained and its initialization is complete and ends when the destructor call starts. The lifetime of a reference begins when its initialization is complete and ends as if it were a scalar object. If the lifetime of the referred object ends before the end of the lifetime of the reference, the reference becomes a dangling reference.

## Temporary Object Lifetime

A prvalue of a complete type `T` can be converted to an xvalue of the same type. This conversion initializes a temporary object of type `T` from the prvalue by evaluating the prvalue with the temporary object as its result object, and produces an xvalue denoting the temporary object. If `T` is a class or array of class type, it must have an accessible and non-deleted destructor. This temporary materialization occurs in the following situations:

- binding a reference to a prvalue
- initializing an object of type `std::initializer_list<T>` from a braced-init-list
- when performing member access on a class prvalue
- when performing an array-to-pointer conversion or subscripting on an array prvalue
- for unevaluated operands in `sizeof` and `typeid`
- when a prvalue appears as a discarded-value expression

All temporary objects are destroyed as the last step in evaluating the full-expression that contains the point where they were created, and if multiple temporary objects were created, they are destroyed in the order opposite to the order of creation. However, the lifetime of a temporary object can be extended when binding to a reference, and the lifetime of temporaries created in the range-based for loop initializer are extended to the end of the loop, except for the temporaries that are function parameters.

## Guaranteed Copy Elision

Since C++17, a prvalue is not materialized until needed, and then it is constructed directly into the storage of its final destination. For example, this optimization can be applied in the following situations:

- Initializing the returned object in a `return` statement, when the operand is a prvalue of the same class type as the function return type
- In the initialization of an object, when the initializer expression is a prvalue of the same class type as the variable type

## Lifetime Extension

Whenever a reference is bound to a temporary object or a subobject thereof, the lifetime of the temporary object is extended to match the lifetime of the reference, where the temporary object or its subobject is denoted by one of the following expression:

- a temporary materialization conversion expression
- a built-in subscript expression of form `a[n]` or `n[a]`, where `a` is an array and is one of these expressions
- a class member access expression of form `e.m`, where `e` is one of these expressions and `m` designates a non-static data member of an object type
- a pointer-to-member operation of form `e.*mp`, where `e` is one of these expressions and `mp` is a pointer to data member

In general, the lifetime of a temporary cannot be further extended. Initializing another reference from the reference to which the temporary was bound doesn't affect its lifetime.
