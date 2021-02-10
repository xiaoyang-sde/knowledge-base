# CS 61B Week 04

## Extends, Casting, Higher Order Functions

Although interfaces provide us a way to define a hierarchical relationship, the `extends` key word let us define a hierarchical relationship between classes.

If we want to define a new class which have all methods in the `SLList` but new ones such as `RotatingRight`.

```java
public class RotatingSLList<Item> extends SLList<Item>
```

With `extends` key word, the subclass withh inhert all these components:

* All instance and static variables
* All methods
* All nested classes

## VengefulSLList

We could build a `VengefulSLList` class to make a list that could remeber the deleted items. `super` key word could be used to call the corresponding method in the super class.

```java
public class VengefulSLList<Item> extends SLList<Item> {
    SLList<Item> deletedItems;

    public VengefulSLList() {
        deletedItems = new SLList<Item>();
    }

    @Override
    public Item removeLast() {
        Item x = super.removeLast();
        deletedItems.addLast(x);
        return x;
    }

    /** Prints deleted items. */
    public void printLostItems() {
        deletedItems.print();
    }
}
```

### Constructors

While constructors are not inherited, Java requires that all constructors must start with a call to one of its superclass's constructors. If you don't call it explicitly, Java will automatically call it for you. If we forget to specify which contructor to use, Java will call the default one without parameters.

### The Object Class

Every class in Java is a descendant of the Object class, or extends the Object class. Even classes that do not have an explicit extends in their class still implicitly extend the Object class.

### Encapsulation

* A model is a set of methods working together to perform some task or set of related tasks.
* A model is said to be encapsulated if its implementation is highly hidden: It can be accessed merely through the documented interfaces.

### Type Checking and Casting

Compilers will check types of objects based on its staitc type. For instance, the following code will result in a compile-time error since the compiler thinks that `SLList` does not have the `printLostItem` method and `vsl2` can't contain the `SLList` object.

```java
VengefulSLList<Integer> vsl = new VengefulSLList<Integer>(9);
SLList<Integer> sl = vsl;
sl.printLostItems();
VengefulSLList<Integer> vsl2 = sl;
```

#### Expressions

As we seen above, expression with `new` key word has compile-time types.

```java
SLList<Integer> sl = new VengefulSLList<Integer>();
```

Above, the compile-time type of the right-hand side of the expression is `VengefulSLList`. The compiler checks to make sure that `VengefulSLList` "is-a" `SLList`, and allows this assignment.

#### Method

The type of a method's return value is the method's compile-time type. Since the return type of `maxDog` is `Dog`, any call to `maxDog` will have compile-time type `Dog`.

```java
Poodle frank = new Poodle("Frank", 5);
Poodle frankJr = new Poodle("Frank Jr.", 15);

Dog largerDog = maxDog(frank, frankJr);
Poodle largerPoodle = maxDog(frank, frankJr); //does not compile! RHS has compile-time type Dog
```

#### Casting

We could specify the type of an expression or a method to let Java compiler ignore type check. That might be dangerous and may cause run-time errors.

```java
Poodle largerPoodle = (Poodle) maxDog(frank, frankJr); // compiles! Right hand side has compile-time type Poodle after casting
```


### High Order Functions

In Python, we could define a function that will take another function as a parameter.

```python
def tenX(x):
    return 10 * x

def do_twice(f, x):
    return f(f(x))
```

In Java, we could do so by declaring an interface.

```java
public interface IntUnaryFunction{
    int apply(int x) {}
}

public class TenX implements IntUnaryFunction{
    public int apply(int x) {
        return 10 * x;
    }

    
}
```

```java
public static int do_twice(IntUnaryFunction f, int x) {
    return f.apply(f.apply(x));
}

System.out.println(do_twice(new TenX(), 2));
```

## Subtype Polymorphism vs. HoFs

### Max Function

Suppose we will write a method `max` that will take an array of objects and return the maximum one.

```java
public static Object max(Object[] items) {
    int maxDex = 0;
    for (int i = 0; i < items.length; i += 1) {
        if (items[i] > items[maxDex]) {
            maxDex = i;
        }
    }
    return items[maxDex];
}

public static void main(String[] args) {
    Dog[] dogs = {new Dog("Elyse", 3), new Dog("Sture", 9), new Dog("Benjamin", 15)};
    Dog maxDog = (Dog) max(dogs);
    maxDog.bark();
}
```

The problem is that the `Dog` object can't work with the `>` operater.
To fix this problem, we may change the function slightly.

```java
public static Dog maxDog(Dog[] dogs) {
    if (dogs == null || dogs.length == 0) {
        return null;
    }
    Dog maxDog = dogs[0];
    for (Dog d : dogs) {
        if (d.size > maxDog.size) {
            maxDog = d;
        }
    }
    return maxDog;
}
```

However, another problem is that we couldn't generalize this function to other type of objects.

### compareTo

We can create an interface that guarantees that any implementing class, like `Dog`, contains a comparison method, which we'll call `compareTo`.

Let's write our own interface.

```java
public interface OurComparable {
    public int compareTo(Object o);
}
```

* Return a negative number if `this` < o.
* Return 0 if `this` equals o.
* Return a positive number if `this` > o.

Now, we could let our `Dog` class implements the `OurComparable` interface.

```java
public class Dog implements OurComparable {
    private String name;
    private int size;

    public Dog(String n, int s) {
        name = n;
        size = s;
    }

    public void bark() {
        System.out.println(name + " says: bark");
    }

    public int compareTo(Object o) {
        Dog uddaDog = (Dog) o;
        return this.size - uddaDog.size;
    }
}
```

Then, we could generalize the `max` function by taking in `OurComparable`  objects.

```java
public static OurComparable max(OurComparable[] items) {
    int maxDex = 0;
    for (int i = 0; i < items.length; i += 1) {
        int cmp = items[i].compareTo(items[maxDex]);
        if (cmp > 0) {
            maxDex = i;
        }
    }
    return items[maxDex];
}
```

### Comparables

Although `OurComparable` interface seems solved the issue, it's awkward to use and there's no existing classes implement `OurComparable`.
The solution is that we could use the existed interface `Comparable`.

```java
public interface Comparable<T> {
    public int compareTo(T obj);
}
```

Notice that `Comparable<T>` means that it takes a generic type. This will help us avoid having to cast an object to a specific type.

```java
public class Dog implements Comparable<Dog> {
    ...
    public int compareTo(Dog uddaDog) {
        return this.size - uddaDog.size;
    }
}
```

### Comparator

We could only implement one `compareTo` method for each class. However, if we want to add more orders of comparasion, we could implement `Comparator` interface.

```java
public interface Comparator<T> {
    int compare(T o1, T o2);
}
```
This shows that the `Comparator` interface requires that any implementing class implements the `compare` method.

To compare two dogs based on their names, we can simply defer to `String`'s already defined `compareTo` method.

```java
import java.util.Comparator;

public class Dog implements Comparable<Dog> {
    ...
    public int compareTo(Dog uddaDog) {
        return this.size - uddaDog.size;
    }

    private static class NameComparator implements Comparator<Dog> {
        public int compare(Dog a, Dog b) {
            return a.name.compareTo(b.name);
        }
    }

    public static Comparator<Dog> getNameComparator() {
        return new NameComparator();
    }
}
```

```java
Comparator<Dog> nc = Dog.getNameComparator();
```

## Exceptions, Iterators, Object Methods

### Lists

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

If we want to do the same (ugly version of enhanced loop) with our own `ArraySet`, we have to implement the `Iterator` interface.

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
