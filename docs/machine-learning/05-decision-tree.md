# Decision Tree

The **decision tree** is a non-parametric supervised learning method used for classification and regression. The goal is to create a model that predicts the value of a target variable by learning simple decision rules inferred from the data features.

Complex decision tree has low bias and high variance, which is able to capture more complex geometry in the training data. Complex decision tree is hard to interpret and expensive to train.

## Outline

- Set of possible instances $\mathbb{X}$
  - Each instance $x \in \mathbb{X}$ is feature vector
- Set of possible labels $\mathbb{Y}$
- Unknown target function $f: \mathbb{X} \rightarrow \mathbb{Y}$
- Model: $H = \{ h | h: \mathbb{X} \rightarrow \mathbb{Y} \}$, in which each hypothesis $h$ is a decision tree
- Goal: Train a function $h$ that maps instance to label
- Learn: The structure of the tree, the threshold values $\theta_i$, and the values for the leaves

## Construction Procedure

```cpp
TreeNode train_decision_tree(vector<Data> data, set<Feature> remaining_features) {
  string guess = most_frequent_answer(data);
  if unambiguous(data) || remaining_features.size() == 0 {
    return LeafNode(guess);
  }

  set<Feature, double> score;
  for (const Feature& f : remaining_features) {
    auto [true_set, false_set] = split_feature(data, f);
    score[f] = node_split_algorithm(true_set, false_set);
  }

  Feature target_f = get_optimal_feature(score);
  auto [true_set, false_set] = split_feature(data, target_f);
  remaining_features.erase(target_f);

  return TreeNode(
    train_decision_tree(true_set, remaining_features),
    train_decision_tree(false_set, remaining_features)
  );
}
```

## ID3 Algorithm

The ID3 algorithm decides the variables or nodes that are the most efficient to split based on information gain. The algorithm selects the attribute that maximizes entropy reduction. The entropy represents the amount of uncertainty of a random variable with a specific probablity distribution.

- The entropy is defined as $H[X] = - \sum_{k = 1}^{K} P(X = a_k) \log_2 P(X = a_k)$, where $a_k$ is a value of the random variable $X$.
- The conditional entropy is defined as $H[Y|X] = \sum_{k} P(X = a_k) H[Y|X = a_k]$, which is the weighted average of the entropy of each branch.
- The information gain is used to capture the reduction in uncertainty, which is defined as $H[Y] - H[Y|X]$.
