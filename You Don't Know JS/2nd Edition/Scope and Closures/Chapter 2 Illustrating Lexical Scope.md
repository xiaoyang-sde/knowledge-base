# Chapter 2: Illustrating Lexical Scope

## Marbles, and Buckets, and Bubbles...

- Marbles: Variables
- Buckest: Scopes

```js
// outer/global scope: RED

var students = [
    { id: 14, name: "Kyle" },
    { id: 73, name: "Suzy" },
    { id: 112, name: "Frank" },
    { id: 6, name: "Sarah" }
];

function getStudentName(studentID) {
    // function scope: BLUE

    for (let student of students) {
        // loop scope: GREEN

        if (student.id == studentID) {
            return student.name;
        }
    }
}

var nextStudent = getStudentName(73);
console.log(nextStudent);   // Suzy
```

- **Bubble 1** (RED) encompasses the global scope, which holds three identifiers/variables: `students` (line 1), `getStudentName` (line 8), and `nextStudent` (line 16).
- **Bubble 2** (BLUE) encompasses the scope of the function `getStudentName(..)` (line 8), which holds just one identifier/variable: the parameter `studentID` (line 8).
- **Bubble 3** (GREEN) encompasses the scope of the `for`-loop (line 9), which holds just one identifier/variable: `student` (line 9).

Each scope bubble is entirely contained within its parent scope bubble: a scope is never partially in two different outer scopes. 

Each variable/identifier is colored based on which bucket it's declared in, not the color of the scope it may be accessed from.

Any variable reference that appears in the scope where it was declared, or appears in any deeper nested scopes, will be labeled a marble of that same color—unless an intervening scope "shadows" the variable declaration.

References (non-declarations) to variables/identifiers are allowed if there's a matching declaration either in the current scope, or any scope above/outside the current scope, but not with declarations from lower/nested scopes.

The determination of colored buckets, and the marbles they contain, happens during compilation.

## A Conversation Among Friends

- Engine: responsible for start-to-finish compilation and execution of our JavaScript program.
- Compiler: one of Engine's friends; handles all the dirty work of parsing and code-generation.
- Scope Manager: another friend of Engine; collects and maintains a lookup list of all the declared variables/identifiers, and enforces a set of rules as to how these are accessible to currently executing code.

Let's take the first line as an example:

1. Encountering `var students`, Compiler will ask Scope Manager to see if a variable named `students` already exists for that particular scope bucket, and then ceate it or ignore it and move on.
2. Compiler then produces code for Engine to later execute, to handle the `students = []` assignment. The code Engine runs will ask Scope Manager if there is a variable called `students` accessible in the current scope bucket or parent scopes. Then it will assign the reference of the `[...]` array to it.

## Nested Scope

Each scope gets its own Scope Manager instance each time that scope is executed (one or more times). Each scope automatically has all its identifiers registered at the start of the scope being executed.

At the beginning of a scope, if any identifier came from a function declaration, that variable is automatically initialized to its associated function reference. Otherwise, any identifier which came from a `var` declaration (instead of `let` or `const`) is automatically initialized to `undefined`.

When an identifier reference cannot be found in the current scope, the next outer scope in the nesting is consulted until there are no more scopes to consult.

## Lookup Failures

### Undefined Mess

If the variable is a source, an unresolved identifier lookup will result in a `ReferenceError` being thrown. If the variable is a target, in strict-mode the code will also throw `ReferenceError`.

"Not defined" in the error message means a variable that has no matching formal declaration in any **lexically available** scope. "undefined" means a variable was declared but contains no value.

Here's a more confused part:

```js
var studentName;

typeof studentName;     // "undefined"
typeof doesntExist;     // "undefined"
```

### Global...

If the variable is a target and strict-mode is not in effect, the global scope's Scope Manager will just create an **accidental global variable** to fulfill that target assignment.

```js
function getStudentName() {
    // assignment to an undeclared variable :(
    nextStudent = "Suzy";
}

getStudentName();

console.log(nextStudent);
// "Suzy" -- oops, an accidental-global variable!
```

Always use strict-mode, and always formally declare the variables.

