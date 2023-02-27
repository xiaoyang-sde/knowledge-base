# Chapter 5: Data types

## 5.1 Methods of primitives

* Primitives except null and undefined provide many helpful methods.
* Formally, these methods work via temporary objects, but JavaScript engines are well tuned to optimize that internally, so they are not expensive to call.

### A primitive as an object

* There are many things one would want to do with a primitive like a string or a number. It would be great to access them as methods.
* Primitives must be as fast and lightweight as possible.
* Primitives are still primitive. A single value, as desired.
* The language allows access to methods and properties of strings, numbers, booleans and symbols.
* In order for that to work, a special "object wrapper" that provides the extra functionality is created, and then is destroyed.

```javascript
let str = "Hello";

alert(str.toUpperCase()); // HELLO
```

1. The string str is a primitive. So in the moment of accessing its property, a special object is created that knows the value of the string, and has useful methods, like `toUpperCase()`.
2. That method runs and returns a new string \(shown by alert\).
3. The special object is destroyed, leaving the primitive str alone.

Don't use constructors like `String`, `Number`, `Boolean`:

```javascript
alert(typeof new Number(0)); // "object"!
```

* Objects are always truthy in if.
* `null`/`undefined` have no methods.
* Permitives \(temporary objects\) can't store additional data. \(Error in strict mode, `undefined` in regular mode.\)

## 5.2 Numbers

* Double precision floating point numbers
* BigInt numbers

### More ways to write a number

```javascript
let billion = 1000000000;
let billion = 1e9;  // 1 billion, literally: 1 and 9 zeroes
let ms = 1e-6; // six zeroes to the left from 1
```

### Hex, binary and octal numbers

```javascript
alert( 0xff ); // 255
let a = 0b11111111; // binary form of 255
let b = 0o377; // octal form of 255
alert( a == b ); // true, the same number 255 at both sides
```

### toString\(base\)

The method num.toString\(base\) returns a string representation of num in the numeral system with the given base.

If we want to call a method directly on a number, like toString in the example above, then we need to place two dots `..` after it or wrap it in a parentheses.

```javascript
alert( 123456..toString(36) ); // 2n9c
(123456).toString(36);
```

### Rounding

* `Math.floor` \(Rounds down\)
* `Math.ceil` \(Rounds up\)
* `Math.round` \(Nearst integer\)
* `Math.trunc` \(Remove anything after the decimal point\)

To round a number into n-th digits:

1. Multiply, floor, and divide
2. toFixed\(n\), which returns a string \(padding with zeros if necessary\)

### Imprecise calculations

Number: 52 of them are used to store the digits, 11 of them store the position of the decimal point \(they are zero for integer numbers\), and 1 bit is for the sign.

#### Integer overflow

```javascript
alert( 1e500 ); // Infinity
```

#### Loss of precision

```javascript
alert( 0.1 + 0.2 == 0.3 ); // 0.30000000000000004
```

Use `toFixed(n)` to solve this problem.

### Tests: isFinite and isNaN

* `Infinity`\(and `-Infinity`\) is a special numeric value that is greater \(less\) than anything.
* `NaN` represents an error.
* `isNaN(value)` converts its argument to a number and then tests it for being `NaN`.
* The value of `NaN` is unique, and it does not equal anything, including itself.
* `isFinite(value)` converts its argument to a number and returns `true` if it’s a regular number, not `NaN/Infinity/-Infinity`.
* Sometimes `isFinite` is used to validate whether a string value is a regular number. \(An empty or a space-only string is treated as 0.\)

## parseInt and parseFloat

They "read" a number from a string until they can’t. In case of an error, the gathered number is returned.

```javascript
alert(parseInt('100px')); // 100
alert(parseFloat('12.5em')); // 12.5
```

The second argument of parseInt\(\) is used to specifies the base of the numeral system.

### Other math functions

* `Math.random()`: returns a random number from 0 to 1 \(not including 1\)
* `Math.max()`, `Math.min()`
* `Math.pow()`

## 5.3 Strings

