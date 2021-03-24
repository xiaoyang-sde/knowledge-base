# Elastic Computing Cloud (EC2)

## Introduction

EC2 is a web service that provides resizable compute capacity in the cloud. It will reduce the time required to obtain and boot new server instances to minutes, allowing you to quickly scale capacity, as your computing requirements change.

### EC2 Pricing models

On demand: Pay a fixed rate by the hour with no commitment.

*   Users that want the low cost and flexibility of EC2 without any up-front payment or long-term commitment.
*   Applications with short term, spiky, or unpredictable workloads that cannot be interrupted.
*   Applications being developed or tested on EC2 for the first time.

Reserved: Provides a capacity reservation with a significant discount. Contract terms are 1 year or 3 year terms.

*   Applications with steady state or predictable usage.
*   Applications that require reserved capacity.
*   Users able to make up-front payments to reduce their total computing cost even further.

*   Standard reserved instances: The more you pay up-front and the longer the contract, the greater the discount.
*   Convertible reserved instances: Allow users to change attributes of the reserved instance as long as the exchange results in the creation of reserved instances of equal or greater value.
*   Scheduled reserved instances: Available to launch within the time windows you reserve.

Spot: Enables you to bid the price for instance capacity, providing for even greater savings if your applications have flexible start and end times.

*   Applications that have flexible start and end times.
*   Applications that are only feasible at very low compute prices.
*   Users with urgent computing needs for large amounts of additional capacity.

Dedicated hosts: Physical EC2 server dedicated for your use, which can help you reduce costs by allowing you to use your existing server.

*   Useful for regulatory requirements that may not support multi-tenant virtualization.
*   Great for licensing which does not support multi-tenancy or cloud deployments.
*   Can be purchased on demand (hourly).
*   Can be purchased on reservation.

### EC2 Lab

Termination Protection is turned off by default,, you must turn it on manually.
On an EBS-backed instance, the default action is for the root EBS volume to be deleted when the instance is terminated.
EBS root volume of your default AMI’s cannot be encrypted. This can be done when creating AMI in the AWS console or API. However, additional volumes can be encrypted.

### Security Groups

*   All inbound traffic is blocked by default.
*   All outbound traffic is allowed.
*   Changes to security groups take effect immediately.
*   You can have any number of EC2 instances within a security group.
*   You can have multiple security groups attached to EC2 instances.
*   Security groups are STATEFUL, which means that if you enable inbound traffic on specific port, its outbound traffic will be allowed correspondingly.
*   You cannot block specific IP addresses using security groups, instead use Network Access Control Lists. Also, you can only specify allow rules, but not deny rules.

### Elastic Block Store (EBS)

Essentially, EBS provides virtual block storage volumes for EC2 instances. Each volume is replicated within its availability zone to protect you from component failure, offering high availability and durability.

#### Different types of EBS storage

*   General Purpose (SSD): Balance price and performance for a wide variety of transactional workloads.
*   Provisioned IOPS (SSD): Highest-performance SSD volume designed for mission-critical applications. Typically designed for Databases.
*   Throughput Optimised Hard Disk Drive: Low cost HDD volume designed for frequently access, throughput intensive workloads. Typically designed for Big Data and Data Warehouses.
*   Cold Hard Disk Drive: Lowest cost HDD volume designed for less frequently accessed workloads. Designed for File Servers.
*   Magnetic: Previous generation HDD. Workloads where data is infrequently accessed.

Volumes exist on EBS. Think of EBS as a virtual hard disk.
Snapshots exist on S3. Think of snapshots as a photograph of the disk.
Snapshots are point in time copies of volumes.
Snapshots are incremental; however, first snapshot may take time to create.

To create a snapshot for Amazon EBS volumes that serve as root device, you should stop the instance before taking the snapshot; however, you can take a snapshot while the instance is running.
You can create AMI from both volumes and snapshots.

You can change the EBS volume sizes on the fly, including changing the size and storage type.

Volumes will always be in the same availability zone as the EC2 instance.
To move an EC2 volume from one AZ to another, take a snapshot of it, create an AMI from the snapshot and then use the AMI to launch the EC2 instance in a new AZ. By copying the AMI from one region to the other, you could move EC2 instance to another region.

