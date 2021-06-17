# Strict Mode

- `StrictMode` is a tool for highlighting potential problems in an application.
- `StrictMode` does not render any visible UI.
- `StrictMode` only checks its descendants.

```jsx
const ExampleApplication = () => (
  <div>
    <Header />
    <React.StrictMode>
      <ComponentOne />
      <ComponentTwo />
    </React.StrictMode>
    <Footer />
  </div>
);
```

## Identifying unsafe lifecycles

Certain legacy lifecycle methods are unsafe for use in async React applications. When strict mode is enabled, React logs a warning message with information about the components using the unsafe lifecycles.

## Detecting legacy context API

The legacy context API is error-prone, and will be removed in a future major version.
