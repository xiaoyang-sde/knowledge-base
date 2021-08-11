# Two Pointers

## Valid Triangle Number

### Binary Search

```py
class Solution:
  def triangleNumber(self, nums: List[int]) -> int:
    nums.sort()
    result = 0
    for i in range(len(nums) - 2):
      for j in range(i + 1, len(nums) - 1):
        lo = j + 1
        hi = len(nums) - 1
        while lo <= hi:
          mid = (hi - lo) // 2 + lo
          if nums[mid] < nums[i] + nums[j]:
            lo = mid + 1
          else:
            hi = mid - 1
        result += lo - j - 1
    return result
```

### Two Pointers

```py
class Solution:
  def triangleNumber(self, nums: List[int]) -> int:
    nums.sort()
    result = 0
    for i in range(len(nums) - 2):
      k = i + 2
      for j in range(i + 1, len(nums) - 1):
        while k < len(nums) and nums[i] + nums[j] > nums[k]:
          k += 1
        result += max(0, k - j - 1)
    return result
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
