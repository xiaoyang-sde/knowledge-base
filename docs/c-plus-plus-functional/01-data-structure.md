# Data Structure

## Pair

`pair` is a class template that stores two heterogeneous objects as a single unit. The `make_pair` function could create a pair from two values. `pair` implements a `swap` method and overloads comparison operators. `pair` supports structured binding declaration.

```cpp
pair<string, int> pair_0 = make_pair("test", 1);
cout << pair_0.first << endl;
cout << pair_0.second << endl;

auto [first, second] = pair_0;
```

## Tuple

`tuple` is a class template that stores a fixed-sized collection of heterogeneous values, which is a generalization of `pair`. `tuple` supports structured binding declaration.

- The `make_tuple` function creates a tuple with copies of elements.
- The `tie` function creates a tuple of lvalue references or unpacks a tuple into individual objects.
- The `get` function accesses a specified element. `get` returns a lvalue reference to the element.
