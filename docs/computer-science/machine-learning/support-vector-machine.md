# Support-vector Machine

## Decision Boundaries

The decision boundary in logistic regression separates the training classes in the feature space. If the dataset is linearly separable, the regularized logistic regression produces the model with the smallest coefficients that can separate the classes. However, the boundary that maximizes the margin between the boundary and both classes could be chosen to prevent overfitting.

The solution of a SVM is determined by a subset of the training instances, which are called support vectors. All other training points do not affect the optimal solution.

## Hard Margin

Let the linear boundary be $w^T x + b = 0$, which is a general equation of a hyperplane. The coefficient vector $\vec{w}$ represents the normal vector of the hyperplane. The Euclidean distance between $x$ and the hyperplane is $r = \frac{w^T x + b}{||w||}$. The unsigned distance is equal to $y_n (w^T x_n + b)$, in which $y_n \in \{ -1, 1 \}$.

The optimization problem could be formulated as to find a decision boundary that maximizes the distance to both classes, which is $\max_{w, b} \frac{1}{||w||} \min_{n} y_n (w^T x_n + b)$. Since the margin doesn't change if $w, b$ is scaled with a constant factor $c$, the problem could be constrained if $w, b$ is scaled such that $\min_{n} y_n (w^T x_n + b) = 1$, which implies that the margin is $\frac{1}{||w||}$. Therefore, the constraint function of SVM is $\max_{w, b} \frac{1}{||w||}$ such that $y_n (w^T x_n + b) \ge 1$.

- Active point: $y_n (w^T x_n + b) = 1$ (The points closest to the decision boundary)
- Inactive point: $y_n (w^T x_n + b) \geq 1$

With the primal-dual representation technique, the perpendicular vector $w$ could be represented as a scaled sum of the support vectors: $w = \sum_{n \in \text{support set}}a_n \cdot y_n \cdot x_n$ for $a_n \geq 0$.

## Soft Margin

To balance the margin and the error in the dataset, the cost function could take both into consideration: $\min_{w, b, \xi} \frac{1}{2} \lVert w \rVert^2 + \lambda \sum_{n=1}^{N} \xi_n$ where $y_n(w^Tx_n+b) \geq 1 - \xi_n$ and $\xi_n \ge 0$. The non-negative slack variable $\xi_n$ is equivalent to the error on the point $x_n$. If $\lambda = \infty$, the optimization problem produces the hard margin solution.

- **Margin violation**: The points that are on the correct side of the boundary but are inside the margin. ($D(x) = \frac{\xi}{||w||}$ where $0 < \xi < 1$)
- **Misclassification**: The points that are on the wrong side of the boundary. ($D(x) = \frac{\xi}{||w||}$ where $\xi \geq 1$)

## Hinge Loss

Assume that $y \in \{ -1, 1 \}$ and the decision rule is $h(x) = \text{SIGN}(a(x))$ with $a(x) = w^T \phi(x) + b$. The hinge loss is defined as $l(y, a(x)) = 0$ if $ya(x) \ge 1$ and $l(y, a(x)) = 1 - ya(x)$ otherwise. The intution is that if raw output $a(x)$ has the same sign with $y$ and is far enough from the decision boundary. The loss increases as the distance increases.

The primal formulation of SVM is $\min_{w, b} \sum_n max(0, 1 - y_n [w^T x_n + b]) + \frac{\lambda}{2} ||w||^2$, which is equavalent to $\min_{w, b, \xi} \frac{1}{2} \lVert w \rVert^2 + \lambda \sum_{n=1}^{N} \xi_n$ where $y_n(w^Tx_n+b) \geq 1 - \xi_n$ and $\xi_n \ge 0$. The formula minimizes the total hinge loss on all training data with L2 regularization.

## Kernelized SVM

For a SVM problem with $y_n(w^T \phi(X_n) + b)$, it can be rewrote so that it depends on the inner product $\phi(x_n)^T \phi(x_m)$, which can be replaced with a kernel function $k(x_n, x_m)$.

- The primal formulation of SVM is $\min_{w, b, \xi} \frac{1}{2} \lVert w \rVert^2 + \lambda \sum_{n=1}^{N} \xi_n$ where $y_n(w^T \phi(x_n) + b) \geq 1 - \xi_n$ and $\xi_n \ge 0$.

- The dual formulation of SVM is $\max_{\alpha} \sum_{i = 1}{n} \alpha_n - \frac{1}{2} \sum_{m, n} y_m y_n \alpha_m \alpha_n k(x_m, x_n)$ where $0 \le \alpha_n \le C$ and $\sum_n \alpha_n y_n = 0$.

The primal variable $w$ is defined as $w_{*} = sum_{i = 1}^{n} \alpha_n y_n \phi(x_n)$ for a new set of dual variables $\alpha_n \ge 0$, thus the prediction of $x_i$ is $h(x_i) = \text{SIGN}(w^T \phi(x_i) + b) = \text{SIGN}(\sum_{i = 1}^{n} y_n \alpha_n k(x_n, x_i) + b)$, which involes the kernel function. The examples with $\alpha_n > 0$ are support vectors.
