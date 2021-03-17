# Chapter 2: JavaScript Fundamentals

## 2.1 Hello, world!

### The "script" tag

```markup
<script>
  alert( 'Hello, world!' );
</script>
```

### Modern Markup

The `<script>` tag has a few attributes that are rarely used nowadays but can still be found in old code:

* The type attribute: `<script type=…>` \(Now, it can be used for JavaScript modules.\)
* The language attribute: `<script language=…>` \(No longer makes sense because JavaScript is the default language.\)
* Comments before and after scripts.

### External Scripts

If we have a lot of JavaScript code, we can put it into a separate file. As a rule, only the simplest scripts are put into HTML. More complex ones reside in separate files, since the browser will download it and store it in its cache.

```markup
<script src="/path/to/script.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/lodash.js/4.17.11/lodash.js"></script>
```

If `src` is set, the script content is ignored.

## 2.2 Code structure

### Statements

Statements are syntax constructs and commands that perform actions. Usually, statements are written on separate lines to make the code more readable.

### Semicolons

A semicolon may be omitted in most cases when a line break exists. JavaScript interprets the line break as an "implicit" semicolon.

However, in this case, the line break does not mean the semicolon:

```javascript
alert(3 +
1
+ 2);
```

JavaScript does not assume a semicolon before square brackets `[...]`. This example will cause error:

```javascript
alert("There will be an error")

[1, 2].forEach(alert)
```

### Comments

* One-line comments start with two forward slash characters //.
* Multiline comments start with a forward slash and an asterisk / _and end with an asterisk and a forward slash_ /.

There may not be /_..._/ inside another /_..._/. There are many tools which minify code before publishing to a production server.

## 2.3 The modern mode, "use strict"

JavaScript evolved without compatibility issues. Any mistake or an imperfect decision made by JavaScript"s creators got stuck in the language forever.

### "use strict"

```javascript
"use strict";
// this code works the modern way
...
```

`"use strict";` can be put at the beginning of a function, but usually people use it for the whole script.

* Ensure that "use strict" is at the top
* There"s no way to cancel "use strict"

### Browser Console

```text
'use strict'; <Shift+Enter for a newline>
//  ...your code
<Enter to run>
```

### Should we "use strict"?

We don't need to explicitly add it to the top. Modern JavaScript supports "classes" and "modules" that enable use strict automatically.

## 2.4 Variables

### A variable

A variable is a "named storage" for data. To create a variable in JavaScript, use the let keyword.

```javascript
let message = 'Hello!';
let user = 'John', age = 25, message = 'Hello';
```

In older scripts, you may also find var keyword. Declaring twice triggers an error.

### Variable naming

* The name must contain only letters, digits, or the symbols `$` and `_`.
* The first character must not be a digit.
* camelCase is commonly used.
* Case matters, and non-Latin letters are allowed, but not recommended.

Reserved names: For example, let, class, return, and function are reserved.

An assignment without use strict: It was technically possible to create a variable by a mere assignment of the value without using let. This is a bad practice.

### Constants

To declare a constant \(unchanging\) variable, use const instead of let. They cannot be reassigned. There is a widespread practice to use constants as aliases for difficult-to-remember values that are known prior to execution.

```javascript
const COLOR_RED = "#F00";
const COLOR_GREEN = "#0F0";
const COLOR_BLUE = "#00F";
const COLOR_ORANGE = "#FF7F00";
```

### Name things right

A variable name should have a clean, obvious meaning, describing the data that it stores.

* Use human-readable names like userName or shoppingCart.
* Stay away from abbreviations or short names like a, b, c.
* Make names maximally descriptive and concise. Examples of bad names are data and value.
* Agree on terms within your team and in your own mind.
* An extra variable is good, not evil, as it won"t create performance issues.

## 2.5 Data types

There are eight basic data types in JavaScript. We can put any type in a variable.

### Number

The number type represents both integer and floating point numbers.

* Special numeric values: Infinity, -Infinity, and NaN.
* NaN represents a computational error. It is a result of an incorrect or an undefined mathematical operation.
* Mathematical operations are safe as they won't cause fatal errors.

### BigInt

The "number" type cannot represent integer values larger than \(253-1\) \(9007199254740991\), or less than -\(253-1\) for negatives. It s created by appending n to the end of an integer:

```javascript
const bigInt = 1234567890123456789012345678901234567890n;
```

### String

