# React Hooks

## State Hook

```jsx
import React, { useState } from 'react';

const Example = () => {
  const [count, setCount] = useState(0);

  return (
    <>
      <p>Count: {count}</p>
      <button onClick={() => setCount(count + 1)}>
        Increment
      </button>
    </>
  );
};
```

## Effect Hook

### Effects Without Cleanup

We want to run some additional code after React has updated the DOM. In React classes, we put side effects into `componentDidMount` and `componentDidUpdate`.

```jsx
class Example extends React.Component {
  ...
  componentDidMount() {
    document.title = `You clicked ${this.state.count} times`;
  }

  componentDidUpdate() {
    document.title = `You clicked ${this.state.count} times`;
  }
  ...
}
```

Since in this case we want to perform the same side effect regardless of whether the component just mounted, we have to duplicate the code between these two lifecycle methods.

This problem could be solved with the `useEffect` hook. React will remember the function you passed and call it both after the first render and after every update.

```jsx
import React, { useState, useEffect } from 'react';

const Example = () => {
  ...
  useEffect(() => {
    document.title = `You clicked ${count} times`;
  });
  ...
};
```

Placing `useEffect` inside the component lets us access the state variables or props right from the effect.

### Effects with Cleanup

Some side effects may require a cleanup. In a React class, you would typically clean an effect in `componentWillUnmount`. Lifecycle methods force us to split this logic even though conceptually code in both of the methods (`componentDidMount`, `componentWillUnmount`) is related to the same effect.

```jsx
class Example extends React.Component {
  ...
  componentDidMount() {
    ChatAPI.subscribeToFriendStatus(
      this.props.friend.id,
      this.handleStatusChange
    );
  }

  componentWillUnmount() {
    ChatAPI.unsubscribeFromFriendStatus(
      this.props.friend.id,
      this.handleStatusChange
    );
  }

  handleStatusChange(status) {
    this.setState({
      isOnline: status.isOnline
    });
  }
  ...
}
```

`useEffect` is designed to keep the logics of adding and removing effects together. Every effect may return a function that cleans up after it.

```jsx
const FriendStatus = (props) => {
  ...
  useEffect(() => {
    ChatAPI.subscribeToFriendStatus(props.friend.id, handleStatusChange);

    // Specify how to clean up after this effect
    return () => {
      ChatAPI.unsubscribeFromFriendStatus(props.friend.id, handleStatusChange);
    };
  });
  ...
}
```

Since effects run for every render and not just once, React performs the cleanup before running the effects next time and when the component unmounts.

### Tips for Using Effects

#### Use Multiple Effects to Separate Concerns

Hooks let us split the code based on what it is doing rather than a lifecycle method name. React will apply every effect used by the component, in the order they were specified.

#### Optimizing Performance by Skipping Effects

In some cases, cleaning up or applying the effect after every render might create a performance problem.

In class components, we can solve this by writing an extra comparison with `prevProps` or `prevState` inside `componentDidUpdate`.

```jsx
componentDidUpdate(prevProps, prevState) {
  if (prevState.count !== this.state.count) {
    document.title = `You clicked ${this.state.count} times`;
  }
}
```

For the Hook API, pass an array as an optional second argument to `useEffect`, and React will skip applying an effect if these values haven’t changed between re-renders.

```jsx
useEffect(() => {
  document.title = `You clicked ${count} times`;
}, [count]); // Only re-run the effect if count changes

useEffect(() => {
  ChatAPI.subscribeToFriendStatus(props.friend.id, handleStatusChange);

  return () => {
    ChatAPI.unsubscribeFromFriendStatus(props.friend.id, handleStatusChange);
  };
}, [props.friend.id]); // Only re-subscribe if props.friend.id changes
```

Make sure the array includes all values from the component scope that change over time and that are used by the effect.

If you want to run an effect and clean it up only once (on mount and unmount), you can pass an empty array (`[]`) as a second argument.

## Rules of Hooks

- Only Call Hooks at the Top Level - Don’t call Hooks inside loops, conditions, or nested functions.
- Only Call Hooks from React Functions: Components or custom Hooks)
