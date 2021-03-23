# Context

Context provides a way to pass data through the component tree without having to pass props down manually at every level.

## When to Use Context

Context is designed to share data that can be considered “global” for a tree of React components, such as the current authenticated user, theme, or preferred language.

```jsx
const ThemeContext = React.createContext('light');

class App extends React.Component {
  render() {
    // Use a Provider to pass the current theme to the tree below.
    // In this example, we're passing "dark" as the current value.
    return (
      <ThemeContext.Provider value="dark">
        <Toolbar />
      </ThemeContext.Provider>
    );
  }
}

class Tool extends React.Component {
  // Assign a contextType to read the current theme context.
  // React will find the closest theme Provider above and use its value.
  // In this example, the current theme is "dark".
  static contextType = ThemeContext;
  render() {
    return <Button theme={this.context} />;
  }
}
```

## API

### React.createContext

```jsx
const MyContext = React.createContext(defaultValue);
```

Creates a Context object. When React renders a component that subscribes to this Context object it will read the current context value from the closest matching `Provider` above it in the tree.

The defaultValue argument is only used when a component does not have a matching `Provider` above it in the tree.

### Context.Provider

```jsx
<MyContext.Provider value={/* some value */}>
```

Every Context object comes with a `Provider` React component that allows consuming components to subscribe to context changes.

- The Provider component accepts a `value` prop to be passed to consuming components that are descendants of this `Provider`.
- `Provider` can be connected to many consumers.
- `Provider` can be nested to override values deeper within the tree.
- All consumers that are descendants of `Provider` will re-render whenever the `value` prop changes.

### Class.contextType

The `contextType` property on a class can be assigned a `Context` object created by `React.createContext()`. Using this property lets you consume the nearest current value of that `Context` type using `this.context`.

```jsx
class MyClass extends React.Component {
  static contextType = MyContext;
  render() {
    const value = this.context;
  }
}
```

### Context.Consumer

Using the `Context.Consumer` component lets you subscribe to a context within a function component. The function inside the component receives the current context value and returns a React node.

```jsx
<MyContext.Consumer>
  {value => <p>Context: {value}</p>}
</MyContext.Consumer>
```

### Context.displayName

The `displayName` string property will be used by React DevTools to determine what to display for the context.

```jsx
const MyContext = React.createContext(/* some value */);
MyContext.displayName = 'MyDisplayName';

<MyContext.Provider> // "MyDisplayName.Provider" in DevTools
<MyContext.Consumer> // "MyDisplayName.Consumer" in DevTools
```
