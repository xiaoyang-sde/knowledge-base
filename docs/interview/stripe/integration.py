import json
import requests

from requests.auth import AuthBase

json_string = json.dumps([
  'foo',
  {
    'bar': ('baz', None, 1.0, 2)
  },
])
print(json.loads(json_string))

class CustomAuth(AuthBase):
  def __call__(self, request):
    request.headers['x-auth'] = 'test-password'
    return request

try:
  httpbin_get_request = requests.get(
    'https://httpbin.org/get',
    params={
      'key': 'value',
    },
    headers={
      'user-agent': 'my-app/0.0.1',
    },
    timeout=10,
    auth=CustomAuth(),
  )

  httpbin_get = httpbin_get_request.json()
  print(httpbin_get)
  print(httpbin_get_request.status_code)
  httpbin_get_request.raise_for_status()

  httpbin_post_request = requests.post(
    'https://httpbin.org/post',
    json={
      'key': 'value',
    },
  )
  print(httpbin_post_request.headers['content-type'])
  print(httpbin_post_request.cookies)
  print(httpbin_post_request.history)
  httpbin_post_request.raise_for_status()

except requests.HTTPError:
  pass

except requests.Timeout:
  pass
