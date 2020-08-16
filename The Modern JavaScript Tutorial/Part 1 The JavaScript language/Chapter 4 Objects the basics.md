# Chapter 4: Objects: the basics

## 4.1 Objects

Objects are used to store keyed collections of various data and more complex entities.

An object can be created with figure brackets {…} with an optional list of properties.

An empty object ('empty cabinet') can be created using one of two syntaxes:

```js
let user = new Object(); // "object constructor" syntax
let user = {};  // "object literal" syntax
```

### Literals and properties

```js
let user = {     // an object
  name: "John",  // by key "name" store value "John"
  age: 30,        // by key "age" store value 30
  "likes birds": true  // multiword property name must be quoted
};
```

```js
alert(user.name); // John
user.isAdmin = true;
delete user.age;
```

An object declared as const can be modified. The const would give an error only if we try to set user=... as a whole.

### Square brackets

The dot requires the key to be a valid variable identifier. There’s an alternative 'square bracket notation' that works with any string:

```js
user["likes birds"] = true;
```

```js
let key = "name";
alert( user[key] );
alert( user.key ) // undefined
```

### Computed properties

We can use square brackets in an object literal, when creating an object.

```js
let fruit = prompt("Which fruit to buy?", "apple");

let bag = {
  [fruit + 'Computers']: 5 // the name of the property is taken from the variable fruit
};
```

### Property value shorthand

The use-case of making a property from a variable is so common, that there’s a special property value shorthand to make it shorter:

```js
function makeUser(name, age) {
  return {
    name, // same as name: name
    age,  // same as age: age
    // ...
  };
}
```

### Property names limitations

```js
// these properties are all right
let obj = {
  for: 1,
  let: 2,
  return: 3
};
```

The property names can be strings or symbols. Other types are automatically converted to strings.

### Property existence test, 'in' operator

Reading a non-existing property just returns undefined instead of an error.

```js
alert(user.noSuchProperty === undefined); // true means "no such property"
```

There's also an "in" operator:

```js
"key" in object
```

When an object property exists, but stores undefined, the comparison failed and the "in" operator works.

### The "for...in" loop

To walk over all keys of an object, there exists a special form of the loop: for..in. This is a completely different thing from the for(;;) construct that we studied before.

```js
for (key in object) {
  // executes the body for each key among object properties
}
```

### Ordered like an object

"ordered in a special fashion": integer properties are sorted, others appear in creation order.

The "integer property" term here means a string that can be converted to-and-from an integer without a change.

To avoid that order of integers, we can use "+1" instead of "1".

```js
let codes = {
  "+49": "Germany",
  "+41": "Switzerland",
  "+44": "Great Britain",
  // ..,
  "+1": "USA"
};

for (let code in codes) {
  alert( +code ); // 49, 41, 44, 1
}
```

There are many other kinds of objects in JavaScript:

- Array to store ordered data collections,
- Date to store the information about the date and time,
- Error to store the information about an error.

## 4.2 Object copying, references

One of the fundamental differences of objects vs primitives is that they are stored and copied "by reference". A variable stores not the object itself, but its "address in memory", in other words "a reference" to it.
When an object variable is copied – the reference is copied, the object is not duplicated.

```js
let user = { name: "John" };

let admin = user; // copy the reference
```

### Comparison by reference

"==" and "===" works same for objects. Two objects are equal only if they are the same object.

For comparisons like obj1 > obj2 or for a comparison against a primitive obj == 5, objects are converted to primitives.

### Cloning and merging, Object.assign

To create an identical object:

```js
let clone = {}; // the new empty object

// let's copy all user properties into it
for (let key in user) {
  clone[key] = user[key];
}
```

We can use the method Object.assign for that.

```js
Object.assign(dest, [src1, src2, src3...])
```

- dest: target object
- src1, ... srcN: source objects
- It copies the properties of all source objects src1, ..., srcN into the target dest. (Overwritten if exists)
- The call returns dest.

