# Chapter 2: Binary data, files

## 2.1 ArrayBuffer, binary arrays

`ArrayBuffer` is a reference to a fixed-length contiguous memory area.

```javascript
let buffer = new ArrayBuffer(16);
```

* It has a fixed length.
* It takes exactly that much space in the memory.
* To access individual bytes, another “view” object is needed, not `buffer[index]`.

To manipulate an ArrayBuffer, we need to use a “view” object. A view object does not store anything on it’s own.

* `Uint8Array`: 8-bit unsigned integer
* `Uint16Array`: 16-bit unsigned integer
* `Uint32Array`: 32-bit unsigned integer
* `Float64Array`

```javascript
let buffer = new ArrayBuffer(16);
let view = new Uint32Array(buffer);
view[0] = 123456;
```

### TypedArray

Typed array is the common term for all these views, which behave like regular arrays: have indexes and iterable.

```javascript
new TypedArray(buffer, [byteOffset], [length]);
new TypedArray(object); // array-like object
new TypedArray(typedArray); // copy and convert values
new TypedArray(length);
new TypedArray();
```

* `arr.buffer` – references the ArrayBuffer.
* `arr.byteLength` – the length of the ArrayBuffer.

### Out-of-bounds behavior

If we attempt to write an out-of-bounds value into a typed array, extra bits on the left are cut-off.

### TypedArray methods

* No `splice`: we can't delete a value because they are contiguous areas of memory.
* No `concat`
* `arr.set(fromArr, [offset])`: copies all elements from `fromArr` to the `arr`, starting at position `offset`
* `arr.subarray([begin, end])`: creates a new view of the same type from `begin` to `end`

### DataView

`DataView` is a special super-flexible untyped view over `ArrayBuffer`. It allows to access the data on any offset in any format.

```javascript
new DataView(buffer, [byteOffset], [byteLength])
```

With `DataView` we can access the data with methods like `.getUint8(i)` or `.getUint16(i)`. It is great when we store mixed-format data in the same buffer.

## 2.2 TextDecoder and TextEncoder

The build-in `TextDecoder` object allows to read the value into an actual string, given the buffer and the encoding.

### TextDecoder

```javascript
let decoder = new TextDecoder([label], [options]);
```

* `label`: the encoding, `utf-8` by default
* `options`
* * `fatal`: boolean, if `true` then throw an exception for invalid characters
* * `ignoreBOM`: boolean, if `true` then ignore BOM

```javascript
let str = decoder.decode([input], [options]);
```

* `input`: `BufferSource` to decode
* `options`
* * `stream`: `true` for decoding streams, when decoder is called repeatedly with incoming chunks of data.

### TextEncoder

`TextEncoder` converts a string into bytes.

```javascript
let encoder = new TextEncoder();
```

* `encode(str)`: returns `Uint8Array` from a string
* `encodeInto(str, destination)`: encodes `str` into `destination` that is the `Uint8Array`

## 2.3 Blob

`Blob` consists of an optional string `type` \(a MIME-type usually\), plus `blobParts` – a sequence of other `Blob` objects, strings and `BufferSource`.

```javascript
new Blob(blobParts, options);
```

* `blobParts`: an array of `Blob`/`BufferSource`/`String`
* `options`
* * `type`: `Blog` type
* * `endings`: whether to transform end-of-line

```javascript
let blob = new Blob(["<html>…</html>"], {type: 'text/html'});
```

### Extract

```javascript
blob.slice([byteStart], [byteEnd], [contentType]);
```

We can’t change data directly in a `Blob`, but we can slice it and create new `Blob` objects from them.

### Blob as URL

`URL.createObjectURL` takes a Blob and creates a unique URL for it, in the form `blob:<origin>/<uuid>`.

```javascript
let blob = new Blob(["Hello, world!"], {type: 'text/plain'});

link.href = URL.createObjectURL(blob);
```

A generated URL is only valid within the current document, while it’s open. If we create a URL, that `Blob` will hang in memory, even if not needed any more.

`URL.revokeObjectURL(url)` removes the reference from the internal mapping.

### Blob to base64

Base64 represents binary data as a string of ultra-safe readable characters with ASCII-codes from 0 to 64.

A data url has the form `data:[<mediatype>][;base64],<data>`.

```markup
<img src="data:image/png;base64,R0lGODlhDAAMAKIFAF5LAP/zxAAAANyuAP/gaP///wAAAAAAACH5BAEAAAUALAAAAAAMAAwAAAMlWLPcGjDKFYi9lxKBOaGcF35DhWHamZUW0K4mAbiwWtuf0uxFAgA7">
```

### Image to blob

Image operations are done via `<canvas>` element.

### From Blob to ArrayBuffer

```javascript
let fileReader = new FileReader();
fileReader.readAsArrayBuffer(blob);
```

## 2.4 File and FileReader

### File

A `File` object inherits from Blob and is extended with filesystem-related capabilities.

```javascript
new File(fileParts, fileName, [options])
```

* `options`
* * `lastModified`

### FileReader

```javascript
let reader = new FileReader();
```

* `readAsArrayBuffer(blob)`
* `readAsText(bot, encoding)`
* `readAsDataURL(blob)`
* `reader.error` is the error.
* `FileReaderSync` is available inside Web Workers.