In JavaScript, there are 3 types of quotes. 1. Double quotes: "Hello". 2. Single quotes: 'Hello'. 3. Backticks: \`Hello\`.

* Double and single quotes are "simple" quotes.
* Backticks are "extended functionality" quotes.

```javascript
alert( `the result is ${1 + 2}` );
```

* `${}` can contains a variable like name or an arithmetical expression like 1 + 2 or something more complex.
* There's no character type in JavaScript.

### Boolean \(logical type\)

The boolean type has only two values: true and false. Boolean values also come as a result of comparisons.

### The "null" value

In JavaScript, null is not a "reference to a non-existing object" or a "null pointer" like in some other languages. It"s just a special value which represents "nothing", "empty", or "value unknown".

### The "undefined" value

The meaning of undefined is "value is not assigned".

```javascript
let age; // undefined
```

It is possible to explicitly assign undefined to a variable.

### Objects and Symbols

All other types are called "primitive" because their values can contain only a single thing. Objects are used to store collections of data and more complex entities.

The symbol type is used to create unique identifiers for objects.

### The typeof operator

The call to `typeof x` returns a string with the type name. It supports two forms of syntax:

1. As an operator: typeof x.
2. As a function: typeof\(x\).

```javascript
typeof "foo" // "string"

typeof Symbol("id") // "symbol"

typeof Math // "object"  (1)

typeof null // "object"  (2)

typeof alert // "function"  (3)
```

1. `Math` is a built-in object that provides mathematical operations.
2. The result of `typeof null` is `"object"`. It's an officially recognized error but kept for compatibility.
3. The result of `typeof alert is "function"`, because `alert` is a function. Functions belong to the object type. That comes from the early days of JavaScript.

## 2.6. Interaction: alert, prompt, confirm

The modal window means that the visitor can"t interact with the rest of the page, press other buttons, etc, until they have dealt with the window.

1. The exact location of the modal window is determined by the browser.
2. The exact look of the window also depends on the browser.

### alert

It shows a message and waits for the user to press "OK".

```javascript
alert("Hello");
```

### prompt

It shows a modal window with a text message, an input field for the visitor, and the buttons OK/Cancel.

```javascript
let result = prompt(title, [default]);
let result = prompt(title, ''); // <-- for IE
```

### confirm

```javascript
result = confirm(question);
```

The function confirm shows a modal window with a question and two buttons: OK and Cancel. The result is true if OK is pressed and false otherwise.

## 2.7 Type Conversions

Most of the time, operators and functions automatically convert the values given to them to the right type.

### String Conversion

We can call the String\(value\) function to convert a value to a string.

String conversion is mostly obvious. A false becomes "false", null becomes "null", etc.

Space characters \(\t, \n, etc.\) are trimmed off string start and end when the string is converted to a number.

### Numeric Conversion

Numeric conversion happens in mathematical functions and expressions automatically. We can use the Number\(value\) function to explicitly convert a value to a number.

Explicit conversion is usually required when we read a value from a string-based source like a text form but expect a number to be entered. If the string is not a valid number, the result of such a conversion is NaN.

* undefined -&gt; NaN
* null -&gt; 0
* true, false -&gt; 1, 0
* string -&gt; Whitespaces from the start and end are removed. If the remaining string is empty, the result is 0. An error gives NaN.

### Boolean Conversion

Values that are intuitively "empty", like 0, an empty string, null, undefined, and NaN, become false. The string "0" or " " is true.

## 2.8 Basic operators, maths

### Terms: unary, binary, operand

* An operand is what operators are applied to.
* An operator is unary if it has a single operand.
* An operator is binary if it has two operands.

### Maths

* Addition +
* Subtraction -
* Multiplication \*
* Division /
* Remainder %
* Exponentiation \*\*

### String concatenation with binary +

If the binary + is applied to strings, it merges \(concatenates\) them. If any of the operands is a string, then the other one is converted to a string too.

The operators work one after another:

```javascript
alert(2 + 2 + '1' ); // "41" and not "221"
```

### Numeric conversion, unary +

The plus + exists in two forms: the binary form and the unary form.

The unary form:

* Doesn"t do anything to numbers.
* If the operand is not a number, the unary plus converts it into a number.

```javascript
let apples = "2";
let oranges = "3";

