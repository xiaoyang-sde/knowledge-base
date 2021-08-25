# Badge and Company

We are working on a security system for a badged-access room in our company's building.

## Question 1

Given an ordered list of employees who used their badge to enter or exit the room, write a function that returns two collections:

- All employees who didn't use their badge while exiting the room – they recorded an enter without a matching exit.
- All employees who didn't use their badge while entering the room – they recorded an exit without a matching enter.

```py
def find_mismatched_entries(badge_records):
  room = set()
  mismatch_exit = set()
  mismatch_enter = set()
  for employee, action in badge_records:
    if action == 'enter':
      if employee in room:
        mismatch_exit.add(employee)
      else:
        room.add(employee)

    if action == 'exit':
      if employee not in room:
        mismatch_enter.add(employee)
      else:
        room.remove(employee)

  mismatch_exit = mismatch_exit.union(room)
  return mismatch_enter, mismatch_exit

badge_records = [
  ["Martha",   "exit"],
  ["Paul",     "enter"],
  ["Martha",   "enter"],
  ["Martha",   "exit"],
  ["Jennifer", "enter"],
  ["Paul",     "enter"],
  ["Curtis",   "enter"],
  ["Paul",     "exit"],
  ["Martha",   "enter"],
  ["Martha",   "exit"],
  ["Jennifer", "exit"],
]

print(find_mismatched_entries(badge_records))
```

## Question 2

We want to find employees who badged into our secured room unusually often. We have an unordered list of names and access times over a single day. Access times are given as three or four-digit numbers using 24-hour time, such as "800" or "2250".

Write a function that finds anyone who badged into the room 3 or more times in a 1-hour period, and returns each time that they badged in during that period. (If there are multiple 1-hour periods where this was true, just return the first one.)

```py
from collections import defaultdict

def find_unusual_entries(badge_records):
  badge_records.sort()
  visited = defaultdict(list)
  result = {}
  for employee, time in badge_records:
    visited[employee].append(time)

  for employee, record in visited.items():
    if len(record) < 3:
      continue

    for start in range(len(record) - 2):
      end = start + 2
      while 0 <= record[end] - record[start] <= 100 and end < len(record):
        end += 1
        result[employee] = record[start:end]
      if employee in result:
        break
  return result

badge_records = [
  ["Paul", 1315],
  ["Jennifer", 1910],
  ["John", 830],
  ["Paul", 1355],
  ["John", 835],
  ["Paul", 1405],
  ["Paul", 1630],
  ["John", 855],
  ["John", 915],
  ["John", 930],
  ["Jennifer", 1335],
  ["Jennifer", 730],
  ["John", 1630],
]

print(find_unusual_entries(badge_records))
```
