# Fragments

Fragments let you group a list of children without adding extra nodes to the DOM.

```jsx
const Columns = () => (
  <React.Fragments>
    <td>Hello</td>
    <td>World</td>
  </React.Fragments>
);
```

## Short Syntax

The short syntax doesn't support keys or attributes.

```jsx
const Columns = () => (
  <>
    <td>Hello</td>
    <td>World</td>
  </>
);
```

## Keyed Fragments

Fragments declared with the explicit `<React.Fragment>` syntax may have keys. `key` is the only attribute that can be passed to `Fragment`.

```jsx
const Columns = (props) => (
  <dl>
    {props.items.map(item => (
      <React.Fragment key={item.id}>
        <dt>{item.term}</dt>
      </React.Fragment>
    ))}
  </dl>
);
