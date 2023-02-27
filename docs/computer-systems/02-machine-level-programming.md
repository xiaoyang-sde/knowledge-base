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
- Integer register file: 16 named locations storing 64-bit values (addresses or integer data)
- Condition codes: Status information about most recent arithmetic or logical operation (implement if and while statements)
- Vector register: Hold one or more integer or floating-point values
- Memory (Addressable byte array): The executable machine code for the program, information required by the OS, a run-time stack for managing procedure calls and returns, and blocks of memory allocated by user (`malloc`)

#### Turning C into Object Code

```
gcc -Og p1.c p2.c -o p
```

1. C program (p1.c, p2.c)
2. Compiler (`gcc -Og -S`) -> Asm program (p1.s, p2.s)
3. Assembler (`gcc` or `as`) -> Object program (p1.o, p2.o)
4. Linker (`gcc` or `ld`) -> Executable program (p)

### Assembly Basics: Registers, Operands, Move

x86-64 Integer Registers are 16 named locations storing 64-bit values: `%rax`, `%rbx`, `%rcx`, `%rdx`, `%rsi`, `%rdi`, `%rsp`, `%rbp`, `%r8`, `%r9`, `%r10`, `%r11`, `%r12`, `%r13`, `%r14`, `%r15`.

|8 bytes|4 bytes|2 bytes|1 byte|Function|
|-|:-:|:-:|:-:|-:|
|`%rax`|`%eax`|`%ax`|`%al`|Return value|
|`%rbx`|`%ebx`|`%bx`|`%bl`|Callee saved|
|`%rcx`|`%ecx`|`%cx`|`%cl`|4th argument|
|`%rdx`|`%edx`|`%dx`|`%dl`|3rd argument|
|`%rsi`|`%esi`|`%si`|`%sil`|2nd argument|
|`%rdi`|`%edi`|`%di`|`%dil`|1st argument|
|`%rbp`|`%ebp`|`%bp`|`%bpl`|Callee saved|
|`%rsp`|`%esp`|`%sp`|`%spl`|Stack pointer|
|`%r8`|`%r8d`|`%r8w`|`%r8b`|5th argument|
|`%r9`|`%r9d`|`%r9w`|`%r9b`|6th argument|
|`%r10`|`%r10d`|`%r10w`|`%r10b`|Caller saved|
|`%r11`|`%r11d`|`%r11w`|`%r11b`|Caller saved|
|`%r12`|`%r12d`|`%r12w`|`%r12b`|Callee saved|
|`%r13`|`%r13d`|`%r13w`|`%r13b`|Callee saved|
|`%r14`|`%r14d`|`%r14w`|`%r14b`|Callee saved|
|`%r15`|`%r15d`|`%r15w`|`%r15b`|Callee saved|

#### Data Formats

- byte: 1 byte (b)
- word: 2 bytes (w)
- double word: 4 bytes (l)
- quad word: 8 bytes (q)

#### Operand Specifiers

- **Immediate**: Constant integer data. Example: `$0x400`
- **Register**: One of 16 integer registers. Example: `%rax`
- **Memory**: 8 consecutive bytes of memory at address given by register. Example: `(%rax)`

Memory Addressing Modes:

- **Register**: `R` => `Reg[R]`
- **Normal**: `(R)` => `Mem[Reg[R]]`
- **Displacement**: `D(R)` => `Mem[Reg[R] + D]`
- **General Form**: `D(Rb, Ri, S)` => `Mem[Reg[Rb] + S * Reg[Ri] + D]`

Operand forms:

- D: Constant displacement
- Rb: Base register
- Ri: Index register (except for `%rsp`)
- S: Scale: 1, 2, 4, 8

Example: `0x8(%rdx, rcx, 4)` => `0xf000 + 4 * 0x100 + 0x8`

#### Data Movement Instructions

```s
mov Src, Dest
```

- `movb`: Move byte
- `movw`: Move word
- `movl`: Move double word (and fill the upper 4 bytes with zeros)
- `movq`: Move quad word
- `movabsq`: Move absolute quad word

`mov` could save immediate data into register or memory, move data from register to register or memory, and from memory to register.

`movabsq` can move an arbitrary 64-bit immediate value to a register. (`movq` only support 32-bit immediate value and extend it to 64-bit with sign extension)

```s
movz Src, Dest
```

Instructions in the `movz` class fill out the remaining bytes of the destination with zeros.

- `movzbw`: Move zero-extended byte to word
- `movzbl`: Move zero-extended byte to double word
- `movzwl`: Move zero-extended word to double word
- `movzbq`: Move zero-extended byte to quad word
- `movzwq`: Move zero-extended word to quad word

