# Subsequence

## Arithmetic Slices II - Subsequence

[LeetCode 446](https://leetcode.com/problems/arithmetic-slices-ii-subsequence/)

- Let `dp[i]` be a dictionary of `diff`
- Let `dp[i][diff]` be the number of subsequences (length >= 2) that ends with `nums[i]` with difference `diff`
- Let `dp[i][nums[i] - nums[j]] = dp[j][nums[i] - nums[j]] + 1`

```py
class Solution:
  def numberOfArithmeticSlices(self, nums: List[int]) -> int:
    dp = [defaultdict(int) for _ in nums]
    result = 0
    for i in range(1, len(nums)):
      for j in range(i):
        diff = nums[i] - nums[j]
        dp[i][diff] += dp[j][diff] + 1
        result += dp[j][diff]

    return result
```

## Longest Common Subsequence

[LeetCode 1143](https://leetcode.com/problems/longest-common-subsequence/)

```py
class Solution:
  def longestCommonSubsequence(self, text1: str, text2: str) -> int:
    dp = [[0 for _ in range(len(text2) + 1)] for _ in range(2)]

    for i in range(len(text1)):
      for j in range(len(text2)):
        if text1[i] == text2[j]:
          dp[i + 1][j + 1] = dp[i][j] + 1
        else:
          dp[i + 1][j + 1] = max(dp[i][j + 1], dp[i + 1][j])

    return dp[-1][-1]
```
