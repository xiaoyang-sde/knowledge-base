# Monitoring Distributed Systems

## Definition

- Monitoring: Collecting, processing, aggregating, and displaying real-time quantitative data about a system, such as query counts and types, error counts and types, processing times, and server lifetimes.
  - White-box monitoring: Monitoring based on metrics exposed by the internals of the system, including logs, interfaces like the Java Virtual Machine Profiling Interface, or an HTTP handler that emits internal statistics.
  - Black-box monitoring: Testing externally visible behavior as a user would see it.
- Dashboard: Web-based application that provides a summary view of a service's core metrics.
- Alert: Notifications that are pushed to a system such as a bug or ticket queue, an email alias, or a pager.
- Root cause: The defect in a software or human system that, if repaired, instills confidence that this event won't happen again in the same way.
- Node and machine: The single instance of a running kernel in either a physical server, virtual machine, or container.

## The Four Golden Signals

- Latency: The time it takes to service a request. It's important to distinguish between the latency of successful requests and the latency of failed requests.
- Traffic: The measure of how much demand is being placed on the system, measured in a high-level system-specific metric. For a web service, the measurement could be HTTP requests per second. For a key-value storage system, the measurement could be transactions and retrievals per second.
- Error: The rate of requests that fail, such as HTTP 500s. Where protocol response codes are insufficient to express all failure conditions, internal protocols might be necessary to track partial failure modes.
- Saturation: The measure of the system fraction, emphasizing the resources that are most constrained.

## Symptom and Cause

The monitoring system should address the symptom and cause of an indicent. The "what's broken" indicates the symptom and the "why" indicates a cause. For example, HTTP 500s is a symptom and the database is refusing connections is a cause.

## Black-Box and White-Box

The simplest way to think about black-box monitoring versus white-box monitoring is that black-box monitoring is symptom-oriented and represents active—not predicted—problems. White-box monitoring depends on the ability to inspect the innards of the system, such as logs or HTTP endpoints, with instrumentation. White-box monitoring therefore allows detection of imminent problems, failures masked by retries, and so forth.

## Tail Performance

The simplest method to differentiate between a slow average and a slow tail of requests is to collect request counts bucketed by latencies, rather than actual latencies. The buckets boundaries could be distributed exponentially, such as 0 ms - 10 ms, 10 ms - 30 ms, 30 ms - 100 ms, and 100 ms - 300 ms.

## Appropriate Resolution for Measurement

Different aspects of a system should be measured with different levels of granularity. For example, observing CPU load over the time span of a minute won’t reveal even quite long-lived spikes that drive high tail latencies. Collecting per-second measurements of CPU load might yield interesting data, but such frequent measurements may be very expensive to collect, store, and analyze.
