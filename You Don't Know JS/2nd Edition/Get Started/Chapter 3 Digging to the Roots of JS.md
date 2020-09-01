# Chapter 3: Digging to the Roots of JS

## Iteration

The iteration protocol defines a `next()` method whose return is an object called an iterator result; the object has `value` and `done` properties, where `done` is a boolean that is `false` until the iteration over the underlying data source is complete.

### Consuming Iterators

```js
// loop over its results one at a time
for (let val of it) {
    console.log(`Iterator value: ${ val }`);
}
```

Another mechanism that's often used for consuming iterators is the `...` operator. This operator actually has two symmetrical forms: *spread* and *rest*. The spread form is an iterator-consumer.

```js
var vals = [ ...it ];
doSomethingUseful( ...it );
```

### Iterables

ES6 defined the basic data structure/collection types in JS as iterables. This includes strings, arrays, maps, sets, and others.

For the most part, all built-in iterables in JS have three iterator forms available: keys-only (`keys()`), values-only (`values()`), and entries (`entries()`).

## Closure

Closure is when a function remembers and continues to access variables from outside its scope, even when the function is executed in a different scope. Closure can observe updates to these variables over time.

```js
for (let [idx,btn] of buttons.entries()) {
    btn.addEventListener("click",function onClick(){
       console.log(`Clicked on button (${ idx })!`);
    });
}
```

## this Keyword

One common misconception is that a function's `this` refers to the function itself.

When a function is defined, it is attached to its enclosing scope via closure. It also has an execution context, and that is exposed to the function via its `this` keyword. The execution context is dynamic, entirely dependent on how it is called.

Without strict mode, context-aware functions that are called without any context specified default the context to the global object. (`window` in browser)

```js
function classroom(teacher) {
    return function study() {
        console.log(
            `${ teacher } says to study ${ this.topic }`
        );
    };
}
var assignment = classroom("Kyle");

assignment(); // Kyle says to study undefined
```

```js
var homework = {
    topic: "JS",
    assignment: assignment
};

homework.assignment(); // Kyle says to study JS
```

The `call(..)` method takes an object to use for setting the `this` reference for the function call.

```js
var otherHomework = {
    topic: "Math"
};

assignment.call(otherHomework); // Kyle says to study Math
```

## Prototypes

A prototype is a characteristic of an object, and specifically resolution of a property access. Delegation of property/method access allows two objects to cooperate with each other to perform a task.

### Object Linkage

To define an object prototype linkage, create the object using the `Object.create(..)`.

```js
var homework = {
    topic: "JS"
};

var otherHomework = Object.create(homework);

otherHomework.topic;
```

### this Revisited

```js
var homework = {
    study() {
        console.log(`Please study ${ this.topic }`);
    }
};

var jsHomework = Object.create(homework);
jsHomework.topic = "JS";
jsHomework.study();
// Please study JS

var mathHomework = Object.create(homework);
mathHomework.topic = "Math";
mathHomework.study();
// Please study Math
```

