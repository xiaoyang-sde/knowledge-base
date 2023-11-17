# eBPF

BPF is a virtual machine that runs instructions in an isolated environment. LLVM and GCC Supports compiling C code into BPF instructions. BPF uses a verifier to ensure that the program is safe to run, which prevents the program from injecting system crashes and malicious behavior. The JIT compiler will transform the BPF bytecode into machine code after the program is verified, avoiding this overhead on execution time.

Before the kernel runs a BPF program, it needs to know which execution point the program is attached to. For a specified execution point, the kernel exposes function helpers to provide relevant data to the program. The BPF map is responsible for sharing data between the kernel and the user-space. BPF maps are bidirectional structures to share data, which allows reading and writing from both sides.

## Program Type

- Socket filter (`BPF_PROG_TYPE_SOCKET_FILTER`): The program is attached to a raw socket and has read-access to all the packets of that socket. The metadata contains information related to the network stack, such as the protocol type.
- Kprobe (`BPF_PROG_TYPE_KPROBE`): The program is attached to certain call points in the kernel as kprobe handlers. The BPF VM ensures that the programs are safe to run, which is an advantage from traditional kprobe modules.
- Tracepoint (`BPF_PROG_TYPE_TRACEPOINT`): The program is attached to certain tracepoints in the kernel. BPF supports declaring its own tracepoints, which will be listed in `/sys/kernel/debug/tracing/events/bpf`.
- Raw Tracepoint (`BPF_PROG_TYPE_RAW_TRACEPOINT`): The program is attached to certain tracepoints in the kernel and has access to more detailed information about the task that the kernel is executing. (It has a small performance overhead.)
- XDP (`BPF_PROG_TYPE_XDP`): The program is able to filter network packets at the lowest layer of the network stack. It has access to a limited set of information from the packet given that the kernel hasn't been processed them.
- Perf Event (`BPF_PROG_TYPE_PERF_EVENT`): The program is attached to certain perf events, which are generated from the kernel's internal profiler.
- Socket Option (`BPF_PROG_TYPE_SOCK_OPS`): The program modifies socket connection options at runtime, while a packet transits through several stages in the kernel's networking stack.
- Socket Map (`BPF_PROG_TYPE_SK_SKB`): When the kernel creates a socket, it stores the socket in the socket map. The program keeps references to several sockets and is able to redirect an incoming packet from a socket to a different socket.
- Socket Message Delivery (`BPF_PROG_TYPE_SK_MSG`): The program controls whether a message sent to a socket should be delivered.
- Cgroup Socket (`BPF_PROG_TYPE_CGROUP_SKB`): The program is attached to certain cgroups. It controls the network packets delivered to or sent from a process in the cgroup.
- Cgroup Socket Address (`BPF_PROG_TYPE_CGROUP_SOCK_ADDR`): The program manipulates the IP addresses and port nubmers that user-space programs in a cgroup are attached to. It ensures that all incoming and outgoing connections from those applications use the IP and port that the BPF program provides.
- Cgroup Open Socket (`BPF_PROG_TYPE_CGROUP_SOCK`): The program is attached to certain cgroups. It controls the behavior when a process in the cgroup opens a new socket.
- Socket Reuseport (`BPF_PROG_TYPE_SK_REUSEPORT`): The program hooks into the logic that the kernel uses to decide whether it's going to reuse a port through the `SO_REUSEPORT` socket option.
- Flow Dissection (`BPF_PROG_TYPE_FLOW_DISSECTOR`): The program hooks into the logic in the flow dissector path, which modifies the flow that network packets follow within the kernel.

## Verifier

- The verifier performs static analysis of the code that the VM is going to load. The objective is to ensure that the program has an expected end. It creates a DAG of the code, where each instruction is a node, and each node is linked to the next instruction. After the verifier generates this graph, it performs a DFS to ensure that the program finishes and the code doesn't include dangerous paths.
  - Infinite loop is not allowed.
  - The maximum number of instructions to execute is 4096.
  - The program doesn't include unreachable instruction.
- The verifier performs a dry-run of the program. It checks if all instructions are valid and all memory pointers are accessed correctly. It also ensures that no matter which control path the program takes, it arrives to the `BPF_EXIT` instruction.

## Example Program

```c
#include <linux/bpf.h>
#include <bpf/bpf_helpers.h>

SEC("tracepoint/syscalls/sys_enter_execve")
int bpf_prog(void *ctx) {
    const char message [] = "execve\n";
    bpf_trace_printk(message, sizeof(message));
    return 0;
}

const char _license[] SEC("license") = "GPL";
```

```shell
clang -target bpf -O2 -c bpf.c -o bpf.o
sudo bpftool prog load bpf.o /sys/fs/bpf/bpf_program autoattach
sudo bpftool prog tracelog

sudo rm /sys/fs/bpf/bpf_program
```
