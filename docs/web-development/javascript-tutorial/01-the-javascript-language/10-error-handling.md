# Chapter 10: Error handling

## 10.1 Error handling, "try..catch"

### The “try…catch” syntax

```javascript
try {

  // code...

} catch (err) {

  // error handling

}
```

`try..catch` only works for runtime errors, not syntax errors.

`try..catch` works synchronously, which means that it cannot catch the errors in the scheduled function such as `setTimeout`, unless the `try..catch` is inside that function.

### Error object

* `name`: Example: `ReferenceError`
* `message`: Textual message about error details.
* `stack`: A string with information about the sequence of nested calls that led to the error.

### Using “try…catch”

If we don’t need error details, `catch` may omit it.

#### Throwing our own errors

JavaScript has many built-in constructors for standard errors: `Error`, `SyntaxError`, `ReferenceError`, `TypeError` and others.

```javascript
throw <error object>

throw new SyntaxError("Incomplete data: no name");
```

#### Rethrowing

Catch should only process errors that it knows and “rethrow” all others.

```javascript
if (e instanceof SyntaxError) {
    alert( "JSON Error: " + e.message );
} else {
    throw e; // rethrow (*)
}
```

### try…catch…finally

If `finally` exists, it runs in all cases:

* After `try`, if there were no errors.
* After `catch`, if there were errors.

The `finally` clause is often used when we start doing something and want to finalize it in any case of outcome.

Variables are local inside \`try..catch..finally\`.

Before the `try` returns the code, `finally` is executed just before it.

The `try..finally` construct, without `catch` clause, is also useful.

### Global catch

```javascript
window.onerror = function(message, url, line, col, error) {
  // ...
};
```

* `message`: Error message.
* `url`: URL of the script where error happened.
* `line`, `col`: Line and column numbers where error happened.
* `error`: Error object.

## 10.2 Custom errors, extending Error

### Extending Error

```javascript
class ValidationError extends Error {
  constructor(message) {
    super(message); // (1)
    this.name = "ValidationError"; // (2)
  }
}
```

### Further inheritance

```javascript
class PropertyRequiredError extends ValidationError {
  constructor(property) {
    super("No property: " + property);
    this.name = "PropertyRequiredError";
    this.property = property;
  }
}
```

