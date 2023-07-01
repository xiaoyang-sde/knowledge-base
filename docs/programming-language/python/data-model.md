# Data Model

## Collection API

The essential collection types in Python share the interfaces defined in `collections.abc`. The abstract base class `collections.abc.Collection` inherits from `Sized`, `Iterable`, and `Container`. There are multiple specializations of `Collection` in `collections.abc`, such as `Sequence`, `Set`, and `Mapping`.

- `Sized` is an abstract base class that provide the `__len__()` method, which supports `for`, unpacking, and other forms of iteration.
- `Iterable` is an abstract base class that provide the `__iter__()` method, which supports the `len` built-in function.
- `Container` is an abstract base class that provide the `__contains__()` method, which supports the `in` operator.

## Special Method

The Python's approach to operator overloading is the special method, allowing classes to define their own behavior with respect to language operators. For instance, if a class defines a method named `__getitem__()`, and `x` is an instance of this class, then `x[i]` is equivalent to `type(x).__getitem__(x, i)`. The full list of special method names are listed in [The Python Language Reference](https://docs.python.org/3/reference/datamodel.html#special-method-names).

## Sequence

- The container sequence, such as `list`, `tuple`, and `collections.deque`, holds references to the objects it contains.
- The flat sequence, such as `str`, `bytes`, and `array.array`, stores the value of its contents in its own space.

## Mapping

The `collections.abc` module provides the `Mapping` and `MutableMapping` ABCs describing the interfaces of `dict` and similar types. The `collections.UserDict` class could be used to implement a `dict`. The keys in a mapping class must be hashable.

Each object is hashable if it has a hash code which never changes during its lifetime (`__hash__()`), and can be compared to other objects (`__eq__()`). Hashable objects which compare equal must have the same hash code.

- Numeric types, flat immutable types (`str`, `bytes`), and `frozenset` are hashable.
- `tupe` is hashable if all elements are hashable.
- User-defined types are hashable because their hash code is their `id()`, and the `__eq__()` method inherited from the object class compares the object `id()`.
