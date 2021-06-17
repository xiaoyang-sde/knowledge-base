# Hashing

## Limits of Search Tree Based Sets

* Require items to be comparable.
* Have excellent performance, but could be better.

## Using Data as an Index

We could create an array of booleans indexed by data.

* Initially all values are `false`.
* When an item is added, set the index to `true`.

```java
DataIndexedIntegerSet diis;
diis = new DataaIndexedIntegerSet();
diis.add(0); // set 0 to true
diis.add(5); // set 5 to true
```

### Implementation

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

### Performance

* `contains(x)`: Theta\(1\);
* `add(x)`: Theta\(1\);
* Extremely wasteful of memory.
* Need ways to generalize beyond integers.

## Inserting Words

### First Attempt

Suppose we want to insert "cat" to the data strucutre, we could use the first letter as index. \(a = 1, b = 2, ... , z = 26\)

However, other words may start with 'c' or special characters.

### Avoiding Collisions

We could se all digits by multiplying each by a power of 27.

* a = 1, b = 2, ... , z = 26
* The index of "cat" is \(3 x 27^2\)  + \(1 x 27^1\) + \(20 x 27^0\) = 2234.
* The index of "bee" is \(2 x 27^2\)  + \(5 x 27^1\) + \(5 x 27^0\) = 1598.

As long as we pick a base &gt;= 26, this algorithm is guaranteed to give each lowercase English word a unqiue number. Thus, we will never have a collision.

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

## Inserting Strings

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

However, if we want to represent character sets for other languages like Chinese, we could use unicode. For example, `char c = "鳌"` is equivalent to `char c = 40140`. The largest possible value for Chinese characters is 40959, so we need to use this as our base, but the index might be really large.

### Integer Overflow

Because Java has a maximum integer \(2,147,483,647\), we will easily run into overflow for short strings and create collisions.

For example, `asciiToInt(omens)` will return `-1,867,853,901` instead of `28,196,917,171`.

From the smallest to the largest possible integers, there are a total of 4,294,967,296 integers in Java, which means that collision is inevitable, and we need to find a way to avoid it.

Pigeonhole princeple tells us that if there are more than 4,294,967,296 possible items, multiple items will share the same hash code. \(The official term of the number we are computing.\)

* Resolve hash code collisions. \(collision handling\)
* Compute a hash code for arbitrary objects. \(computing a hash code\)

## Collision Handling

We know that a few items will share a same hash code due to integer overflow. However, we could store that bucket of these items into the position in the array instead of store `true` or `false`. We could implement the bucket with LinkedList, BST, and other data structures.

Each bucket in our array is initially empty. Bucket h is a separate chain of all items that have a hash code h. When an item x gets added at h:

* If bucket h is empty, create a new list containing x and store it at h.
* If bucket h is already a list, add x to the list at h if it's not already present.

Worst case runtime will be proportional to length of longest list.

* `contains(x)`: Theta\(Q\);
* `add(x)`: Theta\(Q\);

## Saving Memory

We can use modulus of hash code to reduce bucket count, but lists will be longer. If the hash code of "abomamora" is 111239443 and we have only 10 buckets, we will put that string into bucket 3, since 111239443 % 10 = 3.

What we've just created is called a hash table.

* Data is converted by a hash function into an integer representation called a hash code.
* The hash code is then reduced to a valid index, usually using the modulus operator.
* data -&gt; hash function -&gt; hash code -&gt; reduce -&gt; index

## Dynamic Growth

We could dynamicly adjust the number of buckets to make the data structure more efficient. Suppose we have M buckets and N items, the load factor is `N/M`, and we need to keep that factor low. When the load factor reaches certain threshold, we double M:

* Create a new HashTable with 2M buckets.
* Iterate through all the items in the old HashTable, and add them into this new HashTable.
* Since the modulus changes, items may belong to different buckets.

If all elements are evenly distributed, the runtime will be `Theta(N/M)`. Because `N/M` is lower than the threshold, `Theta(N/M)` is equal to `Theta(1)`.

Item will be evenly distributed if we have a hash code algorithm that gives fairly random values for different items.

## Hash Code Algorithm

* We could use the base strategy as we developed before.
* We could use a small prime base number, such as 31, since we don't need to avoid collisions.
* Base 126 means that any string that ends in the same last 32 characters has the same hashcode.

