# Chapter 3: Network requests

## 3.1 Fetch

JavaScript can send network requests to the server and load new information whenever it’s needed.

AJAX: Asynchronous JavaScript And XML

```javascript
let promise = fetch(url, [options])
```

* `url`
* `options`: method, headers, etc.
* The promise resolves with an object of the built-in `Response` class as soon as the server responds with headers. The promise rejects if the fetch was unable to make HTTP-request.
* `status`: HTTP status code
* `ok`: boolean, `true` if code is 200-299
* `Response` provides multiple promise-based methods to access the body in various.
* `response.text()`
* `response.json()`
* `response.formData()`
* `response.blob()`
* `response.arrayBuffer()`
* `response.body`: `ReadableStream` object

We can choose only one body-reading method.

### Headers

```javascript
let response = fetch(protectedUrl, {
  headers: {
    Authentication: 'secret'
  }
});

response.headers.get('Content-Type'));
```

There’s a list of forbidden HTTP headers that we can’t set: `Accept-Encoding`, `Connection`, `Content-Length`, `Keep-Alive`, etc.

### POST requests

* `method`: `POST`
* `body`: one of the following:
* * string
* * `FormData`
* * `Blob`/`BufferSource`
* * `URLSearchParams`

```javascript
let response = await fetch('/article/fetch/post/user', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json;charset=utf-8'
  },
  body: JSON.stringify(user)
});
```

## 3.2 FormData

```javascript
let formData = new FormData([form]);
```

If HTML `form` element is provided, it automatically captures its fields. It has a special type: `Content-Type: multipart/form-data`.

* `formData.append(name, value)`: add a form field with the given `name` and `value`.
* `formData.append(name, blob, fileName)`: add a `file` field
* `formData.delete(name)`
* `formData.get(name)`
* `formData.has(name)`
* `formData.set(name, value)`: remove other `name` and add the new one \(guaranteed unique\)

## 3.3 Fetch: Download progress

The `fetch` method allows to track download progress.

`response.body` gives full control over the reading process, and we can count how much is consumed at any moment.

```javascript
const reader = response.body.getReader();

while(true) {
  const {done, value} = await reader.read();
  if (done) {
    break;
  }
  console.log(`Received ${value.length} bytes`)
}
```

## 3.4 Fetch: Abort

`AbortController` can be used to abort not only fetch, but other asynchronous tasks as well.

1. Create a controller.

```javascript
let controller = new AbortController();
```

It has a single method `abort()`, and a single property `signal`.

When `abort()` is called:

* `abort` event triggers on `controller.signal`.
* `controller.signal.aborted` property becomes `true`.
* Pass the `signal` property to `fetch` option.

```javascript
fetch(url, {
  signal: controller.signal
});
```

1. To abort, call `controller.abort()`. Its promise rejects with an error `AbortError`.

`AbortController` is scalable, it allows to cancel multiple fetches at once.

## 3.5 Fetch: Cross-Origin Requests

If we send a `fetch` request to another web-site, it will probably fail.

Cross-origin requests require special headers from the remote side.

### Simple requests

A simple request is a request that satisfies two conditions: 1. GET, POST or HEAD 2. Headers: `Accept`, `Accept-Language`, `Content-Language`, `Content-Type` \(`application/x-www-form-urlencoded`, `multipart/form-data`, or `text/plain`\)

A “simple request” can be made with a `<form>` or a `<script>`, without any special methods.

### CORS for simple requests

#### Request

```text
GET /request
Host: anywhere.com
Origin: https://javascript.info
...
```

#### Response

```text
200 OK
Content-Type:text/html; charset=UTF-8
Access-Control-Allow-Origin: https://javascript.info
...
```

If the `Access-Control-Allow-Origin` contains the origin or equals `*`, the response is successful, otherwise an error.

Because `Referer` is unreliable \(can be modified or removed by `fetch`\), `Origin` was invented, and the correctness of that header is ensured by the browser.

### Response headers

For cross-origin request, by default JavaScript may only access simple response headers:

* `Cache-Control`
* `Content-Language`
* `Content-Type`
* `Expires`
* `Last-Modified`
* `Pragma`

