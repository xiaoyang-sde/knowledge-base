# Coroutine

## Introduction

The normal function provides the call operation and the return operation. The call operation creates an activation frame, suspends execution of the calling function and transfers execution to the start of the function being called. The return operation passes the return-value to the caller, deallocates the activation frame and then resumes execution of the caller. The activation frame of a normal function is allocated on stack, which contains parameters, local variables, the return address, and the address of the caller's activation frame.

The coroutine is a function that could suspend execution to be resumed later, which provides the suspend operation, the resume operation, and the destroy operation. Since coroutines could be suspended without deallocating the activation frame, the lifetime of the activation frame are not nested. The frame on heap holds part of the coroutine's activation frame that persists while the coroutine is suspended and the frame on stack exists while the coroutine is executing and is freed when the coroutine suspends and transfers execution back to the caller or resumer.

- Suspend: The suspend operation of a coroutine allows the coroutine to suspend execution and transfer execution back to the caller or resumer of the coroutine. The suspend-points are represented with the `co_await` and `co_yield` operators. When a coroutine hits a suspend-point, it writes the registers to the activation frame, stores an identifier of the current suspend-point, which allows a resume operation to know where to resume execution of the coroutine. The coroutine could execute logic after the coroutine enters the suspended state, which allows the coroutine to be scheduled for resumption without the need for synchronization. The coroutine could choose to either resume execution of the coroutine or transfer execution back to the caller or resumer.
- Resume: The resume operation could be performed on a suspended coroutine. The coroutine-frame handle provides a `resume()` function, which will transfer execution to the point in the function at which it was last suspended. When the coroutine next suspends or runs to completion, the call to `resume()` will return and resume execution of the calling function.
- Destroy: The destroy operation could be performed on a suspended coroutine. The coroutine-frame handle provides a `destroy()` function, which will transfer execution to a code-path that calls the destructors of all local variables in-scope at the suspend-point and deallocates the activation frame.

## Awaiter and Awaitable

- The `Awaitable` interface specifies methods that control the semantics of a `co_await` expression. When a value is `co_await`ed, the code is translated into a series of calls to methods on the awaitable object that allow it to define whether to suspend the current coroutine, execute some logic after it has suspended to schedule the coroutine for later resumption, and execute some logic after the coroutine resumes to produce the result of the `co_await` expression.
- The `Awaiter` type is a type that implements the three special methods that are called as part of a `co_await` expression: `await_ready`, `await_suspend` and `await_resume`.

### `co_await`

The `co_await <expr>` operator could be used within the context of a coroutine. It suspends the coroutine and returns control to its caller or resumer.

Assume the promise object for the awaiting coroutine have type `Promise` and `promise` is an lvalue reference to the promise object for the coroutine. If the promise type `Promise` has a member named `await_transform`, then `<expr>` is first passed into a call to `promise.await_transform(<expr>)` to obtain the `Awaitable` value, `awaitable`. Otherwise, the result of evaluating `<expr>` is the `Awaitable` object, `awaitable`. If the `Awaitable` object, `awaitable`, has an operator `co_await()` overload, it's called to obtain the `Awaiter` object. Otherwise, `awaitable` is used as the awaiter object.

The `awaiter.await_ready()` is called, which is a short-cut to avoid the cost of suspension. If its result is `false`, the coroutine is suspended, and `awaiter.await_suspend()` is called with the coroutine handle representing the current coroutine. The function should schedule the coroutine to resume on some executor or to be destructed. Because the coroutine is suspended before entering `awaiter.await_suspend()`, the coroutine handle can be transferred across threads, with no additional synchronization.

- If `await_suspend()` returns `void` or `true`, control is returned to the caller or resumer of the current coroutine.
- If `await_suspend()` returns `false`, the current coroutine is resumed.
- If `await_suspend()` returns a coroutine handle, the coroutine represented with the handle is resumed.

After the coroutine resumes, `awaiter.await_resume()` is called and its result is the result of the whole `co_await expr` expression. If the coroutine was suspended in the `co_await` expression, the resume point is before the call to `awaiter.await_resume()`.

### Coroutine Handle

The `std::coroutine_handle<Promise>` type represents a non-owning handle to the coroutine frame and could be used to resume execution of the coroutine or to destruct the coroutine frame. It could also be used to get access to the coroutine’s promise object. The coroutine handle will be passed to `await_suspend()` and could be used to schedule the coroutine. In the `await_suspend()` method, once it's possible for the coroutine to be resumed on a separated thread, accessing `this` or the `std::coroutine_handle<Promise>::promise()` are unsafe because the coroutine could have been destructed.

## Promise

The Promise object defines and controls the behaviour of the coroutine itself. It implements methods that are called at specific points during execution of the coroutine. The compiler performs a few steps when a coroutine function is called.

The type of the promise object is determined from the signature of the coroutine with the `std::coroutine_traits` class. Let the return type of the coroutine be `return_type`, `std::coroutine_traits` returns the `return_type::promise_type` type alias.

```cpp
template <class, class...> struct coroutine_traits {};

template <class R, class... Args>
requires requires { typename R::promise_type; }
struct coroutine_traits<R, Args...> {
  using promise_type = typename R::promise_type;
};
```

### Allocation and Construction

The compiler allocates the coroutine frame with `operator new` to allocate a coroutine frame. The compiler is free to elide the allocation if it's able to determine that the lifetime of the coroutine frame is nested within the lifetime of the caller and it could see the size of coroutine frame required at the call-site.

The parameters are copied or moved to the coroutine frame. If the promise has a constructor that accepts lvalue references to each of the parameters, it's invoked. Otherwise, the promise is default-constructed. `promise_type.get_return_object()` is invoked to obtain the result to return to the caller when the coroutine first suspends or runs to completion.

### Initial Suspension

The `co_await promise_type.initial_suspend()` statement is evaluated and the result is discarded. This allows the author of the `promise_type` to control whether the coroutine should suspend before executing. `std::suspend_never` and `std::suspend_always` are trivial `noexcept` awaitables that suspends or executes the coroutine.

### Return from Coroutine

When the coroutine reaches a `co_return` statement, it is translated into either a call to `promise_type.return_void()` or `promise_type.return_value(<expr>)`. It causes all local variables with automatic storage duration to be destructed in reverse order of construction before then evaluating `co_await promise_type.final_suspend()`. If an exception propagates out of the coroutine then the exception is caught and the `promise_type.unhandled_exception()` method is called inside the `catch` block.

The final suspension allows the coroutine to execute some logic, such as publishing a result, signalling completion or resuming a continuation. It is undefined behaviour to `resume()` a coroutine that is suspended at the `final_suspend` point.
