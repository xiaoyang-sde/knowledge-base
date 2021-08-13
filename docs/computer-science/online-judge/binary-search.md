# Binary Search

## Search Insert Position

[LeetCode 35](https://leetcode.com/problems/search-insert-position/)

```py
class Solution:
  def searchInsert(self, nums: List[int], target: int) -> int:
    lo = 0
    hi = len(nums) - 1
    while lo <= hi:
      mid = (hi - lo) // 2 + lo
      if nums[mid] < target:
        lo = mid + 1
      else:
        hi = mid - 1
    return lo
```
