# Friend Circle

## Question 1

Given a list of employee records and a list of friendship, return the list of friends for each employee.

```py
def getFriendList(records, friendshipList):
  graph = {}
  for record in records:
    employee = record.split(',')[0]
    graph[employee] = []
  for friendship in friendshipList:
    f1, f2 = friendship.split(',')
    graph[f1].append(f2)
    graph[f2].append(f1)
  return graph
```

## Question 2

Given a list of employee records and a list of friendship, for each department, return the number of employees and the number of employees who have a friend outside the department.

```py
from collections import defaultdict

def department(records, friends):
  friends = getFriendList(records, friends)
  employee_department = {}
  department_employee = defaultdict(list)
  result = {}
  for record in records:
    employee, _, department = record.split(',')
    employee_department[employee] = department
    department_employee[department].append(employee)

  for department, employees in department_employee.items():
    count = 0
    for employee in employees:
      for friend in friends[employee]:
        if employee_department[friend] != department:
          count += 1
          break
    result[department] = (len(employees), count)
  return result
```

## Question 3

Determine if all employees are in the same friend circle.

```py
def sameFriendCircle(records, friends):
  friends = getFriendList(records, friends)
  visited = set()
  def dfs(employee):
    visited.add(employee)
    for friend in friends[employee]:
      if friend in visited:
        continue
      dfs(friend)

  dfs(friends.keys()[0])
  return len(visited) == len(friends.keys())
```
