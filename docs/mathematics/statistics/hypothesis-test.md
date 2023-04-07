# Hypothesis Test

## Hypothesis Test of Mean

Let $X \sim N(\mu, \sigma^2)$, where $\mu$ is unknown. Let $H_0: \mu = \mu_1$ be a simple null hypothesis and $H_1 \mu = \mu_2$ be a simple alternative hypothesis. Let $C$ be a partition of the sample space, which is the critical region for the test.

- If $(x_1, \dots, x_n) \in C$, $H_0$ is rejected.
- If $(x_1, \dots, x_n) \not \in C$, $H_0$ is accepted.

The partitioning of the sample space is specified in terms of a test statistic, such as $\bar{X}$, which implies that $C = \{ (x_1, \dots, x_n): \bar{x} \ge \mu_\alpha \}$, thus $H_0$ will be rejected if $\bar{x} \ge \mu_\alpha$.

- Type I error: If $(x_1, \dots, x_n) \in C$ when $H_0$ is true, $H_0$ would be rejected when it's true. Let $\alpha = P(\text{Type I error}) = P[(X_1, \dots, X_n) \in C; H_0]$, which is the significance level of the test.
- Type II error: If $(x_1, \dots, x_n) \not \in C$ when $H_0$ is false, $H_0$ would be accepted when it's false. Let $\beta = P(\text{Type II error}) = P[(X_1, \dots, X_n) \not \in C; H_1]$.

The p-value is the probability of obtaining a test statistic at least as extreme as the observed sample, assuming the $H_0$ is true. If the p-value of a sample is small, $H_0$ might not be true. $H_0$ will be rejected if the p-value of the sample is less than $\alpha$, the significance level of the test.

- $H_0: \mu = \mu_0, H_1: \mu > \mu_0, p = P(\bar{X} \ge \bar{x}; H_0), C = \{ \bar{X}: \bar{X} > \mu_0 + z_\alpha \frac{\sigma}{\sqrt{n}} \}$
- $H_0: \mu = \mu_0, H_1: \mu < \mu_0, p = P(\bar{X} \le \bar{x}; H_0), C = \{ \bar{X}: \bar{X} < \mu_0 - z_\alpha \frac{\sigma}{\sqrt{n}} \}$
- $H_0: \mu = \mu_0, H_1: \mu \ne \mu_0, p = P(|\bar{X} - \mu_0| \ge |\bar{x} - \mu_0|; H_0), C = \{ \bar{X}: |\bar{X} - \mu_0| > z_{\frac{\alpha}{2}} \frac{\sigma}{\sqrt{n}} \}$

### Unknown Variance

- $H_0: \mu = \mu_0, H_1: \mu > \mu_0, p = P(\bar{X} \ge \bar{x}; H_0), C = \{ \bar{X}: \bar{X} > \mu_0 + t_\alpha(n - 1) \frac{\sigma}{\sqrt{n}} \}$
- $H_0: \mu = \mu_0, H_1: \mu < \mu_0, p = P(\bar{X} \le \bar{x}; H_0), C = \{ \bar{X}: \bar{X} < \mu_0 - t_\alpha(n - 1) \frac{\sigma}{\sqrt{n}} \}$
- $H_0: \mu = \mu_0, H_1: \mu \ne \mu_0, p = P(|\bar{X} - \mu_0| \ge |\bar{x} - \mu_0|; H_0), C = \{ \bar{X}: |\bar{X} - \mu_0| > t_{\frac{\alpha}{2}}(n - 1) \frac{\sigma}{\sqrt{n}} \}$
