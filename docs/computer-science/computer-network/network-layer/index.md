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
