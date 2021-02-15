# Bloaters

Bloaters are code, methods and classes that have increased to such gargantuan proportions that they are hard to work with. Usually these smells do not crop up right away, rather they accumulate over time as the program evolves \(and especially when nobody makes an effort to eradicate them\).

## Long Method

### Signs and Symptoms

A method contains too many lines of code. Generally, any method longer than ten lines should make you start asking questions. The longer a method or function is, the harder it becomes to understand and maintain it.

### Treatment

If you feel the need to comment on something inside a method, you should take this code and put it in a new method. Even a single line can and should be split off into a separate method, if it requires explanations.

### Performance

In almost all cases the impact of increasing amount of methods is so negligible that it’s not even worth worrying about.

## Large Class

### Signs and Symptoms

A class contains many fields/methods/lines of code.

### Treatment

* Extract Class
* Extract Subclass
* Extract Interface

## Primitive Obsession

### Signs and Symptoms

* Use of primitives instead of small objects for simple tasks \(such as currency, ranges, special strings for phone numbers, etc.\)
* Use of constants for coding information.
* Use of string constants as field names for use in data arrays.

### Treatment

If you have a large variety of primitive fields, it may be possible to logically group some of them into their own class.

## Long Parameter List

### Signs and Symptoms

More than three or four parameters for a method.

A long list of parameters might happen after several types of algorithms are merged in a single method. A long list may have been created to control which algorithm will be run and how.

### Treatment

Instead of passing a group of data received from another object as parameters, pass the object itself to the method.

If there are several unrelated data elements, sometimes you can merge them into a single parameter object.

However, don't get rid of parameters if doing so would cause unwanted dependency between classes.

## Data Clumps

### Signs and Symptoms

Sometimes different parts of the code contain identical groups of variables \(such as parameters for connecting to a database\). These clumps should be turned into their own classes.

However, passing an entire object in the parameters of a method, instead of passing just its values \(primitive types\), may create an undesirable dependency between the two classes.

### Treatment

If repeating data comprises the fields of a class, use Extract Class to move the fields to their own class.

If the same data clumps are passed in the parameters of methods, use Introduce Parameter Object to set them off as a class.

