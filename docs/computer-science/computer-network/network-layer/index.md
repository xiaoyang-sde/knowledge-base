# Network Layer

The role of the network layer is to move packets from a sending host to a receiving host. The network layer provides a best-effort service. The router performs both forwarding and routing functions.

- Forwarding: Forwarding refers to the router-local action of transferring a packet from an input link interface to the appropriate output link interface. When a packet arrives at a router's input link, the router might move the packet to the appropriate output link, drop the packet, or duplicate the packet. Forwarding is implemented in the data plane. The router examines the value of one or more fields in the arriving packet's header, and then using these header values to index into its forwarding table. The forwarding table stores the values that indicate the outgoing link interfaces of the router.

- Routing: Routing refers to the network-wide process that determines the end-to-end paths that packets take from source to destination. The network layer must determine the route or path of packets from a sender to a receiver. The algorithms that calculate these paths are referred to as routing algorithms, which might determine the contents of the router's forwarding table.
