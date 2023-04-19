# STL Container

## STL Container Operation

- Type Aliases
  - `iterator`: Type of the iterator for the container type
  - `const_iterator`
  - `size_type`: Unsigned integral type that holds the size of the largest possible container of this container type
  - `difference_type`: Signed integral type that holds the distance between two iterators
  - `value_type`: Element type
  - `reference`: Element's lvalue type (`value_type&`)
  - `const_reference`
- Construction
  - Default constructor: `Container container`
  - Constructor: `Container container(size, [initializer])`
  - Constructor: `Container container(container_1)`
  - Constructor: `Container container(iterator_begin, iterator_end)`
  - List initializer: `Container container{1, 2, 3}`
- Assignment
  - Replace element: `container_1 = container_2`
  - Replace element with list initializer: `container_1 = {1, 2, 3}`
  - Swap: `container_1.swap(container_2)` or `swap(container_1, container_2)`
- Size: `container.size()` (not valid for `forward_list`)
- Add or Remove Element
  - Add from reference: `container.insert()`
  - Add from initializer: `container.emplace()`
  - Remove: `container.erase()`
  - Clear: `contanier.clear()`
- Iterator
  - Forward iterator: `c.begin(), c.end(), c.cbegin(), c.cend()`
  - Reverse iterator: `c.rbegin(), c.rend(), c.crbegin(), c.crend()`

## Iterator

Iterator provides indirect access to elements in the containers. The `begin` method returns an iterator that points to the first element, while the `end` method returns an iterator that points to an element past the last element. The iterator could be dereferenced to access the element it points to. The `cbegin` and `cend` methods return constant iterators. Iterator allows arithmetic operations to move the iterator to different positions of the container.

- Input iterator: Read, single-pass increment
- Output iterator: Write, single-pass increment
- Forward iterator: Read and write, multi-pass increment
- Bidirectional iterator: Read and write, multi-pass increment and decrement
- Random-access iterator: Read and write, multi-pass iterator arithmetic

There are several additional kinds of iterators in the `iterator` header:

- Insert iterator: The iterator is bound to a container and can be used to insert elements into the container.
- Stream iterator: The iterator is bound to an input or output stream and can be used to iterate through the associated IO stream.
- Reverse iterator: The iterator moves backward, rather than forward.
- Move iterator: The special-purpose iterator moves rather than copies the element.

```cpp
vector<char> char_list(10);
for (auto it = char_list.begin(); it != char_list.end(); ++it) {
  *it = toupper(*it);
}

vector<char>::iterator it = char_list.begin(); // regular iterator
vector<char>::const_iterator it = char_list.begin(); // constant iterator
```

### Insert Iterator

The insert iterator is an iterator adaptor that takes a container and yields an iterator that adds elements to the specified container. When a value is assigned to the iterator, the iterator calls a container operation to add an element at a specified position in the given container.

- `back_inserter(container)`: `push_back`
- `front_inserter(container)`: `push_front`
- `inserter(container, iterator)`: `insert` before `iterator`

### `iostream` Iterator

Even though the `iostream` types are not containers, there are iterators that can be used with objects of the IO types. The `istream_iterator` reads an input stream, and the `ostream_iterator` writes an output stream. These iterators treat their corresponding stream as a sequence of elements of a specified type.

```cpp
istream_iterator<int> int_it(cin);
istream_iterator<int> eof_it;
vector<int> vec(int_it, eof_it);
```

### Reverse Iterator

The reverse iterator is an iterator that traverses a container backward, from the last element toward the first. The reverse iterator inverts the meaning of increment and decrement. Incrementing (`++it`) a reverse iterator moves the iterator to the previous element, and derementing (`--it`) moves the iterator to the next element. The reverse iterator enables the generic algorithms to process the container from backward.

```cpp
sort(vec.rbegin(), vec.rend());
```

## Sequential Container

