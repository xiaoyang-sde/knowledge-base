'''
In an HTTP request, the Accept-Language header describes the list of
languages that the requester would like content to be returned in. The header
takes the form of a comma-separated list of language tags. For example:

Accept-Language: en-US, fr-CA, fr-FR

means that the reader would accept:

1. English as spoken in the United States (most preferred)
2. French as spoken in Canada
3. French as spoken in France (least preferred)

We're writing a server that needs to return content in an acceptable language
for the requester, and we want to make use of this header. Our server doesn't
support every possible language that might be requested (yet!), but there is a
set of languages that we do support. Write a function that receives two arguments:
an Accept-Language header value as a string and a set of supported languages,
and returns the list of language tags that that will work for the request. The
language tags should be returned in descending order of preference (the
same order as they appeared in the header).

In addition to writing this function, you should use tests to demonstrate that it's
correct, either via an existing testing system or one you create.

Examples:

parse_accept_language(
  "en-US, fr-CA, fr-FR", # the client's Accept-Language header, a string
  ["fr-FR", "en-US"] # the server's supported languages, a set of strings
)
returns: ["en-US", "fr-FR"]

parse_accept_language("fr-CA, fr-FR", ["en-US", "fr-FR"])
returns: ["fr-FR"]

parse_accept_language("en-US", ["en-US", "fr-CA"])
returns: ["en-US"]
'''
def parse_accept_language_1(client_header, server_languages):
  result = []
  server_languages = set(server_languages)
  client_languages = client_header.split(', ')
  for language in client_languages:
    if language in server_languages:
      result.append(language)
  return result

print(parse_accept_language_1("en-US, fr-CA, fr-FR", ["fr-FR", "en-US"]))
print(parse_accept_language_1("fr-CA, fr-FR", ["en-US", "fr-FR"]))
print(parse_accept_language_1("en-US", ["en-US", "fr-CA"]))

'''
Part 2

Accept-Language headers will often also include a language tag that is not
region-specific - for example, a tag of "en" means "any variant of English". Extend
your function to support these language tags by letting them match all specific
variants of the language.

Examples:

{
  'fr': {'fr-CA', 'fr-FR'},
  'en': {'en-US'},
}
parse_accept_language("en", ["en-US", "fr-CA", "fr-FR"])
returns: ["en-US"]

parse_accept_language("fr", ["en-US", "fr-CA", "fr-FR"])
returns: ["fr-CA", "fr-FR"]

parse_accept_language("fr-FR, fr", ["en-US", "fr-CA", "fr-FR"])
returns: ["fr-FR", "fr-CA"]
'''
from collections import defaultdict

def parse_accept_language_2(client_header, server_languages):
  server_tags = defaultdict(set)
  for language in server_languages:
    tag, _ = language.split('-')
    server_tags[tag].add(language)

  result = []
  client_languages = client_header.split(', ')
  for language in client_languages:
    tag = language.split('-')[0]
    if '-' in language:
      if language in server_tags[tag]:
        result.append(language)
        server_tags[tag].remove(language)
    else:
      result.extend(server_tags[tag])
      del server_tags[tag]

  return result

print('--------')
print(parse_accept_language_2("en", ["en-US", "fr-CA", "fr-FR"]))
print(parse_accept_language_2("fr", ["en-US", "fr-CA", "fr-FR"]))
print(parse_accept_language_2("fr-FR, fr", ["en-US", "fr-CA", "fr-FR"]))

'''
Part 3

Accept-Language headers will sometimes include a "wildcard" entry, represented
‍‍‍‌‍‍‍‍‌‌‌‍‍‌‍‌‌‍‍
by an asterisk, which means "all other languages". Extend your function to
support the wildcard entry.

Examples:

parse_accept_language("en-US, *", ["en-US", "fr-CA", "fr-FR"])
returns: ["en-US", "fr-CA", "fr-FR"]

parse_accept_language("fr-FR, fr, *", ["en-US", "fr-CA", "fr-FR"])
returns: ["fr-FR", "fr-CA", "en-US"]
'''
from collections import defaultdict

