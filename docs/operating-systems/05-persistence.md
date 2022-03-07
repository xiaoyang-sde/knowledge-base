# Persistence

## File and Directory

### File

The file is a linear array of bytes with an associated inode number. The file descriptor is an integer, private per process, and is used in Linux systems to access files. The system tracks the current offset for each file a process opens, which determines where the next read or write will begin. The open file table represents the opened files in the system.

To create a file, the system creates an inode that tracks the file metadata, links a human-readable name to the file, and puts the link into a directory.

```c
struct file {
  int ref;
  char readable;
  char writable;
  struct inode *ip;
  uint off;
};

struct {
  struct spin_lock lock;
  struct file file[NFILE];
} open_file_table;
```

- The `open()` routine takes a number of different flags (e.g. `O_CREAT`, `O_WRONLY`, or `O_TRUNC`) and returns a file descriptor.
- The `read()` routine reads up to specific bytes from a file descriptor into a buffer.
- The `write()` routine writes up to specific bytes from the buffer to the file descriptor. The file system will buffer  writes in memory and writes to the storage device later.
- The `lseek()` routine repositions the file offset of the open file description.
- The `dup()` routine allocates a new file descriptor that refers to the same open file description as the specific descriptor.
- The `fsync()` routine flushes all modified data of the file descriptor to the storage device.
- The `fstat()` routine fills a `stat` structure for the file descriptor. The structure includes file metadata such as size, inode number, ownership information, etc.
- The `link()` routine creates a new hard link to an existing file. The hard link can't points to a directory or files in other disk partitions.
- The `unlink()` routine deletes a name from the file system. The file system checks a reference count that track the number of different file names have been linked to the inode. The file is removed if the reference count is 0.

### Directory

The directory is a list of (user-readable name, inode number) pairs with an associated inode number. The directory hierarchy starts at a root directory and uses the separator to name subsequent sub-directories until the desired file or directory is named.

```c
struct dirent {
  char d_name[256];         // filename
  ino_t d_ino;              // inode number
  off_t d_off;              // offset to the next dirent
  unsigned short d_reclen;  // length of this record
  unsigned char d_type;     // type of file
};
```

- The `mkdir()` routine creates a directory with specific name.
- The `rmdir()` routine removes a empty directory.
- The `opendir()` routine opens a directory stream corresponding to the directory name, and returns a pointer to the directory stream. The stream is positioned at the first entry in the directory.
- The `readdir()` routine returns a pointer to a `dirent` structure representing the next directory entry in the specific directory stream.
- The `closedir()` routine closes the specific directory stream.

### Permission

The abstraction of the file system enables multiple processes to share the same files.

- The permission bits of the file indicates whether the three groupings, the owner of the file, the group of the file, and other users, could read, write, or execute the file.
- The access control list (ACL) represents exactly which user could access a given resource.

## File System Implementation

### Disk Organization

The section describes the implementation details of **vsfs** (very simple file system). The vsfs partitions the disk into multiple fixed-size blocks.

- Super block: The block that contains information about the file system (e.g. the number of inodes and data blocks)
- Inode bitmap: The bitmap that indicates whether the corresponding inode is free (0) or in-use (1)
- Data bitmap: The bitmap that indicates whether the corresponding data block is free (0) or in-use (1)
- Inode table: The list of on-disk inodes
- Data region: The region of disk that stores user data

The **inode** is the generic name that is used in the file system to describe the structure that holds the metadata for a given file. Each inode is implicitly referred to by the **i-number**.

- **Multi-level index**: The inode contains an indirect pointer, which points to a block that contains more pointers, each of which point to user data. If the file is large enough, multi-level indirection could be introduced. (ext2, ext3, etc.)
- **Extent**: The inode contains multiple extents. Each extent specifies a pointer to the on-disk location of a part of the file and its length. (ext4, XFS, etc.)
- **File allocation table**: The system maintains a in-memory table of link information. The table is indexed by the address of a data block and the content of an entry is the pointer to the next block in the same file. The null value indicates the end-of-file. (FAT, etc.)

The **directory** is a list of (entry name, inode number) pairs. For each file or directory in a given directory, there is a string and a number in the data block of the directory. The directory has an inode in the inode table and data blocks in the data region of the file system.

### Access Path

- Create: The file system reads the inode bitmap to find a free inode, writes to the inode bitmap to allocate the inode, initializes the inode, writes to the data of the directory, and updates the directory inode.
- Open: The file system traverse the pathname, locate the desired inode, checks the permission, and allocates a file descriptor for the current process.
- Read: The file system consults the inode to find the location of the block with the current offset and updates the file offset.
- Write: The file system determines which block to allocate to the file if required, update the inode and the data bitmap, and write the content to the disk block.

### Cache and Buffer

- **Static partitioning cache**: The system uses a fixed-size cache to hold popular blocks with strategies such as LRU. The fixed-size cache is allocated at boot time.
- **Dynamic partitioning cache**: The system integrates the virtual memory pages and file system pages into a unified page cache.
- **Write buffer**: The system batches updates into a smaller set of I/O operations and schedules these operations to improve performance.

## Locality and The Fast File System

The fast file system (FFS) implements the file system structures and allocation policies to be disk aware to improve performance.

FFS divides the disk into a number of cylinder groups. Each cylinder is a set of tracks on different surfaces of a hard drive that are the same distance from the center of the drive. If the files are located in the same group, FFS ensures that accessing these files won't result in long seeks across the disk.

Each cylinder group contains multiple data blocks. FFS includes the super block, inode bitmap, data bitmap, inodes, and user data into each cylinder group.

### Allocation

FFS keeps related files together to improve performance and uses a few heuristics to determine where to place the files and directories.

- Placement of directories: FFS finds the cylinder group with a low number of allocated directories and a high number of free inodes, and puts the directory data into that group.
- Placement of files: FFS allocates the data blocks of a file in the same group as its inode. FFS places all files in the same directory in the same cylinder group.
- Placement of large files: FFS places the direct blocks in the same group as the inode and places each indirect block of the file in a different block group.

To prevent internal fragmentation of 4-KB blocks, FFS introduces 512 byte sub-blocks that could be allocated. If a file grows to 4 KB, the file system copies the sub-blocks into a new 4 KB block and removes the sub-blocks.
