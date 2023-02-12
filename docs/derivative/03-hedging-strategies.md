# Hedging Strategies

## Basic Principles

When an individual chooses to use futures markets to hedge a risk, the objective is to take a position that neutralizes the risk as far as possible.

### Short Hedges

A short hedge is a hedge that involves a short position in futures contracts. A short hedge is appropriate when the hedger owns or expects to own an asset and expects to sell it at some time in the future. It has the effect of offsetting the hedger's risk.

Assume that an oil producer negotiates a contract to sell 1 million barrels of crude oil. The producer will gain $10,000 for each 1 cent increase in the oil price. Suppose that the spot price is $50 per barrel and the futures price for August delivery is $49 per barrel. The producer could short 1,000 future contracts (1,000 barrels per contract) at $49.

- If the spot price on the delivery date is $45 per barrel. The producer realizes $45 million for the oil and earns $4 million in the short contracts. The total gain is $49 million.
- If the spot price on the delivery date is $54 per barrel. The producer realizes $54 million for the oil and losses $5 million in the short contracts. The total gain is $49 million.

### Long Hedges

A long hedge is a hedge that involes a long position in futures contracts. A long hedge is appropriate when the hedger expects to purchase a certain asset in the future. It has the effect of offsetting the hedger's risk.

Assume that a copper fabricator knows it will require 100,000 pounds of copper on May to meet a certain contract. The spot price of copper is 340 cents per pound, and the futures price for May delivery is 320 cents per pound. The fabricator could long 4 futures contracts (25,000 pounds per contract) at 320 cents.

- If the spot price on the delivery date is 325 cents per pound. The fabricator earns $5,000 in the long contract and purchases the copper with $325,000. The total loss is $320,000.
- If the spot price on the delivery date is 315 cents per pound. The fabricator losses $5,000 in the long contract and purchases the copper with $315,000. The total loss is $320,000.

## Arguments For and Against Hedging

The arguments in favor of hedging are obvious. Most nonfinancial companies are in the business of manufacturing or retailing, which can't predict commodity prices. It makes sense for them to hedge the risks to avoid unpleasant surprises.

- Hedging and shareholds: One argument is that the shareholders can do the hedging themselves. However, the commissions are more expensive.
- Hedging and competitors: If hedging is not the norm in a certain industry, it doesn't make sense for one particular company to choose to hedge.
- Hedging and worse outcome: A hedge using futures contracts can result in a decrease or an increase in the profits relative to the position without hedging.

## Basis Risk

- The asset whose price is to be hedged could not be the same as the asset in the futures contract.
- The exact date when the asset will be bought or sold is not certain.
- The hedge might require the futures contract to be closed out before its delivery month.

The basis in a hedging situation is defined as basis = spot price of asset to be hedged - futures price of contract used. If the asset to be hedged and the asset in the futures contract are the same, the basis should be zero at the expiration of the futures contract. Prior to expiration, the basis could be positive or negative.

As time passes, the spot price and the futures price for a particular month do not necessarily change by the same amount, which affects the basis. An increase in the basis is a strengthening of the basis. A decrease in the basis is a weakening of the basis.

- $S_1$: Spot price at time $t_1$
- $S_2$: Spot price at time $t_2$
- $F_1$: Futures price at time $t_1$
- $F_2$: Futures price at time $t_2$
- $b_1$: Basis at time $t_1$
- $b_2$: Basis at time $t_2$

From the definition of the basis, $b_1 = S_1 - F_1$ and $b_2 = S_2 - F_2$.

- The hedger takes a short position at $t_1$ and sells the asset at $t_2$. The price realized for the asset is $S_2$ and the profit of the futures is $F_1 - F_2$. The total profit is $S_2 + F_1 - F_2 = F_1 + b_2$. In a perfect hedge, $S_2 = F_2$, thus the total profit is $F_1$.
- The hedger takes a long position at $t_1$ and purchases the asset at $t_2$. The cost of the asset is $S_2$ and the loss of the futures is $F_1 - F_2$. The total loss is $S_2 + F_1 - F_2 = F_1 + b_2$. In a perfect hedge, $S_2 = F_2$, thus the total loss is $F_1$.

### Choice of Contract

- The choice of the asset of the futures contract: A careful analysis is required to determine the futures contract that are closely correlated with the price of the asset being hedged.
- The choice of the delivery month: A good rule of thumb is to choose a delivery month that is as close as possible to, but later than, the expiration of the hedge.

## Cross Hedging

Cross hedging occurs when the asset in the futures contract is different from the asset whose price is being hedged. The hedge ratio is the ratio of the size of the position taken in futures contracts to the size of the exposure. The hedge ratio is 1.0 for a non-cross hedging.

### Minimum Variance Hedge Ratio

Assume that there's no daily settlement of futures contracts. The minimum variance hedge ratio represents the optimal proportion of futures contracts required to hedge an asset.

- $\Delta S$: Change in spot price during a period of time equal to the life of the hedge
- $\Delta F$: Change in futures price during a period of time equal to the life of the hedge

Assume that there's a linear relationship, $\Delta S = a + b \Delta F + \epsilon$. Let $h$ be the hedge ratio, which means that a percentage $h$ of the exposure to $S$ is hedged with futures. The change in the value of the position per unit of exposure to $S$ is $\Delta S - h \Delta F = a + (b - h) \Delta F + \epsilon$. The minimum variance hedge ratio is $h^* = b$.

Based on the linear regression model, $h^* = \rho \frac{\sigma_S}{\sigma_F}$, where $\rho$ is the coefficient of correlation between $\Delta S$ and $\Delta F$.

The hedge effectiveness is defined as the proportion of the variance that is eliminated with hedging. It's the $R^2$ of the regression, which is equal to $\rho^2$. These parameters are estimated from historical data on $\Delta S$ and $\Delta F$.

### Optimal Number of Contracts

To calculate the number of contracts that should be used in hedging, define:

- $Q_A$: Size of position beging hedged
- $Q_F$: Size of one futures contract
- $N^*$: Optimal number of futures contracts for hedging

The futures contracts should be on $h^{*} Q_A$ units of the asset. The number of futures contracts required is $N^{*} = \frac{h^{*} Q_A}{Q_F}$.

### Impact of Daily Settlement

The daily settlement of futures contract means that, when futures contracts are used, there are a series of one-day hedges. The hedge portfolio could be re-balanced each day to ensure that the hedge ratio remains optimal.

- $\hat{\sigma}_S$: Standard deviation of percentage one-day changes in the spot price
- $\hat{\sigma}_F$: Standard deviation of percentage one-day changes in the futures price
- $\hat{\rho}$: Correlation between percentage one-day changes in the spot and futures
- Optimal hedge ratio based on prices: $h^{*} = \hat{\rho} \frac{\hat{\sigma}_S S}{\hat{\sigma}_F F}$
- Optimal futures contracts based on prices: $N^{*} = \frac{h^{*} Q_A}{Q_F}$
- Optimal hedge ratio based on percentage changes: $\hat{h} = \hat{\rho} \frac{\hat{\sigma}_S}{\hat{\sigma}_F}$
- Optimal futures contracts based on percentage changes: $N^{*} = \frac{h^{*} V_A}{V_F}$, where $V_A = SQ_A$ (the value of the positioin being hedged) and $V_F = FQ_F$ (the price of a futures contract)
