# Dynamic Programming

## Word Break

[LeetCode 139](https://leetcode.com/problems/word-break/)

> Given a string `s` and a dictionary of strings `wordDict`, return true if `s` can be segmented into a space-separated sequence of one or more dictionary words.

```py
class Solution:
  def wordBreak(self, s: str, wordDict: List[str]) -> bool:
    dp = [True] + [False] * len(s)

    for i in range(1, len(s) + 1):
      for word in wordDict:
        if s[i - len(word):i] == word and dp[i - len(word)]:
          dp[i] = True

    return dp[-1]
```

## Word Break II

[LeetCode 140](https://leetcode.com/problems/word-break-ii/)

> Given a string s and a dictionary of strings wordDict, add spaces in s to construct a sentence where each word is a valid dictionary word. Return all such possible sentences in any order.

```py
class Solution:
  def wordBreak(self, s: str, wordDict: List[str]) -> List[str]:
    dp = [[[]]] + [[]] * len(s)

    for i in range(1, len(s) + 1):
      for word in wordDict:
        if s[i - len(word):i] == word and len(dp[i - len(word)]) > 0:
          for x in dp[i - len(word)]:
            dp[i].append(x + [word])

    return [' '.join(x) for x in dp[-1]]
```

## House Robber

[LeetCode 198](https://leetcode.com/problems/house-robber/)

```py
class Solution:
  def rob(self, nums: List[int]) -> int:
    prev, cur = 0, 0
    for num in nums:
      prev, cur = cur, max(prev + num, cur)
    return max(prev, cur)
```

## House Robber II

[LeetCode 213](https://leetcode.com/problems/house-robber-ii/)

```py
class Solution:
  def _rob(self, nums: List[int]) -> int:
    # Solution of House robber
    prev, cur = 0, 0
    for num in nums:
      prev, cur = cur, max(prev + num, cur)
    return max(prev, cur)

  def rob(self, nums: List[int]) -> int:
    return max(
      self._rob(nums[1:]),
      nums[0] + self._rob(nums[2:-1]),
    )
```

## All Possible Full Binary Trees

[LeetCode 894](https://leetcode.com/problems/all-possible-full-binary-trees/)

```py
class Solution:
  @cache
  def allPossibleFBT(self, n: int) -> List[TreeNode]:
    if n % 2 == 0:
      return []
    if n == 1:
      return [TreeNode()]

    result = []
    for i in range(0, n, 2):
      for j in self.allPossibleFBT(i - 1):
        for k in self.allPossibleFBT(n - i):
          result.append(TreeNode(left=j, right=k))

    return result
```

```py
class Solution:
  def allPossibleFBT(self, n: int) -> List[TreeNode]:
    if n % 2 == 0:
      return []
    dp = [[] for _ in range(n + 1)]
    dp[1].append(TreeNode())

    for count in range(3, n + 1, 2):
      for i in range(1, count, 2):
        for j in dp[i]:
          for k in dp[count - i - 1]:
            dp[count].append(TreeNode(left=j, right=k))

    return dp[-1]
```

## Ugly Number II

[LeetCode 264](https://leetcode.com/problems/ugly-number-ii/)

```py
class Solution:
  def nthUglyNumber(self, n: int) -> int:
    dp = [1] * n
    n2 = 0
    n3 = 0
    n5 = 0
    for i in range(1, n):
      dp[i] = min(dp[n2] * 2, dp[n3] * 3, dp[n5] * 5)
      if dp[i] == dp[n2] * 2:
        n2 += 1
      if dp[i] == dp[n3] * 3:
        n3 += 1
      if dp[i] == dp[n5] * 5:
        n5 += 1

    return dp[-1]
```

## Super Ugly Number

```py
class Solution:
  def nthSuperUglyNumber(self, n: int, primes: List[int]) -> int:
    pointer = [0] * len(primes)
    dp = [1] * n
    for i in range(1, n):
      dp[i] = min(dp[pointer[index]] * primes[index] for index in pointer)
      for index in pointer:
        num = dp[pointer[index]] * primes[index]
        if num == dp[i]:
          pointer[index] += 1

    return dp[-1]
```

## Flip String to Monotone Increasing

[LeetCode 926](https://leetcode.com/problems/flip-string-to-monotone-increasing/)

Let `dp[i]` be the number of flips required to make the string monotonne increasing if the pivot point is at `s[i + 1]`.

```py
class Solution:
  def minFlipsMonoIncr(self, s: str) -> int:
    dp = [0 for _ in range(len(s) + 1)]
    dp[0] = s.count('0')
    for index, c in enumerate(s):
      if c == '1':
        dp[index + 1] = dp[index] + 1
      else:
        dp[index + 1] = dp[index] - 1

    return min(dp)
```

## Arithmetic Slices

[LeetCode 413](https://leetcode.com/problems/arithmetic-slices/)

```py
class Solution:
  def numberOfArithmeticSlices(self, nums: List[int]) -> int:
    start = 0
    window = 0
    result = 0
    for i in range(1, len(nums)):
      diff = nums[i] - nums[i - 1]
      if window == diff and i - start >= 2:
        result += i - start - 1
      else:
        window = diff
        start = i - 1
    return result
```
