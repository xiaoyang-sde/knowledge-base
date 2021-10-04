import unittest

from collections import deque, defaultdict

class RateLimiter:
  def __init__(self, time_limit, max_rate):
    self.time_limit = time_limit
    self.max_rate = max_rate
    self.access_record = defaultdict(lambda: deque(maxlen=max_rate))

  def access(self, user, timestamp):
    user_record = self.access_record[user]
    if len(user_record) > 0 and timestamp < user_record[-1]:
      pass
    if (
      self.max_rate == len(user_record)
      and timestamp - user_record[0] <= self.time_limit
    ):
      return False

    user_record.append(timestamp)
    print(user_record)
    return True

class TestRateLimiter(unittest.TestCase):
  def test_1(self):
    rate_limiter = RateLimiter(10, 4)
    test_cases = [
      (0, True),
      (1, True),
      (2, True),
      (3, True),
      (4, False),
      (10, False),
      (11, True),
      (12, True),
      (15, True),
    ]
    for timestamp, result in test_cases:
      print(timestamp, result)
      self.assertEqual(
        rate_limiter.access('test_user', timestamp),
        result,
      )

if __name__ == '__main__':
  unittest.main()
