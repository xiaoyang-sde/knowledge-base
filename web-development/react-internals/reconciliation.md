# Reconciliation

## Motivation

`render()` function creates a tree of React elements. On the next state or props update, that function will return a different tree of React elements.

React implements a heuristic O(n) algorithm to figure out how to efficiently update the UI to match the updated tree. The algorithm is based on two assumptions:

- Two elements of different types will produce different trees.
- The `key` prop indicates which child elements are stable across different renders.

## The Diffing Algorithm

When diffing two trees, React first compares the two root elements. The behavior is different depending on the types of the root elements.

### Elements Of Different Types

Whenever the root elements have different types, React will tear down the old tree and build the new tree from scratch. (e.g. from `<Article>` to `<Comment>`)

- When tearing down a tree, old components are unmounted and have their state destroyed. (`componentWillUnmount()`)
- When building up a new tree, new components are mounted. (`UNSAFE_componentWillMount()`, `componentDidMount()`)

### DOM Elements Of The Same Type

When comparing two React DOM elements of the same type, React looks at the attributes of both and only updates the changed attributes.

After handling the DOM node, React then recurses on the children.

```jsx
<div className="before" title="stuff" />

<div className="after" title="stuff" />
```

### Component Elements Of The Same Type

When a component updates, the instance stays the same, so that state is maintained across renders.

React updates the props of the underlying component instance to match the new element. Then, the `render()` method is called and the diff algorithm recurses on the previous result and the new result.

### Recursing On Children

By default, React just iterates over both lists of children at the same time and generates a mutation whenever there’s a difference.

### Keys

In order to make recursing more efficient, React supports a `key` attribute. When children have keys, React uses the key to match children in the original tree with children in the subsequent tree.

```jsx
// Old Tree
<ul>
  <li key="2015">Duke</li>
  <li key="2016">Villanova</li>
</ul>

// New Tree
<ul>
  <li key="2014">Connecticut</li>
  <li key="2015">Duke</li>
  <li key="2016">Villanova</li>
</ul>
```

- The key only has to be unique among its siblings, not globally unique.
- The key should be stable, predictable, and unique.
- Unstable keys (e.g. `Math.random()`) will cause many component instances and DOM nodes to be unnecessarily recreated.