* Strings can be enclosed within either single quotes, double quotes, or backticks.
* It is still possible to create multiline strings with single and double quotes by using a so-called “newline character”, written as `\n`, which denotes a line break.
* The length property has the string length.
* To get a character at position pos, use square brackets \[pos\] or call the method `str.charAt(pos)`. Square brackets return undefind, but charAt returns an empty string.
* We can also iterate over characters using for..of.

```javascript
for (let char of "Hello") {
  alert(char); // H,e,l,l,o (char becomes "H", then "e", then "l" etc)
}
```

Strings can’t be changed in JavaScript and it is impossible to change a character. The usual workaround is to create a whole new string and assign it to str.

Methods `toLowerCase()` and `toUpperCase()` change the case.

### Searching for a substring

* `str.indexOf(substr, pos)` \(pos is the starting position\)
* `str.lastIndexOf(substr, position)`
* `if (~str.indexOf(...))` reads as "if found". \(Ancient Bitwise NOT trick\)
* `includes`, `startsWith`, `endsWith`

### Getting a substring

* `str.slice(start [, end])`
* `str.substring(start [, end])` \(start could be greater than end\)
* `str.substr(start [, length])`
* `str.trim()` – removes \(“trims”\) spaces from the beginning and end of the string.
* `str.repeat(n)` – repeats the string n times.

### Comparing Strings

* `str.codePointAt(pos)` \(z -&gt; 122\)
* `str.fromCodePoint(code)` \(122 -&gt; z, \u005a -&gt; z\)

The characters are compared by their numeric code. \(Ö &gt; a &gt; Z\)

Use str.localeCompare\(str2\) to correctly handle all cases:

```javascript
alert( 'Österreich'.localeCompare('Zealand') ); // -1
```

### Internals, Unicode

Surrogate pairs: Rare symbols are encoded with a pair of 2-byte characters called a surrogate pair.

```javascript
alert( '𝒳'.length ); // 2, MATHEMATICAL SCRIPT CAPITAL X
alert( '😂'.length ); // 2, FACE WITH TEARS OF JOY
```

```javascript
alert( '𝒳'[0] ); // garbage
alert( '𝒳'[1] ); // garbage
```

If a character has the code in the interval of 0xd800..0xdbff, then it is the first part of the surrogate pair. The next character \(second part\) must have the code in interval 0xdc00..0xdfff.

```javascript
alert( '𝒳'.charCodeAt(0).toString(16) ); // d835, between 0xd800 and 0xdbff
alert( '𝒳'.charCodeAt(1).toString(16) ); // dcb3, between 0xdc00 and 0xdfff
```

UTF-16 allows us to use several unicode characters: the base character followed by one or many "mark" characters that "decorate" it.

```javascript
alert( 'S\u0307' ); // Ṡ
alert( 'S\u0307\u0323' ); // Ṩ
```

"S\u0307\u0323".normalize\(\) will bring that into one unicode: \(\u1e68\)

## 5.4 Arrays

### Declaration

```javascript
let arr = new Array();
let arr = [];
```

The total count of the elements in the array is its length property.

### Methods pop/push, shift/unshift

* `push` adds an element to the end.
* `pop` takes an element from the end.
* `unshift` adds an element to the beginning.
* `shift` takes an element from the beginning.

### Internals

Array is an object and thus behaves like an object. It is copied by reference. If we add propertie to the array, the engine will disable all of its array-specific optimizations.

Methods push/pop run fast O\(1\), while shift/unshift are slow O\(N\).

### Loops

```javascript
for (let fruit of fruits) {
  alert( fruit );
}
```

* The loop for..in iterates over all properties, not only the numeric ones.
* The `for..in` loop is optimized for generic objects, not arrays, and thus is 10-100 times slower.

### A word about "length"

It's not the count of values in the array, but the greatest numeric index plus one, and it's writable. If we decrease it, the array is truncated.

```javascript
fruits[123] = "Apple";
alert( fruits.length ); // 124
```

### new Array\(\)

If new Array is called with a single argument which is a number, then it creates an array without items, but with the given length. new Array\(number\) has all elements undefined:

