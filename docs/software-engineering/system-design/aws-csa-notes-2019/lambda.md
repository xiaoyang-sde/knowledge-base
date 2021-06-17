# Lambda

## Introduction

AWS Lambda is a compute service where you can upload your code and create a Lambda function. AWS Lambda takes care of provisioning and managing the servers that you use to run the code. You don’t have to worry about operating systems, patching, scaling, etc.

*   Event-driven compute service where AWS Lambda runs your code in response to events such as changes to data in a S3 bucket.
*   Response to HTTP requests using API Gateway or API calls made using AWS SDKs.
*   Charged with number of requests and duration.
*   Lambda functions are independent. 1 event is 1 function. However, Lambda can trigger other functions.
