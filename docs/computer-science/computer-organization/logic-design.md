# Logic Design

## Gate, Truth Table, and Logic Equation

The electronics inside a modern computer are digital. Digital electronics operate with a high voltage and a low voltage. Rather than refer to the voltage levels, the signals are represented as true (asserted) or false (deasserted).

- Combinational logic: A logic system whose blocks do not contain memory and hence compute the same output given the same input.
- Sequential logic: A group of logic elements that contain memory and hence whose value depends on the inputs and the current contents of the memory.

Logic blocks are built from gates that implement basic logic functions. For example, an AND gate implements the AND function, and an OR gate implements the OR function. NOR and NAND are universal gates.

- Decoder: Each decoder has an $n$-bit input and $2^n$ outputs, where a unique output is asserted for each input combination. For example, a 3-to-8 decoder takes 3 inputs and 8 outputs, where `decode(101) == 00010000`.
- Multiplexer: Each multiplexer has $n$ data inputs and $\lceil \log_2 n \rceil$ control inputs, where the control inputs select which data input will become the output.

Bus is a collection of data lines that is treated together as a single logical signal. For example, in the RISC-V instruction set, the result of an instruction that is written into a register can come from one of two sources. Since each register contains 64 bits, the 1-bit multiplexer will need to be replicated 64 times.

### Two-Level Logic

Each logic function could be written in a canonical form, where each input is either a true or complemented variable and there are two levels of gates, where one level is AND and the other level is OR, with a possible inversion on the final output.

- Sum of products representation is a logical sum (OR) of products (AND).
- Product of sums representation is a logical product (AND) of sums (OR).

PLA (programmable logic array) is a structured-logic element composed of a set of inputs and corresponding input complements. The first stage of logic generates product terms of the inputs and input complements. The second stage generates sum terms of the product terms. Therefore, PLAs implement logic functions as a sum of products.

ROM is a memory whose contents are designed at creation time, after which the contents can only be read. ROM is used as structured logic to implement a set of logic functions by using the terms in the logic functions as address inputs and the outputs as bits in each word of the memory.

## Hardware Description Language

HDL is a programming language for describing hardware, used for generating simulations of a hardware design and as input to synthesis tools that generates actual hardware.

There are several data types in Verilog. `wire` specifies a combinational signal and `reg` holds a value that might change with time. For example, a 64-bit register could be declared as `reg [63:0] x1` and a register file could be declared as `reg[63:0] rf[0:31]`.

Each Verilog program is structured as a set of modules. Each module specifies its input and output ports, which describe the incoming and outgoing connections of a module. Each module contains `initial` constructs that initializes `reg` variables, continue assignments that defines combinational logic, and `always` constructs, which defines either sequential or combinational logic.

- Continuous assignment (`assign`) is a combinational logic function. The output is assigned based on the input value.
- `always` block specifies an optional list of signals on which the block is sensitive, and the block is re-evaluated if a listed signal changes value.
  - `reg` variables should be assigned inside an `always` block, using a procedural assignment statement. `=` is a blocking assignment, which blocks the next statement, and `<=` is a non-blocking assignment, which continues after evaluating the right-hand side, and assigning the left-hand side after all right-hand sides are evaluated.

```v
module mult_4_to_1 (
  input [63:0] in_1, in_2, in_3, in_4,
  input [1:0] sel,
  output reg [63:0] out
);
  always @(in_1, in_2, in_3, in_4, sel)
    case (sel)
      0: out <= in_1;
      1: out <= in_2;
      2: out <= in_3;
      3: out <= in_4;
    endcase
endmodule
```

## Clock

The clock is a free-running signal with a fixed cycle time, which is needed in sequential logic to decide when an element that contains state should be updated. Edge-triggered clocking means that all state changes occur on a clock edge.

The major constraint in a clocked system is that the signals that are written into state elements must be valid when the active clock edge occurs. The length of the clock period must be long enough for all state element inputs to be valid.
