# Optimizing Program Performance

## Capabilities and Limitations of Optimizing Compilers

Compilers must be careful to apply only safe optimizations to a program. The resuling program must have the exact same behavior as the unoptimized version.

- Memory aliasing: Two pointers may designate the same memory location.
- Side effect: The function that modifies some part of the global program state.

## Expressing Program Performance

The CPE, or cycles per element, is a metric to express program performance that could be used to improve the code. The measurement is expressed in clock cycles.

## Program Example

The definition of the `vector` data structure:

```cpp
typedef struct {
  long len;
  data_t *data; // type of the underlying element
} vec_rec, *vec_ptr;
```

```cpp
void combine1(vec_ptr v, data_t *dest) {
  long i;
  *dest = IDENT;
  for (i = 0; i < vec_length(v); i++) {
    data_t val;
    get_vec_element(v, i, &val);
    *dest = *dest OP val;
  }
}
```

## Eliminating Loop Inefficiencies

The test codition is evaluated on every iteration of the loop. Therefore, the function `vec_length(v)` is called on every iteration.

Code motion optimization involves identifying a computation that is performed multiple times. In this case, we move the computation of `vec_length(v)` from within the loop to before the loop.

```cpp
void combine2(vec_ptr v, data_t *dest) {
  long i;
  long length = vec_length(v);

  *dest = IDENT;
  for (i = 0; i < length; i++) {
    data_t val;
    get_vec_element(v, i, &val);
    *dest = *dest OP val;
  }
}
```

The compiler won't attempt to perform code motion, since it can't determine whether the function `vec_length(v)` has side effects. If so, the behavior of `combine2` will be different from `combine1`.

## Reducing Procedure Calls

Procedure calls can incur overhead and block most forms of program optimization.

The `get_vec_start` returns the starting address of the data array. Rather than making a function call to retrieve each vector element, it accesses the array directly. However, it violates the principle of abstract data type, since it shouldn't know the detailed implementation.

```cpp
data_t *get_vec_start(vec_ptr v) {
  return v->data;
}

void combine3(vec_ptr v, data_t *dest) {
  long i;
  long length = vec_length(v);
  data_t *data = get_vec_start(v);

  *dest = IDENT;
  for (i = 0; i < length; i++) {
    *dest = *dest OP data[i];
  }
}
```

## Eliminating Unneeded Memory References

The accumulated value is read from and written to memory on each iteration. These operations could be eliminated by adding a temporary variable `acc` to accumulate the computed value. The compiler won't attempt to elimnimate memory references due to memory aliasing.

```cpp
void combine4(vec_ptr v, data_t *dest) {
  long i;
  long length = vec_length(v);
  data_t *data = get_vec_start(v);
  data_t acc = IDENT;

  for (i = 0; i < length; i++) {
    acc = acc OP data[i];
  }

  *dest = acc;
}
```

## Loop Unrolling

Loop unrolling is a program transformation that reduces the number of iterations for a loop by increasing the number of elements computed on each iteration.

- Reduce the number of opreations that don't contribute directly to the result of the program
- Expose ways that could transform the code to reduce the number of operations in the critical path

'k x 1 loop unrolling' refers to the transformation that unroll by a factor of `k` but accumulate values in a single variable.

```cpp
void combine5(vec_ptr v, data_t *dest) {
  long i;
  long length = vec_length(v);
  long limit = length - 1;
  data_t *data = get_vec_start(v);
  data_t acc = IDENT;

  // Combine 2 elements at a time
  for (i = 0; i < limit; i+=2) {
    acc = (acc OP data[i]) OP data[i + 1];
  }

  // Unroll remaining elements
  for (; i < length; i++) {
    acc = acc OP data[i];
  }

  *dest = acc;
}
```

The number of overhead operations is reduced, and then the latency bound of 1.00 becomes the performance limiting factor.

## Enhancing Parallelism

### Modern CPU Design

Definition: Most modern CPU are superscalar. A superscalar processor can issue and execut multiple instructions in one cycle. The instructions are retrieved from a sequential instruction stream and are scheduled dynamically.

Benifit: Without programming effort, superscalar processor can take advantage of the instruction level parallelism.

Pipelined functional units divides computation into stages and passes partial computation from stage to stage. Stage `i` can start on new computation once values passed to `i + 1`.

### Multiple Accumulators

'k x l' loop unrolling refers to the transformation that unroll by a factor of `k` but accumulate values in `l` variables. (`k` must be multiple of `l`)

```cpp
void combine6(vec_ptr v, data_t *dest) {
  long i;
  long length = vec_length(v);
  long limit = length - 1;
  data_t *data = get_vec_start(v);
  data_t acc0 = IDENT;
  data_t acc1 = IDENT;

  // Combine 2 elements at a time
  for (i = 0; i < limit; i+=2) {
    acc0 = acc0 OP data[i];
    acc1 = acc1 OP data[i + 1];
  }

  // Unroll remaining elements
  for (; i < length; i++) {
    acc = acc OP data[i];
  }

  *dest = acc0 OP acc1;
}
```

With multiple accumulators, `acc0` and `acc1` could be computed in parallel, thus reduce the critical path of the program by a factor of 2.

### Reassociation Transformation

Reassociation transformation shifts the order in which the vector elements are combined with the accumulated value `acc`, yielding a form of '2 x 1a' loop unrolling.

By a very small change in the code of `combine5`, the way of combining could be fundamentally changed and thus increase the performance.

```cpp
// combine5
acc = (acc OP data[i]) OP data[i + 1];

//combine7
acc = acc OP (data[i] OP data[i + 1]);
```

With reassociation transformation, the `(data[i] OP data[i + 1])` and `acc OP (data[i] OP data[i + 1])` could be computed in parallel, thus reduce the critical path of the program by a factor of 2.
