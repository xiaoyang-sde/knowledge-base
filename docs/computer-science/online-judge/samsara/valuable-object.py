from heapq import heappush, heappop

class Valuable:
  def __init__(self, id, weight, size):
    self._id = id
    self._weight = weight
    self._size = size

  def get_id(self):
    return self._id

  def get_weight(self):
    return self._weight

  def get_size(self):
    return self._size

class Box:
  def __init__(self, size_bar):
    self._valuable_map = {}
    self._current_size = 0
    self._size_bar = size_bar
    self._priority_queue = []

  def add(self, valuable):
    valuable_id = valuable.get_id()
    if valuable_id in self._valuable_map:
      raise Exception()

    valuable_size = valuable.get_size()
    if self._current_size + valuable_size > self._size_bar:
      raise Exception()

    self._valuable_map[valuable_id] = valuable
    self._current_size += valuable_size
    heappush(self._priority_queue, (-valuable_size, valuable_id))

  def remove(self, valuable_id):
    if valuable_id not in self._valuable_map:
      raise Exception()

    valuable_size = self._valuable_map[valuable_id].get_size()
    self._current_size -= valuable_size
    del self._valuable_map[valuable_id]

  def max_valuable_size(self):
    while self._priority_queue:
      max_valuable_value = -self._priority_queue[0][0]
      max_valuable_id = self._priority_queue[0][1]
      
      if max_valuable_id in self._valuable_map:
        return max_valuable_value
      else:
        heappop(self._priority_queue)

    if not self._priority_queue:
      raise Exception()
