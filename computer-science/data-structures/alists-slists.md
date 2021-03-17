# ALists, Resizing, vs. SLists

### Limitation of DLists

Suppose we added `get(int i)`, which returns the ith item of the list. While we have a quite long DList, this operation will be significantly slow.

By instead, we could use Array to build a list without links.

### Random Access in Arrays

Retrieval from any position of an array is very fast, which is independent of the size of it.

### Naive AList Code

```java
public class AList {
    private int[] items;
    private int size;

    public AList() {
        items = new int[100];
        size = 0;
    }

    public void addLast(int x) {
        items[size] = x;
        size += 1;
    }

    public int getLast() {
        return items[size - 1];
    }

    public int get(int i) {
        return items[i];
    }

    public int size() {
        return size;
    }
}
```

Here are some invariants of this piece of code:

* The position of the next item to be inserted is always `size`.
* `size` is always the number of items in the AList.
* The last item in the list is always in position `size - 1`.

### Delete Operation

```java
public int removeLast() {
    int returnItem = items[size - 1];
    items[size - 1] = 0;
    size -= 1;
    return returnItem;
}
```

### Naive Resizing Arrays

The limitation of the above data structure is that the size of array is fixed.

To solve that problem, we could simply build a new array that is big enough to accomodate the new data. For example, we can imagine adding the new item as follows:

```java
public void addLast(int x) {
  if (size == items.length) {
    int[] a = new int[size + 1];
    System.arraycopy(items, 0, a, 0, size);
    items = a;
  }
  items[size] = x;
  size += 1;
}
```

The problem is that this method has terrible performance when you call `addLast` a lot of times. The time required is exponential instead of linear for SLList.

Geometric resizing is much faster: Just how much better will have to wait. \(This is how the Python list is implemented.\)

```java
public void addLast(int x) {
  if (size == items.length) {
    resize(size * 2);
  }
  items[size] = x;
  size += 1;
}
```

### Memory Performance

Our AList is almost done, but we have one major issue. Suppose we insert 1,000,000,000 items, then later remove 990,000,000 items. In this case, we'll be using only 10,000,000 of our memory boxes, leaving 99% completely unused.

To fix this issue, we can also downsize our array when it starts looking empty. Specifically, we define a "usage ratio" R which is equal to the size of the list divided by the length of the `items` array. For example, in the figure below, the usage ratio is 0.04.

### Generic Array

When creating an array of references to Item:

* `(Item []) new Object[cap];`
* Causes a compiler warning, which you should ignore.

The another change to our code is that we will delete an item by setting it to `null` instead of `0`, which could be collected by Java Garbage Collector.
