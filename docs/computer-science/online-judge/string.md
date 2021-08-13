# String

## Longest Substring Without Repeating Characters

[LeetCode 3](https://leetcode.com/problems/longest-substring-without-repeating-characters/)

```py
class Solution:
  def lengthOfLongestSubstring(self, s: str) -> int:
    chars = set()
   start = 0
    result = 0
    for end in range(len(s)):
      while s[end] in chars:
        chars.remove(s[start])
        start += 1
      chars.add(s[end])
      result = max(end - start + 1, result)
    return result
```

## Longest Palindromic Substring

[LeetCode 5](https://leetcode.com/problems/longest-palindromic-substring/)

### Dynamic Programming

```py
class Solution:
  def longestPalindrome(self, s: str) -> str:
    dp = [[False for _ in range(len(s))] for _ in range(len(s))]
    result = ''
    for i in range(len(s) - 1, -1, -1):
      for j in range(i, len(s)):
        if s[i] != s[j]:
          continue
        if i == j or i + 1 == j or dp[i + 1][j - 1]:
          dp[i][j] = True
        if dp[i][j] and j - i + 1 > len(result):
          result = s[i:j + 1]

    return result
```

## Palindromic Substrings

```py
class Solution:
  def countSubstrings(self, s: str) -> int:
    dp = [[False for _ in range(len(s))] for _ in range(len(s))]
    result = 0
    for i in range(len(s) - 1, -1, -1):
      for j in range(i, len(s)):
        if s[i] != s[j]:
          continue
        if i == j or i + 1 == j or dp[i + 1][j - 1]:
          dp[i][j] = True
          result += 1
    return result
```

## Longest Repeating Character Replacement

[LeetCode 424](https://leetcode.com/problems/longest-repeating-character-replacement/)

- If the substring with length `L` doesn't meet the requirement and `start` doesn't change, the longer substring doesn't meet the requirement.
- `max_count` doesn't neet to be updated in the `if` statement.

```py
class Solution:
  def characterReplacement(self, s: str, k: int) -> int:
    count = defaultdict(int)
    start = 0
    result = 0
    max_count = 0

    for end in range(len(s)):
      count[s[end]] += 1
      max_count = max(max_count, count[s[end]])

      if max_count + k < end - start + 1:
        count[s[start]] -= 1
        start += 1
      result = max(end - start + 1, result)
    return result
```

## Group Anagrams

[LeetCode 49](https://leetcode-cn.com/problems/group-anagrams/)

```py
class Solution:
  def groupAnagrams(self, strs: List[str]) -> List[List[str]]:
    result = defaultdict(list)
    for s in strs:
      count = [0] * 26
      for c in s:
        count[ord(c) - ord('a')] += 1
      result[tuple(count)].append(s)
    return list(result.values())
```

## Valid Palindrome

[LeetCode 125](https://leetcode.com/problems/valid-palindrome/)

```py
class Solution:
  def isPalindrome(self, s: str) -> bool:
    left = 0
    right = len(s) - 1

    while left < right:
      while left < right and not s[left].isalnum():
        left += 1
      while left < right and not s[right].isalnum():
        right -= 1

      if left >= right:
        break
      if s[left].lower() != s[right].lower():
        return False
      left += 1
      right -= 1

    return True
```
