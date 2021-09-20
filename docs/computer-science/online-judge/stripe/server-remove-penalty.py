# Throughout this interview, we'll write code to analyze a simple server process uptime log. These logs are
# much simplified, and are just strings of space separated 0's and 1's. The log is a string of binary
# digits (e.g. "0 0 1 0"). Each digit corresponds to 1 hour of the server running:
#  
# "1" = <crashed>, "down" // server process crashed during the hour
# "0" = <didn't crash>, "up" // server process did not crash during the hour
#  
#  EXAMPLE: A server with log "0 0 1 0" ran for 4 hours and its process crashed during hour #3
#  
#    hour: |1|2|3|4|
#    log : |0|0|1|0|
#               ^
#               |
#              down during hour #3
#  
# We can *permanently remove* a server at the beginning of any hour during its operation. A server
# is on the network until it is removed. Note that a server stays POWERED ON after removal, it's
# just not on the network.
#  
# We'd like to understand the best times to remove a server. So let's introduce an aggregate metric called a "penalty" for removing a server at a bad time.
#  
#  
#  EXAMPLE: Remove a server with log "0 0 1 0"
#  
#     hour :  | 1 | 2 | 3 | 4 |
#     log  :  | 0 | 0 | 1 | 0 |
# remove_at:  0   1   2   3   4   // remove_at being `x` means "server removed before hour `x+1`"
#             ^               ^
#             |               |
#      before hour #1         after hour #4
#  
#  
# We define our penalty like this:
# +1 penalty for each DOWN hour when a server is on the network
# +1 penalty for each UP hour after a server has been removed
#  
# Further Examples:
#  
#    hour :   1 2 3 4     // total penalty = 3  (3 server-up hours after remove)
#    log  :   0 0 1 0
#            ^
#            |
#          remove_at = 0
#  
#    hour : 1 2 3 4      // total penalty = 2 (1 server-down hour before remove + 1 server-up hour after remove)
#    log  : 1 0 1 0
#              ^
#              |
#            remove_at = 2
#  
#  
#    hour :   1 2 3 4     // total penalty = 1  (1 server-down hour before remove)
#    log  :   0 0 1 0
#                    ^
#                    |
#                  remove_at = 4
#  
#  
# Note that for a server log of length `n` hours, the remove_at variable can range
# from 0, meaning "before the first hour" to n, meaning "after the final hour".
#  
# 1a)
# Write a function: compute_penalty, that computes the total penalty, given
#  
# a server log (as a string) AND
# a time at which we removed the server from the network (call that variable remove_at)
#  
# In addition to writing this function, you should use tests to demonstrate that it's correct.
#  
# ## Examples
#  
# compute_penalty("0 0 1 0", 0) should return 3
# compute_penalty("1 0 1 0", 2) should return 2
# compute_penalty("0 0 1 0", 4) should return 1

from os import remove


def compute_penalty(log, remove_time):
  # log = map(int, log.split())
  result = 0
  for index, status in enumerate(log):
    if index < remove_time and status == 1:
      result += 1
    if index >= remove_time and status == 0:
      result += 1
  return result

print(compute_penalty("0 0 1 0", 0))
print(compute_penalty("1 0 1 0", 2))
print(compute_penalty("0 0 1 0", 4))

# 1b)
# Use your answer for compute_penalty to write another function: find_best_removal_time, that returns
# the best remove_at hour, given a server log.

# Again, you should use tests to demonstrate that it's correct.

# ## Example
# find_best_removal_time("0 0 1 1") should return 2

def find_best_removal_time(log):
  # log = list(map(int, log.split()))
  # min_penalty = log.count(0)
  # for remove_time in range(1, len(log) + 1):
  #   penalty = compute_penalty(log, remove_time)

  #   if penalty < min_penalty:
  #     min_penalty = penalty
  #     result = remove_time
  # return result
  log = list(map(int, log.split()))
  penalty = log.count(0)
  min_penalty = penalty
  result = 0

  for remove_time in range(1, len(log) + 1):
    if log[remove_time - 1] == 0:
      penalty -= 1
    else:
      penalty += 1

    if penalty < min_penalty:
      min_penalty = penalty
      result = remove_time

  return result

print(find_best_removal_time("0 0 1 1"))

# 2a)
# Now that we're able to analyze single server logs, let's analyze some aggregate logs.
# Aggregate logs are text files that contain lots of logs. The files contain only BEGIN,
# END, 1, 0, spaces and newlines. Aggregate logs include some servers that aren’t actually
# finished, so we might have some BEGINs scattered throughout. We'll only consider innermost
# BEGINs and ENDs to be valid log sequences.‍‍‍‌‍‍‍‍‌‌‌‍‍‌‍‌‌‍‍ *Log sequences cannot be nested*

# Put another way, any sequence of 0s and 1s surrounded by BEGIN and END forms a valid
# sequence. For example, the sequence "BEGIN BEGIN BEGIN 1 1 BEGIN 0 0 END 1 1 END BEGIN" has
# only one valid sequence "BEGIN 0 0 END".

# Write a function get_best_removal_times, that takes the file's contents as a parameter,
# and returns an array of best removal hours for every valid server log in that file.

# Note: that logs can span 1 or many lines.

# Again, you should use tests to demonstrate that your solution is correct.

# ## Example
# get_best_removal_times("BEGIN BEGIN \nBEGIN 1 1 BEGIN 0 0\n END 1 1 END BEGIN")
# should return an array: [2]

def get_best_removal_times(logs):
  logs = ' '.join(logs.split('\n'))
  logs = logs.split(' ')
  start = -1
  result = []
  for end, log in enumerate(logs):
    if log == 'BEGIN':
      start = end
    if log == 'END' and start >= 0:
      result.append(find_best_removal_time(' '.join(logs[start + 1:end])))
      start = -1
  return result

print(get_best_removal_times("BEGIN BEGIN \nBEGIN 0 0\n END BEGIN 1 1 BEGIN 0 0\n END 1 BEGIN 0 0\n END 1 END BEGIN"))