// both values converted to numbers before the binary plus
alert( +apples + +oranges ); // 5
```

### Assignment

An assignment = is also an operator. The call x = value writes the value into x and then returns it.

```javascript
let c = 3 - (a = b + 1);
```

#### Chaining assignments

```javascript
let a, b, c;
a = b = c = 2 + 2;
```

### Modify-in-place

Applies an operator to a variable and store the new result in that same variable.

```javascript
let n = 2;
n += 5; // now n = 7 (same as n = n + 5)
n *= 2; // now n = 14 (same as n = n * 2)
```

Such operators have the same precedence as a normal assignment, so they run after most other calculations.

### Increment/Decrement

* Increment ++ increases a variable by 1.
* Decrement -- decreases a variable by 1.
* Increment/decrement can only be applied to variables.
* If the result of increment/decrement is not used, there is no difference.
* Prefix: Increment a value and immediately use the result of the operator.
* Postfix: Increment a value but use its previous value.

```javascript
let counter = 1;
let a = ++counter;
let b = counter++;

alert(a); // 2
alert(b); // 2
```

Their precedence is higher than most other arithmetical operations.

### Bitwise operators

Bitwise operators treat arguments as 32-bit integer numbers and work on the level of their binary representation.

* AND \( & \)
* OR \( \| \)
* XOR \( ^ \)
* NOT \( ~ \)
* LEFT SHIFT \( &lt;&lt; \)
* RIGHT SHIFT \( &gt;&gt; \)
* ZERO-FILL RIGHT SHIFT \( &gt;&gt;&gt; \)

Read [MDN: Expressions and operators](https://developer.mozilla.org/en/docs/Web/JavaScript/Reference/Operators/Bitwise_Operators) for more details as they are rarely used in web development.

### Comma

The comma operator allows us to evaluate several expressions, dividing them with a comma. Each of them is evaluated but only the result of the last one is returned.

```javascript
let a = (1 + 2, 3 + 4);

alert( a ); // 7 (the result of 3 + 4)
```

Comma has a very low precedence, even lower than the assignment operator.

## 2.9 Comparisons

### Boolean is the result

```javascript
alert( 2 > 1 );  // true (correct)
```

When values of different types are compared, they get converted to numbers \(with the exclusion of a strict equality check\).

A comparison result can be assigned to a variable.

### String comparison

JavaScript uses the so-called "dictionary" or "lexicographical" order.

```javascript
alert( 'Glow' > 'Glee' ); // true
```

1. Compare the first character of both strings.
2. If the first character from the first string is greater \(or less\) than the other string"s, then the first string is greater \(or less\) than the second. We"re done.
3. Otherwise, if both strings" first characters are the same, compare the second characters the same way.
4. If both strings end at the same length, then they are equal. Otherwise, the longer string is greater.

'A' is smaller than 'a', ecause the lowercase character has a greater index in the internal encoding table JavaScript uses \(Unicode\).

### Comparison of different types

When comparing values of different types, JavaScript converts the values to numbers.

```javascript
alert( '01' == 1 ); // true
```

### Strict equality

A regular equality check == has a problem. It cannot differentiate 0 or empty strings from false.

A strict equality operator === checks the equality without type conversion.

```javascript
alert('' == false); // true
alert(0 === false); // false
alert(0 !== false); // true
```

### Comparison with null and undefined

There"s a non-intuitive behavior when null or undefined are compared to other values.

```javascript
alert( null === undefined ); // false
alert( null == undefined ); // true
```

For maths and other comparisons &lt; &gt; &lt;= &gt;=, null/undefined are converted to numbers: null becomes 0, while undefined becomes NaN.

```javascript
alert( null > 0 );  // (1) false
alert( null == 0 ); // (2) false
alert( null >= 0 ); // (3) true
```

Comparisons convert null to a number, treating it as 0. The equality check == for undefined and null is defined such that, they equal each other and don"t equal anything else.

```javascript
alert( undefined > 0 ); // false
alert( undefined < 0 ); // false
alert( undefined == 0 ); // false
```

* Comparisons \(1\) and \(2\) return false because undefined gets converted to NaN.
* undefined only equals null, undefined, and no other value.

To avoid problems:

* Treat any comparison with undefined/null except the strict equality === with exceptional care.
* Don"t use comparisons &gt;= &gt; &lt; &lt;= with a variable which may be null/undefined.

## 2.10 Conditional branching: if, '?'

### The "if" statement

The if\(...\) statement evaluates a condition in parentheses and, if the result is true, executes a block of code.

```javascript
if (year == 2015) {
  alert( "That's correct!" );
  alert( "You're so smart!" );
}
```

### Boolean conversion

The if \(…\) statement evaluates the expression in its parentheses and converts the result to a boolean.

A number 0, an empty string "", null, undefined, and NaN all become false.

```javascript
if (0) { // 0 is falsy
  ...
}
```

### The "else" clause

The if statement may contain an optional "else" block. It executes when the condition is false.

### Several conditions: "else if"

The final else is optional.

```javascript
if (year < 2015) {
  alert( 'Too early...' );
} else if (year > 2015) {
  alert( 'Too late' );
} else {
  alert( 'Exactly!' );
}
```

### Conditional operator

We need to assign a variable depending on a condition.

```javascript
if (age > 18) {
  accessAllowed = true;
} else {
  accessAllowed = false;
}

