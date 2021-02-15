# Change Preventers

These smells mean that if you need to change something in one place in your code, you have to make many changes in other places too.

## Shotgun Surgery

Making any modifications requires that you make many small changes to many different classes. A single responsibility has been split up among a large number of classes.

### Treatment

* Move Method and Move Field
* Inline Class

## Divergent Change

You find yourself having to change many unrelated methods when you make changes to a class.

### Treatment

* Extract Class
* Extract Superclass and Extract Subclass

## Parallel Inheritance Hierarchies

Whenever you create a subclass for a class, you find yourself needing to create a subclass for another class.

### Treatment

First, make instances of one hierarchy refer to instances of another hierarchy.

Then, remove the hierarchy in the referred class, by using Move Method and Move Field.

