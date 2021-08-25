# Trie (Prefix Tree)

## Implement Trie (Prefix Tree)

> A trie (pronounced as "try") or prefix tree is a tree data structure used to efficiently store and retrieve keys in a dataset of strings. There are various applications of this data structure, such as autocomplete and spellchecker.

[LeetCode 208](https://leetcode.com/problems/implement-trie-prefix-tree/solution/shi-xian-trie-qian-zhui-shu-by-leetcode-ti500/)

```py
class Node:
  def __init__(self):
    self.child = [None] * 26
    self.end = False

class Trie:
  def __init__(self):
    self.root = Node()

  def insert(self, word: str) -> None:
    node = self.root
    for c in word:
      index = ord(c) - ord('a')
      if not node.child[index]:
        node.child[index] = Node()
      node = node.child[index]
    node.end = True

  def _search(self, word: str):
    node = self.root
    for c in word:
      index = ord(c) - ord('a')
      if not node.child[index]:
        return
      node = node.child[index]
    return node

  def search(self, word: str) -> bool:
    node = self._search(word)
    return node is not None and node.end

  def startsWith(self, prefix: str) -> bool:
    return self._search(prefix) is not None
```

## Word Search II

[LeetCode 202](https://leetcode.com/problems/word-search-ii/)

The optimization reduced the runtime from 5000ms to 200ms:

```py
if not node.child:
  del parent.child[c]
```

```py
class Node:
  def __init__(self):
    self.child = {}
    self.val = ''

class Trie:
  def __init__(self):
    self.root = Node()

  def insert(self, word: str) -> None:
    node = self.root
    for c in word:
      if c not in node.child:
        node.child[c] = Node()
      node = node.child[c]
    node.val = word

class Solution:
  def findWords(self, board: List[List[str]], words: List[str]) -> List[str]:
    def dfs(node, i, j):
      c = board[i][j]
      if c not in node.child:
        return
      parent, node = node, node.child[c]

      if node.val:
        result.append(node.val)
        node.val = None

      board[i][j] = '#'
      for di, dj in ((1, 0), (-1, 0), (0, 1), (0, -1)):
        ni, nj = i + di, j + dj
        if ni < 0 or nj < 0 or ni >= len(board) or nj >= len(board[0]):
          continue
        dfs(node, ni, nj)

      board[i][j] = c
      if not node.child:
        del parent.child[c]

    trie = Trie()
    for word in words:
      trie.insert(word)

    result = []
    for i in range(len(board)):
      for j in range(len(board[0])):
        if board[i][j] not in trie.root.child:
          continue
        dfs(trie.root, i, j)
    return result
```