```js
let user = { name: "John" };

let permissions1 = { canView: true };
let permissions2 = { canEdit: true };

// copies all properties from permissions1 and permissions2 into user
Object.assign(user, permissions1, permissions2);

// Simply cloning an object
let clone = Object.assign({}, user);
```

### Nested cloning

Properties can be references to other objects. The cloning loop that examines each value of the properties and replicate the structure is called a "deep cloning".

## 4.3 Garbage collection

What happens when something is not needed any more?

### Reachability

The main concept of memory management in JavaScript is reachability. (The variables that are accessible or usable somehow.)

Base set of inherently reachable values:

- Local variables and parameters of the current function.
- Variables and parameters for other functions on the current chain of nested calls.
- Global variables.

These values are called roots. Any other value is considered reachable if it's reachable from a root by a reference or by a chain of references.
The garbage collector monitors all objects and removes those that have become unreachable.

### Internal algorithms

The basic garbage collection algorithm is called "mark-and-sweep".

- The garbage collector takes roots and "marks" (remembers) them.
- Then it visits and "marks" all references from them.
- Then it visits marked objects and marks their references. All visited objects are remembered, so as not to visit the same object twice in the future.
- All objects except marked ones are removed.

### Optimization

- Generational collection – objects are split into two sets: "new ones" and "old ones". Many objects appear, do their job and die fast, they can be cleaned up aggressively. Those that survive for long enough, become "old" and are examined less often.
- Incremental collection – if there are many objects, and we try to walk and mark the whole object set at once, it may take some time and introduce visible delays in the execution. So the engine tries to split the garbage collection into pieces. Then the pieces are executed one by one, separately.
- Idle-time collection – the garbage collector tries to run only while the CPU is idle, to reduce the possible effect on the execution.

## 4.4 Object methods, "this"

Actions are represented in JavaScript by functions in properties.

```js
user.sayHi = function() {
  alert("Hello!");
};
```

Shorthand:

```js
user = {
  sayHi() { // same as "sayHi: function()"
    alert("Hello");
  }
};
```

### "this" in methods

The value of this is the object "before dot", the one used to call the method.

"this" is not bound, as there's no syntax error in the following example:

```js
function sayHi() {
  alert( this.name );
}
```

```js
let user = { name: "John" };
let admin = { name: "Admin" };

function sayHi() {
  alert( this.name );
}

// use the same function in two objects
user.f = sayHi;
admin.f = sayHi;
```

Its value is evaluated at call-time and does not depend on where the method was declared.

Arrow functions are special: they don’t have their "own" this. It will take from the outer normal function.

## 4.5 Constructor, operator "new"

### Constructor function

Constructor functions technically are regular functions. They are used to to implement reusable object creation code.
1. They are named with capital letter first.
2. They should be executed only with "new" operator.

```js
function User(name) {
  this.name = name;
  this.isAdmin = false;
}

let user = new User("Jack");
```

When a function is executed with new, it does the following steps:

1. A new empty object is created and assigned to this.
2. The function body executes. Usually it modifies this, adds new properties to it.
3. The value of this is returned.

```js
function User(name) {
  // this = {};  (implicitly)

  // add properties to this
  this.name = name;
  this.isAdmin = false;

  // return this;  (implicitly)
}
```

If we have many lines of code all about creation of a single complex object, we can wrap them in constructor function.

### Constructor mode test: new.target

Inside a function, we can check whether it was called with new or without it, using a special new.target property. It equals the function if called with new.

```js
function User(name) {
  if (!new.target) { // if you run me without new
    return new User(name); // ...I will add new for you
  }

  this.name = name;
}
```

### Return from constructors

- If return is called with an object, then the object is returned instead of this.
- If return is called with a primitive, it’s ignored.

We can omit parentheses after new, if it has no arguments.

### Methods in constructor

We can add to this not only properties, but methods as well.

## 4.6 Optional chaining '?.'

The optional chaining ?. is an error-proof way to access nested object properties, even if an intermediate property doesn’t exist.

