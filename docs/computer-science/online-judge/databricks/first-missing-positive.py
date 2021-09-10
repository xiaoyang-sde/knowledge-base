class Solution:
  def firstMissingPositive(self, nums) -> int:
    index = 0
    while index < len(nums):
      target = nums[index] - 1
      if nums[index] > 0 and nums[index] < len(nums) and nums[target] != nums[index]:
        nums[index], nums[target] = nums[target], nums[index]
      else:
        index += 1

    for i in range(len(nums)):
      if nums[i] != i + 1:
        return i + 1
    return len(nums) + 1
