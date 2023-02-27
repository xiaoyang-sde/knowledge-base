# The Lox Language

- Dynamic Type: Variables in Lox can store values of any type, and a single variable can even store values of different types at different times. The type checking is deferred to runtime.

- Automatic Memory Management: Lox uses tracing garbage collection to manage its memory allocation.

- Data Type
  - Boolean: `true` or `false`
  - Number: Double-precision floating point, such as `1234` or `12.34`
  - String: Double-quotes enclosed string literal, such as `"I am a string"`
  - Nil: `nil`

- Expression
  - Arithmetic: `+, -, *, /` （The subexpression on either side of the **binary infix operator** are **operands**.）
  - Comparison: `==, !=, <, <=, >, >=` (Variables of different types are never equivalent.)
  - Logical Operator: `!, and, or`
  - Precedence: All of the operators have the same precedence as C. `()` could group operators.

- Statement: The expression followed by a semicolon is an expression statement. Expressions could be grouped with blocks.

- Variable: The variable is declared with `var` statement. If the initializer is omitted, the value is `nil`. The variable could be accessed or assigned.

- Control Flow
  - `if (condition) {} else {}`
  - `while (condition) {}`
  - `for (initialization; condition; update;) {}`

- Function: `fun name(parameters) { return; }` (The function is first-class in Lox, which means that it's stored in variables. The function returns `nil` without `return` statement.)

- Class: `class name < inherited_name { init(parameters) {} }` (The class is first-class in Lox, which means that it's stored in variables. The class itself is a function that produces a new instance.)
