# Sorting and Data Structures Conclusion

## Radix Sort vs. Comparison Sorting

Merge Sort requires `Θ(N log N)` compares.

Merge Sort’s runtime on strings of length W:
* `Θ(N log N)` if each comparison takes constant time. (Strings are all different in top character.)
* `Θ(WN log N)` if each comparison takes `Θ(W)` time. (   Strings are all equal.)

### LSD vs. Merge Sort

Treating alphabet size as constant, LSD Sort has runtime `Θ(WN)`. 

Merge Sort has runtime between `Θ(N log N)` and `Θ(WN log N)`.

* Intuitively if `W < log N`, or if the Strings are similar, LSD is faster.
* If the Strings are very different, Merge Sort is faster.

### Cost Model

An alternate approach is to pick a cost model: the number of characters examined.
* Radix Sort: Calling charAt in order to count occurrences of each character.
* Merge Sort: Calling charAt in order to compare two Strings.

Suppose there are 100 strings of 1000 characters each.

Estimate the total number of characters examined by MSD Radix Sort if all strings are equal. In the worst case (all strings equal), every character is examined exactly once. 

Estimate the total number of characters examined by Merge Sort if all strings are equal. Merging N strings of 1000 characters requires `N/2 * 2000 = 1000N` examinations. Examine approximately `1000N log2 N` total characters.

* MSD radix sort will examine `~1000N` characters (For N = 100: 100,000).
* Merge sort will examine `~1000N log2(N)` characters (For N = 100: 660,000).

### Empirical Study

The cost model isn’t representative of everything that is happening.

Java’s Just-In-Time Compiler secretly optimizes your code when it runs. 

Example: Performing calculations whose results are unused.

Comparing algorithms that have the same order of growth is challenging.
* Have to perform computational experiments.
* In modern programming environments, experiments can be tricky due to optimizations like the JIT in Java.

## Sorting Summary

Three basic flavors: Comparison, Alphabet, and Radix based.

Each can be useful in different circumstances, but the important part was the analysis and the deep thought.

* Comparison based: Selection (Heapsort), Merge, Partition, Insertion.
* Alphabet based: Counting.
* Radix based: MSD, LSD.
