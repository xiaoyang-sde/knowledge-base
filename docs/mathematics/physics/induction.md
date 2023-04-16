# Induction

## Faraday's Law

The magnetic flux $\Phi_B$ through an infinitesimal-area element $d \vec{A}$ in a magnetic field $\vec{B}$ is $d \Phi_B = \vec{B} \cdot d \vec{A} = B dA \cos \theta$, where $\theta$ is the angle between $d \vec{A}$ and $\vec{B}$. The total magnetic flux over a finite area is $\Phi_B = \int \vec{B} \cdot d \vec{A} = \int B dA \cos \theta$. If $\vec{B}$ is uniform on a flat area $\vec{A}$, $\Phi_B = \vec{B} \cdot \vec{A} = BA \cos \theta$.

Faraday's law of induction states that $\varepsilon = - \frac{d \Phi}{dt}$, which has the unit of $V$. The direction of the inducted emf can be determined with the right hand rule. For a coil with $N$ identical turns, the total inducted emf is $\varepsilon = - N \frac{d \Phi}{dt}$.

- Simple Generator: There's a rectangular loop that is rotated with constant angular speed $\omega$ about an axis. The magnetic field $B$ is uniform and constant. At time $t = 0$, $\omega = 0$, thus $\theta = \omega t$. The magnetic flux $\Phi_B$ is $BA \cos \omega t$ and the inducted emf is $- \frac{d}{dt} BA \cos \omega t = \omega BA \sin \omega t$.
- DC Generator: The DC generator has the same setup with the simple generator except for a commutator that reverses the connections to the external circuit at angular positions at which the emf reverses. Therefore, the inducted emf is positive.
- Slidewire Generator: There's a U-shaped conductor with a metal rod of length $L$ across the two arms of the conductor, forming a circuit. The metal rod moves with constant velocity $\vec{v}$, which increases the area of the circuit. The magnetic field $B$ is uniform and constant. The magnetic flux $\Phi_B$ is $BA \cos \theta$ and the inducted emf is $- \frac{d}{dt} BA \cos \theta = -B \cos \theta \frac{dA}{dt} = -B Lv \cos \theta$.

## Lenz's Law

Lenz's law is a convenient method for determining the direction of an induced current or emf. The law states that the direction of a magnetic induction effect is such as to oppose the cause of the effect. When a conductor, such as a wire, is exposed to a changing magnetic field, a current is induced in the wire, and the direction of the current is such that it creates a magnetic field that opposes the original changing magnetic field.

## Motional EMF

Motional emf is an induced voltage or potential difference that is generated in a conductor when it moves through a magnetic field. For an element $d \vec{l}$ of the conductor, the contribution $d \varepsilon$ to the emf is the magnitude $dl$ multiplied with the component of $\vec{v} \times \vec{B}$ (the magnetic force per unit charge) parallel to $d \vec{l}$, which implies that $d \varepsilon = (\vec{v} \times \vec{B}) d \vec{l}$. The total emf is $\varepsilon = \oint (\vec{v} \times \vec{B}) d \vec{l}$. For a straight conductor, the total emf is $vBL \sin \theta$, where $\theta$ is the angle between $\vec{B}$ and $\vec{L}$.

## Induced Electric Field

Faraday's law of electromagnetic induction states that a time-varying magnetic field induces an electric field in a closed loop, which is defined as $\oint \vec{E} d\vec{l} = - \frac{d\Phi_B}{dt}$, where $\oint \vec{E} d\vec{l}$ is the line integral of the electric field around a closed loop and $\frac{d\Phi_B}{dt}$ is the time derivative of the magnetic flux through the loop.
