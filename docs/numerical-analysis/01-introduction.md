# Introduction

## Calculus Review

### Limit

The function $f$ defined on a set $X$ of real numbers has the limit $L$ at $x_0$, denoted as $\lim_{x \rightarrow x_0} f(x) = L$, if, for all real number $\epsilon > 0$, there exists a real number $\delta > 0$ such that $|f(x) - L| < \epsilon$, whenever $x \in X$ and $0 < |x - x_0| < \epsilon$.

The function $f$ defined on a set $X$ of real numbers is continuous at $x_0 \in X$ if $\lim_{x \rightarrow x_0} f(x) = f(x_0)$. $f$ is continuous on $X$ if it's continuous at each number in $X$. The set of all functions that are continuous on the set $X$ is denoted $C(X)$. When $X$ is an interval of the real line, the parentheses are omitted.

Let $\{ x_n \}_{n = 1}^{\infty}$ be an infinite sequence of real numbers. The sequence converges to $x$ if for all $\epsilon > 0$, there exists a positive integer $N(\epsilon)$ such that $|x_n - x| < \epsilon$, whenever $n > N(\epsilon)$. The notation $x_n \rightarrow x$ means that the sequence converges to $x$.

### Differentiation

Let $f$ be a function defined in an open interval that contains $x_0$. $f$ is differentiable at $x_0$ if $f'(x_0) = \lim_{x \rightarrow x_0} \frac{f(x) - f(x_0)}{x - x_0}$ exists. $f'(x_0)$ is the derivative of $f$ at $x_0$. $f$ is differentiable on $X$ if it has a derivative at each number in $X$.

The set of all functions that have $n$ continuous derivatives on $X$ is denoted $C^n (X)$. The set of functions that have derivatives of all orders on $X$ is denoted $C^\infty (X)$.

- Rolle's Theorem: If $f \in C[a, b]$ and $f$ is differentiable on $(a, b)$. If $f(a) = f(b)$, then $c \in (a, b)$ exists with $f'(c) = 0$.
- Mean Value Theorem: If $f \in C[a, b]$ and $f$ is differentiable on $(a, b)$, then a number $c \in (a, b)$ exists with $f'(c) = \frac{f(b) - f(a)}{b - a}$.
- Extreme Value Theorem: If $f \in C[a, b]$, then $c_1, c_2 \in [a, b]$ exist with $f(c_1) \le f(x) \le f(c_2)$ for all $x \in [a, b]$. If $f$ is differentiable on $(a, b)$, then $c_1$ and $c_2$ occur either at the endpoints of $[a, b]$ or where $f' = 0$.
- Intermediate Value Theorem: If $f \in C[a, b]$ and $f(a) \le K \le f(b)$, then there exists a $c \in [a, b]$ for which $f(c) = K$.

### Integration

The Riemann integral of $f$ on $[a, b]$ is the limit $\int_a^b f(x) dx = \lim_{\max \Delta x_i \rightarrow 0} \sum_{i = 1}^n f(z_i) \Delta_{x_i}$, where $x_0, x_1, \dots, x_n$ satisfies $a = x_0 \le x_1 \le \dots \le x_n = b$, where $\Delta x_i = x_i - x_{i - 1}$, and $z_i$ is a random number chosen in $[x_{i - 1}, x_i]$.

- Mean Value Theorem: If $f \in C[a, b]$, then there exists a $c \in (a, b)$ with $\int_a^b f(x) dx = \frac{1}{b - a} \int_a^b f(x) dx$.
- Weighted Mean Value Theorem: If $f \in C[a, b]$, the Riemann integral of $g$ exists on $[a, b]$, and $g(x)$ doesn't change sign on $[a, b]$, then there exists a $c \in (a, b)$ with $\int_a^b f(x)g(x) dx = f(c) \int_a^b g(x) dx$.

## Computer Arithmetic

The error that is produced when a calculator or computer is used to perform real-number calculations is called round-off error. It's caused because the machine number could hold finite number of digits.

### Double-Precision Floating-Point

The 64-bit representation is used for a real floating-point number, which contains a sign indicator $s$, 11-bit exponent $c$ (characteristic), and 52-bit fraction $f$ (mantissa). The base for the exponent if $2$. The floating-point has the form of $(-1)^s 2^{c - 1023} (1 + f)$, where the $c - 1023 \in [-1023, 1024]$.

- The number that is less than $2^{-1022}$ results in underflow.
- The number that is greater than $2^{2023} (2 - 2^{-52})$ results in overflow.

The floating-point representative $fl(x)$ for a real number $x$ is defined to be a number in $F$ that is nearest to $x$. The standard permits several tie-breakers when the distances are equal. If $2^{-1022} \le |x| \le 2^2023 (2 - 2^{-52})$, then $fl(x) = x(1 + \delta)$ where $|\delta| \le 2^{-53}$.

The epsilon machine $\epsilon_M$ represents the order of rounding error in a floating point number. The $\epsilon_M$ of double-precision floating point is $2^{-52}$.

### Decimal Machine Number

The machine number is represented in the normalized decimal floating-point form $\pm 0.d_1 d_2 d_3 \dots d_k \times 10^n$, where $1 \le d_1 \le 9$ and $0 \le d_k \le 9$ for $k \ge 2$. The floating point form of $a = s(0.d_1 d_2 d_3 \dots d_k d_{k + 1} \dots) \times 10^n$ is denoted as $fl(a)$, which could be obtained with:

