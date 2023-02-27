# Machine Learning

## General Framework

The loss function $l(y, \hat{y})$ is defined to evaluate the performance of the learning algorithm. The regrssion problems could use squared loss $l(y, \hat{y}) = (y - \hat{y})^2$ or absolute loss $l(y, \hat{y}) = |y - \hat{y}|$, while classification problems could use zero/one loss $l(y, \hat{y}) = 0$ if $y = \hat{y}$.

The data generating distribution is a probability distribution $D$ over the input and output pairs. $D$ assigns high probability to reasonable $(x, y)$ pairs. The learning algorithm doesn't know the distribution. The training data is a random sample of input and output pairs drawn from $D$.

Based on the training data, the machine learning algorithm could induce a function $f$ that maps new inputs $\hat{x}$ to corresponding prediction $\hat{y}$. $f$ should generalize for future examples that are drawn from $D$. It's expected loss $\epsilon$ over $D$ with respect to $l$ should be minimized.

$$\epsilon = \mathbb{E}_{(x, y) \sim D} [l(y, f(x))] = \sum_{(x, y)} D(x, y) l(y, f(x))$$

The training error is the average error over the training data, which is defined as $\cap{\epsilon} = \frac{1}{N} \sum_{n = 1}^{N} l(y_n, f(x_n))$, where $N$ is the size of the training data. The distribution $D$ for training data must match the distribution $D$ of the test data.

## Data Generating Distribution

The Bayes optimal classifier $f^{\text{BO}}$ is a classifier that for each test input $\hat{x}$, returns the $\hat{y}$ that maximizes $D(\hat{x}, \hat{y})$, which achieves the smallest zero/one error.

The Bayes error rate is the error rate of the Bayes optimal classifier, which is the best error rate that can be achieved on this classification problem under zero/one loss. The machine learning algorithm needs to learn the mapping from $x$ to $y$ with access to a training set sampled from $D$.

## Model Selection

- **Training data**: The data used for the fitting procedure for a given model.
- **Development data**: The data used for tuning hyperparameters.
- **Test data**: The data not used during the fitting procedure for a given model.

### Bias-Variance Trade-off

- **Bias** is the difference between the prediction of the model and the corresponding true output variables. Models with high bias will not fit the training data well since the predictions are quite different from the true data (**underfitting**).
- **Variance** is the variability of model predictions for a given input. Models with high variance are highly dependent on the exact training data used, which will not generalize to the test data (**overfitting**).

### Cross Validation

Different models have different quality of predictions on the training data and on the test data. The **validation data** is not used for the fitting procedure but is used to select the best model.

The **k-fold cross-validation** method divides the training data into $k$ subsets, trains the model on the first $k - 1$ folds, and then compute the error on the last fold. The procedure is repeated $k$ times on each $k - 1$ folds of the data, and the average error of the $k$ trained models could be used to evaluate the model performance.

### Geometric View

Each data could be represented as a feature vector in a multi-dimensional feature space consisting of one dimension for each feature, where each dimension is a real value. The feature vector is denoted as $x = (x_1, x_2, \dots, x_D) \in \mathbb{R}^D$, where $x_d$ is the value of the $d$-th feature of $x$.

- For a real-valued feature, its value maps to a feature vector.
- For a binary feature, `true` maps to `1` and `false` maps to `0`.
- For a categorical feature with $V$ possible categories, each value is mapped to $V$-dimension binary indicator features. (If a consecutive sequence of numbers are assigned to each category, the unordered set will turn into an ordered set, which has a negative effect on the geometric view.)
