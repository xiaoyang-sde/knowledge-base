# Chapter 6: Advanced working with functions

## 6.1 Recursion and Stack

If you are familiar with programming, you may skip this chapter.

## 6.2 Rest parameters and spread syntax

### Rest parameters ...

The rest parameters must be at the end.

```javascript
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

```javascript
function showName() {
  alert( arguments.length ); // 2
}

showName("Julius", "Caesar");
```

Arrow functions do not have "arguments".

### Spread syntax

`...arr` "expands" an iterable object arr into the list of arguments.

```javascript
alert( Math.max(arr) ); // NaN
alert( Math.max(...arr) );
alert( Math.max(1, ...arr1, 2, ...arr2, 25) );
```

The spread syntax works only with iterables. It internally uses iterators to gather elements, the same way as for..of does.

```javascript
// merge arrays
let arr = [3, 5, 1];
let arr2 = [8, 9, 15];

let merged = [0, ...arr, 2, ...arr2];

// turn string into an array of characters
let str = "Hello";

alert( [...str] ); // H,e,l,l,o
```

### Get a new copy of an array/object

```javascript
let arr = [1, 2, 3];
let arrCopy = [...arr];

let obj = { a: 1, b: 2, c: 3 };
let objCopy = { ...obj };
```

## 6.3 Variable scope, closure

### Code blocks

If a variable is declared inside a code block `{...}`, it’s only visible inside that block.

For if, for, while and so on, variables declared in `{...}` are only visible inside.

### Nested functions

A function is called “nested” when it is created inside another function. It can be returned by the outer function.

### Lexical Environment

#### Step 1. Variables

Every running function, code block `{...}`, and the script as a whole have an internal associated object known as the Lexical Environment.

1. Environment Record – an object that stores all local variables as its properties. A “variable” is just a property of the special internal object, Environment Record.
2. A reference to the outer lexical environment, the one associated with the outer code.
3. When the script starts, the Lexical Environment is pre-populated with all declared variables.
4. Then `let phrase` definition changes the value to `undefined`.
5. `phrase` is assigned a value.
6. `phrase` changes the value.

#### Step 2. Function Declarations

A Function Declaration is instantly fully initialized. That’s why we can use a function even before the declaration itself. \(This behavior only applies to Function Declarations.\)

#### Step 3. Inner and outer Lexical Environment

During the function call we have two Lexical Environments: the inner one \(for the function call\) and the outer one \(global\). The inner Lexical Environment has a reference to the outer one.

When the code wants to access a variable, the inner Lexical Environment is searched first, then the outer one, then the more outer one, and so on until the global one.

#### Step 4. Returning a function

All functions remember the Lexical Environment in which they were made. The `[[Environment]]` reference is set once and forever at function creation time.

```javascript
function makeCounter() {
  let count = 0;

  return function() {
    return count++;
  };
}
```

When the code inside `counter()` looks for count variable, it first searches its own Lexical Environment \(empty, as there are no local variables there\), then the Lexical Environment of the outer `makeCounter()` call, where it finds and changes it.

**A variable is updated in the Lexical Environment where it lives.**

A closure is a function that remembers its outer variables and can access them. In JavaScript, they automatically remember where they were created using a hidden `[[Environment]]` property, and then their code can access outer variables.

### Garbage collection

If there’s a nested function that is still reachable after the end of a function, then it has `[[Environment]]` property that references the outer lexical environment. A Lexical Environment object dies when it becomes unreachable.

```javascript
function f() {
  let value = 123;

  return function() {
    alert(value);
  }
}

let g = f(); // while g function exists, the value stays in memory

g = null; // ...and now the memory is cleaned up
```

### Real-life optimizations

In theory while a function is alive, all outer variables are also retained. An important side effect in V8 \(Chrome, Opera\) is that such variable will become unavailable in debugging.

They analyze variable usage and if it’s obvious from the code that an outer variable is not used, remove it.

## 6.4 The old "var"

### "var has no block scope

Variables, declared with var, are either function-wide or global. They are visible through blocks.

```javascript
if (true) {
  var test = true;
}

alert(test); // true
```

If a code block is inside a function, then var becomes a function-level variable.

### “var” tolerates redeclarations

```javascript
var user = "Pete";

var user = "John";
```

### “var” variables can be declared below their use

`var` declarations are processed when the function starts \(or script starts for globals\).

```javascript
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

```javascript
(function() {
    // regular function declaration requires a name
    // use () to avoid that limitation

  let message = "Hello";

  alert(message); // Hello

})();
```

## 6.5 Global object

The global object provides variables and functions that are available anywhere. By default, those that are built into the language or the environment.

* `window`: Browswer
* `global`: Node.js
* `globalThis`: Standard name for a global object \(Recent change\)

In a browser, global functions and variables declared with `var` \(not `let`/`const`!\) become the property of the global object.

```javascript
var gVar = 5;

alert(window.gVar);

// can be written directly as a property
window.currentUser = {
  name: "John"
};
```

### Using for polyfills

We use the global object to test for support of modern language features.

For instance, test if a built-in Promise object exists \(it doesn’t in really old browsers\):

```javascript
if (!window.Promise) {
  window.Promise = ... // custom implementation of the modern language feature
}
```

## 6.6 Function object, NFE

### The "name" property

Function objects contain some useable properties.

A function’s name is accessible as the `name` property. It also assigns the correct name to a function even if it’s created without one.

