# Perceptron

## Model Representation

The neuron stores weights $w_1, w_2, \dots, w_D$ and a bias $b$, accepts an input vector $x = (x_1, x_2, \dots, x_D)$, and then computes the sum $a = [\sum_{d = 1}^{D} w_d x_d] + b$ to determine the amount of activation. The neuron predicts positive if $a > \theta$ for a threshold $\theta$.

## Training Algorithm

The perceptron is an algorithm for neural model of learning. It's an online and error driven algorithm, which means that iterates through the training data multiple times and updates its parameters to reduce errors. For a gvien data, it makes a prediction, and updates the parameters if the prediction is incorrect. The algorithm assumes that the label is either `-1` or `1`.

```cpp
auto train_perceptron(
  vector<tuple<vector<int>, int>> data_set,
  int max_iteration,
  int dimension
) -> tuple<vector<int>, int> {
  vector<int> weight(dimension);
  int bias = 0;

  for (int i = 0; i < max_iteration; ++i) {
    for (const auto& [input_vector, label] : data_set) {
      int a = inner_product(weight.cbegin(), weight.cend(), input_vector.cbegin(), 0) + bias;

      // `a` and `label` have the same sign,
      // which means that the prediction is correct
      if (a * label >= 0) {
        continue;
      }

      for (int d = 0; d < dimension; ++d) {
        weight[d] += label * input_vector[d];
        bias += label;
      }
    }
  }

  return {weight, bias};
}

auto test_perceptron(
  vector<int> weight,
  int bias,
  vector<int> input_vector
) {
  int a = inner_product(weight.cbegin(), weight.cend(), input_vector.cbegin(), 0);
  return a >= 0;
}
```

The hyperparameter of the perceptron algorithm is `max_iteration`, the number of passes to make over the training data. The algorithm might overfit if `max_iteration` is high, and might underfit if `max_iteration` is low.

## Geometric Intrepretation

The decision boundary of a perceptron is where the sign of the activation $a$ changes from $-1$ to $1$, which is the set of points $x$ that achieve zero activation: $\mathbb{B} = \{ x : \sum_{d} w_d x_d = w \cdot x = 0 \}$. The decision boundary is the plane perpendicular to $w$. The bias shifts the decision boundary from the origin by $b$ units in the direction of $w$.

## Interpreting Perceptron Weight

The perceptron learns a classifier of the form $x = \text{sign}(\sum_d w_d x_d + b)$. The rate at which the activation changes as a function of the $i$-th feature is $w_i$. Therefore, the features with largest weights are the
features that the perceptron is most sensitive to for making positive, and the features with smallest weights are the features that the perceptron is most sensitive to for making negative predictions.

## Convergence

The perceptron algorithm converges when it iterates through the entire training data without making an update, which means that it has correctly classified all training data. In this case, the data is linearly separable, which means that there's a hyperplane that separates positive and negative data. The perceptron algorithm will converge if such hyperplane exists.

The margin is the distance between the hyperplane and the nearest point. Given a data set $D$, a weight vector $w$, and bias $b$, $\text{margin}(D, w, b) = \min_{(x, y) \in D} y(w \cdot x + b)$ if $w$ separates $D$. The margin of a data set is the largest attainable margin on the data.

The perceptron convergence theorem states that if the perceptron algorithm is run on a linearly separable data set $D$ with margin $\gamma > 0$, and $||x|| \le 1$ for all $x \in D$, then the algorithm will converge after at most $\frac{1}{\gamma^2}$ updates.

## Voting and Averaging

- Voting: As the perceptron learns, it remembers how long each hyperplane survives. At test time, each hyperplane encountered during training votes on the class of a test example. Let $(w, b)^{(1)}, \dots, (w, b)^{(k)}$ be the $K + 1$ weight vectors encountered during training, and $c^{(1)}, \dots, c^{(k)}$ be the survival times for each weight vectors. The prediction on a test point is $\hat{y} = \text{sign}(\sum_{k = 1}^{K} c^{(k)} \text{sign}(w^{(k)} \cdot \hat{x} + b^{(k)}))$. However, this approach requires an absurd amount of storage.
- Averaging: As the perceptron learns, it maintains a running sum of the averaged weight vector and averaged bias. At test time, it predicts based on the average weight vector. $\hat{y} = \text{sign}(\sum_{k = 1}^{K} c^{(k)} w^{(k)} \cdot \hat{x} + b^{(k)}) = \text{sign}((\sum_{k = 1}^{K} c^{(k)} w^{(k)}) \cdot \hat{x} + \sum_{k = 1}^{K} c^{(k)} b^{(k)})$.

## Limitation

The limitation of perceptron is that its decision boundaries are linear. For data sets that are not linearly seperable, the algorithm could not separate the data. The feature combination approach could address the problem, but it will increase computation and result in overfitting.
