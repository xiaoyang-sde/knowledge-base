from collections import defaultdict, deque

class TreeNode:
  def __init__(self, val=0, left=None, right=None):
    self.val = val
    self.left = left
    self.right = right

class Solution:
  def verticalOrder(self, root: TreeNode):
    if not root:
      return []
    column_map = defaultdict(list)
    min_column = 0
    queue = deque([(0, root)])
    while queue:
      column, node = queue.popleft()
      column_map[column].append(node.val)
      min_column = min(min_column, column)
      if node.left:
        queue.append((column - 1, node.left))
      if node.right:
        queue.append((column + 1, node.right))

    result = []
    while min_column in column_map:
      result.append(column_map[min_column])
      min_column += 1
    return result

  def verticalTraversal(self, root: TreeNode):
    if not root:
      return []
    column_map = defaultdict(list)
    min_column = 0
    queue = deque([(0, 0, root)])
    while queue:
      row, column, node = queue.popleft()
      if len(column_map[column]) > 0 and column_map[column][-1][0] == row:
        column_map[column][-1][1].append(node.val)
      else:
        column_map[column].append((row, [node.val]))

      min_column = min(min_column, column)
      if node.left:
        queue.append((row + 1, column - 1, node.left))
      if node.right:
        queue.append((row + 1, column + 1, node.right))

    result = []
    while min_column in column_map:
      column =[]
      for row, nodes in column_map[min_column]:
        column += sorted(nodes)
      result.append(column)
      min_column += 1
    return result
