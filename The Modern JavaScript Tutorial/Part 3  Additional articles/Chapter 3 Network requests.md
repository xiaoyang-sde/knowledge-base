# Chapter 3: Network requests

## 3.1 Fetch

JavaScript can send network requests to the server and load new information whenever it’s needed.

AJAX: Asynchronous JavaScript And XML

```js
let promise = fetch(url, [options])
```

- `url`
- `options`: method, headers, etc.

1. The promise resolves with an object of the built-in `Response` class as soon as the server responds with headers. The promise rejects if the fetch was unable to make HTTP-request.

- `status`: HTTP status code
- `ok`: boolean, `true` if code is 200-299

2. `Response` provides multiple promise-based methods to access the body in various.
- `response.text()`
- `response.json()`
- `response.formData()`
- `response.blob()`
- `response.arrayBuffer()`
- `response.body`: `ReadableStream` object

We can choose only one body-reading method.

### Headers

```js
let response = fetch(protectedUrl, {
  headers: {
    Authentication: 'secret'
  }
});

response.headers.get('Content-Type'));
```

There’s a list of forbidden HTTP headers that we can’t set: `Accept-Encoding`, `Connection`, `Content-Length`, `Keep-Alive`, etc.

### POST requests

- `method`: `POST`
- `body`: one of the following:
- - string
- - `FormData`
- - `Blob`/`BufferSource`
- - `URLSearchParams`

```js
let response = await fetch('/article/fetch/post/user', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json;charset=utf-8'
  },
  body: JSON.stringify(user)
});
```

## 1.2 FormData

```js
let formData = new FormData([form]);
```

If HTML `form` element is provided, it automatically captures its fields. It has a special type: `Content-Type: multipart/form-data`.

- `formData.append(name, value)`: add a form field with the given `name` and `value`.
- `formData.append(name, blob, fileName)`: add a `file` field
- `formData.delete(name)`
- `formData.get(name)`
- `formData.has(name)`
- `formData.set(name, value)`: remove other `name` and add the new one (guaranteed unique)

## 1.3 Fetch: Download progress

The `fetch` method allows to track download progress.

`response.body` gives full control over the reading process, and we can count how much is consumed at any moment.

```js
const reader = response.body.getReader();

while(true) {
  const {done, value} = await reader.read();
  if (done) {
    break;
  }
  console.log(`Received ${value.length} bytes`)
}
```

## 1.4 Fetch: Abort

`AbortController` can be used to abort not only fetch, but other asynchronous tasks as well.

1. Create a controller.

```js
let controller = new AbortController();
```
It has a single method `abort()`, and a single property `signal`.

When `abort()` is called:
- `abort` event triggers on `controller.signal`.
- `controller.signal.aborted` property becomes `true`.

2. Pass the `signal` property to `fetch` option.

```js
fetch(url, {
  signal: controller.signal
});
```

3. To abort, call `controller.abort()`. Its promise rejects with an error `AbortError`.

`AbortController` is scalable, it allows to cancel multiple fetches at once.

## 3.5 Fetch: Cross-Origin Requests

If we send a `fetch` request to another web-site, it will probably fail.

Cross-origin requests require special headers from the remote side.

### Simple requests

A simple request is a request that satisfies two conditions:
1. GET, POST or HEAD
2. Headers: `Accept`, `Accept-Language`, `Content-Language`, `Content-Type` (`application/x-www-form-urlencoded`, `multipart/form-data`, or `text/plain`)

A “simple request” can be made with a `<form>` or a `<script>`, without any special methods.

### CORS for simple requests

#### Request

```
GET /request
Host: anywhere.com
Origin: https://javascript.info
...
```

#### Response

```
200 OK
Content-Type:text/html; charset=UTF-8
Access-Control-Allow-Origin: https://javascript.info
...
```

If the `Access-Control-Allow-Origin` contains the origin or equals `*`, the response is successful, otherwise an error.

Because `Referer` is unreliable (can be modified or removed by `fetch`), `Origin` was invented, and the correctness of that header is ensured by the browser.

### Response headers

For cross-origin request, by default JavaScript may only access simple response headers:

- `Cache-Control`
- `Content-Language`
- `Content-Type`
- `Expires`
- `Last-Modified`
- `Pragma`

To grant JavaScript access to any other response header, the server must send `Access-Control-Expose-Headers` header.

```
Access-Control-Allow-Origin: https://javascript.info
Access-Control-Expose-Headers: Content-Length,API-Key
```

### Non-simple requests

A preflight request uses method `OPTIONS`, no body and two headers:

- `Access-Control-Request-Method`: the method of the non-simple request.
- `Access-Control-Request-Headers`: a comma-separated list of its non-simple HTTP-headers.

If the server agrees to serve the requests, then it should respond with empty body, status 200 and headers:

- `Access-Control-Allow-Method`
- `Access-Control-Allow-Headers`
- `Access-Control-Max-Age`: a number of seconds to cache the permissions.

Then the actual request is sent, the previous simple scheme is applied.

### Credentials

A cross-origin request by default does not bring any credentials (cookies or HTTP authentication).

#### Request

```js
fetch('http://another.com', {
  credentials: "include"
});
```

#### Response

