# Interval Estimation

## Confidence Interval for Mean

Let $X_1, \dots, X_n$ be samples drawn from a distribution $X$ with an unknown mean $\mu$ and a known variance $\sigma^2$, where $\bar{X}$ is an unbiased estimator of $\mu$. The confidence interval for $\mu$ is the interval $[\bar{X} - \epsilon, \bar{X} + \epsilon]$ such that $\mu$ is contained in the interval with a probability of at least $1 - \alpha$.

Let $X \sim N(\mu, \sigma^2)$, then $\bar{X} = \frac{1}{n} \sum_{i = 1}^{n} X_i \sim N(\mu, \frac{\sigma^2}{n})$, since $\text{Var}(\bar{x}) = \text{Var}(\frac{1}{n} \sum_{i = 1}^{n} X_i) = \frac{1}{n^2} \text{Var}(\sum_{i = 1}^{n} X_i) = \frac{1}{n} \text{Var}(X) = \frac{\sigma^2}{n}$. Furthermore, $\frac{\bar{X} - \mu}{\frac{\sigma}{\sqrt{n}}} \sim N(0, 1)$.

Since $P(-z_{\frac{\alpha}{2}} \le \frac{\bar{X} - \mu}{\frac{\sigma}{\sqrt{n}}} \le z_{\frac{\alpha}{2}}) = 1 - \alpha$, which is equivalent to $P(\mu \in [\bar{X} - z_{\frac{\alpha}{2}} (\frac{\sigma}{\sqrt{n}}), \bar{X} + z_{\frac{\alpha}{2}} (\frac{\sigma}{\sqrt{n}})]) = 1 - \alpha$, then $\bar{X} \pm z_{\frac{\alpha}{2}} (\frac{\sigma}{\sqrt{n}})$ is the $1 - \alpha$ confidence interval for $\mu$. For example, $\bar{X} \pm 1.96 \frac{\sigma}{\sqrt{n}}$ is the $0.95$ confidence interval.

### Unknown Distribution

If the distribution is not normal, the central limit theorem implies that the ratio $\frac{\bar{X} - \mu}{\frac{\sigma}{\sqrt{n}}}$ has the approximate normal distribution $N(0, 1)$ when $n$ is large enough. Therefore, $P(\mu \in [\bar{X} - z_{\frac{\alpha}{2}} (\frac{\sigma}{\sqrt{n}}), \bar{X} + z_{\frac{\alpha}{2}} (\frac{\sigma}{\sqrt{n}})]) \approx 1 - \alpha$.

### Unknown Variance

If the variance $\sigma^2$ is unknown, the sample variance $S^2 = \frac{1}{n - 1} \sum_{i = 1}^{n} (X_i - \bar{X})^2$ can be used as its estimation since $S^2$ is an unbiased estimator of $\sigma^2$.

- If the sample size is large enough (e.g., greater than $30$), $\frac{\bar{X} - \mu}{\frac{S}{\sqrt{n}}} \sim N(0, 1)$.
- If the sample size is small, $\frac{\bar{X} - \mu}{\frac{S}{\sqrt{n}}} \sim t(n - 1)$, which is a t-distribution with $n - 1$ degree of freedom. The confidence interval is $\bar{X} \pm t_{\frac{\alpha}{2}} (\frac{\sigma}{\sqrt{n}})$, which is the $1 - \alpha$ confidence interval for $\mu$.

## Confidence Interval of the Differentce of Two Means

Let $X_1, \dots, X_n$ be samples drawn from a distribution $X \sim N(\mu_x, \sigma_x^2)$ and $Y_1, \dots, Y_n$ be samples drawn from a distribution $Y \sim N(\mu_y, \sigma_y^2)$, where $\mu_x$ and $\mu_y$ are unknown, and $\bar{X}$ and $\bar{Y}$ are unbiased estimators of $\mu_x$ and $\mu_y$. The confidence interval for $\mu_x - \mu_y$ is the interval $[\bar{X} - \bar{Y} - \epsilon, \bar{X} - \bar{Y} + \epsilon]$ such that $\mu_x - \mu_y$ is contained in the interval with a probability of at least $1 - \alpha$.

- Let $X \sim N(\mu_x, \sigma_x^2)$, then $\bar{X} = \frac{1}{n_x} \sum_{i = 1}^{n_x} X_i \sim N(\mu_x, \frac{\sigma_x^2}{n_x})$.
- Let $Y \sim N(\mu_y, \sigma_y^2)$, then $\bar{Y} = \frac{1}{n_y} \sum_{i = 1}^{n_y} Y_i \sim N(\mu_y, \frac{\sigma_y^2}{n_y})$.
- Therefore, $W = \bar{X} - \bar{Y} \sim N(\mu_x - \mu_y, \frac{\sigma^2_x}{n_x} + \frac{\sigma^2_y}{n_y})$.

