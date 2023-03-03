# Automated Test

The test in Rust is a function that's annotated with the `#[test]` attribute. The `cargo test` command runs the functions annotated with the `#[test]` attribute and reports on whether each test function passes or fails. The `#[should_panic]` attribute makes a test pass if the function panics.
The `#[cfg(test)]` annotation on the tests module tells Rust to compile and run the test code when the `cargo test` command is invoked.

- The `assert!` macro invokes the `panic!` macro if its argument evaluates to `false`.
- The `asserteq!` macro invokes the `panic!` macro if the arguments are not equal.
- The `assertne!` macro invokes the `panic!` macro if the arguments are equal.