```s
movs Src, Dests
```

- `movsbw`: Move sign-extended byte to word
- `movsbl`: Move sign-extended byte to double word
- `movswl`: Move sign-extended word to double word
- `movsbq`: Move sign-extended byte to quad word
- `movswq`: Move sign-extended word to quad word
- `movslq`: Move sign-extended double word to quad word

Instructions in the `movs` class fill out the remaining bytes of the destination with sign extension.

#### Pushing and Popping Stack Data

```s
pushq Src
popq Dest
```

Pushing a quad word value onto the stack involves decrementing the stack pointer (`%rsp`) by 8 and writing the value at the new top-of-stack address.

Popping a quad word value involves reading from the top-of-stack location and incrementing the stack pointer (`%rsp`) by 8.

### Arithmetic and Logical Operations

#### Address Computation Instruction

```s
leaq Src, Dst
```

- Computing addresses without a memory reference
- Computing arithmetic expressions of the form `x + k * y`

```s
# t = x + 2 * x + 4 = 3 * x + 4
leaq 4(%rdi, %rdi, 2), %rax
```

#### Shift Operations

- `sal Src, Dest`: Dest = Dest << Src
- `shl Src, Dest`: Dest = Dest << Src (Same as `sal`)
- `sar Src, Dest`: Dest = Dest >> Src (Arithmetic)
- `shr Src, Dest`: Dest = Dest >> Src (Logical)

#### Unary Instructions

- `inc`: Dest = Dest + 1
- `dec`: Dest = Dest - 1
- `neg`: Dest = -Dest
- `not`: Dest = ~Dest

#### Binary Operations

- `add Src, Dest`: Dest = Dest + Src
- `sub Src, Dest`: Dest = Dest - Src
- `imul Src, Dest`: Dest = Dest * Src

- `xor Src, Dest`: Dest = Dest ^ Src
- `and Src, Dest`: Dest = Dest & Src
- `or Src, Dest`: Dest = Dest | Src

## Control

- Conditional control transfer: jump to specific branch based on the condition
- Conditional data transfer: compute the result of both conditional branches, and save the result based on the condition

### Condition Codes

CPU maintains a set of single-bit condition code registers describing the attributes of the most recent arithmetic or logical operation. `leaq` instruction doesn't alter any condition codes.

- CF: Carry flag, if the most recent operation caused an unsigned overflow
- OF: Overflow flag, if the most recent operation caused a two's-complement overflow
- SF: Sign flag, if the most recent operation is negative
- ZF: Zero flag, if the most recent operation is zero

For the logical operations (XOR, etc.), CF and OF are 0. For the shift operations, CF is the last bit shifted out, and OF is 0.

#### Explicitly Set (cmp instructions)

```s
cmp Src1, Src2
```

`cmp` (`cmpb`, `cmpw`, `cmpl`, `cmpq`) computes `Src2 - Src1` and changes the condition codes without setting destination.

- CF: If unsigned overflow
- OF: If signed overflow
- ZF: If `Src2 == Src1`
- SF: If `(Src2 - Src1) < 0`

```s
test Src1, Src2
```

`test` (`testb`, `testw`, `testl`, `testq`) computes `Src2 & Src1` and changes the condition codes without setting destination.

- CF: 0
- OF: 0
- ZF: If `Src2 & Src1 == 0`
- SF: If `Src2 & Src1 < 0`

#### Accessing the Condition Codes

`set` instructions set lower-order byte of destination to 0 or 1 based on combinations of condition codes.

- `sete`: Equal
- `setne`: Not equal
- `sets`: Negative
- `setns`: Non-negative

- `setg`: Greater (Signed)
- `setge`: Greater or equal (Signed)
- `setl`: Less (Signed)
- `setle`: Less or equal (Signed)

- `seta`: Above (Unsigned)
- `setae`: Above or equal (Unsigned)
- `setb`: Below (Unsigned)
- `setbe`: Below or equal (Unsigned)

```s
comp:
  cmpq %rsi, %rdi # Compare x, y
  setg %al # Set the low-order byte of %eax to 1 if x > y
  movzbl %al, %eax # Clear the rest of %eax (and rest of %rax)
  ret
```

### Conditional Control Transfer

#### Jump Instructions

`jump` instructions jump to different part of code depending on conditional codes. The target for `jmp` could be a label or a value in register or memory. (`jmp *%rax`)

- `jmp`: Unconditional
- `je`: Equal
- `jne`: Not equal
- `js`: Negative
- `jns`: Non-negative

