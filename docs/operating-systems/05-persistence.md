# Persistence

## Hard Disk Drive

- Platter: The circular hard surface on which data is stored persistenly. Each platter has two surfaces.
- Spindle: The platters are bound around the spindle, which is connected to a motor that spins the platters around at a fixed rate. The rate of rotation is measured in rotations per minute.
- Sector: The disk consists of a large number of sectors (512-byte blocks), each of which could be read or written.
- Track: The surface consists of multiple concentric circles of sectors.
- Disk head and disk arm: The process of reading and writing is accomplished by the disk head. There's one disk head per surface of the drive. The disk head is attached to a single disk arm, which moves across the surface to position the head over the desired track.

### I/O Time

The I/O time is the sum of three major components: $T_{I/O} = T_{\text{seek}} + T_{\text{rotation}} + T_{\text{transfer}}$. The transfer rate is defined as $R_{\text{I/O}} = \frac{\text{Size}_{Transfer}}{T_{\text{I/O}}}$. There's a huge different bewteen sequential $R_{\text{I/O}}$ and random $R_{\text{I/O}}$.

- The **seek time** is the time it takes the disk arm to move to the desired track.
- The **rotational delay** is the time it takes the disk head to move to the desired sector.
- The **transfer time** is the time it takes the disk head to read or write the data to the desired sector.

### Disk Scheduling

- **Shortest Seek Time First** (SSTF): The disk scheduler orders the queue of I/O requests by track, picking requests on the nearest track to complete first. The policy could lead to starvation of I/O requests.
- **Elevator**: The disk scheduler moves the disk head back and forth across the tracks to serve requests. If a request comes for a block on a track that has been serviced on the current sweep, it's queued until the next sweep.
- **Shortest Positioning Time First** (SPTF): The disk scheduler orders the queue of I/O requests by the positioning time ($T_{\text{seek}} + T_{\text{rotation}}$), picking requests on the nearest track to complete first.

## RAID (Redundant Arrays of Inexpensive Disks)

- The **RAID level 0** stripes blocks across the disk of the system in a round-robin fashion. The approach is designed to extract the most parallelism from the contiguous chunks. The chunk size of a RAID array is the size of block placed to each disk.
- The **RAID level 1** replicates blocks across the disk of the system to telerate disk failures. The read operation could be served by any of the disks, and the write operation must be applied to all disks.
- The **RAID level 4** stripes blocks across the disk of the system and adds a single parity block that stores the redundant information for each stripe of blocks. The redundant information is calculated with XOR of all bits in the stripe. If a disk in the stripe fails, the system could reconstruct the disk with the parity information.
- **The RAID level 5** is identical to RAID level 4, expect that it rotates the parity blocks across drives to address the small-write problem.

|| RAID 0 | RAID 1 | RAID 4 | RAID 5 |
| --- | --- | --- | --- | --- |
| Capacity | $N \cdot B$ | $(N \cdot B) / 2 $ | $(N - 1) \cdot B$ | $(N - 1) \cdot B$ |
| Reliability | 0 | 1 | 1 | 1| 1 |
| Sequential Read | $N \cdot S$ | $(N / 2) \cdot S$ | $(N - 1) \cdot S$ | $(N - 1) \cdot S$ |
| Sequential Write| $N \cdot S$ | $(N / 2) \cdot S$ | $(N - 1) \cdot S$ | $(N - 1) \cdot S$ |
| Random Read | $N \cdot R$ | $N \cdot R$ | $(N - 1) \cdot R$ | $N \cdot R$ |
| Random Write| $N \cdot R$ | $(N / 2) \cdot R$ | $\frac{1}{2} \cdot R$ | $\frac{N}{4} \cdot R$ |
| Read Latency| $T$ | $T$ | $T$ | $T$ |
| Write Latency | $T$ | $T$ | $2T$ | $2T$ |

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

- Superblock: The block that contains information about the file system (e.g. the number of inodes and data blocks)
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

Each cylinder group contains multiple data blocks. FFS includes the superblock, inode bitmap, data bitmap, inodes, and user data into each cylinder group.

### Allocation

FFS keeps related files together to improve performance and uses a few heuristics to determine where to place the files and directories.

- Placement of directories: FFS finds the cylinder group with a low number of allocated directories and a high number of free inodes, and puts the directory data into that group.
- Placement of files: FFS allocates the data blocks of a file in the same group as its inode. FFS places all files in the same directory in the same cylinder group.
- Placement of large files: FFS places the direct blocks in the same group as the inode and places each indirect block of the file in a different block group.

To prevent internal fragmentation of 4-KB blocks, FFS introduces 512 byte sub-blocks that could be allocated. If a file grows to 4 KB, the file system copies the sub-blocks into a new 4 KB block and removes the sub-blocks.

