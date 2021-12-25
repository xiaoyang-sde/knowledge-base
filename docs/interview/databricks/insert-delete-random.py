from random import choice
from collections import defaultdict

# Insert Delete GetRandom O(1)
class RandomizedSet:
  def __init__(self):
    """
    Initialize your data structure here.
    """
    self.map = {}
    self.list = []

  def insert(self, val: int) -> bool:
    """
    Inserts a value to the set. Returns true if the set did not already contain the specified element.
    """
    if val in self.map:
      return False
    self.map[val] = len(self.list)
    self.list.append(val)
    return True

  def remove(self, val: int) -> bool:
    """
    Removes a value from the set. Returns true if the set contained the specified element.
    """
    if val not in self.map:
      return False
    self.map[self.list[-1]] = self.map[val]
    self.list[self.map[val]] = self.list[-1]
    self.list.pop()
    del self.map[val]
    return True

  def getRandom(self) -> int:
    """
    Get a random element from the set.
    """
    return choice(self.list)

# Duplicates allowed
class RandomizedCollection:
  def __init__(self):
    """
    Initialize your data structure here.
    """
    self.map = defaultdict(set)
    self.list = []

  def insert(self, val: int) -> bool:
    """
    Inserts a value to the collection. Returns true if the collection did not already contain the specified element.
    """
    self.map[val].add(len(self.list))
    self.list.append(val)
    return len(self.map[val]) == 1

  def remove(self, val: int) -> bool:
    """
    Removes a value from the collection. Returns true if the collection contained the specified element.
    """
    if len(self.map[val]) == 0:
      return False

    i = self.map[val].pop()
    if len(self.map[val]) == 0:
      del self.map[val]

    prev_i = len(self.list) - 1
    self.list[i] = self.list[prev_i]
    self.list.pop()
    if prev_i != i:
      self.map[self.list[i]].remove(prev_i)
      self.map[self.list[i]].add(i)
    return True

  def getRandom(self) -> int:
    """
    Get a random element from the collection.
    """
    return choice(self.list)
