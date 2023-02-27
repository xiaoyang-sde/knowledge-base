# Solution of Equation in One Variable

## Bisection Method

If $f$ is a continuous function defined on the interval $[a, b]$, with $f(a)$ and $f(b)$ of opposite sign. The Intermediate Value Theorem implies that a number $p$ exists in $(a, b)$ with $f(p) = 0$. The bisection method calls for a repated bisecting of subintervals of $[a, b]$ and loating the half containing $p$.

- Let $c = a + (b - a) / 2$.
- If $f(a)$ and $f(c)$ has opposite sign, then search in $[a, c]$.
- If $f(c)$ and $f(b)$ has opposite sign, then search in $[c, b]$.

```cpp
auto bisection(
  const function<double(double)>& f,
  const double lower_bound,
  const double upper_bound
) noexcept -> double {
  double mid = lower_bound + (upper_bound - lower_bound) / 2;
  if (abs(f(mid)) < epsilon) {
    return mid;
  }

  if ((f(mid) < 0) == (f(upper_bound) < 0)) {
    return bisection(f, lower_bound, mid);
  } else  {
    return bisection(f, mid, upper_bound);
  }
}
```

The algorithm generates a sequence ${p_n}$ that could converge to a root $p$ of the function $f$. The stopping condition could be $| p_N - p_{N - 1} | < \epsilon$, $\frac{| p_N - p_{N - 1} |}{|p_N|} < \epsilon$, or $| f(p_N) | < \epsilon$. It's a good practice to set an upper bound on the number of iterations to prevent infinite loop.

Suppose that $f \in C[a, b]$ and $f(a) \cdot f(b) < 0$. The bisection method generates a sequence ${p_n}_{n = 1}^{\infty}$ approximating to a root $p$ of $f$ with $|p_n - p| \le \frac{b - a}{2^n}$, when $n \ge 1$.

## Fixed-Point Iteration

### Fixed-Point

The fixed point for a function is a number at which the value of the function does not change when the function is applied. The number $p$ is a fixed point for a given function $g$ if $g(p) = p$.

Given a root-finding problem $f(p) = 0$. Let a function $g$ with a fixed point at $p$ be $g(x) = x - f(x)$, which implies that $f(x) = x - g(x)$.

- If $g \in C[a, b]$ and $g(x) \in [a, b]$ for all $x \in [a, b]$, then $g$ has at least one fixed point in $[a, b]$.
- If $g'(x)$ exists on $(a, b)$ and a positive constant $k < 1$ exists with $|g'(x)| \le k$ for all $x \in (a, b)$, then there's a single fixed point in $[a, b]$.

### Approximation

To approximate the fixed point of a function $g$, choose an initial approximation $p_0$ and generate the sequence ${p_n}_{n = 0}^{\infty}$ with $p_n = g(p_{n - 1})$ for each $n \ge 1$.

If the sequence converges to $p$ and $g$ is continuous, then $p = \lim_{n \rightarrow \infty} p_n = \lim_{n \rightarrow \infty} g(p_{n - 1}) = g(\lim_{n \rightarrow \infty} p_{n - 1}) = g(p)$, which is a solution to $x = g(x)$. The method is fixed-point iteration.

```cpp
const double epsilon = pow(1, -9);

auto fixed_point_iteration(
  function<double(double)> f,
  int iteration_limit
) -> double {
  double p_0 = 0;
  for (int i = 0; i < iteration_limit; ++i) {
    double p = f(p_0);
    if (abs(p - p_0) < epsilon) {
      return p;
    }
    p_0 = p;
  }
}
```

### Fixed-Point Theorem

Let $g \in C[a, b]$ be such that $g(x) \in [a, b]$ for all $x$ in $[a, b]$. Suppose that $g'$ exists on $(a, b)$ and a constant $0 < k < 1$ exists with $|g'(x)| \le k$ for all $x$ in $[a, b]$. For $p_0$ in $[a, b]$, the sequence $p_n = g(p_{n - 1})$ converges to the unique fixed point $p$ in $[a, b]$.

### Convergence

If $g'(p) = g''(p) = \dots = g^{(\alpha - 1)}(p) = 0$ but $g^{(\alpha)}(p) \ne 0$, then the sequence converges to $p$ for all $p_0$ close to $p$ with order $\alpha$.

