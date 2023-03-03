# Point Estimation

## Descriptive Statistics

The sample is a collection of results of a certain random experiment that is performed $n$ times, which is denoted as $x_1, x_2, \dots, x_n$. The empirical distribution $f$ is the distribution on $\{ x_1, x_2, \dots, x_n \}$ such that $f(x_i) = \frac{1}{n} \times$ the number of times $x_i$ appeared in the sample.

- The sample mean is $\bar{x} = \frac{1}{n} \sum_{i = 1}^{n} x_i$.
- The variance of the empirical distribution is $v = \frac{1}{n} \sum_{i = 1}^{n} (x_i - \bar{x})^2$.
- The sample variance is $s^2 = \frac{1}{n - 1} \sum_{i = 1}^{n} (x_i - \bar{x})^2$.
- The sample standard deviation is $s = \sqrt{s^2} \ge 0$, which is a measure of how dispersed the data are from the sample mean.

If $X$ is a continuous random variable, each $x_i$ might appear once. In this case, $f(x_i) = \frac{1}{n}$ is a uniform distribution, which doesn't provide a meaningful information on the pdf or distribution of $X$.

The relative frequency histogram could estimate the pmf or pdf of a continuous random variable. Given a set of continuous sample data, the data could be grouped into classes, which could be used to construct a hisstogram of the grouped data. Suppose the data is grouped into $k$ classes, then the class intervals could be denoted as $(c_0, c_1], (c_1, c_2], \dots, (c_{k - 1}, c_k]$.

- The class boundaries are the limits of the class interval.
- The class limits are the smallest and the largest possible observed values in a class.
- The class mark is the midpoint of a class.
- The class mode is the interval with the largest height, and the respective class mark is called the mode.

The empirical rule states that if the histogram of a sample data, with a sample mean of $\bar{x}$ and a sample standard deviation $s$, is bell-shaped, then for large samples, $68\%$ of the data are in the interval $(\bar{x} - s, \bar{x} + s)$, $95\%$ of the data are in the interval $(\bar{x} - 2s, \bar{x} + 2s)$, and $99.7\%$ of the data are in the interval $(\bar{x} - 3s, \bar{x} + 3s)$.

## Data Analysis

Ordered stem-and-leaf display is a data visualization method, which preserves all the exact values of data points. The ordered stem-and-leaf display helps in the data
visualization/indexing in two folds. The stems are in the first column and the leaves are stored in the second column. Stems and leaves are sorted in increasing order, and the number of data point for each stem is recorded.

Let $X$ be a sample that contains $x_1, \dots, x_n$. Consider reordering the samples from the smallest to the largest. Let $y_k$ be the $k$-th order statistic of the sample, which is the $k$-th smallest value.

For $0 < p < 1$, the sample percentiles is the $(n + 1)p$-th order statistic if $(n + 1)p$ is an integer. If $(n + 1)p$ is a proper fraction, then the $(100p)$-th percentile is the weighted average of the $r$-th and $(r + 1)$-th order statistics. $y_r + [(n + 1)p - r](y_{r+1} - y_r)$. The $100p$-th sample percentile has $np$ samples less than it and $n(1-p)$ samples greater than it. The $(100p)$-th percentile is denoted as $\tilde{\pi}_p$.

- The median is the $50$-th percentile.
- The first, second, and third quartiles are the $25$-th, $50$-th, and $75$-th percentiles.
- The deciles of the sample are the $10$-th, $20$-th, ..., and $90$-th percentiles.
- The five-number summary of a data set are the minimum, the first quartile, the median, the third quartile, and the maximum.
- The interquartile range (IQR) is the difference between the third and first quartiles.

## Order Statistics

Order statistics are the observations of the random sample ordered in magnitude from the smallest to the largest. Let $X_1, X_2, \dots, X_n$ be observations of a random sample of size $n$ from a continuous distribution. The random variables $Y_1 < Y_2 < \dots < Y_n$ denote the order statistics of the sample, in which $Y_n$ is the $n$-th smallest of $X_1, X_2, \dots, X_n$.

Let $Y_1 < Y_2 < \dots < Y_n$ be the order statistics of $n$ independent observations from a distribution of the continuous type with cdf $F(x)$ and pdf $F'(x) = f(x)$, where $0 < F(x) < 1$ for $a < x < b$ and $F(a) = 0$, $F(b) = 1$. The event that the $r$-th order statistics $Y_r$ is at most $y$, $\{ Y_r \le y \}$, occurs if at least $r$ of the $n$ observations are less than or equal to $y$.