```javascript
let arr = new Array(2);
```

### toString

toString method of arrays returns a comma-separated list of elements.

## 5.5 Array methods

### splice

```javascript
arr.splice(index[, deleteCount, elem1, ..., elemN])
```

It starts from the position index: removes `deleteCount` elements and then inserts `elem1, ..., elemN` at their place. Returns the array of removed elements.

### concat

```javascript
arr.concat(arg1, arg2...);

alert( arr.concat([3, 4], [5, 6]) ); // 1,2,3,4,5,6
alert( arr.concat([3, 4], 5, 6) ); // 1,2,3,4,5,6
```

If an array-like object has a special `Symbol.isConcatSpreadable` property, then it’s treated as an array by `concat`.

### Iterate: forEach

The `arr.forEach` method allows to run a function for every element of the array.

```javascript
arr.forEach(function(item, index, array) {
  // ... do something with item
});
```

### indexOf/lastIndexOf and includes

* `arr.indexOf(item, from)` – looks for item starting from index from, and returns the index where it was found, otherwise -1.
* `arr.lastIndexOf(item, from)` – same, but looks for from right to left.
* `arr.includes(item, from)` – looks for item starting from index from, returns true if found. \(correctly handles NaN\)

### find and findIndex

```javascript
let result = arr.find(function(item, index, array) {
  // if true is returned, item is returned and iteration is stopped
  // for falsy scenario returns undefined
});
```

`findIndex` returns the index or -1 instead of the element itself.

### filter

The find method looks for a single \(first\) element that makes the function return true.

```javascript
let results = arr.filter(function(item, index, array) {
  // if true item is pushed to results and the iteration continues
  // returns empty array if nothing found
});
```

### map

It calls the function for each element of the array and returns the array of results.

```javascript
let result = arr.map(function(item, index, array) {
  // returns the new value instead of item
});
```

### sort\(fn\)

The items are sorted as **strings** by default.

```javascript
function compareNumeric(a, b) {
  if (a > b) return 1;
  if (a == b) return 0;
  if (a < b) return -1;
}

let arr = [ 1, 2, 15 ];

arr.sort(compareNumeric);
arr.sort((a, b) => a - b); // works exactly the same
```

### reverse

The method `arr.reverse` reverses the order of elements in arr.

### split and join

```javascript
let arr = 'Bilbo, Gandalf, Nazgul, Saruman'.split(', ', 2);

alert(arr); // Bilbo, Gandalf

let str = arr.join(';'); // glue the array into a string using ;
```

Split into letters:

```javascript
let str = "test";

alert( str.split('') ); // t,e,s,t
```

### reduce/reduceRight

```javascript
let value = arr.reduce(function(accumulator, item, index, array) {
  // ...
}, [initial]);
```

If there’s no initial, then reduce takes the first element of the array as the initial value and starts the iteration from the 2nd element.

If the array is empty, then reduce call without initial value gives an error.

The method `arr.reduceRight` does the same, but goes from right to left.

### Array.isArray

It returns true if the value is an array, and false otherwise.

```javascript
alert(typeof []); // object
alert(Array.isArray({})); // false
```

### thisArg

```javascript
arr.find(func, thisArg);
arr.filter(func, thisArg);
arr.map(func, thisArg);
// ...
// thisArg is the optional last argument
```

The value of thisArg parameter becomes this for func.

## 5.6 Iterables

Iterable objects is a generalization of arrays.

### Symbol.iterator

```javascript
let range = {
  from: 1,
  to: 5
};

// 1. call to for..of initially calls this
range[Symbol.iterator] = function() {

  // ...it returns the iterator object:
  // 2. Onward, for..of works only with this iterator, asking it for next values
  return {
    current: this.from,
    last: this.to,

    // 3. next() is called on each iteration by the for..of loop
    next() {
      // 4. it should return the value as an object {done:.., value :...}
      if (this.current <= this.last) {
        return { done: false, value: this.current++ };
      } else {
        return { done: true };
      }
    }
  };
};
```

