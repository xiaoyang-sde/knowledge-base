class Solution:
  def isMatch(self, s: str, p: str) -> bool:
    dp = [[False for _ in range(len(p) + 1)] for _ in range(len(s) + 1)]
    dp[0][0] = True
    for i in range(2, len(p) + 1):
      dp[0][i] = dp[0][i - 2] and p[i - 1] == '*'

    for i in range(1, len(s) + 1):
      for j in range(1, len(p) + 1):
        if p[j - 1] == '*':
          dp[i][j] = dp[i][j - 2]
          if p[j - 2] == s[i - 1] or p[j - 2] == '.':
            dp[i][j] = dp[i][j] or dp[i - 1][j]
        else:
          dp[i][j] = dp[i - 1][j - 1] and (s[i - 1] == p[j - 1] or p[j - 1] == '.')

    return dp[-1][-1]
