# Applications

## Simple Queue Service (SQS)

SQS is a web service that gives you access to a message queue that can be used to store messages while waiting for a computer to process them.

*   Messages can contain up to 256kb of text in any format.
*   Messages can be kept in the queue from 1 min to 14 days. Default it 4 days.
*   Any component of a distributed application can store message in a fail-safe queue.
*   The queue acts as a buffer between the component producing and saving data, and the component receiving the data for processing.
*   Visibility timeout is the amount of time that the message is invisible in the SQS queue after a reader picks up that message. Provided the job is processed before the timeout expires, the message will then be deleted from the queue. If the job is not processed, the message will become visible again and another reader will process it.
*   Long polling is a way to retrieve messages from SQS queues. It won’t return until a message arrives in the queue, or the long poll timeout. 

### Standard Queues

*   Guarantee that a message is delivered at least once. 
*   Same order as they are sent.

### Fifo Queues

*   First in first out. Exact one time processing. 
*   Support multiple message groups. 
*   Same capability with standard queues.

## Simple Workflow Service (SWF)

SWF is a web service that makes it easy to coordinate work across distributed application components. Used in Amazon Warehouse, which involves human actions.  
Task-oriented API.  
SWF can last up for a 1 year.  
Assigned only once and never duplicated.  
Keep track of all the tasks and events in an application.

### SWF Actors

*   Workflow starters: An application that can initiate a workflow, such as e-commerce website following the placement of an order.
*   Deciders: Control the flow of activity tasks in a workflow execution.
*   Activity workers: Carry out the activity tasks.

# Simple Notification Service (SNS)

SNS is a simple service that makes it easy to set up, operate, and send notifications on the cloud.

*   Push notification to Apple, Google, Fire OS, and Windows devices.
*   As well as SMS texts, email, SQS, or HTTP endpoints.
*   Group multiple recipients using topics. A topic is an access point for allowing recipients to dynamically subscribe for identical copies of the same notification.
*   Messages are stored redundantly in multiple AZs.

### SNS Benefits

*   Instantaneous, push-based delivery.
*   Simple APIs and easy integration with applications.
*   Flexible message delivery over multiple transport protocols.
*   Inexpensive, pay-as-you-go model with no up-front costs.
*   Web-based AWS Management Console.

## Elastic Transcoder

*   Media transcoder in the cloud,
*   Convert media files from their original source format in to different formats.
*   Provides transcoding presets for popular output formats.

## API Gateway

Publish, maintain, monitor, and secure APIs in every detail.

It’s like a front door for applications to access data, business logic, or functionally from you backend service.

*   Expose HTTPS endpoints to define a RESTful API.
*   Serverlessly connect to services like Lambda or DynamoDB.
*   Send each API endpoint to a different target.
*   Low cost and scale effortlessly.
*   Track and control usage by API key.
*   Throttle requests to prevent attacks.

### How do I configure API Gateway?

*   Define an API.
*   Define resources and nested resources. (URL paths)
*   For each resource: Select supported HTTP methods, set security, and choose target (EC2, Lambda, DynamoDB), as well as set request and response transformations.
*   Enable CORS on API Gateway is you are using Javascript that uses multiple domains.

### How do I deploy API Gateway?

Deploy API to a stage:

*   Use API Gateway domain.
*   Use custom domain.
*   Free SSL/TLS certs.

### API Caching

With caching, you can cache your endpoint’s response and reduce the number of calls made to your endpoint and also improve the latency of the requests to your API.

## Kinesis

Streaming Data is data that generated continuously by thousands of data sources, which send in the data records simultaneously, and in small sizes, such as stock prices, game data, social network data, iOT sensor data. Kinesis is a platform on AWS to send your streaming data to.

### Kinesis Streams

Kinesis Streams could keep data for 24 hours or 7 days.

Shards: 5 transactions per second for reads, up to a maximum total data read rate of 2 MB per second and up to 1000 records per second for writes, up to a maximum total data write rate of 1 MB per second. The total capacity of the stream is the sum of the capacities of its shards.

### Kinesis Firehose

Inside the firehose, your data won’t be kept and you should analyze them immediately. Typically, there’s lambda in the firehose, which would process data and save the result into S3 or other sources.

### Kinesis Analytics

Analyze stream data, gain actionable insights, and respond to your business and customer needs in real time.

## Web Identity Federation – Cognito

Web Identity Federation lets you give your users access to AWS resources after they have successfully authenticated with a web-based identity provider like Facebook or Google.

*   Sign-up and sign-in to your apps.
*   Access for guest users.
*   Sync user data for multiple devices.
*   No need to store credentials locally.

Steps for login:

*   Login with Facebook Account. Facebook sends token to the user pool. User pool grant JWT Tokens to user.
*   User use the JWT Token to access the identity pool. The identity pool grants an AWS credentials to the user.
*   User use the AWS credentials to access AWS resources such as S3 buckets.