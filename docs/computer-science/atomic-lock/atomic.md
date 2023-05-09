# Atomic

Atomic is an operation that is indivisible. Atomic operations allow different thread to read and write the same variable. Since such an operation is indivisible, it either happens before or after another operation, avoiding undefined behavior. Atomic operations are the main building block for primitives involving multiple threads, such as mutexes and condition variables.

In Rust, atomic operations are available as methods on the standard atomic types that live in `std::sync::atomic`, such as `AtomicI32` or `AtomicUsize`. Atomic types allow modification through a shared reference. Each atomic operation takes an argument of type `std::sync::atomic::Ordering`, which determines the guarantees about the relative ordering of operations.

## Load and Store

```rs
impl AtomicI32 {
  pub fn load(&self, ordering: Ordering) -> i32;
  pub fn store(&self, value: i32, ordering: Ordering);
}
```

- The `load` method is an atomic operation that loads the value stored in the atomic variable.

- The `store` method is an atomic operation that stores a new value in it. This method takes a shared reference rather than an exclusive reference.

```rs
static STOP: AtomicBool = AtomicBool::new(false);

let thread = thread::spawn(|| {
  while !STOP.load(Relaxed) {
    thread::sleep(Duration::from_secs(1));
  }
});

thread::sleep(Duration::from_secs(5));
STOP.store(true, Relaxed);
thread.join().unwrap();
```

## Fetch

The fetch operation modifies the atomic variable and loads the original value in a single atomic operation. The return value from these operations might not relevant. `fetch_add` and `fetch_sub` implement wrapping behavior for overflows.

```rs
impl AtomicI32 {
  pub fn fetch_add(&self, v: i32, ordering: Ordering) -> i32;
  pub fn fetch_sub(&self, v: i32, ordering: Ordering) -> i32;
  pub fn fetch_or(&self, v: i32, ordering: Ordering) -> i32;
  pub fn fetch_and(&self, v: i32, ordering: Ordering) -> i32;
  pub fn fetch_nand(&self, v: i32, ordering: Ordering) -> i32;
  pub fn fetch_xor(&self, v: i32, ordering: Ordering) -> i32;
  pub fn fetch_max(&self, v: i32, ordering: Ordering) -> i32;
  pub fn fetch_min(&self, v: i32, ordering: Ordering) -> i32;
  pub fn swap(&self, v: i32, ordering: Ordering) -> i32;
}
```

## Compare and Exchange

The compare-and-exchange operation if the atomic value is equal to a given value and if that is the case does it replace it with a new value in a single atomic operation. It will return the previous value and whether it was replaced or not.

```rs
impl AtomicI32 {
  pub fn compare_exchange(
    &self,
    expected: i32,
    new: i32,
    success_order: Ordering,
    failure_order: Ordering
  ) -> Result<i32, i32>;
}
```

If the atomic variable changes from some value `A` to `B` and then back to `A` after the `load` operation, but before the `compare_exchange` operation, it would still succeed, even though the atomic variable was changed in the meantime.
