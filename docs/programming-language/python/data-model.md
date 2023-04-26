# Data Model

## Collection API

The essential collection types in Python share the interfaces defined in `collections.abc`. The abstract base class `collections.abc.Collection` inherits from `Sized`, `Iterable`, and `Container`. There are multiple specializations of `Collection` in `collections.abc`, such as `Sequence`, `Set`, and `Mapping`.

- `Sized` is an abstract base class that provide the `__len__()` method, which supports `for`, unpacking, and other forms of iteration.
- `Iterable` is an abstract base class that provide the `__iter__()` method, which supports the `len` built-in function.
- `Container` is an abstract base class that provide the `__contains__()` method, which supports the `in` operator.

## Special Method

The Python's approach to operator overloading is the special method, allowing classes to define their own behavior with respect to language operators. For instance, if a class defines a method named `__getitem__()`, and `x` is an instance of this class, then `x[i]` is equivalent to `type(x).__getitem__(x, i)`. The full list of special method names are listed in [The Python Language Reference](https://docs.python.org/3/reference/datamodel.html#special-method-names).