The sequential containers provide fast sequential access to their elements. However, these containers offer different performance trade-offs relative to the costs to add or delete elements to the container and the costs to perform non-sequential access to elements of the container.

- `vector`: Flexible-size list that supports fast random access. Inserting or deleting elements at the back is efficient.
- `deque`: Double-ended queue that supports fast random access. Inserting or deleting elements at the front and back is efficient.
- `list`: Doubly linked list that supports bidirectional sequential access. Fast insert or delete in the list.
- `forward_list`: Singly linked list that supports sequential access in one direction. Fast insert or delete in the list.
- `array`: Fixed-size list that supports fast random access.
- `string`: `vector<char>` with specialized operations.

### Sequential Container Operation

- Insert
  - Insert to back: `push_back(element)`
  - Insert to front: `push_front(element)`
  - Insert before an iterator: `insert(iterator, element)`
  - Insert multiple before an iterator: `insert(iterator, n, element)`
  - Insert a range before an iterator: `insert(iterator, begin, end)`
  - Insert an initializer list before an iterator: `insert(iterator, initializer_list)`
- Access
  - Access the first element: `front()`
  - Access the last element: `back()`
  - Access a random element: `[]` or `at(index)` (`at` could throw an `out_of_range` exception)
- Erase
  - Erase the first element: `pop_front()`
  - Erase the last element: `pop_back()`
  - Erase a random element: `erase(index)`
  - Erase a range: `erase(begin, end)`
  - Erase all elements: `clear()`
- Resize
  - Resize the container: `resize(n)`
  - Resize the container and initialize the new elements: `resize(n, t)`

### Pair

`pair` is a class template that stores two heterogeneous objects as a single unit. The `make_pair` function could create a pair from two values. `pair` implements a `swap` method and overloads comparison operators. `pair` supports structured binding declaration.

```cpp
pair<string, int> pair_0 = make_pair("test", 1);
cout << pair_0.first << endl;
cout << pair_0.second << endl;

auto [first, second] = pair_0;
```

### Tuple

`tuple` is a class template that stores a fixed-sized collection of heterogeneous values, which is a generalization of `pair`. `tuple` supports structured binding declaration.

- The `make_tuple` function creates a tuple with copies of elements.
- The `tie` function creates a tuple of lvalue references or unpacks a tuple into individual objects.
- The `get` function accesses a specified element. `get` returns a lvalue reference to the element.

### String

The `string` container is a variable-length sequence of characters. It's defined in the `string` header of the `std` namespace. Each class defines how objects of its type can be initialized. The `string` class could be initialized with default constructor, string literal, or a character with count.

- Direct initialization: Invoke the constructor of the class
- Copy initialization: Assign an initializer to the object

```cpp
#include <string>
using std::string;

string s_1;
string s_2 = "test"; // copy initialization
string s_3(10, 'c'); // direct initialization
string s_4 = string(10, 'c'); // copy initialization
```

The `size()` method of the `string` class returns an unsigned integer with `string::size_type` type. The type is able to hold the length of the string.

The comparator of the `string` class determine the order of two strings with these criteria:

- If two strings have different lengths and if every character in the shorter string is equal to the corresponding character of the longer string, then the shorter string is less than the longer one.
- If a characters at corresponding positions in the two strings differ, then the result of the string comparison is the result of comparing the first character at which the strings differ.

The range for-loop could process all characters the string holds. To access individual character, the `[]` operator takes a `string::size_type` and returns a reference of `char`. Out-of-range access returns undefined result.

### Vector

The `vector` container is a collection of objects with the same type. The `vector` is a class template such that the compiler could use it to generate code.

```cpp
#include <vector>
using std::vector;

vector<int> int_vector; // default initialization
vector<int> int_vector_clone(int_vector); // clone
vector<string> article_list = {"a", "an", "the"}; // list initialization
vector<string> a_list("a", 10); // value initialization
vector<string> string_list(10); // value initialization
```

The range for-loop could process all characters the vector holds. To access individual element, the `[]` operator takes a `vector<T>::size_type` and returns a reference of type `T`. Out-of-range access returns undefined result.

