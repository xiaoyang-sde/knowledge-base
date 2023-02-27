# Randomized Algorithms

Randomized algorithms make decisions randomly as they process the input. In a worst- case world, an algorithm that does its own internal randomization may be able to offset certain worst-case phenomena. Problems that may not have been solvable by efficient deterministic algorithms may still be amenable to randomized algorithms.

## Median-Finding and Quicksort

### Finding the Median

Let $S$ be a set of numbers ${a_1, a_2, \dots, a_n }$. The median of $S$ is the $k^{th}$ largest element in $S$. $k = n / 2$ if $n$ is even. $k = (n + 1) / 2$ if $n$ is odd.

The median could be computed in $O(n \log n)$ by sorting the numbers. However, the time complexity could be reduced to $O(n)$ with randomized divide-and-conquer.

### Quickselect

Given a set of $n$ numbers $S$ and a number $k$ between 1 and $n$, consider the function `Select(S, k)` that returns the kth largest element in $S$. `Select(S, n/2)` and `Select(S, (n + 1/2)` could be used to find the median.

The basic structure of the algorithm is selecting an element $a \in S$ as the pivot,and form the sets $S^- = {a_j: a_j < a_i}$ and $S^+ = {a_j: a_j > a_i}$, and then determining which of $S^-$ or $S^+$ contains the k-th largest element, and iterate only on this one.

The algorithm is called recursively on strictly smaller set, thus it must terminate. Regardless of how the pivot is chosen, the algorithm returns the k-th largest element oof $S$.

#### Choose the Pivot

The good choice of pivot should produce $S^-$ and $S^+$ that are approximately equal in size.

- If the pivot is always the median, the recurrence is $T(n) \leq T(n / 2) + cn$. The solution of the recurrence is $T(n) = O(n)$.
- If the pivot is reasonably well-centered and could reduce the sets in the recursive call by a factor of $\epsilon$, the recurrence is $T(n) \leq T((1- \epsilon)n) + cn$. The solution of the recurrence is $T(n) = O(n)$.
- If the pivot is always the minimum, the recurrence is $T(n) \leq T(n - 1) + cn$. The solution of the recurrence is $T(n) = O(n^2)$.

Since a fairly large fraction of the elements are reasonably well-centered, the pivot could be choosed at random.

#### Analyzing the Algorithm

Let an element of set be central if at least a quarter of the elements are smaller than it and at least a quarter of the elements are larger than it.

If a central element is chosen as a pivot, the set will shrink by a factor of $\frac{3}{4}$. Since half of elements in the set are central, the probability that our random choice of pivot produces a central element is $\frac{1}{2}$. Therefore, the expected number of iterations before a central element is found is 2.

Since the sum $\sum_{i} (\frac{3}{4})^i$ is a geometric series that converges, the expected running time of `Select(n, k)` is $O(n)$.

### Quicksort

Quicksort behaves slightly different from quickselect: rather than looking for the median on just one side of the splitter, the algorithm sorts both sides recursively and glues the two sorted pieces together to produce the overall output.

#### Choose the Pivot

- If the pivot is always the median, the recurrence is $T(n) \leq 2T(n / 2) + cn$. The solution of the recurrence is $T(n) = O(n \log n)$.
- If the pivot is always the minimum, the recurrence is $T(n) \leq T(n - 1) + cn$. The solution of the recurrence is $T(n) = O(n^2)$.

#### Analyzing the Algorithm

The expected running time for the algorithm on a set S, excluding the time spent on recursive calls, is $O(|S|)$. The expected running time of Quicksort is $O(n \log n)$.
