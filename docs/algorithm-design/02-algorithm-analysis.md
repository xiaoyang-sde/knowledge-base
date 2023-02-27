# Basics of Algorithm Analysis

## Computational Tractability

1. An algorithm is efficient if, when implemented, it runs quickly on real input instances.
2. An algorithm is efficient if it achieves qualittatively better worst-case performance, at an analytical level, than brute-force search.
3. An algorithm is efficient if it has a polynomial running time. The running time is polynomial if it's bounded by $cN^{d}$ primitive computational steps.

## Asymptotic Order of Growth

The computational tractability is based on bounding an algorithm's worst-case running time on inputs of size n grows at a rate that is at most proportional to $f(n)$.

### Asymptotic Upper Bounds

Let $T(n)$ be a function of the worst-case running time of a certain algorithm on an input size of n.

Given another function $f(n)$, we say that $T(n)$ is $O(f(n))$ if $T(n)$ is bounded above by a constant multiple of $f(n)$. ($T(n) \leq c \cdot f(n)$)

Example: $T(n) = pn^{2} + qn + r \leq pn^{2} + qn^{2} + rn^{2} = (p + q + r)n^{2} = c \cdot n^{2}$, thus $T(n)$ is $O(n^{2})$ or $O(n^{k}), k \geq 2$.

### Asymptotic Lower Bounds

Let $T(n)$ be a function of the worst-case running time of a certain algorithm on an input size of n.

Given another function $f(n)$, we say that $T(n)$ is $\Omega (f(n))$ if $T(n)$ is bounded below by a constant multiple of $f(n)$. ($T(n) \geq \epsilon \cdot f(n)$)

Example: $T(n) = pn^{2} + qn + r \geq pn^{2} = \epsilon \cdot n^{2}$, thus $T(n)$ is $\Omega (n^{2})$ or $\Omega(n^{k}), k \leq 2$.

### Asynmptotic Tight Bounds

If we can show that a running time $T(n)$ is both $O(f(n))$ and $\Omega (f(n))$, then $T(n)$ grows exactly like $f(n)$ to within a constant factor. Therefore, we could say that $T(n)$ is $\Theta (f(n))$.

Example: $T(n)$ is $\Omega (n^{2})$ and $O(n^{2})$, thus $T(n)$ is $\Theta (n^{2})$.

### Properties of Asynmptotic Growth Rates

#### Transitivity

- Upper bounds: $f = O(g), g = O(h), f = O(h)$
- Lower bounds: $f = \Omega (g), g = \Omega (h), f = \Omega (h)$
- Tight bounds: $f = \Theta (g), g = \Theta (h), f = \Theta (h)$

#### Sum of Functions

- Suppose that f and g are two functions and for some other function h, we have $f = O(h)$ and $g = O(h)$. Then $f + g = O(h)$.
- Let k be a fixed constant, and let $f_{1}, f_{2}, ..., f_{k}$ and h be functions such that $f_{i} = O(h)$. Then $f_{1} + f_{2} + ... + f_{k} = O(h)$.
- Suppose that f and g are two functions such that $g = O(f)$. Then $f + g = \Theta(f)$. f is an asymptotically tight bound for the combined function.

#### Limit

Let f and g be two functions that $\lim_{n \to \infty} \frac{f(n)}{g(n)}$ exists.

- If the limit is equal to 0. Then $f(n) = O(g(n)), f(n) \neq \Theta (g(n))$.
- If the limit is equal to some number c > 0. Then $f(n) = \Theta (g(n))$.
- If the limit is equal to infinity. Then $f(n) = \Omega (g(n)), f(n) \neq \Theta (g(n))$.

### Asymptotic Bounds for Common Functions

- **Polynomial**: Let f be a polynomial of degree d, in which the coefficient $a_{d}$ is positive. Then $f = \Theta (n^{d})$.
- **Logarithms**: For every b > 1 and every x > 0, $log_{b}n = O(n ^ {x})$.
- **Exponentials**: For every r > 1 and every d > 0, $n^{d} = O(r^{n})$.

## Implementing the Stable Matching Algorithm

The algorithm terminates in at most $n^{2}$ iterations. If we implement each iteration in constant time, the upper bound of this algorithm is $n^{2}$.

### Data Structures

- `ManPerf[m, i]` array: The $i^{th}$ woman on man m's preference lists.
- `WomanPerf[w, i]` array: The $i^{th}$ man on woman w's preference lists.
- `FreeMan` list: The linked list of free men.
- `Next[m]` array: The array that indicates for each man m the position of the next woman he will propose to.
- `Current[w]` array: The array that indicates for each woman her current partner. The default value is `null`.
- `Ranking[w, m]` array: The array that contains the rank of man m in the sorted order of w' preferences.

### The Algorithm

