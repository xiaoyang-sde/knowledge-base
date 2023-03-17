# Ensemble Method

The ensemble method is a machine learning technique that combines multiple models in order to improve their predictive power and generalization performance. The basic idea behind an ensemble method is to train multiple models on the same dataset and then combine their predictions to produce a final prediction.

- Bagging: This method involves training multiple models on random subsets of the training data (with replacement) and then averaging their predictions.
- Boosting: This method involves iteratively training models that are weighted based on their performance on the previous iteration. Boosting algorithms prioritize samples that were misclassified in previous iterations.
- Stacking: This method involves training multiple models with different algorithms on the same dataset, and then using a meta-model to combine their predictions.

## AdaBoost

Give $N$ samples $\{ x_n, y_n \}$ and a method to construct weak classifiers. The weight of each training sample is initialized to $w_1(n) = \frac{1}{N}$.

### Algorithm

- For each iteration, the algorithm trains a weak classifier $h_t(x)$ using the current weights $w_t(n)$ that minimizes the weighted classification error $\epsilon_t = \sum_{i = 1}^{n} w_t(n) l(h_t(x_n), y_n)$.
- The contribution of the classifier is $\beta_t = \frac{1}{2} \log \frac{1 - \epsilon_t}{\epsilon_t}$.
- The weight on each training point $w_{t + 1}(n)$ is updated to $w_t(n) e^{-\beta_t y_n h_t(x_n)}$ and then normalized such that $\sum_{i = 1}^{n} w_{t + 1}(n) = 1$.
- The final classifier is $h[x] = \text{sign}[\sum_{t = 1}^{T} \beta_t h_t(x)]$, which is a weighted combination of all weak classifiers, where the weight is proportional to its accuracy.

### Formulation

The loss function used in AdaBoost is exponential loss, where $l(y, h(x)) = e^{-ya(x)}$.

For a classifier $a_{t - 1}(x)$, it could be improved with a weak learner $h_t(x)$, where $a(x) = a_{t - 1}(x) + \beta_t(h_t(x))$. Therefore, AdaBoost picks $h_t, \beta_t$ that minimizes the objective function, where $(h_t(x), \beta_t) = \min_{h_t(x), \beta_t} \sum_{i = 1}^{n} e^{-y_n a(x_n)} = \min_{h_t(x), \beta_t} \sum_{i = 1}^{n} e^{-y_n [a_{t - 1}(x_n) + \beta_t h_t (x_n)]} = \min_{h_t(x), \beta_t} \sum_{i = 1}^{n} w_t(i) e^{-y_i \beta_t h_t (x_i)}$, where $w_t(i) = e^{-y_i a_{t - 1}(x_i)}$.

The weighted loss function could be decomposed as $\sum_{i = 1}^{n} w_t(i) e^{-y_i \beta_t h_t(x_i)} = (e^{\beta_t} - e^{-\beta_t}) \sum_{i = 1}^{n} w_t(i) \mathbf{I}[y_i \ne h_t(x_i)] + e^{-\beta_t} \sum_{i = 1}^{n} w_t(i)$. Therefore, $(h_t(x), \beta_t) = \min_{h_t(x), \beta_t} (e^{\beta_t} - e^{-\beta_t}) \sum_{i = 1}^{n} w_t(i) \mathbb{I}[y_i \ne h_t(x_i)] + e^{-\beta_t} \sum_{i = 1}^{n} w_t(i)$.

- $h_t(x) = \min_{h_t(x)} \epsilon_t = \sum_{i = 1}^{n} w_t(i) \mathbb{I}[y_i \ne h_t(x_i)]$
- $\beta_t = \min_{h_t(x), \beta_t} (e^{\beta_t} - e^{-\beta_t}) \epsilon_t + e^{-\beta_t}$, because $\sum_{i = 1}^{n} w_t(i) = 1$, which implies that $\beta_t = \frac{1}{2} \log \frac{1 - \epsilon_t}{\epsilon_t}$.

The classifier could be updated as $a(x) = a_{t - 1}(x) + \beta_t h_t (x)$ and its weights are $w_{t + 1}(n) = e^{-y_n a(x_n)} = w_t(n) e^{-y_n \beta_t h_t(x_n)}$, which is $w_t(n) e^{-\beta_t}$ if $y_n = h_t (x_n)$ and $w_t(n) e^{\beta_t}$ if $y_n \ne h_t (x_n)$. Therefore, misclassified data points will get their weights increased, while classified data points will get their weight decreased, which means that the weak classifiers in the next iteration will focus more on the samples that were difficult to classify correctly, with the goal of improving the overall accuracy of the model.
