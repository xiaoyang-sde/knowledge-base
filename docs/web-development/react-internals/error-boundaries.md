# Error Boundaries

Error boundaries are React components that catch JavaScript errors anywhere in their child component tree, log those errors, and display a fallback UI instead of the component tree that crashed.

A class component becomes an error boundary if it defines either (or both) of the lifecycle methods `static getDerivedStateFromError()` or `componentDidCatch()`.

- `static getDerivedStateFromError()`: renders a fallback UI after an error has been thrown
- `componentDidCatch()`: logs error information

```tsx
class ErrorBoundary extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      hasError: false
    };
  }

  static getDerivedStateFromError(error) {
    // Update state so the next render will show the fallback UI.
    return { hasError: true };
  }

  componentDidCatch(error, errorInfo) {
    // Log the error to an error reporting service
    logErrorToMyService(error, errorInfo);
  }

  render() {
    if (this.state.hasError) {
      // Render any custom fallback UI
      return <h1>Something went wrong.</h1>;
    }

    return this.props.children;
  }
}

// Usage of ErrorBoundary

const Component = () => (
  <ErrorBoundary>
    <MyWidget />
  </ErrorBoundary>
);
```

Error boundaries do not catch errors for:

- Event handlers
- Asynchronous code (e.g. `setTimeout`)
- Server side rendering
- Errors thrown in the error boundary itself
