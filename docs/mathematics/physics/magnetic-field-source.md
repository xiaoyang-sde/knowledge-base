# Source of Magnetic Field

## Moving Charge

The single point charge $q$ moving with $\vec{v}$ could produce a magnetic field. Let $\hat{r} = \frac{\vec{r}}{r}$ be the unit vector from the source to the observation point. The $\vec{B}$ is defined as $\vec{B} = \frac{\mu_0}{4 \pi} \frac{q \vec{v} \times \hat{r}}{r^2}$, where the magnetic constant $\mu_0 = 4 \pi \times 10^{-7} \frac{T \cdot m}{A}$. The magnitude of $\vec{B}$ is $B = \frac{\mu_0}{4 \pi} \frac{|q| v \sin \theta}{r^2}$, where $\theta$ is the angle between $\vec{v}$ and $\hat{r}$. The direction of $\vec{B}$ is perpendicular to the plane containing the line between the point charge and $P$ and $\vec{v}$.

## Current Element

The magnetic field follows the principle of superposition. For a current in a conductor, the volume of a segment $d \vec{l}$ is $A dl$, where $A$ is the cross-sectional area of the conductor. If there are $n$ moving charged particles per unit volume, each of charge $q$, the total moving charge $dQ = n q A dl$.

Let $\hat{r} = \frac{\vec{r}}{r}$ be the unit vector from the source to the field point. The $\vec{B}$ is defined as $d \vec{B} = \frac{\mu_0}{4 \pi} \frac{I d \vec{l} \times \hat{r}}{r^2}$, where $d \vec{l}$ points in the same direction as the current. The magnitude of the field $d \vec{B}$ is $dB = \frac{\mu_0}{4 \pi} \frac{|dQ| v_d \sin \theta}{r^2} = \frac{\mu_0}{4 \pi} \frac{I dl \sin \theta}{r^2}$, where $\theta$ is the angle between $\vec{l}$ and $\hat{r}$.

The law of Biot and Savart states that the magnetic field $\vec{B}$ due to the current in a circuit is $\vec{B} = \frac{\mu_0}{4 \pi} \int \frac{I d \vec{l} \times \hat{r}}{r^2}$.

## Straight Conductor

Let there be a conductor with length $2a$ with a current $I$. Assume that the conductor aligns with the $y$-axis, where $dl = dy$. The magnitude of the field $d \vec{B}$ of the element of conductor of length $dl$ at a point is $B = \frac{\mu_0}{4 \pi} \frac{I dy \sin \theta}{r^2} = \frac{\mu_0}{4 \pi} \frac{I x dy}{(x^2 + y^2)^{\frac{3}{2}}}$.

The magnitude of the total $\vec{B}$ is $B = \frac{\mu_0 I}{4 \pi} \int_{-a}^{a} \frac{x dy}{(x^2 + y^2)^{\frac{3}{2}}}$. When $a$ is much larger than $x$, $B = \frac{\mu_0 I}{2 \pi x}$. Therefore, all points on a circle of radius $r$ around the conductor has a magnitude of $\vec{B}$ be $\frac{\mu_0 I}{2 \pi r}$.

## Parallel Conductor

Let there be two parallel conductors with a distance $r$ with a current $I$ and $I'$ in the same direction. At the position of the upper conductor, the magnitude of $\vec{B}$ is $B = \frac{\mu_0 I}{2 \pi r}$. The force on a length $L$ of the upper conductor is $\vec{F} = I' \vec{L} \times \vec{B}$. Since $\vec{B}$ is perpendicular to the length of the conductor, the magnitude of the force is $F = I'LB = \frac{\mu_0 I I' L}{2 \pi r}$, thus the force per unit length is $\frac{F}{L} = \frac{\mu_0 I I'}{2 \pi r}$.

- If the directions of currents are the same, the conductors attract each other.
- If the directions of currents are the opposite, the conductors repel each other.

## Circular Current Loop

Let there be a circular conductor with radius $a$ and current $I$. For a point $P$ on the axis of the loop with distance $x$ from the center, the magnitude of $dB$ due to element $d\vec{l}$ is $dB = \frac{\mu_0 I}{4 \pi} \frac{dl}{(x^2 + a^2)}$. The total field $\vec{B}$ at $P$ has an $x$-component with magnitude of $B_x = \int \frac{\mu_0 I}{4 \pi} \frac{a dl}{(x^2 + a^2)^{\frac{3}{2}}} = \frac{\mu_0 I a^2}{2 (x^2 + a^2)^{\frac{3}{2}}}$.

Let there be a coil of $N$ loops, all with the same radius $a$.  For a point $P$ on the axis of the loop with distance $x$ from the center, the magnitude of $B_x = \frac{\mu_0 N I a^2}{2 (x^2 + a^2)^{\frac{3}{2}}}$ is $N$ times the field of a single loop.

## Ampere's Law

The Ampere's law states that $\oint \vec{B} \cdot d \vec{l} = \mu_0 I_{\text{encl}}$, where $I_{\text{encl}}$ is the amount of enclosed current in a closed loop. The direction of the surface can be determined with the right-hand rule, and the currents of the same direction contribute to $I_{\text{encl}}$.

- The magnetic field at a distance $r$ from a long, straight conductor with current $I$ is $B = \frac{\mu_0 I}{2 \pi r}$. The line integral of $\vec{B}$ around a circle with radius $r$ is $\oint \vec{B} \cdot d \vec{l} = B \oint dl = \frac{\mu_0 I}{2 \pi r} (2 \pi r) = \mu_0 I \implies B = \frac{\mu_0 I}{2 \pi r}$.

- The magnetic field at a distance $r < R$ from a cylindrical conductor with radius $R$ and current $I$ is $B = \frac{\mu_0 I}{2 \pi} \frac{r}{R^2}$, where the enclosed current $I_{\text{encl}} = \frac{Ir^2}{R^2}$.

- The magnetic field near the center of a solenoid with $n$ turns per unit length and current $I$ is $B = \mu_0 nI$, where the enclosed current $I_{\text{encl}} = nLI$.

- The magnetic field at a distance $r$ from a toroidal solenoid with $N$ turns of wire and current $I$ is $B = \frac{\mu_0 N I}{2 \pi r}$, where $r$ locates between the inner and outer loop.
