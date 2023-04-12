# Socket

## Internet Socket

The socket in Linux is an endpoint for communication bewteen processes either in the same kernel or over a network.

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
