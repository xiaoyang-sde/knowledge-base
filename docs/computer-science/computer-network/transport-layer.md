# Transport Layer

The transport-layer protocol provides for logical communication between application processes running on different hosts. Transport-layer protocols are implemented in the end systems but not in network routers. TCP and UDP are transport-layer protocols available to the application layer.

- TCP is a reliable, connection-oriented protocol that provides guaranteed delivery and error detection.
- UDP is a connectionless, unreliable protocol that does not provide guarantees about delivery or error detection.

## Multiplexing and Demultiplexing

Transport-layer multiplexing and demultiplexing extends the host-to-host delivery service of the network layer to a process-to-process delivery service for applications running on the hosts. Each process has one or more sockets with unique identifiers. The format of the identifier depends on whether the socket is a UDP or a TCP socket.

- Multiplexing: The sending host gathers data chunks, encapsulates each data chunk with header information to create segments, and passes the segments to the network layer. The header of a TCP or a UDP segment contains the source port number field and the destination port number field
- Demultiplexing: For each transport-layer segment, the receiving host examines the fields and directs the segment to the receiving socket.
  - Connectionless: Each UDP socket is identified with the destination IP address and the destination port number. The receiving host uses these values to direct a segment to appropriate socket.
  - Connection-oriented: Each TCP socket is identified with the source IP address, the source port number, the destination IP address, and the destination port number. The receiving host uses these values to direct a segment to appropriate socket.

## Congestion Control

The router has buffers that allow it to store incoming packets when the packet-arrival rate exceeds the outgoing link's transmission rate. Packeet loss could result from the overflowing of router buffers as the network becomes congested. To treat the cause of network congestion, mechanisms are needed to throttle senders in the face of network congestion.

In a router with finite buffer and senders with retransmission mechanisms, packets could get dropped at router and senders might retransmit packets because either the packet is known to be lost or timed out, which let the router deliver duplicated packets.
