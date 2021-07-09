# DynamoDB

## Designing and Architecting

### NoSQL Design for DynamoDB

In DynamoDB, the schema should be designed specifically to make the most common and important queries as fast and as inexpensive as possible.

- In RDBMS, data can be queried flexibly, but queries are relatively expensive and don't scale well in high-traffic situations.
- In a NoSQL database such as DynamoDB, data can be queried efficiently in a limited number of ways, outside of which queries can be expensive and slow.
