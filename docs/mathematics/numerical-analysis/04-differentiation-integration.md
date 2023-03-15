# Numerical Differentiation and Integraion

## Numerical Differentiation

The derivative is defined as $f'(x_0) = \lim_{h \to 0} \frac{f(x_0 + h) - f(x_0)}{h}$. If $h$ is small, then $f'(x_0) = \frac{f(x_0 + h) - f(x_0)}{h}$.

Let $f \in C^2([a, b])$ and $x_0, x_1 \in [a, b]$, then $f(x_1) = f(x_0) + f'(x_0)(x_1 - x_0) + f''(\xi) \frac{(x_1 - x_0)^2}{2}$. Let $x_1 = x_0 + h$, then $f(x_0 + h) = f(x_0) + f'(x_0)h + \frac{f''(\xi) h^2}{2}$, which implies that $\frac{f(x_0 + h) - f(x_0)}{h} = f'(x_0) + h \frac{f''(\xi)}{2}$. Therefore, $f'(x_0) \approx \frac{f(x_0 + h) - f(x_0)}{h}$.

- Forward-difference formula: $f'(x_0) \approx \frac{f(x_0 + h) - f(x_0)}{h}$
- Backward-difference formula: $f'(x_0) \approx \frac{f(x_0) - f(x_0 - h)}{h}$

The error of the approximation is defined as $|f'(x_0) - \frac{f(x_0) - f(x_0 - h)}{h}| \le M \frac{|h|}{2}$, where $M$ is a bound on $|f''(x)|$ for $x$ between $x_0$ and $x_0 + h$.

### Three-Point Midpoint Formula

Let $f \in C^3([a, b])$ and $x_0, x_1, x_2 \in [a, b]$. Let $x_1 = x_0 + h$ and $x_2 = x_0 - h$, then $f'(x_0) = \frac{1}{2h} [f(x_0 + h) - f(x_0 - h)] - \frac{h^2}{6} f^{(3)}(\xi)$, where $x_0 - h \le \xi \le x_0 + h$.

### Second Derivative Midpoint Formula

Let $f \in C^4([a, b])$ and $x_0, x_1, x_2 \in [a, b]$. Let $x_1 = x_0 + h$ and $x_2 = x_0 - h$, then $f''(x_0) = \frac{1}{h^2} [f(x_0 - h) - 2f(x_0) + f(x_0 + h)] - \frac{h^2}{12} f^{(4)}(\xi)$, where $x_0 - h \le \xi \le x_0 + h$.

### Round-Off Error in Numerical Differentiation

Round-off error occurs due to the finite precision of numerical methods and the limitations of computer hardware. For example, in the three-point midpoint formula, if $h$ is too small, the difference between $f(x_0 - h)$ and $f(x_0 + h)$ is too small to be represented.

Let $e(x_0 - h)$ and $e(x_0 + h)$ be the round-off errors in evaluating $f(x_0 - h)$ and $f(x_0 + h)$. The total error in the approximation of $f'(x_0)$ is $\frac{1}{2h} [e(x_0 + h) - e(x_0 - h)] - \frac{h^2}{6} f^{(3)}(\xi_1)$, which is due to round-off error and truncation error. If $e(x_0 \pm h) \le \epsilon$ and $f^{(3)}(\xi_1) \le M$, then the total error is less than $\frac{\epsilon}{h} + \frac{h^2}{6} M$. Therefore, if $h$ is small, the round-off error will dominate the calculations.

## Richardson's Extrapolation

Richardson's extrapolation is an effective mechanism for genearting higher order numerical methods from lower order ones. For example, the three-point midpoint formula is $f'(x_0) = \frac{1}{2h} [f(x_0 + h) - f(x_0 - h)] - \frac{h^2}{6} f^{(3)}(x_0) + O(h^4)$.

- Let $D_h = \frac{1}{2h} [f(x_0 + h) - f(x_0 - h)]$, then $f'(x_0) = D_h - \frac{h^2}{6} f^{(3)}(x_0) + O(h^4)$.
- Let $D_{\frac{h}{2}} = \frac{1}{h} [f(x_0 + \frac{h}{2}) - f(x_0 - \frac{h}{2})]$, then $f'(x_0) = D_{\frac{h}{2}} - \frac{h^2}{24} f^{(3)}(x_0) + O(h^4)$.

