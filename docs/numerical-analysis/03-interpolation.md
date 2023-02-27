# Interpolation

## Interpolation and the Lagrange Polynomial

### Weierstrass Approximation Theorem

Suppose $f$ is defined and continuous on $[a, b]$. For each $\epsilon > 0$, there's a polynomial $P(x)$, with the property that $|f(x) - P(x)| < \epsilon$, for all $x$ in $[a, b]$.

### Lagrange Interpolating Polynomial

The linear Lagrance interpolating polynomial through $(x_0, y_0)$ and $(x_1, y_1)$ is the same as approximating a function $f$ for which $f(x_0) = y_0$ and $f(x_1) = y_1$ by means of a first-degree polynomial interpolating.

Let $L_0 = \frac{x - x_1}{x_0 - x_1}$ and $L_1 = \frac{x - x_0}{x_1 - x_0}$. $P(x) = L_0(x) f(x_0) + L_1(x) f(x_1) = \frac{x - x_1}{x_0 - x_1} f(x_0) + \frac{x - x_0}{x_1 - x_0} f(x_1)$.

Consider the case of the at most $n$-degree polynomial passing thorough $n + 1$ points given points $(x_0, f(x_0)), (x_1, f(x_1)), \dots, (x_n, f(x_n))$. For these set of $n + 1$ points, there's a polynomial degree of at most $n$ that passes through these points.

For each $k = 0, 1, 2, \dots, n$, construct a function $L_{n, k}(x)$ where $L_{n, k}(x_i) = 0$ when $i \ne k$ and $L_{n, k}(x_k) = 1$:

$L_{n, k}(x) = \frac{(x - x_0) \dots (x - x_{k - 1}) (x - x_{k + 1}) \dots (x - x_n)}{(x_k - x_0) \dots (x_k - x_{k - 1}) (x_k - x_{k + 1}) \dots (x_k - x_n)} = \Pi_{i = 0, i \ne k}^n \frac{x - x_i}{x_k - x_i}$.

$P(x) = f(x_0)L_{n, 0}(x) + \dots + f(x_n)L_{n, n}(x) = \sum_{i = 1}^{n} f(x_k) L_{n, k} (x)$, where $P(x_i) = f(x_i)$ for each $i = 0, 1, 2, \dots, n$.

Suppose $x_0, \dots x_n$ are distinct numbers in $[a, b]$ and $f \in c^{n + 1} [a, b]$. For each $x$ in $[a, b]$, a number $\epsilon(x)$ between $\min{x_0, \dots, x_n}$ and $\max{x_0, \dots, x_n}$ exists with $f(x) = P(x) + \frac{f^{(n + 1)}(\epsilon(x))}{(n + 1)!} (x - x_0) \dots (x - x_n)$.

## Neville's Method

The Neville's method derives the $n$-th degree Lagrange polynomial from the $(n - 1)$-th degree Lagrange polynomial.

Let $f$ be a function defined at $x_0, x_1, \dots, x_n$ and suppose that $m_0, m_1, \dots, m_k$ are $k$ distinct integers with $0 \le m_i \le n$ for each $i$. The Lagrange polynomial that agrees with $f(x)$ at the $k$ points $x_{m_1}, \dots x_{m_k}$ is denoted $P_{m_1, m_2, \dots, m_k}(x)$.

Let $x_i$ and $x_j$ be the two distinct numbers in the set $x_0, \dots, x_n$. $P(x) = \frac{(x - x_j)P_{0, 1, \dots, j - 1, j + 1, \dots k}(x) - (x - x_i) P_{0, 1, \dots, i - 1, i + 1, \dots k}}{x_i - x_j}$ is the $k$-th Lagrange polynomial that interpolates $f$ at the $k + 1$ points $x_0, x_1, \dots x_k$, since it satisfies these conditions:

- $P(x_i) = P_{0, 1, \dots, j - 1, j + 1, \dots k}(x_i) = f(x_i)$
- $P(x_j) = P_{0, 1, \dots, i - 1, i + 1, \dots k}(x_j) = f(x_j)$
- $P(x_r) = f(x_r)$, for all $0 \le r \le k$

