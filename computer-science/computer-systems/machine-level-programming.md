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
