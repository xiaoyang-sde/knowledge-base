# Multiple Integral

The double integral of a function of two variables over a rectangular region $R$ is defined as $\iint_{R} f(x, y) dA = \lim_{n,m \to \infty } \sum_{i = 1}^n \sum_{j = 1}^m {f (x_i^*,y_j^*)\,\Delta A}$.

The Fubini's theorem states that if $f(x, y)$ is continuous on $R = [a, b] \times [c, d]$, then $\iint_{R} f(x, y) dA = \int_a^b \int_c^d f(x, y) dy dx = \int_c^d \int_a^b f(x, y) dx dy$, which are iterated integrals. If $f(x, y) = g(x) h(y)$, then $\iint_{R} f(x, y) dA = \int_a^b \int_c^d f(x, y) dy dx = \int_a^b g(x) dy \int_c^d h(y) dx = \int_c^d h(y) dx \int_a^b g(x) dy$.

- For a general region $D = \{ (x, y) | a \le x \le b, g_1(x) \le y \le g_2(x) \}$, $\iint_D f(x, y) dA = \int_a^b \int_{g_1(x)}^{g_2(x)} f(x, y) dy dx$.

- For a general region $D = \{ (x, y) | h_1(x) \le x \le h_2(x), c \le y \le d \}$, $\iint_D f(x, y) dA = \int_c^d \int_{h_1(y)}^{h_2(y)} f(x, y) dx dy$.

## Change of Variable

- Polar Coordinate: To convert a double integral from Cartesian coordinate to polar coordinate, let $x = r \cos \theta, y = r \sin \theta, dA = r dr d\theta$. Therefore, $\iint_D f(x, y) dA = \int_\alpha^\beta \int_{h_1(\theta)}^{h_2(\theta)} f(r \cos \theta, r \sin \theta) r dr d\theta$.

- Spherical Coordinate: To convert a triple integral from Cartesian coordinate to spherical coordinate, let $x = \rho \sin \phi \cos \theta, y = \rho \sin \phi \sin \theta, z = \rho \cos \phi, dV = \rho^2 \sin \phi d\rho d\theta d\phi$, where $\rho \ge 0$ and $0 \le \phi \le \pi$. Therefore, $\iiint_E f(x, y, z) dV = \int_\delta^\gamma \int_\alpha^\beta \int_a^b \rho^2 \sin \phi f(\rho \sin \phi \cos \theta, \rho \sin \phi \sin \theta, \rho \cos \phi) d\rho d\theta d\phi$.
