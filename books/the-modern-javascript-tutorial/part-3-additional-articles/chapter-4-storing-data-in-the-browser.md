# Chapter 4: Storing data in the browser

## 4.1 Cookies, document.cookie

Cookies are small strings of data that are stored directly in the browser. They are set by a web-server using response `Set-Cookie` HTTP-header.

### Reading from document.cookie

```javascript
// cookie1=value1; cookie2=value2;...
alert( document.cookie );
```

Use regular expresssion or array functions to split that string and find a particular cookie.

### Writing to document.cookie

A `write` operation to document.cookie updates only cookies mentioned in it, but doesn’t touch other cookies.

```javascript
document.cookie = "user=John"; // update only cookie named 'user'
alert(document.cookie); // show all cookies
```

Technically, the strings should be escaped using a built-in `encodeURIComponent` function:

```javascript
document.cookie = encodeURIComponent(name) + '=' + encodeURIComponent(value);
```

* The `name=value` pair, after `encodeURIComponent`, should not exceed 4kb.
* The total number of cookies per domain is limited to around 20+.

### Cookie options

```javascript
document.cookie = "user=John; path=/; expires=Tue, 19 Jan 2038 03:14:07 GMT"
```

* `path=/mypath`: The cookie will be accessible for pages under that path.
* `domain=site.com`: There’s no way to let a cookie be accessible from another 2nd-level domain. However, we could explicitly use this option.

```javascript
document.cookie = "user=John; domain=site.com"
```

* `expires`: Without these options, the cookie disappears when the browser is closed.

```javascript
expires=Tue, 19 Jan 2038 03:14:07 GMT
```

* `max-age`: An alternative to `expires`, count in seconds.
* `secure`: The cookie should be transferred only over HTTPS.
* `samesite`: It’s designed to protect from so-called XSRF \(cross-site request forgery\) attacks.

### XSRF attack

* `samesite=strict`: Never sent if the user comes from outside the same site.
* `samesite=lax`: Sent if both of these conditions are true: The HTTP method is “safe” \(e.g. GET, but not POST\), or the operation performs top-level navigation \(not in an `<iframe>`\).

However, `samesite` is ignored \(not supported\) by old browsers before 2017.

### httpOnly

The web-server uses `Set-Cookie` header to set a cookie and it may set the `httpOnly` option. This option forbids any JavaScript access to the cookie using `document.cookie`.

### Third-party cookies

A cookie is called “third-party” if it’s placed by domain other than the page user is visiting. They can track the visitor as he moves between sites.

Example: A page at site.com loads a banner from another site: `<img src="https://ads.com/banner.png">`.

### GDPR

GDPR requires an explicit permission for tracking/identifying/authorizing cookies from a user.

## 4.2 LocalStorage, sessionStorage

Web storage objects `localStorage` and `sessionStorage` allow to save key/value pairs in the browser.

* Unlike cookies, web storage objects are not sent to server with each request.
* The server can’t manipulate storage objects via HTTP headers.
* The storage is bound to the origin.

### Methods and properties

* `setItem(key, value)`
* `getItem(key)`
* `removeItem(key)`
* `clear()`: delete everything.
* `key(index)`: get the key on a given position.
* `length`

### localStorage

* Shared between all tabs and windows from the same origin.
* The data does not expire. It remains after the browser restart and even OS reboot.

### Object-like access

```javascript
localStorage.test = 2;
alert(localStorage.test);
delete localStorage.test;
```

It's not recommended to use `localStorage` as a plain object.

### Looping over keys

Storage objects are not iterable.

```javascript
for (let i=0; i < localStorage.length; i++) {
  let key = localStorage.key(i);
  alert(`${key}: ${localStorage.getItem(key)}`);
}
```

```javascript
let keys = Object.keys(localStorage);
for(let key of keys) {
  alert(`${key}: ${localStorage.getItem(key)}`);
}
```

### Strings only

If we set the key or value to any other type, like a number, or an object, it gets converted to string automatically. We can use JSON to store objects though.

### sessionStorage

* The `sessionStorage` exists only within the current browser tab. It is shared between `iframes` with same origin in the same tab.
* The data survives page refresh, but not closing/opening the tab.

```javascript
sessionStorage.setItem('test', 1);
```

### Storage event

`storage` event triggers when the data gets updated in `localStorage` or `sessionStorage`.

* `key`: `null` if `clear()` is called
* `oldValue`: `null` if newly added
* `newValue`: `null` if removed
* `url`: the url of the document
* `storageArea`: a reference to the object

The event triggers on all window objects where the storage is accessible, except the one that caused it.

That allows different windows from the same origin to exchange messages.

### 4.3 IndexedDB

IndexedDB is a database that is built into browser, much more powerful than `localStorage`.

* Stores almost any kind of values by keys, multiple key types.
* Supports transactions for reliability.
* Supports key range queries, indexes.
* Stores much bigger volumes of data than `localStorage`.

It is intended for offline apps, to be combined with ServiceWorkers and other technologies.

### Open database

```javascript
let openRequest = indexedDB.open(name, version);

let deleteRequest = indexedDB.deleteDatabase(name)
```

name – a string, the database name. version – a positive integer version, by default `1`.

The database must locates in the same origin.

This call returns `openRequest` object, which has three events: `success`, `error`, `ungradeneeded` \(version is outdated\).

### Version

IndexedDB has a built-in mechanism of “schema versioning”, absent in server-side databases.

