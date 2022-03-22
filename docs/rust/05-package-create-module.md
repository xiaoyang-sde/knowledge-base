# Package, Crate, and Module

The crate is a binary or library. The crate root (`src/main.rs` or `src/lib.rs`) is a source file that Rust compiler starts from and makes up the root module of the crate. The package is a few crates that provide a set of functionality, which contains a `Cargo.toml` file. The package could contain at most one library crate.

The module system organizes code within a crate into groups and controls the privacy of items. The module is defined with the `mod` keyword. The `crate` module is the root of the module tree that contains all modules in the crate. The module could be referenced with absolute or relative path.

Rust marks all items (functions, modules, structs, etc.) as private by default. Items in a parent module can’t use the private items inside child modules, but items in child modules can use the items in their ancestor modules. The `pub` keyword marks an item as public.

- The `use <module_path>` syntax imports a path into a scope.
- The `use <module_path> as <module_alias>` syntax specifies an alias to the imported path.
- The `pub use <module_path>` syntax re-exports the path from the current scope.
