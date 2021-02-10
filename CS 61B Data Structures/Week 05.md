# CS 61B Week 05

## Asymptotics1

### Writing Efficient Programs

In the past few weeks, our focus is on saving our time: Java syntax, debugging tools, and other stuffs. However, in this week, we will focus on designing efficient programs. 

Let's take a typical question as an example: Determine if a sorted array contains any duplicates.
Given sorted array `A`, are there indices `i` and `j` where `A[i] = A[j]`?

Silly algorithm: Consider every possible pair, returning true if any match.
Are (-3, -1) the same? Are (-3, 2) the same? ...

Better algorithm: For each number A[i], look at A[i+1], and return true the first time you see a match. If you run out of items, return false.

### Intuitive Runtime Characterizations

Our goal is to somehow characterize the runtimes of the functions below.

Characterization should be simple and mathematically rigorous.

Characterization should demonstrate superiority of `dup2` over `dup1`.

```java
public static boolean dup1(int[] A) {
  for (int i = 0; i < A.length; i += 1) {
    for (int j = i + 1; j < A.length; j += 1) {
      if (A[i] == A[j]) {
         return true;
      }
    }
  }
  return false;
}

public static boolean dup2(int[] A) {
  for (int i = 0; i < A.length - 1; i += 1) {
    if (A[i] == A[i + 1]) { 
      return true; 
    }
  }
  return false;
}
```

#### Technique 1

Measure in Seconds using the following tools:

*  Physical stopwatch.
* Unix has a built in time command that measures execution time.
* Princeton Standard library has a Stopwatch class.

Advantage: Easy to measure and interpret.

Disadvantage: Result varies with machine and compilers.

#### Technique 2A

Count possible operations for an array of size `N = 10,000`.

For example, the `<` operates for 2 to 50,015,001 times.

Advantage: Machine independent. Input dependence captured in model. 

Disadvantage: Tedious to compute. Array size was arbitrary. Doesn’t tell you actual time.

#### Technique 2B

Count possible operations in terms of input array size N.

For example, the `<` operater in `dup1` runs for 2 to (N^2+3N+2)/2 times.

Advantage: Machine independent. Input dependence captured in model. Tells you how algorithm scales.

Disadvantage: Even more tedious to compute. Doesn’t tell you actual time.

### Comparing Algorithms

Although we have these techniques, we have to compare the efficiency of two algorithms: `dup1` and `dup2`, which cost 2 to (N^2+3N+2)/2 and 0 to N.

Since parabolas grow faster than straight lines, `dup2` is better.

In most case, we only consider asymptotic behavior (what happens for very large N), which means we will ignore the case that for small datasets, `dup1` might faster than `dup2`.

#### Simplified Solution

* Consider only the worst case where the difference of efficience occurs.
* Restrict attention to one operation.
* Eliminate low order terms. (N^2+3N+2)/2 -> (N^2)/2
* Eliminate multiplicative constants. (N^2)/2 -> N^2

Through the four steps, the order of growth of `dup1` is N^2, while that of `dup2` is N.

#### Simplified Analysis Process

Rather than building an entire table, we could:

* Choose representative operation to count. (cost model)
* Figure out the order of growth of that function by making exact count or using intuition and inspection.

### Big Theta

Big Theta represents the order of growth of a function.

### Big O

Whereas Big Theta means "equals", Big O means "less than or equal". (The order of growth of a function is less than or equal to Big O)
