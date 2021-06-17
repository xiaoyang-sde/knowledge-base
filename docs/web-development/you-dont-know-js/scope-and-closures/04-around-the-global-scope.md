# Chapter 4: Around the Global Scope

## Why Global Scope?

How exactly do all those separate files get stitched together in a single runtime context by the JS engine?

1. If you're directly using ES modules without transpiling, these files are loaded individually by the JS environment. Each module then `import` references to whichever other modules it needs to access.
2. If you're using a bundler in your build process, all the files are typically concatenated together, which then only processes one big file.

In some build setups, the entire contents of the file are wrapped in a single enclosing scope, such as a wrapper function, universal module \(UMD—see Appendix A\), etc.

```javascript
(function wrappingOuterScope(){
    var moduleOne = (function one(){
    })();

    var moduleTwo = (function two(){
        function callModuleOne() {
            moduleOne.someMethod();
        }
    })();
})();
```

The scope of `wrappingOuterScope()` acts as a sort of "application-wide scope," a bucket where all the top-level identifiers can be stored.

1. If there is no single surrounding scope encompassing all these pieces, the global scope is the only way for them to cooperate with each other.

The global scope is also where:

* JS exposes its built-ins:
* * primitives: `undefined`, `null`, `Infinity`, `NaN`
* * natives: `Date()`, `Object()`, `String()`, etc.
* * global functions: `eval()`, `parseInt()`, etc.
* * namespaces: `Math`, `Atomics`, `JSON`
* * friends of JS: `Intl`, `WebAssembly`
* The environment hosting the JS engine exposes its own built-ins:
* * `console` \(and its methods\)
* * the DOM \(`window`, `document`, etc\)
* * timers \(`setTimeout(..)`, etc\)
* * web platform APIs: `navigator`, `history`, `geolocation`, `WebRTC`, etc

Node also exposes several elements "globally," but they're technically not in the `global` scope: `require()`, `__dirname`, `module`, `URL`, and so on.

## Where Exactly is this Global Scope?

Different JS environments handle the scopes of your programs, especially the global scope, differently.

### Browser "Window"

```javascript
var studentName = "Kyle";

function hello() {
    console.log(`Hello, ${ studentName }!`);
}

hello();

window.hello();
```

#### Globals Shadowing Globals

Within just the global scope itself, a global object property can be shadowed by a global variable.

```javascript
window.something = 42;

let something = "Kyle";

console.log(something);
// Kyle

console.log(window.something);
// 42
```

The `let` declaration adds a `something` global variable but not a global object property. The effect then is that the `something` lexical identifier shadows the `something` global object property.

#### DOM Globals

A DOM element with an `id` attribute automatically creates a global variable that references it.

```markup
<ul id="my-todo-list">
   <li id="first">Write a book</li>
   ..
</ul>
```

```javascript
first;

window["my-todo-list"]
```

If the `id` value is a valid lexical name, the lexical variable is created. If not, the only way to access that global is through the global object \(`window[..]`\).

#### What's in a \(Window\) Name?

```javascript
var name = 42;

console.log(name, typeof name);
// "42" string
```

`window.name` is a pre-defined "global" in a browser context; it's a property on the global object, so it seems like a normal global variable.

We used `var` for our declaration, which does not shadow the pre-defined `name` global property. The `var` declaration is ignored, since there's already a global scope object property of that name.

The weirdness is because `name` is actually a pre-defined getter/setter on the `window` object, which insists on its value being a string value.

### Web Workers

Web Workers are a web platform extension on top of browser-JS behavior, which allows a JS file to run in a completely separate thread \(operating system wise\) from the thread that's running the main JS program.

Since a Web Worker is treated as a wholly separate program, it does not share the global scope with the main JS program. Web Worker code does not have access to the DOM.

In a Web Worker, the global object reference is typically made using `self`. Just as with main JS programs, `var` and `function` declarations create mirrored properties on the global object, where other declarations \(`let`, `const`\) do not.

### Developer Tools Console/REPL

In some cases, favoring DX when typing in short JS snippets, over the normal strict steps expected for processing a full JS program, produces observable differences in code behavior between programs and tools.

For example, certain error conditions applicable to a JS program may be relaxed and not displayed when the code is entered into a developer tool.

Such tools typically emulate the global scope position to an extent; it's emulation, not strict adherence.

### ES Modules \(ESM\)

```javascript
var studentName = "Kyle";

function hello() {
    console.log(`Hello, ${ studentName }!`);
}

hello();
// Hello, Kyle!

export hello;
```

The observable effects from the overall application perspective will be different for ES modules. In the outermost obvious scope, `studentName` and `hello` are not global variables. Instead, they are module-wide, or if you prefer, "module-global."

However, there's no implicit "module-wide scope object" for these top-level declarations to be added to as properties. It's just that global variables don't get created by declaring variables in the top-level scope of a module.

The module's top-level scope is descended from the global scope, almost as if the entire contents of the module were wrapped in a function. Thus, all variables that exist in the global scope are available as lexical identifiers from inside the module's scope.

### Node

One aspect of Node that often catches JS developers off-guard is that Node treats every single .js file that it loads as a module. The practical effect is that the top level of your Node programs is never actually the global scope.

```javascript
var studentName = "Kyle";

function hello() {
    console.log(`Hello, ${ studentName }!`);
}

hello();
// Hello, Kyle!

// CommonJS Module
module.exports.hello = hello;
```

Node effectively wraps such code in a function, so that the var and function declarations are contained in that wrapping function's scope.

Node defines a number of "globals" like `require()`, but they're not actually identifiers in the global scope \(nor properties of the global object\). They're injected in the scope of every module.

The only way to define actual global variable is to add properties to another of Node's automatically provided "globals," which is called `global`.

## Global This

* Declare a global variable in the top-level scope with `var` or `function` declarations—or `let`, `const`, and class.
* Also add global variables declarations as properties of the global scope object if `var` or `function` are used for the declaration.
* Refer to the global scope object \(for adding or retrieving global variables, as properties\) with `window`, `self`, or `global`.

Another "trick" for obtaining a reference to the global scope object:

```javascript
const theGlobalScopeObject = (new Function("return this"))();
```

As of ES2020, JS has finally defined a standardized reference to the global scope object, called `globalThis`.

Polyfill for `globalThis`:

```javascript
const theGlobalScopeObject =
    (typeof globalThis != "undefined") ? globalThis :
    (typeof global != "undefined") ? global :
    (typeof window != "undefined") ? window :
    (typeof self != "undefined") ? self :
    (new Function("return this"))();
```

