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

```as
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

```as
leaq Src, Dst
```

- Computing addresses without a memory reference
- Computing arithmetic expressions of the form `x + k * y`

```
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
