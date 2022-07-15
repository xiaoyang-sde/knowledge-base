# Transport

## Transmissioin Control Protocol (TCP)

The Transmission Control Protocol provides a communication service at an intermediate level between an application program and the Internet Protocol. It provides host-to-host connectivity at the transport layer of the Internet model. TCP is connection-oriented, and a connection between client and server is established before data can be sent. Three-way handshake, retransmission, and error detection guarantees the reliability of the protocol.

![TCP Finite State Machine](https://upload.wikimedia.org/wikipedia/commons/thumb/f/f6/Tcp_state_diagram_fixed_new.svg/1920px-Tcp_state_diagram_fixed_new.svg.png)

### TCP Header

- Source port (16 bits)
- Destination port (16 bits)
- Sequence number (32 bits): The accumulated sequence number of the first data byte of this segment for the current session.
- Acknowledgment number (32 bits): If the ACK flag is set, the value of this field is the next sequence number that the sender of the ACK is expecting.
- Data offset (4 bits): The value of this field is the size of the TCP header in 32-bit words.
- Reserved (3 bits)
- Flags (9 bits): NS, CWR, ECE, URG, ACK, PSH, RST, SYN, FIN
- Window size (16 bits): The size of the receive window, which specifies the number of window size units that the sender of this segment is currently willing to receive.
- Checksum (16 bits): The 16-bit checksum field is used for error-checking of the TCP header, the payload and an IP pseudo-header.
- Urgent pointer (16 bits): If the URG flag is set, then this 16-bit field is an offset from the sequence number indicating the last urgent data byte.
- Options (Variable 0–320 bits, in units of 32 bits)

### Connection Establishment

- **SYN**: The client sends a SYN to the server with a random sequence number `A`.
- **SYN-ACK**: The server replies with a SYN-ACK with an acknowledgment number of `A + 1` and a random sequence number `B`.
- **ACK**: The client sends an ACK back to the server with an acknowledgment number of `B + 1` and a sequence number `A + 1`.

### Connection Termination

- **FIN**: The endpoint `A` that wishes to stop its half of the connection sends a FIN to the other endpoint `B`.
- **ACK**: The endpoint `B` sends an ACK to the endpoint `A`. `B` could still send data to `A`.
- **FIN**: The endpoint `B` sends a FIN to the endpoint `A`.
- **ACK**: The endpoint `A` sends an ACK to the endpoint `B`.

## User Datagram Protocol (UDP)

UDP uses a simple connectionless communication model with a minimum of protocol mechanisms. UDP provides checksums for data integrity, and port numbers for addressing different functions at the source and destination of the datagram. It has no handshaking dialogues, and thus exposes the user's program to any unreliability of the underlying network. There is no guarantee of delivery, ordering, or duplicate protection.

### UDP Header

- Source port (16 bits)
- Destination port (16 bits)
- Checksum (16 bits): The 16-bit checksum field is used for error-checking of the UDP header, the payload and an IP pseudo-header.
- Length (16 bits): The length of the UDP header and data

## Internet Control Message Protocol (ICMP)

The Internet Control Message Protocol (ICMP) is used by network devices, including routers, to send error messages and operational information indicating success or failure when communicating with another IP address. ICMP is not used to exchange data between systems. ICMP could be used for `ping` and `traceroute`.
