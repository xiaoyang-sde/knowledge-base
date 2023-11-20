# Declaration

## Linkage

In a C++ program, a symbol, for example a variable or function name, can be defined once and declared multiple times within its scope. The one definition rule states that a unique definition of a variable, function, class type, enumeration type, concept or template is allowed in a translation unit.

- The definition introduces a name and provides all the information needed to create it, such as `int i = 42;` or `void f() {}`. If the name represents a variable, a definition creates storage and initializes it.

- The declaration introduces a name into the program with enough information to associate the name with a definition, such as `int f(int x);` or `class C;`.

Each program has one or more translation units. Each translation unit consists of a `.cpp` implementation file and all the headers that it includes. The compiler compiles each translation units and the linker merges the compiled units into a single program. Include guards prevent a name from being declared multiple times in the same translation unit.

- No linkage: The name can be referred to from its scope.
  - Variables declared at block scope that aren't declared `extern`
  - Local classes and member functions declared at block scope

- Internal linkage: The name can be referred to from all scopes in the current translation unit. When a name has internal linkage, the same name might exist in another translation unit.
  - Variables, variable templates, functions, or function templates declared `static`
  - Non-template non-`inline` variables of non-`volatile` `const`-qualified type
  - All names declared in unnamed namespace

- External linkage: The name can be referred to from all translation units in the program.
  - Non-`const` variables and functions not declared `static`
  - Variables declared `extern`
  - Classes, member functions, and static data members

`inline` can be applied to a variable or function. The definition of an inline variable or function must be reachable in the translation unit where it is accessed. The inline variable or function with external linkage could have more than one definitions in the program as long as each definition appears in a different translation unit and all definitions are identical. For example, an inline variable or function could be defined in a header file that is included in multiple source files. It must be declared `inline` in all translation units and it has the same address in all translation units. Function templates and member functions defined inside a class are defined `inline`.

### Example

```cpp
extern int extern_variable;
static int static_variable = 0;
int global_variable = 0;
inline int inline_variable = 0;
```

```console
$ clang++ -c main.cpp -o main.o
$ readelf -s main.o

Symbol table '.symtab' contains 9 entries:
   Num:    Value          Size Type    Bind   Vis      Ndx Name
     0: 0000000000000000     0 NOTYPE  LOCAL  DEFAULT  UND
     1: 0000000000000000     0 FILE    LOCAL  DEFAULT  ABS main.cpp
     2: 0000000000000000     0 SECTION LOCAL  DEFAULT    2 .text
     3: 0000000000000004     4 OBJECT  LOCAL  DEFAULT    6 static_variable
     4: 0000000000000000     0 SECTION LOCAL  DEFAULT    6 .bss
     5: 0000000000000000   143 FUNC    GLOBAL DEFAULT    2 main
     6: 0000000000000000     0 NOTYPE  GLOBAL DEFAULT  UND extern_variable
     7: 0000000000000000     4 OBJECT  GLOBAL DEFAULT    6 global_variable
     8: 0000000000000000     4 OBJECT  WEAK   DEFAULT    8 inline_variable
     9: 0000000000000000     0 NOTYPE  GLOBAL DEFAULT  UND __stack_chk_fail
```

- `extern_variable`: `GLOBAL` binding in the `UND` (undefined) section
- `static_variable`: `LOCAL` binding
- `global_variable`: `GLOBAL` binding
- `inline_variable`: `WEAK` binding

## Storage Class

- Automatic storage duration: The storage for the object is allocated at the beginning of the enclosing code block and deallocated at the end. Local objects have this storage duration, except those declared `static`, `extern` or `thread_local`.

- Static storage duration: The storage for the object is allocated when the program begins and deallocated when the program ends. Each object has a unique instance. Objects declared at namespace scope have static storage duration.

- Thread storage duration: The storage for the object is allocated when the thread begins and deallocated when the thread ends. Each thread has its own instance of the object.

- Dynamic storage duration. The storage for the object is allocated and deallocated using heap allocation functions, such as `new` and `delete`.

Variables declared at block scope with the specifier `static` or `thread_local` have static or thread storage duration but are initialized the first time control passes through their declaration. On all further calls, the declaration is skipped. The destructor for a block-scope `static` variable is called at program exit.

## One-Definition Rule

- No translation unit shall contain more than one definition of a variable, function, class type, enumeration type, or template.
- Each program shall contain one definition of each non-inline function or variable that is odr-used in that program. "odr-used" means that an item is used in a context where the definition of it must be present. (This rule doesn't cover class type, enumeration type, inline function with external linkage, class template, non-static function template, static data member of a class template, member function of a class template, or partial template specialization.)

## Header Guard

The header guard is a few preprocessors that prevent a header to be included more than once in a translation unit. It defines preprocessor variables to indicate whether or not the header has been included.

```cpp
#ifndef SALES_DATA_H
#define SALES_DATA_H

#include <string>

struct sales_data {
  string book_no;
  unsigned unit_sold = 0;
  double revenue = 0.0;
}

#endif
```
