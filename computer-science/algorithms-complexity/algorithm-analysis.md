# Basics of Algorithm Analysis

## Computational Tractability

1. An algorithm is efficient if, when implemented, it runs quickly on real input instances.
2. An algorithm is efficient if it achieves qualittatively better worst-case peformance, at an analytical level, than brute-force search.
3. An algorithm is efficient if it has a polynomial running time. The running time is polynomial if it's bounded by $$ cN^{d} $$ primitive computational steps.

## Asymptotic Order of Growth

The computational tractability is based on bounding an algorithm's worst-case running time on inputs of size n grows at a rate that is at most proportional to $$ f(n) $$.

### Asymptotic Upper Bounds

Let $$ T(n) $$ be a function of the worst-case running time of a certain algorithm on an input size of n.

Given another function $$ f(n) $$, we say that $$ T(n) $$ is $$ O(f(n)) $$ if $$ T(n) $$ is bounded above by a constant multiple of $$ f(n) $$. ($$ T(n) \leq c \cdot f(n) $$)

Example: $$ T(n) = pn^{2} + qn + r \leq pn^{2} + qn^{2} + rn^{2} = (p + q + r)n^{2} = c \cdot n^{2} $$, thus $$ T(n) $$ is $$ O(n^{2}) $$ or $$ O(n^{k}), k \geq 2 $$.

### Asymptotic Lower Bounds

Let $$ T(n) $$ be a function of the worst-case running time of a certain algorithm on an input size of n.

Given another function $$ f(n) $$, we say that $$ T(n) $$ is $$ \Omega (f(n)) $$ if $$ T(n) $$ is bounded below by a constant multiple of $$ f(n) $$. ($$ T(n) \geq \epsilon \cdot f(n) $$)

Example: $$ T(n) = pn^{2} + qn + r \geq pn^{2} = \epsilon \cdot n^{2} $$, thus $$ T(n) $$ is $$ \Omega (n^{2}) $$ or $$ \Omega(n^{k}), k \leq 2 $$.

### Asynmptotic Tight Bounds

If we can show that a running time $$ T(n) $$ is both $$ O(f(n)) $$ and $$ \Omega (f(n)) $$, then $$ T(n) $$ grows exactly like $$ f(n) $$ to within a constant factor. Therefore, we could say that $$ T(n) $$ is $$ \Theta (f(n)) $$.

Example: $$ T(n) $$ is $$ \Omega (n^{2}) $$ and $$ O(n^{2}) $$, thus $$ T(n) $$ is $$ \Theta (n^{2}) $$.

### Properties of Asynmptotic Growth Rates

#### Transitivity

- Upper bounds: $$ f = O(g), g = O(h), f = O(h) $$
- Lower bounds: $$ f = \Omega (g), g = \Omega (h), f = \Omega (h) $$
- Tight bounds: $$ f = \Theta (g), g = \Theta (h), f = \Theta (h) $$

#### Sum of Functions

- Suppose that f and g are two functions and for some other function h, we have $$ f = O(h) $$ and $$ g = O(h) $$. Then $$ f + g = O(h) $$.
- Let k be a fixed constant, and let $$ f_{1}, f_{2}, ..., f_{k} $$ and h be functions such that $$ f_{i} = O(h) $$. Then $$ f_{1} + f_{2} + ... + f_{k} = O(h) $$.
- Suppose that f and g are two functions such that $$ g = O(f) $$. Then $$ f + g = \Theta(f) $$. f is an asymptotically tight bound for the combined function.

#### Limit

Let f and g be two functions that $$ lim_{n \to \infty} \frac{f(n)}{g(n)} $$ exists.

- If the limit is equal to 0. Then $$ f(n) = O(g(n)), f(n) \neq \Theta (g(n)) $$.
- If the limit is equal to some number c > 0. Then $$ f(n) = \Theta (g(n)) $$.
- If the limit is equal to infinity. Then $$ f(n) = \Omega (g(n)), f(n) \neq \Theta (g(n)) $$.

### Asymptotic Bounds for Common Functions

- **Polynomial**: Let f be a polynomial of degree d, in which the coefficient $$ a_{d} $$ is positive. Then $$ f = \Theta (n^{d}) $$.
- **Logarithms**: For every b > 1 and every x > 0, $$ log_{b}n = O(n ^ {x}) $$.
- **Exponentials**: For every r > 1 and every d > 0, $$ n^{d} = O(r^{n}) $$.

## Implementing the Stable Matching Algorithm

The algorithm terminates in at most $$ n^{2} $$ iterations. If we implement each iteration in constant time, the upper bound of this algorithm is $$ n^{2} $$.

- `ManPerf[m, i]` array: The $$ i^{th} $$ woman on man m's preference lists.
- `WomanPerf[w, i]` array: The $$ i^{th} $$ man on woman w's preference lists.
- `FreeMan` list: The linked list of free men.
- `Next[m]` array: The array that indicates for each man m the position of the next woman he will propose to.
- `Current[w]` array: The array that indicates for each woman her current partner. The default value is `null`.
- `Ranking[w, m]` array: The array that contains the rank of man m in the sorted order of w' preferences.

1. Identify a free man: Take the first man from `FreeMan`.
2. Identify the highest-ranking woman to whom the man m has not yet proposed: Find the woman with `Next[m]`.
3. Decide if woman w is engaged and who is her current partner: Find the partner with `Current[w]`.
4. Decide which of m or m' is preferred by w in constant time: Compare `Ranking[w, m]` and `Ranking[w, m']`.
