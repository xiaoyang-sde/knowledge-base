# Bits, Bytes, and Integers

## Representing Informantion as Bits

Each bit is 0 or 1. By encoding or interpreting sets of bits in various ways, computers could represent and manipulate numbers, sets, strings, etc.

Why bits? It's easy to store and could be transmitted reliably.

### Data Representations in C (x86-64)

Each byte is 8 bits.

- `char`: 1 byte
- `short`: 2 bytes
- `int`: 4 bytes
- `long`: 8 bytes
- `float`: 4 bytes
- `double`: 8 bytes
- `long double`: 10 or 16 bytes
- `pointer`: 8 bytes

## Bit-level manipulations

### Boolean	Algebra

Encode `true` as 1 and `false` as 0.

- AND: A & B = 1 when both A = 1 and B = 1
- OR: A | B = 1 when either A = 1 or B = 1
- NOT: ~A = 1 when A = 0
- XOR: A ^ B = 1 when either A = 1 or B = 1, but not both

### Bitwise Operations

We could apply properties of boolean algebra on bit vectors. Example: 01101001 & 01010101 = 01000001

Operations `&`, `|`, `~`, `^` in C applies to any integral data type: `long`, `int`, `short`, `char`, `unsigned`. They are different from logical operators (`!`, `||`, `&&`).

### Shift Operations

- Left Shift (`x << y`): Shift bit-vector x left y positions and fill with 0's on the right.
- Right Shift (`x >> y`): Shift bit-vector x right y positions.
- - Logical: Fill with 0's on the left.
- - Arithmetic: Replicate the most significant bit on the left.
