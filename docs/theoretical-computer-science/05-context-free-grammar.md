# Context Free Grammar

The **context free grammar** is a tuple $(V, \Sigma, R, S)$ where $V$ is the set of variables, $\Sigma$ is the set of alphabets, $R$ is the set of rules, and $S$ is the start variable. The **language of a context free grammar** is the set of all strings that can be derived.

## Example

- $(\{ S, A, B \}, \{ a, b \}, \{ S \to A | B, A \to aAb | \epsilon, B \to bBa | \epsilon \}, S)$ represents the language $L = a^n b^n \cup b^n a^n (n \geq 0)$.
- $(\{ S, A \}, \{ a, b \}, \{ S \to AA, A \to A \}, S)$ represents the language $L = \emptyset$.
- $(\{ S \}, \{ a, b \}, \{ S \to abS | a \}, S)$ represents the language $L = (ab)^* a$.
- $(\{ S, B \}, \{ a, b \}, \{ S \to aSa | B, B \to bB | \epsilon \}, S)$ represents the language $L = a^n b^* a^n (n \geq 0)$.
- $(\{ S, A, B \}, \{ a, b \}, \{ S \to AB, A \to aAa |\epsilon, B \to bBb | \epsilon \}, S)$ represents the language $L = (aa)^{*} (bb)^{*}$.
- $(\{ S, T \}, \{ a, b \}, \{ S \to \epsilon | aT | bT, T \to aS | bS \}, S)$ represents the language $L = (\Sigma \Sigma)^{*}$.
- $(\{ S \}, \{ a, b \}, \{ S \to \epsilon | aS | aSb \}, S)$ represents the language $L = \{ a^n b^m: n \geq m \}$.
- $(\{ S \}, \{ a, b \}, \{ S \to a | aS | aSb \}, S)$ represents the language $L = \{ a^n b^m: n > m \}$.
- $(\{ S, G \}, \{ a, b \}, \{ S \to GabbG, G \to aG | bG | \epsilon \}, S)$ represents the language $L = \{ w: w \text{ contains } abb \}$.
- $(\{ S, G \}, \{ a, b \}, \{ S \to GaGaGaG, G \to aG | bG | \epsilon \}, S)$ represents the language $L = \{ w: w \text{ at least three } a \}$.
- $(\{ S, L, R \}, \{ a, b \}, \{ S \to LR, L \to aGb | \epsilon, R \to bGa|\epsilon \}, S)$ represents the language $L = \{ a^n b^{m + n} a^m: n, m \geq 0 \}$.

## Programming Language

Let $L$ be all valid programs defined in the table below} and $\Sigma = \{ a, b, \dots, z, 0, 1, \dots, 9, >, =, <, \text{space}, \text{newline} \}$.

|Field|Definition|Context Free Grammar|
|-|-|-|
|Identifier|$(a \cup \dots \cup z)(a\cup \dots \cup z \cup 0 \cup \dots \cup 9)^*$ |$L \to a\|b\|\dots\|z$ <br /> $I \to ID\|IL\|L$|
|Number|Positive Integer|$D^+ \to 1\|2\|\dots\|9$ <br /> $D \to 0\|D^+$ <br /> $N \to AND \| D^+$ <br /> $Q \to I \| N $|
|Statement|Assignment, Conditional| $W \to \text{space} \| \text{newline} \| WW$ (Conditional) <br /> $W^{'} \to W \| \epsilon$ <br /> $T \to QW^{'} = W^{'}Q \| QW^{'} < W^{'}Q \| QW^{'} > W^{'}Q$ (Test) <br /> $A \to IW^{'} = W^{'}Q$ (Assignment) <br /> $C \to \text{if}WTW\text{then}WS \| \text{if}WTW\text{then}WSW\text{else}WS$ |
|Program|Single statement|$S \to A\|C$|

## Ambiguous Grammar

The context free grammar is ambiguous if some string has more than one parse tree. Otherwise, the grammar is unambiguous.

- The number of $b$'s is equal to the number of $a$'s in the string. The number of $b$'s is less than or equal to the number of $a$'s in each prefix of the string.
  - $P \to ab | aPb | abP | aPbP$
- The number of $b$'s is equal to the number of $a$'s in the string.
  - $S \to S' | S'' | \epsilon$
  - $S' \to P | PS''$
  - $S'' \to Q | QS'$
  - $P \to ab | aPb | abP | aPbP$
  - $Q \to ba | bQa | baQ | bQaQ$
- Each prefix of the string has at least same number of $a$'s as $b$'s.
  - $S \to \epsilon | P | aS | PaS$
  - $P \to ab | aPb | abP | aPbP$
