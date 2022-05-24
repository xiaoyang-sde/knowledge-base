# Context Free Grammar

The **context free grammar** is a tuple $(V, \Sigma, R, S)$ where $V$ is the set of variables, $\Sigma$ is the set of alphabets, $R$ is the set of rules, and $S$ is the start variable. The **language of a context free grammar** is the set of all strings that can be derived.

## Example

- $(\{ S, A, B \}, \{ a, b \}, \{ S \rightarrow A | B, A \rightarrow aAb | \epsilon, B \rightarrow bBa | \epsilon \}, S)$ represents the language $L = a^n b^n \cup b^n a^n (n \geq 0)$.
- $(\{ S, A \}, \{ a, b \}, \{ S \rightarrow AA, A \rightarrow A \}, S)$ represents the language $L = \emptyset$.
- $(\{ S \}, \{ a, b \}, \{ S \rightarrow abS | a \}, S)$ represents the language $L = (ab)^* a$.
- $(\{ S, B \}, \{ a, b \}, \{ S \rightarrow aSa | B, B \rightarrow bB | \epsilon \}, S)$ represents the language $L = a^n b^* a^n (n \geq 0)$.
- $(\{ S, A, B \}, \{ a, b \}, \{ S \rightarrow AB, A \rightarrow aAa |\epsilon, B \rightarrow bBb | \epsilon \}, S)$ represents the language $L = (aa)^{*} (bb)^{*}$.
- $(\{ S, T \}, \{ a, b \}, \{ S \rightarrow \epsilon | aT | bT, T \rightarrow aS | bS \}, S)$ represents the language $L = (\Sigma \Sigma)^{*}$.
- $(\{ S \}, \{ a, b \}, \{ S \rightarrow \epsilon | aS | aSb \}, S)$ represents the language $L = \{ a^n b^m: n \geq m \}$.
- $(\{ S \}, \{ a, b \}, \{ S \rightarrow a | aS | aSb \}, S)$ represents the language $L = \{ a^n b^m: n > m \}$.
- $(\{ S, G \}, \{ a, b \}, \{ S \rightarrow GabbG, G \rightarrow aG | bG | \epsilon \}, S)$ represents the language $L = \{ w: w \text{ contains } abb \}$.
- $(\{ S, G \}, \{ a, b \}, \{ S \rightarrow GaGaGaG, G \rightarrow aG | bG | \epsilon \}, S)$ represents the language $L = \{ w: w \text{ at least three } a \}$.
- $(\{ S, L, R \}, \{ a, b \}, \{ S \rightarrow LR, L \rightarrow aGb | \epsilon, R \rightarrow bGa|\epsilon \}, S)$ represents the language $L = \{ a^n b^{m + n} a^m: n, m \geq 0 \}$.

## Programming Language

Let $L$ be all valid programs defined in the table below} and $\Sigma = \{ a, b, \dots, z, 0, 1, \dots, 9, >, =, <, \text{space}, \text{newline} \}$.

|Field|Definition|Context Free Grammar|
|-|-|-|
|Identifier|$(a \cup \dots \cup z)(a\cup \dots \cup z \cup 0 \cup \dots \cup 9)^*$ |$L \rightarrow a\|b\|\dots\|z$ <br /> $I \rightarrow ID\|IL\|L$|
|Number|Positive Integer|$D^+ \rightarrow 1\|2\|\dots\|9$ <br /> $D \rightarrow 0\|D^+$ <br /> $N \rightarrow ND \| D^+$ <br /> $Q \rightarrow I \| N $|
|Statement|Assignment, Conditional| $W \rightarrow \text{space} \| \text{newline} \| WW$ (Conditional) <br /> $W^{'} \rightarrow W \| \epsilon$ <br /> $T \rightarrow QW^{'} = W^{'}Q \| QW^{'} < W^{'}Q \| QW^{'} > W^{'}Q$ (Test) <br /> $A \rightarrow IW^{'} = W^{'}Q$ (Assignment) <br /> $C \rightarrow \text{if}WTW\text{then}WS \| \text{if}WTW\text{then}WSW\text{else}WS$ |
|Program|Single statement|$S \rightarrow A\|C$|

## Ambiguious Grammar

The context free grammar is ambiguous if some string has more than one parse tree. Otherwise, the grammar is unambiguous.

- The number of $b$'s is equal to the number of $a$'s in the string. The number of $b$'s is less than or equal to the number of $a$'s in each prefix of the string.
  - $P \rightarrow ab | aPb | abP | aPbP$
- The number of $b$'s is equal to the number of $a$'s in the string.
  - $S \rightarrow S' | S'' | \epsilon$
  - $S' \rightarrow P | PS''$
  - $S'' \rightarrow Q | QS'$
  - $P \rightarrow ab | aPb | abP | aPbP$
  - $Q \rightarrow ba | bQa | baQ | bQaQ$
- Each prefix of the string has at least same number of $a$'s as $b$'s.
  - $S \rightarrow \epsilon | P | aS | PaS$
  - $P \rightarrow ab | aPb | abP | aPbP$
