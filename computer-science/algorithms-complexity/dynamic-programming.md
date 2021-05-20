# Dynamic Programming

The basic idea of dynamic programming is the opposite of the greedy strategy: one implicitly explores the space of all possible solutions by carefully decomposing things into a series of subproblems, and then building up correct solutions to larger and larger subproblems.

## Weighted Interval Scheduling

The Interval Scheduling Problem is the special case in which all weights are equal to 1. Therefore, most greedy algorithms will not solve this problem optimally.

There are $$ n $$ requests labeled $$ 1, ... n $$, with each request $$ i $$ has a start time $$ s_i $$, a finish time $$ f_i $$, and a value $$ v_i $$. The goal is to select a subset of mutually compatible intervals to maximize the sum of the values of the selected intervals.

Suppose that the requests are sorted in order of the finishing time. $$ p(j) $$ is defined as the largest index $$ i < j $$ that intervals $$ i $$ and $$ j $$ are disjoint.

Let $$ O $$ be the optimal solution, either interval $$ n $$ belongs to $$ O $$, or it doesn't. If $$ n $$ belongs to $$ O $$, no interval between $$ p(n) $$ and $$ n $$ belong to $$ O $$.

Let $$ O_j $$ denotes the optimal solution of smaller problems of the form $$ {1, 2, \dots, j} $$, and $$ OPT(j) $$ denotes the total weight of the solution. Since $$ j $$ could either belongs to $$ O $$ or not, $$ OPT(j) = max(OPT(p(j)) + v_j, OPT(j - 1)) $$.

```py
def compute_opt(j):
  if j == 0:
    return 0
  return max(
    values[j] + compute_opt(p(j)),
    compute_opt(j - 1)
  )
```

However, the recursion would take exponential time to run in the worst case, since the recursion tree widens quickly due to the recursive branching.

### Memoizing the Recursion

In fact, the recursive algorithm only solves $$ n + 1 $$ different subproblems. To eliminate the redundancy, the result of each subproblem could be cached, which is called memoization.

```py
cache = {
  0: 0
}

def mem_compute_opt(j):
  if j in cache:
    return cache[j]

  cache[j] = max(
    values[j] + compute_opt(p(j)),
    compute_opt(j - 1)
  )
  return cache[j]
```

The progress measure is the number of keys in `cache`. Initially the number is 1, and it will grow to $$ n + 1 $$, since there are total `n + 1` unique subproblems. The running time of `mem_compute_opt` is $$ O(n) $$ if the intervals are sorted by their finishing time.

### Find Solution

```py
def find_solution(j):
  if j == 0:
    return []
  if value[j] + cache[p(j)] > cache[j - 1]:
    return find_solution(p(j)) + [j]
  return find_solution(j - 1)
```

Since the recursive calls only on strictly smaller values, it makes a total of $$ O(n) $$ recursive calls. Therefore, the function returns an optimal solution of the problem in $$ O(n) $$.

## Principle of Dynamic Programming

### Designing the Algorithm

```py
def iterative_compute_opt:
  cache[0] = 0
  for j in range(1, n + 1):
    cache[j] = max(
      value[j] + cache[p(j)],
      cache[j - 1]
    )
```

The running time of the iterative algorithm is $$ O(n) $$, since each iteration takes constant time.

### Basic Outline of Dynamic Programming

To develop an algorithm based on dynamic programming, the collection of subproblems derived from the original problem has to satisfies a few basic properties.

1. The number is subproblems is polynomial.
2. The solution can be easily computed from the solutions to the subproblems.
3. There's a natural ordering on subproblems from "smallest" to "largest" with a neasy-to-compute recurrence that could determine the solution of a subproblem from smaller subproblems.

## RNA Secondary Structure

Double-stranded DNA is zipped together by complementary base-pairing. Each base is drawn from the set $$ {A, C, G, T} $$, in which $$ A-T $$ and $$ C-G $$ forms the pairings.

Single-stranded RNA loops back and form base pairs with itself, which is the secondary structure of RNA. A single-stranded RNA molecule can be viewed as a sequence of $$ n $$ symbols drawn from the set $$ { A, C, G, U } $$.