To grant JavaScript access to any other response header, the server must send `Access-Control-Expose-Headers` header.

```text
Access-Control-Allow-Origin: https://javascript.info
Access-Control-Expose-Headers: Content-Length,API-Key
```

### Non-simple requests

A preflight request uses method `OPTIONS`, no body and two headers:

* `Access-Control-Request-Method`: the method of the non-simple request.
* `Access-Control-Request-Headers`: a comma-separated list of its non-simple HTTP-headers.

If the server agrees to serve the requests, then it should respond with empty body, status 200 and headers:

* `Access-Control-Allow-Method`
* `Access-Control-Allow-Headers`
* `Access-Control-Max-Age`: a number of seconds to cache the permissions.

Then the actual request is sent, the previous simple scheme is applied.

### Credentials

A cross-origin request by default does not bring any credentials \(cookies or HTTP authentication\).

#### Request

```javascript
fetch('http://another.com', {
  credentials: "include"
});
```

#### Response

```text
Access-Control-Allow-Credentials: true
```

`Access-Control-Allow-Origin` is prohibited from using `*` for requests with credentials.

## 3.6 Fetch API

```javascript
let promise = fetch(url, {
  method: "GET", // POST, PUT, DELETE, etc.
  headers: {
    // the content type header value is usually auto-set
    // depending on the request body
    "Content-Type": "text/plain;charset=UTF-8"
  },
  body: undefined // string, FormData, Blob, BufferSource, or URLSearchParams
  referrer: "about:client", // or "" to send no Referer header,
  // or an url from the current origin
  referrerPolicy: "no-referrer-when-downgrade", // no-referrer, origin, same-origin...
  mode: "cors", // same-origin, no-cors
  credentials: "same-origin", // omit, include
  cache: "default", // no-store, reload, no-cache, force-cache, or only-if-cached
  redirect: "follow", // manual, error
  integrity: "", // a hash, like "sha256-abcdef1234567890"
  keepalive: false, // true
  signal: undefined, // AbortController to abort request
  window: window // null
});
```

### referrer, referrerPolicy

The referrer option allows to set any `Referer` within the current origin or remove it.

```javascript
fetch('/page', {
  referrer: "" // no Referer header
});
```

The `referrerPolicy` option sets general rules for `Referer`.

* `"no-referrer-when-downgrade"`: Do not send `Referer` when we send a request from HTTPS to HTTP.
* `no-referrer`
* `origin`
* `origin-when-cross-origin`: Send origin as `Referer` when we send a cross-origin request.
* `same-origin`: Send full Referer to the same origin, but no Referer for cross-origin requests.
* `strict-origin`: Send origin as `Referer`, but don’t send it for downgrade requests.
* `strict-origin-when-cross-origin`: For same-origin send full Referer, for cross-origin send only origin, but don’t send it for downgrade requests.
* `unsafe-url`: Always send full url in `Referer`.

### mode

The `mode` option is a safe-guard that prevents occasional cross-origin requests:

* `cors`: the default, cross-origin requests are allowed
* `same-origin`: cross-origin requests are forbidden
* `no-cors`

### credentials

The credentials option specifies whether fetch should send `cookies` and `HTTP-Authorization` headers with the request.

* `same-origin`: the default, don’t send for cross-origin requests
* `include`: always send.
* `omit`: never send.

### cache

By default, `fetch` requests make use of standard HTTP-caching. It uses `Expires`, `Cache-Control` headers, sends `If-Modified-Since`, etc.

* `default`
* `no-store`
* `reload`
* `no-cache`
* `force-cache`
* `only-if-cached`

### redirect

* `follow`
* `error`: throw error in case of HTTP-redirect.
* `manual`: don’t follow HTTP-redirect, but `response.url` will be the new URL, and `response.redirected` will be `true`.

### integrity

The `integrity` option allows to check if the response matches the known-ahead checksum. \(SHA-256, SHA-384, and SHA-512\)

```javascript
fetch('http://site.com/file', {
  integrity: 'sha256-abcdef'
});
```

### keepalive

The `keepalive` option indicates that the request may outlive the webpage that initiated it.