- `push_back` is a method that could add elements to the end of the vector.
- `size` is a method that returns the size of the vector with the `vector<T>::size_type` type.

#### Vector Growth

To support fast random access, vector elements are stored in a continuous chunk of memories. To add an element that exceeds the quota, the container must allocate a new chunk to hold the elements. To minimize the costs, the implementor uses allocation strategies that reduce the number of times the container is reallocated.

- The number of elements in the container: `size()`
- The number of elements that the container could hold before reallocation: `capacity()`
- Allocate space for at least `n` elements: `reserve(n)`
- Request to reduce the allocated chunk to `size()`: `shrink_to_fit()`

### Array

The array is a collection of objects with the same type and fixed size. The array is a compound type and has a declarator with the form of `a[d]`, in which `d` indicates the size of the array. The size must be a constant expression. The elements in the array are default initialized. It's illegal to assign an array to another array.

```cpp
int int_list_1[3];
int int_list_2[3] = {1, 2, 3};
int int_list_3[] = {1, 2, 3};

char char_list_1[] = {'a', 'b', 'c'};
char char_list_2[] = "abc"; // 'a', 'b', 'c', '\0'

int *ptr_arr[10]; // array of pointers
int (*arr_ptr)[10] = &arr; // pointer to array
int (&arr_ptr)[10] = arr; // reference to array
```

The range for-loop could process all elements the array holds. To access individual element, the `[]` operator takes a `size_t` and returns a reference of the element. Out-of-range access returns undefined result.

#### Array and Pointer

In C++ pointers and arrays are intertwined. When an array is used, the compiler substitutes a pointer to the first element.

```cpp
int arr[3] = {1, 2, 3};
int *element = arr; // equivalent to `int *element = &arr[0]`
element++; // point to the second element
```

The `begin` and `end` function returns a pointer to the first or the one past the last element in the array. These functions are defined in the `iterator` header.

```cpp
int *first = begin(arr);
int *last = end(arr);
```

The `vector` class provides a constructor that accepts two pointers. The two pointers used to construct the vector mark the range of values to use to initialize the elements.

```cpp
vector<int> int_vec(begin(int_arr), end(int_arr));
```

#### Multi-dimensional Array

The multi-dimensional array in C++ is an array of arrays. It could be initialized similar to the one-dimensional array.

```cpp
int arr[10][20][30] = {0};
int matrix[3][3] = {
  {1, 2, 3},
  {1, 2, 3},
  {1, 2, 3}
};
```

### Container Adaptor

The adaptor is a mechanism for making a container acts like another container. There are three sequential container adaptors: `stack`, `queue`, and `priority_queue`. The `stack` and `queue` are implemented from `deque`, and the `priority_queue` is implemented from `vector`. To override the default container, use a sequential container as the second type argument, such as `stack<string, vector<string>>`.

- `stack` requires `push_back`, `pop_back`, and `back`
- `queue` requires `back`, `push_back`, `front`, and `push_front`
- `priority_queue` requires random access, `front`, `push_back`, and `pop_back`

## Associative Container

Associative container supports efficient lookup. The two main containers are `set` and `map`. The elements in a `map` are key–value pairs. The standard associative containers are `map`, `multimap`, `unordered_map`, `unordered_multimap`, `set`, `multiset`, `unordered_set`, `unordered_multiset`, which are different in three dimensions: `map` or `set`, single or multiple keys, and elements in order or not.

### Overview

Associative container supports the general container operations. The iterators are bidirectional. For the ordered containers (`map`, `multimap`, `set`, and `multiset`), the key type must have a strict weak ordering. For example, the container could accept a custom comparator:

```cpp
bool compare_int(const int &lhs, const int &rhs) {
  return lhs < rhs;
}

set<int, decltype(compare_int)*> integer_set(compare_int);
```

### Associative Container Operation

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

### Unordered Container

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
