# Functions

In TypeScript, while there are classes, namespaces, and modules, functions still play the key role in describing how to do things.

## Function Types

### Typing the function

We can add types to each of the parameters and then to the function itself to add a return type.

```ts
const add = function (x: number, y: number): number {
  return x + y;
};
```

### Writing the function type

Let’s write the full type of the function out by looking at each piece of the function type.

A function’s type has the same two parts: the type of the arguments and the return type. When writing out the whole function type, both parts are required.

```ts
const add: (x: number, y: number) => number = function(
  x: number,
  y: number
): number {
  return x + y;
};
```

## Inferring the types

The TypeScript compiler can figure out the type even if you only have types on one side of the equation. This is called "contextual typing", a form of type inference.

```ts
let inferredAdd: (baseValue: number, increment: number) => number = function (x, y) {
  return x + y;
};
```

## Optional and Default Parameters

In TypeScript, the number of arguments given to a function has to match the number of parameters the function expects.

However, every parameter in JavaScript is optional, and users may leave them off as they see fit. When they do, their value is `undefined`.

We can get this functionality in TypeScript by adding a `?` to the end of parameters we want to be optional. Any optional parameters must follow required parameters.

```ts
function buildName(firstName: string, lastName?: string) {
  if (lastName) return firstName + " " + lastName;
  else return firstName;
}

const result = buildName("Bob");
```

In TypeScript, we can also set a value that a parameter will be assigned if the user does not provide one, or if the user passes `undefined` in its place.

```ts
function buildName(firstName: string, lastName = "Smith") {
  return firstName + " " + lastName;
}
```

Optional parameters and trailing default parameters will share commonality in their types. For example, the functions above will share the type `(firstName: string, lastName?: string) => string`. The default value of `lastName` disappears in the type.

Unlike plain optional parameters, default-initialized parameters don’t need to occur after required parameters. User could explicity pass `undefined` to get the default initialized value if a default-initialized parameter comes before a required parameter.

## Rest Parameters

In JavaScript, you can work with the arguments directly using the `arguments` variable that is visible inside every function body.

In TypeScript, you can gather these arguments together into a single variable. The compiler will build an array of the arguments passed in with the name given after the ellipsis `(...)`, allowing you to use it in your function.

```ts
function buildName(firstName: string, ...restOfName: string[]) {
  return firstName + " " + restOfName.join(" ");
}
```

## this

ypeScript lets you catch incorrect uses of `this` with a couple of techniques.

### this parameters

`this` parameters are fake parameters that come first in the parameter list of a function:

```ts
function f(this: void) {
  // make sure `this` is unusable in this standalone function
}
```

### this parameters in callbacks

```ts
interface UIElement {
  addClickListener(onclick: (this: void, e: Event) => void): void;
}
```

`this: void` means that `addClickListener` expects `onclick` to be a function that does not require a `this` type.

```ts
class Handler {
  info: string;
  onClickBad(this: Handler, e: Event) {
    // oops, used `this` here. using this callback would crash at runtime
    this.info = e.message;
  }
}
```

With `this` annotated, you make it explicit that `onClickBad` must be called on an instance of `Handler`. Thus, it has a type of `(this: Handler, e: Event) => void`, which is not assignable to `addClickListener`.

However, arrow functions use the outer `this`, so you can always pass them to something that expects `this: void`.

```ts
class Handler {
  info: string;
  onClickGood = (e: Event) => {
    this.info = e.message;
  };
}
```

## Overloads

It's not uncommon for a single JavaScript function to return different types of objects based on the shape of the arguments passed in.

We could describe this behavior in the type system by supplying multiple function types for the same function as a list of overloads.

```ts
function pickCard(x: { suit: string; card: number }[]): number;
function pickCard(x: number): { suit: string; card: number };
function pickCard(x: any): any {
  // Check to see if we're working with an object/array
  // if so, they gave us the deck and we'll pick the card
  if (typeof x == "object") {
    let pickedCard = Math.floor(Math.random() * x.length);
    return pickedCard;
  }
  // Otherwise just let them pick the card
  else if (typeof x == "number") {
    let pickedSuit = Math.floor(x / 13);
    return { suit: suits[pickedSuit], card: x % 13 };
  }
}
```

In order for the compiler to pick the correct type check, it follows a similar process to the underlying JavaScript. It looks at the overload list and, proceeding with the first overload, attempts to call the function with the provided parameters. If it finds a match, it picks this overload as the correct overload.

Note that the function `pickCard(x): any` piece is not part of the overload list, so it only has two overloads. Thus, calling `pickCard` with other parameter types would cause an error.
