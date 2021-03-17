# Exceptions, Iterators, Object Methods

## Lists

Java provides us a built-in `List` interface and several implementations.

```java
java.util.List<Integer> L = new java.util.ArrayList<>();
```

We could import the packages to make the code more simple.

```java
import java.util.List;
import java.util.ArrayList;

public class Example {
    public static void main(String[] args) {
        List<Integer> L = new ArrayList<>();
        L.add(5);
        L.add(10);
        System.out.println(L);
    }
}
```

### Sets

Sets are a collection of unique elements, which means that you can only have one copy of each element. There is also no sense of order.

```java
import java.util.Set;
import java.util.HashSet;

Set<String> s = new HashSet<>();
s.add("Tokyo");
s.add("Lagos");
System.out.println(S.contains("Tokyo"));
```

### ArraySet

Our goal is to make our own set with `ArrayList` we built before, which has the following methods.

* `add(value)`: add the value to the set if not already present
* `contains(value)`: check to see if ArraySet contains the key
* `size()`: return number of values

Here's our code.

```java
public class ArraySet<T>  {
    private T[] items;
    private int size; // the next item to be added will be at position size

    public ArraySet() {
        items = (T[]) new Object[100];
        size = 0;
    }

    /* Returns true if this map contains a mapping for the specified key.
     */
    public boolean contains(T x) {
        for (int i = 0; i < size; i += 1) {
            if (items[i].equals(x)) {
                return true;
            }
        }
        return false;
    }

    /* Associates the specified value with the specified key in this map. */
    public void add(T x) {
        if (contains(x)) {
            return;
        }
        items[size] = x;
        size += 1;
    }

    /* Returns the number of key-value mappings in this map. */
    public int size() {
        return size;
    }
}
```

### Exceptions

When we add `null` to our `ArraySet`, the program will crash since we are calling `null.equals(x)` and will throw a `NullPointerException`.

However, we could manually throw an exception `IllegalArgumentException` by updating the `add` method.

```java
public void add(T x) {
    if (x == null) {
        throw new IllegalArgumentException("can't add null");
    }
    if (contains(x)) {
        return;
    }
    items[size] = x;
    size += 1;
}
```

### Iterations

We could use enhanced loop in Java's `HashSet`:

```java
Set<String> s = new HashSet<>();
s.add("Tokyo");
s.add("Lagos");
for (String city : s) {
    System.out.println(city);
}
```

The code above is equal to the following ugly implementaion.

```java
Set<String> s = new HashSet<>();
...
Iterator<String> seer = s.iterator();
while (seer.hasNext()) {
    String city = seer.next();
    ...
}
```

If we want to do the same \(ugly version of enhanced loop\) with our own `ArraySet`, we have to implement the `Iterator` interface.

```java
public interface Iterator<T> {
    boolean hasNext();
    T next();
}
```

```java
private class ArraySetIterator implements Iterator<T> {
    private int wizPos;

    public ArraySetIterator() {
        wizPos = 0;
    }

    public boolean hasNext() {
        return wizPos < size;
    }

    public T next() {
        T returnItem = items[wizPos];
        wizPos += 1;
        return returnItem;
    }
}
```

However, if we want to let our class support enhanced for loop, we have to implement `Iterable` interface.

```java
import java.util.Iterator;

public class ArraySet<T> implements Iterable<T> {
    private T[] items;
    private int size; // the next item to be added will be at position size

    public ArraySet() {
        items = (T[]) new Object[100];
        size = 0;
    }

    /* Returns true if this map contains a mapping for the specified key.
     */
    public boolean contains(T x) {
        for (int i = 0; i < size; i += 1) {
            if (items[i].equals(x)) {
                return true;
            }
        }
        return false;
    }

    /* Associates the specified value with the specified key in this map.
       Throws an IllegalArgumentException if the key is null. */
    public void add(T x) {
        if (x == null) {
            throw new IllegalArgumentException("can't add null");
        }
        if (contains(x)) {
            return;
        }
        items[size] = x;
        size += 1;
    }

    /* Returns the number of key-value mappings in this map. */
    public int size() {
        return size;
    }

    /** returns an iterator (a.k.a. seer) into ME */
    public Iterator<T> iterator() {
        return new ArraySetIterator();
    }

    private class ArraySetIterator implements Iterator<T> {
        private int wizPos;

        public ArraySetIterator() {
            wizPos = 0;
        }

        public boolean hasNext() {
            return wizPos < size;
        }

        public T next() {
            T returnItem = items[wizPos];
            wizPos += 1;
            return returnItem;
        }
    }

    public static void main(String[] args) {
        ArraySet<Integer> aset = new ArraySet<>();
        aset.add(5);
        aset.add(23);
        aset.add(42);

        //iteration
        for (int i : aset) {
            System.out.println(i);
        }
    }

}
```

### toString

If we want to print out the whole class, we have to override `toString` method, or we will only get the name and the address of the object.

```java
public String toString() {
    String returnString = "{";
    for (int i = 0; i < size; i += 1) {
        returnString += keys[i];
        returnString += ", ";
    }
    returnString += "}";
    return returnString;
}
```

Here's an enhanced version utilizing the `StringBuilder` which is more efficient.

```java
public String toString() {
    StringBuilder returnSB = new StringBuilder("{");
    for (int i = 0; i < size - 1; i += 1) {
        returnSB.append(items[i].toString());
        returnSB.append(", ");
    }
    returnSB.append(items[size - 1]);
    returnSB.append("}");
    return returnSB.toString();
}
```

### equals

In order to check whether a given object is equal to another object, we have to implement the `equals` method, since `==` will only compare the memory address.

```java
public boolean equals(Object other) {
    if (this == other) {
        return true;
    }
    if (other == null) {
        return false;
    }
    if (other.getClass() != this.getClass()) {
        return false;
    }
    ArraySet<T> o = (ArraySet<T>) other;
    if (o.size() != this.size()) {
        return false;
    }
    for (T item : this) {
        if (!o.contains(item)) {
            return false;
        }
    }
    return true;
}
```

