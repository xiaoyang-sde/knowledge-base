# Function

## Local Object

In C++, names have scope, and objects have lifetimes. The scope of a name is the part of the program’s text in which that name is visible. The life time of an object is the time during the program's execution that the object exists. Parameters and variables defined inside a function are local variables. The lifetime of a local variable depends on how it is defined.

- Automatic object is created when the function's control path passes through the variable's definition and is destroyed when control passes through the end of the block. Function parameters are automatic objects.
- Local static object is initialized before the first time execution passes through the variable's definition and is destroyed when the program terminates.

## Function Declaration

The function declaration is similar to a function definition except that a declaration has no function body. Function should be declared in header file and defined in source file. The header file that declares a function should be included in the source file that defines that function.

## Argument Passing

- When the argument value is copied, the function's parameter is passed by value. The function's parameter could accept a pointer to an object to achieve similar effect with pass by reference.
- When a parameter is a reference, the function's parameter is passed by reference. Reference parameters that are not changed inside a function should be references to `const`.

### Array Parameter

When an array is used as function parameter, it's converted to a pointer. These declarations are equivalent, since each declares a function with a single parameter of type `const int*`:

```cpp
void print(const int*);
void print(const int[]);
void print(const int[10]);
```

Because arrays are passed as pointers, the function doesn't know the size of the array. The program could either pass the `begin` and `end` iterators to the function instead of the array pointer, or use an additional parameter to indicate the size.

### Main

The `main` function has the signature of `int main(int argc, char *argv[])`, which takes the number of arguments and an array of C-strings.

### Various Parameter

The function that takes an unknown number of arguments of a single type could use an `initializer_list` parameter. The `initializer_list` is a template type that contains `const` values.

```cpp
void error_message(initializer_list<string> list) {
  for (auto it = list.begin(); it != list.end(); ++it) {
    cout << *it << endl;
  }
}

error_message({"test", "case", "1"})
```

## Return

The return statement could return a value from a function. The return statement with no value could be used in a function that has a return type of `void`. Otherwise, the value returned must have the same type as the function return type, or it must have a type that can be implicitly converted to that type.

When a function completes, its storage is freed. After a function terminates, references and pointers to local objects are no longer valid.

Calls to functions that return references are lvalues. Other return types yield rvalues. For example, `get_val(s, 0) = 'A'` is valid if `s` is `string[]` and `get_val` returns a reference to an element in `s`.

The function can return a braced list of values that is used to initialize the function's return type.

For complicated return types, the trailing return type is useful. For example, `auto func(int i) -> int(*)[10]` will help the compiler to deduce the return type of `func`.

## Overloaded Function

Functions that have the same name but different parameter lists and that appear in the same scope are overloaded. The compiler could deduce which function is invoked based on the list of argument types. The compiler could use the `const`-ness of the argument to distinguish which function to call.

```cpp
func print(string* text);
func print(const string* text);

func print(string& text);
func print(const string& text);
```

## Specialized Feature

### Default Argument

Functions with default arguments can be called with or without that argument. The default argument is specified as an initializer for a parameter in the parameter list. If a parameter has a default argument, all the parameters that follow it must also have default arguments.

```cpp
string screen(
  string::size_type height = 24,
  string::size_type width = 80,
  char background = ' '
);

screen();
screen(10);
```

### Inline and `constexpr` Function

Calling a function is apt to be slower than evaluating the equivalent expression. On most machines, a function call does a lot of work. The function specified as `inline` could be expanded in line at each call.

The `constexpr` function is a function that can be used in a constant expression. The return type and the type of each parameter in a must be a literal type, and the function must contains one return statement.

```cpp
constexpr int new_size() { return 42; }
constexpr int foo = new_size();
```

## Pointer to Function

The function's type is determined by its return type and the types of its parameters. When a function is used as a value, it's converted to a pointer. The pointer could be invoked to call the function.

```cpp
bool compare(const string&, const string&) { return true };
bool (*compare_pointer)(const string &, const string &) = compare;
compare_pointer("test_1", "test_2");
```

### Function Pointer Parameter

To use a function as a parameter, it will be treated as a pointer.
To return a function pointer, the return type should be a pointer to a function. `decltype` and `auto` simplifies the declaration.

```cpp
void compare(
  const string& s1,
  const string& s2,
  bool (*compare_pointer)(const string&, const string&)
);

int (*f1(int))(int*, int);
auto f1(int) -> int (*)(int*, int);
```

## Lambda Expression

```cpp
[capture_list](argument_list) mutable exception_specification -> return_type {
  // function
}
```

The lambda expression generates a closure, which is runtime object that hold copies of or references to the captured data. The closure is instantiated from a closure class and the statements inside a lambda become executable instructions in the member functions of the closure class.

The lambda expression could capture a variable if it's listed in the `capture_list`. The abbreviations `[=]` and `[&]` enable the compiler to deduce the `capture_list`.

- Value capture copies the variable. The copied variable is mutable if `mutable` is set.
- Reference capture creates a reference to the variable. The referenced variable is mutable. The closure might live longer than the captured variable, which results in a dangling reference.
- Init capture specifies the name of a closure data member and an expression that initializes the data member.

```cpp
auto widget_ptr = std::make_unique<widget>();
auto lambda = [widget = std::move(widget_ptr)] -> int {
  return widget->int_member;
};
```

Generic lambda expression has an `auto` in its parameter list, which generates a function template. The type of the parameter can be determined with `decltype()`.

```cpp
auto print = [](auto &&element) { std::cout << element; };
auto forward_lambda = [print](auto &&element) {
  print(std::forward<decltype(element)>(element));
};
```

## `std::function`

`std::function` is a type erasure object, erases the details of how some operations happen, and provides a uniform run time interface to them. `std::function` could contain all objects that act like a function pointer. The signature is `std::function<return_type(argument_type_list)>`.

```cpp
#include <functional>

function<void(int, int)> depth_first_search = [&](
  int node, int parent
) {
  for (auto child : graph[node]) {
    if (child == parent) {
      continue;
    }
    depth_first_search(child, node);
  }
};
```

The function pointer have the disadvantage of not being able to capture some context for lambda expression. It's advised to use `std::function` unless there's a reason not to do so.

## `std::bind` and `std::placeholder`

```cpp
#include <functional>

int sum(int a, int b, int c) {
  return a + b + c;
}

int main() {
  auto bind_sum = std::bind(sum, std::placeholders::_1, std::placeholders::_2, 1);
  assert(bind_sum(1, 1) == 3);
}
```