let result = condition ? value1 : value2;
let accessAllowed = age > 18 ? true : false;
let accessAllowed = age > 18;
```

### Multiple '?'

A sequence of question mark operators ? can return a value that depends on more than one condition.

```javascript
let message = (age < 3) ? 'Hi, baby!' :
  (age < 18) ? 'Hello!' :
  (age < 100) ? 'Greetings!' :
  'What an unusual age!';
```

### Non-traditional use of "?"

Sometimes the question mark ? is used as a replacement for if:

```javascript
(company == 'Netscape') ?
   alert('Right!') : alert('Wrong.');
```

We don"t assign a result to a variable here. Instead, we execute different code depending on the condition.

## 2.11 Logical operators

### \|\| \(OR\)

Returns true if any of the given conditions is true.

```javascript
result = a || b;
```

If an operand is not a boolean, it"s converted to a boolean for the evaluation.

The OR \|\| operator does the following:

* Evaluates operands from left to right.
* For each operand, converts it to boolean. If the result is true, stops and returns the original value of that operand.
* If all operands have been evaluated \(i.e. all were false\), returns the last operand.

A chain of OR "\|\|" returns the first truthy value or the last one if no truthy value is found.

```javascript
alert( null || 0 || 1 ); // 1
alert( undefined || null || 0 ); // 0
```

Another feature of OR \|\| operator is the so-called "short-circuit" evaluation. It means that \|\| processes its arguments until the first truthy value is reached, and then the value is returned immediately, without even touching the other argument.

### && \(AND\)

```javascript
result = a && b;
result = value1 && value2 && value3;
```

AND returns true if both operands are truthy and false otherwise.

* Evaluates operands from left to right.
* For each operand, converts it to a boolean. If the result is false, stops and returns the original value of that operand.
* If all operands have been evaluated \(i.e. all were truthy\), returns the last operand.

A chain of AND returns the first falsy value or the last value if none were found.

The precedence of AND && operator is higher than OR \|\|.

### ! \(NOT\)

The boolean NOT operator is represented with an exclamation sign !.

```javascript
result = !value;
```

1. Converts the operand to boolean type: true/false.
2. Returns the inverse value.

A double NOT !! is sometimes used for converting a value to boolean type.

```javascript
alert(!!"non-empty string");
alert(Boolean("non-empty string"));
```

The precedence of NOT ! is the highest of all logical operators, so it always executes first, before && or \|\|.

## 2.12 Nullish coalescing operator '??'

The nullish coalescing operator ?? provides a short syntax for selecting a first "defined" variable from the list.

The result of `a ?? b`:

* a if it"s not null or undefined,
* b, otherwise.

```javascript
alert(firstName ?? lastName ?? nickName ?? "Anonymous");
```

### Comparison with \|\|

The OR \|\| operator can be used in the same way as ??.

* \|\| returns the first truthy value.
* ?? returns the first defined value. \(**0 is defined but is not true**\)

### Precedence

?? is evaluated after most other operations, but before = and ?. **It's forbidden to use ?? together with && and \|\| operators**.

Use explicit parentheses to work around it:

```javascript
let x = (1 && 2) ?? 3; // Works
```

## 2.13 Loops: while and for

### The "while" loop

```javascript
while (condition) {
  // code
  // so-called "loop body"
}
```

While the condition is truthy, the code from the loop body is executed. The condition is evaluated and converted to a boolean by while. A single execution of the loop body is called an iteration.

### The "do...while" loop

```javascript
do {
  // loop body
} while (condition);
```

This form of syntax should only be used when you want the body of the loop to execute at least once regardless of the condition being truthy.

### The "for" loop

```javascript
for (begin; condition; step) {
  // ... loop body ...
}

