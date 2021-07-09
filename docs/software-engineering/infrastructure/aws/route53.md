# Route 53

## Introduction

DNS is used to convert human friendly domain names into an Internet Protocol (IP) address.

Alias records are used to map resource record sets in your hosted zone to Elastic Load Balancers, CloudFront distributions, or S3 buckets that are configured as websites. Divergent from CNAME records, it can be set to naked domain names (zone apex record).

*   ELBs don’t have pre-defined IPv4 addresses; you resolve to them using a DNS name.
*   Always choose Alias over CNAME.
*   You can buy domain names directly with AWS within 3 days to register.

### Routing Policies

#### Simple Routing

If you choose the simple routing policy you can only have one record with multiple IP addresses. If you specify multiple values in a record, Route 53 will return them all to users in random order.

#### Weighted Routing

Weighted routing allows you to split your traffic based on different weights assigned.

##### Health Checks

*   You can set health checks on individual record sets.
*   If a record set fails a health check it will be removed from Route53 until it passes the health check.
*   You can set SNS notifications to alert if a health check is failed.

#### Latency-based Routing

Latency-based routing allows you to route your traffic based on the lowest network latency for your end user. To use latency-based routing, you create a latency resource record set for the EC2 or ELB resource in each region that hosts your website.

#### Failover Routing

Failover routing policies are used when you want to create an active/passive set up. For example, you may want your primary site to be in EU-WEST-2 and your secondary DR site in AP-SOUTHEAST-2. Route53 will monitor the health of each server and send traffic to active server.

#### Geolocation Routing

Geolocation routing lets you choose where your traffic will be sent based on the geographic location of your users.

#### Geo Proximity Routing (Traffic Flow Only)

Geolocation routing lets you choose where your traffic will be sent based on the geographic location of your users. However, you could also optionally choose to route more traffic or less to a given resource by specifying a value, known as a bias.

#### Multivalue Answer Routing

Multivalue Answer routing allows you to configure Route53 to return multiple values, such as IP addresses for your web servers, in response to DNS queries. You can specify multiple values for almost any record, but also allows you to check the health of each resource and only return healthy ones. (Simple routing with health checks.)
