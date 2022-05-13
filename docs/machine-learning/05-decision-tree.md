# Decision Tree

The **decision tree** is a non-parametric supervised learning method used for classification and regression. The goal is to create a model that predicts the value of a target variable by learning simple decision rules inferred from the data features.

Complex decision tree has low bias and high variance, which is able to capture more complex geometry in the training data. Complex decision tree is hard to interpret and expensive to train.

## Construction Procedure

- Let $n$ be the root of the decision tree and $D$ be the set of unclassified examples in the training set.
- While $D$ is not an empty set:
  - Pick $A$ as the beset decision attribute to query on
  - Assign $A$ to $n$
  - For each value of $A$, create a new descendant of $n$
  - Asign class values to descendants based on $A$
  - Remove examples that are perfectly classified from $D$
- Terminate if $D$ is empty, or recurse over the new leaf nodes.

## Node-Split Algorithm

The algorithm that decides the variables or nodes that are the most efficient to split is based on information gain. The algorithm selects the attribute that maximizes entropy reduction.

- The entropy is used to measure how mixed a variable is, which could be calculated with $- \sum_{i = 1}^{c} P(X_i) \log_b P(X_i)$.
- The information gain is used to capture the reduction in uncertainty, which could be calcualted with $\text{Entropy}(\text{parent}) - \sum_{i = 1}^N P_i \text{Entropy}(\text{child}_i)$.

The algorithm that decides the variables or nodes that are the most efficient to split is based on the Gini index. The algorithm selects the attribute that maximizes the Gini index reduction.

- The Gini index is used to measure how pure a variable is, which could be calculated with $1 - \sum_{i = 1}^{n} P_i^2$.
- The Gini index of the parent node could be calcualted with $\text{Gini}(\text{parent}) - \sum_{i = 1}^N P_i \text{Gini}(\text{child}_i)$.

In the cross validation step, the particular split is pruned if decision tree without the split performs better with the validation set.

## Stopping Condition

- Purity: Terminate if all instances in the region belong to the same class.
- Majority: Terminate if all instances have the same attribute but different label. Pick the majority.
- Thresholding: Terminate if the number of instances in the subregion will fall below the pre-defined threshold or the number of leaves in tree will exceed pre-defined threshold. The appropriate thresholds can be determined by cross-validation.

## Adaptation for Regression

- For regression, the splitting criterion that promotes splits that improves the predictive accuracy of the model as measured by MSE is selected.
- For regression with output in $R$, each region in the model is labeled with a real number, which is the average of the output values of the training points contained in the region.

To construct a decision tree for regression task, start with an empty decision tree, and choose a predictor $j$ to split and choose a threshold value $t_j$ such that the weighted average MSE of the new regions is as small as possible ($\text{argmin}_{j, t_j} \{ \frac{N_1}{N} \text{MSE}(R_1) + \frac{N_2}{N} \text{MSE}(R_2) \}$) where $N_i$ is the number of training points in $R_i$ and $N$ is the number of points in $R$.

To predict a data point $x_i$, traverse the tree until reaching a leaf node, and $\hat{y}$ is the averaged value of the $y$ in the leaf.
