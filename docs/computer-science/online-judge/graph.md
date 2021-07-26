# Graph

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

## Number of Islands

[LeetCode 200](https://leetcode.com/problems/number-of-islands/)

```py
class Solution:
  def numIslands(self, grid: List[List[str]]) -> int:
    parent = {}
    weight = defaultdict(lambda: 1)

    def find(x):
      if parent[x] == None:
        return x
      parent[x] = find(parent[x])
      return parent[x]

    def union(x, y):
      rx = find(x)
      ry = find(y)
      if rx == ry:
        return
      if weight[rx] < weight[ry]:
        parent[rx] = ry
        weight[ry] += weight[rx]
      else:
        parent[ry] = rx
        weight[rx] += weight[ry]

    for i in range(len(grid)):
      for j in range(len(grid[0])):
        if grid[i][j] == '0':
          continue
        parent[(i, j)] = None
        if i > 0 and grid[i - 1][j] == '1':
          union((i - 1, j), (i, j))
        if j > 0 and grid[i][j - 1] == '1':
          union((i, j - 1), (i, j))

    return sum(1 for x in parent.values() if x == None)
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

## Longest Consecutive Sequence

[LeetCode](https://leetcode.com/problems/longest-consecutive-sequence/)

```py
class Solution:
  def longestConsecutive(self, nums: List[int]) -> int:
    nums = set(nums)
    result = 0
    for num in nums:
      if num - 1 in nums:
        continue

      length = 0
      while num in nums:
        num += 1
        length += 1
      result = max(result, length)
    return result
```

```py
class Solution:
  def longestConsecutive(self, nums: List[int]) -> int:
    parent = {}
    weight = defaultdict(lambda: 1)
    def find(x):
      if parent[x] == None:
        return x
      parent[x] = find(parent[x])
      return parent[x]

    def union(x, y):
      rx = find(x)
      ry = find(y)
      if rx == ry:
        return
      if weight[rx] < weight[ry]:
        parent[rx] = ry
        weight[ry] += weight[rx]
      else:
        parent[ry] = rx
        weight[rx] += weight[ry]

    for num in nums:
      if num not in parent:
        parent[num] = None
      if num - 1 in parent:
        union(num, num - 1)
      if num + 1 in parent:
        union(num, num + 1)
    return max(weight.values())
```

## Number of Connected Components in an Undirected Graph

[LeetCode 323](https://leetcode-cn.com/problems/number-of-connected-components-in-an-undirected-graph/)

### Union Find

```py
class Solution:
  def countComponents(self, n: int, edges: List[List[int]]) -> int:
    parent = { k: None for k in range(n) }
    weight = defaultdict(lambda: 1)

    def find(x):
      if parent[x] == None:
        return x
      parent[x] = find(parent[x])
      return parent[x]

    def union(x, y):
      rx = find(x)
      ry = find(y)
      if rx == ry:
        return
      if weight[rx] < weight[ry]:
        parent[rx] = ry
        weight[ry] += weight[rx]
      else:
        parent[ry] = rx
        weight[rx] += weight[ry]

    for x, y in edges:
      union(x, y)
    return sum(1 for v in parent.values() if v == None)
```

### Breadth-first Search

```py
class Solution:
  def countComponents(self, n: int, edges: List[List[int]]) -> int:
    graph = defaultdict(list)
    for x, y in edges:
      graph[x].append(y)
      graph[y].append(x)

    visited = set()
    def bfs(node):
      queue = deque([node])
      while queue:
        x = queue.popleft()
        for neighbor in graph[x]:
          if neighbor in visited:
            continue
          visited.add(neighbor)
          queue.append(neighbor)

    count = 0
    for i in range(n):
      if i in visited:
        continue
      visited.add(i)
      bfs(i)
      count += 1
    return count
```

### Depth-first search

```py
class Solution:
  def countComponents(self, n: int, edges: List[List[int]]) -> int:
    graph = defaultdict(list)
    for x, y in edges:
      graph[x].append(y)
      graph[y].append(x)

    visited = set()
    def dfs(node):
      for neighbor in graph[node]:
        if neighbor in visited:
          continue
        visited.add(neighbor)
        dfs(neighbor)

    count = 0
    for i in range(n):
      if i in visited:
        continue
      visited.add(i)
      dfs(i)
      count += 1
    return count
```

## Graph Valid Tree

[LeetCode 261](https://leetcode-cn.com/problems/graph-valid-tree/)

```py
class Solution:
  def validTree(self, n: int, edges: List[List[int]]) -> bool:
    parent = { k: None for k in range(n) }
    weight = defaultdict(lambda: 1)

    def find(x):
      if parent[x] == None:
        return x
      parent[x] = find(parent[x])
      return parent[x]

    def union(x, y):
      rx = find(x)
      ry = find(y)
      if rx == ry:
        return
      if weight[rx] < weight[ry]:
        parent[rx] = ry
        weight[ry] += weight[rx]
      else:
        parent[ry] = rx
        weight[rx] += weight[ry]

    for x, y in edges:
      if find(x) == find(y):
        return False
      union(x, y)

    return True
```

## Alien Dictionary

[LeetCode 269](https://leetcode-cn.com/problems/alien-dictionary/)

```py
class Solution:
  def alienOrder(self, words: List[str]) -> str:
    graph = defaultdict(list)
    degree = defaultdict(int)
    chars = set()
    for word in words:
      for c in word:
        chars.add(c)

    for i in range(len(words) - 1):
      word1 = words[i]
      word2 = words[i + 1]
      length = min(len(word1), len(word2))
      if word1[:length] == word2[:length] and len(word1) > len(word2):
        return ""

      for j in range(length):
        if word1[j] == word2[j]:
          continue
        graph[word1[j]].append(word2[j])
        degree[word2[j]] += 1
        break

    queue = [c for c in chars if degree[c] == 0]
    for c in queue:
      for n in graph[c]:
        degree[n] -= 1
        if degree[n] == 0:
          queue.append(n)

    if len(queue) != len(graph.keys()):
      return ""
    return "".join(queue)
```

## Number of Provinces

[LeetCode 547](https://leetcode-cn.com/problems/number-of-provinces/)

```py
class Solution:
  def findCircleNum(self, isConnected: List[List[int]]) -> int:
    n = len(isConnected)
    parent = {i: None for i in range(n)}
    weight = defaultdict(lambda: 1)
    def find(x):
      if parent[x] == None:
        return x
      parent[x] = find(parent[x])
      return parent[x]

    def union(x, y):
      rx = find(x)
      ry = find(y)
      if rx == ry:
        return
      if weight[rx] < weight[ry]:
        parent[rx] = ry
        weight[ry] += weight[rx]
      else:
        parent[ry] = rx
        weight[rx] += weight[ry]

    for i in range(n):
      for j in range(n):
        if not isConnected[i][j]:
          continue
        union(i, j)
    return sum(1 for v in parent.values() if v == None)
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

## Redundant Connection

[LeetCode 684](https://leetcode.com/problems/redundant-connection/)

```py
class Solution:
  def findRedundantConnection(self, edges: List[List[int]]) -> List[int]:
    parent = { n: None for n in range(len(edges)) }
    weight = defaultdict(lambda: 1)
    def find(x):
      if parent[x] == None:
        return x
      parent[x] = find(parent[x])
      return parent[x]
    def union(x, y):
      rx = find(x)
      ry = find(y)
      if rx == ry:
        return False
      if weight[rx] < weight[ry]:
        parent[rx] = ry
        weight[ry] += weight[rx]
      else:
        parent[ry] = rx
        weight[rx] += weight[ry]
      return True

    for x, y in edges:
      if not union(x, y):
        return [x, y]

```

## Couples Holding Hands

[LeetCode 765](https://leetcode.com/problems/couples-holding-hands/)

```py
class Solution:
  def minSwapsCouples(self, row: List[int]) -> int:
    parent = { n: None for n in range(len(row) // 2) }
    weight = defaultdict(lambda: 1)
    def find(x):
      if parent[x] == None:
        return x
      parent[x] = find(parent[x])
      return parent[x]
    def union(x, y):
      rx = find(x)
      ry = find(y)
      if rx == ry:
        return False
      if weight[rx] < weight[ry]:
        parent[rx] = ry
        weight[ry] += weight[rx]
      else:
        parent[ry] = rx
        weight[rx] += weight[ry]
      return True

    result = 0
    for i in range(0, len(row), 2):

    return result
```
