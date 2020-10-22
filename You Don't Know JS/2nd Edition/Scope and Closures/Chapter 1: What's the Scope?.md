# Chapter 1: What's the Scope?

Our focus is the scope system and its function closures, as well as the power of the module design pattern.

The first step is to uncover how the JS engine processes our program before it runs.

JS functions are themselves first-class values. Since these functions hold and access variables, they maintain their original scope no matter where in the program, which is called closure.

## Compiled vs. Interpreted

Code compilation processes the text of your code and turn it into a list of instructions the computer can understand.

With interpretation the source code is transformed line by line; each line or statement is executed before immediately proceeding to processing the next line of the source code.

### Compiling Code

Scope is primarily determined during compilation, so understanding how compilation and execution relate is key in mastering scope.

Classic compiling strategy:
1. Tokenizing/Lexing: Breaking up a string of characters into meaningful chunks, called tokens.
2. Parsing: Taking a stream (array) of tokens and turning it into a tree of nested elements. (AST)
3. Code Generation: Taking an AST and turning it into executable code.

JS engines don't have the luxury of an abundance of time to perform their work and optimizations, because JS compilation doesn't happen in a build step ahead of time. It use all kinds of tricks (like JITs, which lazy compile and even hot re-compile).

## Required: Two Phases

1. Parsing/compilation
2. Execution

### Syntax Errors from the Start

```js
var greeting = "Hello";

console.log(greeting);

greeting = ."Hi";
// SyntaxError: unexpected token .
```

This program produces no output, but instead throws a `SyntaxError` about the unexpected `.` token right before the `"Hi"` string.

### Early Errors

```js
console.log("Howdy");

saySomething("Hello","Hi");
// Uncaught SyntaxError: Duplicate parameter name not
// allowed in this context

function saySomething(greeting,greeting) {
    "use strict";
    console.log(greeting);
}
```

Just like the snippet in the previous section, the `SyntaxError` here is thrown before the program is executed.

It's because strict-mode forbids functions to have duplicate parameter names.

### Hoisting

```js
function saySomething() {
    var greeting = "Hello";
    {
        greeting = "Howdy";  // error comes from here
        let greeting = "Hi";
        console.log(greeting);
    }
}

saySomething();
// ReferenceError: Cannot access 'greeting' before initialization
```

The `ReferenceError` here technically comes from `greeting = "Howdy"` accessing the `greeting` variable too early. (Temporal Dead Zone)

The greeting variable for that statement belongs to the declaration on the next line, `let greeting = "Hi"`, rather than to the previous `var greeting = "Hello"` statement.

**In spirit and in practice, what the engine is doing in processing JS programs is much more alike compilation than not.**

## Compiler Speak

```js
var students = [
    { id: 14, name: "Kyle" },
    { id: 73, name: "Suzy" },
    { id: 112, name: "Frank" },
    { id: 6, name: "Sarah" }
];

function getStudentName(studentID) {
    for (let student of students) {
        if (student.id == studentID) {
            return student.name;
        }
    }
}

var nextStudent = getStudentName(73);

console.log(nextStudent);
// Suzy
```

For all variables and identifiers, either they're the target of an assignment or they're the source of a value.

### Targets

Example of target assignment:

```js
students = [ // ..
```

The `var students` part is handled entirely as a declaration at compile time, and is thus irrelevant during execution.

Inobvious target assignment:
- `for (let student of students) {`
- `getStudentName(73)` (The argument `73` is assigned to the parameter `studentID`.)
- `function getStudentName(studentID) {` (Function hoisting: The association between `getStudentName` and the function is set up at the beginning of the scope rather than waiting for an `=` assignment statement.)

### Sources

For example, in `for (let student of students)`, we said that `student` is a target, but `students` is a source reference.

`id`, `name`, and `log` are all properties, not variable references.

### Cheating: Runtime Scope Modifications

Scope is determined as the program is compiled, and should not be affected by runtime conditions. However, in non-strict-mode, there are technically still two ways to cheat this rule.

The `eval(..)` function receives a string of code to compile and execute on the fly during the program runtime.

The second cheat is the `with` keyword, which essentially dynamically turns an object into a local scope.

```js
var badIdea = { oops: "Ugh!" };

with (badIdea) {
    console.log(oops);   // Ugh!
}
```

## Lexical Scope

The key idea of "lexical scope" is that it's controlled entirely by the placement of functions, blocks, and variable declarations, in relation to one another.

If you place a variable declaration inside a function, the compiler handles this declaration as it's parsing the function, and associates that declaration with the function's scope.

A reference for a variable must be resolved as coming from one of the scopes that are lexically available to it; otherwise the variable is said to be "undeclared". If the variable is not declared in the current scope, the next outer/enclosing scope will be consulted, until the global scope is reached.

Compilation creates a map of all the lexical scopes that lays out what the program will need while it executes. @hile scopes are identified during compilation, they're not actually created until runtime.