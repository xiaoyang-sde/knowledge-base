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

## Best Time to Buy and Sell Stock

[LeetCode 121](https://leetcode.com/problems/best-time-to-buy-and-sell-stock/)

> You are given an array prices where `prices[i]` is the price of a given stock on the ith day. You want to maximize your profit by choosing a single day to buy one stock and choosing a different day in the future to sell that stock.

- Hold -> Hold
- Hold -> Sell
- Sell -> Sell

```py
class Solution:
  def maxProfit(self, prices: List[int]) -> int:
    hold = float('-inf')
    sell = 0
    for price in prices:
      hold = max(hold, -price)
      sell = max(sell, hold + price)
    return sell
```

## Best Time to Buy and Sell Stock II

[LeetCode 122](https://leetcode.com/problems/best-time-to-buy-and-sell-stock-ii/)

> You are given an array prices where `prices[i]` is the price of a given stock on the i-th day. Find the maximum profit you can achieve. You may complete as many transactions as you like.

- Sell -> Hold
- Hold -> Hold
- Hold -> Sell
- Sell -> Sell

```py
class Solution:
  def maxProfit(self, prices: List[int]) -> int:
    hold = float('-inf')
    sell = 0
    for price in prices:
      hold = max(hold, sell - price)
      sell = max(sell, hold + price)
    return sell
```

## Best Time to Buy and Sell Stock III

[LeetCode 123](https://leetcode.com/problems/best-time-to-buy-and-sell-stock-iii/)

> You are given an array prices where `prices[i]` is the price of a given stock on the ith day. Find the maximum profit you can achieve. You may complete at most two transactions.

- Init -> Hold 1
- Hold 1 -> Hold 1
- Hold 1 -> Sell 1
- Sell 1 -> Sell 1
- Sell 1 -> Hold 2
- Hold 2 -> Hold 2
- Hold 2 -> Sell 2

```py
class Solution:
  def maxProfit(self, prices: List[int]) -> int:
    s1 = s2 = s3 = s4 = float('-inf')
    for price in prices:
      s1 = max(s1, -price)
      s2 = max(s2, s1 + price)
      s3 = max(s3, s2 - price)
      s4 = max(s4, s3 + price)
    return s4
```

## Best Time to Buy and Sell Stock IV

[LeetCode 188](https://leetcode.com/problems/best-time-to-buy-and-sell-stock-iv/)

> You are given an integer array prices where `prices[i]` is the price of a given stock on the ith day, and an integer k. Find the maximum profit you can achieve. You may complete at most k transactions.

- Init -> Hold 1
- Hold 1 -> Hold 1
- Hold 1 -> Sell 1
- Sell 1 -> Sell 1
- Sell 1 -> Hold 2
- Hold 2 -> Hold 2
...
- Sell K - 1 -> Hold K
- Hold K -> Hold K
- Hold K -> Sell K
- Sell K -> Sell K

```py
class Solution:
  def maxProfit(self, k: int, prices: List[int]) -> int:
    if not prices or k == 0:
      return 0
    state = [float('-inf')] * (2 * k)

    for price in prices:
      state[0] = max(state[0], -price)
      state[1] = max(state[1], state[0] + price)

      for i in range(2, len(state), 2):
        state[i] = max(state[i], state[i - 1] - price)
        state[i + 1] = max(state[i + 1], state[i] + price)
    return state[-1]
```

## Best Time to Buy and Sell Stock with Cooldown

[LeetCode 309](https://leetcode.com/problems/best-time-to-buy-and-sell-stock-with-cooldown/)

> You are given an array prices where `prices[i]` is the price of a given stock on the ith day. Find the maximum profit you can achieve. You may complete as many transactions as you like. After you sell your stock, you cannot buy stock on the next day.

- Hold -> Sell
- Hold -> Hold
- Idle -> Hold
- Idle -> Idle
- Sell -> Idle

```py
class Solution:
  def maxProfit(self, prices: List[int]) -> int:
    hold = 0
    idle = 0
    sell = float('-inf')
    for price in prices:
      idle, hold, sell = max(idle, sell), max(hold, idle - price), hold + price
    return max(sell, idle)
```

## Best Time to Buy and Sell Stock with Transaction Fee

[LeetCode 714](https://leetcode.com/problems/best-time-to-buy-and-sell-stock-with-transaction-fee)

> You are given an array prices where `prices[i]` is the price of a given stock on the ith day, and an integer fee representing a transaction fee. Find the maximum profit you can achieve. You may complete as many transactions as you like, but you need to pay the transaction fee for each transaction.

- Hold -> Hold
- Sell -> Hold
- Hold -> Sell
- Sell -> Sell

```py
class Solution:
  def maxProfit(self, prices: List[int], fee: int) -> int:
    hold = float('-inf')
    sell = 0
    for price in prices:
      hold = max(hold, sell - price)
      sell = max(sell, hold + price - fee)
    return sell
```

## Longest Palindromic Substring

[LeetCode 5](https://leetcode.com/problems/longest-palindromic-substring/)

> Given a string `s`, return the longest palindromic substring in `s`.

```py
class Solution:
  def longestPalindrome(self, s: str) -> str:
    dp = [[0 for _ in range(len(s))] for _ in range(len(s))]
    result = ''

    for i in range(len(s) - 1, -1, -1):
      for j in range(i, len(s)):
        if s[i] != s[j]:
          continue
        elif i == j:
          dp[i][j] = 1
        elif i + 1 == j:
          dp[i][j] = 2
        elif dp[i + 1][j - 1] != 0:
          dp[i][j] = dp[i + 1][j - 1] + 2
        if dp[i][j] > len(result):
          result = s[i:j + 1]

    return result
```

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

## Burst Balloons

[LeetCode 312](https://leetcode.com/problems/burst-balloons/)

- Let `dp[i][j]` be the maximum number of coins in the range `(i, j)`, not including `i` and `j`.
- Let `k`

```py
class Solution:
  def maxCoins(self, nums: List[int]) -> int:
    nums = [1] + nums + [1]
    dp = [[0 for _ in range(len(nums))] for _ in range(len(nums))]
    for i in range(len(nums) - 1, -1, -1):
      for j in range(i + 1, len(nums)):
        for k in range(i + 1, j):
          dp[i][j] = max(dp[i][j], dp[i][k] + dp[k][j] + nums[i] * nums[k] * nums[j])
    return dp[0][-1]
```

```py
class Solution:
  def maxCoins(self, nums: List[int]) -> int:
    nums = [1] + nums + [1]

    @cache
    def dfs(i, j):
      result = 0
      for k in range(i + 1, j):
        result = max(result, dfs(i, k) + dfs(k, j) + nums[i] * nums[k] * nums[j])
      return result

    return dfs(0, len(nums) - 1)
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
