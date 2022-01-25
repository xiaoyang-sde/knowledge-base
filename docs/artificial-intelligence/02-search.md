# Solving Problems by Searching

The problem-solving agent is a kind of goal-based agent that use atomic representations for the states. The process of looking for a sequence of actions that reaches the goal is called search. The search algorithm takes a problem as input and returns a solution in the form of an action sequence that leads from the initial state to a goal state.

- The **initial state** that the agent starts in
- The possible **actions** available to the agent for a particular state $s$
- The **transition model** that represents the state that results from doing action $a$ in state $s$
- The **goal test** that determines whether a given state $s$ is a goal state
- The **path cost** function that assigns a numeric cost to each path, which reflects the performance measure of the agent

## Search algorithm

The search algorithms use a tree data structure to represented the search tree that is being constructed. For each node $n$ of the tree, the data structure contains these components:

- $\text{n.STATE}$: the state represented by the node
- $\text{n.PARENT}$: the node in the search tree that generated the current node
- $\text{n.ACTION}$: the action that was applied to the parent to generate the node
- $\text{n.PATH-COST}$: the cost of the path from the initial state to the node

The time complexity of the search algorithm is examined based on a uniform tree and expressed in terms of three quantities:

- $b$: the branching factor or maximum number of children of a node
- $d$: the depth of the shallowest goal node
- $m$: the maximum length of the path in the state space

### Breadth-first search

The breadth-first search is an algorithm that all the nodes are expanded at a given depth in the search tree before other nodes at the next level are expanded. The algorithm uses a FIFO queue for the frontier to extract the shallowest unexpanded node. The algorithm is optimal if the path cost is a non-decreasing function of depth and is complete. The goal test is applied to a node when it is generated.

- Time complexity: $O(b^d)$
- Space complexity: $O(b^d)$

### Depth-first search

The depth-first search is an algorithm that expands the deepest node in the current frontier of the search tree. The algorithm uses a LIFO queue to extract the deepest unexpanded node. The algorithm is complete if it records visited nodes to avoid redundant paths and is not optimal.

- Time complexity: $O(b^d)$
- Space complexity: $O(bm)$

### Depth-limited search

The depth-limit search is an algorithm that expands the deepest node in the current frontier of the search tree until it reaches the pre-determined depth limit $l$. The algorithm is not complete if $l < d$ and not optimal if $l > d$.

- Time complexity: $O(b^l)$
- Space complexity: $O(bl)$

### Iterative deepening depth-first search

The iterative deepening depth-first search increases the depth limit from $0$ to $d$, the depth of the shallowest goal node. In general, iterative deepening is the preferred uninformed search method when the search space is large and the depth of the solution is not known. The algorithm is optimal if the path cost is a non-decreasing function of depth and is complete.

- Time complexity: $O(b^d)$
- Space complexity: $O(bd)$

### Uniform-cost search

The uniform-cost search is an algorithm that expands the node $n$ with the lowest path cost $g(n)$. The algorithm uses a priority queue ordered by $g$ for the frontier. The goal test is applied to a node when it is selected for expansion to avoid suboptimal path. The algorithm is optimal because whenever it selects a node for expansion, the optimal path to the node has been found. The algorithm is complete if the cost of each step exceeds a positive constant $\epsilon$.

- Time complexity: $O(b^{1 + \lfloor C^{*} / \epsilon \rfloor})$ ($C^{*}$ is the cost of the optimal solution)
- Space complexity: $O(b^{1 + \lfloor C^{*} / \epsilon \rfloor})$

### Bidirectional search

The bidirectional search runs two simultaneous searches. The forward search starts from the initial state and the backward search starts from the goal. The goal test is replaced with a check to see whether the frontiers of the two searches interse. The algorithm is optimal if the path cost is a non-decreasing function of depth and is complete.

- Time complexity: $O(b^{d / 2})$
- Space complexity: $O(b^{d / 2})$

## Informed search strategies

Informed search strategies use problem-specific knowledge beyond the definition of the problem itself to find solutions more efficiently. The general approach is **best-first search**, which selects the node with the lowest cost estimate function $f(n)$. The algorithm is implemented with a priority queue based on $f(n)$. The algorithm uses a heuristic function $h(n)$ as a component of $f(n)$, which is tje estimated cost of the cheapest path from node $n$ to a goal state.

### Greedy best-first search

The greedy best-first search algorithm tries to expand the node that is closest to the goal, which evaluates nodes based on $f(n) = h(n)$. The algorithm is not optimal and is complete if it records the visited nodes.

- Time complexity: $O(b^m)$
- Space complexity: $O(b^m)$

### A* search

The A* search algorithm evaluates nodes based on $f(n) = g(n) + h(n)$, which is the estimated cost of the cheapest solution through $n$. The algorithm is complete and optimal if $h(n)$ satisfies certain conditions.

- Time complexity: $O(b^{\Delta})$, $\Delta = h^{*} - h$, where $h^{*}$ is the actual cost of getting from the root to the goal
- Space complexity: $O(b^d)$

#### Optimality

- **Admissible**: $h(n)$ never overestimates the cost to reach the goal, thus $f(n)$ never overestimates the true cost of a solution along the current path through $n$. The tree-search version of A* is optimal if $h(n)$ is admissible.
- **Consistent**: For node $n$ and successor $n'$, $h(n) \leq c(n, a, n') + h(n')$. (The estimated cost of reaching the goal from $n$ is no greater than the step cost of getting to $n'$ plus the estimated cost of reaching the goal from $n'$.) The graph-search version is optimal if $h(n)$ is consistent.

If $h(n)$ is consistent, then the values of $f(n)$ along any path are nondecreasing. Because the algorithm selects the node with the lowest $f(n)$ in the frontier, the first goal node selected for expansion must be an optimal solution.

If $C^*$ is the cost of the optimal solution path, the algorithm expands all nodes with $f(n) < C^*$ and some nodes with $f(n) = C^*$. The algorithm is complete if there are finite nodes with $f(n) \leq C^*$. The algorithm is **optimally efficient** for any given consistent heuristic, because it expands the fewest nodes to find the optimal solution (except for tie-breaking when $f(n) = C^*$).
