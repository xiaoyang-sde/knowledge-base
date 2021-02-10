# Sorting and Algorithmic Bounds

## Theoretical Bounds on Sorting

Let the ultimate comparison sort (TUCS) be the asymptotically fastest possible comparison sorting algorithm, and let R(N) be its worst case runtime.
* Worst case run-time of TUCS, R(N) is `O(N log N)`. (Not worse than Mergesort.)
* Worst case run-time of TUCS, R(N) is `Ω(N)`. (Have to at least look at every item.)

A stronger statement of the lower bound is `Ω(N log N)`, which means that there's no sorting algorithm that could have a worst case runtime better than `Θ(N log N)`.

### The Game of Puppy, Cat, Dog

Suppose there is a puppy, a cat, and a dog, each in an opaque soundproof box labeled A, B, and C, and a scale is given to figure out which is which.

A decision tree for N = 3 could be generated with 3 levels at most. For N items, the decision tree will have at most `Ω(log(N!))` levels.

The puppy, cat, dog could reduce to sorting, since a solution to the sorting problem also provides a solution to puppy, cat, dog. Thus, any lower bound on difficulty of puppy, cat, dog must also apply to sorting, which means that it should takes `Ω(log(N!))` comparisons in the worst case.

Because `log(N!) ∈ Ω(N log N)`,  `log(N!)` grows at least as quickly as `N log N`.

Since TUCS is `Ω(lg N!)` and `lg N!` is `Ω(N log N)`, TUCS is `Ω(N log N)`.

Any comparison based sort requires at least order `N log N `comparisons in its worst case.

### Proof of log(N!) ∈ Ω(N log N)

*   `N! ≥ (N/2)N/2`.
*   Taking the log of both sides, `log(N!) ≥ log(N/2) ^ (N/2)`.
*   Bringing down the exponent, `log(N!) ≥ N/2 log(N/2)`.
*   Discarding unnecessary constants, `log(N!) ∈ Ω(N log N)`.
