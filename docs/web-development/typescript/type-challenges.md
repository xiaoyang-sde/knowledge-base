# Type Challenges

Solutions of [type-challenges](https://github.com/type-challenges/type-challenges).

## Easy

### Pick

Implement the built-in `Pick<T, K>` generic without using it. Constructs a type by picking the set of properties `K` from `T`

```ts
type MyPick<T, K extends keyof T> = {
  [Key in K]: T[Key]
}
```

### Readonly

Implement the built-in `Readonly<T>` generic without using it. Constructs a type with all properties of `T` set to `readonly`, meaning the properties of the constructed type cannot be reassigned.

```ts
type MyReadonly<T> = {
  readonly [Key in keyof T]: T[Key]
}
```

### Tuple to Object

Given an array, transform to a object type and the key/value must in the given array.

```ts
type TupleToObject<T extends readonly string[]> = {
  [Key in T[number]]: Key
};
```

### First of Array

Implement a generic `First<T>` that takes an Array `T` and returns it's first element's type.

```ts
type First<T extends any[]> = T extends [] ? never : T[0];
```

### Length of Tuple

For given a tuple, you need create a generic `Length`, pick the length of the tuple.

```ts
type Length<T extends readonly any[]> = T['length'];
```

### Exclude

Implement the built-in `Exclude<T, U>`. Exclude from `T` those types that are assignable to `U`.

```ts
type MyExclude<T, U> = T extends U ? never : T;
```

### If

Implement a utils If which accepts condition `C`, a truthy return type `T`, and a falsy return type `F`. `C` is expected to be either `true` or `false` while `T` and `F` can be any type.

```ts
type If<C extends boolean, T, F> = C extends true ? T : F;
```

### Includes

Implement the JavaScript `Array.includes` function in the type system. A type takes the two arguments. The output should be a boolean `true` or `false`.

```ts
type Includes<T extends readonly any[], U> = any;
```

### Awaited

If we have a type which is wrapped type like Promise. How we can get a type which is inside the wrapped type? For example if we have `Promise<ExampleType>` how to get `ExampleType`?

```ts
type Awaited<T extends Promise<unknown>> = T extends Promise<infer R> ? R : unknown;
```

### Concat

Implement the JavaScript `Array.concat` function in the type system. A type takes the two arguments. The output should be a new array that includes inputs in ltr order.

```ts
type Concat<T extends any[], U extends any[]> = [...T, ...U];
```

## Medium

### Get Return Type

Implement the built-in `ReturnType<T>` generic without using it.

```ts
type MyReturnType<T> = T extends (...args: any[]) => infer R ? R : unknown;
```

### Omit

Implement the built-in `Omit<T, K>` generic without using it.

```ts
type MyOmit<T, K extends keyof T> = {
  [Key in Exclude<keyof T, K>]: T[Key];
};

type MyOmit<T, K extends keyof T> = Pick<T, Exclude<keyof T, K>>;
```

### Readonly 2

Implement a generic `MyReadonly2<T, K>` which takes two type argument `T` and `K`. `K` specify the set of properties of `T` that should set to Readonly. When `K` is not provided, it should make all properties readonly just like the normal `Readonly<T>`.

```ts
type MyReadonly2<T, K extends keyof T = keyof T> = T & {
  readonly [Key in K]: T[Key];
};
```

### Deep Readonly

Implement a generic `DeepReadonly<T>` which make every parameter of an object - and its sub-objects recursively - readonly.

You can assume that we are only dealing with Objects in this challenge. Arrays, Functions, Classes and so on are no need to take into consideration. However, you can still challenge your self by covering different cases as many as possbile.

```ts
type DeepReadonly<T> = {
  readonly [K in keyof T]: keyof T[K] extends never ? T[K] : DeepReadonly<T[K]>;
};
```

### Tuple to Union

Implement a generic `TupleToUnion<T>` which covers the values of a tuple to its values union.

```ts
type TupleToUnion<T extends unknown[]> = T[number];
```

### Last of Array

Implement a generic `Last<T>` that takes an Array `T` and returns it's last element's type.

```ts
type Last<T extends any[]> = T extends [...infer _placeholder, infer L] ? L : never;
```

### Pop

Implement a generic `Pop<T>` that takes an Array `T` and returns an Array without it's last element.

```ts
type Pop<T extends any[]> = T extends [...infer R, infer _placeholder] ? R : never;
```

### Type Lookup

In this challenge, we would like to get the corresponding type by searching for the common `type` field in the union `Cat | Dog`. In other words, we will expect to get `Dog` for `LookUp<Dog | Cat, 'dog'>` and `Cat` for `LookUp<Dog | Cat, 'cat'>` in the following example.

```ts
type LookUp<U, T> = U extends { type: T } ? U : never;
```

### Trim Left

Implement `TrimLeft<T>` which takes an exact string type and returns a new string with the whitespace beginning removed.

```ts
type TrimLeft<S extends string> = S extends `${' ' | '\n' | '\t' }${infer T}` ? TrimLeft<T> : S;
```

### Trim

Implement `Trim<T>` which takes an exact string type and returns a new string with the whitespace from both ends removed.

```ts
type Trim<S extends string> = S extends `${' ' | '\n' | '\t'}${infer R}` ? Trim<R> : S extends `${infer R}${' ' | '\n' | '\t'}` ? Trim<R> : S;
```

### Capitalize

Implement `Capitalize<T>` which converts the first letter of a string to uppercase and leave the rest as-is.

```ts
type Capitalize<S extends string> = S extends `${infer C}${infer R}` ? `${Uppercase<C>}${R}` : S;
```

### Flatten

In this challenge, you would need to write a type that takes an array and emitted the flatten array type.

```ts
type Flatten<T> = T extends [infer L, ...infer R] ? L extends unknown[] ? [...Flatten<L>, ...Flatten<R>] : [L, ...Flatten<R>] : T;
```
