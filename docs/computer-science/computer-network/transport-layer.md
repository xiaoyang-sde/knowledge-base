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

## UDP

The UDP protocol is defined in [RFC 768](https://www.rfc-editor.org/rfc/rfc768), which is a connectionless transport-layer protocol that supports multiplexing or demultiplexing function and limited error checking.

- UDP takes messages from the application process, attaches source and destination port number fields for the multiplexing or demultiplexing service, adds the segment length and checksum, and passes the resulting segment to the network layer.

- The network layer encapsulates the transport-layer segment into an IP datagram and then makes a best-effort attempt to deliver the segment to the receiving host.

- If the segment arrives at the receiving host, UDP uses the destination port number to deliver the segment’s data to the correct application process.

## UDP Header Format

```txt
0      7 8     15 16    23 24    31
+--------+--------+--------+--------+
|     Source      |   Destination   |
|      Port       |      Port       |
+--------+--------+--------+--------+
|                 |                 |
|     Length      |    Checksum     |
+--------+--------+--------+--------+
|
|          data bytes ...
+---------------- ...
```

- The source port indicates the port of the sending process.
- The destination port has a meaning within the context of a particular internet destination address.
- The length is the length in bytes of this user datagram including this header and the data.
- The checksum is the 16-bit one's complement of the one's complement sum of a
pseudo header of information from the IP header, the UDP header, and the
data, padded with zero octets at the end to make a multiple of two octets.

```rs
use byteorder::{ByteOrder, NetworkEndian};

fn checksum(data: &[u8]) -> u16 {
    let mut sum = 0u32;
    for i in 0..(data.len() / 2) {
        let word = NetworkEndian::read_u16(&data[i * 2..]);
        sum += u32::from(word);
    }

    if data.len() % 2 == 1 {
        sum += u32::from(data[data.len() - 1]) << 8;
    }

    while sum >> 16 != 0 {
        sum = (sum & 0xFFFF) + (sum >> 16);
    }
    !sum as u16
}
```

The pseudo header prefixed to the UDP header contains the source address, the destination address, the protocol, and the UDP length, which gives protection against misrouted datagrams. The pseudo header is used when calculating the checksum, but it's neither sent with the UDP segment nor counted in the length of the segment.

```txt
0      7 8     15 16    23 24    31
+--------+--------+--------+--------+
|          source address           |
+--------+--------+--------+--------+
|        destination address        |
+--------+--------+--------+--------+
|  zero  |protocol|   UDP length    |
+--------+--------+--------+--------+
```
