# Iterator

Each container defines an iterator, which is an object that points to an element inside the container. The iterators are classified into different categories:

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

## Insert Iterator

The insert iterator is an iterator adaptor that takes a container and yields an iterator that adds elements to the specified container. When a value is assigned to the iterator, the iterator calls a container operation to add an element at a specified position in the given container.

- `back_inserter(container)`: `push_back`
- `front_inserter(container)`: `push_front`
- `inserter(container, iterator)`: `insert` before `iterator`

## `iostream` Iterator

Even though the `iostream` types are not containers, there are iterators that can be used with objects of the IO types. The `istream_iterator` reads an input stream, and the `ostream_iterator` writes an output stream. These iterators treat their corresponding stream as a sequence of elements of a specified type.

```cpp
istream_iterator<int> int_it(cin);
istream_iterator<int> eof_it;
vector<int> vec(int_it, eof_it);
```

## Reverse Iterator

The reverse iterator is an iterator that traverses a container backward, from the last element toward the first. The reverse iterator inverts the meaning of increment and decrement. Incrementing (`++it`) a reverse iterator moves the iterator to the previous element, and derementing (`--it`) moves the iterator to the next element. The reverse iterator enables the generic algorithms to process the container from backward.

```cpp
sort(vec.rbegin(), vec.rend());
```
