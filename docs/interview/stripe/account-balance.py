'''
At Stripe we keep track of where the money is and move money
between bank accounts to make sure their balances are not
below some threshold. This is for operational and regulatory reasons,
e.g. we should have enough funds to pay out to our users,
and we are legally required to separate our users' funds
from our own. This interview question is a simplified
version of a real-world problem we have here.

Let's say there are at most 500 bank accounts, some of
their balances are above 100 and some are below. How do
you move money between them so that they all have at least
100?

Just to be clear we are not looking for the optimal solution,
but a working one.

Example input:
  - AU: 80
  - US: 140
  - MX: 110
  - SG: 120
  - FR: 70

Output:
  - from: US, to: AU, amount: 20
  - from: US, to: FR, amount: 20
  - from: MX, to: FR, amount: 10
'''

def min_transfers(account_balance):
  sufficient = []
  insufficient = []

  for account, balance in account_balance.items():
    if balance > 100:
      sufficient.append([account, balance - 100])
    elif balance < 100:
      insufficient.append([account, balance - 100])

  sufficient.sort(key=lambda item: item[1])
  insufficient.sort(key=lambda item: item[1])

  transfer = []
  while insufficient and sufficient:
    insufficient_account, insufficient_balance = insufficient[-1]
    sufficient_account, sufficient_balance = sufficient[-1]

    if sufficient_balance + insufficient_balance > 0:
      sufficient[-1][1] += insufficient_balance
      transfer.append((sufficient_account, insufficient_account, -insufficient_balance))
      insufficient.pop()

    elif sufficient_balance + insufficient_balance < 0:
      insufficient[-1][1] += sufficient_balance
      transfer.append((sufficient_account, insufficient_account, sufficient_balance))
      sufficient.pop()

    else:
      transfer.append((sufficient_account, insufficient_account, sufficient_balance))
      sufficient.pop()
      insufficient.pop()

  return transfer

def process_transfer(account_balance, transfer):
  for sender, receiver, amount in transfer:
    account_balance[sender] -= amount
    account_balance[receiver] += amount

  return len([value for value in account_balance.values() if value < 100]) == 0

import unittest

class TestMinTransfers(unittest.TestCase):
  def test_1(self):
    account_balance = {
      'AU': 80,
      'US': 140,
      'MX': 110,
      'SG': 120,
      'FR': 70,
      'KR': 110,
      'CA': 80,
    }
    result = min_transfers(account_balance)
    print(result)
    self.assertTrue(process_transfer(account_balance, result))

  def test_2(self):
    account_balance = {
      'AU': 100,
      'US': 140,
    }
    result = min_transfers(account_balance)
    print(result)
    self.assertTrue(process_transfer(account_balance, result))

  def test_3(self):
    account_balance = {
      'AU': 100,
    }
    result = min_transfers(account_balance)
    print(result)
    self.assertTrue(process_transfer(account_balance, result))

unittest.main()

from collections import defaultdict

def min_transfers_optimal(transactions):
  balance = defaultdict(int)
  for f, t, amount in transactions:
    balance[f] -= amount
    balance[t] += amount
  debt = [value for value in sorted(balance.values()) if value != 0]

  def min_transfers(i):
    while i < len(debt) and debt[i] == 0:
      i += 1
    if i == len(debt):
      return 0

    min_transactions = float('inf')

    for j in range(i + 1, len(debt)):
      if debt[i] * debt[j] < 0:
        debt[j] += debt[i]
        min_transactions = min(min_transactions, min_transfers(i + 1) + 1)
        debt[j] -= debt[i]
    return min_transactions

  return min_transfers(0)
