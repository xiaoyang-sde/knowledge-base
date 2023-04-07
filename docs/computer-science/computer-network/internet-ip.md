# The Internet and IP

## The 4 Layer Internet Model

- Applicationt: The application layer (e.g. HTTP, BitTorrent) create bi-directional reliable byte stream between two applications with application-specific semantics.
- Transport: The transport layer guarantees correct, in-order delivery of data end-to-end and control congestion. (e.g. Transmission Control Protocol).
- Network: The network layer makes a best-effort attempt to deliver the datagrams to the other end, but it makes no promises. The datagrams could get lost, could be delivered out of order, and could be corrupted. (e.g. Internet Protocol)
- Link: The link layer delivers data over a single link between an end host and a router, or between routers.

## The IP Service

The IP service is designed to be simple to provide a more streamlined and low cost internet infrastructure and allows unreliable services to be built on top. The end-to-end principle suggests to implement features in the end hosts where possible.

- The IP service tries to prevent packets looping forever with the TTL in header.
- The IP service fragments packets that exceeds the limit of the link. (e.g. 1500 for Ethernet)
- The IP service uses a header checksum to reduce chances of delivering datagram to wrong destination.
- The IP service allows for new options to be added to header.

## Packet Switching Principle

The packet is a self-contained unit of data that carries information for it to reach its destination. Packet switching is the transfer of each arriving packet to its destination. The forward table in each switch contains which next switch or destination a packet should be forwarded to.

- The packet switches don't need state for each flow (the collection of datagrams belonging to the same end-to-end communication) as each packet is self-contained.
- The packet switches allow flows to share all available link capacities.

## Layering Principle

The layering principle divides the Internet infrastructure into multiple layers. Each layer is a functional component and provides a well-defined service to the layer above, using the services provided by layers below and its own private processing. The layers communicate sequentially with the layers above and below.

## IPv4 Name and Address

The IPv4 address identifies a device on the Internet. The address is 32 bits long (4 octets): `a.b.c.d`. The netmask indicates which addresses are in the same network. For example, `255.255.255.0` means that if the first 24 bits match, the addresses are in the same network.

CIDR (Classless Inter-Domain Routing) is a method of assigning IPv4 address that improves the efficiency of address distribution. The address block contains a pair of `<address>/<count>`. For example, `171.64.0.0/16` contains the range from `171.64.0.0` to `171.64.255.255`.

## Longest Prefix Match

The routers use the longest prefix match algorithm to choose matching entries from forwarding table. The forwarding table is a set of CIDR entries. The incoming packet could match multiple entries, and the algorithm selects the entry with the longest matching prefix.
