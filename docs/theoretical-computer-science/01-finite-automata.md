# Finite Automata

## Deterministic Finite Automata

### Basic Notion

- The **alphabet** is a finite set of symbols. (e.g. ${ a, b, c, \dots, z }$)
- The **string** is a finite sequence of symbols from a given alphabet. The string could be empty.
- The **language** is a set of strings over a given alphabet. The language could be empty.
- The **computational device** is a mechanism that inputs a string and either accpets or rejects it.

### Definition

The **deterministic finite automata** is defined as a tuple $\{Q, \Sigma, \delta, q_0, F\}$, in which $Q$ is the set of states, $\Sigma$ is the alphabet, $\delta$ is the transition function ($\delta: Q \times \Sigma \rightarrow Q$), $q_0$ is the start state ($q_0 \in Q$), and $F$ is the set of accept states ($F \subseteq Q$).

- The deterministic finite automata accepts a string $w = w_1 w_2 \dots w_n$ if and only if $\delta(\dots \delta(\delta(q_0, w_1), w_2) \dots, w_n) \in F$.
- The deterministic finite automata recognizes the language $L$ if and only if $L = \{w: D \text{ accepts } w\}$.

The deterministic finite automata could be represented with a diagram that consists of a specific alphabet, the start state, the accept states, and the transitions of the states for all symbols. The language of a deterministic finite automata is the set of all strings it accpets.

## Non-deterministic Finite Automata

The **non-deterministic finite automata** is defined as a tuple $\{Q, \Sigma, \delta, q_0, F\}$, in which $Q$ is the set of states, $\Sigma$ is the alphabet, $\delta$ is the transition function ($\delta: Q \times ( \Sigma \cup \{\epsilon \} ) \rightarrow \mathcal{P}(Q)$), $q_0$ is the start state ($q_0 \in Q$), and $F$ is the set of accept states ($F \subseteq Q$).

- The non-deterministic finite automata accepts a string $w = \sigma_1 \sigma_2 \dots \sigma_{m - 1}$ if and only if $\exists q_1, \dots, q_m \in Q, \sigma_1, \dots, \sigma_{m - 1} \in \Sigma \cup \{ \epsilon \}$, there's $q_1 \in \delta(q_0, \sigma_0), \dots, q_m \in \delta(q_{m - 1}, \sigma_{m - 1})$ and $q_{m} \in F$, which means an accept state is reachable from $q_0$ through some path on input $w$.
- The non-deterministic finite automata recognizes the language $L$ if and only if $L = \{w: N \text{ accepts } w\}$.

The non-deterministic finite automata could be represented with a diagram that consists of a specific alphabet, the start state, the accept states, and the transitions of the states. Each state could have $0$ or a few transitions for each symbol or empty symbol $\epsilon$. The non-deterministic finite automata accepts $w$ if there is at least one path to an accept state.

## Equivalence of DFA and NFA

General theorem: Each non-deterministic finite automata $N$ could be converted to a deterministic finite automata $D$ that recognizes the same language.

Proof: Given NFA $N = \{Q, \Sigma, \delta, q_0, F\}$, define $D = \{\mathcal{Q}, \Sigma, \Delta, \mathcal{S}_0, \mathcal{F}\}$:

- $\mathcal{S}_0 = \{ q \in Q: q \text{ is reachable from } q_0 \text{ through a path } \epsilon \dots \epsilon \}$
- $\Delta (\mathcal{S}, \sigma) = \{ q \in Q: q \text{ is reachable from } q_0 \text{ through a path } \sigma \epsilon \dots \epsilon \}$
- $\mathcal{F} = \{ \mathcal{S} \subseteq Q: \mathcal{S} \text{ contains a state in } F \}$

**The language $L$ is regular if it is recognized by some NFA or DFA.** If $L$ has an NFA with $k$ states, then $L$ has an DFA with $2^k$ states. Define $\Sigma = \{0, 1\}$, $k$ be a positive integer, $L_k = \{w: w \text{ has a } 0 \text{ in } k^{\text{th}} \text{ positiion from the end }\}$, then $L_k$ has an NFA of size $k + 1$ and $L_k$ doesn't have a DFA of size less than $2^k$.

Proof: Let $D$ be a DFA with less than $2^k$ states. If the input is $2^k$ strings of length $k$, then the end state couldn't be all distinct. (Pigenhole principle) There exists two strings $u$ and $v$ with length of $k$ that the $i^\text{th}$ bit of $u$ and $v$ is different with the same end state. Therefore, $u0\dots0$ and $v0\dots0$ with $k - 1$ of $0$ appended from the end has the same end state, which means that $D$ can't recognize $L_k$.

