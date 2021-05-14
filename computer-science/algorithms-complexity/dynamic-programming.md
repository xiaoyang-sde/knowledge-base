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

The progress measure is the number of keys in `cache`. Initially the number is 1, and it will grow to $$ n + 1 $$, since there are total `n + 1 ` unique subproblems. The running time of `mem_compute_opt` is $$ O(n) $$ if the intervals are sorted by their finishing time.

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
