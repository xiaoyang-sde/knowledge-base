# Monotonic Stack

## Next Greater Element II

[LeetCode 503](https://leetcode.com/problems/next-greater-element-ii/)

The `stack` is a monotonic stack that keeps decreasing. While the current element is larger than the top of the stack, pop the top element since the current element is the next greater element of the top of the stack.

- Time Complexity: $O(n)$
- Space Complexity: $O(n)$

```py
class Solution:
  def nextGreaterElements(self, nums):
    stack = []
    result = [-1] * len(nums)
    for index, num in enumerate(nums):
      while stack and stack[-1][0] < num:
        prev, prev_index = stack.pop()
        result[prev_index] = num
      stack.append((num, index))

    for index, num in enumerate(nums):
      if not stack:
        break
      while stack[-1][0] < num:
        prev, prev_index = stack.pop()
        result[prev_index] = num

    return result
```
