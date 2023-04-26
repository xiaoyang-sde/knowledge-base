# Inductance

## Mutual Inductance

Let there be a system with multiple loops. Each loop produces a magnetic field which flows through itself and the other loops.

The mutual inductance of the loop $j$ and the loop $k$ is defined as the ratio of the emf $\varepsilon$ induced in a loop to the rate of change of current in the other loop, which is $M_{jk} = \frac{\Phi_{k \to j}}{I_k} = \frac{\Phi_{j \to k}}{I_j}$, where $\Phi_{k \to j}$ is the flux through loop $j$ due to current $I_k$. The coefficient of mutual inductance satisfies the rule that $M_{jk} = M_{kj}$. The unit of mutual inductance is $H$ (henry), where $1 H = 1 \frac{Wb}{A}$.

If the current $I_k$ is changing over time, then the magnetic flux through loop $J$ is changing over time, which induces an emf $\varepsilon_{k \to j} = - \frac{d \Phi_{k \to j}}{dt} = - \frac{d}{dt} (M_{jk} I_k) = - \frac{d I_k}{dt} M_{kj} - \frac{d M_{kj}}{dt} I_k$ in loop $J$. If the loops are in vaccum, $\frac{dM_{kj}}{dt} = 0$, thus $\varepsilon_{k \to j} = - \frac{d I_k}{dt} M_{kj}$.

## Self-Inductance

For a standalone circuit loop, the current sets up a magnetic field that causes a magnetic flux through the circuit. If the current changes, the flux changes, which induces an emf on itself. The self-induced emf opposes the change in the current that caused the emf. The coefficient of self inductance is $L_k = M_{kk} = \frac{\Phi_{k \to k}}{I_k}$ and the self-induced emf is $\varepsilon = - \frac{d \Phi_{k \to k}}{dt} = - \frac{d}{dt} (L_k I_k) = -L_k \frac{d I_k}{dt}$, where $L_k$ is assumed to be a constant.

## Inductor

The circuit device that is designed to have a particular inductance is called an inductor or a choke. Their purpose is to oppose variations in the current through the circuit. When an inductor is included in the circuit, induced electric field within the coils of the inductor is not conservative, which is $\vec{E}_n$.

In an ideal inductor with negligible resistance, the energy stored in the magnetic field around the coil is entirely converted into electrical energy as the current flows through the coil. Since the inductor has an emf that resists the current, a small electric field is required to make charge move through the coils, so the total electric field is $\vec{E}_c + \vec{E}_n = 0$.

In a circuit loop with variable current $I$, the $\vec{E}_n$ exists in the inductor $ab$ and Faraday's law states that $\int_{a}^{b} \vec{E_n} d \vec{l} = -\int_{a}^{b} \vec{E_c} d \vec{l} = \varepsilon = -L \frac{dI}{dt}$, which is equal to the potential difference of point $a$ with respect to point $b$. Therefore, $V_{ab} = V_a - V_b = L \frac{dI}{dt}$.

## Magnetic Field Energy

The inductor stores energy when the current increases and releases the energy when the current decreases. Establishing a current in an inductor requires an input of energy, and an inductor carrying a current has energy stored in it.

Let $U$ be the total energy inpu  needed to establish a final current $I$ in an inductor with inductance $L$ if the initial current is $0$. The voltage between the terminals of the inductor $a$ and $b$ is $V_{ab}$ and the rate at which the energy is delivered to the inductor is $P = V_{ab} I = L I \frac{dI}{dt}$. The energy $dU$ supplied to the inductor during an infinitesimal time interval $dt$ is $dU = P dt = L I dI$. Therefore, the total energy supplied from $I = 0$ to $I = I_f$ is $U = L \int_{0}^{I_f} I dI = \frac{1}{2} L I_f^2$.

After the current has reached its final steady value $I_f$, $\frac{dI}{dt} = 0$ and no more energy is supplied to the inductor. When the current decreases from $I_f$ to $0$, the inductor acts as a source that supplies a total amount of energy $U = \frac{1}{2} L I_f^2$ to the external circuit.