If $|g'(x)| \le k$, the bounds for the error involved in using $p_n$ to approximate $p$ is $|p_n - p| \le k^n \max{(p_0 - a, b - p_0)}$ and $|p_n - p| \le \frac{k^n}{1 - k} |p_1 - p_0|$ for all $n \ge 1$. When $k$ is closer to $0$, the converge is faster. Therefore, the fixed-point iteration will be fast if the absolute value of its first derivative is small.

## Newton's Method

Suppose that $f \in C^2 [a, b]$. Let $p_0 \in [a, b]$ be an approximation to $p$ such that $f'(p_0) \ne 0$ and $|p - p_0|$ is small. The first Taylor polynomial for $f(x)$ expanded about $p_0$ and evaluated at $x = p$.

$$f(p) = f(p_0) + (p - p_0) f'(p_0) + \frac{(p - p_0)^2}{2} f''(\epsilon(p))$$

Since $f(p) = 0$, the equation is $0 = f(p_0) + (p - p_0) f'(p_0) + \frac{(p - p_0)^2}{2} f''(\epsilon(p))$.

Since $(p - p_0)^2$ is small, $0 \approx f(p_0) + (p - p_0) f'(p_0)$, thus $p \approx p_0 - \frac{f(p_0)}{f'(p_0)}$.

The Newton's method starts with an initial approximation $p_0$ and generates the sequence ${p_n}_{n = 0}^{\infty}$ with $p_n = p_{n - 1} - \frac{f(p_{n - 1})}{f'(p_n - 1)}$ for $n \ge 1$.

```cpp
const double epsilon = pow(10, -9);

auto newton_method(
  function<double(double)> f,
  function<double(double)> f_derivative,
  int iteration_limit,
  double initial_approximation
) -> double {
  double p_0 = initial_approximation;

  for (int i = 0; i < iteration_limit; ++i) {
    double p = p_0 - f(p_0) / f_derivative(p_0);
    if (abs(p - p_0) < epsilon) {
      return p;
    }
    p_0 = p;
  }

  return 0;
}
```

### Convergence Theorem

Let $f \in C^2 [a, b]$. If $p \in (a, b)$ is such that $f(p) = 0$ and $f'(p) \ne 0$, then there's a $\delta > 0$ such that Newton's method generates a sequence ${p_n}_{n = 1}^{\infty}$ converging to $p$ for each initial approximation $p_0 \in [p - \delta, p + \delta]$.

- If $f'(p) = 0$, Newton's method converges with $\alpha \le 1$.
- If $f''(p) \ne 0$, Newton's method converges with $\alpha = 2$.
- If $f''(p) = 0$, Newton's method converges with $\alpha > 2$.

## The Secant Method

Newton's method is a powerful technique, but it has a major weakness: the need to know the value of the derivative of $f$ at each approximation.

Given that $f'(p_{n - 1}) = \lim_{x \rightarrow p_{n - 1}} \frac{f(x) - f(p_{n - 1})}{x - p_{n - 1}}$. If $p_{n - 2}$ is close to $p_{n - 1}$, then $f'(p_{n - 1}) \approx \frac{f(p_{n - 1}) - f(p_{n - 2})}{p_{n - 1} - p_{n - 2}}$.

Substitute $f'(p_{n - 1})$ in Newton's formula, $$p_n = p_{n - 1} - \frac{f(p_{n - 1})(p_{n - 1} - p_{n - 2})}{f(p_{n - 1}) - f(p_{n - 2})}$$.

```cpp
const double epsilon = pow(10, -9);

auto secant_method(
  function<double(double)> f,
  int iteration_limit,
  double initial_approximation_0,
  double initial_approximation_1
) -> double {
  double p_0 = initial_approximation_0;
  double p_1 = initial_approximation_1;

  for (int i = 0; i < iteration_limit; ++i) {
    double p = p_1 - f(p_1) * (p_1 - p_0) / (f(p_1) - f(p_0));
    if (abs(p - p_0) < epsilon) {
      return p;
    }
    p_0 = p_1;
    p_1 = p;
  }

  return 0;
}
```

## Method of False Position

