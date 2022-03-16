# Quantifying Uncertainty

The agents need to handle uncertainty due to partial observability and non-determinism. Problem-solving agents and logical agents handle uncertainty by tracking a **belief state** which could grow infinitely. To solve the **qualification problem**, the principle of **maximum expected utility** states that an agent is rational if and only if it chooses the action that yields the highest expected utility averaged over all the possible outcomes of the action.

## Probabilistic Notation

The **sample space** $\Omega$ contains sample points or atomic events $\omega$. The probability space is a sample space with an assignment $P(\omega)$ for each $\omega \in \Omega$, where $\sum_{\omega} P(\omega) = 1$. The **event** is a set of atomic events and could be represented by a proposition. For the proposition $\phi$, $P(\phi) = \sum_{\{\omega \in \phi\}} P(\omega)$.

**Unconditional probability** correspond to belief in propositions in the absence of other information. (e.g. $P(X = true) = 0.1$) Probability distribution gives values for all possible assignments. $P(X) = (0.1, 0.2, 0.3, 0.4)$. The joint probability distribution for a set of random variables gives the probability of every atomic event on those random variables. $P(A, B)$ denotes the probabilities of the combinations of the values of $A$ and $B$.

**Conditional probability** is defined as $P(a|b) = \frac{P(a \land b)}{P(b)}$ if $P(b) \neq 0$. If $b$ is true, then The set of total probability is $P(b)$, and the fraction in the set that satisfies $a \land b$ is $\frac{P(a \land b)}{P(b)}$.

- **Product rule**: $P(a \land b) = P(a|b)P(b) = P(b|a)P(a)$
- **Chain rule**: $P(X_1, \dots, X_n) = P(X_1, \dots, X_{n - 1}) P(X_n|X_1, \dots, X_{n - 1}) = \sum^{n}_{i = 1} P(X_i|X_1, \dots, X_{i - 1})$

The **random variable** is a function from sample points to some range. For a random variable $X$, $\vec{P}$ defines a probability distribution that $\vec{P}(X = x_i) = \sum_{\{\omega:X(\omega) = x_i\}} P(\omega)$. For joint probability distribution on multiple variables, $\vec{P}(a, b)$ denotes the probabilities of all combinations of the values of $a$ and $b$.

The possible world is defined to be an assignment of values to all of the random variables under consideration. The joint distribution of all of the random variables is the **full joint probability distribution**.

## Probabilistic Inference

The probabilistic inference is the computation of posterior probabilities for query proposition given observed evidence. The full joint distribution is the knowledge base. Let $E$ be the list of evidence variables, $e$ be the list of observed values for them, $Y$ be the remaining unobserved variables, and $\alpha$ be the normalization constant. $P(X|e) = \alpha P(X,e) = \alpha \sum_{y} P(X, e, y)$.

- The **marginalization** rule sums up the probabilities for each possible value of the other variables. For variables $Y$ and $Z$, $\vec{P}(Y) = \sum_{z \in Z} \vec{P}(Y, z)$, where $\sum_{z \in Z}$ means to sum all possible combinations of values of the set of variables $Z$.
- The **conditioning** rule calculates the probabilities with the product rule. For variables $Y$ and $Z$, $\vec{P}(Y) = \sum_{z} \vec{P}(Y|z)P(z)$.

## Independence

- The **absolute independence** between propositions $a$ and $b$ could be represented as $P(A|B) = P(A)$ or $P(B|A) = P(B)$ or $P(A, B) = P(A)P(B)$. If the variables could be divided into independent subsets, then the full joint distribution could be factored into separate joint distribution on the subsets.
- The **conditional independence** could be represented as $P(a|b, c) = P(a, c)$ or $P(a, b|c) = P(a|c)P(b|c)$. The use of conditional independence reduces the size of the representation of the full joint distribution from exponential in $n$ to linear in $n$.

## Bayes' Rule

The Bayes' rule is defined as $P(b|a) = \frac{P(a|b)P(b)}{P(a)}$. The rule is useful for assessing diagnostic probability from causal probability, which is represented as $P(\text{cause}|\text{effect}) = \frac{P(\text{effect}|\text{cause})P(\text{cause})}{P(\text{effect})}$. The **naive bayes model** is defined as $P(\text{cause}, \text{effect}_1, \dots, \text{effect}_n) = P(\text{cause})\prod_{i} P(\text{effect}_i|\text{cause})$.
