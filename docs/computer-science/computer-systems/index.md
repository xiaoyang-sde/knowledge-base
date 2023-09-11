# Computer Systems

## Hello World

```c
#include <stdio.h>

int main() {
  printf("hello, world\n");
  return 0;
}
```

The `hello_world` program is a source program saved in a text file called `hello_world.c`. The source program is a sequence of bits organized in 8-bit chunks called bytes.

To run the `hello_world` program on the system, the individual C statements must be translated into a sequence of low-level machine-language instructions. These instructions are then packaged in a form called an executable object program stored in a executable object file on disk. The compiler performs the translation in several phases.

- The preprocessor (`cpp`) modifies the original C program according to directives that begin with `#`.
- The compiler (`cc`) translates the preprocessed program into an assembly-language program.
- The assembler (`as`) translates the assembly-language program into machine-language instructions, packages them in a form known as a relocatable object program, and stores the result in an object file.
- The linker (`ld`) merges the object file and `libc` into an executable object file.

## Hardware Organization

- The buses are electrical conduits that transmit bytes of information bewteen the components. Buses are designed to transfer fixed-size chunks of bytes known as words.
- The I/O devices are the system's connection to the external world. Each I/O device is connected to the I/O bus through either a controller or an adapter.
- The main memory is a temporary storage device that holds both a program and the data it manipulates while the processor is executing the program. It consists of a collection of DRAM chips and is organized as a linear array of bytes.
- The CPU is the engine that executes instructions stored in the main memory.
  - The register file is a small storage device that consists of a collection of word-size registers, each with its own unique name.
  - The program counter stores the address to the instruction to be executed.
  - The CPU's instruction set architecture (ISA) defines a simple instruction execution model, in which instructions execute in strict sequence. The CPU appears to be a simple implementation of its ISA, but it uses complex mechanisms to speed up execution.
  - The CPU reads the instruction from the memory address stored in the program counter, interprets the bits in the instruction, performs some simple operation, and increments the program counter to point to the next instruction.
- The cache memories are staging areas for information that the processor might need in the near future. Modern system contains multiple levels of caches, such as L1, L2, and L3.

To run the `hello_world` program on the system, the sheel runs a sequence of instructions that copies the code and data of the program from the disk to main memory. The CPU begins executing the instructions in the program's `main` routine. It copies the bytes in the `hello, world\n` string from memory to the register file, and from there to the display device.

## Operating System

The operating system provides an abstraction that protects the hardware from malicious applications and provides applications with simple and uniform mechanisms for manipulating complicated low-level hardware devices.

- Process: The operating system provides the illusion that the program is the unique one running on the system. The CPU appears to execute multiple processes with the context switching mechanism. The operating system keeps track of all the state information that the process needs in order to run, such as the values of the register file. During a context switch, it saves the context of the current process, restores the context of the new process, and passes control to the new process.
- Thread: Each process can consist of multiple execution units, called threads, each running in the context of the process and sharing the same code and global data.
- Virtual memory: The operating system provides the illusion that the program has exclusive use of the main memory. Each process has the same uniform view of memory, which is known as its virtual address space.
- File: The operating system provides the illusion that all I/O devices are files. It provides applications with a uniform view of all the varied I/O devices.

## Concurrent Execution

- Thread-level parallelism: Multi-core processors have several cores integrated onto a single chip, where each core has its unique L1 and L2 cache. Multi-core processors allow parallel execution of multiple threads.
- Instruction-level parallelism: Modern processors can execute multiple instructions at the same time. The pipelining technique partitions the actions required to execute an insturction into different steps and the processor is organized as a series of stages, each performing one of these steps. The stages can operate in parallel, working on different parts of different instructions.
- SIMD parallelism: Modern processors have special hardware that allows a single instruction to cause multiple operations to be performed in parallel.
