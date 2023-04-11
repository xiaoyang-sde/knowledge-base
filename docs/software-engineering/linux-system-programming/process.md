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

## Users and Groups

The user and group identifiers are numeric values represented as `uid_t` and `gid_t`. In a Linux system, a process's uid and gid dictate the operations that the process could undertake.

There are four uids associated with a process: the real, effective, saved, and filesystem uid.

- The real uid is the uid of the user who ran the process, which is derived from the process's parent and doesn't change during an `execve` call. The `login` process sets the real uid of the shell.
- The effective uid is the uid that the process is wielding, which is used for permission verification. The process can change its effective uid with a `setuid` executable. The `setuid` executable is a type of permission on a file that allows it to be ran with the permission of the owner of the file.
- The saved uid is the process's original effective uid.

## Session and Process Group

Each process is a member of a process group, which is a collection of one or more processes associated for the purpose of job control. The attribute of a process group is that signals can be sent to all processes in the group.

Each process group has a pgid and has a process group leader. The pgid is equal to the pid of the process group leader. Even if the process group leader terminates, the process group continues to exist.

There are multiple sessions in a Linux system. One for each user login session and others for processes not tied to user login sessions, such as daemons. Each of these sessions contains one or more process groups, and each process group contains at least one process.

When a new user first logs into a machine, the login process creates a new session that consists of a single process, the user's login shell, which the session leader. The pid of the session leader is used as the session ID. Process groups in a session are divided into a single foreground process group and zero or more background process groups.

- When a user exits a terminal, a `SIGQUIT` is sent to all processes in the foreground process group.
- When a network disconnect is detected, a `SIGHUP` is sent to all processes in the foreground process group.
- When the user enters `Ctrl-C`, a `SIGINT` is sent to all processes in the foreground process group.

## Daemon

The daemon is a process that runs in the background, not connecting to a controlling terminal. Some daemons are started at boot time, are run as root or some other special user, and handle system-level tasks. The daemon process runs as a child of `init`.

The `daemon()` function in `glibc` creates a daemon with a sequence of forking a new child, terminating the parent process, creating a new session, changing the working directory to `/`, and closing unused file descriptors.

```c
#include <unistd.h>

int daemon(int nochdir, int noclose);
```

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

- `setuid()` sets the effective uid of the current process. For a non-root process, the `uid` parameter must be either the effective or the saved uid. For a root process, the real and saved uids are set.
- `seteuid()` sets the effective uid of the current process. For a root process, the real and saved uids are not set.

```c
#include <sys/types.h>
#include <unistd.h>

uid_t getuid(void);
uid_t geteuid(void);
int setuid(uid_t uid);
int seteuid(uid_t euid);

gid_t getgid(void);
gid_t getegid(void);
int setgid(gid_t gid);
int setegid(gid_t egid);
```

- `setsid()` creates a new process group inside of a new session and makes the invoking process the leader of both.
- `setpgid()` sets the pgid of the process `pid` to `pgid`.

```c
#include <unistd.h>

pid_t getsid(pid_t pid);
pid_t setsid(void);

pid_t getpgid(pid_t pid);
pid_t setpgid(pid_t pid, pid_t pgid);
```
