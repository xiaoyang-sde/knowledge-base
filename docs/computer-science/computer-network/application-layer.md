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
- Security referes to usage of techniques such as encryption, authentication, and access control to ensure the security of data.

The TCP service model includes a connection-oriented service and a reliable data transfer service. TCP connection is said to exist between the sockets of the two processes. TCP has a handshaking procedure that allows the client and server to prepare for an exchange of packets. TCP includes a congestion-control mechanism that throttles a sending process when the network is congested.

The UDP service model provides minimal services. UDP is connectionless and unreliable. UDP does not include a congestion-control mechanism.

### Application-Layer Protocol

The application-layer protocol define the syntax, semantics, and synchronization of communication between applications, including the message format, message type, message content, and message sequence. Application-layer protocols are often layered on top of transport-layer protocols, such as TCP or UDP, which provide reliable or unreliable data transfer services.
