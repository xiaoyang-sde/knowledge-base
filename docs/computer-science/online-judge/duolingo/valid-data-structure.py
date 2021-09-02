from collections import deque
from heapq import heappush, heappop

class Stack:
  def __init__(self):
    self.valid = True
    self.stack = []

  def push(self, val):
    self.stack.append(val)

  def pop(self, val):
    if not self.stack:
      return
    real_val = self.stack.pop()
    if real_val != val:
      self.valid = False

class Queue:
  def __init__(self):
    self.valid = True
    self.queue = deque()
    self.visited = set()
    self.clear = False

  def push(self, val):
    self.queue.append(val)
    self.visited.add(val)

  def pop(self, val):
    if not self.queue:
      return
    if val in self.visited:
      self.clear = True
    if self.clear:
      real_val = self.queue.popleft()
      if real_val != val:
        self.valid = False

class PriorityQueue:
  def __init__(self):
    self.valid = True
    self.heap = []
    self.visited = set()

  def push(self, val):
    self.visited.add(val)
    heappush(self.heap, val)

  def pop(self, val):
    if val not in self.visited:
      if val >= self.heap[0]:
        self.valid = False
      return

    self.visited.remove(val)
    real_val = heappop(self.heap)
    if real_val != val:
      self.valid = False

def get_valid_data_structure(operations):
  data_structures = {
    'stack': Stack(),
    'queue': Queue(),
    'priority_queue': PriorityQueue(),
  }
  for operation, val in operations:
    for name, data_structure in data_structures.items():
      if not data_structure:
        continue
      if operation == 'push':
        data_structure.push(val)
      elif operation == 'pop':
        data_structure.pop(val)

      if data_structure.valid == False:
        data_structures[name] = None

  return [key for key in data_structures.keys() if data_structures[key]]

operations = [('push', 5), ('push', 10), ('pop', 1), ('pop', 2), ('pop', 5), ('pop', 10)]
print(get_valid_data_structure(operations))