To make `for...of` work, we need to add a method to the `Symbol.iterator`.

* The method must return an iterator – an object with the method `next`.
* When `for..of` wants the next value, it calls `next()` on that object.
* The result of `next()` must have the form `{done: Boolean, value: any}`

## Calling an iterator explicitly

```javascript
let iterator = str[Symbol.iterator]();

while (true) {
  let result = iterator.next();
  if (result.done) break;
  alert(result.value); // outputs characters one by one
}
```

### Iterables and array-likes

* Iterables are objects that implement the `Symbol.iterator` method, as described above.
* Array-likes are objects that have indexes and `length`, so they look like arrays.

Strings are both iterable \(for..of works on them\) and array-like \(they have numeric indexes and length\).

```javascript
let arrayLike = { // has indexes and length => array-like
  0: "Hello",
  1: "World",
  length: 2
};
```

### Array.from

`Array.from` takes an iterable or array-like value and makes a “real” Array from it.

```javascript
Array.from(obj[, mapFn, thisArg]);

let arr = Array.from(range);
alert(arr);
```

## 5.7 Map and Set

### Map

Map is a collection of keyed data items, just like an Object. Map allows keys of any type, such as objects or NaN.

* `new Map()` – creates the map.
* `map.set(key, value)` – stores the value by the - \`key.
* `map.get(key)` – returns the value by the key, undefined if key doesn’t exist in map.
* `map.has(key)` – returns true if the key exists, - \`false otherwise.
* `map.delete(key)` – removes the value by the key.
* `map.clear()` – removes everything from the map.
* `map.size` – returns the current element count.

Although `map[key]` also works, this is treating map as a plain JavaScript object, so it implies all corresponding limitations.

### Iteration over Map

* `map.keys()` – returns an iterable for keys,
* `map.values()` – returns an iterable for values,
* `map.entries()` – returns an iterable for entries `[key, value]`, it’s used by default in `for..of`.

The iteration goes in the same order as the values were inserted. Map also has a built-in `forEach` method.

### Object.entries: Map from Object

```javascript
let map = new Map([
  ['1',  'str1'],
  [1,    'num1'],
  [true, 'bool1']
]);

let map = new Map(Object.entries(obj));
```

### Object.fromEntries: Object from Map

```javascript
let map = new Map();
map.set('banana', 1);
map.set('orange', 2);
map.set('meat', 4);

let obj = Object.fromEntries(map);
```

### Set

A Set is a special type collection – “set of values” \(without keys\), where each value may occur only once.

* `new Set(iterable)` – creates the set, and if an iterable object is provided \(usually an array\), copies values from it into the set.
* `set.add(value)` – adds a value, returns the set itself.
* `set.delete(value)` – removes the value, returns true if value existed at the moment of the call, otherwise false.
* `set.has(value)` – returns true if the value exists in the set, otherwise false.
* `set.clear()` – removes everything from the set.
* `set.size` – is the elements count.

### Iteration over Set

```javascript
for (let value of set) alert(value);

// the same with forEach:
set.forEach((value, valueAgain, set) => {
  alert(value);
});
```

* `set.keys()` – returns an iterable object for values,
* `set.values()` – same as set.keys\(\), for compatibility with Map,
* `set.entries()` – returns an iterable object for entries `[value, value]`, exists for compatibility with Map.

## 5.8 WeakMap and WeakSet

WeakMap doesn’t prevent garbage-collection of key objects.

### WeakMap

WeakMap keys must be objects, not primitive values.

If there are no other references to that key, it will be removed from memory and from the map automatically.

WeakMap does not support iteration and methods `keys(), values(), entries()`, since it’s not exactly specified when the cleanup happens.

* `weakMap.get(key)`
* `weakMap.set(key, value)`
* `weakMap.delete(key)`
* `weakMap.has(key)`

### Use case

#### additional data

If we’re working with an object that belongs to another code and would like to store some data associated with it, that should only exist while the object is alive, then WeakMap is exactly what’s needed.

For instance, we have code that keeps a visit count for users. When a user leaves \(its object gets garbage collected\), we don’t want to store their visit count anymore.

#### caching

When a function result should be remembered \(“cached”\), so that future calls on the same object reuse it.

```javascript
let cache = new WeakMap();

