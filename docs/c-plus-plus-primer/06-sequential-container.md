# Sequential Container

The container holds a collection of objects of a specified type. The order of the elements in a sequential container corresponds to the positions in which the elements are added to the container. The container classes share a common interface.

## Overview

The sequential containers provide fast sequential access to their elements. However, these containers offer different performance trade-offs relative to the costs to add or delete elements to the container and the costs to perform non-sequential access to elements of the container.

- `vector`: Flexible-size list that supports fast random access. Inserting or deleteing elements at the back is efficient.
- `deque`: Double-ended queue that supports fast random access. Inserting or deleteing elements at the front and back is efficient.
- `list`: Doubly linked list that supports bidirectional sequential access. Fast insert or delete in the list.
- `forward_list`: Singly linked list that supports sequential access in one direction. Fast insert or delete in the list.
- `array`: Fixed-size list that supports fast random access.
- `string`: `vector<char>` with specialized operations.

## Container Operation

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
  - Constructor: `Contaienr container(size, [initializer])`
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

## Sequential Container Operation

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

## Vector Growth

To support fast random access, vector elements are stored in a continuous chunk of memories. To add an element that exceeds the quota, the container must allocate a new chunk to hold the elements. To minimize the costs, the implementor uses allocation strategies that reduce the number of times the container is reallocated.

- The number of elements in the container: `size()`
- The number of elements that the container could hold before reallocation: `capacity()`
- Allocate space for at least `n` elements: `reserve(n)`
- Request to reduce the allocated chunk to `size()`: `shrink_to_fit()`

## Container Adaptor

The adaptor is a mechanism for making a container acts like another container. There are three sequential container adaptors: `stack`, `queue`, and `priority_queue`. The `stack` and `queue` are implemented from `deque`, and the `priority_queue` is implemented from `vector`. To override the default container, use a sequential container as the second type argument, such as `stack<string, vector<string>>`.

- `stack` requires `push_back`, `pop_back`, and `back`
- `queue` requires `back`, `push_back`, `front`, and `push_front`
- `priority_queue` requires random access, `front`, `push_back`, and `pop_back`