- $k$-digit chopping: $fl_c(a) = s(0.d_1 d_2 d_3 \dots d_k) \times 10^n$
- $k$-digit rounding: $fl_r(a) = s(0.d\delta_1 \delta_2 \delta_3 \dots d_k) \times 10^n$ (add $5 \times 10^{n - k - 1}$ to $a$ and chop the result)

If $p^*$ is an approximation to $p$. The actual error is $p - p^*$, the absolute error is $|p - p^*|$, and the relative error is $\frac{|p - p^*|}{|p|}$, if $p \ne 0$. The relative error is more significant.

The number $p^*$ is said to approximate $p$ to $t$ significant digits if $t$ is the largest non-negative integer for which $\frac{|p - p^*|}{|p|} \le 5 \times 10^{-t}$. The floating-point representation $fl(a)$ for $a$ has the relative error $|\frac{a - fl(a)}{a}|$.

- $k$-digit chopping: For $a = 0.d_1 d_2 d_3 \dots \times 10^n$, $|\frac{a - fl(a)}{a}| = |\frac{0.d_{k + 1} d_{k + 2} \dots \times 10^{(n - k)}}{0.d_1 d_2 d_3 \dots \times 10^n}| = |\frac{0.d_{k + 1} d_{k + 2} \dots}{0.d_1 d_2 d_3 \dots}| \times 10^{-k} \le \frac{10^{-k}}{0.1} = 10^{-k + 1}$, which results in $k - 1$ significant digits.
- $k$-digit rounding: For $a = 0.d_1 d_2 d_3 \dots \times 10^n$, $|\frac{a - fl(a)}{a}| = |\frac{0.d_{k + 1} d_{k + 2} \dots \times 10^{(n - k)}}{0.d_1 d_2 d_3 \dots \times 10^n}| = |\frac{0.d_{k + 1} d_{k + 2} \dots}{0.d_1 d_2 d_3 \dots}| \times 10^{-k} \le \frac{10^{-k}}{0.1} = 10^{-k + 1}$, which results in $k - 1$ significant digits.

### Finite-Digit Arithmetic

Assume that the floating-point representations $fl(a)$ and $fl(b)$ are given for the real number $a$ and $b$ and that the symbols $[+], [-], [\times], [\div]$ represent machine arithmetic operations. The finite-digit arithmetic is defined as $a [+] b = fl(fl(a) + b fl(b))$, which performs exact arithmetic on the floating-point representations of $a$ and $b$, and then converting the exact result to its finie-digit floating-point representation.

Substraction of 2 almost equal numbers might cause cancellation of significant digits. Given numbers $a > b$ of $k$-digit representations. Let $fl(a) = 0.d_1 d_2 \dots d_p \alpha_{p + 1} \alpha_{p + 2} \dots \alpha_k \times 10^n$, and $fl(b) = 0.d_1 d_2 \dots d_p \beta_{p + 1} \beta_{p + 2} \dots \beta_k \times 10^n$. The floating point form of $a - b$ is $fl(fl(a) - fl(b)) = 0.\sigma_{p + 1} \sigma_{p + 2} \dots \sigma_k \times 10^{n - p}$, where $0.\sigma_{p + 1} \sigma_{p + 2} \dots \sigma_k = 0.\alpha_{p + 1} \alpha_{p + 2} \dots \alpha_k - \beta_{p + 1} \beta_{p + 2} \dots \beta_k$. $fl(fl(a) - fl(b))$ has most $k - p$ digits of significance. In most cases, $a - b$ will be assigned $k$ digits, with the last $p$ being either zero or random digits.

Division by a small number might causes cancellation of significant ddigits. Let $c$ be a real number and assume that $fl(c) = c + \delta$, where $\delta$ is the round-off error. Let $\epsilon = \frac{1}{10^n}$, then $\frac{z}{\epsilon} \approx = fl(\frac{fl(z)}{fl(\epsilon)}) = fl(\frac{z + \delta}{0.1 \times 10^{-n + 1}}) = (z + \delta) \times 10^n$. The original absolute error $|z - fl(z)| = |\delta|$. After division, the absolute error is $|\frac{z}{\epsilon} - fl(\frac{fl(z)}{fl(\epsilon)})| = |z \times 10^n - (z + \delta) \times 10^n| = |\delta| \times 10^n$.

## Algorithm and Convergence

An algorithm is a procedure that describes, in an unambiguous manner, a finite sequence of steps to be performed in a specified order. $O$-notation for sequences is used to determine the speed at which an algorithm converges.

Suppose ${\beta_n}_{n = 1}^{\infty}$ is a sequence known to converge to zero and ${\alpha_n}_{n = 1}^{\infty}$ converges to a number $\alpha$. If a positive constant $K$ and an integer $N$ exist such that $|\alpha_n - \alpha| \le K|\beta_n|$ for all $n \ge N$, then ${\alpha_n}_{n = 1}^{\infty}$ converges to $\alpha$ with rate of convergence $O(\beta_n)$. Therefore, $\alpha_n = \alpha + O(\beta_n)$.

Suppose that $\lim_{h \rightarrow 0} G(h) = 0$ and $lim_{h \rightarrow 0} F(h) = L$. If a positive constant $K$ exists with $|F(h) - L| \le K |G(h)|$ for a small $h$, then $F(h) = L + O(G(h))$.
