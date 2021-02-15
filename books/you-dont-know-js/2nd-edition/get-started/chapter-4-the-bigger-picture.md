# Chapter 4: The Bigger Picture

## Pillar 1: Scope and Closure

Scopes nest inside each other, and only variables at the level of scope nesting or in higher/outer scopes are accessible.

The scope unit boundaries, and how variables are organized in them, is determined at the time the program is parsed.

hoisting: when all variables declared anywhere in a scope are treated as if they're declared at the beginning of the scope.

`var`-declared variables are function scoped, even if they appear inside a block.

Closure is a natural result of lexical scope when the language has functions as first-class values. It maintains access to its original scope variables even if it is passed around as a value.

## Pillar 2: Prototypes

JS is one of very few languages where you have the option to create objects directly and explicitly, without first defining their structure in a class.

Behavior delegation: Embrace objects as objects, forget classes altogether, and let objects cooperate through the prototype chain.

