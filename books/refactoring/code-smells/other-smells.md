# Other Smells

Below are the smells which don’t fall into any broad category.

## Incomplete Library Class

Sooner or later, libraries stop meeting user needs. The only solution to the problem—changing the library—is often impossible since the library is read-only.

### Treatment

To introduce a few methods to a library class, use Introduce Foreign Method.

For big changes in a class library, use Introduce Local Extension.

