# References, Recursion, and Lists

## Primitive Types

### Mystery of Walrus

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

### Bits

Information are stored in memory of computers, which is a sequence of ones and zeros. The identical sequence may have different meanings, since each Java type has a different way to interpret bits. For instance, '01001000' may represent integer 72 or character 'H'.

Java has 8 **primitive types**: `byte`, `short`, `int`, `long`, `float`, `double`, `boolean`, `char`. Computers set aside exactly enough bits to hold a thing of a certain type when you declare a variable of that type. For example, declaring an int sets aside a box of 32 bit, and a double sets aside a box of 64 bits.

* Java creates an internal table that maps each variable name to a location.
* Java does not write anything into the boxes, and does not allow access to an uninitialiized variable.

## Reference Types

Every type not included in the primitive types is a **reference type**, such as `Array`.

### Object Instantiation

When we instantiate an object, Java first allocates a box of bits for each instance variable of the class and fills them with a default value. The constructer usually fill them with other values.

Typically, object will have overheads in addition to the memory used by its instance variables.

### Reference Variable Declaration

When we declare a variable of any reference type:

* Java allocates a box of size 64 bits.
* These bits can be either set to Null or the address of a specific instance of that class \(returned by `new`\).

## The Golden Rule of Equals

When you write `y = x`, you are telling the Java interpreter to **copy the bits** from x into y.

Just as with primitive types, the equals sign copies th bits stored in the reference variable, which is the address of a specific instance. Moreover, passing parameters will obey the rule.

## Arrays

Arrays are also objects, which are instantiated using the `new` keyword.

* Creates a 64 bit box for storing an array address. \(Declaration\)
* Creates a new array object. \(Instantiation\)
* Puts the address of this new object into the 64 bit box. \(Assignment\)

```java
int[] x; // Declaration
new int[] {0, 1, 2, 3}; // Instantiation
int[] x = new int[] {0, 1, 2, 3}; // Declaration, Instantiation, Assignment
```

## IntLists

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

### size\(\) and iterativeSize\(\)

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

### get\(\)

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

