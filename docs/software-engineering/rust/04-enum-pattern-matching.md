# Enumeration and Pattern Matching

## Definition

Enumeration (Enum) defines a type by enumerating its possible variants. Each enum variant accepts optional associated data.

```rs
enum IpAddrKind {
    V4,
    V6,
}

enum Message {
    Quit,
    Move { x: i32, y: i32 },
    Write(String),
    ChangeColor(i32, i32, i32),
}
```

The variants of the enum are namespaced under its identifier. The `<enum_name>::<variant_name>` syntax could create an instance of the variants of an enum. The instance of each variant shares the same type `<enum_name>`. The name of each variant is also a function that constructs an instance of the enum.

```rs
let four = IpAddrKind::V4;
let change_color_message = Message::ChangeColor(0, 0, 0);
```

## The `Option` Enum

The standard library defines the `Option<T>` enum type that encodes the scenario in which a value could be something or it could be nothing. To prevent compliation error, the program should handle each variant in order to use an `Option<T>` value.

```rs
enum Option<T> {
    None,
    Some(T),
}

let some_number = Some(5);
let absent_number: Option<i32> = None;
```

## The `match` Control Flow Operator

The `match` control flow operator compares a value against a series of patterns and execute code based on the first pattern it matches. Patterns could be made up of literal values, variable names, and wildcards. The match arm is bind to the associated value of enum variants.

```rs
enum Coin {
    Penny,
    Quarter(String),
}

fn value_in_cents(coin: Coin) -> u8 {
    match coin {
        Coin::Penny => 1,
        Coin::Quarter(state) => {
          println!("State quarter from {}", state);
          25
        },
    }
}
```

The pattern matching should be exhaustive. The catch-all arm could be placed in the end to meet the requirement. `_` is a special pattern that matches any value and doesn't bind to that value.

## Concise Control Flow with `if let`

The `if let <pattern> = <expression>` syntax combines `if` and `let` to handle values that match one pattern while ignoring the rest. If the expression matches the pattern, the value in the variant will be bind to the pattern and the code in the block will be executed.

```rs
let config_max = Some(3u8);
if let Some(max) = config_max {
    println!("The maximum is configured to be {}", max);
} else {
    println!("The maximum is None");
}
```