Normally, when a document is unloaded, all associated network requests are aborted. But `keepalive` option tells the browser to perform the request in background, even after it leaves the page.

* The body limit for `keepalive` requests is 64kb.
* We can’t handle the response if the document is unloaded.

## 3.7 URL objects

### Creating a URL

```javascript
let url = new URL(url, [base]);

let jsInfoUrl = new URL('/profile/admin', 'https://javascript.info');
```

* `url`: full URL or only path.
* `base`: if set and `url` argument has only path, then the full URL is generated.

```javascript
let url = new URL('https://javascript.info/url');

alert(url.protocol); // https:
alert(url.host);     // javascript.info
alert(url.pathname); // /url
```

* `href`: the full url, same as `url.toString()`.
* `protocol`: example: `https`
* `search`: a string of parameters, starts with the question mark `?`.
* `hash`: starts with the hash character `#`.

### SearchParams

```javascript
new URL('https://google.com/search?query=JavaScript')
```

Parameters need to be encoded if they contain spaces, non-latin letters, etc.

* `append(name, value)`
* `delete(name)`
* `get(name)`
* `getAll(name)`
* `has(name)`
* `set(name, value)`
* `sort()`

### Encoding

#### Encoding strings

* `encodeURI`: only characters that are totally forbidden in URL.
* `decodeURI`
* `encodeURIComponent`: forbidden characters and `#, $, &, +, ,, /, :, ;, =, ?, @`.
* `decodeURIComponent`

```javascript
let music = encodeURIComponent('Rock&Roll');

let url = `https://google.com/search?q=${music}`;
alert(url); // https://google.com/search?q=Rock%26Roll
```

## 3.8 XMLHttpRequest

In modern web-development `XMLHttpRequest` is used for three reasons:

* Historical reasons
* Support old browsers without polyfills
* Features that are not supported by `fetch`, such as tracking upload progress

### The basics

1. Create `XMLHttpRequest`:

```javascript
let xhr = new XMLHttpRequest();
```

1. Initialize it:

```javascript
xhr.open(method, URL, [async, user, password])
```

* `method`: HTTP-method
* `URL`
* `async`
* `user`, `password`
* Send it out:

```javascript
xhr.send([body])
```

1. Listen to `xhr` events for response.
2. `load`: triggers when the request is complete \(even if HTTP status is like 400 or 500\) and the response is downloaded.
3. `error`
4. `progress`: triggers periodically while the response is being downloaded, reports how much has been downloaded.

```javascript
xhr.onload = function() {
  alert(`Loaded: ${xhr.status} ${xhr.response}`);
};

xhr.onprogress = function(event) {
  // triggers periodically
  // event.loaded: how many bytes downloaded
  // event.lengthComputable: true if the server sent Content-Length header
  // event.total: total number of bytes (if lengthComputable)
  alert(`Received ${event.loaded} of ${event.total}`);
};
```

Once the server has responded, `xhr` has these properties:

* `status`
* `statusText`: example: `OK`, `NotFound`, `Forbidden`
* `response`

We can also specify a timeout using the corresponding property:

```javascript
xhr.timeout = 10000;
```

### xhr.responseType

* `""`: string
* `text`: string
* `arraybuffer`
* `blob`
* `document`: XML
* `json`

```javascript
xhr.open('GET', 'example.json');

xhr.responseType = 'json';

xhr.onload = function() {
  let responseObj = xhr.response;
  alert(responseObj.message);
};
```

### xhr.readyState

```javascript
UNSENT = 0; // initial state
OPENED = 1; // open called
HEADERS_RECEIVED = 2; // response headers received
LOADING = 3; // response is loading (a data packed is received)
DONE = 4; // request complete
```

We can track them using `readystatechange` event. State `3` repeats every time a data packet is received over the network.

To terminate the request, call `xhr.abort()`.

### Synchronous requests

If in the `open` method the third parameter `async` is set to `false`, the request is made synchronously. JavaScript execution pauses at `send()` and resumes when the response is received.

However, if a synchronous call takes too much time, the browser may suggest to close the “hanging” webpage.

### HTTP-headers

```javascript
setRequestHeader(name, value)

