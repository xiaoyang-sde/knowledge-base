# CS 61B Week 02

## References, Recursion, and Lists

### Primitive Types

#### Mystery of Walrus

The code below will both change the weight of Walrus a and b.

```java
Walrus a = new Walrus(1000, 8.3);
Walrus b;
b = a;
b.weight = 5;
System.out.println(a);
System.out.println(b);
```

However, the code below will only change integer x to 2.

```java
int x = 5;
int y;
y = x;
x = 2;
System.out.println("x is: " + x);
System.out.println("y is: " + y);
```

#### Bits

Information are stored in memory of computers, which is a sequence of ones and zeros.
The identical sequence may have different meanings, since each Java type has a different way to interpret bits. 
For instance, '01001000' may represent integer 72 or character 'H'.

Java has 8 **primitive types**: `byte`, `short`, `int`, `long`, `float`, `double`, `boolean`, `char`. 
Computers set aside exactly enough bits to hold a thing of a certain type when you declare a variable of that type. 
For example, declaring an int sets aside a box of 32 bit, and a double sets aside a box of 64 bits. 

* Java creates an internal table that maps each variable name to a location.
* Java does not write anything into the boxes, and does not allow access to an uninitialiized variable.

### Reference Types

Every type not included in the primitive types is a **reference type**, such as `Array`.

#### Object Instantiation

When we instantiate an object, Java first allocates a box of bits for each instance variable of the class and fills them with a default value. The constructer usually fill them with other values.

Typically, object will have overheads in addition to the memory used by its instance variables.

#### Reference Variable Declaration

When we declare a variable of any reference type:

* Java allocates a box of size 64 bits.
* These bits can be either set to Null or the address of a specific instance of that class (returned by `new`).

### The Golden Rule of Equals

When you write `y = x`, you are telling the Java interpreter to **copy the bits** from x into y.

Just as with primitive types, the equals sign copies th bits stored in the reference variable, which is the address of a specific instance. Moreover, passing parameters will obey the rule.

### Arrays

Arrays are also objects, which are instantiated using the `new` keyword.

* Creates a 64 bit box for storing an array address. (Declaration)
* Creates a new array object. (Instantiation)
* Puts the address of this new object into the 64 bit box. (Assignment)

```java
int[] x; // Declaration
new int[] {0, 1, 2, 3}; // Instantiation
int[] x = new int[] {0, 1, 2, 3}; // Declaration, Instantiation, Assignment
```

### IntLists

Here's a very basic linked list:

```java
public class IntList {
    public int first;
    public IntList rest;        

    public IntList(int f, IntList r) {
        first = f;
        rest = r;
    }
}
```

There are two ways to create a list of the numbers 5, 10, and 15: build it forward or backward.

```java
IntList L = new IntList(5, null);
L.rest = new IntList(10, null);
L.rest.rest = new IntList(15, null);
```

```java
IntList L = new IntList(15, null);
L = new IntList(10, L);
L = new IntList(5, L);
```

We will add methods to the IntList class to let it perform basic tasks.

#### size() and iterativeSize()

We could either use recursive or iterative methods to get the size of the IntList.

```java
public int size() {
    if(this.rest == null) {
        return 1;
    } else {
        return 1 + rest.size();
    }
}

public int iterativeSize() {
    IntList p = this;
    int totalSize = 0;
    while (p != null) {
        totalSize += 1;
        p = p .rest;
    }
    return totalSize;
}
```

#### get()

Write a method `get(int i)` that returns the ith item of the list.

```java
public int get(int i) {
    if (i == 0) {
        return this.first;
    } else {
        return this.rest.get(i - 1);
    }
}
```

## SLLists, Nested Classes, Sentinel Nodes

The IntList class we've already created is called a 'naked' data structure which is hard to use.

### Rebranding

```java
public class IntNode {
    public int item;
    public IntNode next;

    public IntNode(int i, IntNode n) {
        item = i;
        next = n;
    }
}
```

Knowing that `IntNodes` are hard to work with, we're going to create a separate class called `SLList` that the user will interact with.

```java
public class SLList {
    public IntNode first;

    public SLList(int x) {
        first = new IntNode(x, null);
    }
}
```

Thus, we could create a list by using `new SLList(10);`, which is easier to instantiate, instead of using `new IntNode(10, null);`.

### Private

However, our `SLList` can be bypassed and the naked data structure can be accessed. In order to solve this problem, we could make the `first` from public to private. Thus, it could be only accessed within the same class.

```java
public class SLList {
    private IntNode first;
...
```

* Hide implementation details from users of your class.
* Safe for you to change private methods.
* Nothing to do with protection against hackers or other evil entities.

### Nested Classes

* Java provides a simple way to combine two classes within one file.
* Having a nested class has no meaningful effect on code performance, and is simply a tool for keeping code organized.
* When `IntNode` is declared as `static class`, it could not access any instance methods or variables of `SLList`.

