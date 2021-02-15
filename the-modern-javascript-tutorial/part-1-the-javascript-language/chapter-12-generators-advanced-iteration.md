# Chapter 12: Generators, advanced iteration

## 12.1 Generators

Generators can return \(“yield”\) multiple values, one after another, on-demand.

```javascript
function* generateSequence() {
  yield 1;
  yield 2;
  return 3;
}
```

When the `next()` is called, it runs the execution until the nearest `yield <value>` statement.

The result \(object\):

* `value`: the yielded value.
* `done`: `true` if the function code has finished, otherwise `false`.

```javascript
let generator = generateSequence();

let one = generator.next();
```

### Generators are iterable

We can loop over the generator using `for..of`. However, it ignores the last value, when `done: true`.

To fix that, we must return them with `yield`.

```javascript
function* generateSequence() {
  yield 1;
  yield 2;
  yield 3;
}
```

### Using generators for iterables

```javascript
let range = {
  from: 1,
  to: 5,

  *[Symbol.iterator]() { // a shorthand for [Symbol.iterator]: function*()
    for(let value = this.from; value <= this.to; value++) {
      yield value;
    }
  }
};
```

Generators may generate values forever.

### Generator composition

There’s a special `yield*` syntax to “embed” \(compose\) one generator into another.

```javascript
function* generatePasswordCodes() {

  // 0..9
  yield* generateSequence(48, 57);
```

### “yield” is a two-way street

`yield` not only returns the result to the outside, but also can pass the value inside the generator.

```javascript
function* gen() {
  // Pass a question to the outer code and wait for an answer
  let result = yield "2 + 2 = ?"; // (*)

  alert(result);
}

let generator = gen();

let question = generator.next().value; // <-- yield returns the value

generator.next(4);
```

### generator.throw

```javascript
let question = generator.next().value;

generator.throw(new Error("The answer is not found in my database"));
```

## 12.2 Async iterators and generators

### Async iterators

1. Use `Symbol.asyncIterator`
2. `next()` should return a promise
3. Use `for await (let item of iterable)` loop to iterate over the object
4. The spread syntax `...` doesn’t work asynchronously.

### Async generators

```javascript
async function* generateSequence(start, end) {
  for (let i = start; i <= end; i++) {
    // yay, can use await!
    await new Promise(resolve => setTimeout(resolve, 1000));
    yield i;
  }
}

result = await generateSequence.next();
```

