# Inheritance, Implements

## Method Overloading

Suppose we have the method below, which will return the longest string in a `SLList`.

```java
public static String longest(SLList<String> list) {
    int maxDex = 0;
    for (int i = 0; i < list.size(); i += 1) {
        String longestString = list.get(maxDex);
        String thisString = list.get(i);
        if (thisString.length() > longestString.length()) {
            maxDex = i;
        }
    }
    return list.get(maxDex);
}
```

However, since `SLList` and `AList` have exactly the same structure, we could write two `longest` methods to make it compatible with both classes, which is a feature called "method overloading" in Java.

```java
public static String longest(SLList<String> list)
public static String longest(AList<String> list)
```

The disadvantage of method overloading is that it makes the source code quite longer than usual and add more codes to maintain.

### Hypernyms, Hyponyms, and Interface Inheritance

In Java, in order to express the hierarchy, we need to do two things:

* Define a type for the general list hypernym -- we will choose the name List61B.
* Specify that SLList and AList are hyponyms of that type.

First, we will create a `List61B` interface, which is a contract that specifies a list of method a list must able to do:

```java
public interface List61B<Item> {
    public void addFirst(Item x);
    public void add Last(Item y);
    public Item getFirst();
    public Item getLast();
    public Item removeLast();
    public Item get(int i);
    public void insert(Item x, int position);
    public int size();
}
```

Next, we will modify the definition of both classs to make a promise that both of them implement all the method defined in the interface `List61B`.

```text
public class SLList<Item> implements List61B<Item>{...}
public class AList<Item> implements List61B<Item>{...}
```

Now we can edit our `longest` method to take in a `List61B`. Because `AList` and `SLList` share an "is-a" relationship.

### Method Overriding

Method overriding means that you implement a method as the same structure as it is defined in a interface, while overloaded methods could have different parameters. In this course, we will add `@Override` tag above each overrided methods. Although this tag is unnecessary, it's useful in debugging.

Different from the Golden Rules of Equal, if X is a subclass of Y, the memory box of X may contain Y. For instance, this piece of code will works well:

```java
public static void main(String[] args) {
    List61B<String> someList = new SLList<String>();
    someList.addFirst("elk");
}
```

### Implementation Inheritance

Typically, we can't add specific implementation to a interface. However, We could add a `default` key word for a method in a interface to allow subclasses to inhert it. Although `SLList` or `AList` does not implement the `print` method in their classes, it will still work.

```text
default public void print() {
    for (int i = 0; i < size(); i += 1) {
        System.out.print(get(i) + " ");
    }
    System.out.println();
}
```

In order to re-implement the default method in a subclass, we must use the `Override` tag, or the default one will be invoked.

#### Dynamic Method Selection

In Java, variables have two phases of types: static type and dynamic type. In the code below, `lst` has a static type of `List61B` and a dynamic type `SLList`. When Java runs a method that is overriden, it searches for the appropriate method signature in it's **dynamic type** and runs it.

```java
List61B<String> lst = new SLList<String>();
```

#### Method Selection Algorithm

Suppose we have a function `foo.bar(x1)`, where `foo` has the static type `TPrime`, and `x1` has the static type `T1`.

**Compile**

When compiling the code, compiler verifies that `TPrime` has at least one method that could handle `T1`, and will record the most **specific** one.

**Run**

When running the code, if `foo`'s dynamic type **override** the `bar` method in `TPrime`, use the overridden method. Otherwise, use the recorded method.

### Interface Inheritance vs Implementation Inheritance

* Interface inheritance \(what\): Simply tells what the subclasses should be able to do.
* Implementation inheritance \(how\): Tells the subclasses how they should behave.

