# Intro, Hello World Java

### Introduction

**What is 61B about?**

* Writing codes that runs efficiently. \(Algorithms and data structures.\)
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

```text
$ javac HelloWorld.java
$ java HelloWorld
Hello World!
```

#### Defining Functions

Since all Java code is part of a class, we must define functions so that they belong to some class. Functions that are part of a class are commonly called "methods".

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
* Comments \(use `//` for line comments and  `/*` and `*/` for block comments\)
* Avoidance of repetitive code
* Descriptive naming \(variables, functions, classes\)

Moreover, we could use [Javadoc](https://en.wikipedia.org/wiki/Javadoc) to automatically generate documents.
