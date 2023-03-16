# Support-vector Machine

## Decision Boundaries

The decision boundary in logistic regression separates the training classes in the feature space. If the dataset is linearly separable, the regularized logistic regression produces the model with the smallest coefficients that can separate the classes. However, the boundary that maximizes the margin between the boundary and both classes could be chosen to prevent overfitting.

## Hard Margin

Let the linear boundary be $w^T x + b = 0$, which is a general equation of a hyperplane. The coefficient vector $\vec{w}$ represents the normal vector of the hyperplane. The Euclidean distance between $x$ and the hyperplane is $r = \frac{w^T x + b}{||w||}$. The unsigned distance is equal to $y_n (w^T x_n + b)$, in which $y_n \in \{ -1, 1 \}$.

The optimization problem could be formulated as to find a decision boundary that maximizes the distance to both classes, which is $\text{argmax}_{w, b} \frac{1}{||w||} \min_{n} y_n (w^T x_n + b)$. Because the distance between the hyperplane and the closet point is $\frac{1}{||w||}$, the problem could be reformulated as minimizing $||w||$: $\text{argmin}_{w, b} \frac{1}{2} ||w||^2$ where $y_n (w^T x_n + b) \geq 1$.

- Active point: $y_n (w^T x_n + b) = 1$ (The points closest to the decision boundary)
- Inactive point: $y_n (w^T x_n + b) \geq 1$

With the primal-dual representation technique, the perpendicular vector $w$ could be represented as a scaled sum of the support vectors: $w = \sum_{n \in \text{support set}}a_n \cdot y_n \cdot x_n$ for $a_n \geq 0$.

## Soft Margin

To balance the margin and the error in the dataset, the cost function could take both into consideration: $\text{argmin}_{w, b} \frac{1}{2} \lVert w \rVert^2 + \lambda \sum_{n=1}^{N} \epsilon_n$ where $y_n(w^Tx_n+b) \geq 1 - \epsilon_n$. The non-negative quantity $\epsilon_n$ is equivalent to the error on the point $x_n$. If $\lambda = \infty$, the optimization problem produces the hard margin solution.

- **Margin violation**: The points that are on the correct side of the boundary but are inside the margin. ($D(x) = \frac{\epsilon}{||w||}$ where $0 < \epsilon < 1$)
- **Misclassification**: The points that are on the wrong side of the boundary. ($D(x) = \frac{\epsilon}{||w||}$ where $\epsilon \geq 1$)
