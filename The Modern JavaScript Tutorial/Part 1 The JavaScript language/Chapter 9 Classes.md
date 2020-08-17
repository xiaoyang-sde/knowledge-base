# Chapter 9: Classes

## 9.1 Class basic syntax

### The “class” syntax

```js
class MyClass {
  prop = value; // property

  constructor(...) { // constructor
    // ...
  }

  method(...) {} // method

  get something(...) {} // getter method
  set something(...) {} // setter method

  [Symbol.iterator]() {} // method with computed name (symbol here)
  // ...
}
```

The `constructor()` method is called automatically by `new`, so we can initialize the object there. There's no comma between class methods.

```js
class User {

  constructor(name) {
    this.name = name;
  }

  sayHi() {
    alert(this.name);
  }

}
```

### What is a class?

In JavaScript, a class is a kind of function. What `class User {...}` construct really does is:

1. Creates a function named `User`, that becomes the result of the class declaration. The function code is taken from the `constructor` method (assumed empty if we don’t write such method).
2. Stores class methods, such as `sayHi`, in `User.prototype`.

### Not just a syntactic sugar

1. A function created by class is labelled by a special internal property `[[FunctionKind]]:"classConstructor"`.
2. Unlike a regular function, a class must be called with `new`.
3. Class methods are non-enumerable. A class definition sets `enumerable` flag to `false` for all methods in the `prototype`.
4. All code inside the class construct is automatically in strict mode.

### Class Expression

```js
let User = class Name {
  sayHi() {
    alert("Hello");
  }
};
```

Dynamically make a class on-demand:

```js
function makeClass(phrase) {
  // declare a class and return it
  return class {
    sayHi() {
      alert(phrase);
    };
  };
}
```

### Getters/setters

classes may include getters/setters, computed properties etc.

Such class declaration works by creating getters and setters in the `prototype`.

### Computed names […]

```js
class User {
  ['say' + 'Hi']() {
    alert("Hello");
  }
}
```

### Class fields

"Class fields" is a syntax that allows to add any properties. They are set on individual objects, not the `prototype`.

```js
class User {
  name = "John";
  promptName = prompt("Name, please?", "John");

  sayHi() {
    alert(`Hello, ${this.name}!`);
  }
}
```

### Making bound methods with class fields

Instead of using a wrapper-function or bind the method to object, we can use arrow functions to solve the "losing this" problem. The function is created on a per-object basis.

```js
class Button {
  constructor(value) {
    this.value = value;
  }
  click = () => {
    alert(this.value);
  }
}
```

## 9.2 Class inheritance

### The “extends” keyword

The syntax to extend another class is: `class Child extends Parent`. Any expression is allowed after `extends`.

```js
class Rabbit extends Animal {
  hide() {
    alert(`${this.name} hides!`);
  }
}
```

Internally, `extends` keyword works using the good old prototype mechanics. It sets `Rabbit.prototype.[[Prototype]]` to `Animal.prototype`.

### Overriding a method

Usually we don’t want to totally replace a parent method, but rather to build on top of it to tweak or extend its functionality.

- `super.method(...)` to call a parent method.
- `super(...)` to call a parent constructor (inside our constructor only).

Arrow functions have no `super`. It’s taken from the outer function.

### Overriding constructor

If a class extends another class and has no `constructor`, then the following “empty” `constructor` is generated:

```js
class Rabbit extends Animal {
  constructor(...args) {
    super(...args);
  }
}
```

Constructors in inheriting classes must call `super(...)`, and do it before using `this`.

Explain: A derived constructor has a special internal property `[[ConstructorKind]]:"derived"`.

- When a regular function is executed with new, it creates an empty object and assigns it to `this`.
- When a derived constructor runs, it expects the parent constructor to do the job.

### Overriding class fields

Here's the tricky part:

```js
class Animal {
  name = 'animal'

  constructor() {
    alert(this.name); // (*)
  }
}

class Rabbit extends Animal {
  name = 'rabbit';
}

new Animal(); // animal
new Rabbit(); // animal
```

There’s no own constructor in `Rabbit`, so `Animal` constructor is called. Parent constructor always uses its own field value, not the overridden one.

Explain: The class field is initialized:

- Before constructor for the base class (that doesn’t extend anything).
- Imediately after `super()` for the derived class.

So that when the `Rabbit` constructor implicity calls `super()`, its class field has not been initialized yet, and that’s why the parent fields are used.

### Super: internals, [[HomeObject]]

### [[HomeObject]]

When a function is specified as a class or object method, its `[[HomeObject]]` property becomes that object. When `super()` is called, it takes the parent method from the prototype of its `[[HomeObject]]`, without using `this`.

### Methods are not “free”

`[[HomeObject]]` can’t be changed, so this bond is forever. If a method does not use `super`, then we can still consider it free and copy between objects. But with `super` things may go wrong.

### Methods, not function properties

`[[HomeObject]]` is defined for methods both in classes and in plain objects. For objects, methods must be specified exactly as `method()`, not as `"method: function()"`.