Each successive pair of approximations in the Bisection method brackets a root p of the equation. For each $n$, $|p_n - p| \le \frac{1}{2} |a_n - b_n|$. However, root bracketing is not guaranteed for either Newton's method or the Secant method.

The method of False Position generates approximations in the same manner as the Secant method, but it includes a test to ensure that the root is bracketed between successive iterations.

The initial approximations $p_0$ and $p_1$ should meet the requirement that $f(p_0) \cdot f(p_1) < 0$. The approximation $p_2$ is chosen in the same manner as in the Secant method.

- If $f(p_2) \cdot f(p_1) < 0$, then $p_1$ and $p_2$ bracket a root. Choose $p_3$ as the x-intercept of the line joining $(p_1, f(p_1))$ and $(p_2, f(p_2))$.
- If not, choose $p_3$ as the x-intercept of the line joining $(p_0, f(p_0))$ and $(p_2, f(p_2))$, and swap $p_0$ and $p_1$. The relabeling ensures that the root is bracketed between successive iterations.

## Error Analysis for Iterative Method

### Order of Convergence

Suppose that $p_n \rightarrow p$ as $n \rightarrow \infty$ with $p_n \ne p$ for all $n$. If $\lambda, \alpha > 0$ exist with $\lim_{n \rightarrow \infty} \frac{|p_{n + 1} - p|}{|p_n - p|^\alpha} = \lambda$, then ${p_n}_{n = 0}^{\infty}$ converges to $p$ of order $\alpha$, with asymptotic error constant $\lambda$.

$\alpha$ reflects the convergence speed more than $\lambda$. Larger $\alpha$ implies faster convergence.

### Multiple Root

The solution $p$ of $f(x) = 0$ is a zero of multiplicity $m$ of $f$ if for $x \ne p$, $f(x) = (x - p)^m q(x)$, where $\lim_{x \rightarrow p} q(x) \ne 0$.

- The function $f \in C^1 [a, b]$ has a zero of multiplicity $1$ (simple zero) at $p$ in $(a, b)$ if and only if $f(p) = 0$, but $f'(p) \ne 0$.
- The function $f \in C^m [a, b]$ has a zero of multiplicity $m$ at $p$ in $(a, b)$ if and only if $f(p) = f'(p) = f''(p) = \dots f^{(m - 1)}(p) = 0$, but $f^{(m)}(p) \ne 0$.

For Newton's and the Secant method, quadratic convergence might not occur if $f'(p) = 0$ when $f(p) = 0$ (the zero is not simple).

Newton's method and the Secant method have problem if $f'(p) = 0$ when $f(p) = 0$. To handle this problem, define $\mu(x) = \frac{f(x)}{f'(x)} = (x - p)\frac{q(x)}{mq(x) + (x - p)q'(x)}$, which has a simple zero at $p$, because $\frac{q(x)}{mq(x) + (x - p)q'(x)} \ne 0$. Therefore, the solution of $\mu(x)$ is the solution of $f(x)$.

The modified Netwon's method is $g(x) = x - \frac{\mu(x)}{\mu'(x)} = x - \frac{f(x)f'(x)}{[f'(x)]^2 - f(x)f''(x)}$. The method converges to the zero $p$ of $f$ quadratically.

## Accelerating Convergence

### Aitken's $\Delta^2$ Method

Suppose ${p_n}_{n = 0}^{\infty}$ is a linearly convergent sequence with limit $p$. The Aitken's $\Delta^2$ method converts the sequence to $\hat{p_n} = p_n - \frac{(p_{n + 1} - p_n)^2}{p_{n + 2} - 2p_{n + 1} + p_n}$, which converges more rapidly to $p$ than the original sequence.

For a given sequence ${p_n}_{n = 0}^{\infty}$, the forward difference $\Delta p_n$ is defined as $\Delta p_n = p_{n + 1} - p_n$, and higher powers of $\Delta$ are defined as $\Delta^k p_n = \Delta(\Delta^{k - 1} p_n)$. Therefore, $\Delta^2 p_n = \Delta(p_{n + 1} - p_n) = (p_{n + 2} - p_{n + 1}) - (p_{n + 1} - p_n)$, thus $\hat{p_n} = p_n - \frac{(\Delta p_n)^2}{\Delta^2 p_n}$.
