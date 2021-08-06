# Linked List

## Reverse Linked List

```py
class Solution:
  def reverseList(self, head: ListNode) -> ListNode:
    prev = None
    cur = head
    while cur:
      next = cur.next
      cur.next = prev
      prev = cur
      cur = next
    return prev
```

## Remove Nth Node From End of List

```py
class Solution:
  def removeNthFromEnd(self, head: ListNode, n: int) -> ListNode:
    dummy = ListNode()
    dummy.next = head

    slow = fast = dummy
    for _ in range(n):
      fast = fast.next

    while fast.next:
      slow = slow.next
      fast = fast.next

    slow.next = slow.next.next
    return dummy.next
```

## Reorder List

```py
class Solution:
  def reorderList(self, head: ListNode) -> None:
    slow = fast = head
    while fast and fast.next:
      fast = fast.next.next
      slow = slow.next

    list_1 = head
    list_2 = slow.next
    if not slow.next:
      return
    slow.next = None

    prev = None
    node = list_2
    while node:
      next_node = node.next
      node.next = prev
      prev = node
      node = next_node
    list_2 = prev

    while list_1 and list_2:
      next_1 = list_1.next
      next_2 = list_2.next

      list_1.next = list_2
      list_2.next = next_1

      list_1 = next_1
      list_2 = next_2
```

## Flatten a Multilevel Doubly Linked List

[LeetCode 430](https://leetcode.com/problems/flatten-a-multilevel-doubly-linked-list/)

```py
class Solution:
  def flatten(self, head: 'Node') -> 'Node':
    node = head
    while node:
      if not node.child:
        node = node.next
        continue

      next = node.next
      child = self.flatten(node.child)
      node.child = None
      node.next = child
      child.prev = node

      if not next:
        break
      while child.next:
        child = child.next
      child.next = next
      next.prev = child
      node = next

    return head
```

## LRU Cache

[LeetCode 146](https://leetcode.com/problems/lru-cache/)

```py
class ListNode:
  def __init__(self, key=0, val=0, prev=None, next=None):
    self.key = key
    self.val = val
    self.prev = prev
    self.next = next

class LRUCache:
  def __init__(self, capacity: int):
    self.capacity = capacity
    self.map = {}
    self.start = ListNode()
    self.end = ListNode()
    self.start.next = self.end
    self.end.prev = self.start

  def get(self, key: int) -> int:
    if key not in self.map:
      return -1

    node = self.map[key]
    self._remove(node)
    self._insert(node)
    return node.val

  def put(self, key: int, value: int) -> None:
    if key in self.map:
      self._remove(self.map[key])

    node = ListNode(key, value)
    self.map[key] = node
    self._insert(node)

    if len(self.map) > self.capacity:
      remove_node = self.end.prev
      self._remove(remove_node)
      del self.map[remove_node.key]

  def _remove(self, node):
    node.prev.next = node.next
    node.next.prev = node.prev

  def _insert(self, node):
    node.prev = self.start
    node.next = self.start.next
    self.start.next.prev = node
    self.start.next = node
```
