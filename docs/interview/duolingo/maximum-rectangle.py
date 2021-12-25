# f(i) = max(f(i - 1) + nums[i], nums[i])

def max_sub_array(nums) -> int:
  result = nums[0]
  window = nums[0]
  for num in nums[1:]:
    window = max(num, window + num)
    result = max(result, window)
  return result

def find_max_sum(matrix):
  result = float('-inf')
  prefix = [[0] for _ in range(len(matrix))]

  for i in range(len(matrix)):
    for j in range(len(matrix[0])):
      prefix[i].append(prefix[i][-1] + matrix[i][j])

  for left in range(len(matrix[0])):
    for right in range(left + 1, len(matrix[0])):
      row_prefix = []
      for row in range(len(matrix)):
        row_prefix.append(prefix[row][right] - prefix[row][left])
      result = max(result, max_sub_array(row_prefix))
  return result

print(find_max_sum([
  [1, 2, -1, -4, -20],
  [-8, -3, 4, 2, 1],
  [3, 8, 10, 1, 3],
  [-4, -1, 1, 7, -6],
]))
