# Introduction

The book discusses the principles and the practicalities of data systems, and how to use them to build data-intensive applications. The goal is to build reliable, scalable, and maintainable data systems.

## Reliable

The software is reliable when the application performs the function that the user expected. For example, its performance should be good enough for the required use case, under the expected load and data volume.

Fault is defined as components of the system deviating from its spec. It's best to design fault-tolerance mechanisms that prevent faults from causing failures. Systems that could cope with faults are called fault-tolerant.

## Scalable

The system is scalable if its performance could cope with increased load. Load is described with load parameters, such as requests per second or cache hit rate. Performance is described as numbers, such as throughput or response time.

It's common to measure a distribution of response times. Tail latencies (high percentiles of response times) affect users' experience of the service. If an end-user request requires multiple backend calls, it needs to wait for the slowest service.

Scaling approaches contains moving to a more powerful machine or distributing the load across multiple smaller machines. Distributing a stateful data system might introduce complexities.

## Maintainable

The system is maintainable if it's operable, simple, and evolvable. Most of the cost of software is in its ongoing maintenance, such as fixing bugs, investigating failures, or migrating to new platforms.
