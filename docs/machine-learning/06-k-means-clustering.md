# K-Means Clustering

K-Means is a method of vector quantization that aims to partition $n$ observations into $k$ clusters in which each observation belongs to the cluster with the nearest mean (cluster centers), serving as a prototype of the cluster.

For $N$ data points that defined as $x_i (1 \leq i \leq N)$ define an indicator variable $r_{nk} = 1$ if $x_n$ is assigned to cluster $k$, and $r_{nk} = 0$ otherwise. The distortion measure is defined as $J = \sum_{n = 1}^{N} \sum_{k = 1}^{k} r_{nk} || x_n - \mu_k ||^2$, where $\mu_k$ is the prototype of the class $k$. The goal of the algorithm is to minimize the distortion.

## Procedure

The K-means algorithm iterates through the assignment and refitting steps until a stopping criterion is satisfied such as the distortion doesn't decrease.

- Initialization: The algorithm starts with the initialization of the cluster centroid. Pick the first cluster centroid as one of the data points, and pick the second centroid as the data point furthest from the first, and so on.
- Assignment: For each data point, label it with the cluster centroid that is closest to it. $r_{nk} = 1$ if $k = \text{argmin}_{1 \leq i \leq k} || x_n - \mu_i ||^2$. The assignment step minimizes $J$ respect to the data point.
- Refitting: Move each cluster centroid to the average of the points assigned to it. $\mu_k = \frac{\sum^{N}_{n = 1} r_{nk} \cdot x_n}{\sum^N_{n = 1} r_{nk}}$. The assignment step minimizes $J$ respect to the cluster centroid.

## Hierarchical Clustering

Hierarchical clustering is a method of cluster analysis which seeks to build a hierarchy of clusters. The motivation of the method is that the split or merge with the K-means algorithm is not monotonic in the clusters when $k$ increases or decreases.

- Agglomerative: Each observation starts in its own cluster, and pairs of clusters are merged if the overall distortion is reduced.
  - Simple linkage: Merge clusters $G$ and $H$ whose two members are closest.
  - Complete linkage: Merge clusters $G$ and $H$ whose distance is the smallest. The distance between clusters equals the distance between those two elements that are farthest away from each other.
  - Group average: Merge clusters $G$ and $H$ whose average dissimilarity is the smallest.
- Divisive: All observations start in one cluster, and the cluster is split when the overall distortion is reduced.
