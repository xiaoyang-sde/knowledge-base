# Socket

## Internet Socket

The socket in Linux is an endpoint for communication between processes either in the same kernel or over a network.

- `SOCK_STREAM` is a type of socket that provides a reliable, connected communication stream.
- `SOCK_DGRAM` is a type of socket that provides a connectionless and unreliable communication service.

## Byte Order

- Big-endian: The most significant byte of a word is stored at the smallest address, which is the byte order of most networking protocols including IP, TCP, and UDP.
- Little-endian: The least significant byte of a word is stored at the smallest address, which is the byte order of most machine architecture, such as x86, ARM, or RISC-V.

When building packets or filling out data structures, `short` (2 bytes) and `long` (4 bytes) should be converted from the host byte order to the network byte order.

- `htons()`: host to network `short`
- `htonl()`: host to network `long`
- `ntohs()`: network to host `short`
- `ntohl()`: network to host `long`

## Structure

The `struct addrinfo` is used in host name lookups and service name lookups. The `addrinfo` is a node in a linked list, in which the field `ai_next` points to the next node.

```c
struct addrinfo {
  int              ai_flags;     // `AI_PASSIVE`, `AI_CANONNAME`, etc.
  int              ai_family;    // `AF_INET`, `AF_INET6`, `AF_UNSPEC`
  int              ai_socktype;  // `SOCK_STREAM`, `SOCK_DGRAM`
  int              ai_protocol;
  size_t           ai_addrlen;   // size of `ai_addr` in bytes
  struct sockaddr* ai_addr;      // `sockaddr_in` or `sockaddr_in6`
  char           * ai_canonname; // full canonical hostname

  struct addrinfo* ai_next;      // the next node in the linked list
};
```

The `struct sockaddr` holds socket address information for different types of sockets. It's the base of a set of address structures that act like a discriminated union. System calls related to socket, such as `connect()`, accept a `struct sockaddr*`.

```c
struct sockaddr {
  unsigned short    sa_family;    // `AF_INET` or `AF_INET6`
  char              sa_data[14];  // 14 bytes of protocol address
};
```

- The `struct sockaddr_in` is a variant of the union. The  `struct sockaddr_in*` can be cast to `struct sockaddr*`.

```c
struct in_addr {
  uint32_t s_addr; // IPv4 address
};

struct sockaddr_in {
  short int          sin_family;  // `AF_INET`
  unsigned short int sin_port;    // Port number
  struct in_addr     sin_addr;    // IPv4 address
  unsigned char      sin_zero[8]; // Same size as struct `sockaddr`
};
```

- The `struct sockaddr_in6` is a variant of the union. The  `struct sockaddr_in6*` can be cast to `struct sockaddr*`.

```c
struct in6_addr {
  unsigned char   s6_addr[16];   // IPv6 address
};

struct sockaddr_in6 {
  u_int16_t       sin6_family;   // `AF_INET6`
  u_int16_t       sin6_port;     // Port number
  u_int32_t       sin6_flowinfo; // IPv6 flow information
  struct in6_addr sin6_addr;     // IPv6 address
  u_int32_t       sin6_scope_id; // Scope ID
};
```

- The `struct sockaddr_storage` is large enough to hold both `sockaddr_in` and `sockaddr_in6`.

```c
struct sockaddr_storage {
  sa_family_t  ss_family;   // `AF_INET` or `AF_INET6`
  ...                       // Padding
};
```

## IP Address

- `inet_pton()` converts IPv4 or IPv6 address from text to bytes.

```c
struct sockaddr_in sa;
inet_pton(AF_INET, "10.12.110.57", &(sa.sin_addr));

struct sockaddr_in6 sa6;
inet_pton(AF_INET6, "2001:db8:63b3:1::3490", &(sa6.sin6_addr));
```

- `inet_ntop()` converts IPv4 or IPv6 address from bytes to text.

```c
char ipv4[INET_ADDRSTRLEN];
inet_ntop(AF_INET, &(sa.sin_addr), ip4, INET_ADDRSTRLEN);

char ip6[INET6_ADDRSTRLEN];
inet_ntop(AF_INET6, &(sa6.sin6_addr), ip6, INET6_ADDRSTRLEN);
```