for (let i = 0; i < 3; i++) { // shows 0, then 1, then 2
  alert(i);
}
```

* begin
* condition
* body
* step

### Skipping parts

Any part of for can be skipped. For example, we can omit begin if we don"t need to do anything at the loop start.

```javascript
for (;;) {
  // repeats without limits
}
```

* break
* continue

```javascript
(i > 5) ? alert(i) : continue; // continue isn't allowed here
```

A label is an identifier with a colon before a loop:

```javascript
labelName: for (...) {
    break labelName;
}
```

The break \ statement in the loop below breaks out to the label. Labels do not allow us to jump into an arbitrary place in the code.

## 2.14 The "switch" statement

A switch statement can replace multiple if checks.

```javascript
switch(x) {
  case 'value1':  // if (x === 'value1')
    ...
    [break]

  case 'value2':  // if (x === 'value2')
    ...
    [break]

  default:
    ...
    [break]
}
```

* The value of x is checked for a strict equality to the value from the first case \(that is, value1\) then to the second \(value2\) and so on.
* If the equality is found, switch starts to execute the code starting from the corresponding case, until the nearest break \(or until the end of switch\).
* If no case is matched then the default code is executed \(if it exists\).
* If there is no break then the execution continues with the next case without any checks.

### Grouping cases

```javascript
case 3: // (*) grouped two cases
case 5:
    alert('Wrong!');
    break
```

## 2.15 Functions

### Function Declaration

```javascript
function showMessage() {
  alert( 'Hello everyone!' );
}
```

* A variable declared inside a function is only visible inside that function.
* A function can access an outer variable as well. \(Global variables\)
* If a same-named variable is declared inside the function then it shadows the outer one.
* Global variables are visible from any function \(unless shadowed by locals\).

### Parameters

```javascript
function showMessage(from, text) { // arguments: from, text
  alert(from + ': ' + text);
}
```

If a parameter is not provided, then its value becomes undefined. Use default value to prevent that:

```javascript
function showMessage(from, text = "no text given") {
  alert( from + ": " + text );
}
function showMessage(from, text = anotherFunction()) {
}
```

We can set the default value manually after the function is declared:

```javascript
function showMessage(text) {
  if (text === undefined) {
    text = 'empty message';
  }
  text = text ?? 'empty message';
}
```

### Returning a value

A function can return a value back into the calling code as the result.

```javascript
function sum(a, b) {
  return a + b;
}
```

When the execution reaches the return directive, the function stops, and the value is returned to the calling code.

A function with an empty return or without it returns undefined.

### 2.16 Function expressions

In JavaScript, a function is not a "magical language structure", but a special kind of value.

```javascript
let sayHi = function() {
  alert( "Hello" );
};
```

We can copy a function to another variable.

### Callback functions

```javascript
function ask(question, yes, no) {
  if (confirm(question)) yes()
  else no();
}

ask(
  "Do you agree?",
  function() { alert("You agreed."); },
  function() { alert("You canceled the execution."); }
);
```

### Function Expression vs Function Declaration

* Function Declaration: a function, declared as a separate statement, in the main code flow.
* Function Expression: a function, created inside an expression or inside another syntax construct. 
* Function Declarations can be called earlier than they are defined. When JavaScript prepares to run the script, it first looks for global Function Declarations in it and creates the functions. \(initialization stage\)
* In strict mode, when a Function Declaration is within a code block, it"s visible everywhere inside that block.
* Function Expressions are created when the execution reaches them.

```javascript
sayHi("John"); // Hello, John

function sayHi(name) {
  alert( `Hello, ${name}` );
}
```

```javascript
if (age < 18) {
  welcome();               // \   (runs)
                           //  |
  function welcome() {     //  |
    alert("Hello!");       //  |  Function Declaration is available
  }                        //  |  everywhere in the block where it's declared
                           //  |
  welcome();               // /   (runs)

} else {
  function welcome() {
    alert("Greetings!");
  }
}

// Here we're out of curly braces,
// so we can not see Function Declarations made inside of them.

welcome(); // Error: welcome is not defined
```

## 2.17 Arrow functions, the basics

There"s another very simple and concise syntax for creating functions.

```javascript
let func = (arg1, arg2, ...argN) => expression;
let sum = (a, b) => a + b;
let sayHi = () => alert("Hello!");
```

* If we have only one argument, then parentheses around parameters can be omitted.
* If there are no arguments, parentheses will be empty. \(but they should be present\)

```javascript
let sum = (a, b) => {  // the curly brace opens a multiline function
  let result = a + b;
  return result; // if we use curly braces, then we need an explicit "return"
};
```