Unlike server-side databases, IndexedDB is client-side. If the local database version is less than specified in `open`, then a special event `upgradeneeded` is triggered, and we can compare versions and upgrade data structures as needed.

The `upgradeneeded` event also triggers when the database did not exist yet \(version `0`\), so we can perform initialization.

We can’t open an older version of the database.

### Parallel update problem

* A visitor opened our site in a browser tab, with database version 1.
* Then we rolled out an update, so our code is newer.
* And then the same visitor opens our site in another tab.

The `versionchange` event triggers in such case on the “outdated” database object. We should listen to it and close the old database connection, otherwise, the new connection won't be made.

### Object store

To store something in IndexedDB, we need an object store. We can store almost any value, including complex objects.

There must be a unique key for every value in the store.

To create an object store:

```javascript
db.createObjectStore(name[, keyOptions]);
```

* `name`
* `keyOptions`
* * `keyPath`: A path to an object property that IndexedDB will use as the key.
* * `autoIncrement`: The key for a newly stored object is generated automatically.

An object store can only be created/modified while updating the DB version, in `upgradeneeded` handler.

To perform database version upgrade:

* Implement per-version upgrade functions.
* Get a list of existing object stores as `db.objectStoreNames`. That object is a sa that provides `contains(name)` method to check for existance.

### Transactions

A transaction is a group operations to a database. All data operations must be made within a transaction in IndexedDB.

```javascript
db.transaction(store[, type]);
```

* `store`: store name that will access
* `type`
* * `readonly`: can only read, the default.
* * `readwrite`: can only read and write, but not manipulate the object stores.

IndexedDB automatically creates a `versionchange` transaction when opening the database for `updateneeded` handler. In that transaction, we can manipulate the object stores.

1. Create a transaction, mention all stores it’s going to access
2. Get the store object using `transaction.objectStore(name)`.
3. Perform the request to the object store.
4. Handle request success/error.

```javascript
let transaction = db.transaction("books", "readwrite"); // (1)

// get an object store to operate on it
let books = transaction.objectStore("books"); // (2)

let book = {
  id: 'js',
  price: 10,
  created: new Date()
};

let request = books.add(book); // (3)

request.onsuccess = function() { // (4)
  console.log("Book added to the store", request.result);
};
```

* `put(value, [key])`: Replaces the existing key.
* `add(value, [key])`: Raises `ConstraintError` if exists.

### Transactions’ autocommit

When all transaction requests are finished, and the microtasks queue is empty, it is committed automatically.

Transactions are closed before the browser starts doing macrotasks, so we can't use `Promise` to schedule furtuer tasks.

If one part of application initiated `readwrite` on an object store, then another part has to wait.

To manually abort the transaction:

```javascript
transaction.abort();
```

### Error handling

A failed request automatically aborts the transaction, canceling all its changes.

The `request.onerror` handler is able to prevent the abort by calling `event.preventDefault()`.

#### Event delegation

IndexedDB events bubble: `request` - `transaction` - `database`.

```javascript
db.onerror = function(event) {
  let request = event.target; // the request that caused the error

  console.log("Error", request.error);
};
```

### Searching by keys

* By a key or a key range.
* By another object field, e.g. `book.price`.

Methods that involve searching support either exact keys or so-called “range queries”: `IDBKeyRange` objects that specify a “key range”.

* `IDBKeyRange.lowerBound(lower, [open])`: `≥lower` or `>lower` if `open`
* `IDBKeyRange.upperBound(upper, [open])`
* `IDBKeyRange.bound(lower, upper, [lowerOpen], [upperOpen])`
* `IDBKeyRange.only(key)`: a range that consists of only one `key`

All searching methods accept a `query` argument that can be either an exact key or a key range.

* `store.get(query)`: first value by key or range.
* `store.getAll([query], [count])`: all values limited by count.
* `store.getKey(query)`: the first key that satisfies the query, usually a range.
* `store.getAllKeys([query], [count])`: search for all keys that satisfy the query limited by count.
* `store.count([query])`: get the total count of keys that satisfy the query.

Object store sorts values by key internally.

### Searching by any field with an index

To search by other object fields, we need to create an additional data structure named “index”.

An index is an “add-on” to the store that tracks a given object field.

```javascript
objectStore.createIndex(name, keyPath, [options]);
```

* `name`
* `keyPath`: path to the object field that the index should track
* `option`
* * `unique`: one object in the store with the given value at the `keyPath`.
* * `multiEntry`: the index will treat the whole array as the key.

```javascript
openRequest.onupgradeneeded = function() {
  let books = db.createObjectStore('books', {keyPath: 'id'});
  let index = inventory.createIndex('price_idx', 'price');
};
```

The index will track `price` field. Each index keeps a list of keys that have the given price.

### Deleting from store

* `delete(query)`: delete matching values by query.
* `clear()`: delete everything.

### Cursors

A cursor is a special object that traverses the object storage, given a query, and returns one key/value at a time, thus saving memory.

```javascript
let request = store.openCursor(query, [direction]);
```

* `query`: a key or a key range.
* `direction`
* * `next`, `prev`
* * `nextunique`, `prevunique`

Ffor cursors, `request.onsuccess` triggers multiple times, once for each result.

#### methods

* `advance(count)`: advance the cursor `count` times, skipping values.
* `continue([key])`: advance the cursor to the next value in range matching.

