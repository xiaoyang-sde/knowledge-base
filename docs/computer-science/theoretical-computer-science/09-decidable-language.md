# Decidable Language

## Definition

The Turing machine $M$ decides $L$ if $M$ recognizes $L$ and $M$ halts on all inputs, which means that either acceptance or rejection are both explicit. $L$ is decidable if some Turing machine decides it.

The language $L$ is decidable if $L$ is Turing-recognizable and $\bar{L}$ is Turing-recognizable. Let $M_1$ be a recognizer for $L$ and $M_2$ be a recongnizer for $\bar{L}$. Let $M$ be a Turing machine that runs both $M_1$ and $M_2$ on input $w$ in parallel. $M$ accepts if $M_1$ accepts, and rejects if $M_2$ rejects. $M$ is a decider of $L$ because it accepts all strings in $L$ and rejects all strings not in $L$.

## Example

### Finite Automata

Let $A_{\text{DFA}} = \{ (B, w) | B \text{ is a DFA that accepts } w \}$. The language is decidable. Let $M$ be a Turing machine that simulates $B$ on input $w$. If the simulation ends in an accept state, $M$ accepts the input $(B, w)$. $M$ records $B$'s current state and position in the input $w$ in its tape and updates the tape based on the transition function $\delta$ of $B$. If the simulation ends in a non-accept state, $M$ accepts the input $(B, w)$.

Let $A_{\text{NFA}} = \{ (B, w) | B \text{ is an NFA that accepts } w \}$. The language is decidable. Let $M$ be a Turing machine that converts $B$ to a DFA and simulates the DFA on the input $w$.

Let $A_{\text{REX}} = \{ (R, w) | R \text{ is a regular expression that accepts } w \}$. The language is decidable. Let $M$ be a Turing machine that converts $R$ to a DFA and simulates the DFA on the input $w$.

Let $E_{\text{DFA}} = \{ (A) | A \text{ is a DFA and } L(A) = \emptyset \}$. The language represents the DFA that doesn't accept any string. The language is decidable. Let $M$ be a Turing machine that iteratively marks the states that have a transition coming into it from other marked states. If no accept state is marked, $M$ accepts the input $(A)$.

Let $EQ_{\text{DFA}} = \{ (A, B) | A, B \text{ are DFAs and } L(A) = L(B) \}$. The language represents the equivalence paris of DFAs. The language is decidable. Let $C$ be a DFA that accepts the strings that are accepted by either $A$ or $B$ but not both. $L(C) = (L(A) \cap \bar{L(B)}) \cup (\bar{L(A)} \cap L(B))$. If $L(C) = \emptyset$, then $L(A) = L(B)$.

### Context-Free Language

Let $A_{\text{CFG}} = \{ (G, w) | G \text{ is a CFG that generates string } w \}$. The language is decidable. Let $M$ be a Turing machine that converts $G$ into Chomsky normal form and lists all derivations with $2n - 1$ steps, where $n$ is the length of $w$. If some derivations generate $w$, $M$ accepts.

Let $E_{\text{CFG}} = \{ (G) | G \text{ is a CFG and } L(A) = \emptyset \}$. The language is decidable. Let $M$ be a Turing machine that marks all terminal symbols in $G$ and iteratively marks the variable $A$ where $G$ has a rule $A \to U_1 U_2 \dots U_k$ that each $U$ has been marked. $M$ accepts if the start variable is not marked.

Let $EQ_{\text{CFG}} = \{ (G, H) | G, H \text{ are CFGs and } L(G) = L(H) \}$. The language is undecidable because CFGs are not closed under complement or intersection.

Each context-free language is decidable. Let $M$ be the Turing machine that decides $A_{\text{CFG}} = \{ (G, w) | G \text{ is a CFG that generates string } w \}$. The Turing machine $M_G$ accepts an input $w$ and runs $M$ on the input $(G, w)$. If $M$ accepts, $M_G$ accepts.
