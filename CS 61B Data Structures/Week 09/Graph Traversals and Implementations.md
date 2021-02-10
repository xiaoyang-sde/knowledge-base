# Graph Traversals and Implementations

## BFS (Breadth First Search)

This algorithm of exploring a vertex's immediate children before moving on to its grandchildren is known as Breadth First Traversal.

* Initialize the a queue with the starting vertex and mark that vertex.
* Repeat until the queue is empty:
* * Remove vertex v from the front of the queue.
* * For each unmarked neighbor n of v:
* * * Mark n.
* * * Add n to the end of the queue.
* * * Set edgeTo[n] = v.
* * * Set distTo[n] = distTo[v] + 1.

## Graph API

To Implement our graph algorithms like BreadthFirstPaths and DepthFirstPaths:
* An API for Graphs.
* An underlying data structure to represent the graphs.

The API uses integers to represent vertices. `Map<Label, Integer>` is required to lookup vertices by labels.

```java
public class Graph {
  public Graph(int V):               // Create empty graph with v vertices
  public void addEdge(int v, int w): // add an edge v-w
  Iterable<Integer> adj(int v):      // vertices adjacent to v
  int V():                           // number of vertices
  int E():                           // number of edges
...
```

* Number of vertices must be specified in advance.
* Does not support weights on nodes or edges.
* Has no method for getting the degree for a vertex.

### degree(Graph G, int v)

```java
public static int degree(Graph G, int v) {
    int degree = 0;
    for (int w: G.adj(v)) {
        degree += 1;
    }
    return degree;
}
```

### print(Graph G)

```java
public static void print(Graph G) {
	for (int v = 0; v < G.V(); v += 1) {
 	    for (int w : G.adj(v)) {
    	    System.out.println(v + “-” + w);
    	}
    }
}
```

## Graph Representations

The choice of data structure to represent the graph should have implications on both runtime and memory usage.

![Graph Representations](https://joshhug.gitbooks.io/hug61b/content/assets/Screen%20Shot%202019-03-27%20at%202.03.44%20AM.png)

### Adjacency Matrix

Use a 2D array. There is an edge connecting vertex `s` to `t` if that corresponding cell is 1, which represents `true`.

![Adjacency Matrix](https://joshhug.gitbooks.io/hug61b/content/assets/Screen%20Shot%202019-03-27%20at%201.58.11%20AM.png)

### Edge Sets

Use a single set to create a collection of all edges.

![Edge Sets](https://joshhug.gitbooks.io/hug61b/content/assets/Screen%20Shot%202019-03-27%20at%202.03.36%20AM.png)

### Adjacency Lists

Use an array of lists, indexed by vertex number. Each index contains the index of its adjancent vertices.

![Adjacency Lists](https://joshhug.gitbooks.io/hug61b/content/assets/Screen%20Shot%202019-03-27%20at%202.05.55%20AM.png)

#### Implementation

```java
public class Graph {
	private final int V;
    private List<Integer>[] adj;
	
	public Graph(int V) {
        this.V = V;
        adj = (List<Integer>[]) new ArrayList[V];
        for (int v = 0; v < V; v++) {
            adj[v] = new ArrayList<Integer>();
        }
	} 

	public void addEdge(int v, int w) {
        adj[v].add(w);
        adj[w].add(v);
	}

	public Iterable<Integer> adj(int v) {
        return adj[v];
	}
}
```

### Runtime

| Representation | addEdge(s, t)| for(w : adj(v)) | print() | hasEdge(s, t) | space used |
| --- |---|----| --- | --- | --- |
| Adjacency Matrix | `Θ(1)` | `Θ(V)` | `Θ(V^2)` | `Θ(1)` | `Θ(V^2)` |
| Edge Sets | `Θ(1)` | `Θ(E)` | `Θ(E)` | `Θ(E)` | `Θ(E)` |
| Adjacency Lists | `Θ(1)` | `Θ(1) to Θ(V)` | `Θ(V+E)` | `Θ(degree(v))` | `Θ(V+E)` |

## Implementation

Create a Paths API, which takes a Graph object and query the graph-processing for information.

```java
public class Paths {
    public Paths(Graph G, int s)    // Find all paths from G
    boolean hasPathTo(int v)        // is there a path from s to v?
    Iterable<Integer> pathTo(int v) // path from s to v (if any)
}
```

### Depth First Search

```java
public class DepthFirstPaths {
    private boolean[] marked;
    private int[] edgeTo;
    private int s;

    public DepthFirstPaths(Graph G, int s) {
        ...             // Data structure initialization
        dfs(G, s);      // Recursively find vertices connected to s
    }

    private void dfs(Graph G, int v) {
        marked[v] = true;
        for (int w: G.adj(v)) {
            if (!marked[w]) {
                edgeTo[w] = v;
                dfs(G, w);
            }
        }
    }

    public Iterable < Integer > pathTo(int v) {
        if (!hasPathTo(v)) return null;
        List <Integer> path = new ArrayList < > ();
        for (int x = v; x != s; x = edgeTo[x]) {
            path.add(x);
        }
        path.add(s);
        Collections.reverse(path);
        return path;
    }

    public boolean hasPathTo(int v) {
        return marked[v];
    }
}
```

#### Runtime

* The DepthFirstPaths constructor: `O(V+E)`.
* * Each vertex is visited at most once (`O(V)`).
* * Each edge is considered at most twice (`O(E)`).

### Breadth First Search

```java
public class BreadthFirstPaths {
    private boolean[] marked;
    private int[] edgeTo;
    ...

    private void bfs(Graph G, int s) {
        Queue <Integer> fringe = new Queue <Integer> ();
        fringe.enqueue(s);
        marked[s] = true;
        while (!fringe.isEmpty()) {
            int v = fringe.dequeue();
            for (int w: G.adj(v)) {
                if (!marked[w]) {
                    fringe.enqueue(w);
                    marked[w] = true;
                    edgeTo[w] = v;
                }
            }
        }
    }
}
```

## Graph Problems

### Adjacency List Based Graphs

Problem|Description|Solution|Efficiency (adj. list)
|---|---|---|---|
s-t paths|Find a path from s to every reachable vertex.|DepthFirstPaths|`O(V+E)` time, `Θ(V)` space.
s-t shortest paths|Find a shortest path from s to every reachable vertex.|BreadthFirstPaths|`O(V+E)` time, `Θ(V)` space.

### Adjacency Matrix Based Graphs

Problem|Description|Solution|Efficiency (adj. list)
|---|---|---|---|
s-t paths|Find a path from s to every reachable vertex.|DepthFirstPaths|`O(V^2)` time, `Θ(V)` space.
s-t shortest paths|Find a shortest path from s to every reachable vertex.|BreadthFirstPaths|`O(V^2)` time, `Θ(V)` space.