The optional chaining ?. stops the evaluation and returns undefined if the part before ?. is undefined or null. The variable before ?. must be declared.

```js
let user = {};
alert( user?.address?.street ); // undefined (no error)
```

## Short-circuiting

```js
let user = null;
let x = 0;

user?.sayHi(x++); // nothing happens

alert(x); // 0, value not incremented
```

?.() is used to call a function that may not exist.

```js
let user2 = {};
user2.admin?.();
```

The ?.[] syntax also works, if we’d like to use brackets [] to access properties instead of dot.

```js
alert( user1?.[key] ); // John
```

Also we can use ?. with delete:

```js
delete user?.name;
```

It doesn't works with writing:

```js
user?.name = "John"; // Error, doesn't work
// because it evaluates to undefined = "John"
```

## 4.7 Symbol type

Symbol is a primitive type for unique identifiers.

```js
// id is a new symbol
let id = Symbol("id"); // symbol name is "id"
```

Symbols are guaranteed to be unique. Even if we create many symbols with the same description, they are different values.

Symbols don't auto-convert to a string, because strings and symbols are fundamentally different and should not accidentally convert one into another. (Need to explititly call .toString() on it.)

### "Hidden" properties

Symbols allow us to create 'hidden' properties of an object, that no other part of code can accidentally access or overwrite.

```js
let user = { // belongs to another code
  name: "John"
};

let id = Symbol("id");

user[id] = 1;
```

A symbol cannot be accessed accidentally, the third-party code probably won’t even see it, so it's probably all right to do. To access  Symbols in a object, use `Object.getOwnPropertySymbols(obj)`. 

If we want to use a symbol in an object literal {...}, we need square brackets around it.

```js
let id = Symbol("id");

let user = {
  name: "John",
  [id]: 123 // not "id": 123
};
```

Symbolic properties do not participate in for..in loop and not show in Object.keys(obj). If another script or a library loops over our object, it won’t unexpectedly access a symbolic property. In contrast, Object.assign copies both string and symbol properties.

### Global symbols

Sometimes we want same-named symbols to be same entities.

In order to read (create if absent) a symbol from the registry, use `Symbol.for(key)`. That function will check the global registry, and if that symbol does not exist, it. creates a new one and store that.

`Symbol.keyFor(sym)` returns a name by a global symbol. If the symbol is not global, it returns undefined. It reads the `description` of the Symbol.

### System symbols

- `Symbol.hasInstance`
- `Symbol.isConcatSpreadable`
- `Symbol.iterator`
- `Symbol.toPrimitive`

## 4.8 Object to primitive conversion

1. All objects are true in a boolean context. There are only numeric and string conversions.
2. The numeric conversion happens when we subtract objects or apply mathematical functions.
3. As for the string conversion – it usually happens when we output an object like alert(obj).

### ToPrimitive

- "string": An operation on an object that expects a string.
- "number": Do math.
- "default": The operator is not sure what type to expect. (Example: binary plus + can work both with strings and numbers)

All built-in objects except for the Date object implement "default" conversion the same way as "number".

To do the conversion, JavaScript tries to find and call three object methods:

1. Call obj[Symbol.toPrimitive](hint) – the method with the symbolic key Symbol.toPrimitive (system symbol), if such method exists.
2. Otherwise if hint is "string", try obj.toString() and obj.valueOf(), whatever exists.
3. Otherwise if hint is "number" or "default, try obj.valueOf() and obj.toString(), whatever exists.

### Symbol.toPrimitive

There’s a built-in symbol named Symbol.toPrimitive that should be used to name the conversion method.

```js
obj[Symbol.toPrimitive] = function(hint) {
  // must return a primitive value
  // hint = one of "string", "number", "default"
};
```

### toString/valueOf

There is no control whether toString returns exactly a string, or whether Symbol.

toPrimitive method returns a number for a hint "number". These methods must return a primitive, not an object.

For historical reasons, if toString or valueOf returns an object, there’s no error, but such value is ignored.
