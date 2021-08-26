# Parent and Ancestor

## Question 1

All nodes have 0, 1, or multiple parent given the realitionship in pairs `((0, 1), (1, 2), (1, 4))`. Find the node has no parent or exactly 1 parent.

```py
from collections import defaultdict, deque

def findParent(inputList):
  parent_count = {}
  result = []
  for parent, child in inputList:
    parent_count[child] = parent_count.get(child, 0) + 1
    parent_count[parent] = parent_count.get(parent, 0)

  for node, count in parent_count.items():
    if count <= 1:
      result.append(node)
  return result
```

## Question 2

Find whether or not two nodes have a common ancestor.

```py
def commonAncestor(inputList, a, b):
  reverse_graph = defaultdict(list)
  for parent, children in inputList:
    reverse_graph[children].append(parent)

  def bfs(root):
    queue = deque([root])
    visited = set()
    while queue:
      node = queue.popleft()
      for p in reverse_graph[node]:
        if p in visited:
          continue
        queue.append(p)
        visited.add(p)
    return visited

  return len(bfs(a).intersection(bfs(b))) > 0
```

## Question 3

Find the furthest ancestor of a node.

```py
def furthestAncestor(inputList, node):
  reverse_graph = defaultdict(list)
  for parent, children in inputList:
    reverse_graph[children].append(parent)

  max_distance = 0
  result = -1
  queue = deque([(node, 0)])
  visited = set()

  while queue:
    node, distance = queue.popleft()
    if distance > max_distance:
      result = node
      max_distance = distance
    for p in reverse_graph[node]:
      if p in visited:
        continue
      queue.append((p, distance + 1))
      visited.add(p)

  return result
```
