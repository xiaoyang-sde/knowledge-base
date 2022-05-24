# Turing Machine

## Definition

The Turing machine is a mathematical model of computation describing an abstract machine that reads and writes symbols on a infinite tape based on a table of rules. The Turing machine has a head that scans the input forward and backward.

The Turing machine is defined as a tuple $(Q, \Sigma, \Gamma, \delta, q_0, q_{\text{accept}}, q_{\text{reject}})$, in which $Q$ is the set of states, $\Sigma$ is the input alphabet ($\epsilon \notin \Sigma$), $\Sigma \in \Gamma$ is the tape alphabet, $q_\text{accept}$ is the accept state, and $q_\text{reject}$ is the reject state. The transition function $\delta$ is defined as $\delta: Q \times \Gamma \rightarrow Q \times \Gamma \times \{ L, R \}$, in which $L, R$ indicates the movement direction of the head.

The language recognized by the Turing machine $M$ is $L(M) = { w: w \text{ halts in state } q_{\text{accept}} \text{ when started on } w }$. The language is Turing-recognizable.

## Example

Let $L = { w\#w: w \in \{ 0, 1 \}^* }$. The Turing machine for $L$ iterates through these steps until it marks all input symbols as $x$:

- Read the symbol $\sigma_i$ and mark the symbol as $x$.
- Move to the right until it passes $\#$ and reaches a non-$x$ symbol $\sigma_j$.
- If $\sigma_i = \sigma_j$, mark the symbol as $x$.
- Move to the left until it passes $\#$ and reaches the farthest non-$x$ symbol.

Let $L = {a^n b^{kn}: n, k \geq 1}$. The Turing machine for $L$ iterates through these steps until it reaches the accept or reject state:

- Change the start cell from $a$ to $a \cdot$
- Scan the input to ensure that it's in the form $a^+ b^+$.
- Return head to the left end
- Shuttle bewteen $a$'s and $b$'s, crossing off one of each until there's no $b$'s.
- If there's remaining $a$'s and no no $b$'s, reject.
- Restore crossed-off $a$'s. If there's no $b$'s, accept. If there's remaining $b$'s, go to step 3.

Let $L = {w_1 \# w_2 \# \dots \# w_k }$ where $w_1, \dots, w_k$ are distinct. The Turing machine for $L$ iterates through these steps until it reaches the accept or reject state:

- If the current symbol is not blank or $\#$, reject. If the current symbol is blank, accept. If the current symbol is $\#$, change it to $\# \cdot$.
- Scan right for the next $\#$ and change it to $\# \cdot$. If there's not such symbol, accept.
- Compare the dotted strings by shuttling. If they are equal, reject.
- Move the rightmost dot to the next $\#$ and go to the step 3.
- Put the head after the last dotted string and go to the step 1.

## Turing-Equivalent Model

### Church-Turing Thesis

Each real-world computation device could be simulated with a Turing machine. Therefore, if $L$ is recognized by some computational device, then $L$ is Turing-recognizable.

### Bidirectional Tape

If $L$ is recognized by a Turing machine with bidirectional tapes, then $L$ is Turing-recognizable. The part of the tape before the start state is folded to create a new tape where each state contains two original states.

### Multiple Tape

If $L$ is is recognized by a Turing machine with mutliple tapes, then $L$ is Turing-recognizable. Let there be $k$ tapes with each tape contains $\Delta: Q \times \Gamma \rightarrow Q \times \Gamma \times \{ L, R, S \}$. The equivalent Turing machine could be constructed with:

- Merge the tapes into a single tape with each state $q_i$ contains all $i$-th state in each tape
- Circled symbols indicate the tape head locations
- To simulate one step of k-tape Turing machine:
  - Scan the tape while memorizing the $k$ circiled symbols
  - Consult the transition function $\delta$ of each tape
  - Scan the tape again and update the circled symbols to shift the circles themselves

## Non-deterministic Turing Machine

The transition function of a non-deterministic Turing machine has the transition function $\Delta: Q \times \Gamma \rightarrow \mathcal{P}(Q \times \Gamma \times \{ L, R \})$, which allows several legal next moves. The input is accepted if there's a sequence of moves that ends in the accept state.

If $L$ is recognizable by a non-deterministic Turing machine, then $L$ is Turing-recognizable. The equivalence Turing machine could be constructed with:

- The Turing machine has three tapes: input, sequence, and work.
- The input tape contains the original input.
- The sequence tape is intialized to $\epsilon$, and is incremented in graded lexicographical order.
- Implement the sequence on the work tape. Initialize work tape to hold the input, and then apply the sequence tape to the work tape one step at a time.

## Automata with Multiple Stack

The automata has read-once access to input and could push or pop each of $k$ stacks. If $k = 0$, the automata is a deterministic or non-deterministic finite automata. If $k = 1$, the automata is a pushdown automata. If $k \geq 2$ and $L$ is Turing-recognizable, then $L$ is recognizable by a $k$-stack automata.

- For a $k$-stack automata, the equivalent Turing machine contains $k + 1$ tapes, in which the first tape contains the original input, and the other $k$ tapes contains the contents of the $k$ stacks.
- For a Turing machine, the equivalent $k$-stack automata contains two stacks, in which the first stack represents the content before the tape head, and the second stack represents the content on and after the tape head.
  - Left move: Pop $\sigma$ from the first stack, and push $\sigma$ to the second stack.
  - Right move: Pop $\sigma$ from the second stack, and push $\sigma$ to the first stack. If the second is empty, push the blank character to the first stack.
  - Initialization: Push each $\sigma$ of the input to the first stack. Pop each $\sigma$ of the first stack, and push $\sigma$ to the second stack.
