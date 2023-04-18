# File I/O

File I/O is a fundamental operation in Linux system programming. Linux treats all devices, including disks, as files, and the file I/O system calls work for all types of files.

The file descriptor is an integer value that identifies an open file in a process. The file descriptor values range from `0` to the maximum number of open files allowed for the process, which can be queried and set with `getrlimit()` and `setrlimit()`. The first three file descriptors (`0`, `1`, and `2`) are reserved for standard input (`stdin`), standard output (`stdout`), and standard error (`stderr`).

- `open()` opens a file and returns a file descriptor.
  - `open()` creates a file if the `O_CREAT` flag is set. The permissions of the created file are specified in `mode`.
  - `open()` opens a file in non-blocking mode if the `O_NONBLOCK` flag is set. If there's no data to read, `read()` and `write()` will return `EAGAIN` instead of blocking.
  - `open()` opens a file in append mode if the `O_APPEND` flag is set. `write()` will append content at the current end of the file.
  - `open()` opens a file in synchorized mode if the `O_SYNC` flag is set. `write()` will forces an implicit `fsync()`.
  - `open()` opens a file in direct mode if the `O_DIRECT` flag is set. `read()` and `write()` will hint the kernel to minimize the presence of I/O management, such as the page cache.
- `close()` closes a file descriptor.

```c
#include <fcntl.h>

int open(const char* path, int flags, mode_t mode);
int close(int fd);
```

- `read()` reads the content of `fd` to `buf` and returns the number of bytes read, which could be less than count if the end of the file is reached or an error occurs.
- `write()` writes the content of `buf` to `fd` and returns the number of bytes written.

```c
#include <unistd.h>

ssize_t read(int fd, void* buf, size_t count);
ssize_t write(int fd, const void* buf, size_t count);
```

- `lseek()` sets the file offset for the file associated with a file descriptor. The `whence` parameter determines how the `offset` should be applied, which can be either `SEEK_SET`, `SEEK_CUR`, or `SEEK_END`.

```c
#include <unistd.h>

off_t lseek(int fd, off_t offset, int whence);
```

## Kernel Internal

### The Virtual Filesystem

The virtual filesystem is a layer in the Linux operating system kernel that provides an abstract interface for different types of file systems to interact with user-level applications. The VFS provides a set of common data structures, such as inodes and dentries, to represent file system objects across different file systems. Therefore, it's possible to perform common file operations, such as opening, reading, writing, and closing files, in a uniform pattern across all file systems.

### The Page Cache

The page cache is an in-memory cache of disk blocks, which is the first place that the kernel looks for filesystem data. Storing requested data in memory allows the kernel to fulfill subsequent requests for the same data from memory, avoiding repeated disk access. The kernel invokes the memory subsystem to read data from the disk only when it isn't found in the cache.

If the page cache has consumed all available free memory, the kernel can either prune the page cache by removing some cached pages, or it can swap some inactive virtual memory pages to disk to free up memory. The balance of caching and swapping is tuned via `/proc/sys/vm/swappiness`.

To support sequential access patterns, the kernel implements page cache readahead, which reads extra data off the disk and into the page cache following each read request. If the kernel notices that a process is consistently using the data that was read in via readahead, it grows the readahead window.

### Page Writeback

When a process issues a write request, the data is copied into a buffer in the page cache, and the buffer is marked dirty, denoting that the in-memory copy is newer than the on-disk copy. The flusher threads in the kernel perform the page writeback operation.

- When free memory shrinks below a configurable threshold, dirty buffers are written back to disk so that the now-clean buffers may be removed, freeing memory.
- When a dirty buffer ages beyond a configurable threshold, the buffer is written back to disk.

## Synchronized I/O

When invoking the `write()` system call, the data is written to the kernel buffer, rather than being written to disk. Synchronized I/O ensures that data is written to disk before an operation completes, which is useful for applications that require data to be durable, such as databases or file systems.

- `fsync()` ensures that all data associated with the file are written back to disk.
- `fdatasync()` ensures that all data and metadata associated with the file  that are required to access the file in the future are written back to disk.

```c
#include <unistd.h>

int fsync(int fd);
int fdatasync(int fd);
```

## User Buffered I/O

The block is an abstraction representing the smallest unit of storage on a filesystem. Inside the kernel, all filesystem operations occur in terms of blocks. Partial block operations are inefficient. Therefore, User-buffered I/O reads or writes data to a buffer in the user space, which is later flushed to disk in larger batches using system calls. The standard I/O (`stdio.h`) in `glibc` provides a platform-independent, user-buffering solution.

- `fopen()` opens a file with `mode` and returns a `FILE` pointer.
- `fclose()` closes a file.

```c
#include <stdio.h>

FILE* fopen(const char* filename, const char* mode);
int fclose(FILE* stream);
```

- `fgetc()` reads a character.
- `fgets()` reads an entire line.
- `fread()` reads `count` elements (each has a size of `size` bytes) from the file to `ptr`.

```c
int fgetc(FILE* stream);
char* fgets(char* str, int n, FILE* stream);
size_t fread(void* ptr, size_t size, size_t count, FILE* stream);
```

- `fgetc()` writes a character.
- `fgets()` writes an entire string.
- `fread()` writes `count` elements (each has a size of `size` bytes) from `ptr` to the file.

```c
int fputc(int ch, FILE* stream);
int fputs(const char* str, FILE* stream);
size_t fwrite(const void* ptr, size_t size, size_t count, FILE* stream);
```

- `fflush()` writes the user-buffered data to the kernel buffer.

```c
int fflush(FILE* stream);
```

## I/O Advice

- `posix_fadvise()` provides the kernel with advice on the expected file access patterns, which enables the kernel to optimize the file I/O operations based on the provided hints. The advices are documented in [posix_fadvise(2)](https://man7.org/linux/man-pages/man2/posix_fadvise.2.html).

```c
#include <fcntl.h>

int posix_fadvise(int fd, off_t offset, off_t len, int advice);
```

## Memory-Mapped File

- `mmap()` maps a file or a portion of a file into the process's virtual memory, which allows the file to be accessed as if it were an array in memory.
- `munmap()` removes the mapped region from the process's virtual memory.

```c
#include <sys/mman.h>
#include <fcntl.h>

void* mmap(void* addr, size_t length, int prot, int flags, int fd, off_t offset);
int munmap(void* addr, size_t length);
```
