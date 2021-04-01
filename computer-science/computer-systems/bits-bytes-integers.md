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

### Boolean Algebra

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

## Integers

### Representation: unsigned and signed

- Unsigned: $$ \sum^{w-1}_{i=0} x_{i} \cdot 2^{i} $$
- Two's Complement: $$ -x_{w-1} \cdot 2^{2-1} + \sum^{w-2}_{i=0} x_{i} \cdot 2^{i} $$ (The most significant bit indicates the sign.)

#### Numeric Ranges

- UMin = 0 (000...0)
- UMax = $$ 2^{w} - 1 $$ (111...1)
- TMin = $$ -2^{w - 1} $$ (100...0)
- TMax = $$ 2^{w - 1} - 1 $$ (011...1)

- | TMin | = TMax + 1
- UMax = 2 * TMax + 1

### Conversion (Signed and Unsigned)

- Bit pattern is maintained
- The interpretation is changed.

#### Constants

- Constants in C are considered to be signed integers by default.
- Unsigned integers have `U` as suffix. (Example: `114514U`)

#### Expression Evaluation

- If there's a mix of unsigned and signed integers in a single expression, singed values implicitly cast to unsigned.
- These expressions includes comparison operations.

### Expanding and truncating

#### Expading (e.g. short int to int)

- Unsigned: Add zeros to the front of the array of bits.
- Signed: Fill the front of the array of bits with the most significant bit.

#### Truncating (e.g. unsigned to unsigned short)

- Unsigned or signed: Bits are truncated
- Unsigned: mod operation
- Signed: similar to mod operation

### Arithmetic Operation

#### Addition

- Operands: w bits
- True Sum: w bits or w + 1 bits
- Discard Carry: w bits ($$ s = UAdd_{w} (u,v) = u + v mod 2^{w} $$)

Example:

- Operands: u = 1101, v = 0101
- True Sum: 1101 + 0101 = 10010 (18)
- Discard Carry: 0010 (18 mod 16 = 2)

Two's complement addition and unsigned addition have idential bit-level behavior.

- If true sum $$ >= 2^{w - 1} $$, the actual result becomes negative.
- If true sum $$ < -2^{w - 1} $$, the actual result becomes positive.

#### Multiplication

- Operands: w bits
- True Product: 2 * w bits
- Discard Carry: w bits ($$ s = UMult_{w} (u,v) = u \cdot v mod 2^{w} $$)

#### Power-of-2 Multiply with Shift

Most machines shift and add faster than multiply. The compiler might convert some multiplication operation to shift operation.

- Operands: w bits
- True Product: w + k bits ($$ u << k = u * 2^{k} $$)
- Discard k bits: w bits ($$ TMult_{w} (u, 2^{k}) = u * 2^{k} mod 2^{k} $$)

#### Power-of-2 Divide with Shift

- Operands: $$ u / 2 ^{k} $$
- True Division: $$ u / 2 ^{k} $$ ($$ u >> k = u / 2^{k} $$)
- Discard k bits: $$ \lfloor u / 2 ^{k} \rfloor $$

## Representations in Memory, Pointers, Strings

### Byte-Oriented Memory Organization

Programs refer to data by address. We could conceptually envision the memory as a very large array of bytes. An address is like an index to the array.

#### Machine Words

Any given computer has a "Word Size" - the normal size of integer-valued data. Most machines use 64-bit word size, or 18 exabytes of addressable memory.

#### Byte Ordering

- Big Endian: Least significant byte has highest address. (Internet, Sun, PowerPC)
- Little Endian: Least significant byte has lowest address. (x86, ARM, Windows)

### Representing Strings

Strings in C are represented by array of characters. Each character is encoded in ASCII format. The array of characters is null-terminated.
