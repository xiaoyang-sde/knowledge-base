# Reductions and Decomposition

## Topological Sort

* Topological Sort is an ordering of a graph's vertices such that for every directed edge u→v, u comes beforev in the ordering.
* Topological sorts only apply to directed, acyclic (no cycles) graphs (DAG).
* Topological sort is called a linearization of the graph, since all the vertices in the graph could be redrawn in one line.

### Topological Sort Algorithm

* Perform a DFS traversal from every vertex in the graph, not clearing markings in between traversals.
* Record DFS postorder along the way.
* Topological ordering is the reverse of the postorder.

The total runtime of DFS is `O(V + E)`.

```
topological(DAG):
    initialize marked array
    initialize postOrder list
    for all vertices in DAG:
        if vertex is not marked:
            dfs(vertex, marked, postOrder)
    return postOrder reversed

dfs(vertex, marked, postOrder):
    marked[vertex] = true
    for neighbor of vertex:
        dfs(neighbor, marked, postOrder)
    postOrder.add(vertex)
```

## Shortest Path Algorithm for DAGs

* Visit vertices in topological order.
* On each visit, relax all outgoing edges.

