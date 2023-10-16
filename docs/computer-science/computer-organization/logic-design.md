# Logic Design

## Gate, Truth Table, and Logic Equation

The electronics inside a modern computer are digital. Digital electronics operate with a high voltage and a low voltage. Rather than refer to the voltage levels, the signals are represented as true (asserted) or false (deasserted).

- Combinational logic: A logic system whose blocks do not contain memory and hence compute the same output given the same input.
- Sequential logic: A group of logic elements that contain memory and hence whose value depends on the inputs and the current contents of the memory.

Logic blocks are built from gates that implement basic logic functions. For example, an AND gate implements the AND function, and an OR gate implements the OR function. NOR and NAND are universal gates.

- Decoder: Each decoder has an $n$-bit input and $2^n$ outputs, where a unique output is asserted for each input combination. For example, a 3-to-8 decoder takes 3 inputs and 8 outputs, where `decode(101) == 00010000`.
- Multiplexer: Each multiplexer has $n$ data inputs and $\lceil \log_2 n \rceil$ control inputs, where the control inputs select which data input will become the output.

### Two-Level Logic

Each logic function could be written in a canonical form, where each input is either a true or complemented variable and there are two levels of gates, where one level is AND and the other level is OR, with a possible inversion on the final output.

- Sum of products representation is a logical sum (OR) of products (AND).
- Product of sums representation is a logical product (AND) of sums (OR).
