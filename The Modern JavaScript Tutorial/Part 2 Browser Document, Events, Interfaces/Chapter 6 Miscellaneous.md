# Chapter 6: Miscellaneous

## 6.1 Mutation observer

`MutationObserver` is a built-in object that observes a DOM element and fires a callback in case of changes. It could react on any changes within DOM subtree.

### Syntax

```js
let observer = new MutationObserver(callback);

observer.observe(node, config);
```

`config` is an object with boolean options:

- `childList` – changes in the direct children of node.
- `subtree` – in all descendants of node.
- `attributes` – attributes of node.
- `attributeFilter` – an array of attribute names, to observe only selected ones.
- `characterData` – whether to observe `node.data` (text content).
- `attributeOldValue` – if true, pass both the old and the new value of attribute to callback.
- `characterDataOldValue` – if true, pass both the old and the new value of `node.data` to callback.

Changes are passed in the first argument as a list of `MutationRecord` objects, and the observer itself as the second argument.

`MutationRecord` object:

- `type` – mutation type
- - `attributes`: attribute modified
- - `characterData`: data modified, used for text nodes
- - `childList`: child elements added/removed
- `target` – where the change occurred: an element for `attributes`, or text node for `characterData`, or an element for a `childList` mutation
- `addedNodes`/`removedNodes` – nodes that were added/removed
- `previousSibling`/`nextSibling` – the previous and next sibling to added/removed nodes
- `attributeName`/`attributeNamespace` – the name/namespace (for XML) of the changed attribute
- `oldValue` – the previous value, only for attribute or text changes, if the corresponding option is set `attributeOldValue`/`characterDataOldValue`

```js
mutationRecords = [{
  type: "characterData",
  oldValue: "edit",
  target: <text node>
}];
```

### Additional methods

- `observer.disconnect()` – stops the observation
- `observer.takeRecords()` - gets a list of unprocessed mutation records which the callback did not handle.

Observers use weak references to nodes internally. If a node is removed from DOM, it becomes garbage collected.

## 6.2 Selection and Range

### Range

Range is a pair of “boundary points”: range start and range end.

Each point represented as a parent DOM node with the relative offset from its start.

```js
let range = new Range();
```

- `range.setStart(node, offset)`
- `range.setEnd(node, offset)` (not include the end index)

### Selecting parts of text nodes

```html
<p id="p">Example: <i>italic</i> and <b>bold</b></p>

<script>
    let range = new Range();

    range.setStart(p.firstChild, 2);
    range.setEnd(p.querySelector('b').firstChild, 3);
</script>
```

### Range object

#### properties

- `startContainer`, `startOffset` – node and offset of the start
- `endContainer`, `endOffset` – node and offset of the end
- `collapsed ` - `true` if the range starts and ends on the same point
- `commonAncestorContainer` - the nearest common ancestor of all nodes within the range

#### methods

- `setStart(node, offset)`/`setEnd(node, offset)`
- `setStartBefore(node)`/`setEndBefore(node)`
- `setStartAfter(node)`/`setEndAfter(node)`

- `selectNode(node)`
- `selectNodeContents(node)`
- `collapse(toStart)`
- `cloneRange()`

- `deleteContents()`
- `extractContents()` - remove range content from the document and return as `DocumentFragment`.
- `cloneContents()` - clone range content and return as `DocumentFragment`.
- `insertNode(node)` - insert node into the document at the beginning of the `range`.
- `surroundContents(node)` - wrap `node` around range content.

### Selection object

The document selection is represented by Selection object, that can be obtained as `window.getSelection()` or `document.getSelection()`.

A selection may include zero or more ranges. In practice, only Firefox allows to select multiple ranges in the document.

#### properties

A selection has a start, called `anchor`, and the end, called `focus`.

- `anchorNode`/`anchorOffset`
- `focusNode`/`focusOffset`
- `isCollapsed` – true if selection selects nothing.
- `rangeCount` – count of ranges in the selection, maximum 1 in all browsers except Firefox.

Selection end may be in the document before start. (Left-to-right direction)

#### events

- `elem.onselectstart` – when a selection starts on elem
- `document.onselectionchange` - whenever a selection changes

#### methods

- `getRangeAt(i)`
- `addRange(range)`
- `removeRange(range)`
- `removeAllRanges()`
- `empty()` – alias to `removeAllRanges`

To select, remove the existing selection first.

### Selection in form controls

Form elements, such as `input` and `textarea` provide special API for selection, without `Selection` or `Range` objects.

#### properties

- `input.selectionStart` – position of selection start (writeable)
- `input.selectionEnd `– position of selection end (writeable),
- `input.selectionDirection` – selection direction (`forward`, `backward`, `none`)

#### events

- `input.onselect`

#### methods

- `input.select()` - selects everything in the text control
- `input.setSelectionRange(start, end, [direction])`
- `input.setRangeText(replacement, [start], [end], [selectionMode])`
- - `select` – selects the newly inserted text
- - `start` - collapses before the start
- - `end` - collapses after the end
- - `preserve` - preserve the selection

### Making unselectable

- CSS property: `user-select: none`
- Prevent default action in `onselectstart` or `mousedown` events.
- `document.getSelection().empty()`

## 6.3 Event loop: microtasks and macrotasks

Browser JavaScript execution flow, as well as in Node.js, is based on an event loop.

### Event Loop

JavaScript engine does nothing most of the time, only runs if a script/handler/event activates.

- Dequeue and run the oldest task from the macrotask queue.
- Execute all microtasks:
- - While the microtask queue is not empty, dequeue and run the oldest microtask.
- Render changes if any.
- If the macrotask queue is empty, wait till a macrotask appears.

If a task comes while the engine is busy, it's enqueued into the macrotask queue. They are processed on "first come – first served" basis.

- Rendering never happens while the engine executes a task.
- If a task takes too long, it raises an alert like "Page Unresponsive" suggesting to kill the whole page.

### Use-case 1: splitting CPU-hungry tasks

We can split the big task into pieces. 

```js
function count() {
  // do a heavy job
  for (let j = 0; j < 1e9; j++) {
    i++;
  }

  // do a piece of the heavy job
  do {
    i++;
  } while (i % 1e6 != 0);

  if (i != 1e9) {
    setTimeout(count); // schedule the new call
  }
}
```

For long heavy calculations, we could use Web Workers in a parallet thread.

### Use case 2: progress indication

Another benefit of splitting heavy tasks for browser scripts is that we can show progress indication.

```html
<div id="progress"></div>
<script>
  let i = 0;
  function count() {
    // heavy job
  }
  count();
</script>
```

### Use case 3: doing something after the event

In an event handler we may decide to postpone some actions until the event bubbled up and was handled on all levels. We can do that by wrapping the code in zero delay `setTimeout`.

### Macrotasks and Microtasks

An execution of `.then/catch/finally` handler becomes a microtask.

`queueMicrotask(func)` queues func for execution in the microtask queue.

All microtasks are completed before any other event handling or rendering or any other macrotask takes place.

```js
setTimeout(() => alert("timeout"));

Promise.resolve().then(() => alert("promise"));

alert("code");
```

1. `code` - synchronous call
2. `promise` - microtask
3. `timeout` - macrotask

If we’d like to execute a function asynchronously (after the current code), but before changes are rendered or new events handled, we can schedule it with `queueMicrotask`.
