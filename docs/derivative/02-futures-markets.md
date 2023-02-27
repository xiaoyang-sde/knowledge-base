# Futures Markets and Central Counterparties

## Specification

When developing a new contract, the exchange specifies the asset, the contract size, where delivery can be made, and when delivery can be made. Alternatives could be specified for the grade of the asset that will be delivered. When the party with the short position is ready to deliver, it files a notice of intention to deliver with the exchange.

- Asset: For commodity assets, it is important that the exchange stipulate the grade or grades of the commodity that are acceptable. However, the financial assets in futures contracts are well defined and unambiguous.
- Contract size: The contract size specifies the amount of the asset that has to be delivered under one contract.
- Delivery arrangement: The place where delivery will be made must be specified by the exchange. This is important for commodities that involve significant transportation costs.
- Delivery months: The exchange specifies the precise period during the month when delivery can be made.
- Price Quotes: The exchange defines how prices will be quoted.
- Price Limits: For most contracts, daily price movement limits are specified by the exchange. A limit move is a move in either direction equal to the daily price limit. Normally, trading ceases for the day once the contract is limit up or limit down.
- Position Limits: Position limits are the maximum number of contracts that a speculator could hold. The purpose of these limits is to prevent speculators from exercising undue influence on the market.

## Convergence

As the delivery period for a futures contract is approached, the futures price converges to the spot price of the underlying asset. Suppose that the futures price is above the spot price during the delivery period, arbitragers could short the futures contract (which drives down the price), buy the asset, and make delivery.

## The Operation of Margin Accounts

### Settlement

- Initial margin: The amount that must be deposited at the time the contract is entered into.
- Maintained margin: f the balance in the margin account falls below the maintenance margin, the trader receives a margin call and is expected to top up the margin account to the initial margin level within a short period of time.
- Settlement: At the end of each trading day, the margin account is adjusted to reflect the trader's gain or loss.

### Clearing House

The clearing house acts as an intermediary in futures transactions. It guarantees the performance of the parties to each transaction. The clearing house has a number of members. Brokers who are not members themselves must channel their business through a member and post margin with the member. The main task of the clearing house is to keep track of all the transactions that take place during a day, so that it can calculate the net position of each of its members.

The clearing house member is required to provide to the clearing house initial margin reflecting the total number of contracts that are being cleared. In determining margin requirements, the number of contracts outstanding is calculated on a net basis rather than a gross basis. The calculation of the margin requirement is designed to ensure that the clearing house is about 99% certain that the margin will be sufficient to cover losses in the event that the member defaults and has to be closed out.

## OTC Markets

Credit risk has been a feature of OTC derivatives markets. In an attempt to reduce credit risk, the OTC market has borrowed some ideas from exchange-traded markets.

- Central Counterparties: Once an OTC derivative transaction has been agreed between two parties A and B, it can be presented to a CCP. Assuming the CCP accepts the transaction, it becomes the counterparty to both A and B. All members of the CCP are required to provide initial margin to the CCP.
Transactions are valued daily and there are daily variation margin payments to or from the member.
- Bilateral Clearing: Two companies enter into a master agreement covering all their trades. This agreement could include a credit support annex that requires both companies to provide collateral. Collateral agreements in CSAs usually require transactions to be valued each day. CSA requires both initial margin and variation margin.

## Market Quotes

Futures quotes are available from exchanges and several online sources.

- Opening price: The prices at which contracts were trading after the start of trading on a day.
- Settlement price: The prices at which contracts were trading before the end of trading on a day.
- Trading volume: The number of contracts traded in a day.
- Open interest: The number of outstanding contracts, which is the number of long or short positions.

## Delivery

To avoid the risk of having to take delivery, a trader with a long position should close out his or her contracts prior to the first notice day. The first notice day is the first day on which a notice of intention to make delivery can be submitted to the exchange. The last notice day is the last such day. The last trading day is generally a few days before the last notice day.

