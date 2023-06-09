# Network Layer

The role of the network layer is to move packets from a sending host to a receiving host. The network layer provides a best-effort service. The router performs both forwarding and routing functions.

- Forwarding: Forwarding refers to the router-local action of transferring a packet from an input link interface to the appropriate output link interface. When a packet arrives at a router's input link, the router might move the packet to the appropriate output link, drop the packet, or duplicate the packet. Forwarding is implemented in the data plane. The router examines the value of one or more fields in the arriving packet's header, and then using these header values to index into its forwarding table. The forwarding table stores the values that indicate the outgoing link interfaces of the router.

- Routing: Routing refers to the network-wide process that determines the end-to-end paths that packets take from source to destination. The network layer must determine the route or path of packets from a sender to a receiver. The algorithms that calculate these paths are referred to as routing algorithms, which might determine the contents of the router's forwarding table.

## Control Plane

- Per-router control: Each router contains a forwarding and a routing function. Each router has a routing component that communicates with the routing components in other routers to compute the values for its forwarding table.
- Centralized control: The centralized controller computes and distributes the forwarding tables to each router. The controller interacts with a control agent in each of the routers to configure and manage that router's flow table.

### Routing Algorithm

The routing algorithm determines routers from senders to receivers through the network of routers. The graph represents the interconnected routers, where each node represents a router and each edge represents the links between the routers. Each edge $(a, b)$ has a weight $c(a, b)$ epresenting its cost, which might reflect the physical length of the corresponding link, the link speed, or the cost associated with a link.

Let a path in a grpah $G = (N, E)$ be a sequence of nodes $(a_1, a_2, \dots, a_p)$ such that each of the pairs $(a_1, a_2), (a_2, a_3), \dots, (a_{p - 1}, a_p)$ are edges in $E$. Given the nodes $a, b$, the least-cost path between $a$ and $b$ is preferred.

- The Link-State routing algorithm is a centralized routing algorithm that computes the least-cost path between a source and destination using complete, global knowledge about the network.

```cpp
vector<bool> visited(n + 1);
vector<int> distance_list(n + 1, numeric_limits<int>::max());
priority_queue<tuple<int, int>, vector<tuple<int, int>>, greater<tuple<int, int>>> heap;

distance_list[1] = 0;
heap.emplace(0, 1);

while (heap.size()) {
  const auto [_, node] = heap.top();
  heap.pop();
  if (visited[node]) {
    continue;
  }
  visited[node] = true;

  for (const auto &[child, weight] : graph[node]) {
    if (distance_list[node] + weight < distance_list[child]) {
      distance_list[child] = distance_list[node] + weight;
      heap.emplace(distance_list[child], child);
    }
  }
}
```

- The Distance Vector algorithm is a decentralized routing algorithm that computes the least-cost path based on an iterative and distributed approach. Each node has the knowledge of the costs of its own attached links and calculates the least-cost path to a destination or set of destinations through an iterative exchange of information.
  - Let $d_a(b)$ be the cost of the least-cost path from $a$ to $b$, which is initialized as an estimate of the cost oftheleast-costpath.
  - When $a$ receives an updated $d_v$ from $v$, it updates $d_a$ with $d_a(b) = \min \{ c(a, v) + d_v(b) \}$. If there's an update, it sends $d_a$ to all its neighbors.
  - In poisoned reverse, when a router $a$ routes through $b$ to reach $c$, it advertises $d_a(c)$ to $b$ with an infinite distance to prevent $b$ from selecting $c$ as the next hop to reach $a$, which results in a routing loop.

### Routing Protocol

Routers are organized into autonomous systems, with each AS consisting of a group of routers that are under the same administrative control. Routers within the same AS all run the same routing algorithm and have information about each other. The routing algorithm running within an autonomous system is called an intra-autonomous system routing protocol.

#### OSPF

OSPF is a link-state protocol that uses flooding of link-state information and a Dijkstra's least-cost path algorithm. With OSPF, each router constructs a complete topological map of the entire autonomous system. Each router runs Dijkstra's shortest-path algorithm to determine a shortest-path tree to all subnets, with itself as the root node.

With OSPF, a router broadcasts routing information to all other routers in the autonomous system. Each router broadcasts link-state information through a link-state packet both in a fixed period and whenever there is a change in a link's state.

OSPF advertisements are contained in OSPF messages that are carried with IP. The OSPF protocol also checks that links are operational (through a HELLO message that is sent to an attached neighbor) and allows an OSPF router to obtain a neighboring router's database of network-wide link state.

### BGP

BGP is an inter-autonomous system routing protocol. In BGP, packets are not routed to a specific destination address, but instead to CIDR prefixes, with each prefix repsenting a subnet. BGP enables each router to obtain prefix information from neighboring ASs and determine the best routes to the prefixes. In BGP, each pair of routers exchange routing information over a BGP connection, where an external BGP connection spans ASs.

Each AS contains gateway routers that connect to other ASs and internal routers that connect to hosts and routes within its own AS. Each router could advertise the path to its subnet or a subnet of its neighbor. The advertisement message includes several BGP attributes, such as `AS-PATH` and `NEXT-HOP`. When a prefix is passed to an AS, the AS adds its ASN to the existing list in the `AS-PATH`. The `NEXT-HOP` is the IP address of the router interface that begins the `AS-PATH`.

#### Hot Potato Routing

In hot potato routing, the route chosen from among all possible routes is that route with the least cost to the `NEXT-HOP` router beginning that route. When a router learns from the BGP protocol that a subnet $a$ is reachable through multiple `NEXT-HOP` routers, it uses routing information from OSPF to determine costs of least-cost paths to each of the `NEXT-HOP` router and determines the corresponding interface $I$ with the forwarding table.

#### Route-Selection Algorithm

For each destination prefix, the input into BGP's route-selection algorithm is the set of all routes to that prefix that the router has learned.

- Each route is assigned a local preference value as an attribute, which could be set in the router or learned from another router in the same AS. The routes with the highest local preference values are selected.
- For the routes with the maximum local preference, the routes with the shortest `AS-PATH` is selected.
- For the routes with the minimum `AS-PATH`, hot potato routing is used.
