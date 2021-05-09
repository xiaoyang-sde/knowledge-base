# Divide and Conquer

Divide and conquer refers to a class of algorithmic techniques in which one breaks the input into several parts, solves the problem in each part recursively, and then combines the solutions to these subproblems into an overall solution.

## The Mergesort Algorithm

Mergesort sorts a given list of numbers by first dividing them into two equal halves, sorting each half separately by recursion, and then combining the results. The input has been reduced to size 2.

Let $$ T(n) $$ denote the worst-case running time on input instance of size $$ n $$.

- The algorithm takes $$ O(n) $$ time to split the inputs into two halves.
- The algorithm takes $$ T(n / 2) $$ to solve each half.
- The algorithm takes $$ O(n) $$ to combine the solutions.

Recurrence relation: $$ T(n) \leq 2T(n / 2) + cn = 2T(n / 2) + O(n) $$ for $$ n > 2 $$, $$ T(2) \leq c $$ for some constant $$ c $$.

### Solving Recurrences

- **Unrolling**: Find the running time of the first few levels and identify a patten that can be continued as the recursion expands.
- **Substituting**: Guess for a solution and substitute it into the recurrence relation to check whether it works with an argument by induction on $ n $.

#### Unrolling the Mergesort Recurrences

1. Analyzing the first few levels: The third level has four problems each of size $$ n / 4 $$, each taking time at most $$ cn/4 $$, for a total of at most $$ cn $$.
2. Identifying a pattern: The level $$ i $$ contributes a total of at most $$ 2^i (cn/2^i) $$ to the total running time.
3. Summing over all levels: The number of levels is $$ log_2 n $$. Therefore, the function $$ T(\cdot) $$ is bounded by $$ O(nlogn) $$ when $$ n > 1 $$.

#### Substituting a Solution

Suppose the relation $$ T(n) \leq cn \log_2 n $$ satisfies the recurrence relation.

- **Base step**: $$ T(2) \leq cn \log_2 n = 2c $$.
- **Inductive step**: $$ T(n) \leq 2c(n/2) \log_2 (n/2) + cn = cn[(\log_2 n) - 1] + cn = cn \log_2 n $$. Therefore, the function $$ T(\cdot) $$ is bounded by $$ O(nlogn) $$ when $$ n > 1 $$.

## The Closest Pair of Points

Given $$ n $$ points in the plane, find the pair that is closest together.

### The Algorithm

Let the set of points be $$ P = {p_1, \dots, p_n} $$, where $$ p_i $$ has coordinats $$ (x_i, y_i) $$. The Euclidan distance between two points is $$ d(p_i, p_j) $$. The algorithm holds the assumption that no two points in $$ P $$ have the same x-coordinate or the same y-coordinate.

#### The Recursion

Let $$ P' \subseteq P $$ be the input of each recursion. Let the list $$ P'_x $$ be all the points in $$ P' $$ that have been sorted by increasing x-coordinate, $$ P'_y $$ be all the points in $$ P' $$ that have been sorted by increasing y-coordinate.

For each level of recursion, $$ Q $$ is defined as the set of points in the first $$ \lceil n / 2 \rceil $$ positions of $$ P'_x $$ and $$ R $$ is defined as the set of points in the final $$ \lfloor n / 2 \rfloor $$ positions of the list $$ P_x $$.

By a single pass through $$ P'_x $$ and $$ P'_y $$, four lists could be produced: $$ Q_x $$. $$ Q_y $$, $$ R_x $$, $$ R_y $$. Suppose that the closest pairs of points in $$ Q $$ and $$ R $$ are $$ q^\*_0 $$ and $$ q^\*_1 $$, $$ r^\*_0 $$ and $$ r^\*_1 $$.

#### Combine the Solutions

Let $$ \alpha $$ be the minimum of $$ d(q^\*_0, q^\*_1) $$ and $$ d(r^\*_0, r^\*_1) $$. Let $$ L $$ be the vertical line that crosses through the rightmost point of $ Q $.

If there exists $$ q \in Q $$ and $ r \in R $$ for which $$ d(q, r) < \alpha $$, then each of $$ q $$ and $$ r $$ lies within a distance $$ \alpha $$ of $$ L $$.

Let $$ S \subseteq P' $$ be the set of points in this narrow band and $$ S_y $$ be the points in $$ S $$ that sorted with their y-coordinates. Thus, there exist $$ q \ in Q $$ and $$ r \in R $$ for which $$ d(q, r) < \alpha $$ if and only if there exists $$ s, s' \in S $$ for which $$ d(s, s') < \alpha $$.

If $$ s, s' \in S $$ for which $$ d(s, s') < \alpha $$, then $$ s $$ and $$ s' $$ are within 15 positions of each other in the sorted list $$ S_y $$. Therefore, for each point $$ s \in S_y $$, the algorithm computes its distance to each of the next 15 points in $$ S_y $$. If there's a pair of points for which $$ d(s, s') < \alpha $$, they are the closest pair of points in $$ P' $$. Otherwise, the closet pair is the minimum of $$ d(q^\*_0, q^\*_1) $$ and $$ d(r^\*_0, r^\*_1) $$.

### Analyzing the Algorithm

The algorithm correctly outputs a closest pair of points in P. Since the algorithm divides the input into left and right halves, and merges the solutions in linear time, it satisties the basic recurrence and the time complexity is $$ O(nlogn) $$.

- **Basic step**: When $$ |P| \leq 3 $$, the closest pair could be computed by brute-force.
- **Induction step**: Assume that the remainder of the algorithm correctly determines the closest pair of a given input, then the closest pair in $$ P $$ either has both elements in one of $$ Q $$ or $$ R $$, or it has one element in each.
