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