1. Identify a free man: Take the first man from `FreeMan`.
2. Identify the highest-ranking woman to whom the man m has not yet proposed: Find the woman with `Next[m]`.
3. Decide if woman w is engaged and who is her current partner: Find the partner with `Current[w]`.
4. Decide which of m or m' is preferred by w in constant time: Compare `Ranking[w, m]` and `Ranking[w, m']`.

## A Survey of Common Running Times

### Linear Time

The basic way to get an algorithm with linear time ($O(n)$) is to process the input in a single pass, spending a constant amount of time on each item of input encountered.

- Finding the maximum: Process the numbers in a single pass.
- Merging two sorted lists:

### O(n log n) Time

$O(n \log n)$ time is common since it's the running time of any algorithm that splits its input into two equals-ized pieces, solves each piece recursively, and then combines the two solutions in linear time. Mergesort is a well-known example.

### Quadratic Time

Quadratic time arises naturally from a pair of nested loops or performing a search over all pairs of input items and spending constant time per pair.

### Cubic Time

More elaborate sets of nested loops often lead to algorithms that run in $O(n ^ {3})$ time.

### O(n^k) Time

We obtain a running time of $O(n^{k})$ for any constant k when we search over all subsets of size k.

### Beyond Polynomial Time

$O(2^{n})$ arises naturally as a running time for a search algorithm that must consider all subsets.

$O(n!)$ arises in problems where the search space consists of all ways to arrange n items in order or all matches between two sets.

### Sublinear Time

Algorithms with sublinear runtime arise in a model of computation where the input can be "queried" indirectly rather than read completely, since it takes linear time just to read the input.

The running time of binary search is $O(\log n)$, because of the successive shrinking of the search region. It arises as a time bound when an algorithm does a constant amount of work to throw away a constant fraction of the input.

## Priority Queue

A priority queue maintains a set of elements S, where each element v ∈ S has an associated value `key(v)` that denotes the priority of element v; smaller keys represent higher priorities.

A priority queue upports the addition and deletion of elements from the set, and also the selection of the element with smallest key.

A sequence of $O(n)$ priority queue operations can be used to sort a set of n numbers. If each operation is $O(\log n)$ time, we could sort n numbers in $O(n \log n)$ time.

### The Definition of Heap

**Heap order**: The key of any element is at least as large as the key of the element at its parent node in the tree.

A heap is a balanced binary tree that satisfies the heap order. We could use an array indexed by $i = 1, ..., N$ to represent the tree.

- The root node: `Heap[1]`
- Left Child: `leftChild(i) = 2i`
- Right Child: `rightChild(i) = 2i + 1`
- Parent: `parent(i) = ⌊i/2⌋`
- Size: `length(H)`

### Implementing the Heap Operations

The heap element with smallest key is at the root, so it takes $O(1)$ time to identify the minimal element.

#### Addition

1. Append the new element to the end of the list with index `i`.
2. Use the `Heapify-up` procedure to fix our heap.
3. Let `j = ⌊i/2⌋` be the parent of the node `i`.
4. If `key[Heap[i]] < key[Heap[j]]`, swap the positions of `Heap[i]` and `Heap[j]`.
5. Process recursively from the position `i` to continue fixing the heap.

The procedure `Heapify-up(H, i)` fixes the heap property in $O(\log i)$ time, assuming that the array H is almost a heap with the key of `H[i]` too small. Using `Heapify-up` we can insert a new element in a heap of n elements in $O(\log n)$ time.

#### Deletion

1. When deleting the item at position i, move the element `w` in position n to position i.
2. Use the `Heapify-up` or `Heapify-down` procedure to fix our heap.
3. Let `2i` and `2i + 1` be the children of the node `i`.
4. Let `j` be the minimum of `2i` and `2i + 1`.
5. If `key[Heap[i]] > key[Heap[j]]`, swap the positions of `Heap[i]` and `Heap[j]`.
6. Process recursively from the position `j` to continue fixing the heap.

The procedure `Heapify-down(H, i)` fixes the heap property in $O(\log n)$ time, assuming that H is almost a heap with the key value of `H[i]` too big. Using `Heapify-up` or `Heapify-down` we can delete a new element in a heap of n elements in $O(log n)$ time.

### Implementing Priority Queues with Heaps

The heap data structure with the Heapify-down and Heapify-up operations can efficiently implement a priority queue that is constrained to hold at most N elements at any point in time.

- `StartHeap(n)`: returns an empty heap with at most N elements. ($O(n)$)
- `Insert(h, v)`: inserts the item v into heap H. ($O(\log n)$)
- `Delete(h, v)`: delsete the item v at position i of heap H. ($O(\log n)$)
- `FindMin(H)`: identifies the minimum element in the heap H. ($O(1)$)
- `ExtractMin(H)`: identifies and deletes an element with minimum key value from a heap. ($O(\log n)$)

To be able to access given elements of the priority queue efficiently, we simply maintain an additional array `Position` that stores the current position of each element in the heap.

- To delete the element v, we apply `Delete(H,Position[v])`.
- To change the key of element v, we identify the position of v, change its key, and fix the heap with `Heapify-up` or `Heapify-down`.
