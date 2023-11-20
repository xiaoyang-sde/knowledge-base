# Linking

Linking is the process of collecting and combining various pieces of code and data into a single file that can be loaded into memory and executed. Linking can be performed at compile time, load time, or run time.

The C compiler driver `gcc` invokes the language preprocessor `cpp`, the compiler `cc1`, the assembler `as`, and the linker `ld` to convert an ASCII source file to an executable object file.

## Relocatable Object File

The assembler generates the relocatable object file, which contains an ELF header and multiple sections. The ELF header contains several information, such as the word size, byte ordering, the object file type (relocatable, executable, or shared), the machine type, the file offset of the section header table, and the number of entires in the section header table. The section header table describes the locations and sizes of the various sections.

```rust
pub struct Header {
    pub e_ident           : [u8; SIZEOF_IDENT],
    pub e_type            : u16,
    pub e_machine         : u16,
    pub e_version         : u32,
    pub e_entry           : u64,
    pub e_phoff           : u64,
    pub e_shoff           : u64,
    pub e_flags           : u32,
    pub e_ehsize          : u16,
    pub e_phentsize       : u16,
    pub e_phnum           : u16,
    pub e_shentsize       : u16,
    pub e_shnum           : u16,
    pub e_shstrndx        : u16,
}
```

- `.text`: machine code the the compiled program
- `.rodata`: read-only data, such as string literals
- `.data`: initialized global and static variables (local variables are maintained at run time on the stack)
- `.bss`: uninitialized or zero-initialized global and static C variables
- `.symtab`: a symbol table with information about functions and global variables that are defined and referenced in the program (local variables are not accessible outside of their scope, so other parts of the program can't reference them)
- `.rela.text`: a list of locations in the `.text` section that will need to be modified when the linker combines this object file with others (in general, all instructions that call an external function or reference a global variable will need to be modified)
- `.rela.data`: a list of global variables that are referenced or defined in the module
- `.debug`: a debugging symbol table with entires for variables and the original C source file
- `.line`: a mapping between line numbers in the original C source program and machine code instructions in the `.text` section
- `.strtab`: a list of null-terminated strings for the symbol tables in the `.symtab` and `.debug` sections and for the section names in the section headers

## Symbol Resolution

The ELF symbol table in the `.symtab` section contains a sequence of elements.

- `name`: a byte offset into the `.strtab` that points to the null-terminated string name of the symbol
- `value`: an offset from the beginning of the section where the object is defined (relocatable) or an absolute run-time address (executable)
- `size`: the size of the object
- `type`: the type of the symbol
- `binding`: the binding of the symbol
  - `LOCAL`: the symbol will be used to resolve references in its own module
  - `GLOBAL`: the symbol will be used to resolve references between different modules
  - `WEAK`: the symbol will be used to resolve references between different modules, but has lower precedence than a `GLOBAL` symbol with the same name
- `section`: the section of the symbol

The linker associates each reference with one symbol definition from the symbol tables of its input relocatable object files. For global variables not defined in the current module, the compiler generates a linker symbol table element, and let the linker to resolve the reference. If there are duplicated symbols with `GLOBAL` bindings, the linker will generate an error.

Because C++ allows function overloading, the linker needs to distinguish between each overload of the function. The compiler performs mangling to generate a unique symbol name for each method and parameter type combination.

## Static Linking

The static linker `ld` takes a collection of relocatable object files and generates a linked executable object file that can be loaded and run. The linker needs to perform symbol resolution and relocation.
