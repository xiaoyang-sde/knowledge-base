# Chapter 3: The Scope Chain

The connections between scopes that are nested within other scopes is called the scope chain.

## "Lookup" Is (Mostly) Conceptual

What scope a variable originates from is usually determined during the initial compilation processing. This information would likely be stored with each variable's entry in the AST.

Engine doesn't need to lookup through a bunch of scopes to figure out which scope bucket a variable comes from.

Any reference to a variable that's initially undeclared is left as an uncolored marble during that file's compilation. This color cannot be determined until other relevant file(s) have been compiled and the application runtime commences. However, this lookup would only be needed once per variable at most.

## Shadowing

A single scope cannot have two or more variables with the same name.

With the conceptual notion of the "lookup," we asserted that it starts with the current scope and works its way outward, stopping as soon as a matching variable is found. This is a key aspect of lexical scope behavior, called **shadowing**.

## Global Unshadowing Trick

In the global scope, `var` declarations and `function` declarations also expose themselves as properties on the global object. (In browser, it's `window`.)

```js
var studentName = "Suzy";

function printStudent(studentName) {
    console.log(studentName);
    console.log(window.studentName);
}
```

The `window.studentName` is a mirror of the global `studentName` variable, not a separate snapshot copy.

## Copying Is Not Accessing

```js
var special = 42;

function lookingFor(special) {
    var another = {
        special: special
    };

    function keepLooking() {
        var special = 3.141592;
        console.log(special);
        console.log(another.special);  // Ooo, tricky!
        console.log(window.special);
    }

    keepLooking();
}
```

`special: special` is copying the value of the `special` parameter variable into another container. It doesn't mean we're accessing the parameter `special`.

For objects, mutating the contents of the object value via a reference copy is not the same thing as lexically accessing the variable itself.

## Illegal Shadowing

`let` (in an inner scope) can always shadow an outer scope's `var`. `var` (in an inner scope) can only shadow an outer scope's `let` if there is a function boundary in between.

```js
function another() {
    {
        let special = "JavaScript";

        {
            var special = "JavaScript";
            // ^^^ Syntax Error
        }
    }
}
```

The `var` is basically trying to "cross the boundary" of (or hop over) the `let` declaration of the same name, which is not allowed. That boundary-crossing prohibition effectively stops at each function boundary.

## Function Name Scope

```js
var askQuestion = function(){
    // ..
};
```

Since it's a function expression, the function itself will not "hoist".

```js
var askQuestion = function ofTheTeacher(){
    console.log(ofTheTeacher);
};

console.log(ofTheTeacher);
// ReferenceError: ofTheTeacher is not defined
```

`askQuestion` ends up in the outer scope. `ofTheTeacher` is declared as a read-only identifier inside the function itself.

`ofTheTeacher` is not exactly in the scope of the function.

A function expression without a name identifier is referred to as an "anonymous function expression." Anonymous function expressions clearly have no name identifier that affects either scope.

## Arrow Functions

```js
var askQuestion = () => {
    // ..
};
```

Other than being anonymous (and having no declarative form), `=>` arrow functions have the same lexical scope rules as `function` functions do. 

An arrow function, with or without `{ .. }` around its body, still creates a separate, inner nested bucket of scope.
