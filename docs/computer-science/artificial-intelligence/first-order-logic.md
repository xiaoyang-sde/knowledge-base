# First-Order Logic

First-order logic uses objects as its basic component. First-order logic describes relationships between objects and applies functions to them. Each object is represented by a **constant symbol**, each relationship by a **predicate symbol**, and each function by a **function symbol**.

The **model** is a mapping of all constant symbols to objects, predicate symbols to relations between objects, and function symbols to functions of objects. The sentence is true under a model if the relations described by the sentence are true under the mapping.

The **term** is a logical expression that refers to an object. For the term $f(t_1, \dots, t_n)$, $f$ refers to a function $F$ in the model, the arguments $t_1, \dots, t_n$ refers to objects in the domain, and the term refers to the object that is the value of the function $F$ applied to $d_1, \dots, d_n$.

The **atomic sentence** is formed from a predicate symbol optionally followed by a parenthesized list of terms. The atomic sentence is **true** in a given model if the relation referred to by the predicate symbol holds among the objects referred to by the arguments. The **complex sentence** is a group of atomic sentences connected by logical connectives.

- **Universal quantifier** $\forall$: $\forall x P$ is true in a model $m$ if and only if $P$ is true with $x$ being each possible object in the model.
- **Existential quantifier** $\exists$: $\exists x P$ is true in a model $m$ if and only if $P$ is true with $x$ being some possible object in the model.
  - $\forall x \lnot P \equiv \lnot \exists x P$
  - $\lnot \forall x P \equiv \exists x \lnot P$
  - $\forall x P \equiv \lnot \exists x \lnot P$
  - $\exists x P \equiv \lnot \forall x \lnot P$
- **Equality symbol** $=$: $x = y$ if $x$ and $y$ refer to the same object.

## First-Order Logical Inference

### Propositionalization

The first-order logical knowledge base could be reduced to propositional logical knowledge base with instantiation. If a sentence $\alpha$ is entailed by an first-order logical knowledge base, it's entailed by a finite subset of the propositional knowledge base. To avoid infinite loop, the propositional knowledge base could be constructed by instantiating with depth-$n$ terms and the algorithm checks if $\alpha$ is entailed. However, entailment in first-order logic is semidecidable, thus the algorithm can't handle non-entailed $\alpha$.

- **Universal instantiation** $\frac{\forall v \alpha}{\text{SUBST}(\{v / g\}, a)}$: If the knowledge base contains a universally quantified sentence, the instantiation of that sentence where the logic variable $v$ is replaced by a concrete ground term $g$ could be inferred. For the variable $v$ and the ground term $g$, the substitution is denoted as $\theta = \{v / g\}$ and the result is denoted as $\text{SUBST}(\theta, \alpha)$.
- **Existential instantiation** $\frac{\exists v \alpha}{\text{SUBST}(\{v / k\}, a)}$: If the knowledge base contains a existentially quantified sentence, a single instantiation of that sentence where the logic variable $v$ is replaced by a Skolem constant symbol $k$ which must not appear elsewhere in the knowledge base could be inferred.

### Unification

The **unification** process is a key component of first-order inference algorithms. The $\text{UNIFY}$ algorithm takes two sentences and returns a **unifier** if exists: $\text{UNIFY}(p, q) = \theta$ where $\text{SUBST}(\theta, p) = \text{SUBST}(\theta, q)$. If there are multiple unifiers, the algorithm returens the most general unifier.

The **generalized modus ponens** inference rule stats that for atomic sentences $p_{i}$, $p_{i}^{'}$, and $q$, and a substitution $\theta$ such that $\text{SUBST}(\theta, p_{i}^{'}) = \text{SUBST}(\theta, p_{i})$, $\frac{p_{1}^{'}, \dots, p_{n}^{'}, (p_1 \land \dots \land p_n \implies q)}{\text{SUBST}(\theta, q)}$ for all $i$.

The **first-order definite clause** is a disjunction of literals of which exactly one is positive. The clause is either atomic or an implication whose premise is a conjunction of positive literals and whose conclusion is a single positive literal.
