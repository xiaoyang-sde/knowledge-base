# Chapter 5: The (Not So) Secret Lifecycle of Variables

## When Can I Use a Variable?

```js
greeting();
// Hello!

function greeting() {
  console.log("Hello!");
}
```

All identifiers are registered to their respective scopes during compile time. Moreover, every identifier is created at the beginning of the scope it belongs to, **every time that scope is entered**.

The term most commonly used for a variable being visible from the beginning of its enclosing scope, even though its declaration may appear further down in the scope, is called **hoisting**.

When a function declaration's name identifier is registered at the top of its scope, it's additionally auto-initialized to that function's reference. This behavior is called **function hoisting**.

Both function hoisting and var-flavored variable hoisting attach their name identifiers to the nearest enclosing function scope or global scope, not a block scope.

### Hoisting: Declaration vs. Expression

Function hoisting only applies to formal `function` declarations, not to `function` expression assignments.

```js
greeting();
// TypeError

var greeting = function greeting() {
    console.log("Hello!");
};
```

The error is not a `ReferenceError`. It's telling us that `greeting` was found but doesn't hold a function reference at that moment.

On that first line, `greeting` exists, but it holds only the default `undefined` value. It's not until line 4 that `greeting` gets assigned the function reference.

A `function` declaration is hoisted and initialized to its function value. A `var` variable is also hoisted, and then auto-initialized to `undefined`.

### Variable Hoisting

```js
greeting = "Hello!";
console.log(greeting);
// Hello!

var greeting = "Howdy!";
```

- The identifier is hoisted,
- It's automatically initialized to the value `undefined` from the top of the scope.

## Hoisting: Yet Another Metaphor

It's more useful to think of hoisting as a visualization of various actions JS takes in setting up the program **before execution**.

The hoisting proposes that JS pre-processes the original program and re-arranges it a bit, so that all the declarations have been moved to the top of their respective scopes, before execution.

```js
var greeting;           // hoisted declaration
greeting = "Hello!";    // the original line 1
console.log(greeting);  // Hello!
greeting = "Howdy!";    // `var` is gone!
```

The "rule" of the hoisting metaphor is that function declarations are hoisted first, then variables are hoisted immediately after all the functions.

The JS engine doesn't actually re-arrange the code. It can't magically look ahead and find declarations by fully parsing the code.

## Re-declaration?

```js
var studentName = "Frank";
console.log(studentName);
// Frank

var studentName;
console.log(studentName);   // ???
```

The code "generated" by hoisting:

```js
var studentName;
var studentName;    // clearly a pointless no-op!

studentName = "Frank";
console.log(studentName);
// Frank

console.log(studentName);
// Frank
```

`var studentName;` doesn't mean `var studentName = undefined;`.

```js
var studentName = "Frank";
console.log(studentName);   // Frank

var studentName;
console.log(studentName);   // Frank <--- still!

// let's add the initialization explicitly
var studentName = undefined;
console.log(studentName);   // undefined <--- see!?
```

```js
var greeting;

function greeting() {
    console.log("Hello!");
}

// basically, a no-op
var greeting;

typeof greeting;        // "function"

var greeting = "Hello!";

typeof greeting;        // "string"
```

When `let` is involved, the engine will throw a `SyntaxError`.

```js
let studentName = "Frank";

var studentName = "Suzy";
```

"Re-declaration" of variables is seen by some, including many on the TC39 body, as a bad habit that can lead to program bugs. So when ES6 introduced `let`, they decided to prevent "re-declaration" with an error.

### Constants?

Like `let`, `const` cannot be repeated with the same identifier in the same scope.

The `const` keyword requires a variable to be initialized, so omitting an assignment from the declaration results in a `SyntaxError`.

`const` declarations create variables that cannot be re-assigned.

If `const` declarations cannot be re-assigned, and `const` declarations always require assignments, then any `const` "re-declaration" would also necessarily be a `const` re-assignment, which can't be allowed.

### Loops

```js
var keepGoing = true;
while (keepGoing) {
    let value = Math.random();
    if (value > 0.5) {
        keepGoing = false;
    }
}
```

All the rules of scope (including "re-declaration" of `let`-created variables) are applied per scope instance. In other words, each time a scope is entered during execution, everything resets.

Each loop iteration is its own new scope instance, and within each scope instance, `value` is only being declared once.

```js
for (let i = 0; i < 3; i++) {
    let value = i * 10;
    console.log(`${ i }: ${ value }`);
}
```

The `i` and `value` variables are both declared exactly once **per scope instance**. No "re-declaration" here.

```js
for (const i = 0; i < 3; i++) {
    // oops, this is going to fail with
    // a Type Error after the first iteration
}
```

Let's mentally "expand" that loop like we did earlier:

```js
{
    // a fictional variable for illustration
    const $$i = 0;

    for ( ; $$i < 3; $$i++) {
        // here's our actual loop `i`!
        const i = $$i;
        // ..
    }
}
```

The problem is the conceptual `$$i` that must be incremented each time with the `$$i++` expression. That's re-assignment, which isn't allowed for constants.

## Uninitialized Variables (aka, TDZ)

With `var` declarations, the variable is "hoisted" to the top of its scope. But it's also automatically initialized to the `undefined` value, so that the variable can be used throughout the entire scope.

```js
studentName = "Suzy";   // let's try to initialize it!
// ReferenceError

console.log(studentName);

let studentName;
```

For `let` or `const`, the only way to initialize an uninitialized variable is with an assignment attached to a declaration statement.

For `let` and `const`, compiler adds an instruction in the middle of the program, at the point where the variable `studentName` was declared, to handle that declaration's auto-initialization.

The term coined by TC39 to refer to this period of time from the entering of a scope to where the auto-initialization of the variable occurs is: Temporal Dead Zone (TDZ). After that moment, the TDZ is done, and the variable is free to be used for the rest of the scope.

The TDZ is the time window where a variable exists but is still uninitialized, and therefore cannot be accessed in any way. A `var` also has technically has a TDZ, but it's zero in length and thus unobservable to our programs.

```js
askQuestion();
// ReferenceError

let studentName = "Suzy";

function askQuestion() {
  // Still in TDZ
  console.log(`${ studentName }, do you know?`);
}
```

```js
var studentName = "Kyle";

{
  // Still in TDZ since "studentName" is not initialized in this scope
  console.log(studentName);
  // ???

  // ..

  let studentName = "Suzy";

  console.log(studentName);
  // Suzy
}
```
