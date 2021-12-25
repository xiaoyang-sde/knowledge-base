'''
- Follow-up: Create a trie with the english words, and determine if they are
'''

from collections import defaultdict, deque

class EncryptDecrypt:
  # Let n be the number of word, Time: O(n) -> O(26)
  def __init__(self, encrypt_decrypt):
    self.encrypt_decrypt = encrypt_decrypt
    self.decrypt_encrypt = defaultdict(list)
    for key, value in encrypt_decrypt.items():
      self.decrypt_encrypt[value].append(key)

  # Let n be the length of word, Time: O(n)
  def encrypt(self, word):
    result = ''
    for char in word:
      result += self.encrypt_decrypt[char]
    return result

  # Let n be the length of word, Time: O(26^n)
  def decrypt(self, word):
    result = deque(['']) # ([(''), trie_node])
    for i in range(0, len(word) - 1, 2):
      chars = word[i] + word[i + 1]
      for _ in range(len(result)):
        result_word = result.popleft() # result_word, trie_node
        for decrypt in self.decrypt_encrypt[chars]:
          result.append(result_word + decrypt) # if decrypt in trie_node.children()

    # for word, trie_node in result:
    #   if trie_node.is_word(): add to result
    #
    return list(result)

ed = EncryptDecrypt({
  'z': 'aw',
  'r': 'wo',
  't': 'wo',
})

print(ed.encrypt('zr'))
print(ed.decrypt('awwowowowoawwowo'))
