# Overview

## What is Kubernetes?

Kubernetes is a portable, extensible, open-source platform for managing containerized workloads and services, that facilitates both declarative configuration and automation.

- Traditional deployment era: Applications are deployed on physical servers. There was no way to define resource boundaries for applications in a physical server, and this caused resource allocation issues.
- Virtualized deployment era: Virtualization allows applications to be isolated between multiple Virtual Machines on a single physical server's CPU.
- Container deployment era: Containers are considered lightweight and portable across clouds and OS distributions.

## Kubernetes Components

The Kubernetes cluster consists of a set of working machines (nodes) that run containerized applications.

The work nodes host the Pods that are the components of the application workload. The control plane manages the nodes and the Pods in the cluster.

### Control Plane Components

The control plane's components make global decisions about the cluster and responde to cluster events.

- kube-apiserver: The component that exposes the Kubernetes API
- etcd: The component that servces as Kubernetes' backing store for all cluster data
- kube-scheduler: The component that watches for newly created Pods with no assigned nodes and selects a node for them to run on
- kube-controller-manager: The component that runs the controller (node controller, job controller, endpoints controller, service account and token controller) processes
- cloud-controller-manager: The component that embeds cloud-specific control logic

### Node Components

Node components run on each node in the cluster, maintaining running Pods and providing the Kubernetes runtime environment.

- kubelet: The agent that makes sure that containers are running in a Pod
- kube-proxy: The network proxy that implements part of the Kubernetes Service concept
- Container runtime: The software that is responsible for running containers (e.g. Docker, containerd, CRI-O)

### Addons

Addons use Kubernetes resources (DaemonSet, Deployment, etc) to implement cluster features.

- DNS: The required addon that serves DNS records for Kubernetes services
- Web UI: The general purpose, web-based UI for Kubernetes clusters
- Container Resource Monitoring: The addon that records generic time-series metrics about containers in a central database, and provides a UI for browsing that data
- Cluster-level Logging: The addon that is responsible for saving container logs to a central log store with search and browsing interface
