# The Processor

## Simple Implementation

The first section of the chapter covers an implementation that includes a subset of the core RISC-V instruction set. It includes `ld`, `sd`, `add`, `sub`, `and`, `or`, and `beq`. It illustrates the key principles used in creating a data path and designing the control.

- The processor sends the program counter to the memory that contains the code to fetch the instruction.
- The processor reads one or two registers based on the fields of the instruction.
- The processor uses the ALU to perform operations based on the type of the instruction.
  - The memory-reference instructions use the ALU for an address calculation, and then access the memory to read or write data.
  - The arithmetic-logical instructions use the ALU for the operation execution, and then write the data back into a register.
  - The conditional branches use the ALU for the equality test, and then change the next instruction address based on the comparison.

The control unit, which has the instruction as an input, determines how to set the control lines for the function units and the multiplexers.

- The function units selects from several behaviors based on its control lines. For example, the ALU must perform one of several operations.
- The multiplexer (data selector) selects from several inputs based on its control lines. For example, the value written into the PC might come from one of two addresses based on the conditional branches.
