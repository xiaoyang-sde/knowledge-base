# Coroutine

The coroutine is a function that can suspend execution to be resumed later. In C++, the coroutine is stackless, which means that it returns to the caller when suspending execution, and the state required to resume is stored on heap. It allows sequential code to handle non-blocking I/O without explicit callbacks. The function is a coroutine if it contains either of the `co_await` expression, `co_yield` expression, or `co_return` statement.

- Each coroutine is associated with the promise object, manipulated from inside the coroutine. The coroutine submits its result or exception through this object. The compiler determines the type of the promise object with `std::coroutine_traits`.
- Each coroutine is associated with the coroutine handle, manipulated from outside the coroutine. The handle is used to resume or destruct the coroutine.
- Each coroutine is associated with the coroutine state object allocated on heap, which contains the promise object, the parameters, the state of the current suspension point, and variables whose lifetime spans the suspension point.

## Execution

1. The coroutine allocates the coroutine state object and moves or copies all function parameters to the coroutine state.

2. The coroutine constructs the promise object and creates a return object with `promise.get_return_object()`. The return object will be returned to the caller when the coroutine first suspends.

3. The coroutine calls `promise.initial_suspend()` and `co_await` its result.

4. The actual code of the coroutine is executed.

5. When the coroutine reaches a suspension point, the return object is returned to the caller or resumer.

6. When a coroutine reaches the `co_return` statement, it calls `promise.return_void()` or `promise.return_value(expr)` if `expr` has non-`void` type, and then calls `promise.final_suspend()` and `co_await` the result.

7. If the coroutine ends with an uncaught exception, it catches the exception and calls `promise.unhandled_exception()` from within the `catch`-block.

## `co_await`

The operator `co_await` suspends a coroutine and returns control to the caller. Its operand is an expression whose type must either define operator `co_await`, or be convertible to such type with the current coroutine's `promise_type::await_transform()`.

The `awaiter.await_ready()` is called, which is a short-cut to avoid the cost of suspension. If its result is `false`, then the coroutine is suspended, `awaiter.await_suspend()` is called with the coroutine handle representing the current coroutine. The function should schedule it to resume on some executor or to be destructed. Because the coroutine is suspended before entering `awaiter.await_suspend()`, the coroutine handle can be transferred across threads, with no additional synchronization.

- If `await_suspend()` returns `void`, control is returned to the caller or resumer of the current coroutine.
- If `await_suspend()` returns `true`, control is returned to the caller or resumer of the current coroutine.
- If `await_suspend()` returns `false`, the current coroutine is resumed.
- If `await_suspend()` returns a coroutine handle, the coroutine represented with the handle is resumed.

In the end, `awaiter.await_resume()` is called and its result is the result of the whole `co_await expr` expression. If the coroutine was suspended in the `co_await` expression, the resume point is before the call to `awaiter.await_resume()`.
