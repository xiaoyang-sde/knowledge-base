# Logistic Regression

## Classification

In classification, the dataset is represented with $D = \{(\mathbf{x}_1, y_1), \dots, (\mathbf{x}_N, y_N)\}$, in which $y_i \in \{ 0, \dots, K - 1 \}$ is a discrete label. For binary classification problems, the labels $y_i \in \{0, 1\}$. Let the model be $f_\theta(\mathbf{x}) = \theta^{T} \mathbf{x}$, the sigmoid function $\text{sigmoid}(z) = \frac{1}{1 + e^{-z}}$ turns the output $f_\theta (\mathbf{x})$ into the probability distribution over $y$: $p_\theta (y = 1 | \mathbf{x}) = \text{sigmoid}(f_\theta (\mathbf{x}))$ and $p_\theta(y = 0|\mathbf{x}) = 1 - p_\theta(y = 1|\mathbf{x})$.

## Cost Function

Logistic regression represents a linear model that probabilistically captures the confidence of the classified point. The closer the point is to the decision boundary, the less confidence
the model has in its value. If the probability is greater than 0.5, the point is classified as 1.

The cost function of logistic regression is defined as $J(\theta) = \dfrac{1}{m} \sum_{i=1}^m \mathrm{Cost}(h_\theta(x^{i}),y^{i})$, where $\mathrm{Cost}(h_\theta(x),y) = -\log(h_\theta(x))$ if $y = 1$ and $\mathrm{Cost}(h_\theta(x),y) = -\log(1-h_\theta(x))$ if $y = 0$. The cost function could be simplified to $J(\theta) = \frac{1}{m} \cdot \left(-y^{T}\log(h)-(1-y)^{T}\log(1-h)\right)$. The cost function approaches to $\infty$ if the predicted label is different from the actual label.

## Gradient Descent

The gradient descent is an iterative first-order optimization algorithm used to find a local minimum or maximum of a given function. The method to find the $\theta$ that minimizes the cost function of logistic regression is updating the values of $\theta$ with the gradient rule $\theta_j^{t+1} = \theta_j^t - \alpha \frac{dL(\theta)}{d\theta^t_j}$, where $\frac{dL(\theta)}{d\theta^t_j} = \sum^{n}_{i = 1} (\frac{1}{1 + e^{-(\theta^T x_i)}} - y_i)x^j_i$ or $X^T ((\frac{1}{1 + e^{-(X \theta^T)}}) - \vec{y})$.

## Multiclass Classification

For multiclass classification problems, the labels $y_i \in \{0, 1, \dots, K - 1 \}$.

- The **one-vs-all** approach divides the problem into $K$ binary classification problems, and trains a logistic regression classifier $h_\theta(x)$ for each problem to predict the probability that $y = K_i$.
- The **all-vs-all** approach divides the problem into $K \choose{2}$ binary classification problems. For example, if $K = 3$, there are 3 matches: $K_1$ and $K_2$, $K_1$ and $K_3$, $K_2$ and $K_3$. The label of the test point is determined by the most frequent class.

## Regularization

The regularization is a technique that discourages a more complex or flexible model to avoid the risk of overfitting. The cost function $L$ is modified to $L_{\text{reg}}(\theta) = L(\theta) + \lambda R(\theta)$. The $\lambda$ parameter is a scalar that represents the weight of the regularization term.

- **Ridge**: $L_{\text{Ridge}} = L(\theta) + \lambda \sum_{j = 1}^{J} \theta_j^2$
- **LASSO**: $L_{\text{Ridge}} = L(\theta) + \lambda \sum_{j = 1}^{J} |\theta_j|$
