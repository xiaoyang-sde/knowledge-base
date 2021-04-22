# Linked List

## 2. Add Two Numbers

```py
class Solution:
  def addTwoNumbers(self, l1: ListNode, l2: ListNode) -> ListNode:
    dummy = cur = ListNode()
    carry = 0
    while l1 or l2 or carry:
      if l1:
        carry += l1.val
        l1 = l1.next
      if l2:
        carry += l2.val
        l2 = l2.next
      cur.next = ListNode(carry % 10)
      cur = cur.next
      carry //= 10
    return dummy.next
```

## 61. Rotate List

```py
class Solution:
  def rotateRight(self, head: ListNode, k: int) -> ListNode:
    if not head:
      return

    tail = head
    cur = head
    length = 0
    while cur:
      tail = cur
      cur = cur.next
      length += 1

    rotate = k % length
    if rotate == 0:
      return head

    cur = head
    for _ in range(length - rotate - 1):
      cur = cur.next

    tail.next = head
    head = cur.next
    cur.next = None
    return head
```

## 82 Remove Duplicates from Sorted List II

```py
class Solution:
  def deleteDuplicates(self, head: ListNode) -> ListNode:
    if not head:
      return
    dummy = ListNode()
    dummy.next = head

    prev = dummy
    cur = head

    while cur and cur.next:
      if cur.next.val == cur.val:
        while cur.next and cur.next.val == cur.val:
          cur.next = cur.next.next
        prev.next = cur.next
      else:
        prev = cur

      cur = cur.next

    return dummy.next
```

## 83 Remove Duplicates from Sorted List

```py
class Solution:
  def deleteDuplicates(self, head: ListNode) -> ListNode:
    cur = head
    while cur and cur.next:
      if cur.next.val == cur.val:
        cur.next = cur.next.next
      else:
        cur = cur.next
    return head
```

## 86. Partition List

```py
class Solution:
  def partition(self, head: ListNode, x: int) -> ListNode:
    less_head = less = ListNode()
    greater_head = greater = ListNode()

    while head:
      if head.val < x:
        less.next = head
        less = less.next
      else:
        greater.next = head
        greater = greater.next

      head = head.next

    greater.next = None
    less.next = greater_head.next
    return less_head.next
```

## 92. Reverse Linked List II

```py
class Solution:
  def reverseBetween(self, head: ListNode, m: int, n: int) -> ListNode:
    if not head:
      return

    dummy = ListNode()
    dummy.next = head

    prev = dummy
    for _ in range(m - 1):
      prev = prev.next

    reverse = None
    cur = prev.next
    for _ in range(n - m + 1):
      next = cur.next
      cur.next = reverse
      reverse = cur
      cur = next

    prev.next.next = cur
    prev.next = reverse
    return dummy.next
```

## 141. Linked List Cycle

```py
class Solution:
  def hasCycle(self, head: ListNode) -> bool:
    slow = fast = head
    while fast and fast.next:
      fast = fast.next.next
      slow = slow.next
      if slow == fast:
        return True
    return False
```

## 142. Linked List Cycle II

```py
class Solution:
  def detectCycle(self, head: ListNode) -> ListNode:
    slow = fast = head
    while fast and fast.next:
      fast = fast.next.next
      slow = slow.next
      if slow == fast:
        break
    else:
      return

    slow = head
    while slow != fast:
      slow = slow.next
      fast = fast.next
    return slow
```

## 143. Reorder List

```py
class Solution:
  def reorderList(self, head: ListNode) -> None:
    """
    Do not return anything, modify head in-place instead.
    """
    slow = fast = head
    while fast and fast.next:
      fast = fast.next.next
      slow = slow.next

    prev = None
    cur = slow.next
    while cur:
      next = cur.next
      cur.next = prev
      prev = cur
      cur = next
    slow.next = None

    l1 = head
    l2 = prev
    while l2:
      n1 = l1
      n2 = l2
      l1 = l1.next
      l2 = l2.next

      n2.next = n1.next
      n1.next = n2
```

## 160. Intersection of Two Linked Lists

```py
class Solution:
  def getIntersectionNode(self, headA: ListNode, headB: ListNode) -> ListNode:
    n1 = headA
    n2 = headB
    while n1 != n2:
      if n1:
        n1 = n1.next
      else:
        n1 = headB

      if n2:
        n2 = n2.next
      else:
        n2 = headA
    return n1
```

## 206. Reverse Linked List

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

## 328. Odd Even Linked List

```py
class Solution:
  def oddEvenList(self, head: ListNode) -> ListNode:
    odd_head = odd = ListNode()
    even_head = even = ListNode()

    while head:
      odd.next = head
      even.next = head.next
      odd = odd.next
      even = even.next
      head = head.next.next if head.next else None

    odd.next = even_head.next
    return odd_head.next
```

## 445. Add Two Numbers II

```py
class Solution:
  def addTwoNumbers(self, l1: ListNode, l2: ListNode) -> ListNode:
    len1, len2 = self.length(l1), self.length(l2)
    l1 = self.padding(l1, len2 - len1)
    l2 = self.padding(l2, len1 - len2)
    carry = self.combine(l1, l2)
    if carry == 1:
      n = ListNode(1)
      n.next = l1
      l1 = n
    return l1

  def padding(self, l, n):
    for _ in range(n):
      x = ListNode(0)
      x.next = l
      l = x
    return l

  def length(self, l):
    result = 0
    while l:
      l = l.next
      result += 1
    return result

  def combine(self, l1, l2):
    if not l1 and not l2:
      return 0
    carry = self.combine(l1.next, l2.next)
    s = l1.val + l2.val + carry
    l1.val = s % 10
    return s // 10
```
