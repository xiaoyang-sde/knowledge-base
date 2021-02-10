# Range Searching and Multi-Dimensional Data

## Introduction

Multi-Dimensional data strucutres could perform operations on a set of Body objects in space.

* 2D Range Finding: Count the objects in a specific region.
* Nearest Neighbors: Find the closest object to another object.

## HashTable

If the objects are stored in a HashTable, the problems above could be solved inefficiently.

* Runtime: `Θ(N)`, since the bucket that each object resides in is effectively random.

## Uniform Partitioning

The HashTable could be enhanced if each object is stored in a specific bucket based on its position. The space could be divided with a grid, and each portion is corresponded to a bucket in the HashTable, which is called "spatial hashing".

Instead of using the `hashCode()` method, each object provide a `getX()` and `getY()` method so that it can compute its own bucket number.

This method could reduce the amount of buckets to be searched, so that it is faster than without spatial partitioning on average.

* Runtime: `Θ(N)`, since the amount of grids is a constant.

## X-Based Tree or Y-Based Tree

Search Trees could explicitly track the order of items. To build a tree of objects in a two-dimensional space, the items should be comparable.

* X-Based Tree: Compare objects only based on their x-coordinate.
* Y-Based Tree: Compare objects only based on their y-coordinate.

Here's an example of X-Based Tree:
![X-Based Tree](https://joshhug.gitbooks.io/hug61b/content/assets/Screen%20Shot%202019-03-15%20at%2011.40.56%20AM.png)
![X-Based Tree](https://joshhug.gitbooks.io/hug61b/content/assets/Screen%20Shot%202019-03-15%20at%2011.41.02%20AM.png)

If a range finding operation is performed in the green rectangle, the right child of the root node is ignored. Skipping searching through parts of the search tree is called "pruning".

* Search in the optimized dimension: `log N`, due to the pruning operation.
* Search in the non-optimized dimension: `N`, which could be optimized with the QuadTree.

## QuadTree

The QuadTree is a tree data structure which could partition a two-dimensional space by recursively subdividing it into four quadrants.

![QuadTree](https://joshhug.gitbooks.io/hug61b/content/assets/Screen%20Shot%202019-03-16%20at%201.33.04%20AM.png)
![QuadTree](https://joshhug.gitbooks.io/hug61b/content/assets/Screen%20Shot%202019-03-16%20at%201.33.08%20AM.png)

* Each node has exactly four children: The northwest, northeast, southeast, and southwest region.
* The node B is inserted as the NE child of node A, since B resides in the northeast quadrant of A.

![QuadTree](https://joshhug.gitbooks.io/hug61b/content/assets/Screen%20Shot%202019-03-16%20at%201.46.01%20AM.png)

If a range finding operation is performed in the green rectangle, from any node the quadrants which the green rectangle does not lie within could be ignored and pruned away.

Quad-Trees are effective in 2-D spaces, but K-D trees could perform the operations in higher dimension spaces.

## K-D Trees

The K-D Tree could extend the hierarchical partitioning idea to multi-dimensions, which works by rotating through all the dimensions by each level.

For the 2-D case, it partitions like an X-based Tree on the first level, then like a Y-based Tree on the next, then as an X-based Tree on third level, etc. 

* Each node has two children, since each level is partitioned into "greater" and "less than".
* Items equal in one dimension should always be stored in the "greater" part of its parent node.

![K-D Tree](https://joshhug.gitbooks.io/hug61b/content/assets/Screen%20Shot%202019-03-16%20at%205.33.01%20PM.png)

![K-D Tree](https://joshhug.gitbooks.io/hug61b/content/assets/Screen%20Shot%202019-03-16%20at%201.57.42%20AM.png)

### Nearest Neighbor

![K-D Tree](https://joshhug.gitbooks.io/hug61b/content/assets/Screen%20Shot%202019-03-16%20at%205.42.50%20PM.png)

* Start at the root and compute its distance to the query point, and save that as the minimum distance.
* For each subspace the node partitioned into, try to find a better point by computing the shortest distance between the query point and the edge of the subspace.
* Compute recursively for each subspace which might contains a possibly better point.
* Return the point with the minimum distance.
