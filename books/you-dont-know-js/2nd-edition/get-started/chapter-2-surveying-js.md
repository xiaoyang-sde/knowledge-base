# Chapter 2: Surveying JS

## Each File is a Program

In JS, each standalone file is its own separate program.

The only way multiple standalone .js files act as a single program is by sharing their state via the "global scope." Since ES6, JS has also supported a module format.

## Values

Values come in two forms in JS: primitive and object. Values are embedded in programs using literals.

For strings, the better approach is to use `'` or `"` unless you need interpolation.

## Arrays And Objects

JS arrays can hold any value type, either primitive or object \(including other arrays\).

Objects are more general: an unordered, keyed collection of any various values. Functions, like arrays, are a special kind of object.

## Functions

In JS, we should consider "function" as "procedure", which is a collection of statements that can be invoked many times.

Functions are values that can be assigned and passed around.

### Functions declaration

```javascript
function awesomeFunction(coolThings) {
    // ..
    return amazingStuff;
}
```

### Function expression

```javascript
// let awesomeFunction = ..
// const awesomeFunction = ..
var awesomeFunction = function(coolThings) {
    // ..
    return amazingStuff;
};
```

## Comparisons

### Equal...ish

In other words, we must be aware of the nuanced differences between an **equality** comparison and an **equivalence** comparison.

```javascript
NaN === NaN;            // false
0 === -0;               // true
```

* For `NaN` comparisons, use the `Number.isNaN(..)` utility.
* For `-0` comparison, use the `Object.is(..) utility`.

For object comparison with `===`, the structure and contents don't matter, only the reference identity.

```javascript
[ 1, 2, 3 ] === [ 1, 2, 3 ];    // false
{ a: 42 } === { a: 42 }         // false
(x => x * 2) === (x => x * 2)   // false
```

### Coercive Comparisons

Coercion means a value of one type being converted to its respective representation in another type.

If the comparison is between the same value type, both `==` and `===` do exactly the same thing.

`==` prefers primitive numeric comparisons. However, when both values being compared are strings, they use alphabetical comparison.

## How We Organize in JS

### Classes

A class in a program is a definition of a "type" of custom data structure that includes both data and behaviors that operate on that data.

Inheritance is a powerful tool for organizing data/behavior in separate logical units \(classes\), but allowing the child class to cooperate with the parent by accessing/using its behavior and data.

### Modules

#### Classic Modules

A classic module is an outer function which returns an "instance" of the module with manny functions exposed that can operate on the module instance's internal data.

Because a module of this form is just a function, and calling it produces an "instance" of the module, another description for these functions is "module factories".

```javascript
function Publication(title, author, pubDate) {
    var publicAPI = {
        print() {
            console.log(`
                Title: ${ title }
                By: ${ author }
                ${ pubDate }
            `);
        }
    };

    return publicAPI;
}
```

