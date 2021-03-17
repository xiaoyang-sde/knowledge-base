# Asymptotics II

In this section, we will discuss more difficult examples related to run-time analysis.

## Function dup1

```java
public boolean dup1(int[] A) {
    int N = A.length;
    for (int i = 0; i < N; i += 1)
    for (int j = i + 1; j < N; j += 1)
        if (A[i] == A[j])
            return true;
    return false;
}
```

The worst case is that we have to go through every entry \(the outer loop runs N times\).

The number of comparisons is: `C = 1+2+3+...+(N−3)+(N−2)+(N−1) = N(N−1)/2`

Thus, since `==` is a constant time operation, the overall runtime in the worst case is Theta\(N^2\).

### printParty

```java
public static void printParty(int N) {
   for (int i = 1; i <= N; i = i * 2) {
      for (int j = 0; j < i; j += 1) {
         System.out.println("hello");
         int ZUG = 1 + 1;
      }
   }
}
```

Let's create a visualization to find out the runtime cost of the function above.

![printParty](https://joshhug.gitbooks.io/hug61b/content/assets/loops2_4.png)

We could conclude that `C(N) = 1 + 2 + 4 + ... + N = 2N - 1 (if N is a power of 2)`, which is in the linear family.

### Recursion \(f3\)

```java
public static int f3(int n) {
   if (n <= 1)
      return 1;
   return f3(n-1) + f3(n-1);
}
```

Here's a visualization of the function above.

![recursion](https://joshhug.gitbooks.io/hug61b/content/assets/asymptotics2_tree.png)

We could conclude that the runtime cost of the function is `C(1)=1 C(2) = 1 + 2C(2)=1+2 C(3) = 1 + 2 + 4C(3)=1+2+4 C(N) = 1 + 2 + 4 + ... +C(N) = 1+2+4+...+ ??? = 2^(N−1)`, which is in the `2^N` family.

### Binary Search

Binary search is a practical way to find an item in a sorted list. To do a binary search, we start in the middle of the list, and check if that's our desired element.

* Start in the middle of the list and check if that's our desired element.
* If the desired element is larger, eliminate the first half of the list and return to step one.
* If the desired element is smaller, eliminate the second half of the list and return to step one.

```java
static int binarySearch(String[] sorted, String x, int lo, int hi)
    if (lo > hi) return -1;
    int m = lo + (hi - lo) / 2;
    int cmp = x.compareTo(sorted[m]);
    if (cmp < 0) return binarySearch(sorted, x, lo, m - 1);
    else if (cmp > 0) return binarySearch(sorted, x, m + 1, hi);
    else return m;
}
```

Intuitively, the runtime cost of binary search is `log_2 N`, since we can figure out that the count seems to increase by one only when `N` hits a power of 2.

We can be even more precise: `C(N) = ⌊log_2 (N)⌋+1`. Because `⌊f(N)⌋ = Θ(f(N))`, `Θ(⌊log_2 (N)⌋) = Θ(log N)`.

Log time is faster than linear time and even as better as constant time, which makes binary search an efficient algorithm.

### Merge Sort

Selection sort works off two basic steps:

* Find the smallest item among the unsorted items, move it to the front, and fix it in place.
* Sort the remaining unsorted items using selection sort.

If we analyze selection sort, we see that it's `Theta(N^2)`. To improve it, we could divide the array into two halves, sort them, and merge them, in which merging only costs `Theta(N)`.

This is the essence of merge sort:

* If the list is size 1, return. Otherwise:
* Mergesort the left half
* Mergesort the right half
* Merge the results

Mergesort has worst case runtime: `C = Theta(NlogN)`, since it has `logN` levels:

* The top level takes ~N.
* Next level takes ~N/2 + ~N/2 = ~N.
* One more level down: ~N/4 + ~N/4 + ~N/4 + ~N/4 = ~N.

`Theta(NlogN)` is better than `Theta(N^2)`, so that merge sort is better than selection sort.