- `jg`: Greater (Signed)
- `jge`: Greater or equal (Signed)
- `jl`: Less (Signed)
- `jle`: Less or equal (Signed)

- `ja`: Above (Unsigned)
- `jae`: Above or equal (Unsigned)
- `jb`: Below (Unsigned)
- `jbe`: Below or equal (Unsigned)

#### Jump Instruction Encodings

- **PC relative**: Encode the difference between the address of the target instruction and the address of the instruction immediately following the jump.
- **Absolute address**: Using 4 bytes to directly specify the target.

### Conditional Data Tranfser

Conditional data tranfser moves compute the result of both conditional branches, and save the result based on the condition. GCC tries to use conditional moves since branches are very disruptive to instruction flow through pipelines. The branches must be side-effect free and only contain simple computations.

#### Conditional Move Instructions

The source and destination values of `cmov` can be 16, 32, or 64 bits long. Unlike the unconditional instructions, the assembler can infer the operand length of a conditional move instruction from the name of the destination register.

```s
cmov Src, Dest
```

- `cmove`: Equal
- `cmovne`: Not equal
- `cmovs`: Negative
- `cmovns`: Non-negative

- `cmovg`: Greater (Signed)
- `cmovge`: Greater or equal (Signed)
- `cmovl`: Less (Signed)
- `cmovle`: Less or equal (Signed)

- `cmova`: Above (Unsigned)
- `cmovae`: Above or equal (Unsigned)
- `cmovb`: Below (Unsigned)
- `cmovbe`: Below or equal (Unsigned)

```cpp
long cread(long *xp) {
  // may raise null pointer dereferencing error
  return (xp ? *xp :0);
}
```

If one of the two conditional expressions could possibly generate an error condition, side effect, or require significant computation, the compiler will switch back to conditionial control transfer.

### Loops

#### Do-While Loop

The general form of do-while loop could be translated into conditional and `goto` statements as follows:

```cpp
loop:
  Body
  if (Test)
    goto loop
```

#### While Loop

The general form of while loop could be translated into conditional and `goto` statements as these ways:

```cpp
goto test;

loop:
  Body

test:
  if (Test):
    goto loop;
...
```

```cpp
if (!Test):
  goto done;

loop:
  Body
  if (Test):
    goto loop;

done:
  ...
```

#### For Loop

The general form of while loop could be translated into while loop as follows:

```c
for (init; test; update)

init
while (test)
  Body
  update
```

```c
init
goto test
loop:
  body-statement
  update
test:
  if (Test):
    goto loop
```

### Switch Statements

#### Jump Table

Jump table is an array where entry `i` is the address of a code segment implementing the action the program should take when the switch idnex equals `i`.

```s
.section .rodata # read-only data
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

The compiler selects the method of trasnlating a switch statement based on the number of cases and the sparsity of the case values. Jump tables are used when there are a number of cases and they span a small range of values. Otherwise, the compiler will use a sequence of if-else statements.

#### Assembly Code

- Direct Jump: Jump directly to the target. (`jmp .L8`)
- Indirect Jump: Fetch target from effective address. (`jmp *.L4(, %rdi, 8)`)

```s
switch_eg:
  movq %rdx, %rcx
  cmpq $6, %rdi # Compare x and 6 (Suppose the max case is 6)
  ja .L8 # Jump to the default if x is greater than 6 or less than 0
  jmp *.L4(, %rdi, 8) # goto *jump_table[x]

  .L7:
    imulq %rdi, %rdi
    jmp .L2 # goto done

  .L5:
    addq $11, %rdi
    # fall through
  .L6:
    addq $11, %rdi,
    jmp .L2 # goto done

  .L8 # default
    addq $1, %rdi

  .L2 # done
    movq %rdi, (%rdx)
    ret
```

## Procedures

### The Run-Time Stack

- The region of memory managed with stack discipline
- Grows towards lower addresses
- The `%rsp` register contains lowest stack address

**Stack frame**: When an x86-64 procedure requires storage beyond what it can hold in registers, it alloates a specific space on the stack.

### Control Transfer

```s
call Label # direct