// calculate and remember the result
function process(obj) {
  if (!cache.has(obj)) {
    let result = /* calculate the result for */ obj;

    cache.set(obj, result);
  }

  return cache.get(obj);
}
```

### WeakSet

It is analogous to Set, but we may only add objects to WeakSet \(not primitives\).

An object exists in the set while it is reachable from somewhere else.

Like Set, it supports `add`, `has` and `delete`, but not `size`, `keys()` and no iterations.

## 5.9 Object.keys, values, entries

* `Object.keys(obj)` – returns an array of keys.
* `Object.values(obj)` – returns an array of values.
* `Object.entries(obj)` – returns an array of \[key, value\] pairs.

These methods ignore properties that use `Symbol(...)` as keys.

### Transforming objects

To use `map`, `filter` or other Array-specific methods on an object, transform it to an array and turn it back.

```javascript
let doublePrices = Object.fromEntries(
  // convert to array, map, and then fromEntries gives back the object
  Object.entries(prices).map(([key, value]) => [key, value * 2])
);
```

## 5.10 Destructuring assignment

Destructuring assignment is a special syntax that allows us to “unpack” arrays, iterables, or objects into a bunch of variables.

### Array destructuring

```javascript
let arr = ["Ilya", "Kantor"];
let [firstName, surname] = arr;

let [firstName, surname] = "Ilya Kantor".split(' ');

let [firstName, , title] = ["Julius", "Caesar", "Consul", "of the Roman Republic"]; // ignore elements using commas
```

#### Works with any iterable on the right-side

```javascript
let user = {};
[user.name, user.surname] = "Ilya Kantor".split(' ');
```

#### Looping with .entries\(\)

```javascript
for (let [key, value] of Object.entries(user)) {
  alert(`${key}:${value}`); // name:John, then age:30
}
```

#### Swap variables trick

```javascript
[guest, admin] = [admin, guest];
```

### The rest ‘…’

```javascript
let [name1, name2, ...rest] = ["Julius", "Caesar", "Consul", "of the Roman Republic"];

alert(rest[0]); // Consul
```

### Default values

```javascript
// default values
let [name = "Guest", surname = "Anonymous"] = ["Julius"];
let [name = prompt('name?'), surname = prompt('surname?')] = ["Julius"];
```

### Object destructuring

```javascript
let options = {
  title: "Menu",
  width: 100,
  height: 200
};

let {title, width, height} = options;
let {width: w, height: h, title} = options;
let {width: w = 100, height: h = 200, title} = options;

// only extract title as a variable
let { title } = options;

let title, width, height;
// assign without let
({title, width, height} = {title: "Menu", width: 200, height: 100});
```

### The rest pattern “…”

```javascript
// rest = object with the rest of properties
let {title, ...rest} = options;
```

### Nested destructuring

```javascript
let options = {
  size: {
    width: 100,
    height: 200
  },
  items: ["Cake", "Donut"],
  extra: true
};

// destructuring assignment split in multiple lines for clarity
let {
  size: { // put size here
    width,
    height
  },
  items: [item1, item2], // assign items here
  title = "Menu" // not present in the object (default value is used)
} = options;
```

### Smart function parameters

```javascript
// we pass object to function
let options = {
  title: "My menu",
  items: ["Item1", "Item2"]
};

// it immediately expands it to variables
function showMenu({title = "Untitled", width = 200, height = 100, items = []} = {}) {
    // = {} set the default arguments object to {}
    return title, width, height, items
}