The secondary structure on $$ B = b_1, b_2, \dots, b_n $$ is a set of pairs $$ S = {(i, j)} $$, where $$ i, j \in { 1, 2, \dots, n } $$. The secondary structure is formed with the optimum total free energy, or maximum number of base pairs.

- $$ S $$ is a matching.

- If $$ (i, j) \in S $$, then $$ i < j - 4 $$.

- The elements of any pair consist of either $$ { A, U } $$ or $$ { C, G } $$.

- If $$ (i, j) $$ and $$ (k, l) $$ are two pairs in $$ S $$, then we can't have $$ i < k < j < l $$.

### First Attempt at Dynamic Programming

Let $$ OPT(j) $$ be the maximum number of base pairs in a structure on $$ b_1, b_2, \dots, b_j $$. $$ OPT(j) = 0 $$ for $$ j \leq 5 $$.

- $$ j $$ is not involved in a pair: $$ OPT(j) = OPT(j - 1) $$
- $$ j $$ pairs with $$ t $$ for some $$ t< j - 4 $$: $$ OPT(j) = OPT(t - 1) + $$ the optimal solution between $$ t + 1 $$ and $$ j - 1 $$.

Since there's no way to represent the optimal solution between $$ t + 1 $$ and $$ j - 1 $$, a seperate variable is introduced.

### Dynamic Programming over Intervals

Let $$ OPT(i, j) $$ denote the maximum number of base pairs in a structure on $$ b_i, b_{i + 1}, \dots, b_j $$. $$ OPT(i, j) = 0 $$ for $$ i \geq j - 4 $$.

- $$ j $$ is not involved in a pair: $$ OPT(i, j) = OPT(i, j - 1) $$
- $$ j $$ pairs with $$ t $$ for some $$ t < j - 4 $$: $$ OPT(i, j) = OPT(i, t - 1) + OPT(t + 1, j - 1) + 1 $$.

Therefore, $$ OPT(i, j) = max(OPT(i, j - 1), max(OPT(i, t - 1) + OPT(t + 1, j - 1) + 1)) $$ for each $$ t $$ such that $$ b_t $$ and $$ b_j $$ are allowable base pair.

There are $$ O(n ^ 2) $$ subproblems to solve, and evaluating the recurrence takes time $$ O(n) $$. Therefore, the total running time is $$ O(n ^ 3) $$.

## Sequence Alignment

Let $$ X $$ and $$ Y $$ be two strings, where $$ X $$ consists of the sequence of symbols $$ x_1, x_2, \dots, x_m $$ and $$ Y $$ consists of the sequence of symbols $$ y_1, y_2, \dots, y_n $$. A matching $$ M $$ of these two sets is an alignment if there are no crossing pairs. If $$ (i, j), (i', j') \in M $$ and $$ i < i' $$, then $$ j < j' $$.

The definition of similarity is based on finding the optimal alignment between the two strings. The process of minimizing the costs is referred to as sequence alignment.

- There's a parameter $ \epsilon > 0 $ that defines a gap penalty. For each position that is not matched (a gap), it incurs a cost of $$ \epsilon $$.

- For each pair of letters $$ p, q $$, there's a mismatch cost of $$ a_{pq} $$ for lining up $$ p $$ with $$ q $$. For each $$ (i, j) \in M $$, it incurs the cost $$ a_{x_i, y_j} for lining up $$ x_i $$ with $$ y_j $$. $$ a_pp = 0$$ for each letter $$ p $$.

- The cost of $$ M $$ or the similarity between $$ X $$ and $$ Y $$ is the sum of its gap and mismatch costs.

### Designing the Algorithm

In the optimal alignment $$ M $$, either $$ (m, n) \in M $$ or $$ (m, n) \notin M $$. ($$ m, n $$ are the last symbols in the two strings.)

Let $$ M $$ be any alignment of two strings. If $$ (m, n) \notin M $$, then either the $$ m^{th} $$ position of $$ X $$ or the $$ n^{th} $$ position of $$ Y $$ is not matched in $$ M $$.

Therefore, in an optimal alignment, at least one of the following is true:

- $$ (m, n) \in M $$
- The $$ m^{th} $$ position of $$ X $$ is not matched
- The $$ n^{th} $$ position of $$ Y $$ is not matched

Let $$ OPT(i, j) $$ denote the minimum cost of an alignment between two strings.

- Case 1: Pay $$ a_{x_m, y_n} $$, and $$ OPT(m, n) = a_{x_m, y_n} + OPT(m - 1, n - 1) $$
- Case 2: Pay $$ \epsilon $$, $$ OPT(m, n) = \epsilon + OPT(m = 1, n) $$
- Case 3: Pay $$ \epsilon $$, $$ OPT(m, n) = \epsilon + OPT(m, n - 1) $$

Therefore, the minimum alignment costs satisfy the recurrence: $$ OPT(i, j) = min(a_{x_m, y_n} + OPT(m - 1, n - 1), \epsilon + OPT(m - 1, n), \epsilon + OPT(m, n - 1)) $$.

### Analyzing the Algorithm

The running time is $$ O(mn) $$, since there are $$ mn $$ subproblems, and it takes constant time to solve each. The algorithm takes $$ O(mn) $$ space, which is the size of the array.

## Sequence Alignment in Linear Space

The dynamic programming algorithm to the sequence alignment problem takes $$ O(mn) $$ space, which is not suitable for computation of large strings. With a nice application of divide-and-conquer idea, the space complexity could be reduced to $$ O(mn) $$.

### Designing the Algorithm

If we only care about the value of the optimal alignment, it's easy to reduce the array to $$ 2 \times n $$, since computing the result of each subproblem only requires the result in the current and previous row. Therefore, it costs $$ O(mn) $$ time and $$ O(n) $$ space.

To construct the alignment, we could use a backward formulation of the dynamic programming algorithm. Let $$ g(i, j) $$ denotes the length of the shortest path from $$ (i, j) $$ to $$ (m, n) $$, and starts with $$ g(m, n) = 0 $$.

For $$ i < m $$ and $$ j < n $$, we have $$ g(i, j) = min(a_{x+1, y+1} + g(i + 1, j + 1), \epsilon + g(i, j + 1), \epsilon + g(i + 1, j)). The space complexity backward formulation could also be reduced to $$ O(n) $$ by using an array of $$ 2 \times n $$.

The length of the shortest corner-to-corner path in $$ G_{xy} $$ that passes through $$ (i, j) $$ is $$ f(i, j) + g(i, j) $$.

Let $$ k $$ be any number in $$ {0, \dots, n} $$, and $$ q $$ be an index that minimizes the quantity $$ f(q, k) + g(q, k) $$. There's a corner-to-corner path of minimum length that passes through $$ (q, k) $$.

The algorithm divides $$ G_{XY} $$ along its center column and compute the value of $$ f(i, n / 2) $$ and $$ g(i, n / 2) $$ for each value of $$ i $$. Then the algorithm could determine the minimum value of $$ f(i, n / 2) + g(i, n / 2) $$. Therefore, there's a shortest corner-to-corner path passing the node $$ (i, n / 2) $$. Thus, the algorithm could search again between $$ (0, 0) $$ and (i, n / 2) $$ and between $$ (i, n / 2) $$ and $$ (m, n) $$.

### Analyzing the Algorithm

The algorithm maintains an array with at most $$ m + n $$ entires, which holds nodes on the shortest corner-to-corner path as they are discovered. Therefore, the space complexity is $$ O(m + n) $$.

Let $$ T(m, n) $$ denote the maximum running time of the algorithm on strings of length $$ m $$ and $$ n $$.

- $$ T(m, n) \leq cmn + T(q, n / 2) + T(m - q, n / 2) $$
- $$ T(m, 2) \leq cm $$
- $$ T(2, n) \leq cn $$

Suppose that $$ m = n $$ and $$ p $$ is exactly in the middle, we could write $$ T(\dot) $$ in terms of the single variable $$ n $$, and let $$ q = n / 2 $$. Therefore, $$ T(n) \leq 2T(n / 2) + cn^2 $$, which implies $$ T(n) = O(n ^ 2) $$. Therefore, the time complexity is $$ O(mn) $$.
