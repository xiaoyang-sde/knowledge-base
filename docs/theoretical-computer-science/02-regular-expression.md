# Regular Expression

## Definition

The regular expression is $\empty$, $\{ \epsilon \}$, or $\{ \sigma \}$ for $\sigma$ in  $\Sigma$. The regular expression $R$, $R_1$, and $R_2$ could be combined into $(R^*)$, $(R_1 \cup R_2)$, or $(R_1 \cdot R_2)$. The $\{ \}$, $()$, and $\cdot$ operators could be removed and the precedence of operators is $*$ (repeat `0` or more times), $\cdot$ (concatenation), and $\cup$ (or).

- $R^k = (R \cdot R \dots R \cdot R)$
- $R^+ = (R \cdot (R^*))$
- $\Sigma = (\{ 0 \} \cup \{ 1 \})$

## Example

- $\{ w: w \text{ contains exactly one } 1 \} = 0^{*} 1 0^{*}$
- $\{ w: w \text{ contains at least one } 1 \} = \Sigma^{*} 1 \Sigma^{*}$
- $\{ w: w \text{ doesn't contain 00 } 1 \} = 1^{*} (01^{+})^* (0 \cup \epsilon)$
- $\{ w: w \text{ w contains } 001 \} = \Sigma^{*} 001 \Sigma^{*}$
- $\{ w: |w| \text{ is even} \} = (\Sigma \Sigma)^{*}$
- $\{ w: |w| \text{ is a multiple of } 5\} = (\Sigma^5)^{*}$
- $\{ w: w \text{ starts and ends with same symbol} \} = \Sigma \cup 0\Sigma^{*}0 \cup 1\Sigma^{*}1$
- $\{ 01, 10 \} = 01 \cup 10$

|Regular expression|Shortest accepted|Shortest rejected|
|-|-|-|
|$0^{*}0011^{*}$|$001$|$\epsilon$|
|$0^{*}00(0 \cup 1)^{*}$|$00$|$\epsilon$|
|$(00)^{*}(110)^{*}(11)^{*}$|$\epsilon$|$0$ or $1$|
|$0^{*}(01 \cup 10)^{*} 1^{*}$|$\epsilon$|$110$|

## Equivalence with Automata

The language of the regular expression is regular. The simplist regular expressions are $\empty$, $\{ \epsilon \}$, and $\{ \sigma \}$ for $\sigma$ in $\Sigma$, which correspond to the trivial automata. Because regular languages are closed under kleene star, union, and concatenation, $(R^*)$, $(R_1 \cup R_2)$, or $(R_1 \cdot R_2)$ are regular.

### Convert NFA to Regular Expression

- Create a new start state that connects to the start state with $\epsilon$ and a new unique accept state that connects to all accept states with $\epsilon$.
- Choose a state other than the start and accept state. The state splits the other states into donors and receivers. For each pair of donor and receiver, a shortcut could be added with the kleene star, and then the chosen state could be removed. If there's no donors or receivers, the chosen state could be removed.
- Union the multiple shortcuts on the same pair of states. If there are states other than the start or accept, execute the last step again.
- The edge between the start state and the end state is the regular expression.
