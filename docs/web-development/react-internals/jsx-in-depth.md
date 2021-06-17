# JSX In Depth

JSX is a syntactic sugar for `React.createElement(component, props, ...children)`

```jsx
<MyButton color="blue" shadowSize={2}>
  Click Me
</MyButton>

React.createElement(
  MyButton,
  {color: 'blue', shadowSize: 2},
  'Click Me'
);
```

## Specifying The React Element Type

The first part of a JSX tag determines the type of the React element.

### User-Defined Components Must Be Capitalized

The first part of a JSX tag determines the type of the React element.

- Capitalized: React component
- Uncapitalized: HTML tag (e.g. `<div>`, `<span>`)

### React Must Be in Scope

Since JSX compiles into calls to `React.createElement`, the `React` library must also always be in scope from your JSX code.

### Choosing the Component at Runtime

To render a different component based on a prop, assign the component type to a capitalized variable first.

```jsx
const components = {
  photo: PhotoStory,
  video: VideoStory
};

const Story = ({ storyType }) => {
  const SpecificStory = components[storyType];
  return <SpecificStory story={props.story} />;
};
```

## Props in JSX

### JavaScript Expressions as Props

You can pass any JavaScript expression as a prop, by surrounding it with `{}`: `<MyComponent foo={1 + 2 + 3 + 4} />`. `if` statements and `for` loops are not expressions in JavaScript.

### Props Default to "True"

If you pass no value for a prop, it defaults to `true`. Therefore, `<MyTextBox autocomplete />` is equivalent to `<MyTextBox autocomplete={true} />`.

### Spread Attributes

If you already have `props` as an object, and you want to pass it in JSX, you can use `...` to pass the whole props object.

```jsx
const App1 = () => <Greeting firstName="Ben" lastName="Hector" />;

const App2 = () => {
  const props = {
    firstName: 'Ben',
    lastName: 'Hector'
  };
  return <Greeting {...props} />;
};
```

## Children in JSX

In JSX expressions that contain both an opening tag and a closing tag, the content between those tags is passed as a special prop: `props.children`.

### Booleans, Null, and Undefined Are Ignored

`false`, `null`, `undefined`, and `true` are valid children. They simply don’t render. To make them appear in the output, convert them to string first.
