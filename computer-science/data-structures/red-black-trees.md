# Red Black Trees

We will create a new tree structure similar to B-trees, since B-trees are really difficult to implement.

## Rotating Trees

rotateLeft\(G\): Let x be the right child of G. Make G the new left child of x.

rotateRight\(G\): Let x be the left child of G. Make G the new right child of x.

Below is a graphical description of what happens in a left rotation on the node G.

![Rotating Left](https://joshhug.gitbooks.io/hug61b/content/assets/Screen%20Shot%202019-03-06%20at%2010.25.18%20PM.png)

We could also rotate a non-root node.

![None Root Node](https://joshhug.gitbooks.io/hug61b/content/assets/Screen%20Shot%202019-03-06%20at%2010.37.17%20PM.png)

## Red Black Trees

We could use a red link to convert a 3-node to BST tree. We choose arbitrarily to make the left element a child of the right one. The structure is called left-leaning red-black trees \(LLRB\).

![Red Link](https://joshhug.gitbooks.io/hug61b/content/assets/Screen%20Shot%202019-03-06%20at%2010.56.51%20PM.png)

### Properties

* Left-Leaning Red-Black trees have a 1-1 correspondence with 2-3 trees.
* No node has 2 red links.
* There are no red right-links.
* Every path from root to leaf has same number of black links.
* Height is no more than 2x height of corresponding 2-3 tree.

### Inserting into LLRB

* While inserting, use a red link.
* If there is a right leaning "3-node", rotate left the appropriate node to fix.
* If there are two consecutive left links, rotate right the appropriate node to fix.
* If there are any nodes with two red children, color flip the node to emulate the split operation.

### Runtime

Because a LLRB tree has a 1-1 correspondence with a 2-3 tree and will always remain within 2x the height of its 2-3 tree, the runtimes of the operations will take `log N` time.

