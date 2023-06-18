# Coroutine

## Introduction

The normal function provides the call operation and the return operation. The call operation creates an activation frame, suspends execution of the calling function and transfers execution to the start of the function being called. The return operation passes the return-value to the caller, deallocates the activation frame and then resumes execution of the caller. The activation frame of a normal function is allocated on stack, which contains parameters, local variables, the return address, and the address of the caller's activation frame.

The coroutine is a function that could suspend execution to be resumed later, which provides the suspend operation, the resume operation, and the destroy operation. Since coroutines could be suspended without deallocating the activation frame, the lifetime of the activation frame are not nested. The frame on heap holds part of the coroutine's activation frame that persists while the coroutine is suspended and the frame on stack exists while the coroutine is executing and is freed when the coroutine suspends and transfers execution back to the caller or resumer.

- Suspend: The suspend operation of a coroutine allows the coroutine to suspend execution and transfer execution back to the caller or resumer of the coroutine. The suspend-points are represented with the `co_await` and `co_yield` operators. When a coroutine hits a suspend-point, it writes the registers to the activation frame, stores an identifier of the current suspend-point, which allows a resume operation to know where to resume execution of the coroutine. The coroutine could execute logic after the coroutine enters the 'suspended' state, which allows the coroutine to be scheduled for resumption without the need for synchronization. The coroutine could choose to either resume execution of the coroutine or transfer execution back to the caller or resumer.
- Resume: The resume operation could be performed on a suspended coroutine. The coroutine-frame handle provides a `resume()` function, which will transfer execution to the point in the function at which it was last suspended. When the coroutine next suspends or runs to completion, the call to `resume()` will return and resume execution of the calling function.
- Destroy: The destroy operation could be performed on a suspended coroutine. The coroutine-frame handle provides a `destroy()` function, which will transfer execution to a code-path that calls the destructors of all local variables in-scope at the suspend-point and deallocates the activation frame.

## Awaiter and Awaitable

- The `Awaitable` interface specifies methods that control the semantics of a `co_await` expression. When a value is `co_await`ed, the code is translated into a series of calls to methods on the awaitable object that allow it to define whether to suspend the current coroutine, execute some logic after it has suspended to schedule the coroutine for later resumption, and execute some logic after the coroutine resumes to produce the result of the `co_await` expression.
- The `Awaiter` type is a type that implements the three special methods that are called as part of a `co_await` expression: `await_ready`, `await_suspend` and `await_resume`.

The `co_await <expr>` operator could be used within the context of a coroutine. It suspends the coroutine and returns control to its caller or resumer.

1. Assume the promise object for the awaiting coroutine have type `Promise` and `promise` is an lvalue reference to the promise object for the coroutine. If the promise type `Promise` has a member named `await_transform`, then `<expr>` is first passed into a call to `promise.await_transform(<expr>)` to obtain the `Awaitable` value, `awaitable`. Otherwise, the result of evaluating `<expr>` is the `Awaitable` object, `awaitable`.

2. If the `Awaitable` object, `awaitable`, has an operator `co_await()` overload, it's called to obtain the `Awaiter` object. Otherwise, `awaitable` is used as the awaiter object.

## Execution

1. The coroutine allocates the coroutine state object and moves or copies all function parameters to the coroutine state.

2. The coroutine constructs the promise object and creates a return object with `promise.get_return_object()`. The return object will be returned to the caller when the coroutine first suspends.

3. The coroutine calls `promise.initial_suspend()` and `co_await` its result.

4. The actual code of the coroutine is executed.

5. When the coroutine reaches a suspension point, the return object is returned to the caller or resumer.

6. When a coroutine reaches the `co_return` statement, it calls `promise.return_void()` or `promise.return_value(expr)` if `expr` has non-`void` type, and then calls `promise.final_suspend()` and `co_await` the result.

7. If the coroutine ends with an uncaught exception, it catches the exception and calls `promise.unhandled_exception()` from within the `catch`-block.

## `co_await`

Its operand is an expression whose type must either define operator `co_await`, or be convertible to such type with the current coroutine's `promise_type::await_transform()`.

The `awaiter.await_ready()` is called, which is a short-cut to avoid the cost of suspension. If its result is `false`, then the coroutine is suspended, `awaiter.await_suspend()` is called with the coroutine handle representing the current coroutine. The function should schedule it to resume on some executor or to be destructed. Because the coroutine is suspended before entering `awaiter.await_suspend()`, the coroutine handle can be transferred across threads, with no additional synchronization.

- If `await_suspend()` returns `void`, control is returned to the caller or resumer of the current coroutine.
- If `await_suspend()` returns `true`, control is returned to the caller or resumer of the current coroutine.
- If `await_suspend()` returns `false`, the current coroutine is resumed.
- If `await_suspend()` returns a coroutine handle, the coroutine represented with the handle is resumed.

In the end, `awaiter.await_resume()` is called and its result is the result of the whole `co_await expr` expression. If the coroutine was suspended in the `co_await` expression, the resume point is before the call to `awaiter.await_resume()`.
