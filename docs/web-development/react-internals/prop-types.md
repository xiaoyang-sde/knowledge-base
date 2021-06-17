# Type Checking With PropTypes

React has some built-in typechecking abilities for props of a component. We could assign the `propTypes` property. When an invalid value is provided for a prop, a warning will be shown in the JavaScript console.

```jsx
import PropTypes from 'prop-types';

const Greeting = ({ name }) => <h1>Hello, {name}</h1>;

Greeting.propTypes = {
  name: PropTypes.string
};

export default Greeting;
```

## PropTypes (Example)

```jsx
MyComponent.propTypes = {
  optionalArray: PropTypes.array,
  optionalFunc: PropTypes.func,
  optionalNumber: PropTypes.number,

  optionalEnum: PropTypes.oneOf(['News', 'Photos']),
  // Array with values of specific type
  optionalArrayOf: PropTypes.arrayOf(PropTypes.number),

  // This prop could be one of many types
  optionalUnion: PropTypes.oneOfType([
    PropTypes.string,
    PropTypes.number,
    PropTypes.instanceOf(Message)
  ]),

  // Numbers, strings, elements or fragment containing these types
  optionalNode: PropTypes.node,
  // React element
  optionalElement: PropTypes.element,

  optionalObjectWithShape: PropTypes.shape({
    color: PropTypes.string,
    fontSize: PropTypes.number
  }),

  // Chain any of the above with `isRequired`
  requiredFunc: PropTypes.func.isRequired
};
```

## Requiring Single Child

With `PropTypes.element` you can specify that only a single child can be passed to a component as children.

```js
const MyComponent = ({ children }) => {
  return (
    <div>{children}</div>
  );
};

MyComponent.propTypes = {
  children: PropTypes.element.isRequired
};
```

## Default Prop Values

You can define default values for your props by assigning to the special `defaultProps` property.

```jsx
const Greeting = ({ name }) => <h1>Hello, {name}</h1>;

Greeting.propTypes = {
  name: PropTypes.string
};

Greeting.defaultProps = {
  name: 'Stranger'
};
```