xhr.setRequestHeader('Content-Type', 'application/json');
```

Several headers are managed exclusively by the browser, e.g. `Referer` and `Host`. It can’t undo `setRequestHeader`.

```javascript
xhr.setRequestHeader('X-Auth', '123');
xhr.setRequestHeader('X-Auth', '456');

// the header will be:
// X-Auth: 123, 456
```

```javascript
getResponseHeader(name)
xhr.getResponseHeader('Content-Type')
```

```javascript
getAllResponseHeaders()

Cache-Control: max-age=31536000
Content-Length: 4260
Content-Type: image/png
Date: Sat, 08 Sep 2012 16:53:16 GMT
```

### POST, FormData

To make a POST request, we can use the built-in `FormData` object, which is sent with `multipart/form-data` encoding.

```javascript
let formData = new FormData([form]); // creates an object, optionally fill from <form>
formData.append(name, value); // appends a field
```

### Upload progress

The `progress` event triggers only on the downloading stage. We can use `xhr.upload` to track upload events.

It generates these events:

* `loadstart` – upload started.
* `progress` – triggers periodically during the upload.
* `abort` – upload aborted.
* `error` – non-HTTP error.
* `load` – upload finished successfully.
* `timeout` – upload timed out \(if timeout property is set\).
* `loadend` – upload finished with either success or error.

```javascript
xhr.upload.onprogress = function(event) {
  alert(`Uploaded ${event.loaded} of ${event.total} bytes`);
};
```

## 3.9 Resumable file upload

### Not-so-useful progress event

The event triggers when the data is sent, instead of receving by the server. Maybe it was buffered by a local network proxy, or maybe the remote server process just died and couldn’t process them.

### Algorithm

1. Create an id to uniquely identify the file.
2. Send a request to the server, asking how many bytes it already has.
3. Use `Blob` method `slice` to send the file from the break point.

## 3.10 Long polling

Long polling is the simplest way of having persistent connection with server, that doesn’t use any specific protocol like WebSocket or Server Side Events.

### Regular polling

The simplest way to get new information from the server is periodic polling.

* Messages are passed with a delay up to the period.
* The server may have too many requests to handle.

### Long pooling

Long polling works great in situations when messages are rare.

Every message is a separate request, supplied with headers and authentication overhead.

1. A request is sent to the server.
2. The server doesn’t close the connection until it has a message to send.
3. When a message appears – the server responds to the request with it.
4. The browser makes a new request immediately.

```javascript
async function subscribe() {
  let response = await fetch("/subscribe");

  if (response.status == 502) {
    await subscribe();
  } else if (response.status != 200) {
    showMessage(response.statusText);
    await new Promise(resolve => setTimeout(resolve, 1000));
    await subscribe();
  } else {
    let message = await response.text();
    showMessage(message);
    await subscribe();
  }
}
```

## 3.11 WebSocket

The `WebSocket` protocol provides a way to exchange data between browser and server via a persistent connection.

### A simple example

```javascript
let socket = new WebSocket("wss://javascript.info");
```

Use `wss://` protocol is encrypted and more reliable than `ws://`.

### Events

* `open` – connection established,
* `message` – data received,
* `error` – websocket error,
* `close` – connection closed.

### Opening a websocket

When `new WebSocket(url)` is created, it starts connecting immediately.

Here’s an example of browser headers for request:

```text
GET /chat
Host: javascript.info
Origin: https://javascript.info
Connection: Upgrade
Upgrade: websocket
Sec-WebSocket-Key: Iv8io/9s+lYFgZWcXczP8Q==
Sec-WebSocket-Version: 13
```

* `Origin`: It allows the server to decide whether or not to talk `WebSocket` with this website.
* `Connection: Upgrade`: The signal to change to protocol.
* `Upgrade: websocket`: The requested protocol.
* `Sec-WebSocket-Key`: Random security key.
* `Sec-WebSocket-Version`: WebSocket protocol version. \(Current: 13\)

The server should send code `101` response:

```text
101 Switching Protocols
Upgrade: websocket
Connection: Upgrade
Sec-WebSocket-Accept: hsBlbuDTkk24srzEOTBUlZAlC2g=
```

