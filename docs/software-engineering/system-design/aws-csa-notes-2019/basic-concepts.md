# Basic Concepts

## Understanding AWS

What is a “Cloud”? Simply refer to a computer in Data Centers around the world that you are in some way utilizing it through Internet connection. Amazon Web Services is a Cloud service provider which also known as IaaS (Infrastructure as a Service).

Common personal uses of Cloud Services: Backup and sharing files across devices or individuals.

Common enterprise uses of Cloud Services: Using premise data centers may waste budget and time, and if the increase of actual usage does not meet previous estimations, start-up companies may encounter significant loss. By instead, using Cloud Services will solve these problems.

Considering the following terms:

*   High availability: any type of devices as long as they have Internet connection.
*   Fault tolerant: files storing in local hard drives may fail; however, those backed up in Cloud have a copy for you to access.
*   Scalability and Elasticity: Easily to shrink or grow based on customer’s demand.

## Virtual Private Cloud (VPC)

Take Facebook as a reference:  
Your Homepage is your private section where you can publish posts, photos, and videos, and have a security on top to allow merely specific individuals to view that page.

Your VPS is your private section where you can deploy AWS EC2, AWS RDS, and other infrastructures, and have a security on top to allow you to restrict or grant people permission to access.

## Elastic Cloud Compute (EC2)

EC2 is a virtual equivalent of a computer which consists of CPU, OS, Hard Drive, Network Card, Firewall, and RAM. A EC2 server is called an ‘instance’. Take Netflix as a reference: While you are visiting Netflix’s homepage, you act as a user who connects to an EC2 instance which stores the main features and functions of Netflix as a web hosting server.

When user hits play on Netflix, the EC2 instance goes to an S3 to hold video file convert by encoding or transcoding for he or she to review. In conclusion, EC2 does all the tasks requiring processing activities.

## Relational Database Service (RDS)

RDS is a database platform provided by Amazon which usually be used to store customer account information and inventory catalog. While you sign in your Netflix account, EC2 will retrieve your credential information from an RDS database. After signing in, EC2 will hold the video catalog from the same source for you.

Typically, users of Netflix will increase at 7 pm when people finish dinner and watch television. In order to handle increasing resource costs, AWS will automatically deploy additional EC2 instances to process requests, which is a main feature of cloud computing service. While specific server unfortunately crashed, AWS will redirect users to another available server and move them back when issue solved. Due to the ability to adjust its services, Netflix is highly available to customers around the world.

## Simple Storage Service (S3)

AWS S3 is an unlimited storage bucket which individual companies would never reach its capacity. It’s the place where Netflix saves their video contents, acting as hard drives in personal computers.

## AWS Global Infrastructure

AWS has regions spread around the world since customers are willing to reduce Internet transmission latency between physical servers and their users. An AWS region is comprised of more than one availability zone which contains one or more Data Center. Users’ data are backed up across availability zones within a particular region. Moreover, there’s a few edge locations which are endpoints used by CloudFront CDN for caching content.