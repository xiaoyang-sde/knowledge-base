# Neural Network

## Model Representation

The neural network model accepts the input features $x_1 \dots x_n$ and outputs the result of the hypothesis function. The model consists of input layers, the hidden layers, output layers. The $x_0$ of the input is the bias unit, which is equal to $1$. The **activation** function is $\frac{1}{1+e^{-\theta^T x}}$, which is the same as the logistic function.

The nodes in the hidden layers is defined as $a_i^j$, which represents the activation of the unit $i$ in the layer $j$. The $\Theta^j$ is the matrix of weights controlling function mapping from layer $j$ to layer $j + 1$. If the $j$-th layer contains $s_j$ units and the $j + 1$-th layer contains $s_{j + 1}$ units, the $\Theta_j$ will be of dimension $(s_{j + 1}) \times (s_j + 1)$. For example, for a neural network with $3$ layers, the value of each activation node could be calculated as:

$$
\begin{align*} a_1^{(2)} = g(\Theta_{10}^{(1)}x_0 + \Theta_{11}^{(1)}x_1 + \Theta_{12}^{(1)}x_2 + \Theta_{13}^{(1)}x_3) \newline a_2^{(2)} = g(\Theta_{20}^{(1)}x_0 + \Theta_{21}^{(1)}x_1 + \Theta_{22}^{(1)}x_2 + \Theta_{23}^{(1)}x_3) \newline a_3^{(2)} = g(\Theta_{30}^{(1)}x_0 + \Theta_{31}^{(1)}x_1 + \Theta_{32}^{(1)}x_2 + \Theta_{33}^{(1)}x_3) \newline h_\Theta(x) = a_1^{(3)} = g(\Theta_{10}^{(2)}a_0^{(2)} + \Theta_{11}^{(2)}a_1^{(2)} + \Theta_{12}^{(2)}a_2^{(2)} + \Theta_{13}^{(2)}a_3^{(2)}) \newline \end{align*}
$$

Let $z_k^j = \Theta_{k, 0}^{1} x_0 + \Theta_{k, 1}^{1} x_1 + \dots + \Theta_{k, n}^{1} x_n$, then $z^j = \Theta^{j - 1}a^{j - 1}$ and $a^j = g(z^j)$.

## Perceptron Learning Algorithm

The perceptron learning algorithm is an iterative method that processes one misclassified point at the time, until there are no misclassified points. The algorithm finds the decision boundary for linearly separable data.

- Set $k$ to $1$ and fill the vector $w^k$ with $0$.
- While there exists a misclassified point $j$ such that $y_j (w^{kT} x_j) < 0$ and $y_j = \pm 1$, update $w^{k + 1} = w_k + y_j x_j$ and let $k = k + 1$.
- Return $w_k$ if all points are classified.