- The decision on when to deliver is made by the party with the short position, whom is refer to as trader A.
- When trader A decides to deliver, trader A’s broker issues a notice of intention to deliver to the exchange clearing house.
- The exchange chooses a party with a long position to accept delivery, whom is refer to as trader B. Parties with long positions must accept delivery notices.
- In the case of a commodity, taking delivery means accepting a warehouse receipt in return for immediate payment. In the case of financial futures, delivery is usually made by wire transfer.

Some financial futures are settled in cash. In the case of the futures contract on the S&P 500, delivering the underlying asset would involve delivering a portfolio of 500 stocks. When a contract is settled in cash, all outstanding contracts are declared closed on a predetermined day. The final settlement price is set equal to the spot price of the underlying asset at either the open or close of trading on that day.

## Types of Traders

There are two main types of traders executing trades:

- Futures commission merchants are following the instructions of their clients and charge a commission.
- Locals are trading on their own account.

Individuals taking positions, whether locals or the clients of FCMs, can be categorized as hedgers, speculators, or arbitrageurs. Speculators can be classified as scalpers, day traders, or position traders.

- Scaplers are watching for very short-term trends and attempt to profit from small changes in the contract price.
- Day traders hold their positions for less than one trading day.
- Position traders hold their positions for much longer periods of time.

## Types of Orders

- Market order is a request that a trade be carried out immediately at the best price available in the market.
- Limit order is set at a certain price and it is executable at times when the trade can be performed at the limit price or at a price that is considered more favorable than the limit price.
- Stop order or stop-loss order becomes executable once a set price has been reached and is then filled at the current market price.
- Stop-limit order is a combination of a stop order and a limit order. The order becomes a limit order as soon as a bid or ask is made at a price equal to or less favorable than the stop price.
- Market-if-touched order is executed at the best available price after a trade occurs at a specified price or at a price more favorable than the specified price.
- Market-not-held order is executed at the broker's discretion in an attempt to get a better price.

Unless otherwise stated, an order is a day order and expires at the end of the trading day.

- Time-of-day order specifies a particular period of time during the day when the order can be executed.
- Open order order is in effect until executed or until the end of trading in the particular contract.
- Fill-or-kill order must be executed on receipt or not at all.

## Regulation

Futures markets in the United States are currently regulated federally by the Commodity Futures Trading Commission. The CFTC looks after the public interest. It is responsi ble for ensuring that prices are communicated to the public and that futures traders report their outstanding positions if they are above certain levels.  The CFTC also licenses all individuals who offer their services to the public in futures trading. The CFTC deals with complaints brought by the public and ensures that disciplinary action is taken against individuals when appropriate.

The Dodd–Frank act, signed into law by President Obama in 2010, expanded the role of the CFTC. For example, it is now responsible for rules requiring that standard over- the-counter derivatives between financial institutions be traded on swap execution facilities and cleared through central counterparties.

## Accounting and Tax

Accounting standards require changes in the market value of a futures contract to be recognized when they occur unless the contract qualifies as a hedge. If the contract does qualify as a hedge, gains or losses are generally recognized for accounting purposes in the same period in which the gains or losses from the item being hedged are recognized.

Under the U.S. tax rules, two key issues are the nature of a taxable gain or loss and the timing of the recognition of the gain or loss. Gains or losses are either classified as capital gains or losses or as part of ordinary income.

- For a corporate taxpayer, capital gains are taxed at the same rate as ordinary income. Capital losses are deductible to the extent of capital gains.
- For a noncorporate taxpayer, short-term capital gains are taxed at the same rate as ordinary income, but long-term capital gains are subject to a maximum capital gains tax rate of 20%.

Positions in futures contracts are treated as if they are closed out on the last day of the tax year. For the noncorporate taxpayer, this gives rise to capital gains and losses that are treated as if they were 60% long term and 40% short term without regard to the holding period.

## Forward vs. Futures Contract

- Forward
  - Private contract between two parties
  - Not standardized
  - One specified delivery date
  - The gain and loss is settled at end of contract
  - Delivery or final cash settlement
  - Credit risk
- Futures
  - Traded on an exchange
  - Standardized contract
  - Range of delivery dates
  - The gain or loss is settled daily
  - Contract is usually closed out
  - No credit risk
