# Template

## Dijkstra's algorithm

- Let `k` be the starting node
- Let `graph` be a graph implemented with dictionary (HashMap)

```py
weights = defaultdict(lambda: float('inf'))
weights[k] = 0

visited = set()
queue = []
heappush(queue, (0, k))

while queue:
  prev_weight, node = heappop(queue)
  visited.add(node)
  for child, weight in graph[node]:
    if child in visited:
      continue
    total_weight = weight + prev_weight
    if total_weight > weights[child]:
      continue
    weights[child] = total_weight
    heappush(queue, (total_weight, child))
```

## Union Find

```py
parent = defaultdict(lambda: None)
weight = defaultdict(lambda: 1)

def find(x):
  if parent[x] == None:
    return x
  parent[x] = find(parent[x])
  return parent[x]

def union(x, y):
  rx = find(x)
  ry = find(y)
  if rx == ry:
    return
  if weight[rx] < weight[ry]:
    parent[rx] = ry
    weight[ry] += weight[rx]
  else:
    parent[ry] = rx
    weight[rx] += weight[ry]
```

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
