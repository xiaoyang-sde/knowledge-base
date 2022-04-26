# Pumping Lemma

## Definition

Let $L$ be a regular language, then there **exists** a $P \in N$ such that **for all** $w \in L$ of length $\geq P$, there **exists** $w = xyz$ where $x, y, z$ are strings, $y \neq \epsilon$, $|xy| \leq P$, and $xy^iz \in L$ **for all** $i \in 0, 1, 2, \dots$.

Assume that there's a DFA for $L$ with $P$ states. Run the DFA on $w \in L$, which ends in an accept state. There's a state $q$ that is visited twice while reading $w_1 w_2 \dots w_P$, which results in a loop.

- Let $x$ be he part of $w$ before the first loop
- Let $y$ be he part of $w$ inside the first loop
- Let $z$ be he part of $w$ after the first loop

Therefore, $x, y, z$ are strings, $y \neq \epsilon$, $|xy| \leq P$, and $xy^iz \in L$ for $i \in 0, 1, 2, \dots$.

## Contrapositive Form

Let $L$ be a non-regular language, then **for all** $P \in N$, there **exists** a $w \in L$ of length $\geq P$ such that **for all** $P = xyz$ where $x, y, z$ are strings, $y \neq \epsilon$, $|xy| \leq P$, there **exists** an $i$ such that $xy^iz \notin L$.

## Example

- The language $L = \{ 0^n 1^n: n \ge 1 \}$ is not regular. Let $P$ be an arbitrary number, define $w = \underbrace{0 \dots 0}_P \underbrace{1 \dots 1}_P \in L$. There are $x, y, z$ such that $x, y, z$ are strings, $y \neq \epsilon$, and $|xy| \leq P$, which implies that $xy^2z \notin L$.  $w$ could be partitioned into $\underbrace{0 \dots 0}_x \underbrace{0 \dots 0}_y \underbrace{0 \dots 1 \dots 1}_z$. Therefore, $L$ is not regular.

- The language $L = \{ 0^n 1^m: n > m \}$ is not regular. Let $P$ be an arbitrary number, define $w = \underbrace{0 \dots 0}_{P + 1} \underbrace{1 \dots 1}_P$. For all $P = xyz$, $xy^0z  = xz \notin L$. Therfore, $L$ is not regular.

- The language $L = \{ 1 0^n 1^n: n \geq 1 \}$ is not regular. Let $P$ be an arbitrary number, define $w = 1 \underbrace{0 \dots 0}_P \underbrace{1 \dots 1}_P$.  For all $P = xyz$, $xy^0z = xz = 0 \dots 0 1 \dots 1 \notin L$. Therfore, $L$ is not regular.
