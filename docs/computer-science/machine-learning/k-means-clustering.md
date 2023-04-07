# K-Means Clustering

K-Means is a method of vector quantization that aims to partition $n$ observations into $k$ clusters in which each observation belongs to the cluster with the nearest mean $\mu_k$ (cluster centroids), serving as a prototype of the cluster.

For $N$ data points that defined as $x_i (1 \leq i \leq N)$ define an indicator variable $r_{nk} = 1$ if $x_n$ is assigned to cluster $k$, and $r_{nk} = 0$ otherwise. The distortion measure (clustering objective function) is defined as $J = \sum_{n = 1}^{N} \sum_{k = 1}^{K} r_{nk} || x_n - \mu_k ||^2$, where $\mu_k$ is the prototype of the class $k$. The goal of the algorithm is to minimize the distortion, which is a non-convex objective function.

## Llyod's Algorithm

The K-means algorithm iterates through the assignment and refitting steps until a stopping criterion is satisfied such as the distortion doesn't decrease.

- The algorithm starts with the initialization of the cluster centroid $\{ \mu_k \}$ to some values.
- Assume the value of $\{ \mu_k \}$ is fixed, minimize $J$ over $\{ r_{nk} \}$, which which assigns $x_n$ to its closest centroid. $r_{nk} = 1 \iff k = \min_{j} ||x_n - \mu_j||$.
- Assume the value of $\{ r_{nk} \}$ is fixed, minimize $J$ over $\{ \mu_k \}$, which move each cluster centroid to the average of the points assigned to it. $\mu_k = \frac{\sum^{N}_{n = 1} r_{nk} x_n}{\sum^N_{n = 1} r_{nk}}$.
- Terminate if the objective function $J$ doesn't change.

## Hierarchical Clustering

Hierarchical clustering is a method of cluster analysis which seeks to build a hierarchy of clusters. The motivation of the method is that the split or merge with the K-means algorithm is not monotonic in the clusters when $k$ increases or decreases.

- Agglomerative: Each observation starts in its own cluster, and pairs of clusters are merged if the overall distortion is reduced.
  - Simple linkage: Merge clusters $G$ and $H$ whose two members are closest.
  - Complete linkage: Merge clusters $G$ and $H$ whose distance is the smallest. The distance between clusters equals the distance between those two elements that are farthest away from each other.
  - Group average: Merge clusters $G$ and $H$ whose average dissimilarity is the smallest.
- Divisive: All observations start in one cluster, and the cluster is split when the overall distortion is reduced.
