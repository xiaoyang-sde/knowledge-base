# Extends, Casting, Higher Order Functions

Although interfaces provide us a way to define a hierarchical relationship, the `extends` key word let us define a hierarchical relationship between classes.

If we want to define a new class which have all methods in the `SLList` but new ones such as `RotatingRight`.

```java
public class RotatingSLList<Item> extends SLList<Item>
```

With `extends` key word, the subclass withh inhert all these components:

* All instance and static variables
* All methods
* All nested classes

### VengefulSLList

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
