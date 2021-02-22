# Basic Type

## Boolean

```ts
let isDone: boolean = false;
```

## Number

```ts
let decimal: number = 6;
let hex: number = 0xf00d;
let big: bigint = 100n;
```

## String

```ts
let fullName: string = "Xiaoyang Liu";
let sentence: string = `Hello, my name is ${fullName}.`;
```

## Array

```ts
let list: number[] = [1, 2, 3];
let genericList: Array<number> = [1, 2, 3];
```

## Tuple

Tuple types allow you to epxress an array with a fixed number of elements whose types are known, but need not be the same.

```ts
let x: [string, number] = ["hello", 1];

// Error: Property 'substring' does not exist on type 'number'.
x[1].substring(1);

// Error: Tuple type '[string, number]' of length '2' has no element at index '3'.
x[3] = "world";
```

## Enum

An enum is a way of giving more friendly names to sets of numeric values. By default, enums begin numbering starting at 0.

```ts
enum Color {
  Red,
  Green,
  Blue,
}

let c: Color = Color.Green;
let colorName: string = Color[2]; // map value to name

enum Color1 {
  Red = 1,
  Green,
  Blue,
}

enum Color2 {
  Red = 1,
  Green = 10,
  Blue = 100,
}
```

## Unknown

Values with `unknown` type may come from dynamic content (from user) or we may want to intentionally accept all values in our API.

```ts
let notSure: unknown = 4;
notSure = false;
```

We could narrow the value with `unknown` type down with `typeof` checks, comparison checks, or other advanced type guards.

```ts
declare const maybe: unknown;

if (maybe === true) {
  // TypeScript knows that maybe is a boolean now
  const aBoolean: boolean = maybe;
  // So, it cannot be a string
  const aString: string = maybe; // Error
}

if (typeof maybe === "string") {

}
```

## Any

In some situations, not all type information is available or its declaration would take an inappropriate amount of effort. In these cases, we might want to opt-out of type checking. To do so, we label these values with the `any` type:

```ts
declare function getValue(key: string): any;

// Return value of the function is not checked
const str: string = getValue("myString");
```

Type safety is one of the main motivations for using TypeScript and you should try to avoid using any when not necessary.

## Void

`void` means the absence of having any type at all. You may commonly see this as the return type of functions that do not return a value:

```ts
function warnUser(): void {
  console.log("This is a message");
}
```

## Null and Undefined

In TypeScript, both `undefined` and `null` actually have their types named `undefined` and `null` respectively.

```ts
let u: undefined = undefined;
let n: null = null;
```

By default `null` and `undefined` are subtypes of all other types. That means you can assign `null` and `undefined` to something like `number`.

However, when using the `--strictNullChecks` flag, `null` and `undefined` are only assignable to `unknown`, `any` and their respective types (the one exception being that `undefined` is also assignable to `void`).

## Never

The `never` type represents the type of values that never occur. The `never` type is a subtype of, and assignable to, every type. However, no type is assignable to `never`.

```ts
function error(message: string): never {
  throw new Error(message);
}

// Function returning never must not have a reachable end point
function infiniteLoop(): never {
  while (true) {}
}
```

## Object

`object` is a type that represents the non-primitive type. With `object` type, APIs like `Object.create` can be better represented.

```ts
declare function create(o: object | null): void;

create({ prop: 0 });
create(null);
```

## Type assertions

Type assertions are a way to tell the compiler "trust me, I know what I’m doing."

- `as` syntax

```ts
let someValue: unknown = "this is a string";

let strLength: number = (someValue as string).length;
```

- "angle-bracket" syntax

```ts
let someValue: unknown = "this is a string";

let strLength: number = (<string>someValue).length;
```
