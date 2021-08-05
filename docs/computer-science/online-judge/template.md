# Template

## Quick Sort

```py
def partition(nums, lo, hi):
  pivot = nums[hi]
  less = lo
  for i in range(lo, hi):
    if nums[i] > pivot:
      continue
    nums[less], nums[i] = nums[i], nums[less]
    less += 1
  nums[less], nums[hi] = nums[hi], nums[less]
  return less

def quick_sort(nums, lo, hi + 1):
  if lo >= hi:
    return

  pivot = partition(nums, lo, hi)
  quick_sort(nums, lo, pivot - 1)
  quick_sort(nums, pivot + 1, hi)

# Example
nums = [1, 8, 4, 6, 2, 5, 7, 3]
quick_sort(nums, 0, 7)
```

## Quick Select

```py
def partition(nums, lo, hi):
  pivot = nums[hi]
  less = lo
  for i in range(lo, hi + 1):
    if nums[i] > pivot:
      continue
    nums[less], nums[i] = nums[i], nums[less]
    less += 1
  nums[less], nums[hi] = nums[hi], nums[less]
  return less

def quick_select(nums, lo, hi, k):
  pivot = partition(nums, lo, hi)
  if pivot == k:
    return nums[pivot]
  elif pivot > k:
    quick_select(nums, lo, pivot - 1, k)
  else:
    quick_select(nums, pivot + 1, hi, k)

# Example
nums = [1, 8, 4, 6, 2, 5, 7, 3]
quick_select(nums, 0, 7, 4)
```
