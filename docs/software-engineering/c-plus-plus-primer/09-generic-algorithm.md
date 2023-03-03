# Generic Algorithm

The generic algorithm defined in the `algorithm` header operate in terms of iterators and iterator operations, which makes them independent of the container. With a few exceptions, the algorithms operate over a range of elements, such as `(container.begin(), container.end())`.

```cpp
int sum = accumulate(vec.cbegin(), vec.cend(), 0);
```

- Read algorithm: Some algorithms read, but never write to, the elements in their input range, such as `accumulate` or `find`.
- Write algorithm: Some algorithms write to elements in the input range itself, such as `fill`.
- Reorder algorithm: Some algorithms reorder the elements in the input range, such as `sort` or `unique`.

## Insert Iterator

The insert iterator is an iterator that adds elements to a container, which ensures that the container has enough element to hold the output. The `back_inserter` function defined in the `iterator` header takes a reference to a container and returns an insert iterator bound to that container.

```cpp
vector<int> vec;
auto iterator = back_inserter(vec);
*it = 42; // insert `42` to `vec`

fill_n(back_inserter(vec), 10, 0); // insert `10` elements to `vec`
```

## Customizing Operation

For comparison operations, the algorithm uses either the element type's `<` or `==` operator in default, but it also accepts custom comparison operators. For example, the `sort` algorithm accepts a custom comparator.

```cpp
sort(vec.begin(), vec.end(), [](const string &a, const string &b) {
  return a.size() < b.size();
});
```
