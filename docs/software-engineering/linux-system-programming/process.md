# Process

Each process represents a running program, which contains a binary image, an instance of virtualized memory, kernel resources such as open files, a security context such as an associated user, and one or more threads. Each thread has its own virtualized processor, which includes a stack, processor state such as registers, and an instruction pointer. Threads all share the same memory address space.

## Process ID

Each process is represented by a unique identifier, the process ID, or pid. The pid is guaranteed to be unique at any single point in time. The pid is represented as the `pid_t` type, which is a `typedef` of `int`.

- pid `0`: the `idle` process, which is the process that the kernel runs when there are no other runnable processes
- pid `1`: the `init` process, which is the first process that the kernel executes after booting the system and is responsible for initializing the system, starting various services, and launching a login program

The kernel imposes a maximum pid value of `32768`, which can be modified with `/proc/sys/kernel/pid_max`. The kernel does not reuse pid values until it wraps around from the top, which guarantees the uniqueness of pid values in a short period of time.

## Process Hierarchy

The process that spawns a new process is known as the parent and the new process is known as the child. Except for the `init` process, each process is spawned from another process.

Each process is owned by a user and a group. To the kernel, users and groups are mere integer values. Each child process inherits its parent's user and group ownership.

Each process is a part of a process group, which expresses its relationship to other processes. Each child process inherits its parent's process group ownership. Each process could send signals to multiple processes in the same group at once.

## System Call

- `getpid()` returns the pid of the invoking process.
- `getppid()` returns the pid of the invoking process's parent.

```c
#include <sys/types.h>
#include <unistd.h>

pid_t getpid(void);

pid_t getppid(void);
```

- `execve()` replaces the current process image with a new one at `path`. The function doesn't return if the invocation is successful. `glibc` implements multiple variants of the system call:
  - The `l` variant takes the arguments as a list.
  - The `v` variant takes the arguments as a vector.
  - The `e` variant takes new environment variables.
  - The `p` variant takes a file name and searches it in the user's path.

```c
#include <unistd.h>

int execl(const char* path, const char* arg, ...);

int execlp(const char *file, const char *arg, ...);

int execle(const char *path, const char *arg, ..., char * const envp[]);

int execv(const char *path, char *const argv[]);

int execvp(const char *file, char *const argv[]);

int execve(const char *filename, char *const argv[], char *const envp[]);
```

- `fork()` creates a child process, identical in almost all aspects to the invoking process. Linux uses the copy-on-write optimization to mitigate the overhead of duplicating resources. It doesn't clone the resource until a consumer modifies it.
  - In the child, a successful invocation of `fork()` returns `0`.
  - In the parent, a successful invocation of `fork()` returns the pid of the child.

```c
#include <sys/types.h>
#include <unistd.h>

pid_t fork(void);
```

- `_exit()` terminates the process with a specific exit status. The function doesn't return if the invocation is successful.
  - The `glibc` implements an `exit()` function, which performs the following steps before invoking `_exit()`:
    - Call functions registered with `atexit()` or `on_exit()`
    - Flush all open standard I/O streams
    - Remove files created with `tmp-file()`
  - The `glibc` implements an `atexit()` function, which registers the given function to run during the normal process termination.

```c
#include <stdlib.h>
#include <unistd.h>

void _exit(int status);

int atexit(void (*function)(void));
```

- `wait()` obtains information about terminated children. When a child process exits, it enters the zombine state and waits for its parent to inquire about its status. It returns the pid of a terminated child. If the parent terminates before waiting for its children, these zombie processes are reparented to the `init` process. The `init` process waits on all of its children.

- `waitpid()` obtains information about a specific terminated child.

```c
#include <sys/types.h>
#include <sys/wait.h>

pid_t wait(int *status);
pid_t waitpid(pid_t pid, int *status, int options);

int WIFEXITED(status);
int WIFSIGNALED(status);
int WIFSTOPPED(status);
int WIFCONTINUED(status);

int WEXITSTATUS(status);
int WTERMSIG(status);
int WSTOPSIG(status);
int WCOREDUMP(status);
```

## Users and Groups

The user and group identifiers are numeric values represented as `uid_t` and `gid_t`. In a Linux system, a process's uid and gid dictate the operations that the process could undertake.
