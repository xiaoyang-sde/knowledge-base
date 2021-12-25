# Subtype Polymorphism vs. HoFs

## Max Function

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

The problem is that the `Dog` object can't work with the `>` operater. To fix this problem, we may change the function slightly.

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

## compareTo

We can create an interface that guarantees that any implementing class, like `Dog`, contains a comparison method, which we'll call `compareTo`.

Let's write our own interface.

```java
public interface OurComparable {
    public int compareTo(Object o);
}
```

* Return a negative number if `this` &lt; o.
* Return 0 if `this` equals o.
* Return a positive number if `this` &gt; o.

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

Then, we could generalize the `max` function by taking in `OurComparable` objects.

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

## Comparables

Although `OurComparable` interface seems solved the issue, it's awkward to use and there's no existing classes implement `OurComparable`. The solution is that we could use the existed interface `Comparable`.

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

## Comparator

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

