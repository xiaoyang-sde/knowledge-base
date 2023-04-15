# Magnetic Field and Force

## Magnetic Field

The magnetic field $\vec{B}$ is a vector field. The direction of $\vec{B}$ is the direction in which the north pole of a compass needle tends to point.

The magnetic field exerts a force $\vec{F}$ on other moving charge or current that is present in the field. The formula of $\vec{F}$ is $\vec{F} = q \vec{v} \times \vec{B}$, which has a magnitude of $F = |q| v B \sin \theta$, where $\theta$ is the angle between $\vec{v}$ and $\vec{B}$. The direction of $\vec{F}$ can be determined with the right hand rule of cross product. The force is $0$ when $\vec{v}$ and $\vec{B}$ have the same direction.

The unit of $B$ is **tesla** or $T$, where $1T = 1 \frac{N}{A \cdot m} = 1 \frac{N \cdot s}{C \cdot m}$. The unit **gauss** is defined as $1 G = 10^{-4} T$.

When a charged particle moves through a region of space where both electric and magnetic fields are present, both fields exert forces on the particle. The total force $\vec{F}$ is $q(\vec{E} + \vec{v} \times \vec{B})$.

## Gauss's Law for Magnetism

The magnetic flux $\Phi_B$ a measure of the strength of a magnetic field passing through a surface. Let $d \Phi_B$ be the magnetic flux of a fraction of the surface, where $d \Phi_B = B \cos \theta dA = \vec{B} \cdot d \vec{A}$. The total magnetic flux through the surface is $\Phi_B = \int B \cos \theta dA = \int \vec{B} \cdot d \vec{A}$.

The unit of the magnetic flux is **weber** or $Wb$, where $1 Wb = 1 T \cdot m^2$.

The Gauss's law for magnetism states that the total magnetic flux through a closed surface would be proportional to the total magnetic charge enclosed. Because magnetic monopole doesn't exist, $\oint \vec{B} \cdot d \vec{A} = 0$.

## Motion of Charged Particles

Motion of a charged particle under the action of a magnetic field alone is motion with constant speed, because the force $\vec{F}$ is perpendicular to $\vec{v}$.

If the direction of $\vec{v}$ is perpendicular to the field, the particle moves in a circle of radius $R$ with constant speed $v$. The centripetal acceleration is $\frac{v^2}{R}$, where $F = |q|vB = \frac{mv^2}{R} \implies R = \frac{mv}{|q|B}$. The angular speed $\omega = \frac{v}{R} = \frac{|q|B}{m}$ and the frequency is $f = \frac{\omega}{2\pi}$.

If the direction of $\vec{v}$ is not perpendicular to the field, the component of $\vec{v}$ parallel to the field is constant, and the particle moves in a helix.

## Wire

The magnetic field exerts a force on a wire due to the motion of its charge acrriers. In a short wire segment, each charge carrier feels a force $F_q = q v_d \times B$. Assume that the area is $A$, the length is $l$, and the carrier density is $n$, then the total number of charge carriers in the wire segment is $N = nAl$, and the magnitude of the total magnetic force is $F = N F_q = (nAl)(q v_d B \sin \theta) = F = IlB \sin \theta$, where $I$ is the magnitude of the current.

Let $\vec{l}$ be a vector with direction pointing along the direction of the current, $\vec{F} = I \vec{l} \times \vec{B}$. When the wire is curved and the magnetic field is not uniform, $d\vec{F} = I d\vec{r} \times \vec{B}$.

## Current Loop

Let the vector magnetic moment of a planar loop of current $I$ and enclosed area $A$ be $\mu = IA$, where the direction of $\vec{\mu}$ is defined to be perpendicular to the plane of the loop.

For a rectangular current loop in a uniform magnetic field, the magnitude of net torque is $\tau = 2F(\frac{b}{2}) \sin \theta = (IBa)(b \sin \theta) = IBA \sin \theta = \mu B \sin \theta$, where $A$ is the area of the current loop. The net torque is defined as $\vec{\tau} = \vec{\mu} \times \vec{B}$. The torque is $0$ when $\vec{\tau}$ and $\vec{B}$ are parallel or antiparallel.

The magnetic dipole is defined as an object that experiences a magnetic torque defined above. When a magnetic dipole changes orientation in a magnetic field, the field does work on it. The torque on a magnetic dipole in a magnetic field is $\vec{\tau} = \vec{\mu} \times \vec{B}$, thus the potential energy is $U = -\vec{\mu} \cdot \vec{B} = \mu B \cos \phi$.

## The Hall Effect

Let $q$ be a positive charge carrier moving with drift velocity $\vec{v_d}$ in a wire of cross section area $A = wh$. The wire is in a uniform magnetic field $\vec{B}$ perpendicular to the direction of current flow $I$. The magnetic force on the charge carrier moves them to a side of the wire, until the magnetic force is balanced with the electric force, where $q v_d B = qE \implies v_d = \frac{E}{B}$.

The electric potential $\Delta V$ can be measured, and the whether the charge carriers are positive or negative can be determined. The magnitude of the electric field inside the wire is $E = \frac{|\Delta V|}{w}$.

Since the current in the wire is $I = JA = (nq v_d) A$, the density of charge carriers is $n = \frac{I}{q v_d A} = \frac{IB}{qEA}$. $n$ is expressed in number of charge carriers (electrons or holes) per cubic meter and $q$ is the electric charge per each charge carrier.
