# Interval

## Longest Palindromic Subsequence

[LeetCode 516](https://leetcode.com/problems/longest-palindromic-subsequence/)

> Given a string `s`, find the longest palindromic subsequence's length in `s`.

```py
class Solution:
  def longestPalindromeSubseq(self, s: str) -> int:
    dp = [[0 for _ in range(len(s))] for _ in range(len(s))]
    for i in range(len(s) - 1, -1, -1):
      for j in range(i, len(s)):
        if s[i] != s[j]:
          dp[i][j] = max(dp[i + 1][j], dp[i][j - 1])
        elif i == j:
          dp[i][j] = 1
        elif i + 1 == j:
          dp[i][j] = 2
        else:
          dp[i][j] = dp[i + 1][j - 1] + 2

    return dp[0][-1]
```

## Longest Palindromic Substring

[LeetCode 5](https://leetcode.com/problems/longest-palindromic-substring/)

> Given a string `s`, return the longest palindromic substring in `s`.

```py
class Solution:
  def longestPalindrome(self, s: str) -> str:
    dp = [[False for _ in range(len(s))] for _ in range(len(s))]
    result = ''

    for i in range(len(s) - 1, -1, -1):
      for j in range(i, len(s)):
        if s[i] != s[j]:
          continue
        if i == j or i + 1 == j or dp[i + 1][j - 1]:
          dp[i][j] = True
        if dp[i][j] and j - i + 1 > len(result):
          result = s[i:j + 1]

    return result
```
