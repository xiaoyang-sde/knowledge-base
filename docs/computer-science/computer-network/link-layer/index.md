# Link Layer

Let a node be a device that runs a link-layer protocol, such as hosts, routers, and switches. Let a link be a communication channel that connect adjacent nodes. In order for a datagram to be transferred from source host to destination host, it must be moved over each of the individual links in the end-to-end path. Over a given link, a transmitting node encapsulates the datagram in a link-layer frame and trans- mits the frame into the link.

- Framing: Link-layer protocols encapsulate each network-layer data- gram within a link-layer frame before transmission over the link. The frame contains a header field and a payload field.
- Link access: The MAC (medium access control) protocol specifies how a frame is transmitted to the link. The MAC protocol coordinates the frame transmissions of the nodessharing a link.
- Error detection: The transmitting node might include error-detection bits in the frame, and having the receiving node perform an error check.

The link layer is implemented on the network interface controller (NIC). The controller implements link layer services including framing, link access, and error detection.

- On the sending side, the controller takes a datagram from the host and encapsulates the datagram in a link-layer frame, and then transmits the frame into the communication link.
- On the receiving side, a controller receives the entire frame, and extracts the network-layer datagram.

## Multiple Access Link

- The point-to-point link consists of a single sender and a single receiver.
- The broadcast link consists of multiple senders and multiple receivers.The multiple access problem is the coordiation of multiple senders and receivers on a shared channel.

### Channel Partitioning Protocol

Suppose the channel supports $N$ nodes and the transmission rate is $R$ bps. Time-division multiplexing divides time into time frames and divides each frame into $N$ time slots. Each node is assigned with one of the time slot. When a node has a packet to send, it transmits the packet's bits during its assigned time slot in the time frame. Therefore, each node has a dedicated transmission rate of $\frac{R}{N}$ bps even if not all nodes have packets to send.

### Random Access Protocol

In a random access protocol, a transmitting node transmits at the full rate of the channel. When there is a collision, each node involved in the collision waits a random time interval and retransmits its frame again until its frame gets through without a collision.

#### Slotted ALOHA

Slotted ALOHA is a simple random access protocol. Assume that each frame has $L$ bits and the time is divided into slots of $\frac{L}{R}$ seconds. It allows a node to transmit at the full rate $R$ when it's the unique active node, but it requires the slots to be synchronized in the nodes.

- When the node has a fresh frame to send, it waits until the beginning of the next slot and transmits the entire frame in the slot.
- If there is a collision, the node detects the collision before the end of the slot. The node retransmits its frame in each subsequent slot with probability $p$ until the frame is transmitted without a collision.

#### Carrier Sense Multiple Access with Collision Detection

- Carrier sensing: Each node listens to the channel before transmitting and doesn't transmit until there's no transmission for a time interval.
- Collision detection: Each node listens to the channel while it's transmitting. If it detects that another node is transmitting an interfering frame, it stops transmitting and waits a random time interval before sensing.

The exponential backoff algorithm can be used when picking the time interval for collision detection. When transmitting a frame that has experienced $n$ collisions, a node chooses the time interval at random from $\{ 0, 1, \dots, 2^n - 1 \}$. For Ethernet, the time interval will be multiplied with the number of time needed to send $512$ bits.

### Taking-Turns Protocol

- The polling protocol requires one of the nodes to be a master node. The master node sends a message to tell each node the maximum number of frames it can transmit.
- The token-passing protocol requires the nodes to exchange a token frame in a fixed order. For example, node $i$ should send the token to node $i + 1$. If a node has frames to transmit when it receives the token, it sends up to a maximum number of frames and then forwards the token to the next node.

## Switched Local Area Network

### Media Access Control (MAC) Address

Each network interface has a MAC address, which is 6 bytes long. IEEE manages the MAC address space to ensure the each interface has its unique address.

- The sending adapter inserts the destination adapter's MAC address into the frame and then sends the frame into the LAN. The MAC broadcast address is `FF-FF-FF-FF-FF-FF`, which enables all adapters to receive the frame.
- The receiving adapter passes the enclosed datagram up the protocol stack.

### Address Resolution Protocol (ARP)

The ARP translates translates an IP address to a MAC address. Each host and router has an ARP table that contains mappings of IP addresses to MAC addresses. If the sender's ARP table doesn't contain the MAC address of the receiver, it sends an ARP packet to all the hosts and routers on the subnet to determine the MAC address of the receiver. If the sender and the receiver are in different subnets, the router is responsible of determining the MAC address of the receiver.

### Ethernet

Ethernet is a standard for wired local area networks. It provide an unreliable service to the network layer.

```txt
+---------------------------+
|     Preamble (8 bytes)    |
+---------------------------+
|     Destination MAC       |
|         Address           |
+---------------------------+
|     Source MAC Address    |
+---------------------------+
|  Ethernet Type (2 bytes)  |
+---------------------------+
|                           |
|        Payload            |
|                           |
+---------------------------+
|       CRC (4 bytes)       |
+---------------------------+
```

- Preamble: Each of the first 7 bytes of the preamble serve to wake up the receiver and to synchronize their clocks.
- Destination MAC address
- Source MAC address
- Ethernet type: The type of the Ethernet frame, such as IP and ARP.
- CRC: The CRC field allows the receiver to detect bit errors in the frame.
- Payload: The payload carries the IP datagram if the Ethernet type is IP. The MTU is 1500 bytes.

### Switch

The role of the switch is to receive incoming link-layer frames and forward them onto outgoing links. The switch is transparent to the hosts and routers in the subnet.

- Filtering is the switch function that determines whether a frame should be forwarded to some interface or should be dropped.
- Forwarding is the switch function that determines the interfaces to which a frame should be directed.

Switch filtering and forwarding are done with a switch table. The switch table contains items for some hosts and routers on a LAN. Each item contains a MAC address, the switch interface that leads towards the MAC address, and the time when the item is placed in the table. When a switch receives a packet that sends to an address that is not in the table, it broadcasts the packet.

For each incoming frame received on an interface, the switch stores in its table the MAC address of the frame source, the interface from which the frame arrived, and the current time. The switch deletes an address in the table if no frames are received with that address as the source address after some period of time.
