# Shortest Paths

## BFS v.s. DFS for Path Finding

* BFS returns path with shortest number of edges.
* Time efficiency is similar for both algorithms.
* Space Efficiency is quite different.
* * DFS is worse for spindly graphs. (Call stack gets very deep.)
* * BFS is worse for bushy graphs. (Queue gets very large.)
* Track the `distTo` and `edgeTo` arrays requires `Θ(V)` memory.

## Shortest Path Tree

* For every vertex v (which is not s) in the graph, find the shortest path from s to v.
* Combine all the edges that the algorithm found above.

## Dijkstra's Algorithm

Instead of BFS, an algorithm which takes into account edge distances (edge weights) is created.

* Create a priority queue.
* Add s to the priority queue with priority 0. 
* Add all other vertices to the priority queue with priority infinity.
* While the priority queue is not empty, and relax all of the edges going out from the vertex.

As long as the edges are all non-negative, Dijkstra's is guaranteed to be optimal.

### Relax

* For the popped vertex v, iterate through its edges.
* For the edge (v,w), compare the `currentBestDistToV + weight(v,w)` and `currentBestDistToW`.
* If the former is smaller, set the `currentBestDistToW = currentBestDistToV + weight(v,w)`, and set `edgeTo[w] = v`.
* Never relax edges that point to already visited vertices.

### Code

```
def dijkstras(source):
    PQ.add(source, 0)
    For all other vertices, v, PQ.add(v, infinity)
    while PQ is not empty:
        p = PQ.removeSmallest()
        relax(all edges from p)

def relax(edge p,q):
   if q is visited (or q is not in PQ):
       return

   if distTo[p] + weight(edge) < distTo[q]:
       distTo[q] = distTo[p] + w
       edgeTo[q] = p
       PQ.changePriority(q, distTo[q])
```

### Proof

* At start, `distTo[source] = 0`.
* After relaxing all edges from source, assume v1 is the closet vertex to the source.
* Suppose there's another path `(s,va,vb,...,v1)`, which is shorter than `(s,v1)`.
* Since `(s,va)` is longer than `(s,v1)`, that path doesn't exist.
* Thus, v1 is the closet vertex to the source, and that argument is still valid for all the edges of v1.

### Runtime

Dijkstra's Algorithm is based on the PQ.

Overall runtime: `O(V log(V) + V log(V) + E log(V))`

Connected graph: `O(E log V)` (E > V)

* `add`: V, each costing `O(log V)` time.
* `removeSmallest`: V, each costing `O(log V)` time.
* `changePriority`: E, each costing `O(log V)` time.
