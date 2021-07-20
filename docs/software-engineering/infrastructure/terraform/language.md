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

### Comments

- `#` or `//` begins a single-line comment, ending at the end of the line.
- `/*` and `*/` are start and end delimiters for a comment that might span over multiple lines.

## Resources

Each resource block describes one or more infrastructure objects, such as virtual networks, compute instances, or higher-level components such as DNS records.

### Resource Syntax

The resource block declares a resource of a given type with a given local name. The name has no significance outside that module's scope.

```hcl
resource "aws_instance" "web" {
  ami           = "ami-a1b2c3d4"
  instance_type = "t2.micro"
}
```

### Resource Types

- **Providers**: Each resource type is implemented by a provider, which provides resources to manage a single cloud or on-premises infrastructure platform.
- **Resource Arguments**: Most of the arguments within the body of a resource block are specific to the selected resource type.

### Resource Behavior

- Create resources that exist in the configuration but are not associated with a real infrastructure object in the state.
- Destroy resources that exist in the state but no longer exist in the configuration.
- Update in-place resources whose arguments have changed.
- Destroy and re-create resources whose arguments have changed but which cannot be updated in-place due to remote API limitations.

Expressions within a Terraform module can access information about resources in the same module with the `<RESOURCE TYPE>.<NAME>.<ATTRIBUTE>` syntax.

Resources could provide read-only attributes with information obtained from the remote API, that can't be known until the resource is created.

### Resource Dependencies

Some resources must be processed after other specific resources.

- Terraform analyses any expressions within a resource block to find references to other objects, and treats those references as implicit ordering requirements.
- The `depends_on` meta-argument can explicitly specify a dependency.

## Variables and Outputs

### Declaring an Input Variable

Input variables serve as parameters for a Terraform module, allowing aspects of the module to be customized without altering the module's own source code.

Within the module that declared a variable, the value of a variable can be accessed from within expressions as `var.<NAME>`.

The input variables could be assigned with these methods:

- The `-var` command line option (e.g. `terraform apply -var="image_id=ami-abc123"`)
- `<file_name>.tfvars` variable definition files (e.g. `terraform apply -var-file="testing.tfvars"`) `terraform.tfvars` or `<file_name>.auto.tfvars` file will be loaded automatically.
- Environment variables

Each input variable accepted by a module must be declared using a `variable` block.

```hcl
variable "image_id" {
  type        = string
  description = "The id of the machine image (AMI) to use for the server."
  sensitive   = true
  validation {
    condition     = length(var.image_id) > 4 && substr(var.image_id, 0, 4) == "ami-"
    error_message = "The image_id value must be a valid AMI id, starting with \"ami-\"."
  }
}
```

- **default**: The default value which then makes the variable optional. The value can't reference other objects in the configuration.
- **type**: The value types that are accepted for the variable.
- **description**: The input variable's documentation.
- **validation**: The block to define validation rules. The supported type keywords are: `string`, `number`, `bool`, `list(<TYPE>)`, `set(<TYPE>)`, `map(<TYPE>)`, `object({<ATTR NAME> = <TYPE>, ... })`, `tuple([<TYPE>, ...])`, or `any`.
  - **condition**: The expression that must use the value of the variable to return `true` if the value is valid.
  - **error_message**: The message that shows if `condition` evaluates to `false`.
- **sensitive**: The option that limits Terraform UI output when the variable is used in configuration.

### Declaring an Output Value

Output values are like the return values of a Terraform module.

- A child module can use outputs to expose a subset of its resource attributes to a parent module.
- A root module can use outputs to print certain values in the CLI output after running terraform apply.

Each output value exported by a module must be declared using an `output` block.

```hcl
output "instance_ip_addr" {
  value = aws_instance.server.private_ip
  description = "The private IP address of the main server instance."
}
```

- `value`: The expression whose result is to be returned to the user.
- **description**: The output variable's documentation.
- **sensitive**: The option that limits Terraform UI output when the variable is used in configuration.
- **depends_on**: The option that creates explicit dependencies.
