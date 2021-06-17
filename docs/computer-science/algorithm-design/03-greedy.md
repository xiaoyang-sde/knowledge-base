# Greedy Algorithms

An algorithm is greedy if it builds up a solution in small steps, choosing a decision at each step myopically to optimize some underlying criterion.

## Interval Scheduling

The Interval Scheduling Problem consists of finding the compatible set of maximum size from a set of requests, in which each request has a starting time $$ f_i $$ and finishing time $$ f_i $$.

### Interval Scheduling Algorithm

To design a greedy algorithm, the basic idea is to use a simple rule to select the first request $$ i_1 $$. Once a request $$ i_1 $$ is accepted, all requests that are not compatible with $$ i_1$$ are rejected. The algorithm terminates when all requests are either accepted or rejected.

- (Incorrect) Accept the request that starts earliest.
- (Incorrect) Accept the request that requires the smallest interval of time.
- (Incorrect) Accept the request that has the fewest number of non-compatible requests.
- (Optimal) Accept the request that finishes first.

### Analyzing the Algorithm

The set A returned by the algorithm is a compatible set of requests. Let O be an optimal set of intervals. Let $$ i_1, \dots, i_k $$ be the set of requests in A, and $$ j_1, \dots, j_m $$ be the set of requests in O. If $$ |A| = |O| $$, A is optimal.

**Induction**: For the first request, our greedy rule guarantees that $$ f(i_1) \leq f(j_1) $$. For $$ r > 1 $$, the induction hypothesis assume that $$ f(i_{r-1}) \leq f(j_{r-1}) $$. Since $$ f(j_{r-1}) \leq s(j_r) $$, $$ f(i_{r-1}) \leq s(j_r) $$. Therefore, the interval $$ j_r $$ is in the set of available intervals when the greedy algorithm selects $$ i_r $$, thus $$ f(i_r) \leq f(j_r) $$.

For each $$ r $$, the $$ r^{th} $$ interval finishes at least as soon as the $$ r^{th} $$ interval in O. Since the algorithm terminates when there's no possible requests, $$ |A| = |O| $$ and A is optimal.

### Implementation and Running Time

```py
def eraseOverlapIntervals(intervals):
  intervals.sort(key=lambda x:x[1])
  last = float('-inf')
  result = []
  for start, end in intervals:
    if start >= last:
      last = end
      result.append((start, end))
  return result
```

The sorting operation takes $$ O(nlogn) $$ time. The single-pass iteration takes $$ O(n) $$ time. Therefore, the time complexity of the interval scheduling algorithm is $$ O(nlogn) $$.

## Interval Partitioning

The Interval Partitioning Problem consists of scheduling all requests using as few resources as possible given a set of requests and many identical resources.

In any instance of interval partitioning, the number of resources needed is at least the depth of the set of intervals.

### Interval Partitioning Algorithm

Let d be the depth of the set of intervals. The algorithm assigns a label $$ {1, 2, \dots, d} $$ to each interval, and overlapping intervals have different labels.

The algorithm iterates through the intervals and tries to assign to each interval a label that hasn't been assigned to any previous interval that overlaps it.

### Analyzing the Algorithm

The greedy algorithm above will assign a label to each interval, and no two overlapping intervals will receive the same label. The number of resources in the result equals the depth of the set of intervals, which is the optimal number of resources needed.

## Shortest Paths

For a directed graph $$ G = (V, E) $$ with a designated start node $$ s $$, $$ s $$ has a path to every other node in $$ G $$. Each edge $$ e $$ has a length $$ l_e \leq 0 $$, indicating the time or distance to traverse $$ e $$. $$ l(P) $$ is the sum of the lengths of all edges in $$ P $$. The goal is to determine the shortest path from $$ s $$ to every other node in the graph.

### Dijkstra's Algorithm

The algorithm maintains a set $$ S $$ of vertices $$ u $$ for which we have determined a shortest-path distance $$ d(u) $$ from $$ s $$. Initially $$ S = {s}, d(s) = 0 $$. For each node $$ v \in V - S $$, we determine the shortest path that can be constructed by traveling along a path through $$ S $$ to $$ u \in S $$, followed by the single edge $$ (u, v) $$. The node $$ v $$ that will minimize the distance is added to $$ v $$, and $$ d(v) = d(u) + l_e $$.

To construct the shortest path from $$ s $$ to $$ v $$, we could start at $$ v $$ and recursively follow the edge we stored for $$ v $$ in the reverse direction to $$ u $$ until we reach $$ s $$.

### Analyzing the Algorithm

Consider the set $$ S $$ at any point in the algorithm's execution. For each $$ u \in S $$, the path $$ P_u $$ is the shortest $$ s-u $$ path.

**Induction**: The case $$ |S| = 1 $$ is trivial since $$ d(s) = 0 $$. Suppose the claim holds when $$ |S| = k $$ for $$ k \geq 1 $$. By induction hypothesis, $$ P_u $$ is the shortest $$ s-u $$ path for each $$ u \in S $$. In the iteration $$ k + 1 $$, the node $$ v $$ is added to $$ S $$ by the algorithm. If $$ P $$ is another path from $$ s $$ to $$ v $$ through a node $$ x $$ other than $$ v $$, it can't be shorter than $$ P_v $$. Otherwise, $$ x $$ will be added by the algorithm instead of $$ v $$.

- The algorithm doesn't always find the shortest paths if some of the edges can have negative lengths.
- The algorithm is a continuous version of the breadth-first search algorithm for traversing a graph.

### Implementation and Running Time

