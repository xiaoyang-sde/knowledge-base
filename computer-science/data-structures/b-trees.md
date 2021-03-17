# B-Trees \(2-3, 2-3-4 Trees\)

Height of BST

* Worst case: `Theta(N)`
* Best case: `Theta(log N)`

![Height of BST ](https://joshhug.gitbooks.io/hug61b/content/assets/Screen%20Shot%202019-03-05%20at%2012.56.54%20PM.png)

O is just an upper bound, rather than the worst case. However, many people use O as a shorthand for worst case.

## BST Performance

* depth: the number of links between a node and the root.
* height: the lowest depth of a tree, which determines the worst case of runtime.
* average depth: average of the total depths in the tree, which determines the average-case runtime.

## BST insertion order

If we insert nodes in random order, it will actually end up being relatively bushy, in which the average depth and height are expected to be `Theta(log N)`.

However, in the real world situations, data are unlikely inserted randomly.

## B-Trees

![2-3-4 Tree](https://joshhug.gitbooks.io/hug61b/content/assets/Screen%20Shot%202019-03-05%20at%204.12.17%20PM.png)

The tree showed above is called **B-Tree** or **2-3-4 Tree**. 2-3-4 and 2-3 refer to the number of children each node can have.

The process of adding a node to a 2-3-4 tree is:

* Similar to BST, we compare new item with each node in the tree and insert it into the leaf node.
* If the node has 4 items, pop up the middle left item and re-arrange the children accordingly.
* If the parent node having 4 nodes, pop up the middle left node again, rearranging the children accordingly.
* Repeat this process until the parent node can accommodate or you get to the root.

## B-tree Runtime Analysis

The worst case runtime of searching a B-tree:

* Each node had the maximum number of elements
* Traverse all the way to bottom

The amount of operations will be `L log N`. Since `L` is constant, the runtime is `O(log N)`. B-tree is complex, but it could handle insertion operations in any order.
