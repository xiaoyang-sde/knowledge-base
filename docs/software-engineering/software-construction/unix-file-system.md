# Unix File System

## Principles

The file system appears as one rooted tree of directories. The root of the entire tree is denoted `/`.

- The file is the data stored on the hard drive.
- The directory structure is a list of inodes with their assigned names, which includes an entry for itself, its parent, and each of its children.
- The inode contains the location and metadata for each file.

## Inode

The inode describes a file system object (e.g. file, directory) that stores the attributes (e.g. metadata, owner, permission) and the object's data.

- The inode that refers to a file contains the disk block location of the file.
- The inode that refers to a directory contains the disk block location of the directory structure.

Each inode is identified by an integer inode number. The inodes are held in a table that could be used to find the actual inode by the inode number.

## Hard Link and Symbolic Link

The hard link is a direct reference to an inode value of a file. Each inode has at least 1 hard link. If all links refer to the inode has been removed, the file is deleted.

The symbolic link is a shortcut that reference to a file or a directory. It contains a text string that is automatically interpreted and followed by the operating system as a path to another file or directory.

## Conventional directory layout

- `/`: The root of the file system
- `/bin`: The binaries and fundamental utilities (e.g. `ls`, `cp`)
- `/dev`: The file representatioins of peripheral devices and pseudo-devices
- `/etc`: System-wiide connfiguratiion files and system databases
- `/tmp`: Temporary files not expected to survive a reboot
- `/usr`: Executables, libraries, and shared resources that are not system critical
- `/lib`: Libraries and data files for programs stored within `/usr` or elsewhere
- `/sbin`: Fundamental utilities that used to start, maintain, and recover the system
- `/var`: The files that might change frequently
- `/opt`: The software that are installed locally
