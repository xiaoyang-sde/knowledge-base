# Union Find

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

## Longest Consecutive Sequence

[LeetCode 128](https://leetcode.com/problems/longest-consecutive-sequence/)

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

## Making a Large Island

```py
class Solution:
  def largestIsland(self, grid: List[List[int]]) -> int:
    if not grid:
      return 0
    parent = {}
    weight = {}
    def find(x):
      if not parent[x]:
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
        if grid[i][j] == 0:
          continue
        parent[(i, j)] = None
        weight[(i, j)] = 1
        if i > 0 and grid[i - 1][j] == 1:
          union((i, j), (i - 1, j))
        if j > 0 and grid[i][j - 1] == 1:
          union((i, j), (i, j - 1))

    if weight:
      result = max(weight.values())
    else:
      result = 0

    for i in range(len(grid)):
      for j in range(len(grid[0])):
        if grid[i][j] == 1:
          continue

        size = 1
        v = set()

        for di, dj in ((1, 0), (-1, 0), (0, 1), (0, -1)):
          ni = i + di
          nj = j + dj
          if ni < 0 or ni >= len(grid) or nj < 0 or nj >= len(grid[0]):
            continue
          if grid[ni][nj] == 0:
            continue

          root = find((ni, nj))
          if root in v:
            continue
          v.add(root)
          size += weight[root]

        result = max(result, size)
    return result
```
