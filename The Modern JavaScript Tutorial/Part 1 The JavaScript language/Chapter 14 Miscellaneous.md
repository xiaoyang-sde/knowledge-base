# Chapter 14: Miscellaneous

## 14.1 Proxy and Reflect

A `Proxy` object wraps another object and intercepts operations, like reading/writing properties and others, optionally handling them on its own, or transparently allowing the object to handle them.

```js
let proxy = new Proxy(target, handler)
```

- `target`: an object to wrap, can be anything, including functions.
- `handler`: proxy configuration

### No traps

As there are no traps, all operations on proxy are forwarded to target.

```js
let target = {};
let proxy = new Proxy(target, {});

proxy.test = 5;
alert(target.test); // 5
```

### Internal Method

For every internal method, there’s a trap in this table: the name of the method that we can add to the `handler` parameter of `new Proxy` to intercept the operation.

- `[[Get]]`: `get`
- `[[Set]]`: `set`
- `[[HasProperty]]`: `has` (`in` operator)
- `[[Delete]]`: `deleteProperty`
- `[[Call]]`: `apply`

Full list: [Proxy and Reflect](https://javascript.info/proxy#proxy)

#### Invariants

- `[[Set]]`: must return `true` if the value was written successfully, otherwise `false`.
- `[[Delete]]`: must return `true` if the value was deleted successfully, otherwise `false`.

### Default value with “get” trap

To intercept reading, the `handler` should have a method `get(target, property, receiver)`.

- `target`: the target object
- `property`: property name
- `receiver`: the object that’s going to be used as `this`

```js
numbers = new Proxy(numbers, {
  get(target, prop) {
    if (prop in target) {
      return target[prop];
    } else {
      return 0; // default value
    }
  }
});
```

The proxy should totally replace the target object everywhere. Otherwise it’s easy to mess up.

### Validation with “set” trap

The `set` trap triggers when a property is written: `set(target, property, value, receiver)`.

- `target`: the target object, the one passed as the first argument to `new Proxy`
- `property`: property name
- `value`: property value
- `receiver`: `this`

### Iteration with “ownKeys” and “getOwnPropertyDescriptor”

- `Object.getOwnPropertyNames(obj)`: non-symbol keys
- `Object.getOwnPropertySymbols(obj)`: symbol keys
- `Object.keys/values()`: non-symbol keys/values with `enumerable` flag
- `for..in`: non-symbol keys with `enumerable` flag, and also prototype keys

```js
user = new Proxy(user, {
  ownKeys(target) {
    return Object.keys(target).filter(key => !key.startsWith('_'));
  }
});
```

However, if we return a key that doesn’t exist in the object, `Object.keys` won’t list it. It calls the internal method `[[GetOwnProperty]]` for every property to get its descriptor. As there’s no property, its `descriptor` is empty, no `enumerable` flag, so it’s skipped. To fix this, add the following method to the `handler`:

```js
getOwnPropertyDescriptor(target, prop) {
    return {
        enumerable: true,
        configurable: true
        /* ...other flags, probable "value:..." */
    };
}
```

### “In range” with “has” trap

The has trap intercepts in calls: `has(target, property)`.

### Wrapping functions: "apply"

We can wrap a proxy around a function: `apply(target, thisArg, args)`.

Instead of using a decorator, a proxy can preserve all the other properties of the original function.

### Reflect

`Reflect` is a built-in object that simplifies creation of `Proxy`. It makes the direct calling to the internal methods available.

For every internal method, trappable by `Proxy`, there’s a corresponding method in `Reflect`, with the same name and arguments as the `Proxy` trap. Thus, we can use Reflect to forward an operation to the original object.

```js
set(target, prop, val, receiver) {
    alert(`SET ${prop}=${val}`);
    return Reflect.set(target, prop, val, receiver); // (2)
}

get(target, prop, receiver) {
  return Reflect.get(...arguments);
}
```

### Proxy limitations

Many built-in objects, for example `Map`, `Set`, `Date`, `Promise` and others make use of so-called “internal slots”. (`Array` has no internal slots.)

For instance, Map stores items in the internal slot `[[MapData]]`. Built-in methods access them directly, not via `[[Get]]`/`[[Set]`] internal methods. So `Proxy` can’t intercept that.

