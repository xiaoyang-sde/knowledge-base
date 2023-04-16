# Inductance

## Mutual Inductance

Let there be a system with multiple loops. ach loop produces a magnetic field which flows through itself and the other loops. Because a loop's magnetic field is proportional to its current, $\Phi_{k \to j} = M_{jk} I_k$ where $\Phi_{k \to j}$ is the flux through loop $j$ due to current $I_k$ and $M_{jk}$ is the coefficient of mutual inductance. The coefficient of mutual inductance satisfies the rule that $M_{jk} = M_{kj}$. The coefficient of self inductance is $L_i = M_{ii}$, which is non-negative.

If the current $I_k$ is changing over time, then the magnetic flux through loop $J$ is changing over time, which induces an emf $\varepsilon_{k \to j} = - \frac{d \Phi_{k \to j}}{dt} = - \frac{d I_k}{dt} M_{kj} - \frac{d M_{kj}}{dt} I_k$ in loop $J$.
