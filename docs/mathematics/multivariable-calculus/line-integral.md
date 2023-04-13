# Line Integral

## Parametric Equation

The parametric equation defines the equation of a curve in terms of a separated variable $t$. The collection of points defined with the range of $t$ is the parametric curve.

- The parametric equation of a curve $y = f(x)$ is $x = t, y = f(t)$.
- The parametric equation of a curve $x = g(y)$ is $x = g(t), y = t$.
- The parametric equations of a circle $x^2 + y^2 = r^2$ is $x = r \cos t, y = r \sin t, 0 \le t \le 2 \pi$ or $x = r \cos t, y = r -\sin t, 0 \le t \le 2 \pi$.

For a generial parametric equation $x = h(t), y = g(t), a \le t \le b$, the curve is defined as $\vec{r}(t) = h(t) \vec{i} + g(t) \vec{j}$. The curve is smooth if $\vec{r}'(t)$ is continuous and $\vec{r}'(t) \ne 0$ for all $t$.

- The line integral of $f(x, y)$ with respect to arc length is $\int_C f(x, y) ds = \int_a^b f(h(t), g(t)) \sqrt{(\frac{dx}{dt}^2 + \frac{dy}{dt})^2} dt$. The direction of line integral with respect to arc length doesn't change the value of the integral.
- The line integral of $f(x, y, z)$ with respect to arc length is $\int_C f(x, y, z) ds = \int_a^b f(x(t), y(t), z(t)) \sqrt{(\frac{dx}{dt}^2 + \frac{dy}{dt})^2 + \frac{dz}{dt}^2} dt$.
- The line integral of $f$ with respect to $x$ is $\int_C f(x, y) dx = \int_a^b f(x(t), y(t)) x'(t) dt$.
- The line integral of $f$ with respect to $y$ is $\int_C f(x, y) dx = \int_a^b f(x(t), y(t)) y'(t) dt$.