showMenu(options);
```

## 5.11 Date and time

### Creation

* `new Date()`: create a Date object for the current date and time.
* `new Date(milliseconds)`: create a Date object with the time equal to number of millisecondsa Date object with the time equal to number of milliseconds. \(Dates before 01.01.1970 have negative timestamps\)
* `new Date(datestring)`: date strings are parsed automatically.
* `new Date(year, month, date, hours, minutes, seconds, ms)`: Only the first two arguments are obligatory.

### Access date components

* `getFullYear()`: 4 digits
* `getMonth()`: From 0 to 11
* `getDate()`: From 1 to 31
* `getHours(), getMinutes(), getSeconds(), getMilliseconds()`
* `getDay()`: From 0 \(Sunday\) to 6 \(Saturday\).

All the methods above return the components relative to the local time zone.

* `getTime()`: Returns the timestamp for the date.
* `getTimezoneOffset()`: Returns the difference between UTC and the local time zone, in minutes.

### Autocorrection

We can set out-of-range values, and the Date object will auto-adjust itself. Out-of-range date components are distributed automatically.

### Date to number, date diff

Dates can be subtracted, giving their difference in milliseconds. Dates becomes the timestamp when converted to a number.

```javascript
let date = new Date();
alert(+date); // the number of milliseconds, same as date.getTime()
```

### Date.now\(\)

There’s a special method `Date.now()` that returns the current timestamp. It doesn’t create an intermediate `Date` object.

### Benchmarking

For more reliable benchmarking, the whole pack of benchmarks should be rerun multiple times.

Modern JavaScript engines start applying advanced optimizations only to “hot code” that executes many times.

### Date.parse from a string

`Date.parse(str)` parses the string in the given format and returns the timestamp or NaN.

The string format should be: `YYYY-MM-DDTHH:mm:ss.sssZ`, where:

* `YYYY-MM-DD` – is the date: year-month-day.
* The character "T" is used as the delimiter.
* `HH:mm:ss.sss` – is the time: hours, minutes, seconds and milliseconds.
* The optional 'Z' part denotes the time zone in the format +-hh:mm. A single letter Z that would mean UTC+0.

```javascript
let ms = Date.parse('2012-01-26T13:51:50.417-07:00');

alert(ms); // 1327611110417  (timestamp)
```

## 5.12 JSON methods, toJSON

### JSON.stringify

```javascript
let student = {
  name: 'John',
  age: 30,
  isAdmin: false,
  courses: ['html', 'css', 'js'],
  wife: null
};

let json = JSON.stringify(student);
```

JSON supports following data types:

* Objects { ... }
* Arrays \[ ... \]
* Primitives:
* * strings,
* * numbers,
* * boolean values true/false,
* * null.

Function, Symbol, and Properties that are `undefined` are skipped by `JSON.stringify`. There must be no circular references.

### Excluding and transforming: replacer

```javascript
let json = JSON.stringify(value[, replacer, space])
```

* `value`: A value to encode.
* `replacer`: Array of properties to encode or a mapping function `function(key, value)`.
* `space`: Amount of space to use for formatting.

```javascript
let room = {
  number: 23
};

let meetup = {
  title: "Conference",
  participants: [{name: "John"}, {name: "Alice"}],
  place: room // meetup references room
};

room.occupiedBy = meetup; // room references meetup

alert( JSON.stringify(meetup, function replacer(key, value) {
  return (key == 'occupiedBy') ? undefined : value;
}));
```

### Formatting: space

For example, `space = 4` tells JavaScript to show nested objects on multiple lines, with indentation of 4 spaces inside an object.

### Custom “toJSON”

```javascript
let room = {
  number: 23,
  toJSON() {
    return this.number;
  }
};

alert(JSON.stringify(room)); // 23
```

### JSON.parse

```javascript
let value = JSON.parse(str, [reviver]);
```

* `str`: JSON-string to parse.
* `reviver`: Optional `function(key,value)` that will be called for each `(key, value)` pair and can transform the value.

### Using reviver

```javascript
let schedule = `{
  "meetups": [
    {"title":"Conference","date":"2017-11-30T12:00:00.000Z"},
    {"title":"Birthday","date":"2017-04-18T12:00:00.000Z"}
  ]
}`;

let meetup = JSON.parse(str, function(key, value) {
  if (key == 'date') return new Date(value);
  return value;
});

alert( schedule.meetups[1].date.getDate() );
```

