# Appendix A: Exploring Further

## Values vs. References

Primitive values are always assigned/passed as value copies. However, object values (arrays, objects, functions, etc.) are treated as references.

## So Many Function Forms

Anonymous function expression: no name identifier between the `function` keyword and the `(..)` parameter list.

Different function declaration forms:

```js
// generator function declaration
function *two() { .. }

// async function declaration
async function three() { .. }

// async generator function declaration
async function *four() { .. }

// named function export declaration (ES6 modules)
export function five() { .. }
```

Different function expression forms:

```js
// IIFE
(function(){ .. })();
(function namedIIFE(){ .. })();

// asynchronous IIFE
(async function(){ .. })();
(async function namedAIIFE(){ .. })();
```

Arrow function expressions are syntactically anonymous, meaning the syntax doesn't provide a way to provide a direct name identifier for the function.

## Coercive Conditional Comparison

`if` and `? :`-ternary statements, as well as the test clauses in `while` and `for` loops, all perform an implicit value comparison. But what sort? Is it "strict" or "coercive"? Both, actually.

## Prototypal "Classes"

All functions by default reference an empty object at a property named `prototype`. This is not the function's prototype, but rather the prototype object to link to when other objects are created by calling the function with `new`.



```js
function Classroom() {
    // ..
}

Classroom.prototype.welcome = function hello() {
    console.log("Welcome, students!");
};

var mathClass = new Classroom();

mathClass.welcome();
// Welcome, students!
```
