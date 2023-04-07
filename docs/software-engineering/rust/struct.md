# Struct

The sturct is a custom data type that packages together multiple related values that make up a meaningful group.

## Define and Instantiate Struct

The keyword `struct` could define a struct and the curly brackets define the field names and types. The struct is instantiated by specifying the fields and their values. The `<struct_name>.<field_name>` could be used to access or mutate the value in the field.

```rust
struct User {
    active: bool,
    username: String,
    email: String,
    sign_in_count: u64,
}

let mut test_user = User {
    email: String::from("test@example.com"),
    username: String::from("test-user"),
};

test_user.email = String::from("another@example.com");
```

The field init shorthand syntax is useful when variables and fields have the same name.

```rust
let email = String::from("another@example.com");
let mut test_user = User {
    email,
};
```

The struct update syntax could create a new struct instance while keep most of an old instance's values. The `..` syntax must come last to specify that remaining fields should get their values from the fields in another instance.

```rust
let user2 = User {
  email: String::from("test@example.com"),
  ..user1,
};
```

The tuple struct specifies the type of the fields without the associated names. The tuple struct behaves like a tuple, but it has its own type.

```rust
struct Color(i32, i32, i32);
let black = Color(0, 0, 0);
```

The unit-like struct could be defined without any fields. It's useful to implement a trait on some type but don't have any data to store in it.

```rust
struct AlwaysEqual;
let subject = AlwaysEqual;
```

## Method Syntax

The `impl <struct_name>` block could be used to define the method associated to the struct. The signature for the method takes the `&self` parameter that implicitly has the type `&Self`, which is an alias to the struct type. The method could take ownership of `self` (`self`), borrow `self` immutably (`&self`), or borrow `self` mutably (`&mut self`).

```rust
impl Rectangle {
    fn area(&self) -> u32 {
        self.width * self.height
    }
}
```

The function in the `impl` block that doesn't have the `self` parameter is an associated function, which could be used as constructor.

```rust
impl Rectangle {
  fn square(size: u32) -> Rectangle {
    Rectangle {
      width: size,
      height: size,
    }
  }
}
```
