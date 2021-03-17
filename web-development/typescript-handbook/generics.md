# Generics

Generics allows us to create a component that can work over a variety of types rather than a single one.

## Hello World of Generics

The identity function is a function that will return back whatever is passed in.

Without generics, we actually are losing the information about what that type was when the function returns.

```typescript
function identity(arg: any): any {
  return arg;
}
```

We could use a type variable, which works on types rather than values.

```typescript
function identity<T>(arg: T): T {
  return arg;
}

const output: string = identity<string>("myString");

// Type argument inference
const output1 = identity("myString");
```

## Working with Generic Type Variables

We could use `T[]` to denote an array of variables with type `T`.

```typescript
function loggingIdentity<T>(arg: T[]): T[] {
  console.log(arg.length);
  return arg;
}
```

## Generic Types

The type of generic functions:

```typescript
const myIdentity: <T>(arg: T) => T = identity;
const myIdentitySignature: { <T>(arg: T): T } = identity;
```

Generic Interface:

```typescript
interface GenericIdentityFn {
  <T>(arg: T): T;
}
```

We may want to move the generic parameter to be a parameter of the whole interface.

```typescript
interface GenericIdentityFn<T> {
  (arg: T): T;
}

let myIdentity: GenericIdentityFn<number> = identity;
```

Instead of describing a generic function, we now have a non-generic function signature that is a part of a generic type. When we use `GenericIdentityFn`, we now will also need to specify the corresponding type argument \(here: `number`\), effectively locking in what the underlying call signature will use.

## Generic Classes

Generic classes have a generic type parameter list in angle brackets \(`<>`\) following the name of the class.

```typescript
class GenericNumber<T> {
  zeroValue: T;
  add: (x: T, y: T) => T;
}

const myGenericNumber = new GenericNumber<number>();
myGenericNumber.zeroValue = 0;
```

Generic classes are only generic over their instance side rather than their static side, so static members can not use the class's type parameter.

## Generic Constraints

Instead of working with any and all types, we’d like to constrain this function to work with any and all types that also have the `.length` property.

```typescript
interface Lengthwise {
  length: number;
}

function loggingIdentity<T extends Lengthwise>(arg: T): T {
  return arg;
}
```

### Using Type Parameters in Generic Constraints

```typescript
function getProperty<T, K extends keyof T>(obj: T, key: K) {
  return obj[key];
}

const x = { a: 1, b: 2, c: 3, d: 4 };
getProperty(x, "a");
```

## Using Class Types in Generics

When creating factories in TypeScript using generics, it is necessary to refer to class types by their constructor functions.

```typescript
function create<T>(c: { new (): T }): T {
  return new c();
}
```