Therefore, $4f'(x_0) - f'(x_0) = 4D_{\frac{h}{2}} - \frac{h^2}{6}f^{(3)}(x_0) - D_h + \frac{h^2}{6} f^{(3)}(x_0)$, which implies that $f'(x) = \frac{4D_{\frac{h}{2}} - D_h}{3} + O(h^4)$, which is a formula of $4$-th order.

## Numerical Integration

The numerical integration formula (quadrature rule)  is defined as $I_f = \int_a^b f(x) dx \approx \sum_{j = 0}^n a_j f(x_j)$ for a finite interval $[a, b]$ and an integrable function $f$, which has nodes $x_j$ and weights $a_j$.

Newton-Cotes formulas is a collection of formula based on polynomial interpolation at equidisttant abscissae. If the endpoints $a$ and $b$ are included in the abscissae $x_0 < x_1 < \dots < x_n$ ($x_0 = a$ and $x_n = b$), then the formula is closed. Otherwise, the formula is open.

### Closed Newton-Cotes Formula

Let ${x_0, x_1, \dots, x_n}$  be the set of distinct nodes from the interval $[a, b]$. Let the nodes be $x_i = x_0 + ih$ where $x_0 = a, x_n = b$ and $h = \frac{b - a}{n}$.

Let $P_n(x) = \sum_{i = 0}^{n} f(x_i) L_i(x)$ be the Lagrange polynomial of $f(x)$. $\int_{a}^{b} f(x) dx = \int_{a}^{b} \sum_{i = 0}^{n} f(x_i) L_i(x) dx + \int_{a}^{b} \frac{f^{(n + 1)}(\xi(x))}{(n + 1)!} \Pi_{i = 0}^{n} (x - x_i) dx = \sum_{i = 0}^{n} a_i f(x_i) + \int_{a}^{b} \frac{f^{(n + 1)}(\xi(x))}{(n + 1)!} \Pi_{i = 0}^{n} (x - x_i) dx$, where $a_i = \int_{a}^{b} L_i(x) dx$.

Therefore, the formula is $\int_{a}^{b} f(x) dx \approx \sum_{i = 0}^{n} a_i f(x_i)$ and the error is $E(f) = \int_{a}^{b} \frac{f^{(n + 1)}(\xi(x))}{(n + 1)!} \Pi_{i = 0}^{n} (x - x_i) dx$.

