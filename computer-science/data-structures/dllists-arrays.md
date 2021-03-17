# DLLists, Arrays

Although SLList provides a efficient way for us to manipulate the list, it's really slow when we want to add an item to the end of the list.

### addLast\(\)

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

* Array boxes are numbered and accessed using \[\] notation, and class boxes are named and accessed using dot notation.
* Array boxes must all be the same type. Class boxes can be different types.

