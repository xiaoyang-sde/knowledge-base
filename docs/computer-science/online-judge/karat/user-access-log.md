# Parse User Access Resources Log

## Question 1

For a list of user access resources logs, return each user's min and max access timestamp.

```py
def get_user_access_time(user_access_logs):
  result = {}
  for time, user, _ in user_access_logs:
    time = int(time)
    if user not in result:
      result[user] = [time, time]
    else:
      prev_min, prev_max = result[user]
      result[user] = [min(prev_min, time), max(prev_max, time)]
  return result

user_access_logs = [
  ["58523", "user_1", "resource_1"],
  ["62314", "user_2", "resource_2"],
  ["54001", "user_1", "resource_3"],
  ["200", "user_6", "resource_5"],
  ["215", "user_6", "resource_4"],
  ["54060", "user_2", "resource_3"],
  ["53760", "user_3", "resource_3"],
  ["58522", "user_22", "resource_1"],
  ["53651", "user_5", "resource_3"],
  ["2", "user_6", "resource_1"],
  ["100", "user_6", "resource_6"],
  ["400", "user_7", "resource_2"],
  ["100", "user_8", "resource_6"],
  ["54359", "user_1", "resource_3"],
];
print(get_user_access_time(user_access_logs))
```

## Question 2

Write a function that takes the logs and returns the resource with the highest number of accesses in any 5 minute window, together with how many accesses it saw.

```py
from collections import defaultdict

def get_max_resource_window(user_access_logs):
  resources = defaultdict(list)
  for time, _, resource in user_access_logs:
    resources[resource].append(int(time))

  max_count = 0
  max_resource = None

  for resource, access_times in resources.items():
    access_times.sort()
    start = 0
    count = 0
    for end in range(len(access_times)):
      while start <= end and access_times[end] - access_times[start] > 300:
        start += 1
      count = max(count, end - start + 1)

    if count > max_count:
      max_count = count
      max_resource = resource

  return {
    max_resource: max_count
  }
```

## Question 3

Write a function that takes the logs as input, builds the transition graph and returns it as an adjacency list with probabilities. Add START and END states.

Specifically, for each resource, we want to compute a list of every possible next step taken by any user, together with the corresponding probabilities. The list of resources should include START but not END, since by definition END is a terminal state.

```py
from collections import defaultdict, Counter

def build_resources(user_access_logs):
  user_actions = defaultdict(list)
  for time, user, resource in user_access_logs:
    user_actions[user].append((time, resource))

  next_counter = defaultdict(Counter)
  for _, actions in user_actions.items():
    actions.sort()
    actions.insert(0, (0, 'START'))
    actions.append((float('inf'), 'END'))
    for i in range(len(actions) - 1):
      prev_resource = actions[i][1]
      next_resource = actions[i + 1][1]
      next_counter[prev_resource][next_resource] += 1

  result = {}
  for prev_resource, next_steps in next_counter.items():
    total = sum(next_steps.values())
    result[prev_resource] = {}
    for next_step, count in next_steps.items():
      result[prev_resource][next_step] = count / total
  return result
```