```java
public class SLList {
       private static class IntNode {
            public int item;
            public IntNode next;
            public IntNode(int i, IntNode n) {
                item = i;
                next = n;
            }
       }

       private IntNode first; 

       public SLList(int x) {
           first = new IntNode(x, null);
       } 
...
```

### Methods

#### addFirst()

```java
/** Adds x to the front  of the list. */
public void addFirst(int x) {
    first = new IntNode(x, first);
}
```

#### getFirst()

```java
/** Returns the first item in the list. */
public int getFirst() {
    return first.item;
}
```

#### addLast()

```java
public void addLast(int x) {
    IntNode p = first;
    while (p.next != null) {
        p = p.next;
    }
    p.next = new IntNode(x, null);
}
```
#### size()

```java
/** Returns the size of the list that starts at IntNode p. */
private static int size(IntNode p) {
    if (p.next == null) {
        return 1;
    }
    return p.next.size() + 1;
}

public int size() {
    return size(first);
}
```

### Caching
Obviously, the `size()` method is unefficient, so we will add a integer variable to track the size of the list.

```java
private int size;

public SLList (int x) {
    size = 1;
    ...
}

public void addFirst(int x) {
    size += 1;
    ...
}

public void addLast (int x) {
    size += 1;
    ...
}

public int size() {
    return size;
}
```

### Empty List

Here is a simple way to create an empty list, but it has subtle bugs: The program will crash when you add an item to the last of the list.

```java
public SLList() {
    first = null;
    size = 0;
}
```

In order to fix the bug, we could either fix the `addLast()` method, which is not simple, or add a sentinel node.

### Sentinel Node

We could create a special node that is always there, which is called a "sentinel node".

```java 
/** The first item, if it exists, is at sentinel.next. */
private IntNode sentinel;

public SLList() {
    sentinel = new IntNode(63, null);
    size = 0;
}

public SLList(int x) {
    sentinel = new IntNode(63, null);
    sentinel.next = new IntNode(x, null);
    size = 1;
}

public void addFirst(int x) {
    sentinel.next = new IntNode(x, sentinel.next);
    size = size + 1;
}

public int getFirst() {
    return sentinel.item;
}

public void addLast(inx x) {
    IntNode p = sentinel;
    while (p.next != null) {
        p = p.next;
    }
    p.next = new IntNode(x, null);
    size = size + 1;
}
```

### Invariants

A `SLList` with a sentinel node has at least the following invariants:

* The `sentinel` reference always points to a sentinel node.
* The front item (if it exists), is always at `sentinel.next.item`.
* The `size` variable is always the total number of items that have been added.

## DLLists, Arrays

Although SLList provides a efficient way for us to manipulate the list, it's really slow when we want to add an item to the end of the list.

### addLast()

Similar to `size()`, we could cache the last pointer; however, remove it will be still really slow because we will iterate to the second last item.

We could add a pointer to each node which points to the previous node, but the problem is that the `last` pointer of the `SLList` class may points to the sentinel node.

Thus, we could add a second sentinel at the end of the list, and point it with `sentBack`.

Alternatively, we may just use a single sentinel and make the list circular.

### Generic List

We could improve our IntList to make it available for other types.

```java
public class DLList<BleepBlorp> {
    private IntNode sentinel;
    private int size;

    public class IntNode {
        public IntNode prev;
        public BleepBlorp item;
        public IntNode next;
        ...
    }
    ...
}
```

We put the desired type inside of angle brackets during declaration, and also use empty angle brackets during instantiation.

```java
DLList<String> d2 = new DLList<>("hello");
d2.addLast("world");
```

### Array Overview

### Definition and Creation

Array is a special kind of object which consists of a numbered sequence of memory boxes. An array consists of:

* A fixed integer `length`.

* A sequence of N memory boxes where `N=length`, and all boxes hold the same type of value, which are numbered from 0 to `length-1`.

Like classes, arrays are instantiated with `new`:

```java
y = new int[3];
x = new int[]{1, 2, 3, 4, 5};
int[] w = {9, 10, 11, 12, 13};
```

#### Arraycopy

Two ways to copy arrays:
* Item by item using a loop.
* Using arraycopy: Source array, start position in source, target array, start position in target, number to copy.

```java
System.arraycopy(b, 0, x, 3, 2);
```

#### 2D Array

We could create a 2-dimensional array with the following codes. 

```java
int[][] matrix;
matrix = new int[4][4];
matrix = new int[4][];
matrix[0] = new int[]{1};

int[][] pascalAgain = new int[][]{{1}, {1, 1},{1, 2, 1}, {1, 3, 3, 1}};
```

#### Arrays and Classes

Both arrays and classes can be used to organize a bunch of memory boxes. In both cases, the number of memory boxes is fixed, i.e. the length of an array cannot be changed, just as class fields cannot be added or removed.

The key differences between memory boxes in arrays and classes:

* Array boxes are numbered and accessed using [] notation, and class boxes are named and accessed using dot notation.
* Array boxes must all be the same type. Class boxes can be different types.

