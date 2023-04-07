# Exceptional Control Flow

The program counter of the processor assumes a sequence of addresses of instructions. Each transition from $$ a_k $$ to $$ a_{k+1} $$ is called a control transfer. The sequence of such control transfers is called the control flow.

Systems must be able to react to abrupt changes in system state that are not captured by internal program variables and are not necessarily related to the execution of the program. (e.g. Packets arrive at the network adapter and must be stored in memory.) Modern systems react to these situations by making abrupt changes called exceptional control flow.

- Exceptions: implemented by
- Process context switch: implemented by software and hardware timer.
- Signals: implemented by software.
- Non-local jumps: implemented by C runtime library (`setjmp()` and `longjmp()`)

## Exceptions

An exception is a transfer of control to the OS kernel in response to some event. The kernel is the memory-resident part of the OS. Example of events: Divide by 0, arithmetic overflow, page fault, I/O request complete, typing Ctrl-C.

### Exception Table

Each type of event has a unique exception number `k`, which is the index into the exception table. Handler `k` is called each time the exception `k` occurs.

### Asynchronous Exceptions (Interrupts)

Asynchronous exceptions are caused by events external to the processor and are indicated by setting the processor's interrupt pin. The handler handles the exception and returns to the next instruction.

### Synchronous Exceptions (Interrupts)

Synchronous exceptions are caused by events that occur as a result of executing an instruction.

- **Trap**: These exceptions are intentional. It returns control to the "next" instruction. Examples: system calls, breakpoint traps, special instructions, etc.
- **Fault**: These exceptions are unintentional but possibly recoverable. It either re-executes faulting instruction or aborts. Examples: page faults (recoverable), protection faults (unrecoverable), floating point exceptions.
- **Aborts**: These exceptions are unintentional and unrecoverable. It aborts the current program. Examples: illegal instruction, parity error, machine check.
