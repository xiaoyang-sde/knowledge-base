# Radix Sorts

## Counting Sort

Sorting requires `Ω(N log N)` compares in the worst case. 

However, there are different algorithms that could avoid comparisons.

### Sleep Sort

For each integer x in array A, start a new program that:
* Sleeps for x seconds.
* Prints x.

### Counting Sort

General idea:
*   Create a new array.
*   Copy item with key i into ith entry of new array.
The algorithm could sort N items in `Θ(N)` worst case time.

#### Complex Cases

Alphabet case: Keys belong to a finite ordered alphabet.
* Count number of occurrences of each item.
* Iterate through list, using count array to decide where to put everything.

#### Performance

For sorting an array of the 100 largest cities by population, Quicksort is better than Counting Sort since Counting Sort requires building an array of size 37,832,892 (population of Tokyo).

Runtime for counting sort on N keys with alphabet of size R: `Θ(N + R)`.

* Create an array of size R to store counts: `Θ(R)`.
* Counting number of each item: `Θ(N)`.
* Calculating target positions of each item: `Θ(R)`.
* Creating an array of size N to store ordered data: `Θ(N)`.
* Copying items from original array to ordered array: Do N times:
* * Check target position: `Θ(1)`.
* * Update target position: `Θ(1)`.
* Copying items from ordered array back to original array: `Θ(N)`.

Memory usage: `Θ(N + R)`. (N for ordered array, while R for counts and starting points.)
If `N ≥ R`, then the algorithm will have a reasonable performance.

## LSD Radix Sort

Not all keys belong to finite alphabets, such as Strings. However, Strings consist of characters from a finite alphabet.

General idea of LSD Radix Sort:

* Sort each digit independently from rightmost digit towards left.
* Pick appropriate letters to represent non-constant terms.
* When keys are of different lengths, can treat empty spaces as less than all other characters.

### Performance

Runtime of LSD Radix Sort: `Θ(WN + WR)`.
Runtime depends on length of longest key.
* N: Number of items.
* R: size of alphabet.
* W: Width of each item in # digits.

## MSD Radix Sort

Just like LSD, but sort from leftmost digit towards the right.

Sort each subproblem separately to avoid conflicts.

### Performance

* Best Case: Finish in one counting sort pass, looking only at the top digit. `Θ(N + R)`
* Worst Case: Look at every character, degenerating to LSD sort. `Θ(WN + WR)`
