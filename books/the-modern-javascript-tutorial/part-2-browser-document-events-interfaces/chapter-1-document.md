# Chapter 1: Document

## Browser environment, specs

A host environment provides own objects and functions additional to the language core.

* Web browsers give a means to control web pages. 
* Node.js provides server-side features.

In browsers, there’s a “root” object called `window`.

* It is a global object for JavaScript code.
* It represents the “browser window” and provides methods to control it.

### DOM \(Document Object Model\)

Document Object Model, or DOM for short, represents all page content as objects that can be modified.

For example:

```javascript
// change the background color to red
document.body.style.background = "red";

// change it back after 1 second
setTimeout(() => document.body.style.background = "", 1000);
```

### BOM \(Browser Object Model\)

The Browser Object Model \(BOM\) represents additional objects provided by the browser \(host environment\) for working with everything except the document.

* The `navigator` object provides background information about the browser and the operating system. Example: `navigator.userAgent`, `navigator.platform`
* The `location` object allows us to read the current URL and can redirect the browser to a new one.

## 1.2 DOM tree

The backbone of an HTML document is tags.

According to the DOM, every HTML tag is an object. Nested tags are “children” of the enclosing one. The text inside a tag is an object as well.

### DOM structure

Every tree node is an object.

Spaces and newlines are totally valid characters, like letters and digits. They are a part of the DOM.

* Spaces and newlines before `<head>` are ignored for historical reasons.
* If we put something after `</body>`, then that is automatically moved inside the body, at the end, as the HTML spec requires that all content must be inside `<body>`.

### Autocorrection

If the browser encounters malformed HTML, it automatically corrects it when making the DOM.

* The browser will add `<html>`, `<head>`, `<body>` if they don't exist.
* A document with unclosed tags will become a normal DOM as the browser reads tags and restores the missing parts.

### Other node types

Everything in HTML, even comments, becomes a part of the DOM.

* `document` – the “entry point” into DOM.
* `element nodes` – HTML-tags, the tree building blocks.
* `text nodes` – contain text.
* `comments` – sometimes we can put information there, it won’t be shown, but JS can read it from the DOM.

## 1.3 Walking the DOM

All operations on the DOM start with the document object. That’s the main “entry point” to DOM.

### On top: documentElement and body

* `<html>` = `document.documentElement`
* `<body>` = `document.body`
* `<head>` = `document.head`

A script cannot access an element that doesn’t exist at the moment of running. If a script is inside `<head>`, then `document.body` is `null`.

### Children: childNodes, firstChild, lastChild

* Child nodes \(or children\) – elements that are direct children.
* Descendants – children, children of children, etc.

```javascript
for (let i = 0; i < document.body.childNodes.length; i++) {
    alert( document.body.childNodes[i] ); // Text, DIV, Text, UL, ..., SCRIPT
}
```

Properties `firstChild` and `lastChild` give fast access to the first and last children.

### DOM collections

Dom collection is a special array-like iterable object.

