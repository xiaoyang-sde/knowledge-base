# Chapter 1: Document

## Browser environment, specs

A host environment provides own objects and functions additional to the language core.
- Web browsers give a means to control web pages. 
- Node.js provides server-side features.

In browsers, there’s a “root” object called `window`.

- It is a global object for JavaScript code.
- It represents the “browser window” and provides methods to control it.

### DOM (Document Object Model)

Document Object Model, or DOM for short, represents all page content as objects that can be modified.

For example:

```js
// change the background color to red
document.body.style.background = "red";

// change it back after 1 second
setTimeout(() => document.body.style.background = "", 1000);
```

### BOM (Browser Object Model)

The Browser Object Model (BOM) represents additional objects provided by the browser (host environment) for working with everything except the document.

- The `navigator` object provides background information about the browser and the operating system. Example: `navigator.userAgent`, `navigator.platform`
- The `location` object allows us to read the current URL and can redirect the browser to a new one.

## 1.2 DOM tree

The backbone of an HTML document is tags.

According to the DOM, every HTML tag is an object. Nested tags are “children” of the enclosing one. The text inside a tag is an object as well.

### DOM structure

Every tree node is an object.

Spaces and newlines are totally valid characters, like letters and digits. They are a part of the DOM.

- Spaces and newlines before `<head>` are ignored for historical reasons.
- If we put something after `</body>`, then that is automatically moved inside the body, at the end, as the HTML spec requires that all content must be inside `<body>`.

### Autocorrection

If the browser encounters malformed HTML, it automatically corrects it when making the DOM.

- The browser will add `<html>`, `<head>`, `<body>` if they don't exist.
- A document with unclosed tags will become a normal DOM as the browser reads tags and restores the missing parts.

### Other node types

Everything in HTML, even comments, becomes a part of the DOM.

- `document` – the “entry point” into DOM.
- `element nodes` – HTML-tags, the tree building blocks.
- `text nodes` – contain text.
- `comments` – sometimes we can put information there, it won’t be shown, but JS can read it from the DOM.

## 1.3 Walking the DOM

All operations on the DOM start with the document object. That’s the main “entry point” to DOM.

### On top: documentElement and body

- `<html>` = `document.documentElement`
- `<body>` = `document.body`
- `<head>` = `document.head`

A script cannot access an element that doesn’t exist at the moment of running. If a script is inside `<head>`, then `document.body` is `null`.

### Children: childNodes, firstChild, lastChild

- Child nodes (or children) – elements that are direct children.
- Descendants – children, children of children, etc.

```js
for (let i = 0; i < document.body.childNodes.length; i++) {
    alert( document.body.childNodes[i] ); // Text, DIV, Text, UL, ..., SCRIPT
}
```

Properties `firstChild` and `lastChild` give fast access to the first and last children.

### DOM collections

Dom collection is a special array-like iterable object.

