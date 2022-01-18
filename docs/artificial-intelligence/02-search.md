# Solving Problems by Searching

The problem-solving agent is a kind of goal-based agent that use atomic representations for the states. The process of looking for a sequence of actions that reaches the goal is called search. The search algorithm takes a problem as input and returns a solution in the form of an action sequence that leads from the initial state to a goal state.

- The **initial state** that the agent starts in
- The possible **actions** available to the agent for a particular state $s$
- The **transition model** that represents the state that results from doing action $a$ in state $s$
- The **goal test** that determines whether a given state $s$ is a goal state
- The **path cost** function that assigns a numeric cost to each path, which reflects the performance measure of the agent

## Search Algorithm

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

The breadth-first search is an algorithm that all the nodes are expanded at a given depth in the search tree before other nodes at the next level are expanded. The algorithm uses a FIFO queue for the frontier to extract the shallowest unexpanded node. The algorithm is optimal if all step costs are the same and is complete. The goal test is applied to a node when it is generated.

- Time complexity: $O(b^d)$
- Space complexity: $O(b^d)$

### Depth-first search

The depth-first search is an algorithm that expands the deepest node in the current frontier of the search tree. The algorithm uses a LIFO queue to extract the deepest unexpanded node. The algorithm is not complete if it doesn't record visited nodes and is not optimal.

- Time complexity: $O(b^d)$
- Space complexity: $O(bm)$

### Depth-limited search

The depth-limit search is an algorithm that expands the deepest node in the current frontier of the search tree until it reaches the pre-determined depth limit $l$. The algorithm is not complete if $l < d$ and not optimal if $l > d$.

- Time complexity: $O(b^l)$
- Space complexity: $O(bl)$

### Uniform-cost search

The uniform-cost search is an algorithm that expands the node $n$ with the lowest path cost $g(n)$. The algorithm uses a priority queue ordered by $g$ for the frontier. The goal test is applied to a node when it is selected for expansion to avoid suboptimal path.
