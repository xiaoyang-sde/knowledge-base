# Course Schedule

## Question 1

You are a developer for a university. Your current project is to develop a system for students to find courses they share with friends. The university has a system for querying courses students are enrolled in, returned as a list of (ID, course) pairs.

Write a function that takes in a list of (student ID number, course name) pairs and returns, for every pair of students, a list of all courses they share.

```py
from collections import defaultdict

def find_courses_pairs(student_course_pairs):
  course_map = defaultdict(list)
  student_set = set()
  for student, course in student_course_pairs:
    course_map[course].append(student)
    student_set.add(student)

  result = {}
  student_list = list(student_set)
  for i in range(len(student_list)):
    for j in range(i + 1, len(student_list)):
      result[(student_list[i], student_list[j])] = []

  for course in course_map:
    students = course_map[course]
    for i in range(len(students)):
      for j in range(i + 1, len(students)):
        if (students[i], students[j]) in result:
          result[(students[i], students[j])].append(course)
        else:
          result[(students[j], students[i])].append(course)
  return result

student_course_pairs = [
  ["58", "Software Design"],
  ["58", "Linear Algebra"],
  ["94", "Art History"],
  ["94", "Operating Systems"],
  ["17", "Software Design"],
  ["58", "Mechanics"],
  ["58", "Economics"],
  ["17", "Linear Algebra"],
  ["17", "Political Science"],
  ["94", "Economics"],
  ["25", "Economics"],
]

print(find_courses_pairs(student_course_pairs))
```

## Question 2

Write a function that takes a list of (prerequisite, course) pairs, and returns the name of the course that the student will be taking when they are halfway through their program. (If a track has an even number of courses, and therefore has two "middle" courses, you should return the first one.)

```py
def find_course_mid(prerequisites):
    courses = set()
    graph = defaultdict(list)
    degree = defaultdict(int)
    for pre, course in prerequisites:
      graph[pre].append(course)
      degree[course] += 1
      courses.add(course)
      courses.add(pre)

    queue = [course for course in courses if degree[course] == 0]
    for course in queue:
      for neighbor in graph[course]:
        degree[neighbor] -= 1
        if degree[neighbor] == 0:
          queue.append(neighbor)

    if len(queue) % 2 == 0:
      return queue[(len(queue) // 2) - 1]
    return queue[len(queue) // 2]
```

## Question 3

Students may decide to take different "tracks" or sequences of courses in the
Computer Science curriculum. There may be more than one track that includes the
same course, but each student follows a single linear track from a "root" node to
a "leaf" node. In the graph below, their path always moves left to right.

Write a function that takes a list of (source, destination) pairs, and returns the
name of all of the courses that the students could be taking when they are halfway
through their track of courses.

```py
from collections import defaultdict

def find_course_mid(prerequisites):
  pre_set = set()
  course_set = set()
  graph = defaultdict(list)
  for pre, course in prerequisites:
    pre_set.add(pre)
    course_set.add(course)
    graph[pre].append(course)

  paths = []
  def dfs(course, path):
    if not graph[course]:
      paths.append(path[:])
    for child in graph[course]:
      path.append(child)
      dfs(child, path)
      path.pop()

  for pre in pre_set - course_set:
    dfs(pre, [pre])

  result = set()
  for path in paths:
    result.add(path[(len(path) - 1) // 2])
  return list(result)

prerequisites = [
  ["Logic", "COBOL"],
  ["Data Structures", "Algorithms"],
  ["Creative Writing", "Data Structures"],
  ["Algorithms", "COBOL"],
  ["Intro to Computer Science", "Data Structures"],
  ["Logic", "Compilers"],
  ["Data Structures", "Logic"],
  ["Creative Writing", "System Administration"],
  ["Databases", "System Administration"],
  ["Creative Writing", "Databases"],
  ["Intro to Computer Science", "Graphics"],
]

print(find_course_mid(prerequisites))
```
