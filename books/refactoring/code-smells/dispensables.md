# Dispensables

A dispensable is something pointless and unneeded whose absence would make the code cleaner, more efficient and easier to understand.

## Comments

A method is filled with explanatory comments. If you feel that a code fragment can’t be understood without comments, try to change the code structure in a way that makes comments unnecessary. **The best comment is a good name for a method or class.**

However, comments can be useful when explaining why something is being implemented in a particular way or complex algorithms.

### Treatment

* Extract Variable
* Extract Method
* Rename Method
* Introduce Assertion

## Duplicate Code

Two code fragments look almost identical.

### Treatment

* Extract Variable
* Extract Method

## Lazy Class

Understanding and maintaining classes always costs time and money. So if a class doesn’t do enough to earn your attention, it should be deleted.

### Treatment

* Inline Class
* Collapse Hierarchy

## Data Class

A data class refers to a class that contains only fields and crude methods for accessing them \(getters and setters\). These are simply containers for data used by other classes.

## Dead Code

A variable, parameter, field, method or class is no longer used \(usually because it’s obsolete\).

### Treatment

* Delete unused code and unneeded files.
* Inline Class or Collapse Hierarchy can be applied if a subclass or superclass is used.
* To remove unneeded parameters, use Remove Parameter.

## Speculative Generality

There’s an unused class, method, field or parameter.

