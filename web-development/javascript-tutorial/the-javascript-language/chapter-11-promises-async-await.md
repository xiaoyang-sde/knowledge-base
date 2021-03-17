# Chapter 11: Introduction: callbacks

## 11.1 Introduction: callbacks

### Pyramid of Doom

```javascript
function loadScript(src, callback) {
  let script = document.createElement('script');
  script.src = src;

  script.onload = () => callback(null, script);
  script.onerror = () => callback(new Error(`Script load error for ${src}`));

  document.head.append(script);
}

loadScript('1.js', function(error, script) {
  if (error) {
    handleError(error);
  } else {
    loadScript('2.js', function(error, script) {
      if (error) {
        handleError(error);
      } else {
        loadScript('3.js', function(error, script) {
          if (error) {
            handleError(error);
...
```

The above example is sometimes called “callback hell” or “pyramid of doom.”

Although we can split the function into parts, it's difficult to read. Also the functions are all of single use, so there’s a bit of namespace cluttering here.

## 11.2

### Promise

```javascript
let promise = new Promise(function(resolve, reject) {
  // executor (the producing code, "singer")
});
```

When `new Promise` is created, the executor \(function\) runs automatically.

* `resolve(value)` — called with the result value if the job finished successfully.
* `reject(error)` — called if an error occurred, `error` is the error object.

The `promise` object has these internal properties:

* `state`: `pending` -&gt; `fulfilled` \(when `resolve` is called\) / `rejected` \(when `reject` is called\)
* `result`: `undefined` -&gt; the `value` or the `error`

For example:

```javascript
let promise = new Promise(function(resolve, reject) {
  setTimeout(() => resolve("done"), 1000);
  setTimeout(() => reject(new Error("Whoops!")), 1000);
});
```

1. The executor is called automatically and immediately.
2. The executor receives two arguments: `resolve` and `reject` from the JavaScript engine.
3. After one second of “processing” the executor calls `resolve("done")` to produce the result.

There can be only a single result or an error. Further calls are ignored.

### Consumers: then, catch, finally

#### then

```javascript
promise.then(
  function(result) { /* handle a successful result */ },
  function(error) { /* handle an error */ }
);
```

The second argument is optional.

#### catch

If we are only interested in errors:

* `.then(null, errorHandlingFunction)`
* `.catch(errorHandlingFunction)`

### finally

`finally` always runs when the promise is either resolve or reject. It's a good handler to perform cleanup.

* A `finally` handler has no arguments, thus we don’t know whether the `promise` is successful or not.
* A `finally` handler passes through results and errors to the next handler.

## 11.2 Promises chaining

When a handler returns a value, it becomes the result of that promise, so the next `.then` is called with it.

Technically we can also add many `.then` to a single promise. This is not chaining.

### Returning promises

A handler, used in `.then(handler)` may create and return a promise.

```javascript
new Promise(function(resolve, reject) {
    ...
}).then(function(result) {
  return new Promise((resolve, reject) => {
    setTimeout(() => resolve(result * 2), 1000);
  });

}).then(
    ...
```

To be precise, a handler may return an arbitrary object that has a method `.then`. The idea is that 3rd-party libraries may implement “promise-compatible” objects of their own.

As a good practice, an asynchronous action should always return a promise to keep it chainable.

## 11.4 Error handling with promises

When a promise rejects, the control jumps to the closest rejection handler. `catch` may appear after one or maybe several .then.

### Implicit try…catch

The code of a promise executor and promise handlers has an "invisible `try..catch`" around it. It automatically catches the error and turns it into rejected promise.

### Unhandled rejections

Unhandled promise rejections will cause the script dies with a message in the console.

In the browser we can catch such errors using the event `unhandledrejection`.

## 11.5 Promise API

### Promise.all

`Promise.all` takes an of promises and values and returns a new promise. The new promise returns the array of the results in the same order as the iterable.

If any of the promises is rejected, the promise returned by `Promise.all` immediately rejects with that error, and the results of other promises are ignored but not canceled.

### Promise.allSettled

`Promise.allSettled` just waits for all promises to settle, regardless of the result.

* `{status:"fulfilled", value:result}`
* `{status:"rejected", reason:error}`

### Promise.race

`Promise.race` waits only for the first settled promise and gets its result \(or error\).

```javascript
let promise = Promise.race(iterable);
```

### Promise.resolve/Promise.reject

`Promise.resolve(value)` creates a resolved promise with the result value. `Promise.reject(error)` creates a rejected promise with error.

## 11.6 Promisification

Promisification is the conversion of a function that accepts a callback into a function that returns a promise.

```javascript
function promisify(f) {
  return function (...args) { // return a wrapper-function
    return new Promise((resolve, reject) => {
      function callback(err, result) { // our custom callback for f
        if (err) {
          reject(err);
        } else {
          resolve(result);
        }
      }

      args.push(callback); // append our custom callback to the end of f arguments

      f.call(this, ...args); // call the original function
    });
  };
};
```

The limitation of promise is that it may have only one result, but a callback may technically be called many times.

## 11.7 Microtasks

```javascript
let promise = Promise.resolve();

promise.then(() => alert("promise done!"));

alert("code finished"); // this alert shows first
```

### Microtasks queue

* The queue is first-in-first-out: tasks enqueued first are run first.
* Execution of a task is initiated only when nothing else is running.

When a promise is ready, its `.then/catch/finally` handlers are put into the queue. When the JavaScript engine becomes free from the current code, it takes a task from the queue and executes it.

### Unhandled rejection

An “unhandled rejection” occurs when a promise error is not handled at the end of the microtask queue.

## 11.8 Async/await

### Async

The word “async” before a function means that the function always returns a promise.

This function returns a resolved promise with the result of 1:

```javascript
async function f() {
  return 1;
}
```

### Await

```javascript
let value = await promise;
```

The keyword `await` makes JavaScript wait until that promise settles and returns its result.

* `await` literally suspends the function execution until the promise settles, and then resumes it with the promise result.
* `await` won’t work in the top-level code.
* `await` accepts thenables to support third-party object.

### Error handling

In the case of a rejection, `await promise` throws the error, just as if there were a throw statement at that line.

We can catch that error using `try..catch`, the same way as a regular `throw`.

