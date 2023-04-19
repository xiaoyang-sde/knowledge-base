# Haskell

## Primitive Type

In Haskell, variables are names for values. The `=` operator denotes definition rather than assignment.

- `Bool` represents boolean values, which can be either `True` or `False`.
- `Char` represents single characters in the Unicode character set.
- `Int` represents signed 64-bit integers.
- `Integer` represents arbitrary-precision integers.
- `Float` represents single-precision floating-point numbers.
- `Double` represents double-precision floating-point numbers.

```hs
-- Defines an Int
int = 150 :: Int

-- Defines a Bool
bool = False
```

## Arithmetic

- `+`: addition operator
- `-`: subtraction operator
- `*`: multiplication operator
- `/`: division operator (floating-point division)
- `div`: integer division operator, which performs integer division and rounds the result
- `mod`: modulus operator, which returns the remainder of integer division
- `^`: exponentiation operator, which raises one number to the power of another

Haskell doesn't do implicit conversion.

- `fromIntegral` converts an integral type (`Int` or `Integer`) to another numeric type
- `round`, `floor`, or `ceiling` convert a floating-point type (`Float` or `Double`) to an integral type

## Boolean Logic

- `&&`, `||`, and `not`: boolean logic operators
- `==`, `/=`, and `<=>`: comparison operators
- `if b then t else f`: `if`-expression

## Composite Type

- `Tuple` represents a sequence of values of different types. Each tuple is represented using the `(a, b)` notation, where `a` and `b` can be different types.
- `String` represents a sequence of characters. Each string is implemented as `[Char]`.
- `List` represents a sequence of values of the same type. Each list is represented using the `[type]` notation and implemented as a linked list.

```hs
primes :: [Int]
primes = [1, 2, 3, 5, 7, 11, 13]
```

