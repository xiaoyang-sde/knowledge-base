# File Management

## Files

Each file in Linux is represetend as an inode. The inode stores the metadata associated with a file, such as the file's access permissions, last access timestamp, owner, group, and size, as well as the location of the file's data. Linux provides functions that returns information about a file. The definition of the `stat` structure is documented in [lstat(2)](https://man7.org/linux/man-pages/man2/lstat.2.html).

- `stat()` returns information about a file in `path`.
- `lstat()` returns information about a file in `path` without following symbolic links.
- `fstat()` returns information about a file represented as `fd`.

```c
#include <sys/types.h>
#include <sys/stat.h>
#include <unistd.h>

int stat(const char* path, struct stat* buf);
int lstat(const char* path, struct stat* buf);
int fstat(int fd, struct stat* buf);
```

- `chmod()` changes the mode of a file in `path`.
- `fchmod()` changes the mode of a file represented as `fd`.

```c
int chmod(const char* path, mode_t mode);
int fchmod(int fd, mode_t mode);
```

- `chown()` changes the ownership of a file in `path`.
- `lchown()` changes the ownership of a file in `path` without following symbolic links.
- `fchown()` changes the ownership of a file represented as `fd`.

```c
int chown(const char* path, uid_t owner, gid_t group);
int lchown(const char* path, uid_t owner, gid_t group);
int fchown(int fd, uid_t owner, gid_t group);
```

## Directories

Each directory in Linux is a list of filenames, each of which maps to an inode number. Each name is called a directory entry, and each name-to-inode mapping is called a link. Directories can contain other directories. Each directory contains the `.` directory and the `..` directory.

- The absolute pathname is a pathname that begins with `/`.
- The relative pathname is a pathname that does not begin with `/`.

Each process has a current working directory, which it initially inherits from its parent process.

- `getcwd()` returns the current working directory.
- `chdir()` changes the current working directory to `path`.
- `fchdir()` changes the current working directory represented as `fd`.

```c
#include <unistd.h>

char* getcwd (char* buf, size_t size);

int chdir(const char* path);
int fchdir(int fd);
```

- `mkdir()` creates a directory at `path` with `mode`. The current umask modifies the `mode` argument.
- `rmdir()` removes a directory at `path`. The directory must be empty.

```c
int mkdir(const char* path, mode_t mode);
int rmdir(const char* path);
```

- `opendir()` creates a directory stream representing the directory `name`. The directory stream is a file descriptor representing the open directory, some metadata, and a buffer to hold the directory's contents.
- `readdir()` returns the next entry in the directory stream and returns `NULL` when all entries are processed.
- `closedir()` closes the directory stream,

```c
#include <sys/types.h>
#include <dirent.h>

struct dirent {
  ino_t d_ino;
  off_t d_off;
  unsigned short d_reclen;
  unsigned char d_type;
  char d_name[256];
};

DIR* opendir(const char* name);
struct dirent* readdir(DIR* dir);
int closedir(DIR *dir);
```

## Links

Each name-to-inode mapping is a hard link. For each file, there can be at most `INT_MAX` hard links. The file is removed when the usage count (number of processes that opens the file) and the link count are both `0`.

- `link()` creates a new hard link under the path `new_path` for the existing file `old_path`.
- `symlink()` creates a new symbolic link under the path `new_path` for the existing file `old_path`.
- `unlink()` removes a hard link from the file system without following symbolic links.

```c
int link(const char* old_path, const char* new_path);
int symlink(const char* old_path, const char* new_path);

int unlink(const char* path_name);
```

- `rename()` renames the file name from `old_path` to `new_path`.

```c
int rename(const char *old_path, const char* new_path);
```

## Device Nodes

Device nodes are special files that allow applications to interface with device drivers. For I/O requests on a device node, the kernel passes such requests to a device driver. Device nodes provide device abstraction so that applications do not need to be familiar with device specifics.

Each device node is assigned two numerical values, called a major number and a minor number. These major and minor numbers map to a specific device driver loaded into the kernel.

- The null device lives at `/dev/null` and has a major number of `1` and a minor number of `3`. All read requests to the file return EOF and all write requests are discarded.
- The zero device lives at `/dev/zero` and has a major of `1` and a minor of `5`. All read requests return an infinite stream of `\0` and all write requests are discarded.
- The full device lives at `/dev/full` and has a major of `1` and a minor of `7`. All read requests return an infinite stream of `\0` and all write requests return `ENOSPC`.
