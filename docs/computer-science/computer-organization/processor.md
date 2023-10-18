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

## Logic Design Convention

The datapath is a fundamental component responsible for performing arithmetic and logic operations on data. The datapath elements in the RISC-V implementation consist of elements that operate on data values (combinational, such as the ALU) and elements that contain state (such as the register file).

Each state element has at least 2 inputs and 1 output. The required inputs are the data value and the clock. The output from a state element provides the value that was written in an earlier clock cycle. Logic components that contain state are called sequential.

The clock defines when signals could be read or written. It prevents data races, such as when a signal is written at the same time that it is read. The edge-triggered clocking is a clocking scheme in which all state changes occur on a clock edge. The inputs are values that were written in a previous clock cycle, while the outputs are values that can be used in a following clock cycle.

The control signal is used for multiplexer selection or for directing the operation of a functional unit. If a state element is not updated on each clock, then an explicit write control signal is required.

Each signal is asserted when its logical state is set to true and is deasserted when it's set to false or unknown. Some signals are true with low voltage.

The edge-triggered clock allows a register to be read, send the value through some combinational logic, and write in the same clock cycle. The read will get the value written in an earlier clock cycle, while the value written will be available to a read in a subsequent clock cycle.

## Datapath

Each datapath element is a unit used to operate on or hold data within a processor. In the RISC-V implementation, the datapath elements include the instruction and data memories, the register file, the ALU, and adders.

To execute an instruction, it needs to be fetched from the instruction memory, and the program counter is incremented to point to the next instruction.

- The instruction memory is a memory unit that store the instructions of a program and supply instructions given an address.
- The program counter is a register that holds the address of the current instruction.
- The adder is an ALU that performs add operations.

To execute an arithmetic-logical instruction (R-type), the processor needs to read 2 data words from the register file, calculate the result with ALU, and write 1 data word into the register fie.

- The processor's 32 general-purpose registers are stored in a register file. The register file contains the register state of the computer. Writes are edge-triggered, so that all the write inputs must be valid at the clock edge. It requires a write signal.

To execute a load instruction, the processor needs to read 1 data word from a register, calculate the address with ALU, read 1 data word from the address, and write 1 data word into the register file. The 12-bit signed offset needs to be sign-extended to 64 bits.

To execute a load instruction, the processor needs to read 2 data words from a register, calculate the address with ALU, and write 1 data word into the address.

- The immediate generation unit turns a 12-bit signed value to a 64-bit signed value.
- The data memory is a memory unit with inputs for the address and the write data, and a single output for the read result. It requires a read and a write signal.

To execute a branch instruction, the processor needs to read 2 data words from a register, test the register contents, calculate the branch target address with ALU, and update the PC. The base for the branch target address calculation is the address of the branch instruction. The offset field represents a half word offset (to match the size of an instruction), so it needs to be shifted left 1 bit before calculating the branch target address.

The datapath components could be combined into a single datapath that attempts to execute each instruction in one clock cycle, which means that no datapath resource could be used more than once per instruction. To share a datapath element between two different instruction classes, a multiplexer could be used to select the inputs.

The control unit must be able to take inputs and generate a write signal for each state element, the selector control for each multiplexer, and the ALU control.
