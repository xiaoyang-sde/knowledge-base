def get_cooking_time(dishes, cooldown):
  time = 0
  latest_cooking_time = {}
  for _, dish in enumerate(dishes):
    if dish in latest_cooking_time:
      time = max(time, cooldown + latest_cooking_time[dish])
    time += 1
    latest_cooking_time[dish] = time
  return time

print(get_cooking_time(['fish', 'fish'], 2))
