rank = {
  'a': ['b', 'c', 'd'],
  'b': ['a', 'c'],
  'c': ['d', 'a'],
  'd': ['c', 'b'],
}

def hasMutualRank(user):
  first_choice = rank[user][0]
  return rank[first_choice][0] == user

print(hasMutualRank('a'))
print(hasMutualRank('d'))

def hasMutualRankIndex(user, index):
  user_1 = rank[user][index]
  return index < len(rank[user_1]) and rank[user_1][index] == user

print(hasMutualRankIndex('a', 1))
print(hasMutualRankIndex('a', 2))

def swap(user, index):
  result = []
  user_1 = rank[user][index]
  if index < len(rank[user_1]) and rank[user_1][index] == user:
    result.append(user_1)

  user_2 = rank[user][index - 1]
  if (
    index > 0
    and (index <= len(rank[user_2]) and rank[user_2][index - 1] == user)
    and not (index < len(rank[user_2]) and rank[user_2][index] == user)
  ):
    result.append(user_2)
  return result

print(swap('a', 2))

def get_pairs():
  result = set()
  for user in rank:
    for i in range(len(rank[user])):
      swap_result = swap(user, i)
      for user_1 in swap_result:
        print(user, user_1)
        if (user_1, user) in result:
          continue
        result.add((user, user_1))
  return result

print(get_pairs())
