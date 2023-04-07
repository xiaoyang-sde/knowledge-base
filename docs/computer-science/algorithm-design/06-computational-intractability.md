# NP and Computational Intractability

NP-complete problems is a set of problems that a polynomial-time algorithm for any one of them would imply the existence of a polynomial-time algorithm for all of them.

## Polynomial-Time Reductions

To express the notation that a particular problem X is at least at hard as some other problem Y, we assume that there's a "black box" capable of solving X in a single step.

If an instance of problem Y could be solved using a polynomial number of standard computational steps and a polynomial number of calls to the black box that solves X, Y is polynomial-time reducible to X. ($Y \leq_{p} X$)

- If $Y \leq_{p} X$ and X could be solved in polynomial time, then Y could be solved in polynomial time.
- If $Y \leq_{p} X$ and Y couldn't be solved in polynomial time, then X couldn't be solved in polynomial time.

## Independent Set and Vertex Cover

### Independent Set

In a graph $G = (V, E)$, a set of nodes $S \subseteq V$ is independent if no two nodes in $S$ are joined by an edge. The problem is to determine if G contains an independent set of size at least $k$, given a number $k$.

### Vertex Cover

In a graph $G = (V, E)$, a set of nodes $S \subseteq V$ is a vertex cover if every edge $e \in E$ has at least one end in $S$. The problem is to determine if G contains a vertex cover of size at most $k$, given a number $k$.

### Reduction

In a graph $G = (V, E)$, $S$ is an independent set if and only if its complement $V - S$ is a vertex cover.

- Independent set $\leq_{p}$ vertex cover. If there's a black box to solve vertex cover, the independent set problem of size at least $k$ is equivalent to the vertex problem of size at most $n - k$.
- Vertex cover $\leq_{p}$ independent set. If there's a black box to solve the independent set, the vertex cover problem of size at most $k$ is equivalent to the independent set of size at least $n - k$.

## Set Cover and Set Packing

### Set Cover

Given a set $U$ of elements, a collection $S_1, \dots, S_m$ of subsets of $U$, and a number $k$, the problem is to determine if there exists a collection of at most $k$ of these sets whose union is equal to all of $U$.

### Set Packing

Given a set $U$ of elements, a collection $S_1, \dots, S_m$ of subsets of $U$, and a number $k$, the problem is to determine if there exists a collection of at least $k$ of these sets with the property that no two of them intersect.

### Reduction

- Vertex cover $\leq_{p}$ Set cover: Let the edges be the elements of $U$, and the sets $S_n$ be the edges that incident to the vertex $n$. The vertex cover problem is then converted to the set cover problem.
- Independent set $\leq_{p}$ Set Packing: Let the edges be the elements of $U$, and the set $S_n$ be the edges that incident to the vertex $n$. The independent set problem is then converted to the set packing problem.

## Reductions via "Gadgets": The Satisfiability Problem

### The SAT and 3-SAT Problems

Let $X$ be a set of $n$ boolean variables $x_1, \dots, x_n$, each can take the value 0 or 1. A clause is simply a disjunction of distinct terms $t_1 \vee t_2 \vee \dots \vee t_{l}$. ($t_i \in {x_1, x_2, \dots, x_n, \overline{x_1}, \dots, \overline{x_n}}$)

A truth assignment $v$ assigns 0 or 1 to each $x_i$. An assignment satisfies a clause $C$ if $C = 1$. An assignmeent satisfies a collection of clauses $C_1, \dots, C_k$ if $C_1 \wedge C_2 \wedge \dots \wedge C_k = 1$.

The SAT problem is to determine if a satisfying truth assignment exists for a set of clauses over a set of variables.

The 3-SAT problem if a satisfying truth assignment exists for a set of clauses, each of length 3, over a set of variables.

### Reducing 3-SAT to Independent Set

The 3-SAT problem could be interpreted as choosing one term from each clause without any conflict, then find a truth assignment that causes all of them to evaluate to 1, which satisfies all clauses. Two terms conflict if one is equal to a variable $x_i$ and the other is equal to its negation $\overline{x_i}$.

Let $G$ be a graph with $3k$ nodes grouped into $k$ triangles where each represents a clause. If two terms belong to the same clause (triangle) or conflict, there's an edge that connects them. An independent set with size $k$ in $G$ is the set of terms without conflict, which is the solution to the 3-SAT problem. Therefore, 3-SAT $\leq_{p}$ Independent set.

### Transitivity of Reductions

If $Z \leq_{p} Y, Y \leq_{p} X$, then $Z \leq_{p} X$.

Therefore, 3-SAT $\leq_{p}$ Independent set $\leq_{p}$ Vertex cover $\leq_{p}$ Set cover.

## Efficient Certification and the Definition of NP

### Problems and Algorithms

Let $s$ be the string of input to a problem, which has the length $|s|$. An algorithm $A$ for a decision problem receives an input $s$ and returns the value "yes" or "no", and the returned value is denoted by $A(s)$.

Let $p(\cdot)$ be a polynomial function for the input $s$, $A$ has a polynomial running time if $A$ terminates in at most $O(p(|s|))$ steps. $P$ is the set of all problems that an algorithm $A$ with a polynomial running time that solves $X$ exists.

### Efficient Certification

To check a solution, let $t$ be a certificate string that contains the evidence that $s$ is a "yes" instance of a problem $X$.

$B$ is an efficient certifier for a problem $X$ if:

