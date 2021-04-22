# Microsoft

## 236. Lowest Common Ancestor of a Binary Tree

```py
class Solution:
  def lowestCommonAncestor(self, root: 'TreeNode', p: 'TreeNode', q: 'TreeNode') -> 'TreeNode':
    if not root or root == p or root == q:
      return root

    left = self.lowestCommonAncestor(root.left, p, q)
    right = self.lowestCommonAncestor(root.right, p, q)

    if left and right:
      return root
    else:
      return left or right
```

## 215. Kth Largest Element in an Array

```py
class Solution:
  def findKthLargest(self, nums: List[int], k: int) -> int:
    heap = []
    for num in nums:
      if len(heap) < k:
        heappush(heap, num)
      elif num > heap[0]:
        heappushpop(heap, num)
    return heap[0]
```

## 47. Permutations II

```py
class Solution:
  def permuteUnique(self, nums: List[int]) -> List[List[int]]:
    def backtrack(cur, nums):
      if not nums:
        result.append(cur)
      for i in range(len(nums)):
        if i > 0 and nums[i] == nums[i - 1]:
          continue
        backtrack(cur + [nums[i]], nums[:i] + nums[i + 1:])

    result = []
    backtrack([], sorted(nums))
    return result
```

## 207. Course Schedule

```py
class Solution:
    def canFinish(self, numCourses: int, prerequisites: List[List[int]]) -> bool:
        graph = defaultdict(list)
        degree = defaultdict(lambda: 0)
        for pre, course in prerequisites:
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
