# Chapter 3: Code quality

## 3.1 Debugging in Chrome

Debugging is the process of finding and fixing errors within a script. We can pause the code by using the debugger command in it:

```javascript
function hello(name) {
  let phrase = `Hello, ${name}!`;

  debugger;  // <-- the debugger stops here

  say(phrase);
}
```

- Step: run the next command
- Step over: run the next command, but don't go into a function (not interested to see what happens inside the function call)
- Step into: similar to "Step", but behaves differently in case of asynchronous function calls ("Step" command ignores async actions)
- Step out: continue the execution till the end of the current function (That’s handy when we accidentally entered a nested call)

To output something to console from our code:

```javascript
for (let i = 0; i < 5; i++) {
  console.log("value,", i);
}
```

### 3.2 Coding Style

- The opening brace on the same line as the corresponding keyword.
- The maximum line length is usually 80 or 120 characters.
- Horizontal indents: 2 or 4 spaces.
- Vertical indents: empty lines for splitting code into logical blocks. (There should not be more than nine lines of code without a vertical indentation.)
- A semicolon should be present after each statement.
- Try to avoid nesting code too many levels deep.
- Function placement: Code first, then functions.

### Style Guides

- Google JavaScript Style Guide
- Airbnb JavaScript Style Guide
- Idiomatic.JS
- StandardJS

### Automated Linters

- JSLint – one of the first linters.
- JSHint – more settings than JSLint.
- ESLint – probably the newest one.

## 3.3 Comments

Novices tend to use comments to explain "what is going on in the code".

```javascript
// This code will do this thing (...) and that thing (...)
// ...and who knows what else...
very;
complex;
code;
```

Seriously, the code should be easy to understand without them:

- Factor out functions: Sometimes it’s beneficial to replace a code piece with a function.
- Create functions: Functions themselves tell what’s going on. There's nothing to comment.

### Good comments

- Describe the architecture: Provide a high-level overview of components, how they interact, what’s the control flow in various situations.
- Document function parameters and usage: Use JSDoc to document a function usage, parameters, and returned value.
- Why is the task solved this way? (If there are many ways to solve the task, why this one?)
- Any subtle features of the code? Where they are used?
3.4 Ninja code

- Make the code as short as possible.
- Use single-letter variable names everywhere. Like a, b or c.
- If the team rules forbid the use of one-letter and vague names – shorten them, make abbreviations.
- Use similar variable names, like date and data.
- Use same names for variables inside and outside a function.

## 3.5 Automated testing with Mocha

When testing a code by manual re-runs, it’s easy to miss something.

### Behavior Driven Development (BDD)

BDD is three things in one: tests AND documentation AND examples.

```javascript
describe("pow", function() {

  it("raises to n-th power", function() {
    assert.equal(pow(2, 3), 8);
  });

});
```

- describe("title", function() { ... })
- it("use case description", function() { ... })
- assert.equal(value1, value2)

### The development flow

1. An initial spec is written, with tests for the most basic functionality.
2. An initial implementation is created.
3. To check whether it works, we run the testing framework Mocha (more details soon) that runs the spec. While the functionality is not complete, errors are displayed. We make corrections until everything works.
4. Now we have a working initial implementation with tests.
5. We add more use cases to the spec, probably not yet supported by the implementations. Tests start to fail.
6. Go to 3, update the implementation till tests give no errors.
7. Repeat steps 3-6 till the functionality is ready.

- Mocha – the core framework: it provides common testing functions including describe and it and the main function that runs tests.
- Chai – the library with many assertions. It allows to use a lot of different assertions, for now we need only assert.equal.
- Sinon – a library to spy over functions, emulate built-in functions and more, we’ll need it much later.

```javascript
describe("Raises x to power n", function() {
  it("5 in the power of 1 equals 5", function() {
    assert.equal(pow(5, 1), 5);
  });

  it("5 in the power of 2 equals 25", function() {
    assert.equal(pow(5, 2), 25);
  });

  it("5 in the power of 3 equals 125", function() {
    assert.equal(pow(5, 3), 125);
  });
});
```

## 3.6 Polyfills

It's quite common for an engine to implement only the part of the standard as some of them are hard to implement.

### Babel

Babel is a transpiler. It rewrites modern JavaScript code into the previous standard.

1. First, the transpiler program, which rewrites the code. Modern project build systems like webpack provide means to run transpiler automatically on every code change.
2. New language features may include new built-in functions and syntax constructs, which are not supported by old engines.  A script that updates/adds new functions is called "polyfill". It "fills in" the gap and adds missing implementations.
