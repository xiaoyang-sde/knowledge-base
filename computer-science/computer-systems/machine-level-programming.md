# Machine Level Programming

## Basics

### History of Intel Processors and Architectures

#### Intel x86 Processors

- Dominate laptop, desktop, and server market.
- Backwards compatible up until 8086, introduced in 1978.
- CISC: Complex instruction set computer
- Evolution: 8086, 386, Pentium 4E, Core 2, Core i7, Core i9

#### x86 Clones: Advanced Micro Devices

- Historically, AMD has followed just behind Intel
- Then, it developed x86-64, their own extension to 64 bits and built Opteron, the tough competitor to Pentium 4.
- Recently, AMD has fallen behind and relies on external semiconductor manufacturer.

#### Intel's 64-bit History

- 2001: Intel attempts radical shift from IA32 to IA64 (Itanium)
- 2003: AMD stpes in with evolutionary solution, x86-64.
- 2004: Intel announces EM64T extension to IA32, which is identical to x86-64.

### C, Assembly, Machine Code

- **Architecture** (ISA): The parts of a processor design that one needs to understand or write assembly or machine code.
- **Microarchitecture**: Implementation of the architecture.
- **Machine Code**: The byte-level programs that a processor executes.
- **Assembly Code**: The text representation of machine code.

#### Assembly Code View

- Program Counter: The address in memory of the next instruction to be executed
- Register file: 16 named locations storing 64-bit values
- Condition codes: Status information about most recent arithmetic or logical operation
- Memory: Store code and user data (Byte addressable array)

#### Turning C into Object Code

```
gcc -Og p1.c p2.c -o p
```

1. C program (p1.c p2.c)
2. Compiler (`gcc -Og -S`) -> Asm program (p1.s p2.s)
3. Assembler (`gcc` or `as`) -> Object program (p1.o p2.o)
4. Linker (`gcc` or `ld`) -> Executable program (p)

### Assembly Basics: Registers, Operands, Move

x86-64 Integer Registers are 16 named locations storing 64-bit values: `%rax`, `%rbx`, `%rcx`, `%rdx`, `%rsi`, `%rdi`, `%rsp`, `%rbp`, `%r8`, `%r9`, `%r10`, `%r11`, `%r12`, `%r13`, `%r14`, `%r15`.

#### Moving Data

```s
movq Source, Dest
```

- **Immediate**: Constant integer data. Example: `$0x400`
- **Register**: One of 16 integer registers. Example: `%rax`
- **Memory**: 8 consecutive bytes of memory at address given by register. Example: `(%rax)`

`movq` could save immediate data into register or memory, move data from register to register or memory, and from memory to register.

#### Simple Memory Addressing Modes

**Normal**: `(R)` => `Mem[Reg[R]]`

**Displacement**: `D(R)` => `Mem[Reg[R] + D]`

**General Form**: `D(Rb, Ri, S)` => `Mem[Reg[Rb] + S * Reg[Ri] + D]`

- D: Constant displacement
- Rb: Base register
- Ri: Index register (except for `%rsp`)
- S: Scale: 1, 2, 4, 8

Example: `0x8(%rdx, rcx, 4)` => `0xf000 + 4 * 0x100 + 0x8`

### Arithmetic and Logical Operations

#### Address Computation Instruction

```s
leaq Src, Dst
```

- Computing addresses without a memory reference
- Computing arithmetic expressions of the form `x + k * y`

```s
leaq 4(%rdi, %rdi, 2), %rax # t <- x + 2 * x + 4
```

#### Arithmetic Operations

- `addq Src, Dest`: Dest = Dest + Src
- `subq Src, Dest`: Dest = Dest - Src
- `imulq Src, Dest`: Dest = Dest * Src
- `salq Src, Dest`: Dest = Dest << Src
- `sarq Src, Dest`: Dest = Dest >> Src (Arithmetic)
- `shrq Src, Dest`: Dest = Dest >> Src (Logical)
- `xorq Src, Dest`: Dest = Dest ^ Src
- `andq Src, Dest`: Dest = Dest & Src
- `orq Src, Dest`: Dest = Dest | Src

#### One Operand Instructions

- `incq`: Dest = Dest + 1
- `decq`: Dest = Dest - 1
- `negq`: Dest = -Dest
- `notq`: Dest = ~Dest

## Control

- Temporary data (`%rax`, ...)
- Location of runtime stack (`%rsp`)
- Location of current code control point (`%rip`, ...)
- Status of recent tests (CF, ZF, SF, OF)

### Condition Codes

- CF: Carry Flag (for unsigned)
- SF: Sign Flag (for signed)
- ZF: Zero Flag
- OF: Overflow Flag (for signed)

#### Implicitly Set

- CF: If unsigned overflow
- ZF: If `t == 0`
- SF: If `t < 0`
- OF: If signed overflow

#### Explicitly Set

```s
cmpq Src2, Src1
```

`cmpq` computes `a - b` and changes the condition codes without setting destination.

- CF: If unsigned overflow
- ZF: If `a == b`
- SF: If `(a - b) < 0`
- OF: If signed overflow

```as
testq Src2, Src1
```

`testq` computes `a & b` and changes the condition codes without setting destination.

- ZF: If `a & b == 0`
- SF: If `a & b < 0`

#### Reading Condition Codes

SetX instructions set lower-order byte of destination to 0 or 1 based on combinations of condition codes.

- `sete`: Equal
- `setne`: Not equal
- `setg`: Greater (Signed)
...

