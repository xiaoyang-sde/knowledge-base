# CS 61B Week 01

## 1.1 Intro, Hello World Java

### Introduction

**What is 61B about?**

* Writing codes that runs efficiently. (Algorithms and data structures.)
* Writing code efficiently. 

### First Java Program

#### Hello World

```java
public class HelloWorld {
    public static void main(String[] args) {
        System.out.println("Hello World!");
    }
}
```

* All codes must be in a class.
* Curly braces and semi-colons.
* Variables have declared types and must be declared before use.
* Functions must have a return type or declared as void function.
* The compiler ensures type consistency.

#### Compilation

The common way to run a Java program is to run it through two programs: `javac` and `java`.

```
$ javac HelloWorld.java
$ java HelloWorld
Hello World!
```

#### Defining Functions

Since all Java code is part of a class, we must define functions so that they belong to some class. 
Functions that are part of a class are commonly called "methods". 

For instance, here's a piece of code in Java which defining a function that could compare two integers and return the larger one.

```java
public class LargerDemo {
    public static int larger(int x, int y) {
        if (x > y) {
            return x;
        }
        return y;
    }

    public static void main(String[] args) {
        System.out.println(larger(8, 10));
    }
}
```

#### Code Style

In this course, we will aim to make our code readable. Here are some tips to achieve that goal:
* Consistent style
* Comments (use `//` for line comments and  `/*` and `*/` for block comments)
* Avoidance of repetitive code
* Descriptive naming (variables, functions, classes)

Moreover, we could use [Javadoc](https://en.wikipedia.org/wiki/Javadoc) to automatically generate documents.

## 1.2 Defining and Using Classes

### Defining Classes

* Every method is associated with some class.
* To run a class, we must define a main method.

```java
public class Dog {
    public static void makeNoise() {
        System.out.println("Bark!");
    }
}
```

The code above can't be run directly because there's no main method.

```java
public class DogLauncher {
    public static void main(String[] args) {
        Dog.makeNoise();
    }
}
```

However, the method could be called from another class. 

```
$ java DogLauncher
Bark!
```

### Object Instantiation

* Classes can contain not just methods, but also data, such as the `size` of each dog.
* Classes can be instantiated as objects. For example, we will create a single Dog class, and then create instances of this Dog.

Here's a Dog class which provdes a blueprint of Dog objects.

```java
public class Dog {
    // Instance variable
    public int weightInPounds; 

    // Constructor
    public Dog(int startingWeight) { 
        weightInPounds = startingWeight;
    }

    // Non-static method (Instance method)
    public void makeNoise() {
        if (weightInPounds < 10) {
            System.out.println("yipyipyip!");
        } else if (weightInPounds < 30) {
            System.out.println("bark. bark.");
        } else {
            System.out.println("woof!");
        }
    }
}
```

```java 
public class DogLauncher {
    public static void main(String[] args) {
        Dog smallDog; // Declaration
        new Dog(20); // Instantiation
        smallDog = new Dog(5); // Instantiation and Assignment
        Dog hugeDog = new Dog(150); // Declaration. Instantiation, and Assignment
        smallDog.makeNoise(); // Invocaton
        hugeDog.makeNoise(); // The dot notation 
    }
}
```

### Array of Objects

To create an array of objects:
* First use the new keyword to create the array.
* Then use new again for each object that you want to put in the array.

```java
Dog[] dogs = new Dog[2];
dogs[0] = new Dog(8);
dogs[1] = new Dog(20);
dogs[0].makeNoise();
```

### Staitc vs. Non-static

Key difference between static and non-static methods:
* Static method are invoked using the class name.
* Instance methods are invoked using an instance name.
* Static method can't access instance variables.
, but are more simple.

Sometimes, a class may contain both static and non-static methods.

```java
public static Dog maxDog(Dog d1, Dog d2) {
	if (d1.weightInPounds > d2.weightInPounds) {
   		return d1;
	}
	return d2;
}

public Dog maxDog(Dog d2) {
    if (this.weightInPounds > d2.weightInPounds) {
        return this;
    }
    return d2;
}
```

We could declare static variables which are properties shared by all instances of the class.

```java 
public static String binomen = "Cans famliiaris";
```

* A variable or method defined in a class is also called a **member** of that class. 
* Static members are accessed using **class name**.
* Non-static members cannot be invoked using class name.
* Static methods must access instance variables via **a specific instance**.

### public static void main(String[] args)

* `public`: So far, all of our methods start with this keyword.
* `static`: It is a static method, not associated with any particular instance.
* `void`: It has no return type.
* `main`: This is the name of the method.
* `String[] args`: This is a parameter that is passed to the main method.

For example, this program prints out the first command line argument.

```java
public class ArgsDemo {
    public static void main(String[] args) {
        System.out.println(args[0]);
    }
}
```

```
$ java ArgsDemo these are command line arguments
these
```

### Using Libraries

In CS 61B, we will use libraries include:
* The built-in Java libraries such as Math, String, Integer, List, Map.
* The Princeton standard library such as StdDraw, StdAudio, In.