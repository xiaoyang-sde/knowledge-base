# Ownership

The memory in Rust is managed through a system of ownership with a set of rules that the compiler checks at compile time.

## Definition

### The Stack and the Heap

The stack stores values in the order it gets them and removes the values in the opposite order. All data stored on the stack must have a known, fixed size.

The heap stores values with an unknown size at compile time or a size that might change. The memory allocator finds an empty spot in the heap that is larg enough, marks it as being in use, and returns a pointer, which is the address of that location. The pointer is stored on th stack because it has known, fixed size.

### Ownership Rules

- Each value has a variable that’s called its owner.
- There can only be one owner at a time.
- When the owner goes out of scope, the value will be dropped.

### Variable Scope

The scope is the range within a program for which an item is valid. The variable is valid from the point at which it's declared until it goes out of the current scope.

### The `String` Type

The string literal stores text that is known at compile time. The `String` type manages data allocated on the heap and is able to store an amount of text that is unknown at compile time.

```rust
let mut s = String::from("hello");
s.push_str(", world!");
println!("{}", s);
```

### Memory and Allocation

The `String` type allocates an amount of memory on the heap, unknown at compile time, to hold the contents. The allocated memory is automatically returned once the variable that owns it goes out of scope. Rust calls the special `drop` function to free the memory the variable is allocated.

#### Move

When a `String` variable stored on the heap is assigned to another `String` variable, the `String` data (pointer, length, capacity) on the stack is copied, but the data on the heap is not copied.

To prevent the double free error, Rust consider the previous variable to be no longer valid, thus it won't be deallocated when it goes out of scope.

```rust
let s1 = String::from("hello");
let s2 = s1;

// error[E0382]: borrow of moved value: `s1`
println!("{}, world!", s1);
```

#### Copy and Clone

The data type that stores information on stack or implements the `Copy` trait copies the actual value instead of the pointer. The data type that implements the `Copy` trait couldn't implement the `Drop` trait.

```rust
let x = 5;
let y = x;

println!("x = {}, y = {}", x, y);
```

For data type such as `String` that stores data on the heap, the `clone` method copies the heap data.

```rust
let s1 = String::from("hello");
let s2 = s1.clone();

println!("s1 = {}, s2 = {}", s1, s2);
```

### Ownership and Function

The semantics for passing a value to a function are similar to those for assigning a value to a variable. Passing a variable to a function will move or copy, just as assignment does.

The ownership of a variable follows the pattern that assigning a value to another variable moves it. When a variable that includes data on the heap goes out of scope, the value will be cleaned up by drop unless the data has been moved to be owned by another variable.

## Reference and Borrowing

The `&` operator creates a reference to a variable without taking ownership of it. When a reference goes out of the scope, the actual value it points to won't be dropped. The compiler guarantees that the reference will never be a dangling reference.

```rust
fn calculate_length(s: &String) -> usize {
    s.len()
}
```

The reference is immutable by default. The mutable reference could be created with `&mut` operator. Each variable could have only one mutable reference or many immutable references at the same time to prevent data races. The reference's scope starts from where it is introduced and continues through the last time that reference is used.

```rust
fn change(some_string: &mut String) {
    some_string.push_str("world");
}
```

## The Slice Type

The slice data type could reference a contiguous sequence of elements in a collection and it doesn't have ownership. The slice internally stores the starting point and length.

```rust
let numbers = [1, 2, 3, 4, 5];
let numbers_slice = &numbers[1..3];
```

The string sllice is a reference to part of a `String`, which could be created with `[starting_index..ending_index]`. The string literal is a string slice pointing to the specific point of binary.

```rust
fn first_word(s: &String) -> &str {
    let bytes = s.as_bytes();

    for (i, &item) in bytes.iter().enumerate() {
        if item == b' ' {
            return &s[0..i];
        }
    }

    &s[..]
}
```
