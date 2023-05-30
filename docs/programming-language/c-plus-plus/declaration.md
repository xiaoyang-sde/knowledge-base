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

`inline` can be applied to a variable or function. The definition of an inline variable or function must be reachable in the translation unit where it is accessed. The inline variable or function with external linkage could have more than one definitions in the program as long as each definition appears in a different translation unit and all definitions are identical. For example, an inline variable or function could be defined in a header file that is included in multiple source files. It must be declared `inline` in all translation units and it has the same address in all translation untis. Function templates and member functions defined inside a class are defined `inline`.

## Storage Class

- Atuomatic storage duration: The storage for the object is allocated at the beginning of the enclosing code block and deallocated at the end. Local objects have this storage duration, except those declared `static`, `extern` or `thread_local`.

- Static storage duration: The storage for the object is allocated when the program begins and deallocated when the program ends. Each object has a unique instance. Objects declared at namespace scope have static storage duration.

- Thread storage duration: The storage for the object is allocated when the thread begins and deallocated when the thread ends. Each thread has its own instance of the object.

- Dynamic storage duration. The storage for the object is allocated and deallocated using heap allocation functions, such as `new` and `delete`.

Variables declared at block scope with the specifier `static` or `thread_local` have static or thread storage duration but are initialized the first time control passes through their declaration. On all further calls, the declaration is skipped. The destructor for a block-scope `static` variable is called at program exit.
