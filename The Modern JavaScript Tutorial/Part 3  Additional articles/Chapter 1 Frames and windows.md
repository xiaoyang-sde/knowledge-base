# Chapter 1: Frames and windows

## 1.1 Popups and window methods

```js
window.open('https://javascript.info/')
```
The initial idea of popups to show another content without closing the main window.

### Popup blocking

Most browsers block popups if they are called outside of user-triggered event handlers like `onclick`.

If the popup is scheduled by a `setTimeout` on the `onclick` event, Chrome will allow and Firefox will block it.

### window.open

The syntax to open a popup is: `window.open(url, name, params)`

- `url`: An URL to load into the new window.
- `name`: A name of the new window.
- `params`
- - Position: `left`, `top`, `width`, `height`
- - Window: `menubar`, `toolbar`, `location`, `status`, `resizable`, `scrollbars` (yes/no)

```js
let params = `scrollbars=no,resizable=no,status=no,location=no,toolbar=no,menubar=no,
width=600,height=300,left=100,top=100`;
open('/', 'test', params);
```

### Accessing popup from window

The `open` call returns a reference to the new window.

```js
let newWindow = open('/', 'example', 'width=300,height=300')
newWindow.focus();

newWindow.onload = function() {
  let html = `<div style="font-size:30px">Welcome!</div>`;
  newWindow.document.body.insertAdjacentHTML('afterbegin', html);
};
```

It can only access the content of sites in the same origin. (portocol://domain:port)

### Accessing window from popup

`window.opener` is a reference to the original window.

### Closing a popup

To close a window: `win.close()`.

### Scrolling and resizing

- `win.moveBy(x,y)` (relative)
- `win.moveTo(x,y)` (absolute)
- `win.resizeBy(width,height)` (relativ)
- `win.resizeTo(width,height)` (absolute)

JavaScript has no way to minify or maximize a window.

### Scrolling a window

- `win.scrollBy(x,y)`
- `win.scrollTo(x,y)`
- `elem.scrollIntoView(top = true)` - Scroll the window to make `elem` show up at the top or at the bottom.

### Focus/blur on a window

- `window.focus()`
- `window.blur()`

## 1.2 Cross-window communication

### Same Origin

Two URLs are said to have the “same origin” if they have the same protocol, domain, and port.

If we have a reference to another window from the same origin, then we have full access to that window.

Actions that are always allowed:

- Change the `location` of another window (write-only access).
- Post a message to it.

### iframe

- `iframe.contentWindow`
- `iframe.contentDocument`

### Domain

The exception is that windows can be considered as coming from the same origin if they share the same second-level domain. 

To make it work, each such window should run the code:

```js
document.domain = 'site.com';
```

### iframe: wrong document pitfall

Upon its creation an iframe immediately has a document, which is different from the one that loads into it. The right document is definitely at place when `iframe.onload` triggers.

### Collection: window.frames

- `window.frames[0]` – the window object for the first frame in the document.
`window.frames.iframeName` – the window object for the frame with `name="iframeName"`.

`window` objects form a hierarchy:
- `window.frames`
- `window.parent`
- `window.top`

```js
window.frames[0].parent === window; // true
```

### The “sandbox” iframe attribute

The `sandbox` attribute will let the `iframe` be treated as coming from another origin.

However, we can provide a list of restrictions that should be ignored, such as `<iframe sandbox="allow-forms allow-popups">`.

- `allow-same-origin`
- `allow-top-navigation`
- `allow-forms`
- `allow-scripts`
- `allow-popups`

### Cross-window messaging

The `postMessage` interface allows windows to talk to each other no matter which origin they are from.

#### postMessage

If we want to send the message to `win`, we should call `win.postMessage(data, targetOrigin)`.

- `data`: Any data strucutre (only strings in IE)
- `targetOrigin`: Only a window from the given origin will get the message. (`*` to ignore the check)

#### onmessage

To receive a message, the target window should have a handler on the `message` event.

- `data`
- `origin`
- `source`: The reference to the sender window.

Use `addEventListener` to set hte handler for this event.

## 1.3 The clickjacking attack

### X-Frame-Options

The server-side header `X-Frame-Options` can permit or forbid displaying the page inside a frame.

- `DENY`
- `SAMEORIGIN`
- `ALLOW-FROM domain`

### Samesite cookie attribute

```
Set-Cookie: authorization=secret; samesite
```
