# Myhill-Nerode Theorem

The Myhill-Nerode theorem is a fundamental result in the theory of regular languages. It can be used to prove whether or not a language $L$ is regular and it can be used to find the minimal number of states in a DFA which recognizes $L$ if $L$ is regular. **The Myhill-Nerode theorem states that for a language $L$, the minimum number of states in a DFA for $L$ is equal to the number of equivalence classes of $L$. If the number of equivalence classes is infinite, the langauge is non-regular.**

The motivation of the theorem is that the pumping lemma is not sufficient to prove that a language is non-regular. (Example: $\{ L = 0^{*} 1^{*} \cup \{ 1^m 0^n 1^n: m \geq 1, n \geq 0 \} \}$)

## Indistinguishable

For the langauge $L$, the strings $x$ and $y$ are $L$-indistinguishable if for all strings $w$, $xw \in L \iff yw \in L$. For example, let $L = \{ w: |w| \text{ is divisible by } 3 \}$, then $1 \equiv_L 0$, $\epsilon \equiv_L 000$, and $\epsilon \not \equiv_L 00$.

For the language $L$, the $\equiv_L$ is an equivalence relation, which means that $x \equiv_L x$ (reflective), $x \equiv_L y \iff y \equiv_L x$ (symmetric), and $x \equiv_L y, y \equiv_L z \implies x \equiv_L z$ (transitive). The set $\Sigma^*$ could be divided into several classes of equivalence relations, such that a class is either in $L$ or in $\bar{L}$.

- Let $L = \{ w: |w| \text{ is divisible by } 3 \}$. The set $\Sigma^*$ could be divided into $(\Sigma \Sigma \Sigma)^*$, $\Sigma (\Sigma \Sigma \Sigma)^*$, and $\Sigma \Sigma (\Sigma \Sigma \Sigma)^*$.

- Let $L = \{ w: \text{ each 0 is followed by a 1 }\} = 1^{*} (01^+)^{*}$. The set $\Sigma^*$ could be divided into $\Sigma^{*} 00 \Sigma^{*}$, $1^{*} (01^+)^{*}$, and $1^{*} (01^+)^{*}0$.

- Let $L = \{ w: w \text{ does not contain both a 0 and an 1} \} = 0^{*} \cup 1^{*}$. The set $\Sigma^*$ could be divided into $\epsilon$, $0^{+}$, $1^{+}$, $\Sigma^{*} 01 \Sigma^{*} \cup \Sigma^{*} 10 \Sigma^{*}$.

- Let $L = \{ w: w \text{ contains as many 0s as 1s} \}$. The language is non-regular. The set $\Sigma^*$ could be divided into $C_i = \{w: \text{the number of 0s in } w - \text{ the number of 1s in } w = i \}$.

- Let $L = \{ w: w^{R} = w \}$. The langauge represents all palindromes formed with $\Sigma$. Each string in $\Sigma^*$ is an equivalence class.

## Proof of Myhill-Nerode Theorem

- **Lower bound**: Let $D$ be a DFA for $L$ with $k$ states. Suppose that there exists strings $x_1, x_2, \dots, x_{k+1}$, each in a different equivalence class of $\equiv_L$. For some $x_i, x_j$ where $x_i \neq x_j$, the DFA ends up in the same state while reading $x_i$ and $x_j$. Therefore, $x_i w \in L \iff x_j w \in L$, thus $x_i \equiv_L x_j$, which contradicts to the assumption that $x_i$ and $x_j$ are in different equivalence classes. Therefore, the minimum number of states in a DFA for $L$ is **greater than or equal to** the number of equivalence classes of $L$.

- **Upper bound**: Let $k$ be the number of equivalence classes in $L$. Pick a representative from each class. Let $D$ be a DFA for $L$ with states $\{ s_1, s_2, \dots, s_k \}$, start state $s_1$, and accept states $\{ s_1, s_2, \dots, s_k \} \cap L$. The transition $\delta(s_i, \sigma) = s_k$ if $s_i \sigma \equiv_L s_k$. Therefore, the minimum number of states in a DFA for $L$ is **less than or equal to** the number of equivalence classes of $L$.

## Example

- Let $L = \{ w: |w| \text{ divisible by } k \}$. If $i \neq j (\text{ mod } k) \implies 0^i 0^{k-i} \in L_k, 0^j 0^{k - i} \notin L_K$. The smallest possible DFA contains $k$ states because $\epsilon, 0, 00, 000, \dots, 0^{k - 1}$ are in different $equiv_L$. For each $k$, $L_k$ is a language that can be recognized by a DFA with $k$ states but not $k - 1$ states.

- Given $u = u_1 u_2 \dots u_k$, let $L = \{ \Sigma^{*} u_1 u_2 \dots u_k \Sigma^{*} \}$. The smallest possible DFA contains $k - 1$ states because $\epsilon, u_1, u_1 u_2, \dots, u_1 u_2 \dots u_k$ are in different classes of $\equiv_L$.
