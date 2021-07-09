# TypeScript Integration

## Typing Component Props

A list of TypeScript types that will likely be used in a React + TypeScript app:

```tsx
export declare interface AppProps = {
  names: string[];
  obj: object;
  objArr: {
    id: string;
  }[];
  dict: Record<string, string>;

  children: React.ReactNode;
  props: Props & React.ComponentPropsWithoutRef<"button">;
  functionChildren: (name: string) => React.ReactNode;
  style: React.CSSProperties;
  onChange: React.FormEventHandler<HTMLInputElement>;
  onClick(event: React.MouseEvent<HTMLButtonElement>): void;
}
```

### JSX.Element vs React.ReactNode

- `JSX.Element`: return value of `React.createElement` (`React.createElement` always returns an object)
- `React.ReactNode`: return value of a component

### Types or Interfaces

Use Interface until you need Type. Types are useful for union types (e.g. `type MyType = TypeA | TypeB`) whereas Interfaces are better for declaring dictionary shapes and then implementing or extending them.

## Function Components

These can be written as normal functions that take a `props` argument and return a JSX element.

```tsx
type AppProps = {
  message: string;
};

const App = (
  { message }: AppProps
): JSX.Element => {
  return (
    <div>
      {message}
    </div>
  );
};
```

`React.FC` provides an implicit definition of children, even if the component doesn't need to have children, which might cause an error.

## Hooks

### useState

Type inference works very well for simple values:

```tsx
const [val, toggle] = React.useState(false);

const [user, setUser] = React.useState<IUser | null>(null);
```

### useReducer

```tsx
const initialState = {
  count: 0,
};

type ACTION_TYPE =
  | { type: "increment"; payload: number }
  | { type: "decrement"; payload: string };

const reducer = (
  state: typeof initialState,
  action: ACTION_TYPE,
) => {
  switch (action.type) {
    case "increment":
      return { count: state.count + action.payload };
    case "decrement":
      return { count: state.count - Number(action.payload) };
    default:
      throw new Error();
  }
}
```

### useEffect

```tsx
const App = (
  { message }: AppProps
): JSX.Element => {
  useEffect(() => {
    setTimeout(() => {
      /* do stuff */
    }, timerMs);
  }, [timerMs]);

  return null;
}
```

## Context

```tsx
interface AppContextInterface {
  name: string;
  author: string;
  url: string;
}

const AppCtx = React.createContext<AppContextInterface | null>(null);

export const App = () => (
  <AppCtx.Provider value={sampleAppContext}>...</AppCtx.Provider>
);

export const PostInfo = () => {
  const appContext = React.useContext(AppCtx);
  return (
    <div>
      Name: {appContext.name}, Author: {appContext.author}, Url:{" "}
      {appContext.url}
    </div>
  );
};
```
