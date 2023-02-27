# Linear Regression

## Mean Squared Error (MSE)

**Linear least squares regression** is an optimization procedure used for data fitting. Each data point contains the independent variable $x_i$ and dependent variable $y_i$. If the dependent variables are proportional to the independent variables with some measurement errors $\epsilon_i$, then $y_i = \theta x_i + \epsilon_i$.

The **residuals** is the difference between observed and predicted data. The **mean squared error** (MSE) is defined as $\min_{\theta} \frac{1}{N} \sum^{N}_{i = 1} (y_i  - \theta x_i)^2$. The $\hat{\theta}$ that minimizes the MSE is $\hat{\theta} = \frac{\vec{x}^{T} \vec{y}}{\vec{x}^{T} \vec{x}}$.

## Bootstrapping and Confidence Interval

Bootstrapping is an applicable method to assess confidence or uncertainty about estimated parameters. The method generates new synthetic datasets from the original dataset with random sampling, finds estimators for each of these new datasets, and quantifies the confidence of the accuracy of the model based on the distribution of the estimators. Each new resampled dataset has the same size as the original dataset with the new data points sampled with replacement.

The confidence intervals of the bootstrapped estimates quantifies how uncertain the model is. The 95% confidence interval of the parameters is defined as $\text{CI}_{0.95}^{\hat{\theta_i}} = [\hat{\theta_i} - 1.96 * SE(\hat{\theta_i}), \hat{\theta_i} + 1.96 * SE(\hat{\theta_i})]$. 95% confidence interval implies that if the interval is created from the sampled data, the probability of the true $\theta$ in the confidence interval is 95%.

## Implementation

```cpp
// Given a input feature vector and the parameter vector,
// predict the value of the output parameter
auto hypothesis(
  const vector<double>& x,
  const vector<double>& theta
) -> double {
  return std::inner_product(x.cbegin(), x.cend(), theta.cbegin(), 0.0);
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

## Multiple Linear Regression

The multivariate linear regression is modeled as $y = \theta_0 + \theta_1 x_1 + \theta_2 x_2 + \dots + \theta_d x_d + \epsilon$, where $\theta_0$ is the intercept and $d$ is the number of features. The model could be represented in matrix form as $\mathbf{y} = \mathbf{X}\boldsymbol{\theta} + \mathbf{\epsilon}$, where $\mathbf{y}$ is a vector of measurements, $\mathbf{X}$ is a matrix containing the feature values (columns) for each input sample (rows), and $\boldsymbol{\theta}$ is the parameter vector.

$${\begin{bmatrix}y_{1}\\y_{2}\\y_{3}\\y_{4}\\y_{5}\\y_{6}\\y_{7}\end{bmatrix}}={\begin{bmatrix}1&w_{1}&x_{1}\\1&w_{2}&x_{2}\\1&w_{3}&x_{3}\\1&w_{4}&x_{4}\\1&w_{5}&x_{5}\\1&w_{6}&x_{6}\\1&w_{7}&x_{7}\end{bmatrix}}{\begin{bmatrix}\beta _{0}\\\beta _{1}\\\beta _{2}\end{bmatrix}}+{\begin{bmatrix}\varepsilon _{1}\\\varepsilon _{2}\\\varepsilon _{3}\\\varepsilon _{4}\\\varepsilon _{5}\\\varepsilon _{6}\\\varepsilon _{7}\end{bmatrix}}$$

For the multiple MSE regressor, the optimal vector of parameters $\boldsymbol{\hat\theta}$ could be computed as $\boldsymbol{\hat\theta} = (\mathbf{X}^\top\mathbf{X})^{-1}\mathbf{X}^\top\mathbf{y}$.

```py
def least_squares(X, y):
  theta_hat = np.linalg.inv(X.T @ X) @ X.T @ y
```

## Non-linear Classification

For a training data set that is not linearly separable, the feature vector could be transformed to $\phi(x): x \in R^N \rightarrow z \in R^M$, where $M$ is the dimension of the transformed feature. The prediction is based on $\theta^T \phi(x)$, thus the residual sum squares is $J(\theta) = \sum_n [\theta^T \phi(x_n) - y_n]^2$, where $\theta \in R^M$.

Let $\Phi \in R^{N \times M}$ be the transformed training set, $\hat{\theta} = (\Phi^T \Phi^{-1}) \Phi^{T} y$.

## Polynomial Regression

The polynomial regression is an extension of the linear regression, which could be modeled as $y = \theta_0 + \theta_1 x + \theta_2 x^2 \dots + \epsilon$. The design matrix $X$ for polynomial regression of order $k$ is $\mathbf{X} = \big[ \boldsymbol 1 , \mathbf{x}^1, \mathbf{x}^2 , \ldots , \mathbf{x}^k \big]$, where $\mathbf{x}^p$ is the vector $\mathbf{x}$ with all elements raised to the power $p$.

For inputs with more than one feature, the design matrix could be defined as $\mathbf{X} = \big[ \boldsymbol 1 , \mathbf{x}_m^1, \mathbf{x}_n^1, \mathbf{x}_m^2 , \mathbf{x}_n^2\ldots , \mathbf{x}_m^k , \mathbf{x}_n^k \big]$, where $\mathbf{x}_m$ is a vector of one feature per data point.

## Regularization

The regularization is a technique that discourages a more complex or flexible model to avoid the risk of overfitting. The cost function $L$ is modified to $L_{\text{reg}}(\theta) = L(\theta) + \lambda R(\theta)$. The $\lambda$ parameter is a scalar that represents the weight of the regularization term.

- **Ridge**: $L_{\text{Ridge}} = L(\theta) + \lambda \sum_{j = 1}^{J} \theta_j^2$
- **LASSO**: $L_{\text{Ridge}} = L(\theta) + \lambda \sum_{j = 1}^{J} |\theta_j|$

For Ridge regularized linear regression, the analytical solution is $\theta = (X^{T}X + \lambda I)^{-1} X^{T}y$. When $\lambda = 0$, the solution is reduced to the least squares solution. The optimization is convex when $\lambda \ge 0$.
