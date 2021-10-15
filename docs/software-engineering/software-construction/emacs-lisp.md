# Emacs Lisp

## List Processing

The list in Lisp is preceded by a single apostrophe, e.g. `'(rose violet buttercup)`. Both data and program in Lisp are lists of symbols (e.g. `+`, `foo`), numbers, or nested lists, separated by whitespace and surrounded by parentheses.

The list in Lisp is a program to be evaluated. The single quote lets Lisp treat the list as a literal. If the quote is missing, the first item of the list is treated as a function and the other items are interpreted as the arguments.

```lisp
(+ 2 2)
```

If evaluation applies to a list that is inside another list, the outer list could use the value returned by the first evaluation as information when the outer list is evaluated.

```lisp
(+ 2 (+ 3 3))
```

The symbol could have a value or a function definition attached to it. The value of a symbol could be any expression, such as a symbol, number, list, or string. The `set` function sets the value of a symbol. The `setq` function sets the value of a symbol and treat the first argument as a quoted value.

```lisp
(set 'flowers '(rose violet daisy buttercup))

(setq flowers '(rose violet daisy buttercup))
```

## Function Definitions

The `defun` macro creates a function definition and attaches it to a symbol.

- The name of the symbol to be attached
- The list of arguments that will be passed to the function
- Documentation of the function
- The expression used in `M-x`
- The body of the function definition

```lisp
(
  defun
  multiply-by-seven
  (number)
  "Multiply NUMBER by seven."
  (* 7 number)
)
```

The `let` expression attaches a symbol to a value that the Lisp interpreter will not confuse the variable with a variable of the same name that is not part of the function.

```lisp
(
  let
  (
    (zebra "stripes")
    (tiger "fierce")
  )
  (
    message
    "One kind of animal has %s and another is %s."
    zebra
    tiger
  )
)
```

The `if` expression instructs the interpreter to make decisions. It has the if-part, then-part, and the else-part. In Emacs Lisp, "false" is "nil", and anything else is "true".

```lisp
(
  if
  (> 5 4)
  (message "5 is greater than 4")
  (message "5 is not greater than 4")
)
```
