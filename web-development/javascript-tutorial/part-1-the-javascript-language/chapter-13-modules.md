# Chapter 13: Modules

## 13.1 Modules, introduction

A module is just a file. One script is one module.

* `export` keyword labels variables and functions that should be accessible from outside the current module.
* `import` allows the import of functionality from other modules.

```javascript
export function sayHi(user) {
  alert(`Hello, ${user}!`);
}
```

```javascript
import {sayHi} from './sayHi.js';
```

### Core module features

* Always “use strict”
* Module-level scope: top-level variables and functions from a module are not seen in other scripts.
* If the same module is imported into multiple other places, its code is executed only the first time. \(Changes will share between importers.\)
* The object `import.meta` contains the information about the current module.
* In a module, `this` is `undefined`.

### Browser-specific features

* Module scripts are deferred: Module scripts wait until the HTML document is fully ready and then run. \(Relative order is maintained.\)
* `async` works on inline scripts.

#### External scripts

* External scripts with the same `src` run only once.
* External scripts that are fetched from another origin require `CORS` headers. \(`Access-Control-Allow-Origin`\)
* Modules without any path are not allowed. \(Example: `import {sayHi} from 'sayHi';`\)

### Compatibility, “nomodule”

Old browsers do not understand `type="module"`. Scripts of an unknown type are just ignored. It’s possible to provide a fallback using the `nomodule` attribute.

```javascript
<script type="module">
  alert("Runs in modern browsers");
</script>

<script nomodule>
  alert("Modern browsers know both type=module and nomodule, so skip this")
  alert("Old browsers ignore script with unknown type=module, but execute this.");
</script>
```

### Build tools

Browser modules are rarely used in their “raw” form. We bundle them together with a special tool such as Webpack and deploy to the production server.

## 13.2 Export and Import

### Export before declarations

We can label any declaration as exported by placing export before it, be it a variable, function or a class.

### Import \*

```javascript
import * as say from './say.js';
```

However, we should explicitly list what we need to import?

* Modern build tools \(webpack and others\) bundle modules together and optimize them to speedup loading and remove unused stuff.
* Shorter names.
* Better overview of the code structure.

### Import “as”

Use `as` to import under different names.

```javascript
import {sayHi as hi, sayBye as bye} from './say.js';
```

### Export “as”

```javascript
export {sayHi as hi, sayBye as bye};
```

`hi` and `bye` are official names for outsiders, to be used in imports.

### Export default

Modules provide a special `export default` syntax to make the :one thing per module" structure way look better.

```javascript
export default class User {}

import User from './user.js';
```

`import` needs curly braces for named exports and doesn’t need them for the default one.

The exported entity may have no name.

### The “default” name

```javascript
// user.js
export default class User {
  constructor(name) {
    this.name = name;
  }
}

export function sayHi(user) {
  alert(`Hello, ${user}!`);
}

// main.js
import {default as User, sayHi} from './user.js';

new User('John');
```

There’s a rule that imported variables should correspond to file names.

### Re-export

“Re-export” syntax `export ... from ...` allows to import things and immediately export them.

```javascript
// import login/logout and immediately export them
import {login, logout} from './helpers.js';
export {login, logout};
```

#### Re-exporting the default export

```javascript
export * from './user.js'; // to re-export named exports
export {default} from './user.js'; // to re-export the default export
```

## 13.3 Dynamic imports

### The import\(\) expression

The `import(module)` expression loads the module and returns a promise that resolves into a module object that contains all its exports.

```javascript
let module = await import(modulePath);

let {hi, bye} = await import('./say.js');

let obj = await import('./say.js');
let say = obj.default;
```

