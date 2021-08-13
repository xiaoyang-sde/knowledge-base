# Sliding Window

## Longest Substring Without Repeating Characters

[LeetCode 3](https://leetcode.com/problems/longest-substring-without-repeating-characters/)

Let `chars` be the set of characters in the current window. If `s[end]` in the window, move `start` forward and update `chars` until `s[end]` is not in the window.

- Time Complexity: $O(n)$
- Space complexity: $O(n)$

```py
class Solution:
  def lengthOfLongestSubstring(self, s: str) -> int:
    chars = set()
    start = 0
    length = 0
    for end in range(len(s)):
      while s[end] in chars:
        chars.remove(s[start])
        start += 1
      chars.add(s[end])
      length = max(length, end - start + 1)
    return length
```

## Longest Repeating Character Replacement

[LeetCode 424](https://leetcode.com/problems/longest-repeating-character-replacement/)

Let `window` be the count of each character in the window. Let `max_count` be the maximum count of a character in the window.

When `end - start + 1 == max_count`, then the window is filled with only one character. When `end - start + 1 > max_count`, then `end - start + 1 - max_count` is the count of non-maximum character in the window. `max_count` might be invalid at some point, but it was valid earlier in the string, and the target is to find the longest window in the string.

- Time Complexity: $O(n)$
- Space complexity: $O(n)$

```py
class Solution:
  def characterReplacement(self, s: str, k: int) -> int:
    window = Counter()
    max_count = 0
    start = 0
    for end in range(len(s)):
      window[s[end]] += 1
      max_count = max(max_count, window[s[end]])
      if max_count + k < end - start + 1:
        window[s[start]] -= 1
        max_count = max(window.values())
        start += 1
    return max_count
```

## Max Consecutive Ones III

[LeetCode 1004](https://leetcode.com/problems/max-consecutive-ones-iii/)

Let `count` be the count of `0` in the window. When `count > k`, shrink the left boundary of the window, and then record the current winndow length.

- Time Complexity: $O(n)$
- Space complexity: $O(1)$

```py
class Solution:
  def longestOnes(self, nums: List[int], k: int) -> int:
    count = 0
    start = 0
    result = 0
    for end in range(len(nums)):
      if nums[end] == 0:
        count += 1

      while count > k:
        if nums[start] == 0:
          count -= 1
        start += 1

      result = max(result, end - start + 1)

    return result
```

## Grumpy Bookstore Owner

[LeetCode 1052](https://leetcode.com/problems/grumpy-bookstore-owner/)

The first pass calculates the sum of all customers when the owner is nnot grumpy, and set  the customer count at these minutes to `0`. The second pass finds the maximum subarray with length `minutes`.

- Time Complexity: $O(n)$
- Space complexity: $O(1)$

```py
class Solution:
  def maxSatisfied(self, customers: List[int], grumpy: List[int], minutes: int) -> int:
    result = 0
    for index, customer in enumerate(customers):
      if grumpy[index] == 1:
        continue
      result += customer
      customers[index] = 0

    max_window = 0
    start = 0
    window = 0
    for end in range(len(customers)):
      if end >= minutes:
        window -= customers[start]
        start += 1
      window += customers[end]
      max_window = max(max_window, window)
    return result + max_window
```

## Maximum Points You Can Obtain from Cards

[LeetCode 1423](https://leetcode.com/problems/maximum-points-you-can-obtain-from-cards/)

- Time Complexity: $O(n)$
- Space complexity: $O(1)$

```py
class Solution:
  def maxScore(self, card_points: List[int], k: int) -> int:
    total_points = sum(card_points)
    start = 0
    window = 0
    min_window = float('inf')

    for end in range(len(card_points)):
      if end >= len(card_points) - k:
        min_window = min(min_window, window)
        window -= card_points[start]
        start += 1
      window += card_points[end]

    min_window = min(min_window, window)
    return total_points - min_window
```
