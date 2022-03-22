# Error Handling

- **Unrecoverable Error**: Rust implements the `panic!` macro, which prints a failure message and terminates the program. The `panic!` macro should be invoked if the program is in a state that it can't handle.
- **Recoverable Error**: Rust implements the `Result<T, E>` enum that contains two variants, `Ok` and `Err`, to hold the return value in different situations. The `Result` enum uses Rust's type system to indicate that the operation might fail but the program could recover from it.
  - `unwrap` returns the value if `Ok` and panics if `Err`.
  - `expect` returns the value if `Ok` and panics with a specific message if `Err`.
  - `?` operator let the current function continues if `Ok` and early returns the error if `Err`.

```rust
use std::fs::File;
use std::io::ErrorKind;

fn main() {
  let f = File::open("hello.txt");
  let f = match f {
    Ok(file) => file,
    Err(error) => match error.kind() {
      ErrorKind::NotFound => panic!("File not found: {:?}", error),
      other_error => panic!("Problem opening the file: {:?}", other_error),
    },
  }
}
```