1. We can use `for..of` to iterate over it. \(Don't use `for...in`.\)
2. Array methods won’t work, because it’s not an array. \(Use `Array.from` to convert\)
3. DOM collections are read-only and live.

### Siblings and the parent

```javascript
<html>
  <head>...</head><body>...</body>
</html>
```

* `nextSibling`: `<body>` is the “next” or “right” sibling of `<head>`.
* `previousSibling`: `<head>` is the “previous” or “left” sibling of `<body>`.

### Element-only navigation

For many tasks we only want to manipulate element nodes that represent tags and form the structure of the page.

* `children`:  only those children that are element nodes.
* `firstElementChild`, `lastElementChild`: first and last element children.
* `previousElementSibling`, `nextElementSibling`: neighbor elements.
* `parentElement`: parent element.

#### parentElement

```javascript
alert( document.documentElement.parentNode ); // document
alert( document.documentElement.parentElement ); // null
```

The root node `document.documentElement` \(`<html>`\) has document as its parent. But `document` is not an element node, so `parentNode` returns it and `parentElement` does not.

### More links: tables

* `table.rows`: the collection of `<tr>` elements of the table.
* `table.caption`/`tHead`/`tFoot`: references to elements `<caption>`, `<thead>`, `<tfoot>`.
* `table.tBodies`: the collection of `<tbody>`elements.

#### , ,

* `tbody.rows`: the collection of `<tr>` inside.
* `tr.cells`: the collection of `<td>` and `<th>` cells inside the given `<tr>`.
* `tr.sectionRowIndex`: the position of the given `<tr>` inside the enclosing `<thead>`/`<tbody>`/`<tfoot>`.
* `tr.rowIndex`: the number of the `<tr>` in the table as a whole

#### and

* `td.cellIndex`: the number of the cell inside the enclosing `<tr>`.

## 1.4 Searching: getElement_, querySelector_

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

### getElementsBy\*

* `elem.getElementsByTagName(tag)` looks for elements with the given `tag` and returns the collection of them.
* `elem.getElementsByClassName(className)` returns elements that have the given CSS class.
* `document.getElementsByName(name)` returns elements with the given `name` attribute, document-wide.

These method returns a collection, not a single element.

All methods "`getElementsBy*`" return a live collection. In contrast, `querySelectorAll` returns a static collection. It’s like a fixed array of elements.

`elemA.contains(elemB)` returns true if `elemB` is inside `elemA` or when `elemA == elemB`.

## 1.5 Node properties: type, tag and contents

### DOM node classes

Each DOM node belongs to the corresponding built-in class.

* `EventTarget`: the root “abstract” class. Objects of that class are never created. It serves as a base, so that all DOM nodes support so-called “events”.
* `Node`:  It provides the core tree functionality: `parentNode`, `nextSibling`, `childNodes` and so on \(they are getters\). `Text` for text nodes, `Element` for element nodes and more exotic ones like `Comment` for comment nodes.
* `Element`: It provides element-level navigation like `nextElementSibling`, `children` and searching methods like `getElementsByTagName`, `querySelector`.
* `HTMLElement` – the basic class for all HTML elements.
* * `HTMLInputElement`: the class for `<input>` elements.
* * `HTMLBodyElement`: the class for `<body>` elements.
* * `HTMLAnchorElement`: the class for `<a>` elements.

### The “nodeType” property

* `elem.nodeType == 1` for element nodes
* `elem.nodeType == 3` for text nodes
* `elem.nodeType == 9` for the document object

### Tag: nodeName and tagName

* The `tagName` property exists only for `Element` nodes.
* The `nodeName` is defined for any `Node`.

### innerHTML: the contents

The `innerHTML` property allows to get the HTML inside the element as a string.

`innerHTML+=` does a full overwrite instead of an addition. As the content is rewritten from the scratch, all images and other resources will be reloaded.

### outerHTML: full HTML of the element

The `outerHTML` property contains the full HTML of the element.

Writing to `outerHTML` does not change the element. Instead, it replaces it in the DOM.

For example, what happened in `div.outerHTML=<p>A new element</p>` is:

* `div` was removed from the document.
* Another piece of HTML `<p>A new element</p>` was inserted in its place.
* `div` still has its old value. The new HTML wasn’t saved to any variable.

### nodeValue/data: text node content

For non-element nodes, `nodeValue` and `data` are used to get the content.

### textContent: pure text

The `textContent` provides access to the text inside the element. Writing to `textContent` is much more useful, because it allows to write text the “safe way”.

## 1.6 Attributes and properties

### DOM properties

```javascript
Element.prototype.sayHi = function() {
  alert(`Hello, I'm ${this.tagName}`);
};
```

DOM properties and methods behave just like those of regular JavaScript objects:

* They can have any value.
* They are case-sensitive.

### HTML attributes

When the browser parses the HTML to create DOM objects for tags, it recognizes standard attributes and creates DOM properties from them.

```markup
<body id="test" something="non-standard">
  <script>
    alert(document.body.id); // test
    // non-standard attribute does not yield a property
    alert(document.body.something); // undefined
  </script>
</body>
```

To access all attributes:

* `elem.attributes`: A collection of objects that belong to a built-in `Attr` class.
* `elem.hasAttribute(name)`
* `elem.getAttribute(name)`
* `elem.setAttribute(name, value)`
* `elem.removeAttribute(name)`

HTML attributes have the following features:

* Their name is case-insensitive \(`id` is same as `ID`\).
* Their values are always strings.

### Property-attribute synchronization

When a standard attribute changes, the corresponding property is auto-updated, and vice versa.

`input.value` synchronizes only from attribute to property, but not back.

### DOM properties are typed

DOM properties are not always strings.

The `style` attribute is a string, but the `style` property is an object:

```markup
<div id="div" style="color:red;font-size:120%">Hello</div>

<script>
  // string
  alert(div.getAttribute('style')); // color:red;font-size:120%

  // object
  alert(div.style); // [object CSSStyleDeclaration]
  alert(div.style.color); // red
</script>
```

The `href` DOM property is always a full URL, even if the attribute contains a relative URL or just a `#hash`.

### Non-standard attributes, dataset

All attributes starting with `data-` are reserved for programmers’ use. They are available in the `dataset` property.

```markup
<body data-about="Elephants">
<script>
  alert(document.body.dataset.about); // Elephants
</script>
```

Multiword attributes like `data-order-state` become camel-cased: `dataset.orderState`.

## 1.7 Modifying the document

### Creating an element

* `document.createElement(tag)`
* `document.createTextNode(text)`

### Creating the message

* `div.className = "alert";`
* `div.innerHTML = "<strong>Hi there!</strong> You've read an important message.";`

### Insertion methods

* `node.append(...nodes or strings)` – append nodes or strings at the end of node.
* `node.prepend(...nodes or strings)` – insert nodes or strings at the beginning of node.
* `node.before(...nodes or strings)` - insert nodes or strings before node.
* `node.after(...nodes or strings)` - insert nodes or strings after node.
* `node.replaceWith(...nodes or strings)` - replaces node with the given nodes or strings.

The text is inserted “as text”, not “as HTML”, with proper escaping of characters such as `<`, `>`.

### insertAdjacentHTML/Text/Element

To insert an HTML string “as html”, with all tags and stuff working:

* `elem.insertAdjacentHTML(where, html)`
* * `beforebegin`
* * `afterbegin`
* * `beforeend`
* * `afterend`
* `elem.insertAdjacentText(where, text)`
* `elem.insertAdjacentElement(where, elem)`

### Node removal

To remove a node, there’s a method `node.remove()`.

All insertion methods automatically remove the node from the old place.

### Cloning nodes: cloneNode

* `elem.cloneNode(true)` creates a deep clone of the element with all attributes and subelements.
* `elem.cloneNode(false)` creates a clone without child elements.

### DocumentFragment

`DocumentFragment` is a special DOM node that serves as a wrapper to pass around lists of nodes. When we insert it somewhere, then its content is inserted instead.

```javascript
function getListContent() {
  let fragment = new DocumentFragment();

  for(let i=1; i<=3; i++) {
    let li = document.createElement('li');
    li.append(i);
    fragment.append(li);
  }

  return fragment;
}

ul.append(getListContent()); // (*)
```

### Old-school insert/remove methods

This information helps to understand old scripts.

* `parentElem.appendChild(node)`: Appends `node` as the last child of `parentElem`.
* `parentElem.insertBefore(node, nextSibling)`: Inserts `node` before `nextSibling` into `parentElem`.
* `parentElem.replaceChild(node, oldChild)`: 

  Replaces `oldChild` with `node` among children of `parentElem`.

* `parentElem.removeChild(node)`: Removes `node` from `parentElem`.

### A word about “document.write”

There’s one more, very ancient method of adding something to a web-page: `document.write`. The method comes from times when there was no DOM, no standards.

The call to `document.write` only works while the page is loading. If we call it afterwards, the existing document content is erased.

## 1.8 Styles and classes

### className and classList

* `elem.className`
* `elem.classList`
* * `add/remove`
* * `toggle`
* * `contains`

### Element style

```javascript
background-color  => elem.style.backgroundColor
z-index           => elem.style.zIndex
-moz-border-radius => elem.style.MozBorderRadius
```

### Resetting the style property

`elem.style.display = "none"`: Set a style property. `elem.style.display = ""`: Reset a style property to default. `elem.style.cssText`: Full rewrite the CSS string.

### Computed styles: getComputedStyle

The style property operates only on the value of the `style` attribute, without any CSS cascade.

* `getComputedStyle(element, [pseudo])`
* * `element`
* * `pseudo`: Example: `::before`

Visited links may be colored using `:visited` CSS pseudoclass. `getComputedStyle` can't access that color because of privacy concerns.

## 1.9 Element size and scrolling

### offsetParent, offsetLeft/Top

The `offsetParent` is the nearest ancestor that the browser uses for calculating coordinates during rendering.

Properties `offsetLeft`/`offsetTop` provide x/y coordinates relative to offsetParent upper-left corner.

### offsetWidth/Height

They provide the “outer” width/height of the element or its full size including borders. Geometry properties are zero/null for elements that are not displayed.

### clientTop/Left

* `clientLeft = 25` – left border width
* `clientTop = 25` – top border width

### clientWidth/Height

They include the content width together with paddings, but without the scrollbar.

If there are no paddings, then `clientWidth`/`clientHeight` is exactly the content area, inside the borders and the scrollbar.

### scrollWidth/Height

These properties are like `clientWidth`/`clientHeight`, but they also include the scrolled out parts.

### scrollLeft/scrollTop

Properties `scrollLeft`/`scrollTop` are the width/height of the hidden, scrolled out part of the element.

Don’t take width/height from CSS

## 1.10 Window sizes and scrolling

### Width/height of the window

* `document.documentElement.clientWidth`
* `document.documentElement.clientHeight`

They provide the width/height without the scrollbar.

In modern HTML we should always write `DOCTYPE`. Without it, top-level geomtry properties may work a little bit different.

### Width/height of the document

To reliably obtain the full document height:

```javascript
let scrollHeight = Math.max(
  document.body.scrollHeight, document.documentElement.scrollHeight,
  document.body.offsetHeight, document.documentElement.offsetHeight,
  document.body.clientHeight, document.documentElement.clientHeight
);
```

### Get the current scroll

```javascript
alert('Current scroll from the top: ' + window.pageYOffset);
alert('Current scroll from the left: ' + window.pageXOffset);
```

### Scrolling: scrollTo, scrollBy, scrollIntoView

To scroll the page from JavaScript, its DOM must be fully built.

Regular elements can be scrolled by changing `scrollTop`/`scrollLeft`.

For the whole window, use these methods:

```javascript
window.scrollBy(x,y); // scroll relative to its current position
window.scrollTo(x,y); // scroll to absolute coordinates
```

### scrollIntoView

The call to `elem.scrollIntoView(top)` scrolls the page to make `elem` visible.

* `top=true`: scroll to make `elem` appear on the top of the window
* `top=false`: scroll to make `elem` appear at the bottom

### Forbid the scrolling

The page will freeze on its current scroll.

```javascript
document.body.style.overflow = "hidden"
```

If the scrollbar occupied some space, then that space is now free, and the content fill it.

## 1.11 Coordinates

* Relative to the window \(`clientX`/`clientY`\) \(`position:fixed`\)
* Relative to the document \(`pageX`/`pageY`\) \(`position:absolute`\)

### Element coordinates: getBoundingClientRect

The method `elem.getBoundingClientRect()` returns window coordinates for a minimal rectangle that encloses `elem` as an object of built-in `DOMRect` class.

* `x/y` – X/Y-coordinates of the rectangle origin relative to window,
* `width/height` – width/height of the rectangle \(can be negative\).
* `top/bottom/left/right`

Coordinates `right`/`bottom` are different from CSS position properties. In CSS positioning, `right` property means the distance from the right edge, and `bottom` property means the distance from the bottom edge.

### elementFromPoint\(x, y\)

The call to `document.elementFromPoint(x, y)` returns the most nested element at window coordinates `(x, y)`. It only works if \(x,y\) are inside the visible area.

### Document coordinates

* `pageY` = `clientY` + height of the scrolled-out vertical part of the document.
* `pageX` = `clientX` + width of the scrolled-out horizontal part of the document.

