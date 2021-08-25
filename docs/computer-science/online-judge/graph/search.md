# Search

## Clone Graph

[LeetCode 133](https://leetcode.com/problems/clone-graph/)

```py
class Solution:
  def cloneGraph(self, root: 'Node') -> 'Node':
    if not root:
      return

    clone = { root: Node(root.val) }
    queue = deque([root])
    while queue:
      node = queue.popleft()
      for n in node.neighbors:
        if n not in clone:
          clone[n] = Node(n.val)
          queue.append(n)
        clone[node].neighbors.append(clone[n])

    return clone[root]
```

```py
class Solution:
  def cloneGraph(self, root: 'Node') -> 'Node':
    if not root:
      return

    clone = { root: Node(root.val) }

    def dfs(node):
      if not node:
        return
      for n in node.neighbors:
        if n not in clone:
          clone[n] = Node(n.val)
          dfs(n)
        clone[node].neighbors.append(clone[n])

    dfs(root)
    return clone[root]
```

## Pacific Atlantic Water Flow

[LeetCode 417](https://leetcode.com/problems/pacific-atlantic-water-flow/)

```py
class Solution:
  def pacificAtlantic(self, heights: List[List[int]]) -> List[List[int]]:
    def bfs(queue):
      directions = ((-1, 0), (1, 0), (0, 1), (0, -1))
      while queue:
        i, j = queue.popleft()
        visited.add((i, j))
        for di, dj in directions:
          ni = i + di
          nj = j + dj
          if ni < 0 or ni >= len(heights) or nj < 0 or nj >= len(heights[0]) or heights[ni][nj] < heights[i][j] or (ni, nj) in visited:
            continue
          queue.append((ni, nj))
      return visited

    pacific_border = [(i, 0) for i in range(len(heights))] + [(0, j) for j in range(len(heights[0]))]
    atlantic_border = [(i, len(heights[0]) - 1) for i in range(len(heights))] + [(len(heights) - 1, j) for j in range(len(heights[0]))]

    pacific = bfs(deque(pacific_border))
    atlantic = bfs(deque(atlantic_border))
    return list(pacific & atlantic)
```

## Is Graph Bipartite?

[LeetCode 785](https://leetcode.com/problems/is-graph-bipartite/)

```py
class Solution:
  def isBipartite(self, graph: List[List[int]]) -> bool:
    s1 = set()
    s2 = set()

    for i in range(len(graph)):
      if i in s1 or i in s2:
        continue
      queue = deque([i])
      while queue:
        for _ in range(len(queue)):
          x = queue.popleft()
          s1.add(x)
          for n in graph[x]:
            if n in s1:
              return False
            if n in s2:
              continue
            queue.append(n)
        s1, s2 = s2, s1

    return True
```
