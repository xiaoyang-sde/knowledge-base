# Makefile

Makefile is used to help decide which parts of a large program need to be recompiled. The Makefile consists of a set of rules.

## Syntax

- The targets are file names, separated by spaces. Typically, there is only one per rule.
- The commands are a series of steps typically used to make the targets.
- The prerequisites (dependencies) are also file names, separated by spaces. These files need to exist before the commands for the target are run.

The `make` command compiles the dependencies of the default target and then invokes its commands.

```makefile
target: prerequisite
  command
  ...

prerequisite:
  command
  ...
```

## Variable

The variables could only be strings, which could be defined as `$(variable_name)` or `${variable_name}`.

- `$@`: the target name
- `$?`: the prerequisites newer than the target
- `$^`: the prerequisites

The `*` wildcard searches the file system for matching file names. `*` should be wrapped in the `wildcard` function, such as `$(wildcard *.o)`.

## Implicit Rule

The list of implicit rules in Makefile:

- Compiling C program: `$(CC) -c $(CPPFLAGS) $(CFLAGS)`
- Compiling C++ program: `$(CXX) -c $(CPPFLAGS) $(CXXFLAGS)`
- Linking single object file: `$(CC) $(LDFLAGS) <file_name> $(LOADLIBES) $(LDLIBS)`

The variables used by implicit rules:

- `CC`: Program for compiling C programs (`cc`)
- `CXX`: Program for compiling C++ programs (`g++`)
- `CFLAGS`: Extra flags to give to the C compiler
- `CXXFLAGS`: Extra flags to give to the C++ compiler
- `CPPFLAGS`: Extra flags to give to the C preprocessor
- `LDFLAGS`: Extra flags to give to compilers when they are supposed to invoke the linker
