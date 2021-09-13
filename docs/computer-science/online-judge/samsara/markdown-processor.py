'''
We're going to build the beginnings of a markdown processor.
Markdown is a markup language that allows you to easily create HTML.
We ll provide some sample input and desired output.
Dont worry too much about edge cases, but feel free to ask if you
are unsure or think there's something we ought to consider.
'''

def markdown_to_html(text):
  stack = ['<p>']
  result = '<p>'
  index = 0
  while index < len(text):
    char = text[index:index + 1]
    next_char = text[index + 1:index + 2]

    if char == '\n':
      if next_char == '\n':
        if stack[-1] == '<blockquote>':
          stack.pop()
          result += '</blockquote>'

        stack.pop()
        stack.append('<p>')
        result += '</p><p>'
        index += 2
      else:
        result += '<br />'
        index += 1

    elif char == '>' and next_char == ' ':
      if stack[-1] != '<blockquote>':
        stack.append('<blockquote>')
        result += '<blockquote>'
      index += 2

    elif char == next_char == '~':
      if stack[-1] == '<del>':
        stack.pop()
        result += '</del>'
      else:
        stack.append('<del>')
        result += '<del>'
      index += 2

    else:
      result += char
      index += 1

  result = result + '</p>'
  result = result.replace('<p>', '\n<p>\n')
  result = result.replace('</p>', '\n</p>\n')
  return result

input_text = '''This is a paragraph with a soft
line break.

This is another paragraph that has
> Some text that
> is in a
> block quote.

This is another paragraph with a ~~strikethrough~~ word.'''

print(markdown_to_html(input_text))
