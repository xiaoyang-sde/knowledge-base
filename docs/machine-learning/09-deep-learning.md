# Deep Learning

## Model Representation

The neural network is consist of nodes and edges. Each edge has an associated weight and activation level. Each node has an input function, and activation function, and an output.

The exterior function sets the input values for each neuron in the input layer, and these input values determine the activation level of the neuron's output link. The input function of each unit is applied to compute the input value, and the activation function transforms the input function into a final value.

- Let $a_i^{(j)}$ be the activation of unit $i$ in layer $j$.
- Let $\Theta^{(j)}$ be thew eight matrix of function mapping from layer $j$ to layer $j + 1$.

In general, $z^{(i)} = \Theta^{(i - 1)} x$, $a^{(i)} = g(z^{(i)})$, and $z^{(i + 1)} = \Theta^{(i)}$. The activation of the output layer is the prediction. For a network with 4 input nodes (including a bias node) in layer 1, 4 nodes in layer 2, and a node in layer 3:

$$a_1^{(2)} = g(\Theta_{10}^{(1)} x_0 + \Theta_{11}^{(1)} x_1 + \Theta_{12}^{(1)} x_2 + \Theta_{13}^{(1)} x_3) = g(z_1^{(2)})$$
$$a_2^{(2)} = g(\Theta_{20}^{(1)} x_0 + \Theta_{21}^{(1)} x_1 + \Theta_{22}^{(1)} x_2 + \Theta_{23}^{(1)} x_3) = g(z_2^{(2)})$$
$$a_3^{(2)} = g(\Theta_{30}^{(1)} x_0 + \Theta_{31}^{(1)} x_1 + \Theta_{32}^{(1)} x_2 + \Theta_{33}^{(1)} x_3) = g(z_3^{(2)})$$
$$h_{\Theta}(x) = g(\Theta_{10}^{(2)} a_0^{(2)} + \Theta_{11}^{(2)} a_1^{(2)} + \Theta_{12}^{(2)} a_2^{(2)} + \Theta_{13}^{(2)} a_3^{(2)}) = g(z_1^{(3)})$$

For example, a logistic unit in a neural network sums the input $x$ with weights $\theta$, which results in $\theta^T x$, and then computes the output with the sigmoid activation function $g(z) = \frac{1}{1 + e^{-z}}$: $g(\theta^T x) = \frac{1}{1 + e^{-\theta^T x}}$.

## Back Propagation

The back propagation approach is an algorithm to optimize the loss function of the neural network. The gradient of the los function with respect to the weights is calculated using the chain rule of calculus and propagated backwards through the network.

- Forward pass: Compute the output of the neural network for a given input.
- Compute the loss: Calculate the difference between the network's output and the true target output.
- Backward pass: Compute the gradient of the loss with respect to each weight in the network. For a node with $L = z(w)$, the gradient is $\frac{\partial L}{\partial w} = \frac{\partial z}{\partial w} \frac{\partial L}{\partial z}$.
- Update the weight: $w' = w - \alpha \frac{\partial L}{\partial w}$
