# Array

## Maximum Subarray

- Let `dp[i]` be the array of maximum subarray ends with index `i`.
- Let `dp[i] = (dp[i - 1] + nums[i], nums[i])`.
The `dp` array could be reduced to a single variable `result` that tracks the current maximum entry.

```py
class Solution:
  def maxSubArray(self, nums: List[int]) -> int:
    result = nums[0]
    window = nums[0]
    for num in nums[1:]:
      window = max(num, window + num)
      result = max(result, window)
    return result
```

## Maximum Product Subarray

- Let `max[i]` be the array of maximum product subarray ends with index `i`.
- Let `min[i]` be the array of minimum product subarray ends with index `i`.
- Let `max[i] = max(nums[i], min[i - 1] * nums[i], max[i - 1] * nums[i])`
- Let `min[i] = min(nums[i], min[i - 1] * nums[i], max[i - 1] * nums[i])`

```py
class Solution:
  def maxProduct(self, nums: List[int]) -> int:
    window_max = nums[0]
    window_min = nums[0]
    result = nums[0]
    for num in nums[1:]:
      window_max, window_min = max(num, window_max * num, window_min * num), min(num, window_max * num, window_min * num)
      result = max(result, window_max)
    return result
```

## Product of Array Except Self

```py
class Solution:
  def productExceptSelf(self, nums: List[int]) -> List[int]:
    result = []
    product = 1
    for i in range(len(nums)):
      result.append(product)
      product *= nums[i]

    product = 1
    for i in range(len(nums) - 1, -1, -1):
      result[i] *= product
      product *= nums[i]

    return result
```