## Closure

- **Complement**: If $L$ is regular, then $\bar{L}$ is regular. The complement of the language $L$ is defined as $\bar{L} \{ w, w \not \in L \}$.

  Proof: If DFA $\{Q, \sigma, delta, q_0, F\}$ recognizes $L$, then DFA $\{Q, \sigma, \delta, q_0, Q - F\}$ recognizes $L$.

- **Union and Intersection**: If $L'$ and $L''$ are regular, then $L' \cup L''$ and $L \cap L''$ are regular. Therefore, If $L_1, L_2, \dots, L_n$ are regular, then $L_1 \cup L_2 \cup \dots \cup L_n$ and $L_1 \cap L_2 \cap \dots L_n$ are regular. (Induction)

  Proof: Let the DFA of $L'$ be $\{Q', \Sigma, \delta', q_0', F'\}$ and the DFA of $L''$ be $\{Q'', \Sigma, \delta'', q_0'', F''\}$.

  - The DFA of $L' \cup L''$ is $\{ Q' \times Q'', \Sigma, \delta, (q_0', q_0''), (F' \times Q'') \cup (F'' \times Q') \}$, in which $\delta((q', q''), \sigma) = (\delta'(q', \sigma), \delta''(q'', \sigma))$.

  - The DFA of $L' \cap L''$ is $\{ Q' \times Q'', \Sigma, \delta, (q_0', q_0''), F' \times F'' \}$, in which $\delta((q', q''), \sigma) = (\delta'(q', \sigma), \delta''(q'', \sigma))$.

  - **Each finite language is regular.** Let $L$ be finite, $L = \bigcup_{w \in L} \{w \}$ is regular.

- **Difference**: If $L'$ and $L''$ are regular, then $L' - L''$ is regular. The difference between $L'$ and $L''$ is defined as $L' - L'' = \{ w \in L': w \notin L'' \}$.

  Proof: $L' - L'' = L' \cup \bar{L''}$.

  - If $A$, $S$ are regular, then $L = \{ w \in A \text{ : no substring of } w \text{ is in } S \}$ is regular.

- **Concatenation**: If $L$ and $L'$ are regular, then $L'L''$ is regular. The concatenation between $L'$ and $L''$ is defined as $L'L'' = \{ w'w'': w' \in L', w'' \in L'' \}$. To construct an NFA from the concatenation of two NFAs, make the states in $F'$ reject, and add $\epsilon$ arrows from the states in $F'$ to $q_0^{''}$.
  - $L \emptyset = \emptyset$, $\emptyset L = \emptyset$
  - $L \{ \epsilon \} = L$, $\{ \epsilon \} L = L$

- **Kleene star**: If $L$ is regular, then $L*$ is regular. The kleene star of $L$ is defined as $L^* = \{ w: w \text{ is the concatenation of zero or more strings in L} \}$. To construct an NFA that recognizes $L*$ from the NFA that recognizes $L$, create a accepting dummy start state, add an $\epsilon$ arrow from the dummy start state to $q_0$ and $\epsilon$ arrows from states in $F$ to $q_0$, and then add the dummy start state to $F$.
  - Example: Let $L = \{ a, b \}$, then $L* = \{ \epsilon, a, b, aa, ab, ba, bb, \dots \}$.

- **Reverse**: If $L$ is regular, then $L^R$ is regular. The reverse of $w = w_1 w_2 \dots w_n$ is defined as $w^R = w_n, w_{n - 1}, \dots, w_1$. The reverse of $L$ is defined as $L^R = \{ w^R \in L \}$. To construct an NFA that recognizes $L^R$ from the NFA that recognizes $L$, reverse all arrows, create new start state and link it with $\epsilon$ to states in $F$, and set $F$ to $\{ q_0 \}$.

- **Prefix and Suffix**: The prefix of $L$ is defined as $\text{prefix}(L) = \{ u: uw \in L \text{ for some string u that might be empty} \}$. The suffix of $L$ is defined as $\text{suffix}(L) = \{ u: wu \in L \text{ for some string u that might be empty} \}$.

- **Substring**: The substring of $L$ is defined as $\text{substring}(L) = \{ v: uvw \in L \text{ for some strings} u, w \} = \text{suffix}(\text{prefix}(L))$.

- **Shuffle**: The shuffle of $L'$ and $L''$ is defined as $\text{shuffle}(L', L'') = \{ a_1 b_1 a_2 b_2 \dots a_k b_k: a_1, \dots, a_k, b_1, \dots b_k, a_1 a_2 \dots a_k \in L', b_1, b_2, \dots, b_k \in L'' \}$.
