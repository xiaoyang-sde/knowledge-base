# Two Pointers

## Boats to Save People

If the heaviest person can share a boat with the lightest person, then do so. Otherwise, the heaviest person can't pair with anyone, so they get their own boat.

- Time Complexity: $O(n \log n)$
- Space Complexity: $O(n)$

```py
class Solution:
  def numRescueBoats(self, people, limit: int) -> int:
    people.sort()
    result = 0
    start = 0
    end = len(people) - 1
    
    while start <= end:
      if people[start] + people[end] <= limit:
        start += 1
      result += 1
      end -= 1
    return result
```

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
