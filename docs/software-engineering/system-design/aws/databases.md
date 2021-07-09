# Database Servers

## Introduction

**Relational databases** are what most of us are all used to. They have been around since the 70’s. You may think of traditional spreadsheet.

Relational databases on AWS: SQL Server, Oracle, MySQL Server, PostgreSQL, Aurora, MariaDB.

RDS has two key features: Multi-Az for disaster recovery; Read replicas for performance.

**DynamoDB** (NoSQL): Non Relational databases. (Key-value and document)

**RedShift** (OLAP): Business analytics and data warehouse.

**Elasticache** (Redis and Memcached): Speed up existing databases.

## Relational Database Service (RDS)

*   RDS runs on virtual machines.
*   You cannot log in to these operating systems.
*   Patching of the RDS operating system and databases is Amazon’s responsibility.
*   RDS is not serverless; however, Aurora Serverless is serverless.

### RDS Backups, Multi-Az, and Read replicas

**Automated backups** allow you to recover your database to any point in time within a “retention period.” The retention period can be between one and 35 days. It will take a full daily snapshot and will also store transaction logs throughout the day. It’s enabled by default and the data is stored in S3 and you get free storage space equal to the size of your database. So if you have an RDS instance of 10 GB, you will get 10 GB worth of storage. While backing up data, you may experience elevated latency.

**DB snapshots** are done manually. They are stored even after you delete the original RDS instance, unlike automated backups. 

**Encryption at rest** is supported for MySQL, Oracle, SQL Server, PostgreSQL, Maria DB, and Aurora. Encryption is done using the AWS Key Management Service. Once your RDS instance is encrypted, the data stored at rest in the underlying storage is encrypted, as are its automated backups, read replicas, and snapshots.

**Multi-AZ** allows you to have an exact copy of your production database in another availability zone. AWS handles the replication for you, so when your production database is written to, this write will automatically be synchronized to the standby database. While planned database maintenance, DB instance failure, or AZ failure, RDS will automatically failover to the standby so that database operations can resume quickly without administrative intervention.

**Read replica** allows you to have a read-only copy of your production database. This is achieved by using Asynchronous replication from the primary RDS instance to the read replica. 

*   You could only have 5 copies for a database. 
*   Each read replica will have its own DNS endpoint.
*   You can have read replicas that have multi-AZ.
*   You can create read replicas of multi-AZ source databases.
*   You can have a read replica in another region.
*   Can be promoted to master, this will break the Read Replica.

## DynamoDB


Amazon DynamoDB is a NoSQL database solution for applications that need consistent, single-digit millisecond latency at any scale. It’s a fully managed database and supports both document and key-value data models.

*   Stored on SSD.
*   Spread across 3 geographically distinct data centers.
*   Eventual consistent reads (Default): Consistency across all copies of data is usually reached within a second. Repeating a read after a short time should return the updated data.
*   Strongly consistent reads: A strongly consistent read returns that reflects all writes that received a successful response prior to the read.

## Redshift

**Amazon Redshift** is a fast and powerful data warehouse service in the cloud.

OLAP transaction example: Net profit for EMEA and Pacific for the Digital Radio product. (Pulls in large numbers of records.)

Data warehousing database use different type of architecture both from a database perspective and infrastructure layer.

**Single Node:** 160GB

**Multi-Node:** Leader Node manages client connections and receives queries, while compute node stores data and perform queries and computations. (Up to 128 compute nodes.)

**Advanced Compression:** Amazon Redshift employs multiple compression relative to traditional relational data stores. In addition, Redshift doesn’t require indexes or materialized views, which would use less space than traditional systems. Compression scheme will be automatically selected.

**Massively Parallel Processing:** Amazon Redshift automatically distributes data and query load across all nodes. Redshift makes it easy to add nodes to your data warehouse and enables you to maintain fast query performance as your data warehouse grows.

**Pricing:** You will be billed according to your Compute node hours. Only compute nodes will incur charges.

### **Backups**

*   Enabled by default with a 1 day retention period.
*   Maximum retention period is 35 days. 
*   Maintain at least three copies of your data. Original and replica on the compute nodes and a backup in S3.
*   Asynchronously replicate your snapshots to S3 in another region.

**Encryption:** Encrypted in transit using SSL. Encrypted at rest using AES-256 encryption.

**Availability:** Redshift currently only available in 1 AZ; however you can store snapshot to different zones.

## Aurora

Aurora is a MySQL-compatible, relational database engine that combines the speed and availability of high-end commercial databases with the simplicity and cost-effectiveness of open source databases.

*   Start with 10GB, automatically scales in 10GB increments to 64TB.
*   Compute resources can scale up to 32vCPUs and 244GB of memory.
*   2 copies of your data is contained in each availability zone, with a minimum of 3 availability zones. 6 copies of your data.
*   Aurora is designed to transparently handle the loss of up to two copies of data without affecting database write availability and up to three copies without affecting read availability.
*   Aurora storage is self-healing. Data blocks and disks are continuously scanned for errors and repaired automatically.
*   Aurora supports up to 15 Aurora replicas and up to 5 MySQL replicas.

### Backup

*   Automated backups are always enabled on Aurora DB instances. Backups do not impact performance.
*   You can also take snapshots and share them with other AWS accounts.

## ElastiCache

ElastiCache is a web service that makes it easy to deploy, operate, and scale an in-memory cache in the cloud. The service improves the performance of web applications by applying to retrieve information from a fast, managed, in-memory cache.

*   ElastiCache supports two engines: Redis and Memcached. 
*   Used to increase database and web application performance.
*   Redis is multi-AZ. 
*   You can do backups and restores of Redis.
