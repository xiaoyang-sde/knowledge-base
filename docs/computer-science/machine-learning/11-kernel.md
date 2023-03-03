# Kernel

To increase the expressive power of linear models, a linear transform function $\phi: x \in R^D \to z \in R^M$ could map the feature vector $x$ to a high-dimensional feature space. However, the computation cost is significant when $M$ is large. Therefore, a kernel function equivalent to $\phi$ can be used to make computation more efficient.

The kernel trick is a technique used in machine learning to map the feature vector $x$ into a high-dimensional feature space, without computing $\phi(x)$. The kernel function computes the inner product between the mapped feature vectors, instead of computing the coordinates of the feature vectors themselves.

## Kernel Function

The kernel function $k(\cdot, \cdot)$ is  a bivariate function that satisfies the following properties. For each $x_m$ and $x_n$, $k(x_m, x_n) = k(x_n, x_m)$ and $k(x_m, x_n) = \phi(x_m)^T \phi(x_n)$.

- Polynomial kernel function with degree of $d$: $k(x_m, x_n) = (x_m^T x_n + c)^d$
- Gaussian RBF kernel: $k(x_m, x_n) = e^{\frac{-||x_m - x_n||_2^2}{2 \sigma^2}}$

## Mercer Theorem

The bivariate function $k(\cdot, \cdot)$ is a kernel function $\iff$ for each $N$ and $x_1, \dots, x_N$, the matrix $K$ where $K_{ij} = k(x_i, x_j) = \phi(x_i)^T \phi(x_j)$ is positive semi-definite.

The matrix $K$ is positive semi-definite if the eigenvalues of $K$ are non-negative or $x^T K x \ge 0$ for all vector $x$.

## Kernel Composition

- If $k(x_m, x_n)$ is a kernel, then $ck(x_m, x_n)$ is a kernel if $c > 0$.
- If $k(x_m, x_n)$ is a kernel, then $e^{k(x_m, x_n)}$ is a kernel.
- If $k_1(x_m, x_n)$ and $k_2(x_m, x_n)$ are kernels, then $\alpha k_1(x_m, x_n) + \beta k_2(x_m, x_n)$ is a kernel if $\alpha, \beta \ge 0$.
- If $k_1(x_m, x_n)$ and $k_2(x_m, x_n)$ are kernels, then $k_1(x_m, x_n)k_2(x_m, x_n)$ is a kernel .