- The probability of drawing an observation that is less than or equal to $y$ is $F(y)$. To have at least $r$ successes, $G_r(y) = P(Y_r \le y) = \sum_{k = r}^{n} {n \choose k} [F(y)]^k [1 - F(y)]^{n - k}$.
- The pdf of $Y_r$ is $g_r (y) = G'_r(y) = \frac{n!}{(r - 1)!(n - r)!} [F(y)]^{r - 1} [1 - F(y)]^{n - r} f(y), a < y < b$.
- The pdf of the smallest order statistic is $g_1(y) = n[1 - F(y)]^{n - 1} f(y)$.
- The pdf of the largest order statistic is $g_n(y) = n[F(y)]^{n - 1} f(y)$.

Let $F(x) = P(X < x)$ be the cdf of $X$. If $Y_1 < Y_2 < \dots < Y_n$ are the order statistics of $n$ independent observations $X_1, X_2, \dots, X_n$, then $F(Y_1) < F(Y_2) < \dots < F(Y_n)$, because $F$ is a non-decreasing function.

Let $F(x)$ be the cdf of $X$, then the random variable $F(X)$ has a uniform distribution on the interval from $0$ to $1$, because the CDF of a continuous random variable maps the variable to a value between $0$ and $1$, with each value between $0$ and $1$ being possible. Let $W_1 = F(Y_1) < W_2 = F(Y_2) < \dots < W_n = F(Y_n)$, which is the order statistics of $n$ independent observations from $U(0, 1)$.

- The cdf of $U(0, 1)$ is $G(w) = w$
- The pdf of the $r$-th order statistic, $W_r = F(Y_r)$ is $h_r(w) = \frac{n!}{(r - 1)!(n - r)!} w^{r - 1} (1 - w)^{n - r}$.
- The mean $E(W_r) = E[F(Y_r)]$ is $E(W_r) = \int_0^1 w \frac{n!}{(r - 1)!(n - r)!} w^{r - 1} (1 - w)^{n - r} dw = \frac{r}{n + 1}$.
- The expected value of the random area between adjacent order statistics is $E[F(Y_r) - F(Y_{r - 1})] = E[F(Y_r)] - E[F(Y_{r - 1})] = \frac{1}{n + 1}$.

Therefore, the order statistics $Y_1 < Y_2 < \dots < Y_n$ partition the domain of $X$ info $n + 1$ parts, which has the area equals $\frac{1}{n + 1}$.

## Maximum Likelihood Estimation

Consider a randdom variable for which the functional form of the pmf or pdf is known, but the distribution depends on an unknown parameter $\theta$ that has an value in a set $\Omega$ called the parameter space. The experimenter needs a point estimate of the parameter $\theta$.

The first step is to repeat the experiment $n$ independent times, observe the sample $X_1 \dots X_n$, and estimate the value of $\theta$ using the observations $x_1, \dots x_n$. The function $u(X_1 \dots X_n)$ is an estimator of $\theta$, and the value of $u(x_1, \dots x_n)$ should close to $\theta$. Since $u$ is estimating a member of $\theta \in \Omega$, the estimator is a point estimator.

### Definition

Let $X_1, \dots, X_n$ be a random sample from a distribution that depends on one or more unknown parameters $\theta_1, \dots, \theta_m$ with pmf or pdf that is denoted as $f(x; \theta_1, \dots, \theta_m)$. The joint pmf or pdf of $X_1, \dots, X_n$ is $L(\theta_1, \dots, \theta_m) = f(x_1; \theta_1, \dots, \theta_m) f(x_2; \theta_1, \dots, \theta_m) \dots f(x_n; \theta_1, \dots, \theta_m)$, which is the likelihood function.

Let the $m$-tuple in $\Omega$ that maximzes $L$ be $[u_1(x_1, \dots, x_n), \dots, u_m(x_1, \dots, x_n)]$. The $\hat{\theta_1} = u_1(X_1, \dots, X_n), \dots, \hat{\theta_m} = u_m(X_1, \dots, X_n)$ are maximum likelihood estimators of $\theta_1, \dots, \theta_m$. The observaed values of these statistics $u_1(x_1, \dots, x_n), \dots, u_m(x_1, \dots, x_n)$ are maximum likelihood estimates.

