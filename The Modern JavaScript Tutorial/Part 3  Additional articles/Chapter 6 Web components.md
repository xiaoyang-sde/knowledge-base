# Chapter 6: Web components

## 6.1 From the orbital height

This section describes a set of modern standards for “web components”.

### Component architecture

A good architect is the one who can make the complex simple.

We can split user interface into visual components: each of them has own place on the page, can “do” a well-described task, and is separate from the others.

A component has:
- DOM structure: managed solely by its class, outside code doesn’t access it (encapsulation).
- CSS styles: applied to the component.
- API: events to interact with other components.

- Custom elements: Define custom HTML elements.
- Shadow DOM: Create an internal DOM for the component, hidden from the others.
- CSS Scoping: Declare styles that only apply inside the Shadow DOM of the component.
- Event retargeting: Make custom components better fit the development.

## 6.2 Custom elements

We can create custom HTML elements, described by our class, with its own methods and properties, events and so on.

- `Autonomous custom elements`: "all-new" elements, extending the abstract `HTMLElement` class.
- `Customized built-in elements`: extending built-in elements, like a customized button, based on `HTMLButtonElement` etc.

### Autonomous elements

```js
class MyElement extends HTMLElement {
  constructor() {
    super();
    // element created
  }

  connectedCallback() {
    // browser calls this method when the element is added to the document
  }

  disconnectedCallback() {
    // browser calls this method when the element is removed from the document
  }

  static get observedAttributes() {
    return [/* array of attribute names to monitor for changes */];
  }

  attributeChangedCallback(name, oldValue, newValue) {
    // called when one of attributes listed above is modified
  }

  adoptedCallback() {
    // called when the element is moved to a new document
    // (happens in document.adoptNode, very rarely used)
  }
}
```

To register the element:

```js
customElements.define("my-element", MyElement);
```

Custom element name must contain a hyphen `-`.

### Example: “time-formatted”

```js
class TimeFormatted extends HTMLElement {

  connectedCallback() {
    let date = new Date(this.getAttribute('datetime') || Date.now());

    this.innerHTML = new Intl.DateTimeFormat("default", {
      year: this.getAttribute('year') || undefined,
      month: this.getAttribute('month') || undefined,
      day: this.getAttribute('day') || undefined,
      hour: this.getAttribute('hour') || undefined,
      minute: this.getAttribute('minute') || undefined,
      second: this.getAttribute('second') || undefined,
      timeZoneName: this.getAttribute('time-zone-name') || undefined,
    }).format(date);
  }

}

customElements.define("time-formatted", TimeFormatted);
```

### Custom elements upgrade

If the browser encounters any customzied elements before `customElements.define`, that’s not an error. 

When `customElement.define` is called, they are upgraded.

### Rendering in connectedCallback, not in constructor

Element content is rendered in `connectedCallback`.

When `constructor` is called, the element is created, but the browser did not yet process/assign attributes.

The `connectedCallback` triggers when the element is added to the document. Not just appended to another element as a child, but actually becomes a part of the page. Thus we can build detached DOM, create elements and prepare them for later use. 

### Observing attributes

We can observe attributes by providing their list in `observedAttributes()` static getter. `attributeChangedCallback` is called when they are modified.

### Rendering order

When HTML parser builds the DOM, elements are processed one after another, parents before children.

```js
connectedCallback() {
    alert(this.innerHTML); // empty because its children are not renderred
}
```

We can defer access to the children with zero-delay setTimeout.

### Customized built-in elements

Customized elements don’t have any associated semantics. They are unknown to search engines, and accessibility devices can’t handle them.

However, we can extend and customize built-in HTML elements by inheriting from their classes.

```js
class HelloButton extends HTMLButtonElement { /* custom element methods */ }

customElements.define('hello-button', HelloButton, {extends: 'button'});
```

```html
<button is="hello-button">...</button>
```

## 6.3 Shadow DOM

Shadow DOM serves for encapsulation.

### Built-in shadow DOM

```html
<input type="range">
```

The browser uses DOM/CSS internally to draw it.

We can’t get built-in shadow DOM elements by regular JavaScript calls or selectors.

### Shadow tree

A DOM element can have two types of DOM subtrees:

- Light tree: a regular DOM subtree, made of HTML children.
- Shadow tree: a hidden DOM subtree, not reflected in HTML.

Shadow tree can be used in Custom Elements to hide component internals and apply component-local styles.

```js
connectedCallback() {
    const shadow = this.attachShadow({mode: 'open'});
    shadow.innerHTML = `<p>
        Hello, ${this.getAttribute('name')}
    </p>`;
}
```

We can create only one shadow root per element.

The `mode` option sets the encapsulation level.
- `open`: the shadow root is available as `elem.shadowRoot`.
- `close`: `elem.shadowRoot` is always null. We can only access the shadow DOM by the reference returned by `attachShadow`.

### Encapsulation

- Not visible to `querySelector` from the light DOM.
- Style rules from the outer DOM don’t get applied.

## 6.4 Template element

A built-in `<template>` element serves as a storage for HTML markup templates.

```html
<template>
  <tr>
    <td>Contents</td>
  </tr>
</template>
```

- `<template>` content can be any syntactically correct HTML.
- `<template>` content is considered out of the document.
- We can access `template.content` to clone it to reuse in a new component.

### Inserting template

The template content is available in its content property as a `DocumentFragment`.

When we insert it somewhere, its children are inserted instead.

```html
<template id="tmpl">
  <script>
    alert("Hello");
  </script>
  <div class="message">Hello, world!</div>
</template>

<script>
  let elem = document.createElement('div');
  elem.append(tmpl.content.cloneNode(true));
  document.body.append(elem);
</script>
```

## 6.5 Shadow DOM slots, composition

```html
<custom-menu>
  <title>Candy menu</title>
  <item>Lollipop</item>
  <item>Fruit Toast</item>
  <item>Cup Cake</item>
</custom-menu>
```

Shadow DOM supports `<slot>` elements, that are automatically filled by the content from light DOM.

### Named slots


## 6.6 Shadow DOM styling



## 6.7 Shadow DOM and events


