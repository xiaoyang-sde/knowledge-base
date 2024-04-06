# Trading Options Greeks

## Introduction

### Standardized Contract

An option is a contract that gives its owner the right to buy or the right to sell a fixed quantity of an underlying security at a specific price within a certain time constraint, where a call corresponds to the right to buy and a put corresponds to the right to sell.

- The option buyer is the party who owns the right inherent in the contract. At expiration, the owner may exercise the right or let it expire without exercising it. Option holders won't receive dividends or have voting rights. Options expire on the third Friday of each month.
- The option seller is the party who owns the obligation inherent in the contract. When a trader who is long an option exercises, a trader with a short position gets assigned. Assignment means the trader with the short option position is called on to fulfill the obligation.

Volume is the total number of contracts traded during a time period, which can be stated on a one-day basis. Open interest is the number of contracts that have been created and remain outstanding, which is a running total.

### Strike Price

The price at which the option holder owns the right to buy or to sell the underlying is called the strike price, or exercise price. The price of an option is called its premium, which is made up of a intrinsic value and time value. Intrinsic value is the amount by which the option is ITM. Options that are out-of-the-money have no intrinsic value.

- For calls, if the stock price is above the strike price, the call is in-the-money (ITM). If the stock and the strike prices are close, the call is at-the-money (ATM). If the stock price is below the strike price the call is out-of-the-money (OTM).
- For puts, if the stock price is below the strike price, the call is in-the-money (ITM). If the stock and the strike prices are close, the call is at-the-money (ATM). If the stock price is above the strike price the call is out-of-the-money (OTM).

### Exercise Style

American-exercise options can be exercised, and therefore assigned, anytime after the contract is entered into until either the trader closes the position or it expires. European-exercise options can be exercised and assigned only at expiration. Exchange-listed equity options are American-exercise style, while index options are European-exercise style.

### Clearing

The Options Clearing Corporation (OCC) guarantees all options trades. When a trader buys an option through a broker, the broker submits the trade information to its clearing firm. The clearing firms on both sides submit the trade information to the OCC, which matches up the trade. When the buyer exercises the option, the OCC assigns one of its clearing members with a customer that shorts the option, and the clearing member will assign the trade either randomly or first in, first out.

### Strategies

- Long call: The long call has unlimited profit potential with limited risk. Whenever an option is purchased, the most that can be lost is the premium paid for the option. The break even price is the strike price plus the call premium.
- Short call: The short call has limited profit potential with unlimited risk. If the position is held until expiration without getting assigned, the entire premium represents a profit for the trader. However, if assignment occurs, the trader will be obliged to sell stock at the strike price. The break even price is the strike price plus the call premium.
- Short put:
