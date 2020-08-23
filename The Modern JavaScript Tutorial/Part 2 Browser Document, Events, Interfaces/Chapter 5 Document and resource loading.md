# Chapter 5: Document and resource loading

## 5.1 Page: DOMContentLoaded, load, beforeunload, unload

- `DOMContentLoaded` - the browser fully loaded HTML, and the DOM tree is built, but external resources like pictures `<img>` and stylesheets may be not yet loaded.
- `load` – not only HTML is loaded, but also all the external resources: images, styles etc.
- `beforeunload` – the user is leaving: we can check if the user saved the changes and ask them whether they really want to leave.
- `unload` – the user almost left, but we still can initiate some operations, such as sending out statistics.

### DOMContentLoaded

The `DOMContentLoaded` event happens on the `document` object.

```js
document.addEventListener("DOMContentLoaded", ready);
```

### DOMContentLoaded and scripts

When the browser processes an HTML-document and comes across a `<script>` tag, it needs to execute before continuing building the DOM.

Scripts with the `async` attribute or are generated dynamically with `document.createElement('script')` and then added to the webpage don’t block this event.

### DOMContentLoaded and styles

External style sheets don’t affect DOM, so `DOMContentLoaded` does not wait for them.

Script must wait until the stylesheet loads, because they may want to get coordinates and other style-dependent properties of elements.

### Built-in browser autofill

Firefox, Chrome and Opera autofill forms (username, passwords, etc.) on `DOMContentLoaded`.

### window.onload

The `load` event on the `window` object triggers when the whole page is loaded including styles, images and other resources.

### window.onunload

When a visitor leaves the page, the `unload` event triggers on `window`. 

The `navigator.sendBeacon(url, data)` method sends the data in the background. The transition to another page is not delayed.

```js
window.addEventListener("unload", function() {
  navigator.sendBeacon("/analytics", JSON.stringify(analyticsData));
};
```

The request is sent as post and the data is limited by 64kb.

### window.onbeforeunload

If a visitor initiated navigation away from the page or tries to close the window, the `beforeunload` handler asks for additional confirmation.

Returning `false` or a non-empty string count as canceling the event.

```js
window.onbeforeunload = function() {
  return false;
};
```

### readyState

The `document.readyState` property tells us about the current loading state.

- `loading` – the document is loading.
- `interactive` – the document was fully read.
- `complete` – the document was fully read and all resources are loaded too.

The `readystatechange` event triggers when the state changes.

## 5.2 Scripts: async, defer

- Scripts can’t see DOM elements below them, so they can’t add handlers etc.
- If there’s a bulky script at the top of the page, users can’t see the page content till it downloads and runs.

To workaround it, we can put a script at the bottom of the page. However, the browser notices the scriptonly after it downloaded the full HTML document.

### defer

- Scripts with `defer` never block the page.
- Scripts with `defer` always execute when the DOM is ready, but before `DOMContentLoaded` event.
- Deferred scripts keep their relative order.
- The defer attribute is ignored if the 1 tag has no 1.

### async

- The `async` attribute means that a script is completely independent.
- `DOMContentLoaded` and `async` scripts don’t wait for each other.
- Other scripts don’t wait for `async` scripts.

### Dynamic scripts

We can also add a script dynamically using JavaScript.

```js
let script = document.createElement('script');
script.src = "/article/script-async-defer/long.js";
document.body.append(script);
```

Dynamic scripts behave as async by default.

```js
script.async = false;
```

## 5.3 Resource loading: onload and onerror

The browser allows us to track the loading of external resources – scripts, iframes, pictures, etc.

- `onload`
- `onerror`

### Loading a script

```js
let script = document.createElement('script');
script.src = "my.js";

document.head.append(script);

```

#### script.onload

```js
script.onload = function() {
  alert(_);
};
```

To track script errors, we can use `window.onerror` global handler.

#### script.onerror

```js
script.onerror = function() {
  alert("Error loading " + this.src);
};
```

We can’t get HTTP error details.

### Other resources

The `load` and `error` events also work for other resources, basically for any resource that has an external `src`.

Most resources start loading when adding to the document. But `<img>` starts loading when it gets a `src`.

For `<iframe>`, the `iframe.onload` event triggers when the iframe loading finished, both for load and error.

### Crossorigin policy

Scripts from one site can’t access contents of the other site. (`protocol://domain:port`)

To allow cross-origin access, the `<script>` tag needs to have the `crossorigin` attribute, plus the remote server must provide special headers.

- No `crossorigin` attribute – access prohibited.
- `crossorigin="anonymous"` – access allowed if the server responds with the header `Access-Control-Allow-Origin` with `*` or our origin. Browser does not send authorization information and cookies to remote server.
- `crossorigin="use-credentials"` – access allowed if the server sends back the header` Access-Control-Allow-Origin` with our origin and `Access-Control-Allow-Credentials: true`. Browser sends authorization information and cookies to remote server.

```js
<script crossorigin="anonymous" src="https://cors.javascript.info/error.js"></script>
```
