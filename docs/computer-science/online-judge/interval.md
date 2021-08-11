# Interval

## Merge Intervals

[LeetCode 56](https://leetcode.com/problems/merge-intervals/)

```py
class Solution:
  def merge(self, intervals: List[List[int]]) -> List[List[int]]:
    intervals.sort()
    result = []
    for start, end in intervals:
      if not result or start > result[-1][-1]:
        result.append([start, end])
      else:
        result[-1][-1] = max(result[-1][-1], end)
    return result
```

## Insert Interval

[LeetCode 57](https://leetcode.com/problems/insert-interval/)

```py
class Solution:
  def insert(self, intervals: List[List[int]], newInterval: List[int]) -> List[List[int]]:
    result = []
    index  = 0

    while index < len(intervals) and intervals[index][-1] < newInterval[0]:
      result.append(intervals[index])
      index += 1

    while index < len(intervals) and intervals[index][0] <= newInterval[-1]:
      newInterval[0] = min(newInterval[0], intervals[index][0])
      newInterval[1] = max(newInterval[1], intervals[index][1])
      index += 1
    result.append(newInterval)

    while index < len(intervals):
      result.append(intervals[index])
      index += 1

    return result
```