If $E[u(X_1, \dots, X_n)] = \theta$, then the statistics $u(X_1, \dots, X_n)$ is an unbiased estimator of $\theta$. Otherwise, it's biased.

### Example

Let $X$ be $b(1, p)$, thus the pmf of $X$ is $f(x; p) = p^x (1 - p)^{1 - x}, x = 0, 1$. The parameter $p \in \Omega$. Given a random sample $X_1, \dots X_n$, the problem is to find an estimator $u(X_1, \dots, X_n)$ such that $u(x_1, \dots, x_n)$ is a good point estimate of $p$.

The probability of drawing these samples is $P(X_1 = x_1, \dots, X_n = x_n) = \Pi_{i = 1}^n p^{x_i} (1 - p)^{1 - x_i} = p^{\sum x_i} (1 - p)^{n - \sum x_i}$. The joint pmf, when regarded as a function of $p$, is the likelihood function. $L(p) = L(p; x_1, \dots, x_n) = p^{\sum x_i} (1 - p)^{n - \sum x_i}$.

- If $\sum x_i = 0$, $L(p) = (1 - p)^n$, which has a maximum at $\hat{p} = 0$.
- If $\sum x_i = 1$, $L(p) = p^n$, which has a maximum at $\hat{p} = 0$.
- Find the derivative of $L(p)$, which equals to $0$ when $p = \bar{x}$.

Therefore, the maximum likelihood estimator is $\hat{p} = \bar{X}$.

## Linear Regression

Let $Y$ be a random variable. The problem is to predict the mean of $Y$ given a particular $x$ ($E(Y|x)$). $E(Y|x) = \mu(x)$ is assumed to be of a given form, such as linear, quadratic, or exponential. To estimate $E(Y|x) = \mu(x)$, observe the random variable $Y$ for each of $n$ different values of $x$, and use the results $(x_1, y_1), \dots, (x_n, y_n)$ to estimate the regression.

Assume that $Y_i = \alpha_1 + \beta x_i + \epsilon_i$, where $\epsilon_i \in N(0, \sigma^2)$, which is the difference from the mean.

To find the maximum likelihood estimates, for $\alpha_1, \beta, \sigma^2$. Let $\alpha_1 = \alpha - \beta \bar{x}$, where $\bar{x} = \frac{1}{n} \sum_{i = 1}^{n} x_i$.

The likelihoood function is $L(\alpha, \beta, \sigma^2) = \Pi_{i = 1}^{n} \frac{1}{\sqrt{2\pi \sigma^2}} \exp{-\frac{[y_i - \alpha - \beta(x_i, \bar{x})]^2}{2 \sigma^2}}$. Maximize $L(\alpha, \beta, \sigma^2)$ is equivalent to minimize $-\ln L(\alpha, \beta, \sigma^2) = \frac{n}{2} \ln(2 \pi \sigma^2) + \frac{\sum_{i = 1}^{n} [y_i - \alpha - \beta(x_i - \bar{x})]^2}{2 \sigma^2}$. Therefore, $H(\alpha, \beta) = \sum_{i = 1}^{n} [y_i - \alpha - \beta(x_i - \bar{x})]^2$ should be maximized.

$y_i - \alpha - \beta(x_i - \bar{x})$ is the vertical distance from the point $(x_i, y_i)$ to the line $y = \mu(x)$, thus $H(\alpha, \beta)$ represents the sum of the squares of those distances. Therefore, the least squares estimate is the maximum likelihood estimate of $\alpha$ and $\beta$.

- $\hat{\alpha} = \bar{y}$
- $\hat{\beta} = \frac{\sum y_i(x_i - \bar{x})}{\sum (x_i - \bar{x})^2}$
- $\hat{\sigma^2} = \frac{1}{n} \sum [y_i - \hat{\alpha} - \hat{\beta}(x_i - \bar{x})]^2$

## Sufficient Statistics

The statistic is said to be sufficient if it captures all the information in the data about the unknown parameter, so that all inferences made about the parameter can be based on the value of the sufficient statistic, without reference to the actual data. The sufficient statistics is a function of the maximum likelihood estimator of an unknown parameter.

### Factorization Theorem

Let $X_1, \dots, X_n$ denote random variables with joint pdf $f(x_1, x_2, \dots, x_n; \theta) = \Pi_{i = 1}^{n} f(x_i; \theta)$, which depends on the parameter $\theta$. The statistics $Y = u(X_1, X_2, \dots, X_n)$ is sufficient for $\theta$ if $f(x_1, \dots, x_n; \theta) = \phi[u(x_1, \dots, x_n); \theta]h(x_1, \dots, x_n)$, where $\phi$ depends on $u$ and $h$ doesn't depend on $\theta$.

