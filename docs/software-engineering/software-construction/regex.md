# Regular Expression

The regular expression is a pattern that is matched against a subject string from left to right.

- Phone number: `[1-9]\d{2}-\d{3}-\d{4}`
- Match IPv4 address: `^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$`

## Syntax Cheatsheet

### Character Classes

- `.`: The single character except line terminators (Inside a character class, the dot loses its special meaning and matches a literal dot.)
- `\d`: Digits (`[0-9]`)
- `\D`: Non-digit characters (`^[0-9]`)
- `\w`: `[A-Za-z0-9_]`
- `\W`: `^[A-Za-z0-9_]`
- `\s`: Single white space character, including space, tab, form feed, line feed
- `\S`: Single character other than white space
- `\`: Escape character

### Boundary-type assertions

- `^`: The beginning of input
- `$`: The end of input
- `\b`: The word boundary

### Groups and ranges

- `x|y`: `x` or `y`
- `[xyz]`, `[a-c]`: The character class that matches any one of the enclosed characters
- `[^xyz]`: The negated character class that matches any character not in the enclosed brackets

### Quantifiers

- `x*: The preceding item 0 or more times
- `x+`: The precending item 0 or more times
- `x?`: The precending item 0 or 1 times
- `x{n}`: Exactly `n` occurrences of the preceding item
- `x{n,m}`, `x{n,}`, `x{,m}`: Occurrence range of the preceding item
- `?`: Non-greedy mode

## Extended Regular Expression

Basic and extended regular expressions are two variations on the syntax of the specified pattern. `sed` and `grep` use the basic regular expression by default.

In basic regular expressions, the characters `?`, `+`, `{`, `|`, `(`, and `)` lose their special meaning; instead use the backslashed versions `\?`, `\+`, `\{`, `\|`, `\(`, and `\)`. In extended syntax, these characters are special unless they are prefixed with backslash.
