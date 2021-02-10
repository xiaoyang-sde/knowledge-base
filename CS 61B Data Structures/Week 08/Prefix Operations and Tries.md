# Prefix Operations and Tries

## Introduction

Tries is an ordered tree data structure used to store keys which can be broken into "characters" and share prefixes with other keys.

* Every node stores only one letter.
* Nodes can be shared by multiple keys.
* The last character of the key is marked.

To check if the trie contains a key, walk down the tree from the root along the correct nodes. If the character does not exist or the final node is not marked, the key does not exist.

For example, the following trie contains `["sam", "sad", "sap", "same", "a", "awls"]`.

![Tries](https://joshhug.gitbooks.io/hug61b/content/assets/Screen%20Shot%202019-03-14%20at%2012.47.38%20AM.png "Tries")

* `contains("a)`: true, the final node is marked
* `contains("sa")`: false, the final node is not marked
* `contains("saq")`: false, fell off tree

## Implementation

### DataIndexedCharMap

The `DataIndexedCharMap` class is efficient implementation for a map which takes in character keys. The value `R` represents the number of possible characters (128 for ASCII).

```java
public class DataIndexedCharMap<V> {
    private V[] items;
    public DataIndexedCharMap(int R) {
        items = (V[]) new Object[R];
    }
    public void put(char c, V val) {
        items[c] = val;
    }
    public V get(char c) {
        return items[c];
    }
}
```

When building a trie, the `DataIndexedCharMap` class provides a map to all of a nodes' children. However, the `DataIndexedCharMap` object of a node with relatively few children will have mostly null links, which is wasting excess memories.

Since each link corresponds to a character if and only if that character exists, the character variable of each node can be removed. The value of the character is its position in the parent.

```java
public class TrieSet {
   private static final int R = 128;
   private Node root;

   private static class Node {
      // private char ch;
      private boolean isKey;
      private DataIndexedCharMap next;

      //private Node(char c, boolean blue, int R) {
      //  ch = c; 
      private Node(boolean blue, int R) {
          isKey = blue;
          next = new DataIndexedCharMap<Node>(R);
      }
   }
}
```

### Child Tracking

The problem of the implementation above is that it wastes excess memories with lots of null spots that don't contain any children. However, alternative ways to track children is available.
* Hash-Table based Trie: Initialize the default value and resize the array only when necessary with the load factor.
* BST based Trie: Create children pointers when necessary and store them in a BST. Runtime is not efficient.


DataIndexedCharMap
* Space: 128 links per node
* Runtime: `Θ(1)`

BST
* Space: C links per node
* Runtime: `O(log R)`

Hash Table
* Space: C links per node
* Runtime: `O(R)`

(C is the number of children. R is the size of the alphabet.)

## Performance

If a Trie has N keys, the runtime for our Map/Set operations are as follows:

* add: `Θ(1)`
* contains: `Θ(1)`

The runtime is independent from the number of the keys, since the operations only traverse the length of one key in the worst case.

## Trie String Operations

### Prefix Matching

Tries could efficiently support specific string operations like prefix matching.

The following pseudocode of `collect` operation could return all keys in a trie.

```
collect():
    Create an empty list of results x
    For character c in root.next.keys():
        Call colHelp(c, x, root.next.get(c))
    Return x

colHelp(String s, List<String> x, Node n):
    if n.isKey:
        x.add(s)
    For character c in n.next.keys():
        Call colHelp(s + c, x, n.next.get(c))
```

The following pseudocode of `keyWithPrefix` operation could return all keys with specific prefix. (The `collect` operation begins from the node at the end of the prefix.)

```
keysWithPrefix(String s):
    Find the end of the prefix, alpha
    Create an empty list x
    For character in alpha.next.keys():
        Call colHelp("sa" + c, x, alpha.next.get(c))
    Return x
```

### Autocomplete

The search suggestion of Google is a kind of autocomplete, which could be implemented with a trie.

* Values of nodes will represent the weight of the string.
* Trie could store billions of strings efficiently since they share nodes.
* When a user types a query, `keysWithPrefix` operation will return several strings with the highest value.

