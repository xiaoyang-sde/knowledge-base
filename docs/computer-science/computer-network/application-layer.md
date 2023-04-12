# Application Layer

## Principle of Network Application

Processes on different end systems communicate with each other with messages across the computer network. The process that initiates the communication is labeled as the client. The process that waits to be contacted to begin the session is the server.

- Client-server architecture: The server provides a service to several clients on the network.
- P2P architecture: The application exploits direct communication between pairs of connected hosts. Each computer can act as both a client and a server.

Each process sends messages into, and receives messages from, the network through a software interface called a socket. Sockets are identified with an IP address and a port number.

### Transport Service

Networks provide more than one transport-layer protocol. Each protocol is classified along four dimensions, even though there's no protocol that guarantees throughput and timing.

- Reliable data transfer is the process of ensuring that data is transmitted from a source to a destination without errors or losses.
- Throughput is the rate at which data is transmitted over a network connection.
- Timing refers to the guarantee of a networked system to deliver data within a specified time frame.
- Security refers to usage of techniques such as encryption, authentication, and access control to ensure the security of data.

The TCP service model includes a connection-oriented service and a reliable data transfer service. TCP connection is said to exist between the sockets of the two processes. TCP has a handshaking procedure that allows the client and server to prepare for an exchange of packets. TCP includes a congestion-control mechanism that throttles a sending process when the network is congested.

The UDP service model provides minimal services. UDP is connectionless and unreliable. UDP does not include a congestion-control mechanism.

### Application-Layer Protocol

The application-layer protocol define the syntax, semantics, and synchronization of communication between applications, including the message format, message type, message content, and message sequence. Application-layer protocols are often layered on top of transport-layer protocols, such as TCP or UDP, which provide reliable or unreliable data transfer services.

## The Web and HTTP

The HyperText Transfer Protocol (HTTP) is defined in [RFC 1945](https://www.rfc-editor.org/rfc/rfc1945), [RFC 7230](https://www.rfc-editor.org/rfc/rfc7230) and [RFC 7540](https://www.rfc-editor.org/rfc/rfc7540). HTTP defines the structure of the messages exchanged between the server and the client.

HTTP uses TCP as its transport protocol. The HTTP client first initiates a TCP connection with the server, and the browser and the server processes access TCP through their socket interfaces. HTTP is a stateless protocol because an HTTP server maintains no information about the clients.

### Non-Persistent and Persistent Connection

The client and server might communicate for an extended period of time, with the client making a series of requests and the server responding to each of the requests. HTTP could either use non-persistent connection (HTTP/1.0) or persistent connection (default).

- Non-persistent connection: Each request or response pair is sent over a separate TCP connection.
- Persistent connection: Each request or response pair is sent over the same TCP connection.

The round-trip time (RTT) is the time it takes for a small packet to travel from client to server, and then back to the client. When a client requests for a HTML file in a non-persistent connection, the total response time is 2 RTT plus the transmission time of the HTML file.

- The client sends `SYN` and the server replies `SYN` and `ACK`, which takes an RTT.
- The client sends `ACK` with the HTTP request and the server replies the HTML file, which takes an RTT plus the transmission time of the HTML file.

### HTTP Message Format

- The HTTP request message contains a request line, several header lines, and the request body. The request line contains the HTTP method, URL, and the HTTP version.
- The HTTP response message contains a status line, several header lines, and the entity body. The status line contains the HTTP status code and their phrases.

```c
GET /index.html HTTP/1.1
Host: www.example.com
Connection: close
User-agent: Mozilla/5.0
Accept-language: en


HTTP/1.1 200 OK
Connection: close
Date: Tue, 18 Aug 2015 15:44:04 GMT
Server: Apache/2.2.3 (CentOS)
Last-Modified: Tue, 18 Aug 2015 15:11:03 GMT Content-Length: 6821
Content-Type: text/html
```

### Cookie

The HTTP server is stateless. The HTTP cookie is a small piece of data that a server sends to a user's web browser. The browser stores the cookie and sends it back to the same server with later requests. It remembers stateful information for the stateless HTTP protocol.

- The HTTP response contains an optional `set-cookie` header, which instructs the client to store a pair of cookies.
- The HTTP request contains an optional `cookie` header, which sends the stored cookie to the server.

### HTTP/2

HTTP/2 is standardized in [RFC 7540](https://www.rfc-editor.org/rfc/rfc7540).
