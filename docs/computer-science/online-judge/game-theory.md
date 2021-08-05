# Game Theory

## Predict the Winner

[LeetCode 486](https://leetcode.com/problems/predict-the-winner/submissions/)

Let `dp[i][j]` be the optimal score when current array is `nums[i:j + 1]`.

- If `i == j`, `dp[i][j] = nums[i]`
- If `i < j`, `dp[i][j] = max(nums[i] - dp[i + 1][j], nums[j] - dp[i][j - 1])`
- If `i > j`, `dp[i][j] = 0`

```py
class Solution:
  def PredictTheWinner(self, nums: List[int]) -> bool:
    dp = [[0 for _ in range(len(nums))] for _ in range(len(nums))]
    for i, num in enumerate(nums):
        dp[i][i] = num
    for i in range(len(nums) - 2, -1, -1):
      for j in range(i + 1, len(nums)):
        dp[i][j] = max(nums[i] - dp[i + 1][j], nums[j] - dp[i][j - 1])
    return dp[0][-1] >= 0
```

## Stone Game

[LeetCode 877](https://leetcode.com/problems/stone-game/)

```py
class Solution:
  def stoneGame(self, piles: List[int]) -> bool:
    dp = [[0 for _ in range(len(piles))] for _ in range(len(piles))]
    for index, pile in enumerate(piles):
      dp[index][index] = pile

    for i in range(len(piles) - 2, -1, -1):
      for j in range(i + 1, len(piles)):
        dp[i][j] = max(-dp[i + 1][j] + piles[i], -dp[i][j - 1] + piles[j])

    return dp[0][-1] > 0
```
