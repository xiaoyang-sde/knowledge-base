# Principal Component Analysis

Principal component analysis is the process of computing the principal components and using them to perform a change of basis on the data. The method removes correlated features, overcomes overfitting issue, and makes visualization understandable.

## PCA Formulation

- Let $X \in \mathbb{R}^{n \times d}$, which is a matrix of $n$ training data with $d$ features, where $x_j^{(i)}$ is the $j$-th feature of the $i$-th training data.
  - Mean of the $j$-th feature: $\mu_j = \frac{1}{n} \sum_{i = 1}^{n} x_j^{(i)}$
  - Variance of $j$-th feature: $\sigma_j^2 = \frac{1}{n} \sum_{i = 1}^{n} (x_j^{(i)} - \mu_j)^2$
  - Covariance of $j_1$-th and $j_2$-th feature (assuming zero mean): $\sigma_j^2 = \frac{1}{n} \sum_{i = 1}^{n} x_{j_1}^{(i)} x_{j_2}^{(i)}$, which increases when the features are more correlated
- Let $P \in \mathbb{R}^{d \times k}$, which is a linear transformation that transforms the training data into a reduced representation with $k$ principal components.
- Therefore, $Z \in \mathbb{R}^{n \times k} = XP$ is the reduced representation of the training data.

The covariance matrix $C_X = \frac{1}{n} X^T X$ generalizes the covariance of features into a $d \times d$ matrix, where $i$-th diagonal element is the variance of $i$-th feature, and the $ij$-th element is the covariance between $i$-th and $j$-th features.

The PCA algorithm finds the $P$ such that the variance of the reduced representation $Z$ is maximized, which is equal to the top $k$ eigenvectors of $C_X$. The $d$ eigenvectors are orthonormal directions of max variance, in which their associated eigenvalues are equal variance in these directions.

## Formula ($k = 1$)

$\sigma_z^2 = \frac{1}{n} \sum_{i = 1}^{n} (z^{(i)})^2 = \frac{1}{n} ||z||^2 = \frac{1}{n} z^T z = \frac{1}{n} p^T X^T X p = p^T C_x p$, where $||p|| = 1$.

## Feature Scaling

To compute the principal components of the dataset, the features are scaled with either of these two approaches:

- **Standardization** is a scaling technique where the values are centered around the mean with a unit standard deviation. The technique is suitable if the data follows a Gaussian-like distribution. $x_{\text{new}} = \frac{x_{\text{old}} - x_{\text{mean}}}{\text{std}(x)}$
- **Normalization** is a scaling technique where the values are shifted and rescaled to the range $[0, 1]$. The technique is suitable if the data doesn't follow a Gaussian-like distribution. $x_{\text{new}} = \frac{x_{\text{old}} - x_{\text{min}}}{x_{\text{max}} - x_{\text{min}}}$
