# CS 61B Week 07

## B-Trees (2-3, 2-3-4 Trees)

Height of BST
* Worst case: `Theta(N)`
* Best case: `Theta(log N)`

![Height of BST ](https://joshhug.gitbooks.io/hug61b/content/assets/Screen%20Shot%202019-03-05%20at%2012.56.54%20PM.png "Height of BST ")

O is just an upper bound, rather than the worst case. However, many people use O as a shorthand for worst case.

### BST Performance

* depth: the number of links between a node and the root.
* height: the lowest depth of a tree, which determines the worst case of runtime.
* average depth: average of the total depths in the tree, which determines the average-case runtime.

### BST insertion order

If we insert nodes in random order, it will actually end up being relatively bushy, in which the average depth and height are expected to be `Theta(log N)`.

However, in the real world situations, data are unlikely inserted randomly.

### B-Trees

![2-3-4 Tree](https://joshhug.gitbooks.io/hug61b/content/assets/Screen%20Shot%202019-03-05%20at%204.12.17%20PM.png "2-3-4 Tree")


The tree showed above is called **B-Tree** or **2-3-4 Tree**. 2-3-4 and 2-3 refer to the number of children each node can have. 

The process of adding a node to a 2-3-4 tree is:

* Similar to BST, we compare new item with each node in the tree and insert it into the leaf node.
* If the node has 4 items, pop up the middle left item and re-arrange the children accordingly.
* If the parent node having 4 nodes, pop up the middle left node again, rearranging the children accordingly.
* Repeat this process until the parent node can accommodate or you get to the root.

### B-tree Runtime Analysis

The worst case runtime of searching a B-tree:
* Each node had the maximum number of elements
* Traverse all the way to bottom

The amount of operations will be `L log N`. Since `L` is constant, the runtime is `O(log N)`. B-tree is complex, but it could handle insertion operations in any order.

## Red Black Trees

We will create a new tree structure similar to B-trees, since B-trees are really difficult to implement.

### Rotating Trees

rotateLeft(G): Let x be the right child of G. Make G the new left child of x.

rotateRight(G): Let x be the left child of G. Make G the new right child of x.

Below is a graphical description of what happens in a left rotation on the node G.

![Rotating Left](https://joshhug.gitbooks.io/hug61b/content/assets/Screen%20Shot%202019-03-06%20at%2010.25.18%20PM.png)

We could also rotate a non-root node.

![None Root Node](https://joshhug.gitbooks.io/hug61b/content/assets/Screen%20Shot%202019-03-06%20at%2010.37.17%20PM.png)

### Red Black Trees

We could use a red link to convert a 3-node to BST tree. We choose arbitrarily to make the left element a child of the right one. The structure is called left-leaning red-black trees (LLRB).

![Red Link](https://joshhug.gitbooks.io/hug61b/content/assets/Screen%20Shot%202019-03-06%20at%2010.56.51%20PM.png)

#### Properties

* Left-Leaning Red-Black trees have a 1-1 correspondence with 2-3 trees.
* No node has 2 red links.
* There are no red right-links.
* Every path from root to leaf has same number of black links.
* Height is no more than 2x height of corresponding 2-3 tree.

#### Inserting into LLRB

* While inserting, use a red link.
* If there is a right leaning "3-node", rotate left the appropriate node to fix.
* If there are two consecutive left links, rotate right the appropriate node to fix.
* If there are any nodes with two red children, color flip the node to emulate the split operation.

#### Runtime

Because a LLRB tree has a 1-1 correspondence with a 2-3 tree and will always remain within 2x the height of its 2-3 tree, the runtimes of the operations will take `log N` time.

## Hashing

### Limits of Search Tree Based Sets

* Require items to be comparable.
* Have excellent performance, but could be better.

### Using Data as an Index

We could create an array of booleans indexed by data.
* Initially all values are `false`.
* When an item is added, set the index to `true`.

```java
DataIndexedIntegerSet diis;
diis = new DataaIndexedIntegerSet();
diis.add(0); // set 0 to true
diis.add(5); // set 5 to true
```

#### Implementation

```java
public class DataIndexedIntegerSet {
    private boolean[] present;

    public DataIndexedIntegerSet() {
        present = new boolean[2000000000];
    }

    public void add(int x) {
        present[i] = true;
    }

    public boolean contains(int x) {
        return present[i];
    }
}
```

#### Performance

* `contains(x)`: Theta(1);
* `add(x)`: Theta(1);
* Extremely wasteful of memory.
* Need ways to generalize beyond integers.

### Inserting Words

#### First Attempt

Suppose we want to insert "cat" to the data strucutre, we could use the first letter as index. (a = 1, b = 2, ... , z = 26)

However, other words may start with 'c' or special characters.

#### Avoiding Collisions

We could se all digits by multiplying each by a power of 27.
* a = 1, b = 2, ... , z = 26
* The index of "cat" is (3 x 27^2)  + (1 x 27^1) + (20 x 27^0) = 2234.
* The index of "bee" is (2 x 27^2)  + (5 x 27^1) + (5 x 27^0) = 1598.

As long as we pick a base >= 26, this algorithm is guaranteed to give each lowercase English word a unqiue number. Thus, we will never have a collision.

```java
// englishToInt() is a helper method which could turn string to index.

public class DataIndexedEnglishWordSet {
    private boolean[] present;

    public DataIndexedEnglishWordSet() {
        present = new boolean[2000000000];
    }

    public void add(String s) {
        present[englishToInt(s)] = true;
    }

    public boolean contains(int i) {
        resent present[englishToInt(s)];
    }
}
```

### Inserting Strings

We need to change our base to 126 if we want to insert strings other than English word.

The most basic character set used by computers is ASCII format.
* Each possible charaacter is assigned a value between 0 and 127.
* Characters 33 - 126 are "printable".
* For example, `char c = 'D'` is equivalent to `char c = 68`.

```java
public static int asciiToInt(String s) {
    int intRep = 0;
    for (int i = 0; i < s.length(); i += 1) {           
        intRep = intRep * 126;
        intRep = intRep + s.charAt(i);
    }
    return intRep;
}
```

However, if we want to represent character sets for other languages like Chinese, we could use unicode. 
For example, `char c = "鳌"` is equivalent to `char c = 40140`. 
The largest possible value for Chinese characters is 40959, so we need to use this as our base, but the index might be really large.

#### Integer Overflow

Because Java has a maximum integer (2,147,483,647), we will easily run into overflow for short strings and create collisions.

For example, `asciiToInt(omens)` will return `-1,867,853,901` instead of `28,196,917,171`.

From the smallest to the largest possible integers, there are a total of 4,294,967,296 integers in Java, which means that collision is inevitable, and we need to find a way to avoid it.

Pigeonhole princeple tells us that if there are more than 4,294,967,296 possible items, multiple items will share the same hash code. (The official term of the number we are computing.)

* Resolve hash code collisions. (collision handling)
* Compute a hash code for arbitrary objects. (computing a hash code)

### Collision Handling

We know that a few items will share a same hash code due to integer overflow. 
However, we could store that bucket of these items into the position in the array instead of store `true` or `false`. 
We could implement the bucket with LinkedList, BST, and other data structures.

Each bucket in our array is initially empty. Bucket h is a separate chain of all items that have a hash code h. When an item x gets added at h: 

* If bucket h is empty, create a new list containing x and store it at h.
* If bucket h is already a list, add x to the list at h if it's not already present.

Worst case runtime will be proportional to length of longest list.

* `contains(x)`: Theta(Q);
* `add(x)`: Theta(Q);

### Saving Memory

We can use modulus of hash code to reduce bucket count, but lists will be longer. 
If the hash code of "abomamora" is 111239443 and we have only 10 buckets, we will put that string into bucket 3, since 111239443 % 10 = 3.

What we've just created is called a hash table.

* Data is converted by a hash function into an integer representation called a hash code.
* The hash code is then reduced to a valid index, usually using the modulus operator.
* data -> hash function -> hash code -> reduce -> index

### Dynamic Growth

We could dynamicly adjust the number of buckets to make the data structure more efficient. 
Suppose we have M buckets and N items, the load factor is `N/M`, and we need to keep that factor low. 
When the load factor reaches certain threshold, we double M:

* Create a new HashTable with 2M buckets.
* Iterate through all the items in the old HashTable, and add them into this new HashTable.
* Since the modulus changes, items may belong to different buckets.

If all elements are evenly distributed, the runtime will be `Theta(N/M)`. Because `N/M` is lower than the threshold, `Theta(N/M)` is equal to `Theta(1)`.

Item will be evenly distributed if we have a hash code algorithm that gives fairly random values for different items.

### Hash Code Algorithm

* We could use the base strategy as we developed before.
* We could use a small prime base number, such as 31, since we don't need to avoid collisions.
* Base 126 means that any string that ends in the same last 32 characters has the same hashcode.