- $n = 1$ (Trapezoidal rule): $\int_{x_0}^{x_1} f(x) dx = \frac{h}{2} [f(x_0) + f(x_1)] - \frac{h^3}{12} f''(\xi)$, where $x_0 < \xi < x_1$.
- $n = 2$ (Simpson's rule): $\int_{x_0}^{x_2} f(x) dx = \frac{h}{3} [f(x_0) + 4f(x_1) + f(x_2)] - \frac{h^5}{90} f^{(4)}(\xi)$
- $n = 3$ (Simpson's three-eighths rule): $\int_{x_0}^{x_3} f(x)dx = \frac{3h}{8} [f(x_0) + 3f(x_1) + 3f(x_2) + f(x_3)] - \frac{3h^5}{80} f^{(4)}(\xi)$, where $x_0 < \xi < x_3$.
- $n = 4$: $\int_{x_0}^{x_4} f(x)dx = \frac{2h}{45} [7f(x_0) + 32f(x_1) + 12f(x_2) + 32f(x_3) + 7f(x_4)] - \frac{8h^7}{945} f^{(6)}(\xi)$, where $x_0 < \xi < x_4$.

#### The Trapezoidal Rule

Given the interval $[a, b]$, let $x_0 = a, x_1 = b$ be the nodes and let $h = b - a$. The Lagrange polynomial interpolant of $x_0$ and $x_1$ is $P_1(x) = \frac{x - x_1}{x_0 - x_1} f(x_0) + \frac{x - x_0}{x_1 - x_0} f(x_1)$. The integration is $\int_{x_0}^{x_1} f(x) dx = \int_{x_0}^{x_1} [\frac{x - x_1}{x_0 - x_1} f(x_0) + \frac{x - x_0}{x_1 - x_0} f(x_1)] dx + \frac{1}{2} \int_{x_0}^{x_1} f''(\xi(x)) (x - x_0) (x - x_1) dx = \frac{h}{2} [f(x_0) + f(x_1)] - \frac{h^3}{12} f''(\xi)$.

#### The Simpson's Rule

Given the interval $[a, b]$, let $x_0 = a, x_1 = a + h, x_2 = b$ be the nodes and let $h = \frac{b - a}{2}$. The Lagrange polynomial interpolant of $x_0, x_1$ and $x_2$ is $P_2(x)$. The integration is $\int_{x_0}^{x_2} f(x) dx = \int_{x_0}^{x_2} P_2(x) + \int_{x_0}^{x_2} \frac{(x - x_0)(x - x_1)(x - x_2)}{6} f^{(3)} (\xi(x)) dx = \frac{h}{3} [f(x_0) + 4f(x_1) + f(x_2)] - \frac{h^5}{90} f^{(4)}(\xi)$.

### Open Newton-Cotes Formula

Let ${x_0, x_1, \dots, x_n}$  be the set of distinct nodes from the interval $[a, b]$. Let the nodes be $x_i = x_0 + ih$ where $x_{-1} = a, x_{n + 1} = b$ and $h = \frac{b - a}{n + 2}$.

- $n = 0$ (Midpoint rule): $\int_{x_{-1}}^{x_1} f(x) dx = 2hf(x_0) + \frac{h^3}{3} f''(\xi)$, where $x_{-1} < \xi < x_1$.
- $n = 1$: $\int_{x_{-1}}^{x_2} f(x) dx = \frac{3h}{2} [f(x_0) + f(x_1)] + \frac{3h^3}{4} f''(\xi)$, where $x_{-1} < \xi < x_2$.
- $n = 2$: $\int_{x_{-1}}^{x_2} f(x) dx = \frac{4h}{3} [2f(x_0) - f(x_1) + 2f(x_2)] + \frac{14h^5}{45} f^{(4)}(\xi)$, where $x_{-1} < \xi < x_3$.

## Composite Integration Formula

Newton-Cotes formulas, based on interpolation polynomials using equidistant nodes, are inaccurate over large intervals due to Runge's phenomenon. However, a piecewise approach could be adopted with lower-order Newton-Cotes formulas at each subinterval.

- Composite Trapezoidal rule: Let $n$ be an integer, $x_j = a + jh$, and $h = \frac{b - a}{n}$, thus $\int_{a}^{b} f(x) dx = \frac{h}{2} [f(a) + 2 \sum_{j = 1}^{n - 1} f(x_j) + f(b)] - \frac{b - a}{12} h^2 f''(\mu)$, where $\mu \in (a, b)$.
- Composite Simpson's rule: Let $n$ be an even integer, $x_j = a + jh$, and $h = \frac{b - a}{n}$, thus $\int_{a}^{b} f(x) dx = \sum_{j = 1}^{\frac{n}{2}} \int_{2j - 2}^{2j} f(x) dx = \sum_{j = 1}^{\frac{n}{2}} \{ \frac{h}{3} [f(x_{2j - 2}) + 4f(x_{2j - 1}) + f(x_{2j})] - \frac{h^5}{90} f^{(4)}(\xi) \} = \frac{h}{3} [f(a) + 2 \sum_{j = 1}^{\frac{n}{2} - 1} f(x_{2j}) + 4 \sum_{j = 1}^{\frac{n}{2}} f(x_{2j - 1}) + f(b)] - \frac{b - a}{180} h^4 f^{(4)}(\mu)$, where $\mu \in (a, b)$.
- Composite Midpoint rule: Let $n$ be an even integer, $x_j = a + jh$, and $h = \frac{b - a}{n + 2}$, thus $\int_{a}^{b} f(x) dx = 2h \sum_{j = 0}^{\frac{n}{2}} f(x_{2j}) + \frac{b - a}{6} h^2 f''(\mu)$, where $\mu \in (a, b)$.
