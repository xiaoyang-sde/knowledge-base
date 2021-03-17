# ADTs, Sets, Maps, BSTs

### Abstract Data Type

An Abstract Data Type \(ADT\) is defined only by its operations, not by its implementation. For instance, we say that `ArrayDeque` and `LinkedListDeque` are implementations of the `Deque` ADT. Here are a few commonly used ADTs.

**Stacks**: Structures that support last-in first-out retrieval of elements

* push\(int x\): puts x on the top of the stack
* int pop\(\): takes the element on the top of the stack

**Lists**: an ordered set of elements

* add\(int i\): adds an element
* int get\(int i\): gets element at index i

**Sets**: an unordered set of unique elements \(no repeats\)

* add\(int i\): adds an element
* contains\(int i\): returns a boolean for whether or not the set contains the value

**Maps**: set of key/value pairs

* put\(K key, V value\): puts a key value pair into the map
* V get\(K key\): gets the value corresponding to the key

### Binary Search Trees

Linked List is unefficient in searching for items since it will cost linear time even if the list is sorted.

We can optimize by adding pointers to the middle of each recursive half of the linked list. The structure is called a **binary tree**.

![BST](https://joshhug.gitbooks.io/hug61b/content/assets/Screen%20Shot%202019-02-28%20at%2012.59.39%20AM.png)

#### Properties of trees

Trees are composed of:

* nodes
* edges that connect those nodes. \(Only one path between any two nodes.\)

In some trees, we select a **root** node which is a node that has no parents.

Tree also has **leaves**, which are nodes with no children.

Binary Trees: In addition to the above requirements, also hold the binary property constraint that each node has either 0, 1, or 2 children.

Binary Search Trees: For each node X in a binary tree:

* Every key in the left subtree is less than X’s key.
* Every key in the right subtree is greater than X’s key.

Here's a simple BST class:

```java
private class BST<Key> {
    private Key key;
    private BST left;
    private BST right;

    public BST(Key key, BST left, BST Right) {
        this.key = key;
        this.left = left;
        this.right = right;
    }

    public BST(Key key) {
        this.key = key;
    }
}
```

#### Search

* Start from root note.
* If searched item is larger, move to the child at right.
* Otherwise, move to the child at left.
* Repeat recursively until we find the item or we get to the leaf of the tree, which means that the tree does not contain the item we want to find.

```text
static BST find(BST T, Key sk) {
   if (T == null)
      return null;
   if (sk.equals(T.key))
      return T;
   else if (sk ≺ T.key)
      return find(T.left, sk);
   else
      return find(T.right, sk);
}
```

If our tree is relatively "bushy", the find operation will run in `log N` time because the height of the tree is `log N`.

### Insert

We always insert at a leaf node.

* Search the item in the tree. If we find it, then we don't do anything.
* We can just add the new element to either the left or right of the leaf, preserving the BST property.

```text
static BST insert(BST T, Key ik) {
  if (T == null)
    return new BST(ik);
  if (ik ≺ T.key)
    T.left = insert(T.left, ik);
  else if (ik ≻ T.key)
    T.right = insert(T.right, ik);
  return T;
}
```

### Delete

#### No children

If the node has no children, it is a leaf, and we can just delete its parent pointer and the node will eventually be swept away by the garbage collector.

#### 1 child

If the node only has one child, we know that the child maintains the BST property with the parent of the node because the property is recursive to the right and left subtrees.

Therefore, we can just reassign the parent's child pointer to the node's child and the node will eventually be garbage collected.

![Delete 1 child](https://joshhug.gitbooks.io/hug61b/content/assets/Screen%20Shot%202019-02-28%20at%2010.35.56%20AM.png)

#### 2 children

If the deleted node has two chldren, we choose a new node to replace the deleted one.

We know that the new node must be:

* larger than everything in left subtree.
* smaller than everything right subtree.

We can just take the right-most node in the left subtree or the left-most node in the right subtree. The method is called Hibbard deletion.

![Delete 2 children](https://joshhug.gitbooks.io/hug61b/content/assets/Screen%20Shot%202019-02-28%20at%2010.43.45%20AM.png)

### BSTs as Sets and Maps

We can use BST to implement `Set` ADT, which is better than using ArraySet, since the worst-case runtime cost `log N` is faster than `N`.

We can also implement `Map` with BST by having each node hold \(key,value\) pairs. We will compare each element's key to determine where to place it within our tree.

