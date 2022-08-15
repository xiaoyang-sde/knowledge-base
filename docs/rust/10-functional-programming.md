# Functional Programming

## Closure

```rs
let expensive_closure = |num: u32| -> u32 {
    thread::sleep(Duration::from_secs(2));
    num
};
```

Rust compiler could infer the types of the parameters of a closure function as it's short and relevant within a narrow context.

Closures can capture values from their environment in three ways, which directly map to the three ways a function can take a parameter: borrowing immutably, borrowing mutably, and taking ownership. The closure will decide which of these to use based on what the body of the function does with the captured values.

Functions and closures will implement one, two, or all three of the `Fn` traits:

- `FnOnce` applies to closures that can be called at least once. All closures implement at least this trait. The closure that moves captured values out of its body will only implement `FnOnce`.
- `FnMut` applies to closures that don't move captured values out of their body, but that might mutate the captured values. The closures could be called more than once.
- `Fn` applies to closures that don’t move captured values out of their body and that don’t mutate captured values, as well as closures that capture nothing from their environment.

For example, the `unwrap_or_else` function of `Option` requires a closure that implements `FnOnce`, while the `sort_by_key` function of `Vec` requires `FnMut`.

## Iterator

The iterator pattern performs some task on a sequence of items in turn. In Rust, the iterator has no effect until it's consumed by a method. For example, `Vec` implements these methods that could return an iterator:

- `iter()` returns an iterator of immutable references
- `iter_mut()` returns an iterator of mutable references
- `into_iter()` takes the ownership of the `Vec` and returns an iterator of owned values

```rs
let v1 = vec![1, 2, 3];
let v1_iter = v1.iter();
```

The iterator implements the `Iterator` trait. `type Item` is an associated type with this trait, which means that implementing this trait requires a defined `Item` type. The trait requires the `next` method, which returns one item of the iterator at a time wrapped in `Some`, and returns `None` when the iteration ends.

```rs
pub trait Iterator {
    type Item;

    fn next(&mut self) -> Option<Self::Item>;
}
```

The `Iterator` trait has a number of different methods that consumes the iterator with default implementations. For example, the `sum()` method takes the ownership of the iterator and returns the sum of all items, and the `collect()` method returns a collection that contains all items.

The `Iterator` trait has a number of different methods that produce new iterators with default implementations. For example, the `map()` method takes a closure to call on each item as the items are iterated through, and returns a new iterator that produces the modified items.
