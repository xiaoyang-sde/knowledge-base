# Signal

Signals are software interrupts that provide a mechanism for handling asynchronous events. These events can originate from outside the system from activities within the program or kernel. Each process can send signals to other processes. The signal-handling functions are registered with the kernel, which invokes the functions asynchronously from the rest of the program when the signals are delivered.

## Concept

Each signal has a symbolic name that starts with the prefix `SIG`.

- When a process invokes `fork()`, the child process inherits the signal actions of its parent and ignores pending signals.

- When a process invokes `exec()`, the process sets all signals to their default actions except for ignored signals and inherts pending signals.

When a signal is raised, the kernel stores the signal until it is able to deliver it and handles the signal as appropriate. The kernel can perform one of three actions, depending on what the process asked it to do:

- Ignore the signal: No action is taken. `SIGKILL` and `SIGSTOP` can't be ignored.

- Catch and handle the signal: The kernel will suspend execution of the process's execution path and jump to a registered function. Once the process returns from this function, it will jump back to the original execution path.

- Perform the default action: Some signals, such as `SIGKILL`, have a default action.

## Management

- `signal()` registers a signal handler to handle a specific signal `signo` and returns the previous signal handler. The handler could be function with the `sighandler_t` type, `SIG_IGN` (ignore), or `SIG_DFL` (default).

```c
#include <signal.h>

typedef void (*sighandler_t)(int);

sighandler_t signal(int signo, sighandler_t handler);
```

- `pause()` causes the calling process to sleep until a signal is delivered that either terminates the process or causes the invocation of a signal handler.

```c
#include <unistd.h>

int pause(void);
```

- `sigaction()` examines and changes a signal action of `signo`.
  - The `sa_handler` field dictates the action to take upon receiving the signal, which could be `SIG_DFL`, `SIG_IGN`, or a function pointer `void handler(int signo)`.
  - The `sa_sigaction` field accepts a function pointer `void handler(int signo, siginfo_t *si, void *ucontext)`, which is used is `SA_SIGINFO` is set in `sa_flags`.
  - The `sa_mask` field provides a set of signals that the system should block for the duration of the execution of the signal handler.
  - The `sa_flags` field takes a set of flags. The list of flags are documented in [sigaction(2)](https://man7.org/linux/man-pages/man2/sigaction.2.html).

```c
#include <signal.h>

struct sigaction {
  void (*sa_handler)(int);
  void (*sa_sigaction)(int, siginfo_t *, void *);
  sigset_t sa_mask;
  int sa_flags;
  void (*sa_restorer)(void); // deprecated
};

int sigaction (
  int signo,
  const struct sigaction *act,
  struct sigaction *oldact
);
```

## Sending a Signal

- `raise()` sends a signal to itself.

```c
#include <signal.h>

int raise(int signo);
```

- `kill()` sends a signal from one process to another process. If pid is `0`, `signo` is sent to all processes in the invoking process's process group. The sending process's effective or real uid must be equal to the real or saved uid of the receiving process. `CAP_KILL` is required to avoid this restriction.

- `killpg()` sends a signal from one process to all processes in a process group.

```c
#include <sys/types.h>
#include <signal.h>

int kill(pid_t pid, int signo);

int killpg(int pgrp, int signo);
```

- `sigqueue()` sends a signal with a payload `value` from one process to another process.

```c
#include <signal.h>

union sigval {
  int sival_int;
  void *sival_ptr;
};

int sigqueue(pid_t pid, int signo, const union sigval value);
```

## Reentrant Function

Reentrant function is a function that is safe to call from within itself. If a program is in the middle of executing a non-reentrant function and a signal occurs and the signal handler then invokes that same function, chaos can ensue.

When the kernel raises a signal, the signal handlers can't tell what code the process is executing. Signal handlers must make the assumption that the interrupted process is in the middle of a non-reentrant function. Therefore, the handler must invoke reentrant functions.
