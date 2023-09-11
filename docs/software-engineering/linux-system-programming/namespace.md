# Namespace

The purpose of each Linux namespace is to wrap a particular global system resource in an abstraction that makes it appear to the processes within the namespace that they have their own isolated instance of the global resource.

- Mount namespace (`CLONE_NEWNS`) isolate the set of filesystem mount points for a group of processes.
- UTS namespace (`CLONE_NEWUTS`) isolate the `nodename` and `domainname` identifiers. The names are set using the `sethostname()` and `setdomainname()` system calls. The UTS namespaces feature allows each container to have its own hostname and NIS domain name.
- IPC namespace (`CLONE_NEWIPC`) isolate certain interprocess communication (IPC) resources, such as  POSIX message queues.
- PID namespace (`CLONE_NEWPID`) isolate the PID number space. It allows each namespace to have its own `init` process that manages system initialization tasks. Each process has a PId inside the namespace and the PID on the host system.
- Network namespace (`CLONE_NEWNET`) isolate the networking resources, such as network devices, IP addresses, IP routing tables, and port numbers.
- User namespace (`CLONE_NEWUSER`) isolate the UID and GID number spaces. It allows a process to have full root privileged for operations inside the user namespace.

## Namespace API

- `clone()` creates a child process. If one of the namespace flag is specified in the call, then a new namespace is created, and the new process is made a member of that namespace.

```rs
pub type CloneCb<'a> = Box<dyn FnMut() -> isize + 'a>;

pub unsafe fn clone(
    cb: CloneCb<'_>,
    stack: &mut [u8],
    flags: CloneFlags,
    signal: Option<c_int>
) -> Result<Pid, Errno>
```

Each type of namespace has a file stored in `/proc/PID/ns`. Each of these files is a special symbolic link that provides a kind of handle for performing certain operations on the namespace.

- `setns()` reassociates a thread with a namespace. The `fd` parameter refers to a file stored in `/proc/PID/ns`.

```rs
pub fn setns<Fd: AsFd>(fd: Fd, nstype: CloneFlags) -> Result<(), Errno>
```

- `unshare()` disassociates parts of the process execution context. If one of the namespace flag is specified in the call, then a new namespace is created, and the calling process is made a member of that namespace.

```rs
pub fn unshare(flags: CloneFlags) -> Result<(), Errno>
```
