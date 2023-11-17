# Map

The BPF maps are k-v stores that reside in the kernel, which are accessible from BPF program that knows about them. Programs that run in the user-space can access these maps through file descriptors. Therefore, the BPF program running in the kernel space and the program that loaded the BPF program are able to communicate at runtime with message passing. The BPF verifier ensures that the access pattern is safe.

## Creation

To create a BPF map, invoke the `bpf` system call with the `BPF_MAP_CREATE` command as the first argument and the configuration of the map as the second argument. This call will return the file descriptor identifier associated with the created map.

```c
#include <linux/bpf.h>

union bpf_attr {
  struct {
    __u32 map_type;
    __u32 key_size;
    __u32 value_size;
    __u32 max_entries;
    __u32 map_flags;
  };
}

int bpf(int cmd, union bpf_attr *attr, unsigned int size);
```

`libbpf` provides a declarative API for creating maps. For example, the following code creates a hash-table similar to `unordered_map<int, long>` with a maximum size of `4` elements.

```c
struct {
    __uint(type, BPF_MAP_TYPE_HASH);
    __uint(max_entries, 4);
    __type(key, int);
    __type(value, long);
} hash_map SEC(".maps");
```

## Read, Update, and Delete Element

- Read: `bpf_map_lookup_elem(...)`
- Update: `bpf_map_update_elem(...)`
- Delete: `bpf_map_delete_element(...)`
- Read and Delete: `bpf_map_lookup_and_delete_element(...)`
- Iterate: `bpf_map_get_next_key(...)`

## BPF Virtual Filesystem

Maps and BPF programs pinned to the BPF filesystem will remain in memory after the program that created them terminates. The default directory of the filesystem is `/sys/fs/bpf`.

- `BPF_PIN_FD` is the command to save BPF objects in this filesystem.
- `BPF_OBJ_GET` is the command to fetch BPF objects that have been pinned to the filesystem.

## Map Type

- Hash-Table (`BPF_MAP_TYPE_HASH`): The map is backed by a hash-table.
- Array (`BPF_MAP_TYPE_ARRAY`): The map is backed by a preallocated slice of elements, where each element is set to their zero value. The keys are indexes in the array, and their size must be four bytes. The elements in the map can't be removed.
- Program Array (`BPF_MAP_TYPE_PROG_ARRAY`): The map is used to store references to BPF programs using their file descriptor identifiers. In conjunction with the helper `bpf_tail_call`, this map allows to jump between programs, bypassing the maximum instruction limit of a single program. The size of both keys and values must be four bytes.
- Perf Events Array (`BPF_MAP_TYPE_PERF_EVENT_ARRAY`): The map is used to store `perf_events` in a buffer ring that communicates between BPF programs and user-space programs in real time. The user-space program acts as a listener that waits for events coming from the kernel.
- Stack Trace (`BPF_MAP_TYPE_STACK_TRACE`): The map is used to store stack traces from the running process.
- Cgroup Array (`BPF_MAP_TYPE_CGROUP_ARRAY`): The map is used to store references to cgroups using their file descriptor identifiers.
- LPM Trie (`BPF_MAP_TYPE_LPM_TRIE`): The map is backed by a longest prefix match trie. This algorithm is used in routers that keep traffic forwarding tables to match IP addresses with specific routes.
- Device Map (`BPF_MAP_TYPE_DEVMAP`): The map is used to store references to network devices, which are useful for network applications that want to manipulate traffic at the kernel level.
- CPU Map (`BPF_MAP_TYPE_CPUMAP`): The map is used to store references to CPUs, which are useful for network applications that assigns network stacks to specific CPUs.
- Open Socket (`BPF_MAP_TYPE_XSKMAP`): The map is used to store references to open sockets.
- Socket Hash-Table (`BPF_MAP_TYPE_SOCKHASH`): The map is used to store references to open sockets in the kernel. Each socket in the kernel is identified by a five-tuple key.
- Reuseport Socket (`BPF_MAP_TYPE_REUSEPORT_SOCKARRAY`): The map is used to store references to sockets that can be reused by an open port in the system.
- Queue (`BPF_MAP_TYPE_QUEUE`): The map is backed by a FIFO queue. The key size must be zero.
- Stack (`BPF_MAP_TYPE_STACK`): The map is backed by a LIFO queue. The key size must be zero.

## Example Program

```c
#include <linux/bpf.h>
#include <bpf/bpf_helpers.h>

struct {
    __uint(type, BPF_MAP_TYPE_HASH);
    __uint(max_entries, 4);
    __type(key, int);
    __type(value, long);
} hash_map SEC(".maps");

SEC("tracepoint/syscalls/sys_enter_execve")
int bpf_prog(void *ctx) {
  const int k = 0;
  long *v = bpf_map_lookup_elem(&hash_map, &k);
  if (v == 0) {
    const long initial_value = 1;
    bpf_map_update_elem(&hash_map, &k, &initial_value, BPF_ANY);
  } else {
    *v += 1;
  }
  return 0;
}

const char _license[] SEC("license") = "GPL";
```
