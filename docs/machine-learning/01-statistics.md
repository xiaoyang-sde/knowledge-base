# Statistics

## Probability Distributions

### Uniform Distribution

The **uniform distribution** $U(a, b)$ assigns equal probabilities to all values between $a$ and $b$. The random number $x$ is drawn from a uniform distribution with lower and upper bounds $a$ and $b$ with the notation $x \sim U(a, b)$. The `np.random.uniform` function draw samples from a uniform distribution.

```py
np.random.seed(0)
random_nums = np.random.uniform(0, 100, size = (sample_size,))
```

### Binomial Distribution

The **Bernoulli trail** is a trail with two outcomes $(A, B)$ with probability $P(A) = p$ and $P(B) = 1 - p$, and the result of the trail is defined as **Bernoulli distribution**. The **binomial distribution** simulates $n$ number of Bernoulli trails. Given $n$ trails and probability $p$, the binomial probability of $k$ occurrences of $A$ is equal to $P(k|n,p) = {n \choose k} p^k (1 - p)^{n - k}$ and ${n \choose k} = \frac{n!}{k!(n - k)!}$. The binomial distribution is discrete and $\sum_k P(k|n, p) = 1$. The `np.random.binomial` function draws samples from a uniform distribution.

```py
np.random.binomial(n, p, size = (sample_size,))
```

If the trail has $n$ possible outcomes with probabilities $p_1, p_2, \dots, p_n$, the result of the trail is defined as a **categorical distribution**. The **multinomial distribution** simulates $n$ number of the trails.

### Gaussian (Normal) Distribution

The Gaussian distribution is continuous. The center of the Gaussian distribution is defined with the mean $\mu$ and the spread is defined with its standard deviation $\sigma$ or variance $\sigma^2$. The equation for a Gaussian probability density function is $f(x; \mu, \sigma^2) = N(\mu, \sigma^2) = \frac{1}{\sqrt{2 \pi \sigma^2}} \exp{(\frac{-(x - \mu)^2}{2 \sigma^2})}$. The `np.random.normal` function draws samples from a Gaussian distribution.

```py
np.random.normal(mu, sigma, size = (n_samples,))
```

## Statistical Inference

### Basic Probability

- The probability of the event $A$ is defined as $P(A) \in [0, 1]$ and the complementary is defined as $P(\lnot A) = 1 - P(A)$.
- The conditional probability of $A$ given $B$ is $P(A|B) = \frac{P(A \cap B)}{P(B)} = \frac{P(A, B)}{P(B)}$.
- The joint probability of $A$ and $B$ is $P(A \cap B) = P(A, B) = P(B|A)P(A) = P(A|B)P(B)$.
- The marginalization of discrete variables is $P(A) = \sum P(A, B) = \sum P(A|B)P(B)$ where the summation is over the possible values of $B$.

### Markov Chain

The **Markov chain** is a stochastic model describing a sequence of possible events in which the probability of each event depends on the previous state. The transition matrix $T$ of **Markov chain** is a matrix in which $T_{i, j} = P(\text{state}_{i + 1} = i|\text{state}_i = j)$.

$$P(\text{state}_{i + 1} = 1) = P(\text{state}_{i + 1} = 1|\text{state}_i = 1) + P(\text{state}_{i + 1} = 1|\text{state}_i = 2) + \dots + P(\text{state}_{i + 1} = 1|\text{state}_i = n)$$

Let $P_i$ be a vector of the probabilities of the current state and $T$ be the transition matrix, $P_{i + 1} = P_i T$, and $P_{i + j} = P_i T^j$.

### Likelihood

With $n$ data points, a model could be evaluated by calculating the **likelihood** of the model having generated all of the data points $x_i$. The likelihood function of the whole data set is defined as $L(\mu, \sigma) = P(\bar{x}|\mu, \sigma) = \Pi^{n}_{i = 1} N(x_i, \mu, \sigma)$. Statistical inference is the process of finding the parameters $\mu$ and $\sigma$ that are most likely to generate the given sample data set. The maximum likelihood estimate is $(\hat{\mu}, \hat{\sigma}) = \text{argmax}_{\mu, \sigma} L(\mu, \sigma) = \text{argmax}_{\mu, \sigma} \Pi^{n}_{i = 1} N(x_i, \mu, \sigma)$.

### Bayes' Theorem

$P(A|B) = \frac{P(B|A)P(A)}{P(B)}$ where $A$ and $B$ are events and $P(B) \neq 0$.
