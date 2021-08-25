# Shortest Path

- [Floyd-Warshall algorithm](https://www.youtube.com/watch?v=4OQeCuLYj-4): Shortest path between all pairs of vertices, negative edges allowed.
- [Bellman-Ford algorithm](https://oi-wiki.org/graph/shortest-path/#bellman-ford): Shortest path from one node to all nodes, negative edges allowed.
- Dijkstra's algorithm: Shortest path from one node to all nodes.

## Network Delay Time

### Dijkstra's algorithm

[LeetCode 743](https://leetcode.com/problems/network-delay-time/)

```py
class Solution:
  def networkDelayTime(self, times: List[List[int]], n: int, k: int) -> int:
    graph = defaultdict(list)
    for source, target, weight in times:
      graph[source].append((target, weight))

    weights = defaultdict(lambda: float('inf'))
    weights[k] = 0

    visited = set()
    queue = []
    heappush(queue, (0, k))
    while queue:
      prev_weight, node = heappop(queue)
      visited.add(node)
      for child, weight in graph[node]:
        if child in visited:
          continue
        total_weight = weight + prev_weight
        if total_weight > weights[child]:
          continue
        weights[child] = total_weight
        heappush(queue, (total_weight, child))

    if len(visited) == n:
      return max(weights.values())
    return -1
```

## Cheapest Flights Within K Stops

[LeetCode 787](https://leetcode.com/problems/cheapest-flights-within-k-stops/)

### Bellman-Ford algorithm

```py
class Solution:
  def findCheapestPrice(self, n: int, flights: List[List[int]], src: int, dst: int, k: int) -> int:
    dist = [float('inf')] * n
    dist[src] = 0

    for i in range(k + 1):
      prev_dist = dist[:]
      for f, t, price in flights:
        dist[t] = min(dist[t], price + prev_dist[f])

    if dist[dst] == float('inf'):
      return -1
    return dist[dst]
```

## Find the City With the Smallest Number of Neighbors at a Threshold Distance

[LeetCode 134](https://leetcode.com/problems/find-the-city-with-the-smallest-number-of-neighbors-at-a-threshold-distance/)

### Floyd–Warshall algorithm

```py
class Solution:
  def findTheCity(self, n: int, edges: List[List[int]], distanceThreshold: int) -> int:
    f = [[float('inf') for _ in range(n)] for _ in range(n)]
    for i in range(n):
      f[i][i] = 0
    for src, dst, w in edges:
      f[src][dst] = w
      f[dst][src] = w

    for k in range(n):
      for x in range(n):
        for y in range(n):
          f[x][y] = min(f[x][y], f[x][k] + f[k][y])

    res = { sum(d <= distanceThreshold for d in f[i]): i for i in range(n) }
    return res[min(res)]
```

### Dijkstra's algorithm

```py
class Solution:
  def findTheCity(self, n: int, edges: List[List[int]], distanceThreshold: int) -> int:
    graph = defaultdict(list)
    for f, t, w in edges:
      graph[f].append((t, w))
      graph[t].append((f, w))

    def shortest_path(node, distanceThreshold):
      weights = defaultdict(lambda: float('inf'))
      weights[node] = 0
      queue = [(0, node)]
      visited = set()
      while queue:
        prev_weight, node = heappop(queue)
        for child, weight in graph[node]:
          if child in visited:
            continue
          total_weight = weight + prev_weight
          if total_weight >= weights[child]:
            continue
          heappush(queue, (total_weight, child))
          weights[child] = total_weight
      return sum(1 if weight <= distanceThreshold else 0 for weight in weights.values())

    cities = { node: shortest_path(node, distanceThreshold) for node in range(n) }
    max_count = float('inf')
    result = None
    for city, count in cities.items():
      if count <= max_count:
        max_count = count
        result = city
    return result
```
