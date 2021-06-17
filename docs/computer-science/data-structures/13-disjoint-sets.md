# Disjoint Sets

## Dynamic Connectivity and the Disjoint Sets Problem

Our data structure will implement these operations:

* connect\(x, y\): Connects x and y.
* isConnected\(x, y\): Returns true if x and y are connected. Connections can be transitive, which means that they don’t need to be direct.

For simpilicity, we will declare that all items are integers and independent from each other.

```java
ds = DisjointSets(7)
ds.connect(0, 1)
ds.connect(1, 2)
ds.connect(0, 4)
ds.connect(3, 5)
ds.isConnected(2, 4) // true
ds.isConnected(3, 0) // false

ds.connect(4, 2)
ds.connect(4, 6)
ds.connect(3, 6)
ds.isConnected(3, 0) //true
```

## Disjoint Sets Interface

```java
public interface DisjointSets {
    /** Connects two items P and Q. */
    void connect(int p, int q);

    /** Checks to see if two items are connected. */
    boolean isConnected(int p, int q);
}
```

We will implement this interface to achieve these goals:

* Number of elements N can be huge.
* Number of method calls M can be huge.
* Calls to methods may be interspersed

Naive Approach: Record all the connections in a data structure, and do some iteration to see if one thing can be reached from each other.

Better Approaach: Ignore how things are connected, and only record sets that each belongs to.

## Quick Find

To find whether two items are connected, here are two ways:

1. List of sets of integers.

Very inituitive way, but quite slow for large N.

1. List of integers where ith entry gives set number.

connect\(p, q\): Change entries that equal id\[p\] to id\[q\]

```java
public class QuickFindDS implements DisjointSets {
   private int[] id;

   public QuickFindDS(int N) {
      id = new int[N];
      for (int i = 0; i < N; i++) {
         id[i] = i;
      }
    }

    public boolean isConnected(int p, int q) {
      return id[p] == id[q];
    }

    public void connect(int p, int q) {
      int pid = id[p];
      int qid = id[q];
      for (int i = 0; i < id.length; i++) {
         if (id[i] == pid) {
            id[i] = qid;
         }
      }
    }
}
```

However, connecting is still slow.

## Quick Union

Instead of using random number to represent the index of sets, we could let each entry to be its parent, which results in a tree-like shape.

To connect two items, simply change the root of one item to the root of another item.

However, this method is still slow since the tree might be quite tall and the cost of the worst case is proportional to the height.

## Weighted Quick Union

We could modify Quick Union to avoid tall trees: Track tree size and link root of smaller tree to the larger one.

Thus, the `connect` and `isConnected` operation will never be slower than `log N`, which is fast enough for most programs.

Although we could track the height instead of weight, we will find out that the performance is similar.
