# Logical Agents

## Knowledge-based Agent

The knowledge-based agent maintains a knowledge base, which is a collection of logical sentences that encodes what the agent has observed. The agent is able to perform logical inference to draw new conclusions.

## Logic

The logical sentence defines the semantic that represents the truth of each sentence with respect to each model. If a sentence $\alpha$ is true in model $m$, then $m$ is a model of $\alpha$. $M(\alpha)$ represents the set of models of $\alpha$.

- $\lnot$ (not): $\lnot P$ is true iff $P$ is false in $m$.
- $\land$ (and): $P \land Q$ is true iff both $P$ and $Q$ are true in $m$. The sentence whose main connective is $\land$ is a **conjunction**.
- $\lor$ (or): $P \lor Q$ is true iff either $P$ or $Q$ is true in $m$. The sentence using $\lor$ is a **disjunction**.
- $\implies$ (implies): $P \implies Q$ is true unless $P$ is true and $Q$ is false in $m$. The sentence that uses $\implies$ to connect the **premise** and the **conclusion** is an implication.
- $\iff$ (if and only if): $P \iff Q$ is true if $P$ and $Q$ are both true or both false in $m$. The sentence that uses $\iff$ is a **biconditional**.

## Propositional Logic

Propositional logic is written in sentences composed of **proposition symbols** joined by logical connectives. The **atomic sentences** consist of a single proposition symbol. Each symbol stands for a proposition that can be true or false.

- **Logical equivalence**: The sentences $\alpha$ and $\beta$ are logical equivalent if they are true in the same set of models. $\alpha \equiv \beta$ if and only if $\alpha \models \beta$ and $\beta \models \alpha$.
  - Commutativity of $\land$: $\alpha \land \beta \equiv \beta \land \alpha$
  - Commutativity of $\lor$: $\alpha \lor \beta \equiv \beta \lor \alpha$
  - Associativity of $\land$: $(\alpha \land \beta) \land \gamma \equiv \alpha \land (\beta \land \gamma)$
  - Associativity of $\lor$: $(\alpha \lor \beta) \lor \gamma \equiv \alpha \lor (\beta \lor \gamma)$
  - Contraposition: $\alpha \implies \beta \equiv \lnot \beta \implies \lnot \alpha$
  - Implication elimination: $\alpha \implies \beta \equiv \lnot \alpha \lor \beta$
  - Biconditional elimination: $\alpha \iff \beta \equiv (\alpha \implies \beta) \land (\beta \implies \alpha)$
  - De Morgan: $\lnot (\alpha \land \beta) \equiv \lnot \alpha \lor \lnot \beta$
  - De Morgan: $\lnot (\alpha \lor \beta) \equiv \lnot \alpha \land \lnot \beta$
  - Distributivity of $\land$ over $\lor$: $\alpha \land (\beta \lor \gamma) \equiv (\alpha \land \beta) \lor (\alpha \land \gamma)$
  - Distributivity of $\lor$ over $\land$: $\alpha \lor (\beta \land \gamma) \equiv (\alpha \lor \beta) \land (\alpha \lor \gamma)$
- **Validity**: The sentence is valid if it's true in all models. For sentences $\alpha$ and $\beta$, $\alpha \models \beta$ if and only if the sentence $a \implies b$ is valid.
- **Satisfiability**: The sentence is satisfiable if it's true in some model. The problem of etermining the satisfiability of sentences in propositional logic is NP-complete.

The **conjunctive normal form** (CNF) is a conjunction of clauses, each of which a disjunction of literals. Each logical sentence has a logically equivalent conjunctive normal form.

## Propositional Logical Inference

The logical reasoning involves the relation of logical entailment between sentences. $\alpha \models \beta$ means that the sentence $\alpha$ entails the sentence $\beta$. $\alpha \models \beta$ if and only if $M(\alpha) \subseteq M(\beta)$.

- **Direct proof**: $\alpha \models \beta$ if and only if $\alpha \implies \beta$ is valid.
- **Proof by contradiction**:  $\alpha \models \beta$ if and only if $\alpha \land \lnot \beta$ is unsatisfiable.

The **model checking algorithm** could determine whether $\text{KB} \models q$ by enumerating models and showing that the $q$ must be true in all models where $\text{KB}$ is true. The algorithm is sound (the answer will be correct) and complete (the answer will be found).

- Time complexity: $O(2^n)$ (Propositional entailment is co-NP-complete.)
- Space complexity: $O(n)$

The **DPLL algorithm** is a depth-first backtracking search over possible models with tricks to reduce excessive backtracking. The algorithm aims to solve the satisfiability problem, and the problem of $\alpha \models \beta$ could be reduced to the problem of showing that $\alpha \land \lnot \beta$ is not satisfiable.

- **Early termination**: The clause is true if any of the symbols are true. Therefore the sentence could be known to be true or false even before all symbols are assigned.
- **Pure symbol heuristic**: The pure symbol is a symbol that appears with the same sign in all clauses. The symbol could be immediately assigned to reduce the search space.
- **Unit clause heuristic**: The unit clause is a clause with just one literal or a disjunction with one literal
and many falses. The literal has only one valid assignment.

## Propositional Theorem Proving

Propositional theorem proving applies rules of inference to the sentences of the knowledge base to construct a proof of the desired sentence without consulting models.

- **Modus ponens**: If $\alpha \implies \beta$ and $\alpha$ are given, then $\beta$ could be inferred. $\frac{\alpha \implies \beta, \alpha}{\beta}$
- **And-elimination**: The conjuncts in a conjunction could be inferred. $\frac{\alpha \land \beta}{\alpha}$
- **Resolution**: If $\alpha$ and $\beta$ is given, then $\alpha \land \beta$ could be inferred. $\frac{\alpha, \beta}{\alpha \land \beta}$
  - **Unit resolution**: $\frac{l_1 \lor \dots \lor l_k, m}{l_1 \lor \dots \lor l_{i-1} \lor l_{i+1} \lor \dots \lor l_k}$ if $l_i$ and $m$ are complementary literals
  - **Full resolution**: $\frac{l_1 \lor \dots \lor l_k, m_1 \lor \dots \lor m_k}{l_1 \lor \dots \lor l_{i-1} \lor l_{i+1} \lor \dots \lor l_k \lor m_1 \lor \dots \lor m_{j-1} \lor m_{j+1} \lor \dots \lor m_n}$ if $l_i$ and $m_j$ are complementary literals

The resolution algorithm tries to show that $\text{KB} \land \lnot \alpha$ is unsatisfiable to prove $\text{KB} \models \alpha$. The algorithm converts $\text{KB} \land \lnot \alpha$ to CNF, and applies the resolution rule to the resulting clauses. Each pair that contains complementary literals is resolved to produce a new clause, which is added to the set if it is not already present.

- $\text{KB} \models \alpha$ if two clauses resolve to yield the empty clause. If a set of clauses is unsatisfiable, then the resolution closure of those clauses contains the empty clause. For example, $\lnot \alpha \lor \lnot \beta \lor \gamma$ could be written as $\alpha \land \beta \implies \gamma$.
- $\text{KB} \not \models \alpha$ if there are no new clauses that could be added.

The **forward-chaining algorithm** determines if a singe proposition symbol $q$ is entailed by a knowledge base of definite clauses. The algorithm begins from positive literals, and if all the premises of an implication are known, then its conclusion is added to the set of known facts. The algorithm terminates if $q$ is added or no further inferences could be made. The algorithm is sound and complete.

- **Definite clause**: The disjunction of literals of which exactly one is positive.
- **Horn clause**: The disjunction of literals of which at most one is positive.