#### AMI Types (EBS and Instance Store)

You can select your AMI based on:
Region
Operating system
Architecture (32-bit or 64-bit)
Launch permissions
Storage for the Root device (Root device volume)

*   Instance Store (Ephemeral Storage)
*   EBS Backed Volumes

#### EBS Volumes

The root device for an instance launched from the AMI is an Amazon EBS volume created from an EBS snapshot.

EBS backed instances can be stopped. You will not lose the data on this instance if it’s stopped or rebooted.

By default, EBS volumes will be deleted on termination. However, with EBS volumes, you can tell AWS to keep the root device volume.

#### Instance Store Volume

The root device for an instance launched from the AMI is an instance store volume created from a template stored in Amazon S3.

Instance Store Volumes are sometimes called Ephemeral Storage since it cannot be stopped. You will lose all data is the host fails.

#### Encrypted Root Device Volumes and Snapshots

Snapshots of encrypted volumes are encrypted automatically.

Volumes restored from encrypted snapshots are encrypted automatically.

You can share snapshots with other AWS accounts or made public, but only if they are encrypted.

You can encrypted root device while creating EC2 instance. However, if your device is unencrypted at started, you could create a snapshot, create an encrypted copy of it, and create an AMI from that snapshot, and use that AMI to launch an encrypted instance.

CloudWatch
----------

Amazon CloudWatch is a monitoring service watching services and applications you are running on AWS.

*   Compute: EC2, Autoscaling groups, Elastic load balancers, Route53 health checks.
*   Storage and Content delivery: EBS volumes, Storage gateways, CloudFront.

Host level metrics consist of CPU, Network, Disk, Status Check.
CloudWatch with EC2 will monitor events every 5 minutes by default; however, you can have 1 minute intervals by turning on detailed monitoring. Moreover, you can create CloudWatch alarms which trigger notifications.

*   Dashboards: Creates awesome dashboards to see what is happening with your AWS environment.
*   Alarms: Allows you to set Alarms that notify you when particular thresholds are hit.
*   Events: CloudWatch events helps you to respond to state changes in your AWS resources.

## CloudTrail

AWS CloudTrail basically records AWS Management Console actions and API calls to increase the security of your account.

## AWS Command Line (CLI)

You can interact with AWS from anywhere in the world just by using the CLI. You will need to set up access in IAM.
Commands themselves are not in the exam, but some basic commands will be useful to know for real life.

### Using IAM roles with EC2

User will grant permissions to individuals, while a role is intended to be assumable by anyone who needs it.
Roles are more secure than storing your access key and secret access key on individual EC2 instances and are easier to manage.
Roles can be assigned to an EC2 instance after it is created using both the console and command line.
Roles are universal. You can use them in any region.

### Instance Metadata

Metadata is used to get information about an instance, such as public IP address.

User data: curl [http://169.254.169.254/latest/user-data](http://169.254.169.254/latest/user-data)

Meta data: curl [http://169.254.169.254/latest/meta-data](http://169.254.169.254/latest/user-data)

## Elastic File System (EFS)

Amazon Elastic File System is a file storage service for Amazon EC2, which provides a simple interface for users to create and configure file system. With EFS, storage capacity shrinks and grows automatically as you add or remove files, so your application will have the storage they demand.

*   EFS supports NFSv4 protocol.
*   You only pay for the storage you use.
*   Can scale up to petabytes.
*   Can support thousands of concurrent NFS connections.
*   Data is stored in multiple AZ’s within one region.
*   Read after write consistency.

### **EC2 Placement Groups**

#### Cluster Placement Groups

A cluster placement group is a grouping of instances within a single AZ. Placement groups are recommended for applications that need low network latency, high network throughput, or both. Only certain instances can be launched into a cluster placement group.

#### Spread Placement Groups

A spread placement group is a group of instances that are each placed on distinct underlying hardware. Spread placement groups are recommended for applications that have a small number of critical instances that should be kept separate from each other to protect them from rack fails.

#### Partitioned Placement Groups

Amazon divides each group into logical segments called partitions. Amazon EC2 ensures that each partition within a placement group has its own set of racks. No two partitions within a placement group share the same racks, allowing you to isolate the impact of hardware failure within your application.
