# Pushdown Automata

The **pushdown automata** is a finite state machine with a stack that could remember an infinite amount of information. The transition in the pushdown automata is consists of the input symbol to read or $\epsilon$, the symbol to pop from the stack or $\epsilon$, and the symbol to push to the stack or $\epsilon$.

The **pushdown automata** is defined as a tuple $(Q, \Sigma, \Gamma, \delta, q_0, F)$, where $Q$ is the set of the states, $\Sigma$ is the input alphabet, $\Gamma$ is the stack alphabet, $\delta: Q \times (\Sigma \cup \{ \epsilon \} \times (\Gamma \cup \{ \epsilon \})) \rightarrow (Q \times (\Gamma \cup \{ \epsilon \}))$ is the transition function, $q_0$ is the start state, and $F$ is the set of accept states.

## Equivalence of Pushdown Automata and Context-Free Grammar

The language $L$ is context-free if and only if it has a context-free grammar or a push-down automata. Each regular language is context-free because each non-deterministic finite automata is equivalent to a pushdown automata that doesn't use the stack.

### Convert Context-Free Grammar to Pushdown Automata

If $L$ has a context-free grammar, then $L$ could be recognized by a pushdown automata. Let the context-free grammar be $(V, \Sigma, R, S)$ and the pushdown automata contain the input alphabet $\Sigma$ and the stack alphabet $V \cup \Sigma \cup \{ \bot \}$. Let $X \rightarrow Y_1 Y_2 \dots Y_m \in R$.

- $\delta(\{ q_0, \epsilon, \epsilon \}) \rightarrow q_1, \bot$
- $\delta(\{ q_1, \epsilon, \epsilon \}) \rightarrow q_2, S$
- $\delta(\{ q_1, \epsilon, X \}) \rightarrow q_i, Y_m$
- $\delta(\{ q_i, \epsilon, \epsilon \}) \rightarrow q_{i + 1}, Y_{m - 1}$
- $\dots$
- $\delta(\{ q_{i + m - 1}, \epsilon, \epsilon \}) \rightarrow q_{i + m}, Y_{1}$
- $\delta(\{ q_2, \epsilon, \bot \})\rightarrow q_e, \epsilon$ ($q_e \in F$)

### Convert Pushdown Automata to Context-Free Grammar

- The pushdown automata has a unique accept state. (Create a new sentinel accpet state.)
- The pushdown automata empties the stack before accepting the input. (Rewind the stack before the sentinel accept state.)
- Each transition is either a push or a pop. (Break a single push-pop step into two seperate steps.)

If $L$ is recognized by a pushdown automata, then $L$ has a context-free grammar. Convert the pushdown automata into a new pushdown automata with the assumptions above with $\{ Q, \Sigma, \Gamma, S, q_0, \{ q_f \}\}$, $L$.

Let $L_{p, q}$ be the language that travels from $p$ with empty stack to $q$ with empty stack. Let the context-free grammar be $\{ V_{p, q \text{ for all } p, q \in Q}, \Sigma, V_\{q_0, q_f\} \}$.

- $V_{q, q} \rightarrow \epsilon$
- $V_{p, q} \rightarrow V_{p, r}, V_{r, q}$ if the stack becomes empty at $r$ for all $p, q, r \in Q$
- $V_{p, q} \rightarrow \sigma V_{r, s} \tau$ for all $p, q, r, s \in Q$, for all $\sigma, \tau \in \Sigma \cup \{ \epsilon \}$, for all $\gamma \in \Gamma$, such that $(r, \gamma) \in \delta(p, \sigma, \epsilon)$ and $(q, \epsilon) \in \delta(s, \tau, \gamma)$
