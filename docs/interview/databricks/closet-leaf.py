from collections import defaultdict, deque

class TreeNode:
  def __init__(self, val=0, left=None, right=None):
    self.val = val
    self.left = left
    self.right = right

class Solution:
  def findClosestLeaf(self, root: TreeNode, k: int) -> int:
    graph = defaultdict(list)
    leaves = set()
    def dfs(node):
      if not node:
        return
      if not node.left and not node.right:
        leaves.add(node.val)
        return
      if node.left:
        graph[node.val].append(node.left.val)
        graph[node.left.val].append(node.val)
        dfs(node.left)
      if node.right:
        graph[node.val].append(node.right.val)
        graph[node.right.val].append(node.val)
        dfs(node.right)

    dfs(root)
    queue = deque([k])
    visited = set([k])
    while queue:
      node = queue.popleft()
      if node in leaves:
        return node

      for child in graph[node]:
        if child in visited:
          continue
        queue.append(child)
        visited.add(child)
