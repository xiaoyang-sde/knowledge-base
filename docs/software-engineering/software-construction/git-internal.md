# Git Internal

## Plumbing and Porcelain

- Plumbing: The low-level commands that are designed to be chained together UNIX-style
- Porcelain: The user-friendly commands such as `checkout` or `branch`

The files and directories in the `.git` directory in the workspace contain all information about the current Git repository.

- `objects`: The directory that stores all the content for the Git database
- `refs`: The directory that stores pointers into commit objects in that data
- `HEAD`: The file that points to the branch that has been checked out
- `index`: The file where Git stores the staging area information
- `hooks`: The directory that contains client-side and server-side hook scripts
- `info`: The global exclude file for ignored patterns
- `config`: The project-specific configuration options
- `description`: The file used by the GitWeb program

## Git Object

Git is a content-addressable filesystem. The core of Git is a key-value data store. Git stores content similar to a UNIX file system, such that all the content is stored as `tree` and `blob` objects, with trees corresponding to UNIX directory entries and blobs corresponding more or less to inodes or file contents. Git creates the `blob` from the file content, and creates the `tree` from the state of the staging area or index.

- The `hash-object` command takes some data, stores it in the `.git/objects` directory (the object database), and returns the unique key (40-character checksum hash) that refers to that data object.
- The `cat-file` command takes the checksum hash and returns the content of the file.
- The `update-index` command adds the file to the staging area.
- The `read-tree` command reads the tree object to the staging area.
- The `write-tree` command writes the staging area out to a tree object.
- The `commit-tree` command creates a commit object from a tree object. The commit object specifies the top-level tree for the snapshot of the project, the parent commits, the author information, and the commit message.
- The `update-ref` command points a reference name to a commit object.

## Git Reference

The branch in Git is a simple pointer or reference to the head of a line of work. The `HEAD` file contains the SHA-1 (detached HEAD) or symbolic reference to the last commit.

The `tag` object container a tagger, a date, a messgage, and a pointer to a commit. It's similar to a branch reference, but it never moves.
