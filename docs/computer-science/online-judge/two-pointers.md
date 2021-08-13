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
