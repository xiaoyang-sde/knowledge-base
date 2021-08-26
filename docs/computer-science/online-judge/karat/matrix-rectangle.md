# Matrix and Rectangle

## Question 1

Find the top left and bottom right coordinates of a rectangle of 0's within a matrix of 1's.

```py
def find_rectangle(grid):
  def locate(i, j):
    init_i, init_j = i, j
    while i < len(grid) and grid[i][init_j] == 0:
      i += 1
    while j < len(grid) and grid[init_i][j] == 0:
      j += 1
    return [[init_i, init_j], [i - 1, j - 1]]

  for i in range(len(grid)):
    for j in range(len(grid[0])):
      if grid[i][j] == 1:
        continue
      return locate(i, j)
```

## Question 2

Expand the previous solution so it works for any number of rectangles.

```py
def find_rectangle_extend(grid):
  def locate(i, j):
    init_i, init_j = i, j
    while i < len(grid) and grid[i][init_j] == 0:
      i += 1
    while j < len(grid) and grid[init_i][j] == 0:
      j += 1
    for x in range(init_i, i):
      for y in range(init_j, j):
        grid[x][y] = 1
    return [[init_i, init_j], [i - 1, j - 1]]

  result = []
  for i in range(len(grid)):
    for j in range(len(grid[0])):
      if grid[i][j] == 1:
        continue
      result.append(locate(i, j))
  return result
```

## Question 3

Find the connected components in the grid. Return a list of components which contains all coordinates of each component.

```py
from collections import deque

def connected_components(grid):
  visited = set()
  def bfs(i, j):
    coordinates = [(i, j)]
    queue = deque([(i, j)])
    visited.add((i, j))
    while queue:
      i, j = queue.popleft()
      for di, dj in ((0, 1), (1, 0), (0, -1), (-1, 0)):
        ni = i + di
        nj = j + dj
        if ni < 0 or ni >= len(grid) or nj < 0 or nj > len(grid[0]):
          continue
        if (ni, nj) in visited or grid[ni][nj] == 1:
          continue
        visited.add((ni, nj))
        coordinates.append((ni, nj))
        queue.append((ni, nj))
    return coordinates

  result = []
  for i in range(len(grid)):
    for j in range(len(grid[0])):
      if grid[i][j] == 1 or (i, j) in visited:
        continue
      result.append(bfs(i, j))
  return result
```
