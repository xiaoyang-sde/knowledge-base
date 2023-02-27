# Chapter 6: Limiting Scope Exposure

## Least Exposure

Software engineering articulates a fundamental discipline, typically applied to software security, called "The Principle of Least Privilege" \(POLP\). Components of the system should be designed to function with least privilege, least access, least exposure.

When variables used by one part of the program are exposed to another part of the program, via scope, there are three main hazards that often arise:

* Naming Collisions
* Unexpected Behavior: If you expose variables/functions whose usage is otherwise private to a piece of the program, it allows other developers to use them in ways you didn't intend
* Unintended Dependency: If you expose variables/functions unnecessarily, it invites other developers to use and depend on those otherwise private pieces.

## Hiding in Plain \(Function\) Scope

It's important to hide our variable and function declarations in the most deeply nested scopes possible.

```javascript
var cache = {};

function factorial(x) {
    if (x < 2) return 1;
    if (!(x in cache)) {
        cache[x] = x * factorial(x - 1);
    }
    return cache[x];
}

factorial(6);
// 720

cache;
// {
//     "2": 2,
//     "3": 6,
//     "4": 24,
//     "5": 120,
//     "6": 720
// }

factorial(7);
// 5040
```

The `cache` variable is a private detail of how `factorial(..)` works. We could define another middle scope for `cache` to be located:

```javascript
function hideTheCache() {
    // "middle scope", where we hide `cache`
    var cache = {};

    return factorial;

    function factorial(x) {
        // inner scope
        if (x < 2) return 1;
        if (!(x in cache)) {
            cache[x] = x * factorial(x - 1);
        }
        return cache[x];
    }
}

var factorial = hideTheCache();

factorial(6);
```

In the Functional Programming world, caching a function's computed output is referred to as "memoization". This caching relies on closure.

A better solution is to use a function expression:

```javascript
var factorial = (function hideTheCache() {
    var cache = {};

    function factorial(x) {
        if (x < 2) return 1;
        if (!(x in cache)) {
            cache[x] = x * factorial(x - 1);
        }
        return cache[x];
    }

    return factorial;
})();
```

Since `hideTheCache(..)` is defined as a function expression instead of a function declaration, its name is in its own scope—essentially the same scope as `cache—rather` than in the outer/global scope. That means we can name every single occurrence of such a function expression the exact same name, and never have any collision.

## Invoking Function Expressions Immediately

Notice that we surrounded the entire function expression in a set of `( .. )`, and then on the end, we added that second `()` parentheses set; that's actually calling the function expression we just defined.

In other words, we're defining a function expression that's then immediately invoked. Immediately Invoked Function Expression \(IIFE\)

### Function Boundaries

Beware that using an IIFE to define a scope can have some unintended consequences, depending on the code around it.

1. Non-arrow function IFEs will change the binding of a `this` keyword. 
2. Statements like `break` and `continue` won't operate across an IIFE function boundary to control an outer loop or block.

## Scoping with Blocks

In general, any `{ .. }` curly-brace pair which is a statement will act as a block, but not necessarily as a scope.

Not all `{ .. }` curly-brace pairs create blocks:

* Object literals use `{ .. }` curly-brace pairs to delimit their key-value lists, but such object values are not scopes.
* `class` uses `{ .. }` curly-braces around its body definition, but this is not a block or scope.
* A function uses `{ .. }` around its body, but this is not technically a block—it's a single statement for the function body. It is, however, a function scope.
* The `{ .. }` curly-brace pair on a switch statement does not define a block/scope.

In most languages that support block scoping, an explicit block scope is an extremely common pattern for creating a narrow slice of scope for one or a few variables.

```javascript
if (somethingHappened) {
    // this is a block, but not a scope

    {
        // this is both a block and an
        // explicit scope
        let msg = somethingHappened.message();
        notifyOthers(msg);
    }

    // ..

    recoverFromSomething();
}
```

Each variable is defined at the innermost scope possible for the program to operate as desired.

To minimize the risk of TDZ errors with `let`/`const` declarations, always put those declarations at the top of their scope.

## var and let

Any variable that is needed across all \(or even most\) of a function should be declared by `var` so it can be used across the entire function.

Stylistically, var has always, from the earliest days of JS, signaled "variable that belongs to a whole function." `var` should be reserved for use in the top-level scope of a function.

## What's the Catch?

So far we've asserted that `var` and parameters are function-scoped, and `let`/`const` signal block-scoped declarations.

```javascript
try {
    doesntExist();
}
catch (err) {
    console.log(err);
    // ReferenceError: 'doesntExist' is not defined
    // ^^^^ message printed from the caught exception

    let onlyHere = true;
    var outerVariable = true;
}

console.log(outerVariable);     // true

console.log(err);
// ReferenceError: 'err' is not defined
```

* The `err` variable declared by the catch clause is block-scoped to that block. 
* The `catch` clause block can hold other block-scoped declarations via `let`.
* A `var` declaration inside this block still attaches to the outer function/global scope.

In ES2019, the `catch` clauses do not require the declaration. It's not a scope but a block.

```javascript
try {
    doOptionOne();
}
catch {   // catch-declaration omitted
    doOptionTwoInstead();
}
```

## Function Declarations in Blocks \(FiB\)

```javascript
if (false) {
    function ask() {
        console.log("Does this run?");
    }
}

ask();
```

The JS specification says that function declarations inside of blocks are block-scoped. \(`ReferenceError`\) However, most browser-based JS engines make that the identifier is scoped outside the if block but the function value is not automatically initialized, so it remains `undefined`. \(`TypeError`\)

These engines already had certain behaviors around FiB before ES6 introduced block scoping, and there was concern that changing to adhere to the specification might break some existing website JS code.

Place a function declaration in a block to conditionally define a function one way or another:

```javascript
if (typeof Array.isArray != "undefined") {
    function isArray(a) {
        return Array.isArray(a);
    }
}
else {
    function isArray(a) {
        return Object.prototype.toString.call(a)
            == "[object Array]";
    }
}
```

The only practical answer to avoiding the vagaries of FiB is to simply avoid FiB entirely. Never place a function declaration directly inside any block. However, it's perfectly fine and valid, for function expressions to appear inside blocks.

```javascript
if (true) {
    function ask() {
        console.log("Am I called?");
    }
}

if (true) {
    function ask() {
        console.log("Or what about me?");
    }
}

for (let i = 0; i < 5; i++) {
    function ask() {
        console.log("Or is it one of these?");
    }
}

ask();

function ask() {
    console.log("Wait, maybe, it's this one?");
}
```

FiB is not worth it, and should be avoided.

