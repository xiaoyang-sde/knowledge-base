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

## 53. Maximum Subarray

```py
class Solution:
  def maxSubArray(self, nums: List[int]) -> int:
    result = float('-inf')
    cur = 0
    for i in nums:
      if cur < 0:
        cur = 0
      cur += i
      result = max(result, cur)
    return result
```

## 15. 3Sum

```py
class Solution:
  def threeSum(self, nums: List[int]) -> List[List[int]]:
    if len(nums) < 3:
      return []
    nums.sort()
    result = []
    for i in range(len(nums)):
      if i > 0 and nums[i] == nums[i - 1]:
        continue
      if nums[i] > 0:
        break

      j = i + 1
      k = len(nums) - 1
      while j < k:
        if nums[i] + nums[j] + nums[k] < 0:
          j += 1
        elif nums[i] + nums[j] + nums[k] > 0:
          k -= 1
        else:
          result.append((nums[i], nums[j], nums[k]))
          j += 1
          while j < k and nums[j] == nums[j - 1]:
            j += 1
          k -= 1
          while j < k and nums[k] == nums[k + 1]:
            k -= 1
    return result
```

## 91. Decode Ways

```py
class Solution:
  def numDecodings(self, s: str) -> int:
    cache = {}
    def dp(s):
      if not s:
        return 1
      if s[0] == '0':
        return 0
      if s in cache:
        return cache[s]

      result = dp(s[1:])
      if len(s) > 1 and (s[0] == '1' or (s[0] == '2' and 0 <= int(s[1]) < 7)):
        result += dp(s[2:])
      cache[s] = result
      return result

    return dp(s)
```

## 543. Diameter of Binary Tree

```py
class Solution:
  def diameterOfBinaryTree(self, root: TreeNode) -> int:
    def dfs(node):
      if not node:
        return -1
      left  = dfs(node.left) + 1
      right = dfs(node.right) + 1
      self.ans = max(self.ans, left + right)
      return max(left, right)

    self.ans = 0
    dfs(root)
    return self.ans
```

## 39. Combination Sum

```py
class Solution:
  def combinationSum(self, candidates: List[int], target: int) -> List[List[int]]:
    def backtrack(cur, s, target, index):
      if s == target:
        result.append(cur[:])
      if s > target:
        return
      for i in range(index, len(candidates)):
        cur.append(candidates[i])
        backtrack(cur, s + candidates[i], target, i)
        cur.pop()

    result = []
    backtrack([], 0, target, 0)
    return result
```

## 121. Best Time to Buy and Sell Stock

```py
class Solution:
  def maxProfit(self, prices: List[int]) -> int:
    min_price = float('inf')
    max_profit = 0
    for price in prices:
      min_price = min(min_price, price)
      max_profit = max(max_profit, price - min_price)
    return max_profit
```

## 122. Best Time to Buy and Sell Stock II

```py
class Solution:
  def maxProfit(self, prices: List[int]) -> int:
    result = 0
    for i in range(1, len(prices)):
      if prices[i - 1] < prices[i]:
        result += prices[i] - prices[i - 1]
    return result
```

## 98. Validate Binary Search Tree

```py
class Solution:
  def isValidBST(self, root: TreeNode, left = float('-inf'), right = float('inf')) -> bool:
    return not root or left < root.val < right and self.isValidBST(root.left, left, root.val) and self.isValidBST(root.right, root.val, right)
```

## 62. Unique Paths

```py
class Solution:
  def uniquePaths(self, m: int, n: int) -> int:
    grid = [[0 for _ in range(n)] for _ in range(2)]
    for i in range(m):
      for j in range(n):
        if i == 0 or j == 0:
          grid[i % 2][j] = 1
        else:
          grid[i % 2][j] = grid[(i - 1) % 2][j] + grid[i % 2][j - 1]
    return grid[(m - 1) % 2][-1]
```

## 560. Subarray Sum Equals K

```py
class Solution:
  def subarraySum(self, nums: List[int], k: int) -> int:
    sums = defaultdict(lambda: 0)
    sums[0] = 1
    cur = 0
    result = 0
    for num in nums:
      cur += num
      result += sums[cur - k]
      sums[cur] += 1

    return result
```

## 3. Longest Substring Without Repeating Characters

```py
class Solution:
  def lengthOfLongestSubstring(self, s: str) -> int:
    result = 0
    start = 0
    chars = defaultdict(lambda: 0)
    for end in range(len(s)):
      while chars[s[end]] > 0:
        chars[s[start]] -= 1
        start += 1

      chars[s[end]] += 1
      result = max(result, end - start + 1)
    return result
```
