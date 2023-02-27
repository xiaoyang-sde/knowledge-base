# Chapter 1: What Is JavaScript?

* JS is an implementation of the ECMAScript standard \(version ES2019 as of this writing\), which is guided by the TC39 committee and hosted by ECMA. It runs in browsers and other JS environments such as Node.js.
* JS is a multi-paradigm language, meaning the syntax and capabilities allow a developer to mix and match \(and bend and reshape!\) concepts from various major paradigms, such as procedural, object-oriented \(OO/classes\), and functional \(FP\).
* JS is a compiled language, meaning the tools \(including the JS engine\) process and verify a program \(reporting any errors!\) before it executes.

## What's With That Name?

The name JavaScript is an artifact of marketing shenanigans. It was originally designed to appeal to an audience of mostly Java programmers.

The official name of the language specified by TC39 and formalized by the ECMA standards body is **ECMAScript**. IT has also been suffixed by the revision year, such as ES2019.

## Language Specification

TC39, the technical steering committee that manages JS, meets regularly to vote on any areed changes and then submit them to ECMA, the standards organization.

JS's syntax and behavior are defined in the ES specification. The latest ES2019 standard: [https://www.ecma-international.org/ecma-262/10.0/](https://www.ecma-international.org/ecma-262/10.0/)

All TC39 proposals progress from Stage 0 to Stage 4.

## The Web Rules Everything About \(JS\)

How JS is implemented for web browsers is the only reality that matters. Sometimes the JS engines will refuse to conform to a specification-dictated change, since it would break that web content. Thus, TC39 will backtrack and simply choose to conform the specification to the reality of the web: [\#SmooshGate](https://developers.google.com/web/updates/2018/03/smooshgate)

* The Appendix B, "Additional ECMAScript Features for Web Browsers", is used to detail out any known mismatches between the official JS specification and the reality of JS on the web.
* Section B.1 and B.2 cover additions to JS \(syntax and APIs\) that web JS includes, again for historical reasons, but which TC39 does not plan to formally specify in the core of JS.
* Section B.3 includes some conflicts where code may run in both web and non-web JS engines.

Wherever possible, adhere to the JS specification and don't rely on behavior that's only applicable in certain JS engine environments.

## Not All \(Web\) JS...

The `alert(..)` function and `console.*` are not included in the JS specification, but it is in all web JS environments.

A wide range of JS-looking APIs, like `fetch(..)`, `getCurrentLocation(..)`, and `getUserMedia(..)`, are all web APIs that look like JS. They are functions and object methods and they obey JS syntax rules.

## It's Not Always JS

The developer console is not trying to pretend to be a JS compiler that handles your entered code exactly the same way the JS engine handles a `.js` file. It's trying to make it easy for you to quickly enter a few lines of code and see the results immediately.

Don't trust what behavior you see in a developer console as representing exact to-the-letter JS semantics.

## Many Faces

Typical paradigm-level code categories include procedural, object-oriented \(OO/classes\), and functional \(FP\).

JavaScript is most definitely a multi-paradigm language.

## Backwards & Forwards

One of the most foundational principles that guides JavaScript is preservation of backwards compatibility.

Backwards compatibility means that once something is accepted as valid JS, there will not be a future change to the language that causes that code to become invalid JS.

JS is not forwards-compatible. Being forwards-compatible means that including a new addition to the language in a program would not cause that program to break if it were run in an older JS engine.

### Jumping the Gaps

If you run a program that uses an ES2019 feature in an engine from 2016, you're very likely to see the program break and crash.

For new and incompatible syntax, the solution is transpiling. Forwards-compatibility problems related to syntax are solved by using a transpiler, such as Babel.

Developers should focus on writing the clean, new syntax forms, and let the tools take care of producing a forwards-compatible version of that code.

### Filling the Gaps

If the forwards-compatibility issue is not related to new syntax, but rather to a missing API method that was only recently added, the most common solution is to provide a definition for that missing API method that stands in and acts as if the older environment had already had it natively defined. This process is called polyfill.

Transpilers like Babel typically detect which polyfills your code needs and provide them automatically for you. Avoid negatively impacting the code's readability by trying to manually adjust for the syntax/API gaps.

## What's in an Interpretation?

JS is a compiled language instead of a interpreted \(scripting\) language. Languages regarded as "compiled" usually produce a binary representation of the program that is distributed for execution later.

* In scripted or interpreted languages, an error on line 5 of a program won't be discovered until lines 1 through 4 have already executed.
* In the languages that parse the code before any execution occurs, an invalid command \(such as broken syntax\) on line 5 would be caught before exccuting.

JS source code is parsed before it is executed. It is converted to an optimized binary form, and that "code" is subsequently executed by the "JS virtual machine". JS engines can employ multiple passes of JIT \(Just-In-Time\) processing/optimization on the generated code.

1. After a program leaves a developer's editor, it gets transpiled by Babel, then packed by Webpack \(and perhaps half a dozen other build processes\), then it gets delivered in that very different form to a JS engine.
2. The JS engine parses the code to an AST.
3. Then the engine converts that AST to a kind-of byte code, a binary intermediate representation \(IR\), which is then refined/converted even further by the optimizing JIT compiler.
4. Finally, the JS VM executes the program.

### Web Assembly \(WASM\)

The riginal intent of WASM was to provide a path for non-JS programs to be converted to a form that could run in the JS engine. It gets around some of the inherent delays in JS parsing and compilation before a program can execute.

WASM is a representation format that can be processed by a JS engine by skipping the parsing and compilation that the JS engine normally does.

WASM significantly augments what the web \(including JS\) can accomplish.

## Strictly Speaking

JS added strict mode as an opt-in mechanism for encouraging better JS programs. Most strict mode controls are in the form of early errors, which could be thrown at compile time.

```javascript
// only whitespace and comments are allowed
// before the use-strict pragma
"use strict";
// the rest of the file runs in strict mode
```

Strict mode can alternatively be turned on per-function scope.

```javascript
function someOperations() {
    // whitespace and comments are fine here
    "use strict";

    // all this code will run in strict mode
}
```

Interestingly, if a file has strict mode turned on, the function-level strict mode pragmas are disallowed.

Virtually all transpiled code ends up in strict mode even if the original source code isn't written as such. ES6 modules also assume strict mode.

