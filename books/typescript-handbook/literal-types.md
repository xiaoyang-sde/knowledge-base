# Literal Types

A literal is a more concrete sub-type of a collective type. There are three sets of literal types available in TypeScript: strings, numbers, and booleans; by using literal types you can allow an exact value which a string, number, or boolean must have.

## Literal Narrowing

Using `const` to declare a variable will inform TypeScript that this object will never change.

```ts
// TypeScript sets the type to be string
let hiWorld = "Hi World";

// TypeScript sets the type to be "Hello World", not string
const helloWorld = "Hello World";
```

## String Literal Types

In practice string literal types combine nicely with union types, type guards, and type aliases.

```ts
type Easing = "ease-in" | "ease-out" | "ease-in-out";

animate(dx: number, dy: number, easing: Easing) {
  ...
}

animate(0, 0, "test"); // Error
```

String literal types can be used in the same way to distinguish overloads:

```ts
function createElement(tagName: "img"): HTMLImageElement;
function createElement(tagName: "input"): HTMLInputElement;

function createElement(tagName: string): Element {
  ...
}
```

## Numeric Literal Types

TypeScript also has numeric literal types, which act the same as the string literals above.

```ts
function rollDice(): 1 | 2 | 3 | 4 | 5 | 6 {
  return (Math.floor(Math.random() * 6) + 1) as 1 | 2 | 3 | 4 | 5 | 6;
}

interface MapConfig {
  lng: number;
  lat: number;
  tileSize: 8 | 16 | 32;
}
```

## Boolean Literal Types

TypeScript also has boolean literal types. You might use these to constrain object values whose properties are interrelated.

```ts
interface ValidationSuccess {
  isValid: true;
  reason: null;
}

interface ValidationFailure {
  isValid: false;
  reason: string;
}
```
