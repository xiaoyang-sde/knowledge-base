# Pipeline

Pipelining is an implementation technique in which multiple instructions are overlapped in execution. It increases instruction throughput, in contrast to decreasing the execution time of an individual instruction.

- IF: Instruction fetch
- ID: Instruction decode and register file read
- EX: Execution or address calculation
- MEM: Data memory access
- WB: Write back

There are registers between each pipeline stages in order to retain the value updated in that stage. All information required for a later pipeline stage must be passed to that stage through a pipeline register.

## Pipeline Hazard

There are situations in pipelining when the next instruction can't execute in the following clock cycle.

- Structural hazard: The planned instruction can't execute in the proper clock cycle because the hardware does not support the combination of instructions that are set to execute.
- Data hazard: The planned instruction can't execute in the proper clock cycle because data that are needed to execute the instruction are not available. The pipeline must be stalled because one step must wait for another to complete.
  - Forwarding is a technique that retrieves the missing data element from internal buffers to resolve a data hazard. (There's no need to forward for `x0` because it's value should be `0`.)
  - For example, consecutive instructions might operate on the same register.
- Control hazard: The planned can't execute in the proper clock cycle because the instruction that was fetched is not the one that is needed. (The flow of instruction addresses is not what the pipeline expected.)
  - Branch prediction is technique that assumes a given outcome for the conditional branch and proceeds from that assumption rather than waiting to ascertain the actual outcome. It maintains a mapping from the address of the branch instruction to previous results represented in a 2-bit prediction scheme.
