# Sparse Vector

## Question 1

Design a `SparseVector` class with the `set` method. Throw an error if the index is larger than the size.

```py
from collections import defaultdict

class SparseVector:
  def __init__(self, size):
    self.size = size
    self.map = defaultdict(int)

  def set(self, index, value):
    if index >= self.size:
      raise ValueError('Out of range')
    self.map[index] = value
```

## Question 2

Add these operations to your library: addition, dot product, and cosine. For each operation, your code should throw an error if the two input vectors are not equal length.

```py
from collections import defaultdict
from math import sqrt

class SparseVector:
  def __init__(self, size):
    self.size = size
    self.map = defaultdict(int)

  def set(self, index, value):
    if index >= self.size:
      raise ValueError('Out of range')
    self.map[index] = value

  def dotProduct(self, vec):
    if vec.size != self.size:
      raise ValueError()

    result = 0
    for index, num in self.map.items():
      result += num * vec.map[index]
    return result

  def addition(self, vec):
    if vec.size != self.size:
      raise ValueError()

    result = [0] * self.size
    for index, num in self.map.items():
      result[index] += num
    for index, num in vec.map.items():
      result[index] += num
    return result

  def cosine(self, vec):
    def norm(vec):
      result = 0
      for num in vec.values():
        result += num * num
      return sqrt(result)

    return self.dotProduct(vec) / (norm(vec.map) * norm(self.map))
```