Use a priority queue to explicitly maintain the values of the minima $$ d'(v) = min(d(u) + l_e) $$ for each node $$ v \in V - S $$. In each iteration, `ExtractMin` returns the node that should be added to the set $$ S $$. For each node $$ w $$ in the queue, if $$ e' = (v, w) \in E $$, update the key of $$ w $$ to $$ min(d'(w), d(v) + l_e) $$ with `ChangeKey` operation. Therefore, the algorithm could be implemented on a graph with $$ n $$ nodes and $$ m $$ edges to run in $$ O(m log n) $$ time.

## Minimum Spanning Tree

For a connected graph $$ G = (V, E) $$, the Minimum Spanning Tree is to find a subset of the edges $$ T \subseteq E $$ so that the graph $$ (V, T) $$ is connected, and the total cost $$ \sum_{e \in T} c_e $$ is as small as possible.

Let $$ T $$ be a minimum-cost solution to the problem defined above. If the cost of the edge is positive, the graph $$ (V, T) $$ is a tree.

### Minimum Spanning Tree Algorithm

- **Prim's Algorithm**: The algorithm starts with a root node $$ s $$ and greedily grows a tree from $$ s $$ outward. At each step, the algorithm will add the node that can be attached as cheaply as possible to the partial tree.

- **Kruskal's Algorithm**: The algorithm starts without any edges and inserts edges in order of increasing cost. If inserting an edge $$ e $$ would result in a cycle, then $$ e $$ is discarded.

- **Reverse-Delete Algorithm**: The algorithm starts with the full graph $$ (V, E) $$ and deletes edges in the order of decreasing costs. If deleting an edge $$ e $$ would disconnect the graph, then $$ e $$ is discarded.

### Analyzing the Algorithm

#### Condition to Include an Edge

Assume that all edge costs are distinct. Let $$ S $$ be any subset of nodes that neither empty nor equal to $$ V $$. Let edge $$ e = (v, w) $$ be the minimum cost edge with one end in $$ S $$ and the other in $$ V - S $$. Then every minimum spanning tree contains the edge $$ e $$.

#### Condition to Exclude an Edge

Assume that all edge costs are distinct. Let $$ C $$ be any cycle in $$ G $$, and $$ e = (v, w) $$ be the most expensive edge in $$ C $$. Then $$ e $$ doesn't belong to any minimum spanning tree.

#### Optimality

- **Optimality of Kruskal's Algorithm**: Let edge $$ e = (v, w) $$ added by Kruskal's, and let $$ S $$ be the set of all nodes to which $$ v $$ has a path before $$ e $$ is added. Since adding $$ e $$ doesn't create a cycle, no edge from $$ S $$ to $$ V-S $$ has been added. Thus $$ e $$ is the cheapest edge from $$ S $$ to $$ V-S $$, so it belongs to the minimum spanning tree.

- **Optimality of Prim's Algorithm**: For each iteration of the algorithm, let $$ S $$ be the partial spanning tree and $$ e $$ be the edge that will be added. By definition, $$ e $$ is the cheapest edge from $$ S $$ to $$ V - S $$, so it belongs to the minimum spanning tree.

- **Optimality of Reverse-Delete Algorithm**: Let $$ e = (v, w) $$ be an edge in a cycle $$ C $$ removed by Reverse-Delete. By definition, it's the most expensive edge on $$ C $$. Therefore, $$ e $$ doesn't belong to any minimum spanning tree.

### Implementing Prim's Algorithm

A priority queue could be used to maintain the attachment costs for each node in the graph. For each iteration, the algorithm selects a node with an `ExtractMin` operation and updates the attachment costs of its neighbors with `ChangeKey` operations. For a graph with n nodes and m edges, the algorithm costs $$ O(m log n) $$ time.

## The Union-Find Data Structure

- `MakeUnionFind(S)`: Return a Union-Find data structure on set $$ S $$ where all elements are in seperate sets.
- `Find(u)`: Return the name of the st containing $$ u $$. The time complexity is $$ O(logn) $$.
- `Union(A, B)`: Merge the sets $$ A $$ and $$ B $$ into a single set. The time complexity is $$ O(logn) $$.

### Simple Implementation

Maintain an array `Component` that contains the name of the set currently containing each element. To optimize the time complexity, it's useful to explicitly maintain the list of elements for each set and record their sizes. When merging two sets, always merge the smaller set into the larger set.

For this implementation, `Find` takes $$ O(1) $$ time, `MakeUnionFind(S)` takes $$ O(n) $$ time, and any sequence of $$ k $$ `Union` operations takes at most $$ O(klogk) $$ time.

### Better Implementation

Each node $$ v \in S $$ will be contained in a record with an associated pointer to the name of the set that contains $$ v $$. The initial state is that each element has a pointer that points to itself.

For two sets $$ A $$ and $$ B $$, if $$ A $$ is named after node $$ v $$, and $$ B $$ is named after node $$ u $$, the `Union(A, B)` operation will update $$ u $$'s pointer to point to $$ v $$. Therefore, the `Find` operation has to follow a sequence of pointers to get to the current set name.

For this implementation, `Find` takes $$ O(log n) $$ time, `MakeUnionFind(S)` takes $$ O(n) $$ time, and  `Union` takes $$ O(1) $$ time.

### Path Compression

The path of `Find` operation could be compressed by resetting all pointers along the path to point to the current name of the set. It could make subsequent `Find` operations more efficient. The time complexity of a sequence of `Find` operations with path compression is extremely close to linear time. The actual upper bound is `O(n \alpha (n)) $$, where $$ \alpha (n) $$ is an extremely slow-growing function called the inverse Ackermann function.
