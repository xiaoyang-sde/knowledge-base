# Topological Sort

## Course Schedule

[LeetCode 207](https://leetcode.com/problems/course-schedule/)

```py
class Solution:
  def canFinish(self, numCourses: int, prerequisites: List[List[int]]) -> bool:
    graph = defaultdict(list)
    degree = defaultdict(int)
    for course, pre in prerequisites:
      graph[pre].append(course)
      degree[course] += 1

    queue = [x for x in range(numCourses) if degree[x] == 0]
    for x in queue:
      for n in graph[x]:
        degree[n] -= 1
        if degree[n] == 0:
          queue.append(n)

    return len(queue) == numCourses
```

## Course Schedule II

[LeetCode 210](https://leetcode.com/problems/course-schedule-ii/)

```py
class Solution:
  def findOrder(self, numCourses: int, prerequisites: List[List[int]]) -> List[int]:
    graph = defaultdict(list)
    degree = defaultdict(int)
    for course, pre in prerequisites:
      graph[pre].append(course)
      degree[course] += 1

    queue = [course for course in range(numCourses) if degree[course] == 0]
    for course in queue:
      for neighbor in graph[course]:
        degree[neighbor] -= 1
        if degree[neighbor] == 0:
          queue.append(neighbor)
    if len(queue) == numCourses:
      return queue
    return []
```

## Course Schedule IV

[LeetCode 1462](https://leetcode.com/problems/course-schedule-iv/)

```py
class Solution:
  def checkIfPrerequisite(self, numCourses: int, prerequisites: List[List[int]], queries: List[List[int]]) -> List[bool]:
    graph = defaultdict(list)
    degree = defaultdict(int)
    pre = defaultdict(set)

    for a, b in prerequisites:
      graph[a].append(b)
      pre[b].add(a)
      degree[b] += 1

    queue = deque([course for course in range(numCourses) if degree[course] == 0])
    while queue:
      x = queue.popleft()
      for n in graph[x]:
        pre[n] = pre[n].union(pre[x])
        degree[n] -= 1
        if degree[n] == 0:
          queue.append(n)

    return [u in pre[v] for u, v in queries]
```

## Find Eventual Safe States

[LeetCode 802](https://leetcode.com/problems/find-eventual-safe-states/)

```py
class Solution:
  def eventualSafeNodes(self, graph: List[List[int]]) -> List[int]:
    reverse_graph = defaultdict(list)
    out_degree = defaultdict(int)
    for node, children in enumerate(graph):
      out_degree[node] += len(children)
      for child in children:
        reverse_graph[child].append(node)

    result = []
    queue = deque([node for node in range(len(out_degree)) if out_degree[node] == 0])
    while queue:
      node = deque.popleft()
      result.append(node)
      for parent in reverse_graph[node]:
        out_degree[parent] -= 1
        if out_degree[parent] == 0:
          queue.append(parent)
    return sorted(result)
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
