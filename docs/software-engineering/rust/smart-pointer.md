# Smart Pointer

The smart pointer is a data structure that acts like a pointer but have additional metadata and capabilities. While the reference borrows data, the smart pointer owns the data it points to in most cases. The smart pointer implements the `Deref` and `Drop` traits. The `Deref` trait allows an instance of the smart pointer struct to behave like a reference. The `Drop` trait customizes the code that's run when an instance of the smart pointer goes out of scope.

- `Box<T>` is a type that allocates values on the heap.
- `Rc<T>` is a reference counting type that enables multiple ownership.
- `Ref<T>` and `RefMut<T>`, accessed through `RefCell<T>` is a type that enforces the borrowing rules at runtime.

## `Box<T>`

The most straightforward smart pointer is a box, whose type is written `Box<T>`. `Box` stores data on the heap rather than the stack, while leaving the pointer to the heap data on the stack. `Box` is suitable for types whose size can't be known at compile time, large amount of data that should not be copied, or trait objects.

```rust
fn main() {
  let b = Box::new(5);
  println!("b = {}", b);
}
```

`Box` allows recursive types. The value of recursive type can have another value of the same type as part of itself. The compiler doesn't know how much space a type takes up, so it should be wrapped with a `Box`, which has a fixed size regardless of which value it points to. For example, a cons list (from Lisp) that holds `i32` values could be defined as:

```rust
enum List {
  Cons(i32, Box<List>),
  Nil,
}

let list = Cons(1, Box::new(Cons(2, Box::new(Cons(3, Box::new(Nil))))));
```

## `Deref` Trait

The `Deref` trait customizes the behavior of the dereference operator `*`. The trait requires a `deref` method that borrows `self` and returns a reference to the inner data.

```rust
struct MyBox<T>(T);

impl<T> MyBox<T> {
  fn new(x: T) -> MyBox<T> {
    MyBox(x)
  }
}

impl<T> Deref for MyBox<T> {
  type Target = T;

  fn deref(&self) -> &Self::Target {
    &self.0
  }
}
```

Deref coercion converts a reference to a type that implements the `Deref` trait into a reference to another type. For example, deref coercion can convert `&String` to `&str` because `String` implements the `Deref` trait such that it returns `&str`. It happens when a reference to a particular type's value is passed as an argument to a function or method that doesn't match the parameter type in the function or method definition.

## `Drop` Trait

The `Drop` trait customizes the behavior when a value is about to go out of scope, which could be used to release resources like files or network connections. The `Drop` trait requires a `drop` method that takes a mutable reference to `self`. For example, `Drop` of `Box<T>` is used to deallocate the space on the heap that the box points to.

```rust
impl Drop for CustomSmartPointer {
  fn drop(&mut self) {
    println!("Dropping CustomSmartPointer with data `{}`!", self.data);
  }
}
```

The `drop` method (destructor) is not allowed to be called, because it still calls `drop` at the end of the scope, which would cause a double free error. However, `std::mem::drop` function could be used to force a variable to be dropped.

## `Rc<T>`

There are cases when a single value might have multiple owners. `Rc<T>` type is a reference counting smart pointer that keep trakcs of the number of references to a value to determine whether or not the value is still in use. `Rc::clone()` increments the reference count and clones the `Rc<T>` type.

```rust
enum List {
  Cons(i32, Rc<List>),
  Nil,
}

let a = Rc::new(Cons(5, Rc::new(Cons(10, Rc::new(Nil)))));
let b = Cons(3, Rc::clone(&a));
let c = Cons(4, Rc::clone(&a));
```

## `RefCell<T>`

Interior mutability is a design pattern in Rust that allows a data to be mutated even when there are immutable references to that data. This pattern uses `unsafe` code inside a data structure to bypass the borrowing rules.

`RefCell<T>` type represents single ownership over the data it holds. However, the borrowing rules (either one mutable reference or multiple immutable references) are enforced at runtime instead of compile time. Therefore, its internal data could be mutated even if the reference is immutable. This behavior enables certain memory-safe scenarios that are not allowed by the compiler.

The `borrow` method of `RefCell<T>` returns a `Ref<T>`, and the `borrow_mut` method returns a `RefMut<T>`. Both types implement `Deref`. The `RefCell<T>` tracks the number of `Ref<T>` and `RefMut<T>`, which enforces the number of immutable or mutable borrows at runtime.

`RefCell<T>` could be combined with `Rc<T>` to have multiple references to a mutable data.

```rust
enum List {
  Cons(Rc<RefCell<i32>>, Rc<List>),
  Nil,
}

let value = Rc::new(RefCell::new(5));
let a = Rc::new(Cons(Rc::clone(&value), Rc::new(Nil)));

let b = Cons(Rc::new(RefCell::new(3)), Rc::clone(&a));
let c = Cons(Rc::new(RefCell::new(4)), Rc::clone(&a));
*value.borrow_mut() += 10;
```

## Reference Cycle and `Weak<T>`

It's possible to create references where items refer to each other in cycle, which causes memory leaks. For example, this code creates two lists `a` and `b`, which contains a reference cycle.

```rust
enum List {
  Cons(i32, RefCell<Rc<List>>),
  Nil,
}

impl List {
  fn tail(&self) -> Option<&RefCell<Rc<List>>> {
    match self {
      Cons(_, item) => Some(item),
      Nil => None,
    }
  }
}

let a = Rc::new(Cons(5, RefCell::new(Rc::new(Nil))));
let b = Rc::new(Cons(10, RefCell::new(Rc::clone(&a))));

if let Some(link) = a.tail() {
  *link.borrow_mut() = Rc::clone(&b);
}
```

`Rc::clone` increases the `strong_count` of an `Rc<T>` instance, and it's cleaned up when `strong_count == 0`. The weak reference to the value in an `Rc<T>` instance could be created with `Rc::downgrade`. `Weak<T>` increments the `weak_count`, which isn't required to be `0` for the `Rc<T>` instance to be cleaned up. The `upgrade` method of `Weak<T>` returns an `Option<Rc<T>>`, which resolves to `Some` if `Rc<T>` is not cleaned up. For example, in a `TreeNode`, the node should refer to its parent instead of owning it.

```rust
struct TreeNode {
  value: i32,
  parent: RefCell<Weak<TreeNode>>,
  children: RefCell<Vec<Rc<TreeNode>>>,
}
```