```s
cmpq %rsi, %rdi # Compare x, y
setg %al # Set the lowest bit of %al to 1 if x > y
movzbl %al, %eax
ret
```

### Conditional Branches

#### Jumping

JX instructions jump to different part of code depending on conditional codes.

- `jmp`: Unconditional
- `je`: Equal
- `jne`: Not equal
- `js`: Negative
- `jns`: Non-negative
...

#### Conditional Moves

Conditional moves compute the result of both conditional branches, and save the result based on the condition. GCC tries to use conditional moves since branches are very disruptive to instruction flow through pipelines. The branches must be side-effect free and only contain simple computations.

### Loops

#### Do-While Loop

```c
loop:
  Body
  if (Test)
    goto loop
```

#### While Loop

```c
if (!Test):
  goto done

loop:
  Body
  if (Test)
    goto loop

done:
  ...
```

#### For Loop

```c
for (init; test; update)

init
while (test)
  Body
  update
```

### Switch Statements

#### Jump Table Structure

##### Switch Form

```c
switch (x) {
  case 0:
    Block 0
  case 1:
    Block 1
  case 2:
    Block 2
  ...
  case 6:
    Block 6
}
```

##### Jump Table

```s
.section .rodata
  .align 8

.L4:
  .quad .L8 # x = 0 (goto: default)
  .quad .L3 # x = 1
  .quad .L5 # x = 2
  .quad .L9 # x = 3
  .quad .L8 # x = 4 (goto: default)
  .quad .L7 # x = 5
  .quad .L7 # x = 6
```

#### Assembly Code for Switch Statement

- Direct Jump: Jump directely to the target. (`jmp .L8`)
- Indirect Jump: Fetch target from effective address. (`jmp *.L4(, %rdi, 8)`)

```s
switch_eg:
  movq %rdx, %rcx
  cmpq $6, %rdi # Compare x and 6 (Suppose the max case is 6)
  ja .L8 # Jump to the default if x is greater than 6 or less than 0
  jmp *.L4(, %rdi, 8) # goto *JTab[x]
```

## Procedures

### Stack Structure

#### x86-64 Stack

- The region of memory managed with stack discipline
- Grows towards lower addresses
- The `%rsp` register contains lowest stack address

#### Stack Push

```s
pushq src
```

Fetch operand at `src`, decrement `%rsp` by 8, and then write operand at addreses given by `%rsp`.

#### Stack Pop

```s
popq dest
```

Read value at address given by `%rsp`, increment `%rsp` by 8, and store the value at `dest` (register).

### Calling Conventions

#### Passing Control

- Procedure control flow: Use stack to support procedure call and return.
- Procedure call: `call label`, push return address on stack and then jump to `label`.
- Return address: Save the address of next instruction right after `call` to the stack.
- Procedure return: `ret`: Pop address from stack and then jump to it.

#### Passing Data

- First 6 arguments: `%rdi`, `%rsi`, `%rdx`, `%rcx`, `%r8`, `%r9`
- Other arguments: The stack
- Return value: `%rax`

#### Managing Local Data

Stack allocates the state for single procedure instantiation in frames.

- Contents: Return information, local storage, temporary space
- Management: Space allocated when enter procedure (`call`), and deallocated when return (`ret`)
- Current stack frame: Parameters for function about to call, local variables, saved register context, old frame pointer (optional)
- Caller stack frame: Return address pushed by `call` instruction, arguments for this cal

##### Register Saving Conventions

Contents in register may be overwritten by other functions.
- Caller saved: Caller saves temporary values in its frame before the call. (`%r10`, `%r11`)
- Callee saved: Callee saves temporary values in its frame before using and restores them before returning. (`%rbx`, `%r12`, `%r13`, `%r14`, `%rbp`)

### Illustration of Recursion

Recursions are handled without special consideration. Stack frames mean that each function call has private storage to save registers and local variables. Register saving conventions prevent one function call from corrupting another's data.

## Data

### Arrays

#### Allocation

```c
T A[L];
```

- Array of data type `T` and length `L`
- Contigunously allocated region of `L * sizeof(T)` bytes in memory
- Identifier `A` is also the pointer to the first element
- `A[1]` is equivalent to `*(A + 1)`

#### Access

```c
int get_digit (int[] z, int digit) {
  return z[digit];
}
```

```s
# %rdi = z
# %rsi = digit
movl (%rdi, %rsi, 4) %eax # z[digit]
```

### Nested Arrays

#### Allocation

```c
T A[R][C];
```

- 2D array of data type T with R rows and C columns

#### Access

- Row vectors: `A[i]` is an array of C elements, and each element of type T requires K bytes. The starting address is `A + i * (C * K)`.
- Array elements: `A[i][j]` is an element of type T, which requires K bytes. The address is `A + i * (C * K) + j * K = A + (i * C + j) * K`.

### Structures

#### Allocation

```c
struct rec {
  int a[4];
  size_t i;
  struct rec *next;
};
```

- Structure represented as block of memory to hold all of the fields
- Fields ordered according to declaration
- Compiler determines overall size and positions of fields

#### Alignment

- Aligned data: Primitive data type requires K bytes, and address must be multiple of K.
- Motivation: Memory accessed by aligned chunks of 4 or 8 bytes, so it's inefficient to load or store datu mthat spans quad word bondaries.
- Compiler: Insert gaps in structure to ensure correct alignment of fields.
- Optimization: Put large data types first to save space.
