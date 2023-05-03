# Programming Language

## Programming Paradigm

- Imperative programming is a programming paradigm that focuses on describing how a program operates by giving step-by-step instructions for the computer to follow. It is based on the idea of changing program state by executing statements that alter the program's state. In imperative programming, the programmer specifies the algorithms and control flow directly in terms of statements, loops, and conditionals.

- Functional programming is a programming paradigm that emphasizes the use of lambda functions to perform computation. In functional programming, the focus is on declaring what the program should do, rather than how it should do it. Functions are treated as first-class citizens and can be passed as arguments to other functions or returned as values from functions. In functional programming, the state is kept immutable, which means that once a value is assigned, it cannot be changed.

## Type System

- Static typing is the process of verifying that all operations in a program are consistent with the types of the operands prior to program execution. Types can either be explicitly specified (like in C++) or can be inferred from code (like in Haskell). To support static typing, a language must have a fixed type bound to each variable at the time of definition.
  - Type inference refers to the automatic detection of types of expressions or variables in a language. Type inference is a complex constraint satisfaction problem.

- Dynamic typing is the process of verifying the types of a program during execution. If the code is attempting an illegal operation on a variable's value, an exception is generated and the program crashes. Type annotations for variables might be optional, which means that types are associated with values and a variable name could refer to values of multiple types.
  - Duck typing allows variables or values to be evaluated based on their behavior rather than their specific type or class.

- Gradual typing is a hybrid approach between static and dynamic typing. Some variables might have annotated types, while others are not. It allows the type checker to perform some checks prior to execution and the rest of the checks during execution.

### Strictness

- Strong typing ensures that the program doesn't have undefined behavior during execution due to type issues. The type checker doesn't allow unchecked type error, such as performing an unchecked type cast, dereferencing a dangling pointer, or performing unbounded pointer arithmetic.

- Weak typing doesn't guarantee that all operations are invoked on objects or values of the appropriate type.
  - Undefined behaviour is the result of executing a program whose behaviour is prescribed as unpredictable in the language specification. Defined implicit conversion is not undefined behavior.

### Casting and Conversion

Given two types $T_{\text{sub}}$ and $T_{\text{super}}$, $T_{\text{sub}}$ is a subtype of $T_{\text{super}}$ $\iff$ all elements belonging to the set of values of type $T_{\text{sub}}$ is a member of the set of values of type $T_{\text{super}}$ and all operations that can be used on a value of type $T_{\text{super}}$ must work on a value of type $T_{\text{sub}}$. For example, `float` is a subtype of `double`, `int` is a subtype of `const int`, and `int` is a subtype of `double` because `double` have enough precision to represent all values that `int` can hold.

Type conversion and type casting are methods to convert a value of a type into another type. Widening means that the target type is a super type and can represent the source type's values and narrowing means that the target type might lose precision or failed to represent the source type's values.

- Type conversion takes a value of type $A$ and generates a whole new value of type $B$, which might have different bit representation.
- Type casting takes a value of type $A$ and reinterpret its bit representation as type $B$. The most common type of casting is from a super type to a subtype, such as upcasting from a subclass to a super class.

## Scope and Lifetime

The scope of a variable is the range of a program's instructions where the variable is known. The set of in-scope variables and functions at a particular point in a program is called its lexical environment. Each variable is in scope if it's accessible through its name.

Each variable has a lifetime from its creation to destruction. Each variable's lifetime could include times when the variable is in scope, and times when it is not in scope. While a variable's lifetime is limited to the execution of the function where it's defined, a value could live longer than the variable.

- Lexical scoping: When a variable is referenced in a program, lexical scoping searches for the variable's declaration in the nearest enclosing scope, and if not found, it continues searching in the next enclosing scope, and so on, until the variable is found or the outermost scope is reached. The scope could be expression, block, function, class, namespace, or global.

- Dynamic scoping: When a variable is referenced in a program, dynamic scoping searches for the variable's declaration in the current execution context or call stack, rather than the static structure of the program.

## Garbage Collection

Garbage collection is the automatic reclamation of allocated memory that is no longer referenced. The program doesn't control object destruction. When a value of object on the heap is no longer refereed to, the program detects this at runtime and frees the memory associated with it. It eliminates memory leaks, dangling pointers, and double-free bugs.

- Mark and sweep: The garbage collector traverses from all global, local, and member variables that are object references and frees all objects that are not reached.
  - Mark phase: The algorithm identifies all objects that are still referred to and thus considered to be in-use. Each object is in-use if it's either a root object (global variables, local variables across all stack frames, and parameters on the call stack) or it's reachable from a root object thourgh a pointer or a reference.
  - Sweep phase: The algorithm scans all heap memory from start to finish, and frees all blocks not marked as being in-use. Each object on the heap has a metadata chunk that stores the size of the current object and a pointer to the next object.
- Mark and compact: The garbage collector discovers all in-use objects, moves them to a new block of memory, and reclaims the old block of memory.
- Reference counting: Each object keeps a count of the number of active object references that point at it. When an object's count reaches zero, it's reclaimed.
