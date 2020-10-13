# Couplers

All the smells in this group contribute to excessive coupling between classes or show what happens if coupling is replaced by excessive delegation.

## Feature Envy

A method accesses the data of another object more than its own data.

This smell may occur after fields are moved to a data class. If this is the case, you may want to move the operations on data to this class as well.

### Treatment

- Move Method
- Extract Method

## Inappropriate Intimacy

One class uses the internal fields and methods of another class.

## Message Chains

In code you see a series of calls resembling `$a->b()->c()->d()`.

### Treatment

- Hide Delegate
- Extract Method

## Middle Man

If a class performs only one action, delegating work to another class, why does it exist at all?

### Treatment

If most of a method’s classes delegate to another class, Remove Middle Man is in order.