1. We can use `for..of` to iterate over it. (Don't use `for...in`.)
2. Array methods won’t work, because it’s not an array. (Use `Array.from` to convert)
3. DOM collections are read-only and live.

### Siblings and the parent

```js
<html>
  <head>...</head><body>...</body>
</html>
```

- `nextSibling`: `<body>` is the “next” or “right” sibling of `<head>`.
- `previousSibling`: `<head>` is the “previous” or “left” sibling of `<body>`.

### Element-only navigation

For many tasks we only want to manipulate element nodes that represent tags and form the structure of the page.

- `children`:  only those children that are element nodes.
- `firstElementChild`, `lastElementChild`: first and last element children.
- `previousElementSibling`, `nextElementSibling`: neighbor elements.
- `parentElement`: parent element.

#### parentElement

```js
alert( document.documentElement.parentNode ); // document
alert( document.documentElement.parentElement ); // null
```

The root node `document.documentElement` (`<html>`) has document as its parent. But `document` is not an element node, so `parentNode` returns it and `parentElement` does not.

### More links: tables

#### <table>

- `table.rows`: the collection of `<tr>` elements of the table.
- `table.caption`/`tHead`/`tFoot`: references to elements `<caption>`, `<thead>`, `<tfoot>`.
- `table.tBodies`: the collection of `<tbody> `elements.

#### <thead>, <tfoot>, <tbody>

- `tbody.rows`: the collection of `<tr>` inside.

#### <tr>

- `tr.cells`: the collection of `<td>` and `<th>` cells inside the given `<tr>`.
- `tr.sectionRowIndex`: the position of the given `<tr>` inside the enclosing `<thead>`/`<tbody>`/`<tfoot>`.
- `tr.rowIndex`: the number of the `<tr>` in the table as a whole

#### <td> and <th>

- `td.cellIndex`: the number of the cell inside the enclosing `<tr>`.

## 1.4 Searching: getElement*, querySelector*

### document.getElementById or just id

If an element has the `id` attribute, we can get the element using the method `document.getElementById(id)`. There can be only one element in the document with the given `id`.

It's also available as a global varaible, but it will be overwritten by varaibles declared in the script. Don’t use id-named global variables to access elements.

### querySelectorAll

`elem.querySelectorAll(css)` returns all elements inside elem matching the given CSS selector.

### querySelector

`elem.querySelector(css)` returns the first element for the given CSS selector.

### matches

The `elem.matches(css)` merely checks if `elem` matches the given CSS-selector. It's useful when iterating over elements.

### closest

`elem.closest(css)` looks the nearest ancestor that matches the CSS-selector. The `elem` itself is also included in the search.

### getElementsBy*

- `elem.getElementsByTagName(tag)` looks for elements with the given `tag` and returns the collection of them.
- `elem.getElementsByClassName(className)` returns elements that have the given CSS class.
- `document.getElementsByName(name)` returns elements with the given `name` attribute, document-wide.

These method returns a collection, not a single element.

All methods "`getElementsBy*`" return a live collection. In contrast, `querySelectorAll` returns a static collection. It’s like a fixed array of elements.

`elemA.contains(elemB)` returns true if `elemB` is inside `elemA` or when `elemA == elemB`.

## 1.5 Node properties: type, tag and contents

### DOM node classes

Each DOM node belongs to the corresponding built-in class.

- `EventTarget`: the root “abstract” class. Objects of that class are never created. It serves as a base, so that all DOM nodes support so-called “events”.
- `Node`:  It provides the core tree functionality: `parentNode`, `nextSibling`, `childNodes` and so on (they are getters). `Text` for text nodes, `Element` for element nodes and more exotic ones like `Comment` for comment nodes.
- `Element`: It provides element-level navigation like `nextElementSibling`, `children` and searching methods like `getElementsByTagName`, `querySelector`.
- `HTMLElement` – the basic class for all HTML elements.
- - `HTMLInputElement`: the class for `<input>` elements.
- - `HTMLBodyElement`: the class for `<body>` elements.
- - `HTMLAnchorElement`: the class for `<a>` elements.

### The “nodeType” property

- `elem.nodeType == 1` for element nodes
- `elem.nodeType == 3` for text nodes
- `elem.nodeType == 9` for the document object

### Tag: nodeName and tagName

- The `tagName` property exists only for `Element` nodes.
- The `nodeName` is defined for any `Node`.

### innerHTML: the contents

The `innerHTML` property allows to get the HTML inside the element as a string.

`innerHTML+=` does a full overwrite instead of an addition. As the content is rewritten from the scratch, all images and other resources will be reloaded.

### outerHTML: full HTML of the element

The `outerHTML` property contains the full HTML of the element.

Writing to `outerHTML` does not change the element. Instead, it replaces it in the DOM.

For example, what happened in `div.outerHTML=<p>A new element</p>` is:

- `div` was removed from the document.
- Another piece of HTML `<p>A new element</p>` was inserted in its place.
- `div` still has its old value. The new HTML wasn’t saved to any variable.

### nodeValue/data: text node content

For non-element nodes, `nodeValue` and `data` are used to get the content.

### textContent: pure text

The `textContent` provides access to the text inside the element. Writing to `textContent` is much more useful, because it allows to write text the “safe way”.
