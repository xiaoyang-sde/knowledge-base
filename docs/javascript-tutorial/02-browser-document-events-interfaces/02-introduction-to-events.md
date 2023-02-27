# Chapter 2: Introduction to Events

## 2.1 Introduction to browser events

An event is a signal that something has happened. All DOM nodes generate such signals.

#### Mouse events

* `click` – when the mouse clicks on an element \(touchscreen devices generate it on a tap\).
* `contextmenu` – when the mouse right-clicks on an element.
* `mouseover` / `mouseout` – when the mouse cursor comes over / leaves an element.
* `mousedown` / `mouseup` – when the mouse button is pressed / released over an element.
* `mousemove` – when the mouse is moved.

#### Keyboard events

* `keydown` and `keyup` – when a keyboard key is pressed and released.

#### Form element events

* `submit` – when the visitor submits a `<form>`.
* `focus` – when the visitor focuses on an element.

#### Document events

`DOMContentLoaded` – when the HTML is loaded and processed, DOM is fully built.

#### CSS events

`transitionend` – when a CSS-animation finishes.

### Event handlers

There are several ways to assign a handler.

#### HTML-attribute

A handler can be set in HTML with an attribute named `on<event>`.

```markup
<input value="Click me" onclick="alert('Click!')" type="button">
<input type="button" onclick="countRabbits()" value="Count rabbits!">
```

#### DOM property

```markup
<input id="elem" type="button" value="Click me">
<script>
  elem.onclick = function() {
    alert('Thank you');
  };
</script>
```

* As there’s only one onclick property, we can’t assign more than one event handler.
* To remove a handler, set it to `null`.
* The value of `this` inside a handler is the element.
* Don’t use setAttribute for handlers, since such a call won't work.

### addEventListener

To add multiple handlers to an event:

```javascript
element.addEventListener(event, handler, [options]);

element.removeEventListener(event, handler, [options]);
```

* `event`: event name
* `handler`: function
* `options`: an additional object
* * `once`: the listener is automatically removed after it triggers
* * `capture`: the phase where to handle the event
* * `passive`: the handler will not call `preventDefault()`

To remove a listener, we need to use the same function. If we don’t store the function in a variable, then we can’t remove it.

### Event object

When an event happens, the browser creates an event object with details and pass that to the handler.

```javascript
elem.onclick = function(event) {
// show event type, element and coordinates of the click
    alert(event.type + " at " + event.currentTarget);
    alert("Coordinates: " + event.clientX + ":" + event.clientY);
};
```

* `event.type`
* `event.currentTarget`: Element that handled the event.
* `event.clientX / event.clientY`: Window-relative coordinates of the cursor, for pointer events.

### Object handlers: handleEvent

We can pass an object or a class to the `addEventListener`. When an event occurs, its `handleEvent` method is called.

```javascript
let obj = {
    handleEvent(event) {
        alert(event.type + " at " + event.currentTarget);
    }
};

elem.addEventListener('click', obj);
```

## 2.2 Bubbling and capturing

### Bubbling

When an event happens on an element, it first runs the handlers on it, then on its parent, then all the way up on other ancestors.

### event.target

The most deeply nested element that caused the event is called a target element, accessible as `event.target`.

* `this (=event.currentTarget)`: the current element, the one that has a currently running handler on it.
* `event.target`: the target element that initiated the event, it doesn’t change through the bubbling process.

### Stopping bubbling

```javascript
event.stopPropagation();
event.stopImmediatePropagation(); // stop other handlers on the current element
```

In this example, the `body.onclick` won't work:

```markup
<body onclick="alert(`the bubbling doesn't reach here`)">
  <button onclick="event.stopPropagation()">Click me</button>
</body>
```

### Capturing

Three event phases:

* Capturing phase – the event goes down from the ancestors chain to the element.
* Target phase – the event reached the target element.
* Bubbling phase – the event bubbles up from the element.

To enable capturing:

```javascript
elem.addEventListener(..., {capture: true});
elem.addEventListener(..., true); // alias
```

Listeners on same element and same phase run in their set order.