- $B$ is a polynomial-time algorithm that takes two input arguments $s$ and $t$.
- There's a polynomial function $p$ that for every string $s$, $s \in X$ if there's a string $t$ that $|t| \leq p(|s|)$, and $B(s, t) = "yes"$.

#### Examples

- 3-SAT: the certificate $t$ is an assignment of truth values to the variables; the certifier $B$ evaluates the given set of clauses with respect to this assignment.
- Independent set: the certificate $t$ is the identity of a set of at least $k$ vertices; the certifier $B$ checks that, for these vertices, no edge joins any pair of them.
- Set cover: the certificate $t$ is a list of $k$ sets from the given collection; the certifier $B$ checks that the union of these sets is equal to the underlying set $U$.

### P = NP?

- $P$ is the set of all problems that an algorithm $A$ with a polynomial running time that solves $X$ exists.
- $NP$ is the set of all problems for which there exists an efficient certifier, which indicates that the solution to the problems could be efficiently verified.

$P \subseteq NP$. Let $B = A$, ignore $t$, and then use $A$ to directly solve the problem in polynomial time.

The question of whether $P = NP$, or whether every problem whose solution can be quickly verified can also be solved quickly, is one of the most famous unsolved problems in computer science.

## NP-Complete Problems

If $X$ is an NP-complete problem, then $X \in NP$ and for all $Y \in NP, Y \leq_{p} X$. If $X$ is solvable in polynomial time, then $P = NP$.

### Circuit Satisfiability

The definition of a circuit is a labeled, directed acyclic graph.

- The inputs (nodes without incoming edges) are labeled either 0 or 1.
- Every other node is labeled with one of the boolean operators AND, OR, or NOT. Nodes labeled with AND or OR will have two incoming edges, and nodes labeled with NOT will have one incoming edge.
- There's an output node without outgoing edges, which is the output computed by the circuit.

The circuit satisfiability problem is to determine whether there's an assignment of values to the inputs that cause the output to be 1, given a circuit as input.

Any algorithm that takes a fixed number of bits as input and produces a yes/no answer could be represented by a circuit. (1 -> yes, 0 -> no) If the algorithm takes several polynomial steps, then the circuit has a polynomial size. Algorithms implemented on physical computers could be reduced to the boolean logic gates.

To proof that $X \leq_{p}$ circuit satisfiability, we know that $X$ has an efficient certifier $B()$. To determine whether an input (with length $n$) $s \in X$, we need to determine whether a $t$ of length $p(n)$ exists so that $B(s, t) = \text{yes}$.

The question could be answered with the black box for circuit satisfiability. Suppose there's a circuit with $n + p(n)$ inputs, and the first $n$ inputs are hard-coded with the value of $s$. The remaining inputs will be labeled with variables representing $t$. If there's a way to set the input so that the circuit produces an output of 1, then the $t$ ($B(s, t) = yes$) exists, and $s \in X$. Therefore, $X \leq_{p}$ circuit satisfiability.

### General Strategy for Proving NP-Complete

If $Y$ is an NP-complete problem, and $X$ is a problem in NP with the property that $Y \leq_{p} X$, then $X$ is NP-complete.

For a new problem $X$:

1. Prove that $X \in NP$.
2. Choose a problem $Y$ that is known to be NP-complete.
3. Consider an arbitrary instance $s_Y$ of problem $Y$, and show how to construct an instance $s_X$ of problem $X$ that satisfies the following properties:

- If $s_Y$ is a yes instance of $Y$, then $s_X$ is a yes instance of $X$.
- If $s_X$ is a yes instance of $X$, then $s_Y$ is a yes instance of $Y$.

## Graph Coloring

Let $G$ be an undirected graph in which nodes are the regions to be colored and edges are the pairs that are neighbors. The problem is to assign a color to each node of $G$. If $(u, v)$ is an edge, $u$ and $v$ are assigned different colors. The goal is to minimize the number of colors. If $G$ has a k-coloring, then it's a k-colorable graph. The algorithmic version of the problem is that given a graph $G$ and a bound $k$, does $G$ have a k-coloring?

### The Computational Complexity

The graph $G$ is 2-colorable if and only if it is bipartite. It could be solved efficiently with breadth-first search.

3-coloring is NP-complete. The problem is NP because the solution could be verified efficiently by checking the number of colors and if there's a pair of nodes that receive the same color.

To prove the NP-completeness, we could determine if 3-SAT could be solved using a black box for 3-coloring.

Let $G$ be a graph with three special nodes $True$, $False$, and $Base$, which are joined into a triangle.

To begin, join each pair of nodes $v_i, \overline{v_i}$ with an edge, and join both these nodes to $Base$.

- In any 3-coloring of $G$, the nodes $v_i$ and $\overline{v_i}$ must get different colors, and must be different from $Base$.
- In any 3-coloring of $G$, the nodes $True$ $False$, and $Base$ must get all tree colors in some permutation. Therefore, one of $v_i$ and $\overline{v_i}$ gets the $True$ color, and the other gets the $False$ color.

For each clause in the 3-SAT instance, attach a six-node subgraph to the nodes in the clause so that at least one of them must have $True$ color. Therefore, the 3-SAT instance is satisfiable if and only if $G$ has 3-coloring.

k-coloring for $k > 3$ is NP-complete. Take an instance of 3-coloring, add $k - 3$ ne wnodes, and join them to each other and to every node in G. The resulting graph is k-colorable if and only if the original graph $G$ is 3-colorable.

The circuit satisfiability problem could be reduced to an equivalent instance of 3-SAT problem, thus 3-SAT, Independent set, Set packing, Vertex cover, and Set cover are NP-complete.
