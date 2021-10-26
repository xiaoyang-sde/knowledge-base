# Python Module and Package

When Python executes the `import <module_name>` statement, the interpreter searches for `<module_name>.py` in a list of directories assembled from the following sources:

- The directory from which the input script was run or the current directory if the interpreter is being run interactively
- The list of directories contained in the `PYTHONPATH` environment variable, if it is set
- An installation-dependent list of directories configured at the time Python is installed

## Module

Each module has its own private symbol table, which serves as the global symbol table for all objects defined in the module. Thus, a module creates a separate namespace. The location of a module could be accessed with its `__file__` attribute. The `dir()` function returns a list of defined names in a namespace.

- The `import <module_name> (as <alt_name>)` statement places `<module_name>` in the caller’s symbol table. The objects in the module are only accessible when prefixed with `<module_name>` via dot notation.
- The `from <module_name> import <name> (as <alt_name>)` statement allows individual objects from the module to be imported directly into the caller's symbol table.

When a file is imported as a module, Python sets the special dunder variable `__name__` to the name of the module. However, if a file is run as a standalone script, `__name__` is set to the string `'__main__'`.

## Package

Package allows for a hierarchical structuring of the module namespace using dot notation.

If a file named `__init__.py` is present in a package directory, it is invoked when the package or a module in the package is imported. It is used to effect automatic importing of modules from a package.

For the `from <package_name> import *` statement, Python takes the `__all__` list from the `__init__.py` file in the package directory.