```
Access-Control-Allow-Credentials: true
```

`Access-Control-Allow-Origin` is prohibited from using `*` for requests with credentials.

## 3.6 Fetch API

```js
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

```js
fetch('/page', {
  referrer: "" // no Referer header
});
```

The `referrerPolicy` option sets general rules for `Referer`.

- `"no-referrer-when-downgrade"`: Do not send `Referer` when we send a request from HTTPS to HTTP.
- `no-referrer`
- `origin`
- `origin-when-cross-origin`: Send origin as `Referer` when we send a cross-origin request.
- `same-origin`: Send full Referer to the same origin, but no Referer for cross-origin requests.
- `strict-origin`: Send origin as `Referer`, but don’t send it for downgrade requests.
- `strict-origin-when-cross-origin`: For same-origin send full Referer, for cross-origin send only origin, but don’t send it for downgrade requests.
- `unsafe-url`: Always send full url in `Referer`.

### mode

The `mode` option is a safe-guard that prevents occasional cross-origin requests:

- `cors`: the default, cross-origin requests are allowed
- `same-origin`: cross-origin requests are forbidden
- `no-cors`

### credentials

The credentials option specifies whether fetch should send `cookies` and `HTTP-Authorization` headers with the request.

- `same-origin`: the default, don’t send for cross-origin requests
- `include`: always send.
- `omit`: never send.

### cache

By default, `fetch` requests make use of standard HTTP-caching. It uses `Expires`, `Cache-Control` headers, sends `If-Modified-Since`, etc.

- `default`
- `no-store`
- `reload`
- `no-cache`
- `force-cache`
- `only-if-cached`

### redirect

- `follow`
- `error`: throw error in case of HTTP-redirect.
- `manual`: don’t follow HTTP-redirect, but `response.url` will be the new URL, and `response.redirected` will be `true`.

### integrity

The `integrity` option allows to check if the response matches the known-ahead checksum. (SHA-256, SHA-384, and SHA-512)

```js
fetch('http://site.com/file', {
  integrity: 'sha256-abcdef'
});
```

### keepalive

The `keepalive` option indicates that the request may outlive the webpage that initiated it.

Normally, when a document is unloaded, all associated network requests are aborted. But `keepalive` option tells the browser to perform the request in background, even after it leaves the page.

- The body limit for `keepalive` requests is 64kb.
- We can’t handle the response if the document is unloaded.

## 3.7 URL objects

### Creating a URL

```js
let url = new URL(url, [base]);

let jsInfoUrl = new URL('/profile/admin', 'https://javascript.info');
```

- `url`: full URL or only path.
- `base`: if set and `url` argument has only path, then the full URL is generated.

```js
let url = new URL('https://javascript.info/url');

alert(url.protocol); // https:
alert(url.host);     // javascript.info
alert(url.pathname); // /url
```

- `href`: the full url, same as `url.toString()`.
- `protocol`: example: `https`
- `search`: a string of parameters, starts with the question mark `?`.
- `hash`: starts with the hash character `#`.

### SearchParams

```js
new URL('https://google.com/search?query=JavaScript')
```

Parameters need to be encoded if they contain spaces, non-latin letters, etc.

- `append(name, value)`
- `delete(name)`
- `get(name)`
- `getAll(name)`
- `has(name)`
- `set(name, value)`
- `sort()`

### Encoding

#### Encoding strings

- `encodeURI`: only characters that are totally forbidden in URL.
- `decodeURI`
- `encodeURIComponent`: forbidden characters and `#, $, &, +, ,, /, :, ;, =, ?, @`.
- `decodeURIComponent`

```js
let music = encodeURIComponent('Rock&Roll');

let url = `https://google.com/search?q=${music}`;
alert(url); // https://google.com/search?q=Rock%26Roll
```

## 3.8 XMLHttpRequest

In modern web-development `XMLHttpRequest` is used for three reasons:

- Historical reasons
- Support old browsers without polyfills
- Features that are not supported by `fetch`, such as tracking upload progress

### The basics

1. Create `XMLHttpRequest`:

```js
let xhr = new XMLHttpRequest();
```

2. Initialize it:

```js
xhr.open(method, URL, [async, user, password])
```

- `method`: HTTP-method
- `URL`
- `async`
- `user`, `password`

3. Send it out:

```js
xhr.send([body])
```

4. Listen to `xhr` events for response.

- `load`: triggers when the request is complete (even if HTTP status is like 400 or 500) and the response is downloaded.
- `error`
- `progress`: triggers periodically while the response is being downloaded, reports how much has been downloaded.

```js
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

- `status`
- `statusText`: example: `OK`, `NotFound`, `Forbidden`
- `response`

We can also specify a timeout using the corresponding property:

```js
xhr.timeout = 10000;
```

### xhr.responseType

- `""`: string
- `text`: string
- `arraybuffer`
- `blob`
- `document`: XML
- `json`

```js
xhr.open('GET', 'example.json');

xhr.responseType = 'json';

xhr.onload = function() {
  let responseObj = xhr.response;
  alert(responseObj.message);
};
```

### xhr.readyState

```js
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

## 3.9 Resumable file upload

## 3.10 Long polling

## 3.11 WebSocket

## 3.12 Server Sent Events