call *Operand # indirect
```

- Procedure control flow: Use stack to support procedure call and return
- Procedure call: `call label`, push return address on stack and then jump to `label`
- Return address: Save the address of next instruction right after `call` to the stack
- Procedure return: `ret`: Pop address from stack and then jump to it

### Data Transfer

- First 6 arguments: `%rdi`, `%rsi`, `%rdx`, `%rcx`, `%r8`, `%r9`
- Other arguments: The stack (the argument 7 locates at the top of the stack)
- Return value: `%rax`

### Local Storage on the Stack

Stack allocates the state for single procedure instantiation in frames.

- Contents: Return information, local storage, temporary space
- Management: Space allocated when enter procedure (`call`), and deallocated when return (`ret`)
- Current stack frame: `%rsp` (Stack top) -> Parameters for function about to call, local variables, saved register context, old frame pointer (optional)
- Caller stack frame: Return address pushed by `call` instruction, argument 7, ..., argument n

#### Local Storage in Registers

Contents in register may be overwritten by other functions. The function could save the content of registers on stack (saved register context), alter them, and retrieve them back from stack before returning.

- Callee saved: Callee must preserve the values of these registers, ensuring that they have the same values before and after the function is executed. (`%rbx`, `%r12`, `%r13`, `%r14`, `%rbp`)
- Caller saved: All other registers except for `%rsp` could be modified by any function. The caller should save the data before making the call when necessary. (`%r10`, `%r11`)

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
- Identifier `A` is the pointer to the first element
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

- Aligned data: Primitive data type requires K bytes, and address must be multiple of K (1 for `char`, 2 for `short`, 4 for `int` and `float`, 8 for `long`, `double`, and pointers)
- Motivation: Memory accessed by aligned chunks of 4 or 8 bytes, so it's inefficient to load or store datu mthat spans quad word bondaries
- Compiler: Insert gaps in structure to ensure correct alignment of fields
- Optimization: Put large data types first to save space

### Unions

Unions allow a single object to be referenced according to multiple types. The size of a union equals the maximum size of any of its fields. In this code below, the pointer `U3` references the start of the union.

```cpp
union U3 {
  char c;
  int i[2];
  double v;
}
```

## Buffer Overflow

C doesn't perform any bounds checking for array references, and local variables, saved registers, and return addresses are stored on the stack. The state stored on the stack could be corrupted by a write to an out-of-bounds array element that will lead to serious errors.

Buffer overflow is a common source of state corruption. Some character array is allocated on the stack to hold a string, but the size of the string exceeds the space allocated for the array.

```cpp
void echo() {
  char buf[8];
  gets(buf);
}
```

The code above reads the standard input into a character array. If the input is longer than 7, `gets` will overwrite some of the information stored on the stack. If the return address is overrided, the program will jump to a totally unexpected location.

- 0-7: Safe
- 9-23: Unused stack space
- 24-31: Return address
- 32+: Saved state in caller

### x86-64 Linux Memory Layout

- Stack: Runtime stack (e.g. local variables)
- Heap: Dynamically allocated memories (e.g. `malloc()`)
- Data: Statically allocated data (e.g. `static` vars, string constants)
- Text and Shared Libraries: Read-only executable machine instructions

#### Code Injection Attacks

A pernicious use of buffer overflow is to get a program perform a function that it would otherwise be unwilling to do. The program is fed with a string that contains the byte encoding of some executable code, called exploit code, and some extra bytes that overwrite the return address with a pointer to the exploit code.

#### Return-Oriented Programming Attacks

- Challenge: Stack randomization makes it hard to predict buffer location, and marking stack nonexecutable makes it hard to insert binary code.
- Alternative Strategy: String together fragments (gadgets) to achieve overall desired outcome.

### Thwarting Buffer Overflow Attacks

#### Stack Randomization

To insert exploit code into a system, the attacker needs to inject both the code and a pointer to the code as part of the attack string. Historically, the stack addresses for a program were highly predictable.

The idea of stack randomization is to make the position of the stack vary from one run of a program to another. This is implemented by allocating a random amount of space between 0 and n bytes on the stack at the start of a program. The range is large enough to get sufficient variations, and small enough that it doesn't waste too much space.

However, if the attacker includes a long sequence of `nop` instructions before the actual exploit code, they could guess an address somewhere within this sequence and let the program hit the exploit code.

#### Stack Corruption Detection

The program can attempt to detect when such an overwrite has occurred before it can have any harmful effects. The idea of stack protector is to store a special guard (canary) value in the stack frame between any local buffer and the rest of the stack state. The guard value is generated randomly each time the program runs. Before restoring the register state and returning from the function, the program aborts with an error if the guard has been altered.

#### Limiting Executable Code Regions

To eliminate the ability of an attacker to insert executable code into a system, one method is to limit which memory regions hold executable code. Typically only the portion of memory holding the code generated by the compiler need be executable, and the other portions can be restricted to allow just reading and writing. The stack can be marked as being readable and writable, but not executable.
