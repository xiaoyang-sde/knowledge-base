# Object-Orientation Abusers

All these smells are incomplete or incorrect application of object-oriented programming principles.

## Alternative Classes with Different Interfaces

Two classes perform identical functions but have different method names. The programmer who created one of the classes probably didn’t know that a functionally equivalent class already existed.

### Treatment

- Rename Methods
- Move Method

### Payoff

You get rid of unnecessary duplicated code, making the resulting code less bulky.

## Refused Bequest

If a subclass uses only some of the methods and properties inherited from its parents, the hierarchy is off-kilter.

Someone was motivated to create inheritance between classes only by the desire to reuse the code in a superclass. But the superclass and subclass are completely different. (Dog inhert from Chair because they both have 4 legs.)

### Treatment

- Replace Inheritance with Delegation.
- Get rid of unneeded fields and methods in the subclass.

## Temporary Field

Temporary fields get their values (and thus are needed by objects) only under certain circumstances. Outside of these circumstances, they’re empty.

- Temporary fields and all code operating on them can be put in a separate class via Extract Class.

## Switch Statements

You have a complex `switch` operator or sequence of `if` statements.

