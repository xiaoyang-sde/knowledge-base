# Undecidable Language

## Diagonalization

Let $A$ and $B$ be two sets and $f$ be a function from $A$ to $B$. $f$ is one-to-one if $f(a) \neq f(b)$ whenever $a \neq b$. $f$ is onto if it hits all elements of $B$. If $f$ is both one-to-one and onto, $A$ and $B$ are the same size because each element of $A$ maps to a unique element of $B$ and each element of $B$ has a unique element of $A$ mapped to it.

If the set $A$ is either finite or has the same size as $N$, then $A$ is **countable**. If the set $A$ doesn't have a correspondence with $N$, then $A$ is **uncountable**. For example, the set of rational numbers $Q$ is countable, but the set of real numbers $R$ is uncountable.

The set of Turing machines is countable, but the set of all languages is uncountable. Therefore, some language are not Turing-recognizable.

Let $A_{\text{TM}} = \{ (M, w) | M \text{ is a Turing machine and accepts } w \}$. The language is Turing-recognizable and undecidable. Let $U$ be a Turing machine that simulates $M$ on the input $w$. If $M$ enters an accept state, $U$ accepts. However, $U$ loops if $M$ loops on $w$, which indicates that $U$ doesn't decide $A_{\text{TM}}$.

## Reduction

Reduction is a method to convert one problem $A$ to another problem $B$ such that a solution to $B$ could be used to solve $A$. If $A$ is reducible to $B$ and $B$ is decidable, then $A$ is decidable. If $A$ is undecidable and reducible to $B$, $B$ is undecidable.

The $\text{HALT}_{\text{TM}} = \{ (M, w) | M \text{ is a Turing machine and halts on input } w \}$ is undecidable because $A_{\text{TM}}$ is reducible to $\text{HALT}_{\text{TM}}$. Let R$ be a Turing machine that decides $\text{HALT}$. The Turing machine $S$ that accepts $(M, w)$, decides $A_{\text{TM}}$ could be constructed such that $S$ runs $R$ to determine if $M$ will loop and then accepts if $M$ has accepted $w$. Because $A_{\text{TM}}$ is undecidable, $\text{HALT}$ is undecidable.

## Rice's Theorem

Let $P$ be a non-trivial property of the language of a Turing machine. The problem of determining whether a given Turing machine's language has property $P$ is undecidable. Let $C$ a subset of a Turing-recognizable language such that $C \neq \emptyset$ and $\bar{C} \neq \emptyset$, then the Turing machine that recognizes $L(M) \in C$ is undecidable.
