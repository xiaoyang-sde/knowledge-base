# Enums

Enums allow a developer to define a set of named constants. Using enums can make it easier to document intent, or create a set of distinct cases.

## Numeric enums

An enum can be defined using the `enum` keyword. `Up` would have the value `0`, `Down` would have `1`, etc.

```ts
enum Direction {
  Up,
  Down,
  Left,
  Right,
}

function respond(recipient: string, direction: Direction): void {
  // ...
}

respond("Princess Caroline", Direction.Up);
```

Numeric enums can be mixed in computed and constant members. The short story is, enums without initializers either need to be first, or have to come after numeric enums initialized with numeric constants.

```ts
enum E {
  A,
  B = getSomeValue(),
}
```

## String enums

```ts
enum Direction {
  Up = "UP",
  Down = "DOWN",
  Left = "LEFT",
  Right = "RIGHT",
}
```

While string enums don't have auto-incrementing behavior, string enums have the benefit that they "serialize" well (in debugger or alert).

## Heterogeneous enums

It’s advised that you don’t do this:

```ts
enum HeterogeneousEnum {
  No = 0,
  Yes = "YES",
}
```

## Computed and constant members

An enum member is considered constant if:
- It is the first member in the enum and it has no initializer. (`0` as default)
- It does not have an initializer and the preceding enum member was a numeric constant.
- The enum member is initialized with a constant enum expression. (It is a compile time error for constant enum expressions is `NaN` or `Infinity`.)

```ts
enum FileAccess {
  // constant members
  None,
  Read = 1 << 1,
  Write = 1 << 2,
  ReadWrite = Read | Write,
  // computed member
  G = "123".length,
}
```

## Union enums and enum member types

A literal enum member is a constant enum member with no initialized value, or with values that are initialized to

- any string literal
- any numeric literal
- a unary minus applied to any numeric literal (`-1`, `-100`, etc.)

When all members in an enum have literal enum values, enum members could also become types:

```ts
interface Square {
  kind: ShapeKind.Square;
  sideLength: number;
}
```

When all members in an enum have literal enum values, enum types themselves effectively become a union of each enum member.

```ts
function f(x: E) {
  if (x === E.Foo) {
    ...
  }
  ...
}
```

## Enums at runtime

Enums are real objects that exist at runtime. They can be passed around to functions:

```ts
function f(obj: { X: number }) {
  return obj.X;
}
```

## Enums at compile time

We could use `keyof typeof` to get a Type that represents all enum keys as strings.

```ts
enum LogLevel {
  ERROR,
  WARN,
  INFO,
  DEBUG,
}

/**
 * This is equivalent to:
 * type LogLevelStrings = 'ERROR' | 'WARN' | 'INFO' | 'DEBUG';
*/
type LogLevelStrings = keyof typeof LogLevel;
```

### Reverse mappings

Numeric enums members also get a reverse mapping from enum values to enum names.

```ts
enum Enum {
  A,
}

const a = Enum.A;
const nameOfA = Enum[a];
```

### const enums

Const enums can only use constant enum expressions and they are completely removed during compilation.

```ts
const enum Direction {
  Up,
  Down,
  Left,
  Right,
}

let directions = [
  Direction.Up,
  Direction.Down,
  Direction.Left,
  Direction.Right,
];
```

The code above will be compiled to:

```ts
"use strict";
let directions = [
    0 /* Up */,
    1 /* Down */,
    2 /* Left */,
    3 /* Right */,
];
```

## Ambient enums

Ambient enums are used to describe the shape of already existing enum types. Ambient enum member that does not have initializer is always considered computed.

```ts
declare enum Enum {
  A = 1,
  B,
  C = 2,
}
```
