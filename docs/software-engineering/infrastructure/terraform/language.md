# Terraform Language

The main purpose of the Terraform language is declaring resources, which represent infrastructure objects. The Terraform configuration is a complete document in the Terraform language that tells Terraform how to manage a given collection of infrastructure.

```ruby
resource "aws_vpc" "main" {
  cidr_block = var.base_cidr_block
}

<BLOCK TYPE> "<BLOCK LABEL>" "<BLOCK LABEL>" {
  # Block body
  <IDENTIFIER> = <EXPRESSION> # Argument
}
```

- Blocks are containers for other content and usually represent the configuration of some kind of object. Blocks have a block type, can have zero or more labels, and have a body that contains any number of arguments and nested blocks.
- Arguments assign a value to a name.
- Expressions represent a value, either literally or by referencing and combining other values.

## Files and Directories

- **File extension**: Code in the Terraform language is stored in plain text files with the `.tf` file extension.
- **Text encoding**: Configuration files must always use UTF-8 encoding and use Unix-style line endings (LF).
- **Directories and modules**: A module is a collection of top-level `.tf` files in a directory. Terraform treats the entire module as a single document. A Terraform module can use module calls to explicitly include other modules into the configuration.
- **The root module**: Terraform always runs in the context of a single root module. In Terraform CLI, the root module is the working directory where Terraform is invoked.

### Override Files

Terraform loads all of the `.tf` files within a directory. If two files attempt to define the same object, Terraform returns an error. However, Terraform has special handling of any configuration file whose name ends in `_override.tf`.

Terraform initially skips these override files when loading configuration, and then Terraform attempts to find an already-defined object corresponding to that block for each top-level block defined in an override file, and then merges the override block contents into the existing object.

## Syntax

The low-level syntax of the Terraform language is defined in terms of a syntax called HCL, which is also used by configuration languages in other applications, and in particular other HashiCorp products.

The Terraform language syntax is built around two key syntax constructs: arguments and blocks.

### Arguments

An argument assigns a value to a particular name. The identifier before the equals sign is the argument name, and the expression after the equals sign is the argument's value.

```hcl
image_id = "abc123"
```

### Blocks

A block is a container for other content. A block has a type and a few labels. For example, the `resource` block type expects two labels, which are `aws_instance` and `example`. The block body is delimited by the `{` and `}` characters.

```hcl
resource "aws_instance" "example" {
  ami = "abc123"

  network_interface {
    # ...
  }
}
```

### Identifiers

Argument names, block type names, and the names of most Terraform-specific constructs like resources, input variables, etc. are all identifiers.

Identifiers can contain letters, digits, underscores (`_`), and hyphens (`-`). The first character of an identifier must not be a digit, to avoid ambiguity with literal numbers.