## System Call

- `getaddrinfo()` takes a host name, a port number, a `addrinfo` struct that contains relevant information, and returns a pointer to a linked list.

```c
#include <sys/socket.h>
#include <sys/types.h>
#include <netdb.h>

int getaddrinfo(
  const char* node,     // Domain name or ip address
  const char* service,  // Port number
  const struct addrinfo* hints,
  struct addrinfo** res
);
```

- `socket()` takes a `domain`, a `type`, a `protocol`, and returns a socket descriptor. The parameters can be obtained with `getaddrinfo()`.

```c
#include <sys/socket.h>
#include <sys/types.h>

int socket(
  int domain, // `PF_INET` or `PF_INET6`
  int type, // `SOCK_STREAM` or `SOCK_DGRAM`
  int protocol // TCP or UDP
);

socket(addrinfo->ai_family, addrinfo->ai_socktype, addrinfo->ai_protocol);
```

- `bind()` takes a socket descriptor and binds it to a port on the local machine. The parameters can be obtained with `getaddrinfo()`.

```c
#include <sys/socket.h>
#include <sys/types.h>

int bind(int sockfd, struct sockaddr* my_addr, int addrlen);

bind(sockfd, addrinfo->ai_addr, addrinfo->ai_addrlen);
```

- `connect()` takes a socket descriptor and connects to a destination IP address and port. The parameters can be obtained with `getaddrinfo()`.

```c
#include <sys/socket.h>
#include <sys/types.h>

int connect(int sockfd, struct sockaddr* serv_addr, int addrlen);

connect(sockfd, addrinfo->ai_addr, addrinfo->ai_addrlen);
```

- `listen()` takes a socket descriptor, a number of connections allowed on the incoming queue, and listens on the address and port the socket is bound to. The incoming connections are waiting in a queue until a call to `accept()`.

```c
#include <sys/socket.h>

int listen(int sockfd, int backlog);
```

- `accept()` takes a client from the waiting queue and returns a new socket descriptor for the current connection. The original socket descriptor is still listening for new connections and the new descriptor is used to communicate with the client.

```c
#include <sys/socket.h>
#include <sys/types.h>

int accept(int sockfd, struct sockaddr* addr, socklen_t* addrlen);
```

- `send()` sends a message to a socket descriptor and returns the number of bytes sent out.
- `recv()` reads a message from a socket descriptor to a buffer and returns the number of bytes read.

```c
#include <sys/socket.h>

int send(int sockfd, const void* msg, int len, int flags);
int recv(int sockfd, void* buf, int len, int flags);

int send_all(int sockfd, char* buf, int len) {
  int bytes_sent = 0;
  int bytes_left = len;

  while (bytes_sent < len) {
    int result = send(sockfd, buf + bytes_sent, 0);
    if (result == -1) {
      return -1;
    }
    bytes_sent += result;
    bytes_left -= result;
  }

  return 0;
}
```

- `sendto()` sends a message to a socekt descriptor, which forwards the message to a destination IP address and port, and returns the number of bytes sent out.
- `recvfrom()` reads a message from a socket descriptor and returns the address of the other side and the number of bytes read.

```c
#include <sys/socket.h>
#include <sys/types.h>

int sendto(
  int sockfd, const void* msg,
  int len, unsigned int flags,
  const struct sockaddr* to, socklen_t to_len
);

int recvfrom(
  int sockfd, void* buf,
  int len, unsigned int flags,
  struct sockaddr* from, int* fromlen
);
```

- `close()` closes the connection on a socket descriptor.
- `shutdown()` turns off communication in a certain direction.

```c
#include <sys/socket.h>

int shutdown(int sockfd, int how);
```

- `gethostname()` returns the host name of the local machine.
- `getpeername()` returns the address of the other side of a connected stream socket.

```c
#include <sys/socket.h>

int gethostname(char* hostname, size_t size);
int getpeername(int sockfd, struct sockaddr* addr, int* addrlen);
```
