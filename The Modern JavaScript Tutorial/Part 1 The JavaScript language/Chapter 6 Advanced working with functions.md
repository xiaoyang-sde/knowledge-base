# Chapter 6: Advanced working with functions

## 6.1 Recursion and Stack

If you are familiar with programming, you may skip this chapter.

## 6.2 Rest parameters and spread syntax

### Rest parameters ...

The rest parameters must be at the end.

```js
function sumAll(...args) {
    return;
}

sumAll(1, 2, 3);

function showName(firstName, lastName, ...titles) {
    alert( titles.length ); // 2
}

showName("Julius", "Caesar", "Consul", "Imperator");
```

### The "arguments" variable

There is also a special array-like and iterable object named arguments that contains all arguments by their index.

```js
function showName() {
  alert( arguments.length ); // 2
}

showName("Julius", "Caesar");
```

Arrow functions do not have "arguments".

### Spread syntax

`...arr` "expands" an iterable object arr into the list of arguments.

```js

alert( Math.max(arr) ); // NaN
alert( Math.max(...arr) );
alert( Math.max(1, ...arr1, 2, ...arr2, 25) );
```

The spread syntax works only with iterables. It internally uses iterators to gather elements, the same way as for..of does.

```js
// merge arrays
let arr = [3, 5, 1];
let arr2 = [8, 9, 15];

let merged = [0, ...arr, 2, ...arr2];

// turn string into an array of characters
let str = "Hello";

alert( [...str] ); // H,e,l,l,o
```

### Get a new copy of an array/object

```js
let arr = [1, 2, 3];
let arrCopy = [...arr];

let obj = { a: 1, b: 2, c: 3 };
let objCopy = { ...obj };
```

## 6.3 Variable scope, closure

## 6.4 The old "var"

### "var has no block scope

Variables, declared with var, are either function-wide or global. They are visible through blocks.

```js
if (true) {
  var test = true;
}

alert(test); // true
```

If a code block is inside a function, then var becomes a function-level variable.

### “var” tolerates redeclarations

```js
var user = "Pete";

var user = "John";
```

### “var” variables can be declared below their use

`var` declarations are processed when the function starts (or script starts for globals).

```js
function sayHi() {
  phrase = "Hello";

  alert(phrase);

  if (false) {
    var phrase; // code blocks are ignored
  }
}
sayHi();
```

Declarations are hoisted, but assignments are not. The assignment always works at the place where it appears

### IIFE

Immediately-invoked function expressions: programmers invented a way to emulate the block-level visibility for `var`.

```js
(function() { 
    // regular function declaration requires a name
    // use () to avoid that limitation

  let message = "Hello";

  alert(message); // Hello

})();
```
