# https://leetcode-cn.com/problems/longest-increasing-path-in-a-matrix/

from functools import cache

def longestIncreasingPath(matrix):
  @cache
  def dfs(i, j):
    result = 0
    for di, dj in ((1, 0), (0, 1), (-1, 0), (0, -1)):
      ni = i + di
      nj = j + dj

      if ni < 0 or nj < 0 or ni >= len(matrix) or nj >= len(matrix[0]):
        continue
      if matrix[ni][nj] <= matrix[i][j]:
        continue

      result = max(result, dfs(ni, nj))

    return result + 1

  max_path = 0
  for i in range(len(matrix)):
    for j in range(len(matrix[0])):
      max_path = max(dfs(i, j), max_path)
  return max_path