After a built-in object like that gets proxied, the proxy doesn’t have these internal slots, so built-in methods will fail.

So we need to bind the functions on the original object instead of the `Proxy` object:

```js
let proxy = new Proxy(map, {
  get(target, prop, receiver) {
    let value = Reflect.get(...arguments);
    return typeof value == 'function' ? value.bind(target) : value;
  }
});
```

### Private fields

A similar thing happens with private class fields.

Private fields are implemented using internal slots. JavaScript does not use `[[Get]]`/`[[Set]]` when accessing them. However, the solution above with binding the method makes it work.

### Proxy != target

If we use the original object as a key, and then proxy it, then the proxy can’t be found, because it's a different object.

Proxies can’t intercept a strict equality test `===`.

### Revocable proxies

A revocable proxy is a proxy that can be disabled.

```js
let object = {
  data: "Valuable data"
};
let {proxy, revoke} = Proxy.revocable(object, {});

revoke();

alert(proxy.data); // Error
```
A call to `revoke()` removes all internal references to the target object from the proxy, so they are no longer connected.


## 14.2 Eval: run a code string

The built-in `eval` function allows to execute a string of code.

```js
let code = 'alert("Hello")';
eval(code);
```

The eval’ed code is executed in the current lexical environment, so it can see outer variables. It can change outer variables as well.

```js
function f() {
  let a = 2;

  eval('alert(a)'); // 2
}
```

In strict mode, `eval` has its own lexical environment. So functions and variables, declared inside `eval`, are not visible outside.


### Eval is Evil

Its ability to access outer variables has side-effects since code minifiers rename local variables into shorter ones.

## 14.3 Currying

Currying is a transformation of functions that translates a function from callable as `f(a, b, c)` into callable as `f(a)(b)(c)`.

Helper function:
```js
function curry(f) {
  return function(a) {
    return function(b) {
      return f(a, b);
    };
  };
}

let curriedSum = curry(sum);
alert( curriedSum(1)(2) );
```

### Advanced curry implementation

```js
function curry(func) {
  return function curried(...args) {
    if (args.length >= func.length) {
      return func.apply(this, args);
    } else {
      return function(...args2) {
        return curried.apply(this, args.concat(args2));
      }
    }
  };
}
```

The currying requires the function to have a fixed number of arguments.

## 14.4 Reference Type

A dynamically evaluated method call can lose `this`.

```js
let user = {
  name: "John",
  hi() { alert(this.name); },
  bye() { alert("Bye"); }
};

user.hi(); // Works
(user.name == "John" ? user.hi : user.bye)(); // Error
```

### Reference type explained

- First, the dot '.' retrieves the property `obj.method`.
- Then parentheses `()` execute it.

To make `user.hi()` calls work, JavaScript uses a trick – the dot '.' returns not a function, but a value of the special Reference Type. It is used internally by the language.

The value of Reference Type is a three-value combination `(base, name, strict)`:
- `base`: the object
- `name`: the property name
- `strict`: true if `use strict`

```js
// Reference Type value
(user, "hi", true)
```

When parentheses `()` are called on the Reference Type, they receive the full information about the object and its method, and can set the right `this` (`=user` in this case).

Any other operation like assignment `hi = user.hi` discards the reference type as a whole, takes the value of `user.hi` (a function) and passes it on. So any further operation “loses” `this`.

## 14.5 BigInt

`BigInt` is a special numeric type that provides support for integers of arbitrary length.

```js
const bigint = 1234567890123456789012345678901234567890n;

const sameBigint = BigInt("1234567890123456789012345678901234567890");
```

### Math operators

```js
alert(1n + 2n); // 3
```

- We can’t mix bigints and regular numbers.
- We should explicitly convert them using either `BigInt()` or `Number()`.
- The unary plus is not supported on bigints.

### Comparisons

Comparisons, such as `<`, `>` work with bigints and numbers just fine.

Numbers and bigints belong to different types, they can be equal `==`, but not strictly equal `===.`

### Boolean operations

In if, bigint `0n` is falsy, other values are truthy.

```js
alert( 1n || 2 ); // 1 (1n is considered truthy)
alert( 0n || 2 ); // 2 (0n is considered falsy)
```