For example, the joint pdf of a Poisson distribution is $\Pi_{i = 1}^{n} f(x_i; \theta) = \frac{\lambda^{\sum x_i} e^{-n\lambda}}{x_{1}!x_{2}! \dots x_{n}!}$ = $(\lambda^{n \bar{x}} e^{-n\lambda})(\frac{1}{x_{1}!x_{2}! \dots x_{n}!})$. Therefore, $\bar{x}$ is a sufficient statistic for $\lambda$, which is the maximum likelihood estimator.

### Invertible Function

If $Y$ is sufficient for a parameter $\theta$, then all single-valued invertible functions of $Y$ not involving $\theta$ is sufficient for $\theta$. For example, if $W = v(Y)$ and $Y = v^{-1}(W)$ is unique, then the factorization theorem is $f(x_1, \dots X_n; \theta) = \phi[v^{-1}{v[u(x_1, \dots, x_n)]}; \theta]h(x_1, \dots, x_n)$.

### Exponential Form

Let $X_1, \dots X_n$ be a random sample from a distribution with a pdf of the exponential form $f(x; \theta) = \exp[K(x)p(\theta) + S(x) + q(\theta)]$ on a support free of $\theta$. The statistic $\sum_{i = 1}^{n} K(X_i)$ is sufficient for $\theta$.

For example, the pdf of a distribution is $f(x; \theta) = \frac{1}{\theta} e^{\frac{-x}{\theta}} = \exp[x(-\frac{1}{\theta}) - \ln \theta]$. Since $K(x) = x$, then $\sum_{i = 1}^{n} X_i$ is sufficient for $\theta$.

### Rao-Blackwell Theorem

Let $X_1, X_2, \dots, X_n$ be a random sample from a distribution with pdf or pmf $f(x; \theta), \theta \in \Omega$. Let $Y_1 = u_1 (X_1, X_2, \dots, X_n)$ be a sufficient statistic for $\theta$ and let $Y_2 = u_2 (X_1, X_2, \dots, X_n)$ be an unbiased estimator of $\theta$, where $Y_2$ is not a function of $Y_1$ alone.

- $E(Y_2|Y_1)$ defines a statistic $Y_3$, a function of the sufficient statistic $Y_1$, which is an unbiased estimator of $\theta$, and its variance is less than that of $Y_2$.
- If $Y_1$ and $Y_2$ are unbiased, then $Y_3$ is unbiased.

## Bayesian Estimation

Let $P_\Theta(\theta)$ be a distribution on $\theta$, which is the prior pmf or pdf. Let $P(\Theta = \theta|X_1 = x_1, \dots, X_n = x_n)$ be the posterior pmf or pdf, then the most probable $\theta$ is the one that maximizes the posterior pmf or pdf.

Based on the Bayes' theorem, the posterior pmf of $\theta$ is $P(\Theta = \theta|X_1 = x_1, \dots, X_n = x_n) = \frac{P(X_1 = x_1, \dots, X_n = x_n | \Theta = \theta) P_\Theta (\theta)}{P(X_1 = x_1, \dots, X_n = x_n)} = \frac{P(X_1 = x_1, \dots, X_n = x_n | \Theta = \theta) P_\Theta (\theta)}{\sum P(X_1 = x_1, \dots, X_n = x_n | \Theta = \hat{\theta}) P_\Theta(\hat{\theta})}$ for all possible $\hat{\theta}$.

### Error Function

The estimator $\hat{\theta}$ should minimizes the squared loss function, which is represented as $E[(\theta - u(X_1, \dots, X_n))^2 | X_1, \dots, X_n] = \sum_\theta (\theta - u(X_1, \dots, X_n))^2 P_\Theta (\theta | X_1, \dots, X_n)$. $E[\theta | X_1, \dots, X_n]$ is the minimium of the squared loss function.

- Discrete $\Theta$: $E[\theta | X_1, \dots, X_n] = \sum_\Theta \theta P_\Theta (\theta | X_1, \dots, X_n)$
- Continuous $\Theta$: $E[\theta | X_1, \dots, X_n] = \int \theta f_\Theta (\theta | X_1, \dots, X_n) d\theta$
