# Generic Type, Trait, and Lifetime

- Generic function: `fn generic<T>(args: T) -> T {}`
- Generic struct: `struct Generic<T> {}` and `impl<T> Generic<T> {}`
- Generic enum: `enum Generic<T> {}` and `impl<T> Generic<T> {}`

## Trait

The trait tells the Rust compiler about functionality a particular type has and could share with other types. The trait definition groups method signatures together to define a set of behaviors to accomplish some purpose. The trait could be defined with `trait` keyword. The method definition could be provided as the default implementation.

The trait implementation is defined with `impl <trait_name> for <struct_name>` syntax. Each type implementing the trait should provide custom implementation of the trait methods. The trait should be implemented on a type if at least one of the trait or the type is local to the crate to prevent duplicate implementation.

```rust
pub trait Summary {
  fn summarize_author(&self) -> String;
  fn summarize(&self) -> String {
    format!("(Read more from {}...)", self.summarize_author())
  }
}

impl Summary for Tweet {
  fn summarize_author(&self) -> String {
    format!("@{}", self.username)
  }
}
```

The trait bound syntax limits the parameter to be a type that implements the specific trait. The `impl <trait_1>` syntax is a syntax sugar that simplifies the trait bound syntax. The `<trait_1> + <trait_2>` syntax combines multiple traits in the bound. The `impl <trait_1>` syntax could be used as the return type.

```rust
pub fn generic_function_2<T: Summary>(item: &T) {
  println!("Breaking news! {}", item.summarize());
}

pub fn generic_function_1(item: &impl Summary) {
  println!("Breaking news! {}", item.summarize());
}
```

## Lifetime

The main aim of lifetimes is to prevent dangling references, which cause a program to reference data other than the data it’s intended to reference. The borrow checker of the Rust compiler compares scopes to determine whether all borrows are valid.

The lifetime annotation describes the relationships of the lifetimes of multiple references to each other without affecting the lifetime and informs the borrow checker to reject values that don't adhere to these constraints. The `'<lifetime_paramater>` syntax is used to define the lifetime annotation. The special `'static` lifetime means the reference could live for the entire duration of the program. The content of the reference is stored directly in the program's binary, which is always available.

The Rust compiler implements deterministic lifetime elision rules. If Rust applies the rules but there is still ambiguity as to what lifetimes the references have, the compiler will throw an error. Lifetimes on function or method parameters are called input lifetimes, and lifetimes on return values are called output lifetimes.

- Each parameter that is a reference gets its own lifetime parameter.
- If there is one input lifetime parameter, that lifetime is assigned to all output lifetime parameters.
- If there are multiple input lifetime parameters, but one of them is `&self` or `&mut self` because this is a method, the lifetime of `self` is assigned to all output lifetime parameters.
