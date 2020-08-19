# Chapter 13: Modules

## 13.1 Modules, introduction

A module is just a file. One script is one module.

- `export` keyword labels variables and functions that should be accessible from outside the current module.
- `import` allows the import of functionality from other modules.

```js
export function sayHi(user) {
  alert(`Hello, ${user}!`);
}
```

```js
import {sayHi} from './sayHi.js';
```

### Core module features

- Always “use strict”
- Module-level scope: top-level variables and functions from a module are not seen in other scripts.
- If the same module is imported into multiple other places, its code is executed only the first time. (Changes will share between importers.)
- The object `import.meta` contains the information about the current module.
- In a module, `this` is `undefined`.

### Browser-specific features

- Module scripts are deferred: Module scripts wait until the HTML document is fully ready and then run. (Relative order is maintained.)
- `async` works on inline scripts.

#### External scripts
- External scripts with the same `src` run only once.
- External scripts that are fetched from another origin require `CORS` headers. (`Access-Control-Allow-Origin`)
- Modules without any path are not allowed. (Example: `import {sayHi} from 'sayHi';`)

### Compatibility, “nomodule”

Old browsers do not understand `type="module"`. Scripts of an unknown type are just ignored. It’s possible to provide a fallback using the `nomodule` attribute.

```js
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
