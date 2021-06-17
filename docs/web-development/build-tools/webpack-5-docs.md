# Webpack 5 Documentation

Article Source: [https://webpack.js.org/concepts/](https://webpack.js.org/concepts/)

Webpack is a static module bundler for modern JavaScript applications.

## Concepts

### Entry

An entry point indicates which module webpack should use to begin building out its internal dependency graph. webpack will figure out which other modules and libraries that entry point depends on.

#### Single Entry (Shorthand) Syntax

Single Entry Syntax is useful for quickly setup a configuration for an application or tool with one entry point, such as a library.

```javascript
module.exports = {
  entry: './src/file_1.js',
};
```

To inject multiple dependent files together, we could pass an array of file paths.

```javascript
module.exports = {
  entry: ['./src/file_1.js', './src/file_2.js'],
  output: {
    filename: 'bundle.js',
  },
};
```

#### Object Syntax

Object syntax is the most scalable way of defining entry/entries in your application.

```javascript
module.exports = {
  entry: {
    app: './src/app.js',
    adminApp: './src/adminApp.js',
  },
};
```

**EntryDescription object**

```javascript
module.exports = {
  entry: {
    a2: 'dependingfile.js',
    b2: {
      dependOn: 'a2',
      import: './src/app.js',
    },
  },
};
```

* `dependOn`: The entry points that the current entry point depends on. \(Must not be circular\)
* `filename`: The name of each output file on disk.
* `import`: Modules that are loaded upon startup.
* `library`: Options for library.

**Separate App and Vendor Entries**

With this you can import required libraries that aren't modified \(e.g. Bootstrap, jQuery, images, etc\) inside `vendor.js` and the content hash remains the same, which allow the browser to cache them separately.

```javascript
// webpack.config.js
module.exports = {
  entry: {
    main: './src/app.js',
    vendor: './src/vendor.js',
  },
  output: {
    filename: '[name].[contenthash].bundle.js',
  },
};
```

**Multi Page Application**

Use exactly one entry point for each HTML document.

```javascript
module.exports = {
  entry: {
    pageOne: './src/pageOne/index.js',
    pageTwo: './src/pageTwo/index.js',
    pageThree: './src/pageThree/index.js',
  },
};
```

### Output

Configuring the `output` configuration options tells webpack how to write the compiled files to disk.

```javascript
module.exports = {
  output: {
    filename: 'bundle.js',
  },
};
```

This configuration would output a single `bundle.js` file into the `dist` directory.

#### Multiple Entry Points

```javascript
module.exports = {
  entry: {
    app: './src/app.js',
    search: './src/search.js',
  },
  output: {
    filename: '[name].js',
    path: __dirname + '/dist',
  },
};
```

### Loaders

Loaders are transformations that are applied to the source code of a module. They allow you to pre-process files as you import or "load" them. Loaders can transform files from a different language \(like TypeScript\) to JavaScript or load inline images as data URLs.

#### Example

To load CSS files and TypeScript files:

```text
npm install --save-dev css-loader ts-loader
```

Instruct webpack to use the `css-loader` for every `.css` file and the `ts-loader` for all `.ts` files:

```javascript
module.exports = {
  module: {
    rules: [
      { test: /\.css$/, use: 'css-loader' },
      { test: /\.ts$/, use: 'ts-loader' },
    ],
  },
};
```

#### Configuration

`module.rules` allows you to specify several loaders within your webpack configuration. Loaders are evaluated/executed from bottom to top.

#### Loader Features

* Loaders can be chained. Each loader in the chain applies transformations to the processed resource.
* Loaders can be configured with an `options` object.
* Loaders run in Node.js and can do everything that’s possible there.

### Plugins

#### Anatomy

A webpack plugin is a JavaScript object that has an `apply` method. This `apply` method is called by the webpack compiler, giving access to the entire compilation lifecycle.

```javascript
const pluginName = 'ConsoleLogOnBuildWebpackPlugin';

class ConsoleLogOnBuildWebpackPlugin {
  apply(compiler) {
    compiler.hooks.run.tap(pluginName, (compilation) => {
      console.log('The webpack build process is starting!!!');
    });
  }
}

module.exports = ConsoleLogOnBuildWebpackPlugin;
```

#### Usage

Since plugins can take arguments/options, you must pass a `new` instance to the `plugins` property in your webpack configuration.

#### Configuration

```javascript
const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
  ...
  plugins: [
    new webpack.ProgressPlugin(),
    new HtmlWebpackPlugin({ template: './src/index.html' }),
  ],
}
```

### Modules

#### What is a webpack Module

* An ES2015 `import` statement
* A CommonJS `require()` statement
* An AMD `define` and `require` statement
* An `@import` statement inside of a css/sass/less file.
* An image url in a stylesheet `url(...)` or HTML `<img src=...>` file.

#### Supported Module Types

* ECMAScript modules
* CommonJS modules
* AMD modules
* Assets
* WebAssembly modules

### Module Resolution

A resolver is a library which helps in locating a module by its absolute path.

```javascript
import foo from 'path/to/module';

require('path/to/module');
```

#### Resolving rules

* Absolute paths

```javascript
import '/home/me/file';
```

* Relative paths

```javascript
import '../src/file1';
```

* Module paths

```javascript
import 'module';
```

### Dependency Graph

Any time one file depends on another, webpack treats this as a dependency. This allows webpack to take non-code assets, such as images or web fonts.

When webpack processes your application, it starts from a list of modules defined on the command line or in its configuration file. Starting from these entry points, webpack recursively builds a dependency graph that includes every module your application needs, then bundles all of those modules into a small number of bundles - often, just one - to be loaded by the browser.

### Targets

```javascript
module.exports = {
  target: 'node',
};
```

Available targets: [https://webpack.js.org/configuration/target/](https://webpack.js.org/configuration/target/)

### The Manifest

In a typical application or site built with webpack, there are three main types of code:

* The source code
* Any third-party library or "vendor" code

  A webpack runtime and manifest that conducts the interaction of all modules

#### Runtime

The runtime, along with the manifest data, is basically all the code webpack needs to connect your modularized application while it's running in the browser.

#### Manifest

As the compiler enters, resolves, and maps out your application, it keeps detailed notes on all your modules. This collection of data is called the "Manifest."

The "Manifest" is what the runtime will use to resolve and load modules once they've been bundled and shipped to the browser.

No matter which module syntax you have chosen, those import or require statements have now become `__webpack_require__` methods that point to module identifiers.

#### The Problem

By using content hashes within your bundle file names, you can indicate to the browser when the content of a file has changed, thus invalidating the cache.

However, certain hashes change even when their content apparently does not. This is caused by the injection of the runtime and manifest, which changes every build.

### Hot Module Replacement

Hot Module Replacement (HMR) exchanges, adds, or removes modules while an application is running, without a full reload.

* Retain application state which is lost during a full reload.
* Save valuable development time by only updating what's changed.
* Instantly update the browser when modifications are made to CSS/JS in the source code.

#### In the Application

* The application asks the HMR runtime to check for updates.
* The runtime asynchronously downloads the updates and notifies the application.
* The application then asks the runtime to apply the updates.
* The runtime synchronously applies the updates.

#### In the Compiler

In addition to normal assets, the compiler needs to emit an "update" to allow updating from the previous version to the new version.

* The updated manifest \(JSON\)
* One or more updated chunks \(JavaScript\)

The manifest contains the new compilation hash and a list of all updated chunks. Each of these chunks contains the new code for all updated modules.

The compiler ensures that module IDs and chunk IDs are consistent between these builds.

#### In a Module

HMR is an opt-in feature that only affects modules containing HMR code. When implementing the HMR interface in a module, you can describe what should happen when the module is updated.

#### In the Runtime

For the module system runtime, additional code is emitted to track module `parents` and `children`. On the management side, the runtime supports two methods: `check` and `apply`.

A `check` makes an HTTP request to the update manifest. If this request fails, there is no update available. If it succeeds, the list of updated chunks is compared to the list of currently loaded chunks. For each loaded chunk, the corresponding update chunk is downloaded. All module updates are stored in the runtime. When all update chunks have been downloaded and are ready to be applied, the runtime switches into the `ready` state.

The `apply` method flags all updated modules as invalid. For each invalid module, there needs to be an update handler in the module or in its parents. Otherwise, the invalid flag bubbles up and invalidates parents as well. Each bubble continues until the app's entry point or a module with an update handler is reached. If it bubbles up from an entry point, the process fails.

Afterwards, all invalid modules are disposed and unloaded. The current hash is then updated and all accept handlers are called. The runtime switches back to the `idle` state and everything continues as normal.

### Under The Hood

The bundling is a function that takes some files and emits others. Between input and output, it also has modules, entry points, chunks, chunk groups, and many other intermediate parts.

#### The main parts

Every file used in your project is a Module.

By using each other, the modules form a graph \(ModuleGraph\).

During the bundling process, modules are combined into chunks. Chunks combine into chunk groups and form a graph \(ChunkGraph\) interconnected through modules. When you describe an entry point - under the hood, you create a chunk group with one chunk.

```javascript
module.exports = {
  entry: {
    home: './home.js',
    about: './about.js',
  },
};
```

Two chunk groups with names home and about are created. Each of them has a chunk with a module - `./home.js` for `home` and `./about.js` for `about`.

#### Chunks

`initial` is the main chunk for the entry point. This chunk contains all the modules and its dependencies that you specify for an entry point.

`non-initial` is a chunk that may be lazy-loaded. It may appear when dynamic import or `SplitChunksPlugin` is being used.

Each chunk has a corresponding asset. The assets are the output files - the result of bundling.

**Example**

```javascript
module.exports = {
  entry: './src/index.jsx',
};
```

```javascript
import React from 'react';
import ReactDOM from 'react-dom';

import('./app.jsx').then((App) => {
  ReactDOM.render(<App />, root);
});
```

Initial chunk with name `main` is created. It contains:

* `./src/index.jsx`
* `react`
* `react-dom`

and all their dependencies, except `./app.jsx`

Non-initial chunk for `./app.jsx` is created as this module is imported dynamically.

Output:

```text
/dist/main.js - an initial chunk
/dist/394.js - non-initial chunk
```

By default, there is no name for non-initial chunks so that a unique ID is used instead of a name.

#### Output

The names of the output files are affected by the two fields in the config:

* `output.filename` - for initial chunk files
* `output.chunkFilename` - for non-initial chunk files

A few placeholders are available in these fields. Most often:

* `[id]` - chunk id \(`[id].js` -&gt; `485.js`\)
* `[name]` - chunk name \(`[name].js` -&gt; `app.js`\). If a chunk has no name, then its id will be used.
* `[contenthash]` - md4-hash of the output file content \( `[contenthash].js` -&gt; `4ea6ff1de66c537eb9b2.js`\)

