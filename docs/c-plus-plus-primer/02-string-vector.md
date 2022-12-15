# String and Vector

## `using`

The `using` declaration (`using namespace::name`) lets the program to use a name from a namespace without qualifying the name. For example, `using std::cin` imports the `cin` name from the `std` namespace. The header files should not use the `using` declaration because their entire contents will be copied to the program that includes them, which might cause conflicting definitions.

## String

The `string` container is a variable-length sequence of characters. It's defined in the `string` header of the `std` namespace. Each class defines how objects of its type can be initialized. The `string` class could be initialized with default constructor, string literal, or a character with count.

- Direct initialization: Invoke the constructor of the class
- Copy initialization: Assign an initializer to the object

```cpp
#include <string>
using std::string;

string s_1;
string s_2 = "test"; // copy initialization
string s_3(10, 'c'); // direct initialization
string s_4 = string(10, 'c'); // copy initialization
```

The `size()` method of the `string` class returns an unsigned integer with `string::size_type` type. The type is able to hold the length of the string.

The comparator of the `string` class determine the order of two strings with these criteria:

- If two strings have different lengths and if every character in the shorter string is equal to the corresponding character of the longer string, then the shorter string is less than the longer one.
- If a characters at corresponding positions in the two strings differ, then the result of the string comparison is the result of comparing the first character at which the strings differ.

The range for-loop could process all characters the string holds. To access individual character, the `[]` operator takes a `string::size_type` and returns a reference of `char`. Out-of-range access returns undefined result.

## Vector

The `vector` container is a collection of objects with the same type. The `vector` is a class template such that the compiler could use it to generate code.

```cpp
#include <vector>
using std::vector;

vector<int> int_vector; // default initialization
vector<int> int_vector_clone(int_vector); // clone
vector<string> article_list = {"a", "an", "the"}; // list initialization
vector<string> a_list("a", 10); // value initialization
vector<string> string_list(10); // value initialization
```

The range for-loop could process all characters the vector holds. To access individual element, the `[]` operator takes a `vector<T>::size_type` and returns a reference of type `T`. Out-of-range access returns undefined result.

- `push_back` is a method that could add elements to the end of the vector.
- `size` is a method that returns the size of the vector with the `vector<T>::size_type` type.

## Iterator

Iterator provides indirect access to elements in the containers. The `begin` method returns an iterator that points to the first element, while the `end` method returns an iterator that points to an element past the last element. The iterator could be dereferenced to access the elememt it points to. The `cbegin` and `cend` methods return constant iterators. Iterator allows arithmetic operations to move the iterator to different positions of the container.

```cpp
vector<char> char_list(10);
for (auto it = char_list.begin(); it != char_list.end(); ++it) {
  *it = toupper(*it);
}

vector<char>::iterator it = char_list.begin(); // regular iterator
vector<char>::const_iterator it = char_list.begin(); // constant iterator
```

## Array

The array is a collection of objects with the same type and fixed size. The array is a compound type and has a declarator with the form of `a[d]`, in which `d` indicates the size of the array. The size must be a constant expression. The elements in the array are default initialized. It's illegal to assign an array to another array.

```cpp
int int_list_1[3];
int int_list_2[3] = {1, 2, 3};
int int_list_3[] = {1, 2, 3};

char char_list_1[] = {'a', 'b', 'c'};
char char_list_2[] = "abc"; // 'a', 'b', 'c', '\0'

int *ptr_arr[10]; // array of pointers
int (*arr_ptr)[10] = &arr; // pointer to array
int (&arr_ptr)[10] = arr; // reference to array
```

The range for-loop could process all elements the array holds. To access individual element, the `[]` operator takes a `size_t` and returns a reference of the element. Out-of-range access returns undefined result.

### Array and Pointer

In C++ pointers and arrays are intertwined. When an array is used, the compiler substitutes a pointer to the first element.

```cpp
int arr[3] = {1, 2, 3};
int *element = arr; // equivalent to `int *element = &arr[0]`
element++; // point to the second element
```

The `begin` and `end` function returns a pointer to the first or the one past the last element in the array. These functions are defined in the `iterator` header.

```cpp
int *first = begin(arr);
int *last = end(arr);
```

The `vector` class provides a constructor that accepts two pointers. The two pointers used to construct the vector mark the range of values to use to initialize the elements.

```cpp
vector<int> int_vec(begin(int_arr), end(int_arr));
```

### Multi-dimensional Array

The multi-dimensional array in C++ is an array of arrays. It could be initialized similar to the one-dimensional array.

```cpp
int arr[10][20][30] = {0};
int matrix[3][3] = {
  {1, 2, 3},
  {1, 2, 3},
  {1, 2, 3}
};
```
