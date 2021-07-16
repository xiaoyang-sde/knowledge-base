# Dynamic Programming

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
