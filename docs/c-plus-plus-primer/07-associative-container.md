# Associative Container

Associative container supports efficient lookup. The two main containers are `set` and `map`. The elements in a `map` are key–value pairs. The standard associative containers are `map`, `multimap`, `unordered_map`, `unordered_multimap`, `set`, `multiset`, `unordered_set`, `unordered_multiset`, which are different in three dimensions: `map` or `set`, single or multiple keys, and elements in order or not.

## Overview

Associative container supports the general container operations. The iterators are bidirectional. For the ordered containers (`map`, `multimap`, `set`, and `multiset`), the key type must have a strict weak ordering. For example, the container could accept a custom comparator:

```cpp
bool compare_int(const int &lhs, const int &rhs) {
  return lhs < rhs;
}

set<int, decltype(compare_int)*> integer_set(compare_int);
```

## Associative Container Operation

- Type Aliases
  - `key_type`: Type of the key
  - `mapped_type`: Type associated with each key for `map`
  - `value_type`: `key_type` for `set` or `pair<const key_type, mapped_type>` for `map`
- Iterator
  - The iterator of `set` points to `const key_type`.
  - The iterator of `map` points to `pair<const key_type, mapped_type>`.
- Insert
  - Insert an element: `insert(v)`, which returns a pair of an iterator to the element and a `bool` that indicates whether or not the element is inserted
  - Insert a range: `insert(begin, end)`
- Erase
  - Erase an element: `erase(k)`, which returns the number of elements removed
  - Erase a range: `erase(begin, end)`
- Access: `[]`, which initializes an element if it's not present, or `at(k)`, which throws an `out_of_range` exception if it's not present
- Find
  - Find an element: `find(k)`
  - Count the number of elements: `count(k)`
  - Find the first element with key no less than `k`: `lower_bound(k)`
  - Find the first element with key greater than `k`: `upper_bound(k)`

## Unordered Container

Rather than using a comparison operation to organize their elements, the unordered container uses a hash function and the key type's `==` operator. The unordered container is organized as a collection of buckets, each of which holds zero or more elements. The container puts all of its elements with a given hash value into the same bucket.

- Bucket Interface
  - Number of buckets: `bucket_count()`
  - Largest number of buckets the container can hold: `max_bucket_count()`
  - Number of elements in a specific bucket: `bucket_size(n)`
  - The bucket that contains `k`: `bucket(k)`
- Bucket Iteration
  - Iterator type that can access elements in a bucket: `local_iterator`
  - Iterator: `begin()`, `end()`
- Hash
  - Average number of elements per bucket: `load_factor()`
  - Average bucket size that the container tries to maintain: `max_load_factor()`
  - Reorganize storage so that `bucket_count >= n` and and `bucket_count > size / max_load_factor`: `rehash(n)`
  - Reorganize so that the container can hold `n` elements without a `rehash`: `reserve(n)`

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
