# Learning Theory

## PAC Learning

PAC learning, short for Probably Approximately Correct learning, is a framework for studying the theoretical properties of machine learning algorithms. The goal of PAC learning is to design algorithms that can learn a target concept with high probability and low error, given a sample of labeled data drawn randomly from a distribution.

### Definition

- Instance space: $X$, the set of examples
- Concept space: $C$, the set of possible target functions (e.g., all $n$-dimensional linear functions)
  - $f \in C$ is the hidden target function
- Hypothesis space: $H$, the set of possible hypothese (the learning algorithm explores the hypothesis space)
- Training instances: $S \times \{ -1, 1 \}$, the set of positive and negative examples of the target concept

### PAC Learnability

Consider a concept class $C$ defined over an instance space
$X$ (containing instances of length $n$), and a learner $L$ using a hypothesis space $H$. The concept class $C$ is PAC learnable by $L$ using $H$ if for all $f \in C$, for all distribution $D$ over $X$, and fixed $\epsilon > 0, \delta < 1$, given $m$ examples sampled from $D$, the algorithm $L$ produces, with probability at least $(1 - \delta)$, a hypothesis $h \in H$ that has error at most $\epsilon$. In other words, for all desired levels of accuracy and confidence, the algorithm can learn a hypothesis that is close to the true concept with high probability.

- Polynomial sample complexity: The sample size $m$ is polynomial in $\frac{1}{\epsilon}$, $\frac{1}{\delta}$, $n$, and the size of $H$, which is necessary for PAC learnability.
- Polynomial time complexity: The learning algorithm produces the hypothesis in time that is polynomial in $\frac{1}{\epsilon}$, $\frac{1}{\delta}$, $n$, and the size of $H$.

For example, given $m$ examples, the algorithm $L$ produces, with probability at least $(1 - \delta)$, a hypothesis $h \in H$ that has error at most $\epsilon$, then $m > \frac{1}{\epsilon} (\ln(|H) + \ln(\frac{1}{\delta}))$.

### Shattering

A set $S$ of examples is shattered by a set of functions $H$ if for all partitions of the examples in $S$ into positive and negative examples, there's a function in $H$ that gives these labels to the examples. For example, a linear function could shatter a set of $2$ points, but couldn't shatter a set of $3$ points.

### Vapnik-Chervonenkis Dimension

The Vapnik-Chervonenkis dimension is a measure of an infinite hypothesis space, such as a linear threshold function. The VC dimension of hypothesis space $H$ over instance space $X$ is the size of the largest finite subset of $X$ that is shattered by $H$. If there's a subset of size $d$ that can be shattered, $VC(H) \ge d$. In general, the VC dimension of an $n$-dimensional linear function is $n + 1$.