Reference: [Documentation of Data.List](https://hackage.haskell.org/package/base-4.18.0.0/docs/Data-List.html)

### List Comprehension

List comprehension in Haskell is a combination of `map` and `filter`.

- Single input list: `[(f x) | x <- input_list, (guard x)]`, which is equivalent to `list(map(f, filter(guard, input_list)))`
- Multiple input lists: `[(f a b) | a <- input_list_1, b <- input_list_2, (guard_1 a), (guard_2 b)]`, which acts like a nested loop

```hs
output_list = [x^2 | x <- input_list, x > 5]

product = [a * b | a <- input_list_1, b <- input_list_2]
```

## Algebraic Data Type

The algebraic data type is a data type derived from existing data types with algebraic operations. In Haskell, an algebraic data type has one or more data constructors, and each data constructor can have zero or more arguments. For example, the value of `AlgDataType` can be constructed in one of four variants.

```hs
data AlgDataType
  = Constr1 Type11 Type12
  | Constr2 Type21
  | Constr3 Type31 Type32 Type33
  | Constr4
```

### Enumeration

The enumeration type is an algebraic data type that represents a set of distinct values, which represents as a choice between alternatives. The enumeration type supports pattern matching.

```hs
data Thing
  = Shoe
  | Ship
  | SealingWax
  | Cabbage
  | King
  deriving (Show)

isSmall :: Thing -> Bool
isSmall Shoe = True
isSmall Ship = False
```

### Recursive Data Type

The algebraic data type can be recursive. The `List` type is recursive, which is either empty or contains an element followed with a remaining list.

```hs
data IntList
  = Empty
  | Cons Int IntList

data Tree
  = Leaf
  | Node Tree Int Tree
  deriving (Show)

tree = Node Leaf 1 (Node Leaf 2 Leaf)
```

### Polymorphic Data Type

The polymorphic data type is defined with type variables, which can represent another types. For example, `Failable` or `List` is a type constructor, which takes a type and constructs another type.

```hs
data Failable a
  = Failure
  | OK a

data List a
  = Nil
  | Cons a (List a)

list = Cons 1 (Cons 2 (Cons 3 Nil)) :: List Int
```

## Function

The function is defined with the `fn_name arg_1 arg_2 ... = expression` syntax. The type of the function is `arg_1_type -> ... -> result_type`. Haskell functions are pure and deterministic.

```hs
add :: Int -> Int -> Int
add a b = a + b
```

### Partial Function Application

The partial function application is an operation that defines a function `g` with an existing function `f` that takes one or more arguments with default values for one or more of these arguments. `g` is a specialization of `f`, with hard-coded values for some of `f`'s parameters.

```hs
add_5 = (+) 5
add_5 100

cuber = map (\x -> x^3)
cuber [2,3,5]
```

### Local Binding

Local binding allows the definition of variables and functions that are accessible within a limited scope, such as within a specific function or block of code. The `let` clause defines one or more bindings to associate a name with an expression and the `in` clause contains an expression where the bindings are used. Each binding is evaluated when it's used and the result is cached.

```hs
is_nerd gpa =
  let
      nerd_score = 1 / (4.01 - gpa)
  in
      if nerd_score > 100 then True
      else False
```

### Pattern Matching

Pattern matching is a feature used to deconstruct data structures such as lists, tuples, and algebraic data types. Each clause is checked in order from top to bottom, and the first matching clause is chosen.

```hs
sumtorial :: Integer -> Integer
sumtorial 0 = 0
sumtorial n = n + sumtorial (n - 1)
```

The `length` function destructs a list into the first element `_` and the remaining list `remaining_list`, and compute the length of the list in a recursion.

```hs
length :: [Integer] -> Integer
length [] = 0
length (_:remaining_list) = 1 + length remaining_list
```

- `_` represents a wildcard pattern
- `name@pattern` assigns a name to the entire value being matched
- Literal values can be used as patterns

```hs
foo (Constr1 _ b)   = ...
foo p@(Constr2 a)   = show p
foo (Constr3 a b c) = ...
foo Constr4         = ...
```

The fundamental construct for doing pattern-matching in Haskell is the `case` expression. The expression `expression` is matched against each of the patterns and the first matched pattern is chosen.

```hs
case expression of
  pattern_1 -> expression_1
  pattern_2 -> expression_2
  ...
```

### Guard

Guard is used in conjunction with pattern matching to provide filtering or branching based on the input value. The first guard with a condition that evaluates to `True` is chosen. If none of the guards evaluate to `True`, matching continues with the next clause.

```hs
hailstone :: Integer -> Integer
hailstone n
  | n `mod` 2 == 0 = n `div` 2
  | otherwise      = 3 * n + 1

qsort :: [a] -> [a]
qsort lst
  | null lst = []
  | otherwise = less_eq ++ [pivot] ++ greater
  where
    pivot = head lst
    rest_lst = tail lst
    less_eq = qsort [a | a <- rest_lst, a <= pivot]
    greater = qsort [a | a <- rest_lst, a > pivot]
```

## Recursion Pattern

### Map

The `map` function is a higher-order function that applies a given function to each element of a list and returns a new list with the results.

```hs
map :: (a -> b) -> [a] -> [b]
map _ [] = []
map f, (element : remaining_list) = f element : map f remaining_list

list = [1, 2, 3, 4, 5]
square_list = map (\x -> x * x) list
```

### Filter

The `filter` function is a higher-order function that keeps elements of a list based on the return value of a function applied to each element.

```hs
filter :: (a -> Bool) -> [a] -> [a]
filter _ [] = []
filter f (element : remaining_list)
  | f element = element : filter f remaining_list
  | otherwise = filter f remaining_list

list = [1, 2, 3, 4, 5]
even_list = filter even list
```

### Fold

#### `foldl`

The `foldl` function applies a given function to the first element of the list and the initial accumulator value, then to the second element and the result of that, until all elements of the list have been processed.

```hs
foldl :: (b -> a -> b) -> b -> [a] -> b
foldl _ accumulate [] = accumulate
foldl f accumulate (element : remaining_list) = foldl f (f accumulate element) remaining_list

list = [1, 2, 3, 4, 5]
list_sum_1 = foldl (+) 0 list
list_sum_2 = foldl (\a b -> a + b) 0 list
```

#### `foldr`

The `foldr` function applies a given function to the last element of the list and the initial accumulator value, then to the second-to-last element and the result of that, until all elements of the list have been processed.

```hs
foldr :: (a -> b -> b) -> b -> [a] -> b
foldr _ accumulate [] = accumulate
foldr f accumulate (element : remaining_list) = f element (foldr f accumulate remaining_list)

list = [1, 2, 3, 4, 5]
list_sum_1 = foldr (+) 0 list
list_sum_2 = foldr (\a b -> a + b) 0 list
```

## Type Class

The type class correspond to a set of types which have certain operations defined for them, and the type class polymorphic function works for types which are instances of the type class constraint. For example, `Num`, `Eq`, `Ord`, and `Show` are type classes and `(+)`, `(<)`, and `show` are type class polymorphic function.

```hs
(+) :: Num a => a -> a -> a
(<) :: Ord a => a -> a -> Bool
show :: Show a => a -> String
```

For example, the `Eq` type class is defined such that its instance must define the `(==)` and `(/=)` functions with the indicated type signatures. The `instance [type class] [data type name]` syntax implements a type class for a data type.

```hs
class Eq a where
  (==) :: a -> a -> Bool
  (/=) :: a -> a -> Bool

data Foo = A Int | B Char

instance Eq Foo where
  (A i1) == (A i2) = i1 == i2
  (B c1) == (B c2) = c1 == c2
  _ == _ = False

  foo1 /= foo2 = not (foo1 == foo2)
```
