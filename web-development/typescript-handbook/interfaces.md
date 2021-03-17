# Interfaces

One of TypeScript’s core principles is that type checking focuses on the shape that values have. This is sometimes called "duck typing" or "structural subtyping".

## Our First Interface

```typescript
function printLabel(labeledObj: { label: string }) {
  console.log(labeledObj.label);
}

const myObj = { size: 10, label: "Size 10 Object" };
printLabel(myObj);
```

The printLabel function has a single parameter that requires that the object passed in has a property called `label` of type string. If the object passed in has more properties than this, the compiler will ignore it.

```typescript
interface LabeledValue {
  label: string;
}

function printLabel(labeledObj: LabeledValue) {
  console.log(labeledObj.label);
}
```

The interface `LabeledValue` is a name we can now use to describe the requirement in the previous example.

## Optional Properties

Interfaces with optional properties are written similar to other interfaces, with each optional property denoted by a ? at the end of the property name in the declaration.

```typescript
interface SquareConfig {
  color?: string;
  width?: number;
}
```

The advantage of optional properties is that you can describe these possibly available properties while still also preventing use of properties that are not part of the interface.

## Readonly properties

```typescript
interface Point {
  readonly x: number;
  readonly y: number;
}

let p1: Point = { x: 10, y: 20 };
p1.x = 5; // Error

let ro: ReadonlyArray<number> = [1, 2, 3, 4];
ro[0] = 12; // Error
```

Variables use `const` whereas properties use `readonly`.

## Excess Property Checks

Object literals get special treatment and undergo excess property checking when assigning them to other variables, or passing them as arguments.

If an object literal has any properties that the “target type” doesn’t have, you’ll get an error.

```typescript
interface SquareConfig {
  color?: string;
  width?: number;
}

function createSquare(config: SquareConfig) {}

let mySquare = createSquare({ colour: "red", width: 100 }); // Error: 'colour'

let myObj = { colour: "red", width: 100 };
let mySquare = createSquare(myObj); // No Error
```

We could solve this by adding a string index signature if you’re sure that the object can have some extra properties that are used in some special way.

```typescript
interface SquareConfig {
  color?: string;
  width?: number;
}
```

## Function Types

To describe a function type with an interface, we give the interface a call signature.

```typescript
interface SearchFunc {
  (source: string, subString: string): boolean;
}
```

```typescript
let mySearch: SearchFunc = function (source, subString) {
  return source.search(subString) > -1;
};
```

## Indexable Types

Indexable types have an index signature that describes the types we can use to index into the object.

```typescript
interface StringArray {
}
```

There are two types of supported index signatures: `string` and `number`. It is possible to support both types of indexers, but the type returned from a numeric indexer must be a subtype of the type returned from the string indexer.

While string index signatures are a powerful way to describe the "dictionary" pattern, they also enforce that all properties match their return type.

```typescript
interface NumberDictionary {
  length: number;
  name: string;
  // Error, since obj.name is the same as obj['name']
}
```

You can make index signatures readonly in order to prevent assignment to their indices.

```typescript
interface ReadonlyStringArray {
  readonly [index: number]: string;
}
```

## Class Types

### Implementing an interface

```typescript
interface ClockInterface {
  currentTime: Date;
  setTime(d: Date): void;
}

class Clock implements ClockInterface {
  constructor(h: number, m: number) {}
  currentTime: Date = new Date();
  setTime(d: Date) {
    this.currentTime = d;
  }
}
```

### The static and instance sides

To work with the static side of the class directly, we should define two interfaces.

```typescript
interface ClockConstructor {
  new (hour: number, minute: number): ClockInterface;
}

interface ClockInterface {
  tick(): void;
}

const Clock: ClockConstructor = class Clock implements ClockInterface {
  constructor(h: number, m: number) {}
  tick() {
    console.log("beep beep");
  }
};

const clock = new Clock(12, 17);
```

## Extending Interfaces

Like classes, interfaces can extend each other. An interface can extend multiple interfaces, creating a combination of all of the interfaces.

```typescript
interface Shape {
  color: string;
}

interface PenStroke {
  penWidth: number;
}

interface Square extends Shape, PenStroke {
  sideLength: number;
}
```

## Hybrid Types

An object could act as both a function and an object, with additional properties.

```typescript
interface Counter {
  (start: number): string;
  interval: number;
  reset(): void;
}

function getCounter(): Counter {
  let counter = function (start: number) {} as Counter;
  counter.interval = 123;
  counter.reset = function () {};
  return counter;
}
```

