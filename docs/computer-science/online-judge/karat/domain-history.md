# Domain, History, and Ads Conversion

## Question 1

A website domain like "discuss.leetcode.com" consists of various subdomains. At the top level, we have "com", at the next level, we have "leetcode.com", and at the lowest level, "discuss.leetcode.com". When we visit a domain like "discuss.leetcode.com", we will also visit the parent domains "leetcode.com" and "com" implicitly.

Now, call a "count-paired domain" to be a count (representing the number of visits this domain received), followed by a space, followed by the address. An example of a count-paired domain might be "9001 discuss.leetcode.com".

We are given a list cpdomains of count-paired domains. We would like a list of count-paired domains, (in the same format as the input, and in any order), that explicitly counts the number of visits to each
subdomain.

```py
from collections import defaultdict

def subdomainVisits(cpdomains):
  domain_count = defaultdict(int)

  for count_domain in cpdomains:
    count, domain = count_domain.split()
    subdomains = domain.split('.')
    current_domain = ''

    for subdomain in subdomains[::-1]:
      if not current_domain:
        current_domain = subdomain
      else:
        current_domain = subdomain + '.' + current_domain
      domain_count[current_domain] += int(count)

  return [f'{count} {domain}' for domain, count in domain_count.items()]
```

## Question 2

Given the browsing history of two users, find the longest continuous common history between them.

```py
def longestCommonHistory(user1, user2):
  result = []
  dp = [[0 for _ in range(len(user2) + 1)] for _ in range(len(user1) + 1)]
  for i in range(len(user1)):
    for j in range(len(user2)):
      if user1[i] != user2[j]:
        continue

      dp[i + 1][j + 1] = dp[i][j] + 1
      length = dp[i + 1][j + 1]
      if length > len(result):
        result = user1[i - length + 1:i + 1]

  return result
```

## Question 3

Given a list of user ids + IPs, a list of user ids who have made purchases, a list of advertisement clicks with user IPs. For each ad, output the number of clicks and the number of purchases

```py
from collections import defaultdict

def ad_conversion(user_ids, ad_clicks, user_ips):
  user_ids = set(user_ids)
  ip_user = {}
  for user_ip in user_ips:
    user, ip = user_ip.split(',')
    ip_user[ip] = user

  ads = defaultdict(list)
  for ad_click in ad_clicks:
    ip, _, text = ad_click.split(',')
    ads[text].append(ip)

  result = []
  for text, clicks in ads.items():
    count = 0
    for ip in clicks:
      if ip in ip_user and ip_user[ip] in users:
        count += 1
    result.append("{} of {} {}".format(count, len(clicks), text))
  return result
```
