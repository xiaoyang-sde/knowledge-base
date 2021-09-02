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

## Merge k Sorted Lists

[LeetCode 23](https://leetcode.com/problems/merge-k-sorted-lists/)

```py
class Solution:
  def mergeKLists(self, lists: List[ListNode]) -> ListNode:
    if not lists:
      return
    dummy = head = ListNode()
    heap = []
    for index, node in enumerate(lists):
      if not node:
        continue
      heappush(heap, (node.val, index))

    while heap:
      val, index = heappop(heap)
      node = lists[index]
      head.next = node
      head = head.next

      if not node.next:
        continue
      lists[index] = node.next
      heappush(heap, (node.next.val, index))

    return dummy.next
```

## Find Median from Data Stream

[LeetCode 295](https://leetcode.com/problems/find-median-from-data-stream/)

```py
class MedianFinder:
  def __init__(self):
    self.left_heap = []
    self.right_heap = []

  def addNum(self, num: int) -> None:
    max_num = -heappushpop(self.left_heap, -num)
    heappush(self.right_heap, max_num)
    if len(self.right_heap) > len(self.left_heap):
      heappush(self.left_heap, -heappop(self.right_heap))

  def findMedian(self) -> float:
    if len(self.right_heap) == len(self.left_heap):
      return (self.right_heap[0] - self.left_heap[0]) / 2
    return -self.left_heap[0]
```
