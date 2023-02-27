# Load Balancing at the Frontend

The traffic load balancing mechanism decide which machine in the data centers will serve a particular request. The mechanism tries to distribute traffic across multiple network links, data centers, and machines in an optimal fashion. The frontend load balancing balances user traffic between data centers.

## Load Balancing with DNS

Before a client can even send an HTTP request, it has to look up an IP address using DNS. The simple solution is to return multiple `A` or `AAAA` records in the DNS response and let the client pick a random IP address. However, this method provides little control over the client behavior and the client could not determine the closest address.

To serve the closest address to a particular client, the authoritative nameservers could use an anycast address and leverage the fact that DNS queries will flow to the closest address. The authoritative nameservers could also maintain a map of networks and approximate physical locations, and serves DNS replies based on that mapping. However, the methods will make the DNS server implementation more complex.

The fundamental characteristic of DNS is that a recursive DNS server lies between the client and the nameservers to serve and cache queries between a user and a server. Recursive resolution of IP addresses is problematic, as the IP address seen by the authoritative nameserver does not belong to a client. The solution is to use the EDNS0 extension, which includes information about the client's subnet in the DNS queries sent from a recursive resolver.

The recursive resolvers cache responses and forward those responses within limits indicated by the time-to-live (TTL) field in the DNS record. The end result is that estimating the impact of a given response is difficult. Google analyzes traffic changes and update the list of known DNS resolvers with the approximate size of the user base behind a given resolver, which allows Google to track the potential impact of a given resolver.

The DNS load balancer also needs to know if a particular datacenter has enough capacity to serve requests from users. Therefore, the authoritative DNS server should be integrated with a global control system that tracks traffic, capacity, and the state of the infrastructure.

The RFC 1035 enforces that the size of a DNS response should be less than 512-byte. The limit sets an upper bound on the number of addresses in a single DNS response, which is smaller than the number of servers. The virtual IP address solves the problem.

## Load Balancing with Virtual IP Address

Virtual IP addresses (VIPs) are not assigned to any particular network interface. Instead, they are usually shared across many devices. However, from the user's perspective, the VIP remains a single, regular IP address. In practice, the most important part of VIP implementation is a device called the network load balancer. The balancer receives packets and forwards them to one of the machines behind the VIP. These backends can then further process the request.

The load balancer could prefer the least loaded backend server. However, if the session is stateful, the balancer must track all connections and redirect all requests from the client to the same backend server. The alternative is to use some parts of a packet to create a connection ID with consistent hashing to avoid storing state. Consistent hashing is a mapping algorithm that remains stable when new backends are added to or removed from the list.

The load balancer chould change the destination MAC address of a forwarded packet to leave all the information in upper layers intact, so the backend receives the original source and destination IP addresses. The backend could send a response to the original sender, which is a technique called Direct Server Response. If user requests are small and replies are large, DSR provides tremendous savings in the load balancer. However, this method requires that all machines must be able to reach each other at the data link layer.

Google uses a VIP load balancing solution that uses packet encapsulation. The load balancer puts the forwarded packet into another IP packet with Generic Routing Encapsulation, and uses a backend's address as the destination. The backend receiving the packet strips off the outer IP/GRE layer and processes the inner IP packet as if it were delivered directly to its network interface. However, encapsulation introduces overhead, which can cause the packet to exceed the available Maximum Transmission Unit size and require fragmentation. The fragmentation could be avoided with a largr MTU within the datacenter.
