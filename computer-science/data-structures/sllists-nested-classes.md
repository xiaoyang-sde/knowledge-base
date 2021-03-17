# SLLists, Nested Classes, Sentinel Nodes

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

#### addFirst\(\)

```java
/** Adds x to the front  of the list. */
public void addFirst(int x) {
    first = new IntNode(x, first);
}
```

#### getFirst\(\)

```java
/** Returns the first item in the list. */
public int getFirst() {
    return first.item;
}
```

#### addLast\(\)

```java
public void addLast(int x) {
    IntNode p = first;
    while (p.next != null) {
        p = p.next;
    }
    p.next = new IntNode(x, null);
}
```

#### size\(\)

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
* The front item \(if it exists\), is always at `sentinel.next.item`.
* The `size` variable is always the total number of items that have been added.