## Divided Difference

Suppose that $P_n(x)$ is the $n$-th Lagrange polynomial that agrees with the function $f$ at $x_0, x_1, \dots, x_n$. Although the polynomial is unique, there are alternate algebraic representations that are useful.

The divided differences of $f$ with respect to $x_0, x_1, \dots, x_n$ are used to express $P_n(x)$ in the form $P_n(x) = a_0 + a_1(x - x_0) + a_2(x - x_0)(x - x_1) + \dots + a_n(x - x_0)\dots(x - x_{n - 1})$ for constants $a_0, \dots, a_n$.

- The $0$-th divided difference respect to $x_i$ is $f[x_i] = f(x_i)$.
- The $1$-st divided difference respect to $x_i$ and $x_{i + 1}$ is $f[x_i, x_{i + 1}] = \frac{f[x_{i + 1}] - f[x_i]}{x_{i + 1} - x_i}$.
- The $2$-nd divided difference respect to $x_i, x_{i + 1}$ and $x_{i + 2}$ is $f[x_i, x_{i + 1}, x_{i + 2}] = \frac{f[x_{i + 1}, x_{i + 2}] - f[x_i, x_{i + 1}]}{x_{i + 2} - x_i}$.
- The $k$-th divided difference respect to $x_i, \dots x_{i + k}$ is $f[x_i, x_{i + 1}, \dots, x_{i + k}] = \frac{f[x_{i + 1}, \dots, x_{i + k}] - f[x_i, \dots, x_{i + k - 1}]}{x_{i + k} - x_i}$.
- The $n$-th divided difference respect to $x_0, \dots x_{n}$ is $f[x_0, x_1, \dots, x_n] = \frac{f[x_1, \dots, x_n] - f[x_0, \dots, x_{n - 1}]}{x_{n} - x_0}$.

The $P_n(x)$ could be rewritten in the Newton's Divided-Difference: $P_n(x) = f[x_0] + \sum_{k = 1}^{n} f[x_0, x_1, \dots, x_k](x - x_0) \dots (x - x_{k - 1})$

### Forward Difference

Assume that the nodes are arranged with equal spacing. Let $h = x_{i + 1} - x_i$ for each $i = 0, \dots, n - 1$ and let $x = x_0 + sh$. The difference is $x - x_i = (s - i)h$.

Therefore, $P_n(x) = P_n(x_0 + sh) = f[x_0] + sh f[x_0, x_1] + s(s - 1)h^2 f[x_0, x_1, x_2] + \dots + s(s - 1) \dots (s - n + 1) h^n f[x_0, x_1, \dots, x_n] = f[x_0] + \sum_{k = 1}^{n} s(s - 1) \dots (s - k + 1) h^k f[x_0, x_1, \dots, x_k] = f[x_0] + \sum_{k = 1}^{n} {s \choose k} k! h^k f[x_0, \dots, x_k]$.

### Newton's Forward Difference Formula

The $\Delta$ notation from Aitken's $\Delta^2$ method is defined as $\Delta p_n = p_{n + 1} - p_n$, and higher powers of $\Delta$ are defined as $\Delta^k p_n = \Delta(\Delta^{k - 1} p_n)$. Let $h$ be the difference between two consecutive nodes.

- $f[x_0, x_1] = \frac{f(x_1) - f(x_0)}{x_1 - x_0} = \frac{1}{h} (f(x_1) - f(x_0)) = \frac{1}{h} \Delta f(x_0)$
- $f[x_0, x_1, x_2] = \frac{1}{2h} \frac{\Delta f(x_1) - \Delta f(x_0)}{h} = \frac{1}{h} (f(x_1) - f(x_0)) = \frac{1}{2h^2} \Delta^2 f(x_0)$
- $f[x_0, x_1, \dots, x_k] = \frac{1}{k! h^k} \Delta^k f(x_0)$

Therefore, $P_n(x) = f[x_0] + \sum_{k = 1}^{n} {s \choose k} \Delta^k f(x_0)$.