Since $P(-z_{\frac{\alpha}{2}} \le \frac{\bar{X} - \bar{Y} - (\mu_x - \mu_y)}{\sqrt{\frac{\sigma_x^2}{n_x} + \frac{\sigma_y^2}{n_y}}} \le z_{\frac{\alpha}{2}}) = 1 - \alpha$, which is equivalent to $P(\mu_x - \mu_y \in [\bar{X} - \bar{Y} - z_{\frac{\alpha}{2}} \sqrt{\frac{\sigma_x^2}{n_x} + \frac{\sigma_y^2}{n_y}}, \bar{X} + z_{\frac{\alpha}{2}} \sqrt{\frac{\sigma_x^2}{n_x} + \frac{\sigma_y^2}{n_y}}]) = 1 - \alpha$, then $\bar{X} \pm z_{\frac{\alpha}{2}} \sqrt{\frac{\sigma_x^2}{n_x} + \frac{\sigma_y^2}{n_y}}$ is the $1 - \alpha$ confidence interval for $\mu_X - \mu_y$.

### Unknown Distribution

If the distribution is not normal, the central limit theorem implies that the ratio $\frac{\bar{X} - \bar{Y} - (\mu_x - \mu_y)}{\sqrt{\frac{\sigma_x^2}{n_x} + \frac{\sigma_y^2}{n_y}}}$ has the approximate normal distribution $N(0, 1)$ when $n$ is large enough. Therefore, $P(\mu_x - \mu_y \in [\bar{X} - \bar{Y} - z_{\frac{\alpha}{2}} \sqrt{\frac{\sigma_x^2}{n_x} + \frac{\sigma_y^2}{n_y}}, \bar{X} + z_{\frac{\alpha}{2}} \sqrt{\frac{\sigma_x^2}{n_x} + \frac{\sigma_y^2}{n_y}}]) \approx 1 - \alpha$.

### Unknown Variance

If the variances $\sigma_x^2$ and $\sigma_y^2$ are unknown, the sample variances $S_x^2 = \frac{1}{n_x - 1} \sum_{i = 1}^{n_x} (X_i - \bar{X})^2$ and $S_y^2 = \frac{1}{n_y - 1} \sum_{i = 1}^{n_y} (Y_i - \bar{Y})^2$ can be used as its estimation since $S_x^2$ and $S_y^2$ are an unbiased estimators of $\sigma_x^2$ and $\sigma_y^2$.

- If the sample size is large enough, $\frac{\bar{X} - \bar{Y} - (\mu_x - \mu_y)}{\sqrt{\frac{\sigma_x^2}{n_x} + \frac{\sigma_y^2}{n_y}}} \sim N(0, 1)$.
- If the sample size is small, the confidence interval is $(\bar{X} - \bar{Y} \pm t_0 S_p \sqrt{\frac{1}{n_x} + \frac{1}{n_y}})$, where $S_p = \sqrt{\frac{(n_x - 1) S_x^2 + (n_y - 1) S_y^2}{n_x + n_y - 2}}$. When $\sigma_x = \sigma_y$, the degree of freedom is $n_x + n_y - 2$.

## Confidence Interval for Proportion

Let $X_1, \dots, X_n$ be samples drawn from a distribution $X$ and $E_k$ be the event that $X_k \in [a, b]$, where $P(X_k \in [a, b]) = p$. Let $Y \sim b(n, p)$ be the number of measurements in $[a, b]$ out of the $n$ observations, which is an unbiased estimator of $p$. The confidence interval for $p$ is the interval $[\frac{Y}{n} - \epsilon, \frac{Y}{n} + \epsilon]$ such that $p$ is contained in the interval with a probability of at least $1 - \alpha$.

Let $\frac{Y}{n} \sim N(p, \frac{p(1 - p)}{n})$, which implies that $\frac{\frac{Y}{n} - p}{\sqrt{\frac{p(1 - p)}{n}}} \sim N(0, 1)$. Since $P(-z_{\frac{\alpha}{2}} \le \frac{\frac{Y}{n} - p}{\sqrt{\frac{p(1 - p)}{n}}} \le z_{\frac{\alpha}{2}}) \approx 1 - \alpha$, which is equivalent to $P(p \in [\frac{Y}{n} - z_{\frac{\alpha}{2}} \sqrt{\frac{p(1 - p)}{n}}, \frac{Y}{n} + z_{\frac{\alpha}{2}} \sqrt{\frac{p(1 - p)}{n}}]) = 1 - \alpha$, then $\frac{Y}{n} \pm z_{\frac{\alpha}{2}} \sqrt{\frac{p(1 - p)}{n}}$ is the $1 - \alpha$ confidence interval for $p$.

However, the unknown value $p$ appears in the confidence interval, thus it can be replaced as its unbiased estimator $\frac{Y}{n}$, which results in $\frac{Y}{n} \pm z_{\frac{\alpha}{2}} \sqrt{\frac{\frac{Y}{n}(1 - \frac{Y}{n})}{n}}$.
