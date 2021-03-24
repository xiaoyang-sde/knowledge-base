# Virtual Private Cloud (VPC)

## Introduction

VPC is a virtual data center in the cloud, letting you provision a logically isolated section of AWS where you can launch AWS resources in a virtual network that you define. You can create public-facing subnet for your web servers that has access to the Internet, and place your backend systems in private-facing subnet with no Internet access. Additionally, you can create a Hardware VPN connection between your corporate data center and your VPC and leverage the AWS cloud as an extension of your corporate data center.

### What is VPC?

*   Think of a VPC as a logical data center in AWS.
*   Consists of IGWs, Route Tables, Network Access Control Lists, Subnets, and Security Groups.
*   1 Subnet must in 1 AZ.
*   Security Groups are Stateful; Network ACL are Stateless.
*   NO TRANSITIVE PEERING.

### What can we do with VPC?

*   Launch instances into a subnet of your choosing
*   Assign custom IP address ranges in each subnet. The largest subnet you could use in your VPC is 10.0.0.0/16. (from 10.0.0.1 to 10.0.225.254)
*   Configure route tables between subnets.
*   Create internet gateway and attach it to our VPC.
*   Much better security control over your AWS resources: Instance security groups and subnet network access control lists.

### Default VPC and Custom VPC

*   Default VPC is user friendly, allowing you to immediately deploy instances.
*   All subnets in default VPC have a route out to the Internet.
*   Each EC2 instance has both a public and private IP address.

### VPC Peering

*   Allows you to connect one VPC with another via a direct network route using private IP addresses,
*   Instances behave as if they were on the same private network.
*   You can peer VPC’s with other AWS accounts as well as with other VPCs in the same account.
*   Peering is in a star configuration: 1 central VPC peers with 4 others. However, you can’t peer across regions.

### Create a VPC

*   When you create a VPC a default Route Table, Network ACL, and a default Security Group.
*   It won’t create any subnets or internet gateway.
*   AZs are randomized across different accounts.
*   You can only have one internet gateway per VPC.
*   Security Groups can’t span VPCs.

### NAT Gateway

#### NAT Instance

*   When creating a NAT instance, disable Source/Destination check on the instance.
*   NAT instances must be in a public subnet.
*   There must be a route out of the private subnet to the NAT instance, in order for this to work.
*   The amount of traffic that NAT instances can support depends on the instance size. If you are bottlenecking, increase the instance size.
*   You can create high availability using Autoscaling Groups, multiple subnets in different AZs, and a script to automate failover.

#### NAT Gateways

*   Redundant inside the AZ.
*   Preferred by the enterprise.
*   Starts at 5Gbps and scales currently to 45Gbps.
*   No need to patch, and no associated with security groups.
*   Automatically assigned a public IP address.
*   You need to update route tables.
*   No need to disable source/destination checks.

### Network Access Control List

*   Your VPC automatically comes with a default network ACL, and by default it allows all outbound and inbound traffic.
*   You can create custom network ACLs. Each custom network ACLs denies all inbound and outbound traffic until you add rules.
*   Each subnet in your VPC must be associated with a network ACL.
*   Block IP addresses using network ACLs not security groups.

*   An ACL could be associate with multiple subnets.
*   ACLs are stateless; responses to allow inbound traffic are subject to the rules for outbound traffic.

### VPC Flow Logs

VPC Flow Logs is a feature that enables you to capture information about the IP traffic going to and from network interfaces in your VPC. Flow log data is stored using Amazon CloudWatch Logs. After you’ve created a flow log, you can view and retrieve its data in Amazon CloudWatch Logs.

*   You can only enable flow logs for VPCs that are peered with your VPC unless the peer VPC is in your account.
*   You can’t tag a flow log.
*   You can’t change its configurations.
*   Traffic to Amazon DNS server, Windows instance for Amazon Windows license activation, metadata, reserved IP for VPC router, and DHCP traffic won’t be logged.

### Bastion Host

Bastion Host is a special purpose computer designed to withstand attacks. 

*   A NAT Gateway or NAT instance is used to provide internet traffic to EC2 instances in a private subnets.
*   A bastion is used to securely administer EC2 instances.
*   You can’t use a NAT gateway as a Bastion host.

### Direct Connect

Direct Connect is a cloud service solution that makes it easy to establish a dedicated network connection from your premise to AWS.

*   Direct Connect directly connects your data center to AWS.
*   Useful for high throughput workloads or when you need a stable and reliable secure connection.

### VPC Endpoints

VPC Endpoints enable you to privately connect your VPC to supported AWS services and VPC endpoint services powered by PrivateLInk without requiring an internet gateway, NAT device, VPN connection, or AWS Direct Connect connection.

*   Interface Endpoints
*   Gateway Endpoints: Amazon S3, DynamoDB

## Elastic Load Balancer (ELB)

**Application Load Balancers** are used for load balancing of HTTP and HTTPS traffic. They operate at Layer 7 and are application-aware. You can create advanced request routing, sending sepecifies request to specific web servers.

**Network Load Balancers** are used for load balancing of TCP traffic where extreme performance is required. Operating at the connection level (Layer 4), Network LB are capable of handling millions of requests per second.

**Classic Load Balancers** are legacy ELB. You can load balance HTTP/HTTPS applications and use Layer 7-specific features, such as X-Forwarded and sticky sessions. You can also use strict Layer 4 load balancing for applications that rely purely on TCP protocol. Return a 504 error when application stops responding.

*   Instances monitored by ELB are reported as InService or OutofService.
*   Health Checks check the instance health by talking to it.
*   LBs have their own DNS name. You are never given an IP address.

### Sticky Sessions

Basically, Classic Load Balancer routes each request independently to the registered EC2 instance with the smallest load. However, sticky sessions allow you to bind a user’s session to a specific EC2 instance. This ensures that all requests from the user during the session are sent to the same instance. You can enable sticky sessions for Application LB as well, but the traffic will be sent to target groups.

### Cross Zone Load Balancing

By default, load balancer can’t send traffic across AZs. However, you could enable cross zone load balancing to solve that problem.

### Path Patterns

You can create a listener with rules to forward requests based on the URL path. This is known as path-based routing. For example, you can route regular requests to one EC2, and image compression requests to another EC2.

### HA Architecture

Everything fails. You should always plan for failure.

*   Always design for failure.
*   Use multiple AZs and multiple regions wherever you can.
*   Know the difference between multi-az and read replicas for RDS.
*   Know the difference between scaling out (add more instances) and scaling up (increase the resource for instance).
*   Consider the cost element.
*   Know the different S3 storage classes.

## CloudFormation

*   Is a way of completely scripting your cloud environment.
*   Quick Start is a bunch of CloudFormation templates already built by AWS SAs allowing you to create complex environments quickly.

## Elastic Beanstalk

AWS Elastic Beanstalk is an easy-to-use service for deploying and scaling web applications and services developed with Java, .NET, PHP, Node.js, Python, Ruby, Go, and Docker on familiar servers such as Apache, Nginx, Passenger, and IIS.
