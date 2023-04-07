# Constraint Satisfaction Problems

## Definition

The constraint satisfaction problem consists of three components, $X$, $D$, and $C$. The domain of CSP could be discrete (infinite or finite) or continuous.

- $X$ is a set of variables, ${ X_1, \dots, X_n }$.
- $D$ is a set of variables, ${ D_1, \dots, D_n }$, for each variable. Each domain $D_i$ consists of a set of allowable values, ${ v_1, \dots, v_k }$ for variable $X_i$.
- $C$ is a set of constraints that specifies allowable combinations of variables. Each constraint $C_i$ consists of a pair $<\text{scope}, \text{rel}>$, where $\text{scope}$ is a tuple of variables that participate in the constraint and $\text{rel}$ is a relation that defines the values that those variables can take on. The relation could be defined as an explicit list of tuples of values or an abstract relation.

Each state in a CSP is defined by an assignment of values to some or all of the variables, ${X_i = v_i, \dots}$. The assignment that does not violate constraints is the consistent assignment. The complete assignment is one in which each variable is assigned, and the solution to a CSP is a consistent and complete assignment.

### Variation of Constraint

- Unary constraints invole a single variable.
- Binary constraints invole a pair of variables.
- Higher-order constraints invole 3 or more variables.
- Preferences (soft constraints) specifies a cost of each variable assignment.

## Constraint Propagation

The algorithm to solve CSP could use constraint propagation to reduce the number of legal values of the variables.

- Node consistency: $X_i$ is node-consistent if all values in $D_i$ satisfies the unary constraint on the node $X_i$.
- Arc consistency: $X_i$ is arc-consistent with respect to $X_j$ if for each value in $D_i$, there is value in $D_j$ that satisfies the binary constraint on the arc $(X_i, X_j)$.

### AC-3 Algorithm

The AC-3 algorithm removes values from the domains that violate the arc constraints. The AC-3 algorithm maintains a queue of arcs and initializes the queue with all the arcs in the CSP. The algorithm pops off an arbitrary arc $(X_i, X_j)$ from the queue and makes $X_i$ arc-consistent with respect to $X_j$. If $D_i$ shrinks, the algorithm adds all arcs $(X_k, X_i)$ where $X_k$ is a neighbor of $X_i$ to the queue. If $D_i$ shrinks to an empty set, the CSP doesn't have consistent solution. The time complexity is $O(n^2 d^3)$, in which $n$ is the number of nodes and $d$ is the maximum size of the domains.

## Backtracking Search

The backtracking search algorithm is a depth-first search that chooses values for one variable at a time and backtracks when a variable has no legal values left to assign.

### Variable and Value Ordering

- **Minimum-remaining-values heuristic** picks a variable with the fewest legal values.
- **Degree heuristic** picks a variable that is involved in the largest number of constraints on other unassigned variables.
- **Least-constraining-value heuristic** picks a value that rules out the fewest choices for the neighboring variables in the constraint graph.

### Interleaving search and inference

- The forward checking algorithm runs the constraint inference in the course of a search. When a variable $X$ is assigned, the forward checking algorithm establishes arc consistency for it. For each unassigned variable $Y$ that is connected to $X$ by a constraint, delete from $Y$'s domain any value that is inconsistent.
- The MAC (maintaining arc consistency) algorithm is more effective than forward checking. When a variable $X$ is assigned, the MAC algorithm adds $(X_j, X_i)$ where $X_j$ is an unassigned neighbor of $X_i$ to the queue, and then runs the AC-3 algorithm to perform constraint propagation.

## Problem Structure

The tree-structured CSP could be solved in $O(and^2)$ time. The algorithm picks a node as the root, produces the topological sort of the constraint graph, and makes the graph directed arc-consistent. Because the graph doesn't contain loop, the solution could be produced by selecting a value for each variable.

To reduce a general constraint graph to a tree, the **cutset conditioning** algorithm instantiates a set of variables such that the remaining constraint graph is a tree. If the cutset size is $c$, the time complexity is $O(d^c \cdot (n - c)d^2)$.
