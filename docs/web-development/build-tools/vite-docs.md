# Vite Documentation

## Why Vite

Before ES modules were available in browsers, traditional build tools (e.g. webpack) crawl, process, and concatenate source modules into files that can run in the browser.

However, large-scale projects with thousands of modules usually hit the performance bottleneck. Vite addresses these issues by leveraging the support of native ES modules in the browser.

- Slow server start: Vite divides the modules in an application into **dependencies** and **source code**. Vite transforms and serves source code over native ESM, which lets the browser request the modules on demand. Code behind conditional dynamic imports is only processed if used on the current screen.
  - Dependencies: plain JavaScript that does not change often during development (pre-bundled with esbuild)
  - Source code: non-plain JavaScript that needs transforming (e.g. JSX, CSS)

- Slow updates: Vite implements Hot Module Replacement (HMR) with native ESM. When a file is edited, Vite only needs to invalidate the chain between the edited module and its closest HMR boundary, making updates consistently fast.

In production, shipping unbundled ESM is still inefficient due to the additional network round trips caused by nested imports. Vite bundles the code with Rollup to get the optimal loading performance.

## Getting Started

```sh
npm init @vitejs/app

npm init @vitejs/app my-vue-app -- --template vue-ts
npm init @vitejs/app my-react-app -- --template react-ts
```

In a Vite project, `index.html` is the entry point to the application, thus it's iin the root of the project. Vite treats it as a part of the module graph and resolves `<script type="module" src="...">` that references the JavaScript source code.

## Features

### NPM Dependency Resolving and Pre-Bundling

Native ES imports do not support bare module imports. Vite will detect such bare module imports in all served source files and perform the following:

- Pre-bundle them with esbuild to improve page loading speed and convert CommonJS / UMD modules to ESM.
- Rewrite the imports to valid URLs like `/node_modules/.vite/my-dep.js?v=f3sf2ebd` so that the browser can import them properly.

### Hot Module Replacement

Vite provides an HMR API over native ESM and first-party HMR integrations for Vue Single File Components and React Fast Refresh.

### TypeScript

Vite supports importing `.ts` files out of the box and it performs transpilation with esbuild without type checking. However, it doesn't support certain features like const enum and implicit type-only imports.

### JSX

Vite supports `.jsx` and `.tsx` files with esbuild and defaults to React 16 flavor. Custom `jsxFactory` and `jsxFragment` can be configured using the esbuild option. JSX helpers can be injected with `jsxInject` to avoid manual imports.

```ts
export default {
  esbuild: {
    jsxFactory: 'h',
    jsxFragment: 'Fragment',
    jsxInject: `import React from 'react'`
  }
}
```

### CSS

Vite is pre-configured to support CSS `@import` inlining via `postcss-import`. All CSS `url()` references are always automatically rebased to ensure correctness. The CSS file ending with `.module.css` is considered a CSS modules file.

Because Vite targets modern browsers only, it is recommended to use native CSS variables with PostCSS plugins that implement CSSWG drafts (e.g. `postcss-nesting`) and author plain CSS.

### Static Assets

Importing a static asset will return the resolved public URL when it is served.

```ts
import imgUrl from './img.png'
document.getElementById('hero-img').src = imgUrl

// Explicitly load assets as URL
import assetAsURL from './asset.js?url'
// Load assets as strings
import assetAsString from './shader.glsl?raw'
// Load Web Workers
import Worker from './worker.js?worker'
// Web Workers inlined as base64 strings at build time
import InlineWorker from './worker.js?worker&inline'
```

### Build Optimizations

- CSS code-splitting: Vite automatically extracts the CSS used by modules in an async chunk and generates a separate file for it.
- Preload directives generation: Vite automatically generates `<link rel="modulepreload">` directives for entry chunks and their direct imports in the built HTML.
- Async chunk loading optimization: Rollup often generates common chunks that are shared between two or more chunks. Vite automatically rewrites code-split dynamic import calls with a preload step so that when a chunk is requested, the common chunk it imports is fetched in parallel to eliminate network roundtrips.

## Using Plugins

To use a plugin, it needs to be added to the `devDependencies` of the project and included in the `plugins` array in the `vite.config.js` config file. `plugins` also accept presets including several plugins as a single element.

```ts
// vite.config.js
import legacy from '@vitejs/plugin-legacy'

export default {
  plugins: [
    legacy({
      targets: ['defaults', 'not IE 11']
    })
  ]
}
```

## Dependency Pre-Bundling

Vite pre-builds the dependencies with esbuild.

- CommonJS and UMD compatibility: Because Vite serves source code as native ESM in the development environment, Vite converts dependencies that are shipped as CommonJS or UMD into ESM.
- Performance: Because some packages ship their ES modules builds as many separate files importing one another, Vite converts ESM dependencies with many internal modules into a single module to improve subsequent page load performance.

If an existing cache is not found, Vite will crawl the source code and discover dependency imports and use these found imports as entry points for the pre-bundle. After the server has already started, if a new dependency import is encountered that isn't already in the cache, Vite will re-run the dep bundling process and reload the page.

### Caching

- File system cache: Vite caches the pre-bundled dependencies in `node_modules/.vite`. It determines whether it needs to re-run the pre-bundling step based on a few sources.
  - The `dependencies` in `package.json`
  - The package manager lock files (e.g. `package-lock.json`)
  - Relevant fields in the `vite.config.js`

- Browser cache: Resolved dependency requests are strongly cached with HTTP headers `max-age=31536000,immutable` to improve page reload performance during development. They are invalidated by the appended version query if a different version is installed.

## Building for Production

Vite uses `<root>/index.html` as the build entry point, and produces an application bundle that is suitable to be served over a static hosting service.

The production bundle assumes support for modern JavaScript. By default, Vite targets browsers that support the native ESM script tag and native ESM dynamic import.

### Static Asset Handling

Specific assets could be placed into the special `public` directory under the project root. Assets in this directory will be served at root path `/` during development and copied to the root of the dist directory as-is. Assets in `public` cannot be imported from JavaScript.

## Env Variables and Modes

### Env Variables

- `import.meta.env.MODE`: The mode the app is running in. By default, the dev server runs in `development` mode, and the build command runs in `production` mode. The default mode could be overwritten with the `--mode` option flag. (string)
- `import.meta.env.BASE_URL`: The base URL the app is being served from. (string)
- `import.meta.env.PROD`: whether the app is running in production. (boolean)
- `import.meta.env.DEV`: whether the app is running in development (boolean)

### .env Files

Vite uses dotenv to load additional environment variables from the following files in the environment directory. Loaded env variables are also exposed to the client source code via `import.meta.env`. In the production environment, variables prefixed with `VITE_` are exposed to Vite-processed code.

```sh
.env                # loaded in all cases
.env.local          # loaded in all cases, ignored by git
.env.[mode]         # only loaded in specified mode
.env.[mode].local   # only loaded in specified mode, ignored by git
```
