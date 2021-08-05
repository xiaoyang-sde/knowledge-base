# Linked List

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