def parse_accept_language_3(client_header, server_languages):
  server_tags = defaultdict(set)
  for language in server_languages:
    tag, _ = language.split('-')
    server_tags[tag].add(language)

  result = []
  client_languages = client_header.split(', ')
  for language in client_languages:
    tag = language.split('-')[0]
    if '-' in language:
      if language in server_tags[tag]:
        result.append(language)
        server_tags[tag].remove(language)
    elif tag == '*':
      for tag in server_tags:
        result.extend(server_tags[tag])
      return result
    else:
      result.extend(server_tags[tag])
      del server_tags[tag]

  return result

print('--------')
print(parse_accept_language_3("en-US, *", ["en-US", "fr-CA", "fr-FR"]))
print(parse_accept_language_3("fr-FR, fr, *", ["en-US", "fr-CA", "fr-FR"]))
print(parse_accept_language_3("*", ["en-US", "fr-CA", "fr-FR"]))

'''
Part 4

Accept-Language headers will sometimes include explicit numeric weights (known as
q-factors) for their entries, which are used to designate certain language tags
as specifically undesired. For example:

Accept-Language: fr-FR;‍‍‍‌‍‍‍‍‌‌‌‍‍‌‍‌‌‍‍q=1, fr;q=0.5, fr-CA;q=0

This means that the reader most prefers French as spoken in France, will take
any variant of French after that, but specifically wants French as spoken in
Canada only as a last resort. Extend your function to parse and respect q-factors.

Examples:

parse_accept_language("fr-FR;q=1, fr-CA;q=0, fr;q=0.5", ["fr-FR", "fr-CA", "fr-BG"])
returns: ["fr-FR", "fr-BG", "fr-CA"]

parse_accept_language("fr-FR;q=1, fr-CA;q=0, *;q=0.5", ["fr-FR", "fr-CA", "fr-BG", "en-US"])
returns: ["fr-FR", "fr-BG", "en-US", "fr-CA"]

parse_accept_language("fr-FR;q=1, fr-CA;q=0.8, *;q=0.5", ["fr-FR", "fr-CA", "fr-BG", "en-US"])
'''
from collections import defaultdict

def parse_accept_language_4(client_header, server_languages):
  server_tags = defaultdict(set)
  for language in server_languages:
    tag, _ = language.split('-')
    server_tags[tag].add(language)

  langauge_weight = { '*': 1 }
  result = []
  client_languages = client_header.split(', ')
  for locale_identifier in client_languages:
    if ';q=' in locale_identifier:
      language, weight = locale_identifier.split(';q=')
      langauge_weight[language] = float(weight)
    else:
      language = locale_identifier

    tag = language.split('-')[0]
    if '-' in language:
      if language in server_tags[tag]:
        result.append(language)
        server_tags[tag].remove(language)
    elif tag == '*':
      for tag in server_tags:
        result.extend(server_tags[tag])
      server_tags = {}
    else:
      result.extend(server_tags[tag])
      del server_tags[tag]

  def get_weight(language):
    if language in langauge_weight:
      return langauge_weight[language]

    tag, _ = language.split('-')
    if tag in langauge_weight:
      return langauge_weight[tag]

    return langauge_weight['*']

  return sorted(result, reverse=True, key=get_weight)

print('--------')
print(parse_accept_language_4("fr-FR;q=1, fr-CA;q=0, fr;q=0.5", ["fr-FR", "fr-CA", "fr-BG"]))
print(parse_accept_language_4("fr-FR;q=1, fr-CA;q=0, *;q=0.5", ["fr-FR", "fr-CA", "fr-BG", "en-US"]))
print(parse_accept_language_4("fr-FR;q=1, fr-CA;q=0.8, *;q=0.5", ["fr-FR", "fr-CA", "fr-BG", "en-US"]))

print('--------')
print(parse_accept_language_4("en-US, *", ["en-US", "fr-CA", "fr-FR"]))
print(parse_accept_language_4("fr-FR, fr, *", ["en-US", "fr-CA", "fr-FR"]))
print(parse_accept_language_4("*", ["en-US", "fr-CA", "fr-FR"]))
