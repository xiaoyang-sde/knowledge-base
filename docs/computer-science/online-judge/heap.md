# Heap

## Kth Largest Element in an Array

### Heap

```py
class Solution:
  def findKthLargest(self, nums: List[int], k: int) -> int:
    heap = []
    for num in nums:
      if len(heap) < k:
        heappush(heap, num)
      else:
        heappushpop(heap, num)
    return heappop(heap)
```

### Quick Select

```py
class Solution:
  def findKthLargest(self, nums: List[int], k: int) -> int:
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

    def quick_select(nums, lo, hi, k):

      index = partition(nums, lo, hi)
      if index == k:
        return nums[index]
      elif index < k:
        return quick_select(nums, index + 1, hi, k)
      else:
        return quick_select(nums, lo, index - 1, k)

    return quick_select(nums, 0, len(nums) - 1, len(nums) - k)
```

## Top K Frequent Elements

```py
class Solution:
  def topKFrequent(self, nums: List[int], k: int) -> List[int]:
    heap = []
    for num, freq in Counter(nums).items():
      if len(heap) < k:
        heappush(heap, (freq, num))
      else:
        heappushpop(heap, (freq, num))
    return [num for freq, num in heap]
```

## Top K Frequent Words

```py
class Solution:
  def topKFrequent(self, words: List[int], k: int) -> List[int]:
    freq = Counter(words)
    heap = []
    for word, count in freq.items():
      heappush(heap, (-count, word))

    return list(heappop(heap)[1] for _ in range(k))
```