## Crash Consistency: FSCK and Journaling

The system could crash or lose power between two writes, and thus the on-disk state could partially get updated. The crash consistency tries to ensure that the file system keeps the on-disk image in a reasonable state.

For example, the workload that appends a single data block to an existing file is accomplished by updating the inode, the new data block, and the data bitmap.

- The data block is written to disk: The data is on the disk, but there's no inode that points to it and no bitmap that marks the block as allocated. The write seems like never occured.
- The inode is written to disk: The inode points to the new data block, but the data block is not exist. The system could read garbage data from the location of the new data block. There's also an inconsistency between the data bitmap and the inode.
- The bitmap is written to disk: The bitmap indicates that the block is allocated, but there's no inode that points to it. There's a space leak since the location of the new block could never be used by the file system.
- The inode and bitmap are written to disk: The system could read garbage data from the location of the new data block.
- The inode and the data block are written to disk: There's an inconsistency between the data bitmap and the inode.
- The bitmap and the data block are written to disk: There's an inconsistency between the data bitmap and the inode.

### The File System Checker

`fsck` is a tool for finding and reparing inconsistencies in the file system metadata. The tool is run before the file system is mounted and made available.

- Superblock: `fsck` checks if the superblock looks reasonable with sanity checks.
- Free blocks: `fsck` scans the inodes, indirect blocks, and multi-level indirect blocks to identify the allocated blocks. It compares the allocated blocks with the bitmap to find inconsistencies.
- Inode state: `fsck` scans the each inode to check for corruption.
- Inode link: The link count indicates the number of different directories that contain a reference to the file. `fsck` scans the directory tree to build the link count for each file and compares it with the link count stored in each inode.
- Duplicate: `fsck` checks for duplicate pointers such as two different inodes refer to the same block.
- Bad block: `fsck` checks for bad block pointers such as pointers points to a block greater than the partition size.
- Directory check: `fsck` performs integrity checks on the contents of each directory to make sure each inode in the directory is allocated.

### Journaling

The journaling method could recover from a crash happened after the transaction has commited to the log. When the system boots, the file system recovery process scans the log and replays the incomplete transactions that have committed to the disk.

1. Journal write: Write the contents of the transaction (the transaction-begin block and the exact contents of the metadata and data updates) to the log.
2. Journal commit: Write the transaction commit block to the log
3. Checkpoint: Write the contents of the metadata and data update to their final on-disk locations
4. Free: Update the journal superblock to mark the transaction free.

When the system boots, the file system recovery process scans the log and replays the incomplete transactions that have committed to the disk. The file system buffers updates and commits them into a global transaction to avoid excessive writes.

To avoid the high cost of writing data block to disk twice, the **metadata journaling** writes the data block to the final location, and then writes only the metadata to the log. By forcing the data write first, the file system could guarantee that a pointer will never point to garbage.

## Log-structured File System

The log-structured file system buffers all updates of data and metadata in an in-memory segment and writes the segment to disk in one long, sequential transfer when the segment is full. LFS never overwrites existing data, but rather always writes segments to free locations.

Assume that rotation and seek overheads before each write takes $T_{\text{position}}$ seconds and the disk transfer rate is $R_{\text{peak}}$ MB/s. The time to write a chunk of $D$ MB is $T_{\text{write}} = T_{\text{position}} + \frac{D}{R_{\text{peak}}}$. Therefore, $R_{\text{effective}} = \frac{D}{T_{\text{position}} + \frac{D}{R_{\text{peak}}}}$. Assume $F$ is the fraction of peak bandwidth, $D = \frac{F}{1 - F} \cdot R_{\text{peak}} \cdot T_{\text{position}}$.

### Inode Map

LFS implements an **inode map** to map the inode numbers and the disk address of each inode. Each time an inode is written to disk, the inode map is updated with its new location. LFS places chunks of the inode map next to where it's writing all of the new information. LFS implements the **checkpoint region** that contains pointers to the latest pieces of the inode map.

To read a file from the disk, the file system reads the checkpoint region to find the latest inode map, reads the inode map to memory, and looks up the inode map to find specific inodes.

### Garbage Collection

The LFS garbage collector reads in a number of old segments, determines which blocks are live, writes out a new set of segments with the lives blocks, and them frees up the old segments for writing.

For each data block $D$, LFS includes its inode number and its offset (the index of the block of the file) in the segment summary block at the head of the segment. For a block $D$ located on disk at address $A$, LFS looks in the segment summary block, finds its inode number $N$ and offset $T$, and checks the inode to find the actual address $A'$ of the $T$-th block of the file. If $A \neq A'$, the block $D$ could be garbage collected.
