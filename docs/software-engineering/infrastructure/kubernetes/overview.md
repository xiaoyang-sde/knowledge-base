# Overview

## What is Kubernetes?

Kubernetes is a portable, extensible, open-source platform for managing containerized workloads and services, that facilitates both declarative configuration and automation.

- Traditional deployment era: Applications are deployed on physical servers. There was no way to define resource boundaries for applications in a physical server, and this caused resource allocation issues.
- Virtualized deployment era: Virtualization allows applications to be isolated between multiple Virtual Machines on a single physical server's CPU.
- Container deployment era: Containers are considered lightweight and portable across clouds and OS distributions.

## Kubernetes Components

- Pod: The set of running containers.
- Node: The working machine that run containerized applications.
- Cluster: The deployed Kubernetes that contains a set of nodes.
- Control plane: The container orchestration layer that exposes the API and interfaces to manage the lifecycle of containers.

### Control Plane Components

The control plane's components make global decisions about the cluster and responde to cluster events.

- kube-apiserver: The API server is a component of the Kubernetes control plane that exposes the Kubernetes API.
- etcd: Consistent and highly-available key value store used as Kubernetes' backing store for all cluster data.
- kube-scheduler: Control plane component that watches for newly created Pods with no assigned node, and selects a node for them to run on.
- kube-controller-manager: Control plane component that runs controller processes.
- cloud-controller-manager: A Kubernetes control plane component that embeds cloud-specific control logic.

### Node Components

Node components run on every node, maintaining running pods and providing the Kubernetes runtime environment.

- kubelet: An agent that runs on each node in the cluster. It makes sure that containers are running in a Pod.
- kube-proxy: A network proxy that runs on each node in your cluster, implementing part of the Kubernetes Service concept.
- Container runtime: The software that is responsible for running containers. (e.g. Docker, containerd, CRI-O)

### Addons

Addons use Kubernetes resources (DaemonSet, Deployment, etc) to implement cluster features.

- DNS: Cluster DNS is a DNS server  which serves DNS records for Kubernetes services.
- Web UI: Dashboard is a general purpose, web-based UI for Kubernetes clusters
- Container Resource Monitoring: Container Resource Monitoring records generic time-series metrics about containers in a central database, and provides a UI for browsing that data.
- Cluster-level Logging: A cluster-level logging mechanism is responsible for saving container logs to a central log store with search/browsing interface.
