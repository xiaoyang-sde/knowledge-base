# Principal Component Analysis

Principal component analysis is the process of computing the principal components and using them to perform a change of basis on the data. The method removes correlated features, overcomes overfitting issue, and makes visualization understandable.

## Feature Scaling

To compute the principal components of the dataset, the features are scaled with either of these two approaches:

- **Standardization** is a scaling technique where the values are centered around the mean with a unit standard deviation. The technique is suitable if the data follows a Gaussian-like distribution. $x_{\text{new}} = \frac{x_{\text{old}} - x_{\text{mean}}}{\text{std}(x)}$
- **Normalization** is a scaling technique where the values are shifted and rescaled to the range $[0, 1]$. The technique is suitable if the data doesn't follow a Gaussian-like distribution. $x_{\text{new}} = \frac{x_{\text{old}} - x_{\text{min}}}{x_{\text{max}} - x_{\text{min}}}$

## Dimension Reduction

Suppose the data point is provided in $D$-dimensional space, but it could be explained in a $M$-dimensional subspace for $M < D$. The projection of the data points with the minimum projection error is the appropriate subspace of dimension $M$.

For a vector space $V$ of dimension $D$, a set of vectors $\{ v_1, v_2, \dots, v_D \}$ forms a basis if vectors $v_1, v_2, \dots, v_D$ are linearly independent. The basis that has vectors with unit norm that are orthogonal ($u_i^T u_j = 0$) to each other is an orthonormal basis.

Suppose the dataset contains $N$ data points $x_1, x_2, \dots, x_N$ in a $D$-dimensional space. Let $u_1$ be a vector of unit norm in the most infrormative dimension (highest sample variance). The projection of the data point $x_i$ is $z_i = u_1^T x_i$ and the projected sample mean is $\bar{z} = \frac{1}{N} \sum^N_{n = 1} z_n$. Therefore, the sample variance is $S = \frac{1}{N} \sum_{n = 1}^{N} (x_n - \bar{x})(x_n - \bar{x})^T$, and the projected sample variance is $\frac{1}{N} \sum_{n = 1}^{N} || z_n - \bar{z} ||^2 = u_1^T \times S \times u_1$.
