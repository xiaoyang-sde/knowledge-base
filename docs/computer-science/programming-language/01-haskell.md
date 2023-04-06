# Haskell

## Function

In Haskell, functions are deterministic and side-effect free. All functions must take one or more arguments and return a value.

The order of execution of expressions is not predictable which is determined by the lazy evaluation strategy. Lazy evaluation means that expressions are evaluated when they are needed to produce a result.

```hs
square x = x * x
hypot a b = sqrt (square a + square b)
```

## Data Type

Haskell is a statically typed programming language, which means that types are determined at compile time, rather than at runtime. In Haskell, all values and expressions have a well-defined type that is known at compile time. The compiler is able to deduce the type.

### Primitive Type

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

### Composite Type

- `Tuple` represents a sequence of values of different types. Each tuple is represented using the `(a, b)` notation, where `a` and `b` can be different types.
- `List` represents a sequence of values of the same type. Each list is represented using the `[type]` notation and implemented as a linked list.
- `String` represents a sequence of characters. Each string is implemented as `[Char]`.

### List Operation

```hs
primes :: [Int]
primes = [1, 2, 3, 5, 7, 11, 13]

length primes -- len(primes)
head primes -- primes[0]
primes !! 2 -- primes[2]
elem 11 primes -- 11 in primes

tail primes -- primes[1:]
null primes -- len(primes) == 0
take 3 primes -- primes[:3]
drop 4 primes -- primes[4:]

sum primes -- reduce(lambda state, item: state + item, primes)
or [True, False, False] -- reduce(lambda state, item: state or item, primes)
zip primes primes - zip(primes, primes)

primes ++ [17, 19] -- primes + [17, 19]
23 : primes -- primes.insert(0, 23)

one_to_ten = [1..10]
oddities = [1, 3..10]
infinite_list = [42..]
tricycle = cyclel [1, 3, 5]
```

### List Comprehension

List comprehension in Haskell is a combination of `map` and `filter`.

- Single input list: `[(f x) | x <- input_list, (guard x)]`, which is equivalent to `list(map(f, filter(guard, input_list)))`
- Multiple input lists: `[(f a b) | a <- input_list_1, b <- input_list_2, (guard_1 a), (guard_2 b)]`, which acts like a nested loop

```hs
output_list = [x^2 | x <- input_list, x > 5]

product = [a * b | a <- input_list_1, b <- input_list_2]
```

## Arithmetic

- `+`: addition operator
- `-`: subtraction operator
- `*`: multiplication operator
- `/`: division operator (floating-point division)
- `div`: integer division operator, which performs integer division and rounds the result
- `mod`: modulus operator, which returns the remainder of integer division
- `^`: exponentiation operator, which raises one number to the power of another

All of these operators can be used with the `Int` and `Integer` types, as well as the `Float` and `Double` types for floating-point arithmetic.
