# Backtracking

## Combination Sum II

[LeetCode 40](https://leetcode.com/problems/combination-sum-ii/solution/)

```py
class Solution:
  def combinationSum2(self, candidates, target: int):
    def backtrack(index, state, target):
      if target == 0:
        result.append(state[:])
        return
      if target < 0:
        return
      for i in range(index, len(candidates)):
        if i > index and candidates[i] == candidates[i - 1]:
          continue
        state.append(candidates[i])
        backtrack(i + 1, state, target - candidates[i])
        state.pop()

    candidates.sort()
    result = []
    backtrack(0, [], target)
    return result
```

## Subsets II

- Time complexity $O(n \cdot 2^n)$
- Space complexity: $O(n)$

```py
class Solution:
  def subsetsWithDup(self, nums: List[int]) -> List[List[int]]:
    def backtrack(subset, index):
      result.append(subset[:])
      for i in range(index, len(nums)):
        if i > index and nums[i] == nums[i - 1]:
          continue

        subset.append(nums[i])
        backtrack(subset, i + 1)
        subset.pop()

    nums.sort()
    result = []
    backtrack([], 0)
    return result
```

## Permutations II

[LeetCode 47](https://leetcode.com/problems/permutations-ii/)

If the `index - 1` is not visited, and `nums[index] == nums[index - 1]`, `nums[index]` will be ignored to prevent duplicated results.

- Time complexity $O(n \cdot n!)$
- Space complexity: $O(n \cdot n!)$

```py
class Solution:
  def permuteUnique(self, nums):
    def backtrack(state, visited):
      if len(state) == len(nums):
        result.append(state[:])

      for index, num in enumerate(nums):
        if index in visited:
          continue
        if index > 0 and nums[index] == nums[index - 1] and index - 1 not in visited:
          continue
        state.append(num)
        visited.add(index)
        backtrack(state, visited)
        visited.remove(index)
        state.pop()
    nums.sort()
    result = []
    backtrack([], set())
    return result
```