Then the connection is made. WebSocket handshake can’t be emulated.

#### Extensions and subprotocols

The additional headers `Sec-WebSocket-Extensions` and `Sec-WebSocket-Protocol` describe extensions and subprotocols.

* `Sec-WebSocket-Extensions: deflate-frame`: the browser supports data compression.
* `Sec-WebSocket-Protocol: soap, wamp`: we’d like to transfer the data in SOAP or WAMP \("The WebSocket Application Messaging Protocol"\) protocols.

```javascript
let socket = new WebSocket("wss://javascript.info/chat", ["soap", "wamp"]);
```

The server should respond with a list of protocols and extensions that it agrees to use.

### Data transfer

WebSocket communication consists of “frames” – data fragments, that can be sent from either side, and can be of several kinds:

* text frames
* binary data frames
* ping/pong frames: used to check the connection \(sent by server and responded automatically by browser\)
* connection close frame

WebSocket `.send()` method can send either text or binary data.

When we receive the data, text always comes as string. And for binary data, we can choose between `Blob` and `ArrayBuffer` formats.

### Rate limiting

The `socket.bufferedAmount` property stores how many bytes are buffered at this moment, waiting to be sent over the network.

### Connection close

```javascript
socket.close([code], [reason]);
```

* `code`: a special WebSocket closing code.
* `reason`: a string that describes the reason of closing.

Most common codes:

* `1000`: the default closure
* `1006`: connection was lost \(can't be sent manually\)
* `1001`: the party is going away \(server shuts down or user leaves the page\)
* `1009`: the message is too big to process
* `1011`: unexpected error on server

### Connection state

There's the `socket.readyState` property with values:

* `0`: CONNECTING
* `1`: OPEN
* `2`: CLOSING
* `3`: CLOSED

## 3.12 Server Sent Events

The `Server-Sent Events` specification describes a built-in class `EventSource`, that keeps connection with the server and allows to receive events from it.

It's simpler than `WebSocket`:

* Only server sends data
* Only text
* Regular HTTP protocol
* Auto-reconnect

### Getting messages

To start receiving messages, we just need to create `new EventSource(url)`.

The server should respond with status 200 and the header Content-Type: `text/event-stream`.

```javascript
data: Message 1

data: Message 2

data: Message 3
```

* A message text goes after `data:`, the space after the colon is optional.
* Messages are delimited with double line breaks `\n\n`.
* To send a line break `\n`, we can immediately send one more data.

```javascript
eventSource.onmessage = function(event) {
  console.log("New message", event.data);
};
```

### Cross-origin requests

`EventSource` supports cross-origin requests. The remote server will get the `Origin` header and must respond with `Access-Control-Allow-Origin` to proceed.

```javascript
let source = new EventSource("https://another-site.com/events", {
  withCredentials: true
});
```

### Reconnection

There’s a small delay between reconnections, a few seconds by default. The server can set the recommended delay using `retry:` in response.

```text
retry: 15000
```

* If the browser knows that there’s no network connection at the moment, it may wait until the connection appears, and then retry.
* If the server wants the browser to stop reconnecting, it should respond with HTTP status `204`.

  If the browser wants to close the connection, it should call `eventSource.close()`.

### Message id

When a connection breaks due to network problems, either side can’t be sure which messages were received. To correctly resume the connection, each message should have an `id` field.

```text
data: Message 1
id: 1
```

When a message with `id:` is received, the browser:

* Sets the property `eventSource.lastEventId`to its value.
* Sends the header `Last-Event-ID` with that `id` when reconnecting.

### Connection status: readyState

The `EventSource` object has `readyState` property.

* `0`: Connecting
* `1`: Open
* `2`: Closed

### Event types

* `message` – a message received, available as `event.data`.
* `open` – the connection is open.
* `error` – the connection could not be established.

The server may specify another type of event with `event: ...` at the event start.

```text
event: join
data: Bob
```

To handle custom events, we must use `addEventListener`:

```javascript
eventSource.addEventListener('join', event => {
  alert(`Joined ${event.data}`);
});
```

