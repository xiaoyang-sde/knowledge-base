# Introduction

## The Parts of a Language

### Front End

- Lexical Analysis (Scanning): The **scanner** takes in the linear stream of characters and chunks them into a series of **token** (e.g., characters, numbers, identifiers). Whitespaces and comments are sometimes discarded.

- Parsing: The **parser** takes the flat sequence of tokens and builds an **abstract syntax tree** that mirrors the nested nature of the grammar. The parser could report syntax errors.

- Static Analysis: The static analyzer finds the **scope** for each **identifier**. If the language is statically typed, the analyzer also performs type check.

### Middle End

- Intermediate Representation: The code is stored in an **intermediate representation** that is not tied to either the source or the destination forms. The intermediate representation enables the compiler to support multiple source languages (e.g. Pascal, C, Fortan) and target different platforms (e.g. x86, ARM).

- Optimization: The optimizer swaps the user program with a different one that has the same semantics but more efficient.

### Back End

- Code Generation: The **code generator** converts the optimized intermediate representation to the primitive instructions the CPU could run. The compiler could generate native code for a specific architecture or bytecode for a virtual machine.

- Virtual Machine: If the compiler produces bytecode, a virtual machine that emulates a hypothetical chip is required to run the bytecode.

- Runtime: For all but the basest of low-level languages, a runtime is required to perform several services for the program. For example, each compiled Go application has its own embedded Go runtime, which contains a garbage collector that could reclaim unused bytes.

## Shortcuts and Alternate Routes

- Single-pass Compiler: Simple compilers interleave parsing, analysis, and code generation to produce output code in the parser, without allocating syntax trees or intermediate representations. The single-pass compiler restricts the design of the language because the compiler doesn't have data structures to store global information about the program, and it can't revisit parsed code. Pascal and C are designed around this limitation.

- Tree-walk Interpreter: Some programming languages execute code right after parsing it to an AST. To run the program, the interpreter traverses the syntax tree one branch and leaf at a time, evaluating each node as it goes.

- Transpiler: The transpiler compiles a language to another source language as if it's an intermediate representation. The code generator produces a string of correct source code in the target language. The output could then be compiled with that target langauge's existing compliation pipeline. TypeScript and CoffeeScript are transpiled to JavaScript.

- Just-in-time (JIT) Compilation: Some virtual machines use the JIT compliation technique, which loads a bytecode program and compiles it to native code. The most sophisticated JIT inserts profiling hooks into the generated code to see which regions are most performance critical and what kind of data is flowing through them. Then, it will recompile those hot spots with more advanced optimizations.

## Compilers and Interpreters

- **Compiler** is an implementation technique that involes translating a source language to some other form. The compiler translates the source code but doesn't execute it. For example, GCC and Clang take C code and compile it to machine code. The end user runs that executable and never know which tool was used to compile it.
- **Interpreter** takes in source code and executes it. For example, Matz's Ruby implementation parses the code and executes it directly by traversing the syntax tree.
- **Compiler and Interpreter**: CPython takes the Python code and converts it to an internal bytecode format, which is then executed inside the VM. From the user's perspective, it's an interpreter because it runs program from source. However, there's some compiling in its internal implementation.