```javascript
let sayHi = function() {
  alert("Hi");
};

alert(sayHi.name); // sayHi
```

### The “length” property

`length` property returns the number of function parameters.

### Custom properties

A property assigned to a function does not define a local variable counter inside it. It can replace closure, but the property can be modified by outer codes.

```javascript
function sayHi() {
  sayHi.counter++;
}
sayHi.counter = 0; // initial value
```

### Named Function Expression

1. It allows the function to reference itself internally.
2. It is not visible outside of the function.

```javascript
let sayHi = function func(who) {
  if (who) {
    alert(`Hello, ${who}`);
  } else {
    func("Guest"); // use func to re-call itself
  }
};

sayHi();
```

## 6.7 The "new Function" syntax

### Syntax

The function is created literally from a string, that is passed at run time. For example, we can receive a new function from a server and then execute it, or dynamically compile a function from a template, in complex web-applications.

```javascript
let func = new Function ([arg1, arg2, ...argN], functionBody);

let sayHi = new Function('alert("Hello")');

let sum = new Function('a', 'b', 'return a + b');
```

### Closure

When a function is created using new Function, its `[[Environment]]` is set to reference not the current Lexical Environment, but the global one.

If `new Function` had access to outer variables, it would have problems with minifiers because outer varaibles are renamed into shorter ones.

## 6.8 Scheduling: setTimeout and setInterval

### setTimeout and clearTimeout

```javascript
let timerId = setTimeout(func|code, [delay], [arg1], [arg2], ...)

clearTimeout(timerId);
```

### setInterval

```javascript
let timerId = setInterval(func|code, [delay], [arg1], [arg2], ...)
```

The real delay between func calls for `setInterval` is less than in the code, because the time taken by func's execution "consumes" a part of or even higher than the interval.

The nested `setTimeout` guarantees the fixed delay.

```javascript
setTimeout(function run() {
  func();
  setTimeout(run, 100);
}, 100);
```

### Zero delay setTimeout

The scheduler will invoke the function only after the currently executing script is complete.

```javascript
setTimeout(() => alert("World")); // Second

alert("Hello"); // First
```

In browser, after five nested timers, the interval is forced to be at least 4 milliseconds.

## 6.9 Decorators and forwarding, call/apply

### Transparent caching

```javascript
function slow(x) {
  // there can be a heavy CPU-intensive job here
  alert(`Called with ${x}`);
  return x;
}

function cachingDecorator(func) {
  let cache = new Map();

  return function(x) {
    if (cache.has(x)) {    // if there's such key in cache
      return cache.get(x); // read the result from it
    }

    let result = func(x);  // otherwise call func

    cache.set(x, result);  // and cache (remember) the result
    return result;
  };
}

slow = cachingDecorator(slow);
```

The result of `cachingDecorator(func)` is a “wrapper”: `function(x)` that “wraps” the call of `func(x)` into caching logic.

### func.call

`func.call()` runs func providing the first argument as this, and the next as the arguments.

```javascript
func.call(context, arg1, arg2, ...);

func.call(obj, 1, 2, 3);
```

### func.apply

* `call` accepts an iterable \(...\)
* `apply` accepts an array-like object

```javascript
func.apply(context, args);

let wrapper = function() {
  return func.apply(this, arguments);
};
```

### Borrowing a method

To let any iterable and array-like to use the `join` method:

```javascript
function hash() {
  alert( [].join.call(arguments) ); // 1,2
}

hash(1, 2);
```

Technically the `join` method takes `this` and joins `this[0]`, `this[1]`...etc together.

## 6.10 Function binding

### Losing “this”

```javascript
let user = {
  firstName: "John",
  sayHi() {
    alert(`Hello, ${this.firstName}!`);
  }
};

setTimeout(user.sayHi, 1000); // Hello, undefined!
```

That’s because `setTimeout` got the function `user.sayHi`, separately from the object.

```javascript
let f = user.sayHi;
setTimeout(f, 1000);
```

### Solution 1: a wrapper

```javascript
setTimeout(() => user.sayHi(), 1000); // Hello, John!
```

Vulnerability: Before `setTimeout` triggers, `user` changes value. Thus it will call a wrong object.

### Solution 2: bind

Calling `boundFunc` is like `func` with fixed `this`.

```javascript
let boundFunc = func.bind(context);
```

We take the method `user.sayHi` and bind it to `user`.

```javascript
let user = {
  firstName: "John",
  sayHi() {
    alert(`Hello, ${this.firstName}!`);
  }
};

setTimeout(user.sayHi.bind(user), 1000);
```

### Partial functions

We can bind `this` and arguments to a function.

```javascript
let bound = func.bind(context, [arg1], [arg2], ...);
```

Use `bind` to create a function `double` on `mul`:

```javascript
function mul(a, b) {
  return a * b;
}

let double = mul.bind(null, 2);

double(3); // = mul(2, 3) = 6
```

For instance, we have a function `send(from, to, text)`. Inside a user object we may want a partial function `sendTo(to, text)` that sends from the current user.

### Going partial without context

If we’d like to fix some arguments, but not the context `this`, use this function instead of the native `bind`:

```javascript
function partial(func, ...argsBound) {
  return function(...args) { // (*)
    return func.call(this, ...argsBound, ...args);
  }
}
```

## 6.11 Arrow functions revisited

* Have no `this`. If `this` is accessed, it is taken from the outside.
* Have no `arguments`.
* Have no `super`.
* Can’t run with `new`.

