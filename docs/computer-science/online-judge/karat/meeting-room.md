# Meeting Room

## Question 1

Given a list of meetings, determine if the new meeting could be scheduled.

```py
def can_schedule(meetings, start, end):
  for s, e in meetings:
    if start >= e or end <= s:
      continue
    return False
  return True
```

## Question 2

Given a list of meetings, return the spare time in that day.

```py
def spare_time(meetings):
  meetings.sort()
  merged = []
  for start, end in meetings:
    if not merged or start > merged[-1][-1]:
      merged.append([start, end])
    else:
      merged[-1][-1] = max(merged[-1][-1], end)

  result = []
  if merged[0][0] > 0:
    result.append([0, merged[0][0]])

  for i in range(len(merged) - 1):
    end = merged[i][1]
    start = merged[i + 1][0]
    result.append([end, start])

  if merged[-1][-1] < 2400:
    result.append([merged[-1][-1], 2400])

  return result
```

## Question 3

Given a list of meetings and a list of meeting rooms, return whether or not the meetings could be scheduled to the given meeting rooms.

```py
def can_schedule(meetings, meeting_rooms):
  pass
```
