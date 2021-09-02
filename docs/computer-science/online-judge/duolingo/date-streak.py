'''
- Brute force: Consider each date in dates, get the streak as high as possible. (Time: O(n^2))
- Sorting: Sort the dates in increasing order, iterate through the dates. (Time: O(n log n))
- HashSet: Store the dates in a HashSet and iterate through them. If the day before that
  date is not in the HashSet, get the streak as high as possible. (Time: O(n))
'''

from datetime import datetime, timedelta

def get_streak(dates):
  result = []
  date_set = set()
  for date in dates:
    date = datetime.strptime(date, "%Y-%m-%d")
    date_set.add(date)

  for date in date_set:
    if date - timedelta(days=1) in date_set:
      continue
    streak = []
    while date in date_set:
      streak.append(date.strftime("%Y-%m-%d"))
      date += timedelta(days=1)
    result.append((len(streak), *streak))
  return result

print(get_streak([
  "2021-01-16",
  "2021-01-17",
  "2021-01-15",
  "2021-01-10",
]))
