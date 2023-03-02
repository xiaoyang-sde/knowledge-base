# Turing Machine

## Definition

The **Turing machine** uses an infinite tape that contains the input string and a tape head that could read and write symbols and move around the tape. The machine continues computing until it reaches an accept or reject state.

The Turing machine is defined as a tuple $(Q, \Sigma, \Gamma, \delta, q_0, q_{\text{accept}}, q_{\text{reject}})$, in which $Q$ is the set of states, $\Sigma$ is the input alphabet ($\epsilon \notin \Sigma$), $\Gamma$ ($\Sigma \subseteq \Gamma$) is the tape alphabet, $q_\text{accept}$ is the accept state, and $q_\text{reject}$ is the reject state. The transition function $\delta$ is $Q \times \Gamma \to Q \times \Gamma \to \{ L, R \}$. For example, $\delta(q, a) \to (r, b, L)$ indicates that the machine writes $b$ to replace $a$, transists to state $r$, and moves the head to the right.

The Turing machine receives the input on the leftmost $|w|$ squares of the tape, and the head starts on the leftmost square of the tape. The first blank on the tape marks the end of the input. The head couldn't move to the left of the first square of the tape.

The collections of strings that the Turing machine $M$ accepts is the language recognized by $M$. The language is Turing-recognizable if some Turing machine recognizes it.

When the Turing machine starts on an input, the possible outcomes are accept, reject, and loop. The Turing machine fails to accept an input if the outcome is reject or loop. The Turing machine decides the language if it halts on all inputs. The language is Turing-decidable if some Turing machine decides it.

## Example

Let $L = \{ w\#w | w \in \{ 0, 1 \}^* \}$. The Turing machine for $L$ makes multiple passes over the input string, and on each pass it matches one of the characters on each side of the $\#$ symbol. If it crosses off all the symbols, the input is in $L$. If it discovers a mismatch, it enters a reject state.

- Move across the tape to corresponding positions on either
side of $\#$ to check whether these positions contain the same symbol. The machine rejects if the symbols do not match or no $\#$ is found. Cross off the symbols.
- The machine rejects if there are remaining symbols to the right of the $\#$.

## Turing-Equivalent Model

### Multi-tape Turing Machine

The multi-tape Turing machine is a Turing machine with several tapes. Each tape has its own head for reading and writing. The input appears on tape $1$ and the others are blank. The transition function $\delta: Q \times \Gamma^k \to Q \times \Gamma^k \times \{ L, R, S \}$ allows for reading, writing, and moving the heads on some or all of the tapes at the same time. Each multi-tape Turing machine $M$ has an equivalent single-tape Turing machine $S$.

$S$ could simulate $M$. If $M$ has $k$ tapes, $S$ stores their information on its single tape to simulate the effect of $k$ tapes. $S$ uses a new symbol $#$ as a delimiter to separate the contents of different tapes. $S$ writes a tape symbol with a dot to track the location of the heads.

### Non-deterministic Turing Machine

The transition function of a non-deterministic Turing machine is $\delta: Q \times \Gamma \to \mathcal{P}(Q \times \Gamma \times \{ L, R \})$, which allows several legal next moves. If some branch of the computation leads to the accept state, the machine accepts its input. Each non-deterministic Turing machine $N$ has an equivalent deterministic Turing machine $D$.

$D$ could simulate $N$. $D$ searches on all possible branches of $N$'s computation, and if some branch accepts, $D$ accepts the input. Otherwise, $D$ enters a loop. $D$ could use a breadth-first search to avoid infinite computation branch.

$D$ contains the input tape, the simulation tape, and the address tape. The input tape contains the input string and is never altered. The simulation tape maintains a clone of $N$'s tape on some computation branch. The address take tracks $D$'s location in the computation tree, which could be represented with a list of the node index of each level. For example, $231$ represents the first child of the third child of the second child of the root.

1. Initialize the address tape to be $\epsilon$.
2. Simulate $N$ with input $w$ on a computation branch. Before each step of $N$, consult the next symbol on the address tape to determine the choice to make among those allowed by the transition function. If an reject state is reached, move to step 3. If an accept state is reached, accept the input.
3. Replace the address tape with the next string in the string ordering. Simulate the next branch with step 2.

### Multi-stack Automata

The automata has read-once access to input and could push or pop each of $k$ stacks. If $k = 0$, the automata is a deterministic or non-deterministic finite automata. If $k = 1$, the automata is a pushdown automata. If $k \geq 2$ and $L$ is Turing-recognizable, then $L$ is recognizable by a $k$-stack automata.

- For a $k$-stack automata, the equivalent Turing machine contains $k + 1$ tapes, in which the first tape contains the original input, and the other $k$ tapes contains the contents of the $k$ stacks.
- For a Turing machine, the equivalent $k$-stack automata contains two stacks, in which the first stack represents the content before the tape head, and the second stack represents the content on and after the tape head.
  - Left move: Pop $\sigma$ from the first stack, and push $\sigma$ to the second stack.
  - Right move: Pop $\sigma$ from the second stack, and push $\sigma$ to the first stack. If the second is empty, push the blank character to the first stack.
  - Initialization: Push each $\sigma$ of the input to the first stack. Pop each $\sigma$ of the first stack, and push $\sigma$ to the second stack.
