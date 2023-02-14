# Logistic Regression

## Classification

In classification, the dataset is represented with $D = \{(\mathbf{x}_1, y_1), \dots, (\mathbf{x}_N, y_N)\}$, in which $y_i \in \{ 0, \dots, K - 1 \}$ is a discrete label. For binary classification problems, the labels $y_i \in \{0, 1\}$. Let the model be $f_\theta(\mathbf{x}) = \theta^{T} \mathbf{x}$, the sigmoid function $\text{sigmoid}(z) = \frac{1}{1 + e^{-z}}$ turns the output $f_\theta (\mathbf{x})$ into the probability distribution over $y$: $p_\theta (y = 1 | \mathbf{x}) = \text{sigmoid}(f_\theta (\mathbf{x}))$ and $p_\theta(y = 0|\mathbf{x}) = 1 - p_\theta(y = 1|\mathbf{x})$.

## Cost Function

Logistic regression represents a linear model that probabilistically captures the confidence of the classified point. The closer the point is to the decision boundary, the less confidence the model has in its value. If the probability is greater than 0.5, the point is classified as 1.

The likelihood of a single training sample $(x_n, y_n)$ is $p(y_n|x_n; b; w) = h_{w, b}(x_n)^{y_n}[1 - h_{w, b}(x_n)]^{1 - y_n}$, where $h_{w, b}(x_n) = \sigma(b + w^T x_n)$. The negative log-likelihood of the whole training data is $J(\theta) = -\sum_{n} {y_n \log h_{\theta} (x_n) + (1 - y_n) \log [1 - h_{\theta}(x_n)]}$. The cost function approaches to $\infty$ if the predicted label is different from the actual label.

## Gradient Descent

The gradient descent is an iterative first-order optimization algorithm used to find a local minimum or maximum of a given function. The method to find the $\theta$ that minimizes the cost function of logistic regression is updating the values of $\theta$ with the gradient rule $\theta_j^{t+1} = \theta_j^t - \alpha \frac{dL(\theta)}{d\theta^t_j}$, where $\frac{dL(\theta)}{d\theta^t_j} = \sum^{n}_{i = 1} (\frac{1}{1 + e^{-(\theta^T x_i)}} - y_i)x^j_i$ or $X^T ((\frac{1}{1 + e^{-(X \theta^T)}}) - \vec{y})$.

The general form of minimizing $f(\theta)$ is $\theta^{t + 1} \leftarrow \theta^t - \alpha \Delta f(\theta^t)$, where $\alpha$ is the step size. With a suitable choice of $\alpha$, the iterative procedure converges to a point where $\Delta f(\theta) = 0$.

### Convex Function

The function $f(x)$ is convex if $f(\lambda a + (1 - \lambda) b) \le \lambda f(a) + (1 - \lambda) f(b)$ for $0 \le \lambda \le 1$ or if $f''(x) \ge 0$.

For a multi-variate function, $f(x)$ is convex if the Hessian is positive semi-definite. The matrix $H$ is positive semi-definite if $z^T H z = \sum_{j, k} H_{j, k} z_j z_k \ge 0$ for all $z$. The Hessian of $f(x)$ is defined as $H_{ij} = \frac{\partial^2 f}{\partial x_i \partial x_j}$.

### Stochastic Gradient Descent

The Stochastic Gradient Descent algorithm updates the model parameters based on a random training example in each iteration, rather than using the entire training set. The approach is faster for large-scale models.

## Implementation

```cpp
// Given a input feature vector and the parameter vector,
// predict the likelihood that the target output is `1`
auto hypothesis(
  const vector<double>& x,
  const vector<double>& theta
) -> double {
  double z = std::inner_product(x.cbegin(), x.cend(), theta.cbegin(), 0.0);
  return 1.0 / (1.0 + exp(-z));
}

// Implement the Gradient Descent algorithm
// to optimize the parameter vector
auto gradient_descent(
  const vector<vector<double>>& X,
  const vector<double>& y,
  vector<double>& theta,
  double alpha,
  int iteration_limit
) -> vector<double> {
  for (int i = 0; i < num_iters; ++i) {
    vector<double> gradient(theta.size());
    for (int j = 0; j < m; ++j) {
      const vector<double>& x = X[j];
      double h = hypothesis(x, theta);
      for (int k = 0; k < theta.size(); ++k) {
        gradient[k] += (h - y[j]) * x[k];
      }
    }
    for (int k = 0; k < theta.size(); ++k) {
      theta[k] -= alpha * gradient[k] / m;
    }
  }
  return theta;
}
```

## Multiclass Classification

For multiclass classification problems, the labels $y_i \in \{0, 1, \dots, K - 1 \}$.

- The **one-vs-all** approach divides the problem into $K$ binary classification problems, and trains a logistic regression classifier $h_\theta(x)$ for each problem to predict the probability that $y = K_i$.
- The **all-vs-all** approach divides the problem into $K \choose{2}$ binary classification problems. For example, if $K = 3$, there are 3 matches: $K_1$ and $K_2$, $K_1$ and $K_3$, $K_2$ and $K_3$. The label of the test point is determined by the most frequent class.
