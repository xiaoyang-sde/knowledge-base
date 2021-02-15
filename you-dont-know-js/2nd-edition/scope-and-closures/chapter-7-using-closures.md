# Chapter 7: Using Closures

Closure builds on this approach: for variables we need to use over time, instead of placing them in larger outer scopes, we can encapsulate them but still preserve access from inside functions. Functions remember these referenced scoped variables via closure.

## See the Closure

Closure is a behavior of functions and only functions. For closure to be observed, a function must be invoked in a different branch of the scope chain.

```javascript
function lookupStudent(studentID) {
    var students = [
        { id: 14, name: "Kyle" },
        { id: 73, name: "Suzy" },
        { id: 112, name: "Frank" },
        { id: 6, name: "Sarah" }
    ];

    return function greetStudent(greeting){
        var student = students.find(
            student => student.id == studentID
        );

        return `${ greeting }, ${ student.name }!`;
    };
}
```

While `greetStudent(..)` does receive a single argument as the parameter named `greeting`, it also makes reference to both `students` and `studentID`, identifiers which come from the enclosing scope of `lookupStudent(..)`. Each of those references from the inner function to the variable in an outer scope is called a **closure**.

Closure allows `greetStudent(..)` to continue to access those outer variables even after the outer scope is finished.

### Pointed Closure

The `student => student.id == studentID` arrow function is creating another scope bubble inside the `greetStudent(..)` function scope.

The consequence here is that this arrow function passed as a callback to the array's `find(..)` method has to hold the closure over `studentID`.

### Adding Up Closures

```javascript
function adder(num1) {
    return function addTo(num2){
        return num1 + num2;
    };
}

var add10To = adder(10);
var add42To = adder(42);

add10To(15);    // 25
add42To(9);     // 51
```

Closure is associated with an instance of a function, rather than its single lexical definition. Every time the outer `adder(..)` function runs, a new inner `addTo(..)` function instance is created, and for each new instance, a new closure.

### Live Link, Not a Snapshot

Closure is actually a live link, preserving access to the full variable itself. By closing over a variable in a function, we can keep using that variable as long as that function reference exists in the program.

Though the enclosing scope of a closure is typically from a function, there only needs to be an inner function present inside an outer scope.

```javascript
var hits;
{
    let count = 0;
    hits = function getCurrent(){
        count = count + 1;
        return count;
    };
}
hits();     // 1
hits();     // 2
hits();     // 3
```

Closures are variable-oriented rather than value-oriented. However, if we use `let` in the loop, it creates a new `i` for each iteration, so our closure works well.

```javascript
var keeps = [];

for (var i = 0; i < 3; i++) {
    keeps[i] = function keepI(){
        // closure over `i`
        return i;
    };
}

keeps[0]();   // 3 -- WHY!?
keeps[1]();   // 3
keeps[2]();   // 3
```

### Common Closures: Ajax and Events

Closure is most commonly encountered with callbacks. Why then is `studentID` still around and accessible to the callback? Closure.

```javascript
function lookupStudentRecord(studentID) {
    ajax(
        `https://some.api/student/${ studentID }`,
        function onRecord(record) {
            console.log(
                `${ record.name } (${ studentID })`
            );
        }
    );
}

lookupStudentRecord(114);
```

### What If I Can't See It?

If a closure exists but it cannot be observed in our programs, does it matter? No.

In fact, global scope variables essentially cannot be \(observably\) closed over, because they're always accessible from everywhere.

Variables that are merely present but never accessed don't result in closure. The JS engine doesn't need to keep `studentID` around after `lookupStudent(..)` has finished running, so the memory is cleaned up by GC.

```javascript
function lookupStudent(studentID) {
    return function nobody(){
        var msg = "Nobody's here yet.";
        console.log(msg);
    };
}

var student = lookupStudent(112);

student();
```

If there's no function invocation, closure can't be observed.

### Observable Definition

Closure is observed when a function uses variable\(s\) from outer scope\(s\) even while running in a scope where those variable\(s\) wouldn't be accessible.

* Must be a function involved.
* Must reference at least one variable from an outer scope.
* Must be invoked in a different branch of the scope chain from the variables.

## The Closure Lifecycle and Garbage Collection \(GC\)

Since closure is inherently tied to a function instance, its closure over a variable lasts as long as there is still a reference to that function.

Closure can unexpectedly prevent the GC of a variable that you're otherwise done with, which leads to run-away memory usage over time.

### Per Variable or Per Scope?

Conceptually, closure is per variable rather than per scope.

```javascript
function storeStudentInfo(id,name,grade) {
    return function getInfo(whichValue){
        var val = eval(whichValue);
        return val;
    };
}

var info = storeStudentInfo(73,"Suzy",87);

info("name"); // Suzy
info("grade"); // 87
```

Notice that the inner function `getInfo(..)` is not explicitly closed over any of `id`, `name`, or `grade` variables. However, calls to `info(..)` seem to still be able to access the variables.

Many modern JS engines do apply an optimization that removes any variables from a closure scope that aren't explicitly referenced. However, as we see with `eval(..)`, there are situations where such an optimization cannot be applied, and the closure scope continues to contain all its original variables.

In other words, closure must be per scope, implementation wise, and then an optional optimization trims down the scope to only what was closed over. \(per variable closure\)

It's an optional optimization in the first place, rather than a requirement of the specification, so it's safer to manually discard large values rather than relying on closure optimization or GC.

```javascript
function manageStudentGrades(studentRecords) {
    var grades = studentRecords.map(getGrade);
    studentRecords = null;
    return addGrade;
}
```

We're not removing `studentRecords` from the closure scope. We're ensuring that even if `studentRecords` remains in the closure scope, that variable is no longer referencing the potentially large array of data.

We should manage these closures carefully so we're only holding onto what's minimally needed and not wasting memory.

## An Alternative Perspective

This alternative model de-emphasizes "functions as first-class values," and instead embraces how functions \(like all non-primitive values\) are held by reference in JS, and assigned/passed by reference-copy.

We can envision that function instances actually just stay in place in their own scope environment, of course with their scope-chain intact. What gets sent to the outer scope is just a reference to the in-place function instance, rather than the function instance itself.

Closure instead describes the magic of keeping alive a function instance, along with its whole scope environment and chain, for as long as there's at least one reference to that function instance floating around in any other part of the program.

