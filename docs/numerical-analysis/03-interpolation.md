# Interpolation

## Interpolation and the Lagrange Polynomial

### Weierstrass Approximation Theorem

Suppose $f$ is defined and continuous on $[a, b]$. For each $\epsilon > 0$, there's a polynomial $P(x)$, with the property that $|f(x) - P(x)| < \epsilon$, for all $x$ in $[a, b]$.

### Lagrange Interpolating Polynomial

The linear Lagrance interpolating polynomial through $(x_0, y_0)$ and $(x_1, y_1)$ is the same as approximating a function $f$ for which $f(x_0) = y_0$ and $f(x_1) = y_1$ by means of a first-degree polynomial interpolating.

Let $L_0 = \frac{x - x_1}{x_0 - x_1}$ and $L_1 = \frac{x - x_0}{x_1 - x_0}$. $P(x) = L_0(x) f(x_0) + L_1(x) f(x_1) = \frac{x - x_1}{x_0 - x_1} f(x_0) + \frac{x - x_0}{x_1 - x_0} f(x_1)$.

Consider the case of the at most $n$-degree polynomial passing thorough $n + 1$ points given points $(x_0, f(x_0)), (x_1, f(x_1)), \dots, (x_n, f(x_n))$. For these set of $n + 1$ points, there's a polynomial degree of at most $n$ that passes through these points.

For each $k = 0, 1, 2, \dots, n$, construct a function $L_{n, k}(x)$ where $L_{n, k}(x_i) = 0$ when $i \ne k$ and $L_{n, k}(x_k) = 0$:

$L_{n, k}(x) = \frac{(x - x_0) \dots (x - x_{k - 1}) (x - x_{k + 1}) \dots (x - x_n)}{(x_k - x_0) \dots (x_k - x_{k - 1}) (x_k - x_{k + 1}) \dots (x_k - x_n)} = \Pi_{i = 0, i \ne k}^n \frac{x - x_i}{x_k - x_i}$.

$P(x) = f(x_0)L_{n, 0}(x) + \dots + f(x_n)L_{n, n}(x) = \sum_{i = 1}{n} f(x_k) L_{n, k} (x)$, where $P(x_i) = f(x_i)$ for each $i = 0, 1, 2, \dots, n$.

Suppose $x_0, \dots x_n$ are distinct numbers in $[a, b]$ and $f \in c^{n + 1} [a, b]$. For each $x$ in $[a, b]$, a number $\epsilon(x)$ between $\min{x_0, \dots, x_n}$ and $\max{x_0, \dots, x_n}$ exists with $f(x) = P(x) + \frac{f^{(n + 1)}(\epsilon(x))}{(n + 1)!} (x - x_0) \dots (x - x_n)$.

## Neville's Method

The Neville's method derives the $n$-th degree Lagrange polynomial from the $(n - 1)$-th degree Lagrange polynomial.

Let $f$ be a function defined at $x_0, x_1, \dots, x_n$ and suppose that $m_0, m_1, \dots, m_k$ are $k$ distinct integers with $0 \le m_i \le n$ for each $i$. The Lagrange polynomial that agrees with $f(x)$ at the $k$ points $x_{m_1}, \dots x_{m_k}$ is denoted $P_{m_1, m_2, \dots, m_k}(x)$.

Let $x_i$ and $x_j$ be the two distinct numbers in the set $x_0, \dots, x_n$. $P(x) = \frac{(x - x_j)P_{0, 1, \dots, j - 1, j + 1, \dots k}(x) - (x - x_i) P_{0, 1, \dots, i - 1, i + 1, \dots k}}{x_i - x_j}$ is the $k$-th Lagrange polynomial that interpolates $f$ at the $k + 1$ points $x_0, x_1, \dots x_k$, since it satisfies these conditions:

- $P(x_i) = P_{0, 1, \dots, j - 1, j + 1, \dots k}(x_i) = f(x_i)$
- $P(x_j) = P_{0, 1, \dots, i - 1, i + 1, \dots k}(x_j) = f(x_j)$
- $P(x_r) = f(x_r)$, for all $0 \le r \le k$
