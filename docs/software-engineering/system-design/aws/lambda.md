# Lambda

## Introduction

Lambda is a compute service that runs the code on a high-availability compute infrastructure and performs all of the administration of the compute resources, including server and operating system maintenance, capacity provisioning and automatic scaling, code monitoring and logging.

- Lambda is an event-driven computing service that it runs the code in response to events such as changes to data in a S3 bucket.
- Lambda is charged with number of requests and duration.
- Lambda functions response to HTTP requests using API Gateway or API calls made using AWS SDKs.
- Lambda functions are independent, but they could trigger other functions.

## Concepts

- Function: The resource with the code to process the events passed into the function by the user or other AWS services.
- Trigger: The resource or configuration that invokes a Lambda function. The event source mapping is a resource in Lambda that reads items from a stream or queue and invokes a function.
- Event: The JSON-formatted document that contains data for a Lambda function to process.
- Execution environment: The secure and isolated runtime environment for each Lambda function.
- Deployment package: The `.zip` file archive that contains the function code and its dependencies, or the container image that is compatible with the Open Container Initiative specification.
- Layer: The .zip file archive that can contain additional code or other content. (e.g. libraries, custom runtime, data, or configuration files)
- Concurrency: The number of requests that your function is serving at any given time.

## AWS CLI

List the Lambda functions, retrieve a function, and delete a function:

```sh
aws lambda list-functions --max-items 10

aws lambda get-function --function-name my-function

aws lambda delete-function --function-name my-function
```

Create the execution role:

```sh
aws iam create-role --role-name lambda-ex --assume-role-policy-document file://trust-policy.json
```

The `trust-policy.json` file is a JSON file in the current directory that defines the trust policy for the role.

```json
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Effect": "Allow",
      "Principal": {
        "Service": "lambda.amazonaws.com"
      },
      "Action": "sts:AssumeRole"
    }
  ]
}
```

Create a deployment package for `index.js`:

```sh
zip function.zip index.js
```

```ts title="index.js"
exports.handler = async function(event, context) {
  console.log("ENVIRONMENT VARIABLES\n" + JSON.stringify(process.env, null, 2))
  console.log("EVENT\n" + JSON.stringify(event, null, 2))
  return context.logStreamName
}
```

Deploy `function.zip` to Lambda:

```sh
aws lambda create-function --function-name my-function \
--zip-file fileb://function.zip --handler index.handler --runtime nodejs12.x \
--role arn:aws:iam::<account-id>:role/lambda-ex
```

Invoke the function with AWS CLI:

```sh
aws lambda invoke --function-name my-function out --log-type Tail
```
