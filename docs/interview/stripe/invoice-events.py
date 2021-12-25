import unittest

from collections import defaultdict

class Invoicer:
  def __init__(self, schedule):
    self.schedule = sorted(schedule.items())

  def send_emails(self, invoices):
    email_queue = []
    for invoice_time, user, due_amount in invoices:
      for time_diff, template in self.schedule:
        email_time = invoice_time + time_diff
        email_content = template.format(user, due_amount, invoice_time)
        email_queue.append((email_time, email_content))

    print(email_queue)
    return [email_content for _, email_content in sorted(email_queue)]

  def send_emails_payment(self, invoices, payments):
    email_queue = []
    user_payment = defaultdict(list)
    for pay_time, user, pay_amount in payments:
      user_payment[user].append((pay_time, pay_amount))

    for invoice_time, user, due_amount in invoices:
      next_payment_index = 0
      payment_queue = user_payment[user]
      for time_diff, template in self.schedule:
        email_time = invoice_time + time_diff

        while (
          next_payment_index < len(payment_queue) and
          payment_queue[next_payment_index][0] <= email_time
        ):
          due_amount -= payment_queue[next_payment_index][1]
          next_payment_index += 1

        if due_amount <= 0:
          break

        email_content = template.format(user, due_amount, invoice_time)
        email_queue.append((email_time, email_content))

    print(email_queue)
    return [email_content for _, email_content in sorted(email_queue)]

class TestInvoicer(unittest.TestCase):
  def test_1(self):
    schedule = {
      -10: '{}: You\' have {} due at {}',
      0: '{}: You\' have {} due at today ({})',
      10: '{}: You\' have {} due past {}',
    }
    invoices = [
      (0, 'A', 100),
    ]

    invoicer = Invoicer(schedule)
    result = invoicer.send_emails(invoices)
    print(result)

  def test_2(self):
    schedule = {
      -10: '{}: You\' have {} due at {}',
      0: '{}: You\' have {} due at today ({})',
      10: '{}: You\' have {} due past {}',
    }
    invoices = [
      (10, 'A', 100),
    ]
    payments = [
      (0, 'A', 20),
      (10, 'A', 70),
      (15, 'A', 5),
      (20, 'A', 5),
    ]

    invoicer = Invoicer(schedule)
    result = invoicer.send_emails_payment(invoices, payments)
    print(result)

unittest.main()
