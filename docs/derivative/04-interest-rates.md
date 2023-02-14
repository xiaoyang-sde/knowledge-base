# Interest Rates

## Types of Rates

An interest rate in a particular situation defines the amount of money a borrower promises to pay the lender. One important factor influencing interest rates is credit risk. The extra amount added to a risk-free interest rate to allow for credit risk is known as a credit spread.

- Treasury rates: Treasury rates are the rates an investor earns on Treasury bills and Treasury bonds. These are the instruments used by a government to borrow in its own currency. It is assumed that there is no chance that the government of a developed country will default on an obligation denominated in its own currency.
- Overnight Rates: Banks are required to maintain a certain amount of cash, known as a reserve, with the central bank. The reserve requirement for a bank depends on its outstanding assets and liabilities. Some financial institutions have surplus funds in their accounts with the central bank while others have requirements for funds. This leads to borrowing and lending overnight. Federal Reserve sets the federal funds rate.
- Repo Rates: Repo rates are secured borrowing rates. In a repurchase agreement, a financial institution that owns securities agrees to sell the securities and buy them back later for a higher price. The financial institution is obtaining a loan and the interest it pays is the difference between the sold price and repurchased price. The interest rate is the repo rate.

## Reference Rates

The reference interest rate is an interest rate benchmark used to set other interest rate.

LIBOR (London Interbank Offered Rate) rates is the average interest rate at which major global banks borrow from one another. It's a combination of 5 currencies and 7 maturities. The most quoted rate is the three-month U.S. dollar rate. LIBOR incorporates a credit spread. LIBOR will be discontinued in 2023.

The plan is to base reference rates on the overnight rates. The new reference rate in the U.S. is SOFR. Longer rates such as three-month rates can be determined from overnight rates.

Assume that there are 360 days per year, and the SOFR overnight rate on the $i$-th business day is $r_i$ and the rate applies to $d_i$ days. The interest rate for the period is $[(1 + r_1 \hat{d_1})(1 + r_2 \hat{d_2}) \dots (1 + r_n \hat{d_n} - 1)] \times \frac{360}{D}$, where $\hat{d_i} = \frac{d_i}{360}$. The new reference rates are regarded as risk-free.

## The Risk-Free Rate

The risk-free rate is a central role in derivatives pricing. The risk-free reference rates created from from overnight rates are the ones used in valuing derivatives.

## Measuring Interest Rates

Suppose that an amount $A$ is invested for $n$ years at an interest rate of $R$ per annum. If the rate is compounded once per annum, the terminal value of the investment is $A(1 + R)^n$. If the rate is compounded $m$ times per annum, the terminal value of the investment is $A(1 + \frac{R}{m})^{mn}$.

The limit as the compounding frequency $m$ tends to infinity is known as continuous compounding. With continuous compounding, it can be shown that an amount $A$ invested for $n$ years at rate $R$ grows to $Ae^{Rn}$.

Suppose that $R_c$ is a rate of interest with continuous compounding and $R_m$ is the equivalent rate with compounding $m$ times per annum. Therefore, $Ae^{R_{c}n} = A(1 + \frac{R_m}{m})^{mn}$ or $R_{c} = m \ln(1 + \frac{R_m}{m})$.
