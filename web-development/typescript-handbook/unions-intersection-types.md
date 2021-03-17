# Unions and Intersectoin Types

Intersection and Union types are one of the ways in which you can compose types.

## Union Types

We can use a union type in a function to support different type of parameters.

```typescript
function padLeft(value: string, padding: string | number) {
  ...
}
```

We use the vertical bar \(`|`\) to separate each type, so `number | string | boolean` is the type of a value that can be a `number`, a `string`, or a `boolean`.

## Unions with Common Fields

If we have a value that is a union type, we can only access members that are common to all types in the union.

```typescript
interface Bird {
  fly(): void;
  layEggs(): void;
}

interface Fish {
  swim(): void;
  layEggs(): void;
}

declare function getSmallPet(): Fish | Bird;
const pet = getSmallPet();
pet.layEggs();

// Error: Property 'swim' does not exist on type 'Bird | Fish'.
pet.swim();
```

If a value has the type `A | B`, we only know for certain that it has members that both `A` and `B` have.

## Discriminating Unions

A common technique for working with unions is to have a single field which uses literal types which you can use to let TypeScript narrow down the possible current type.

```typescript
type NetworkLoadingState = {
  state: "loading";
};

type NetworkFailedState = {
  state: "failed";
  code: number;
};

type NetworkSuccessState = {
  state: "success";
  response: {
    title: string;
    duration: number;
    summary: string;
  };
};

type NetworkState =
  | NetworkLoadingState
  | NetworkFailedState
  | NetworkSuccessState;
```

All of the above types have a field named `state`, so we can compare the value of `state` to the equivalent string and TypeScript will know which exact type is used.

```typescript
function logger(state: NetworkState): string {
  swtich (state.state) {
    case "loading":
      return "Downloading...";
    case "failed":
      // The type must be NetworkFailedState here,
      // so accessing the `code` field is safe
      return `Error ${state.code} downloading`;
  }
}
```

## Union Exhaustiveness checking

We would like the compiler to tell us when we don’t cover all variants of the discriminated union.

* Turn on `--strictNullChecks` and specify a return type.
* Use the `never` type that the compiler uses to check for exhaustiveness.

```typescript
function assertNever(x: never): never {
  throw new Error("Unexpected object: " + x);
}

function logger(s: NetworkState): never {
  switch (s.state) {
    ...
    default:
      return assertNever(s);
  }
}
```

## Intersection Types

An intersection type combines multiple types into one. This allows you to add together existing types to get a single type that has all the features you need.

For example, `Person & Serializable & Loggable` is a type which is all of `Person` and `Serializable` and `Loggable`. That means an object of this type will have all members of all three types.

If you had networking requests with consistent error handling then you could separate out the error handling into its own type which is merged with types which correspond to a single response type.

```typescript
interface ErrorHandling {
  success: boolean;
  error?: { message: string };
}

interface ArtworksData {
  artworks: { title: string }[];
}

interface ArtistsData {
  artists: { name: string }[];
}

type ArtworksResponse = ArtworksData & ErrorHandling;
type ArtistsResponse = ArtistsData & ErrorHandling;
```

