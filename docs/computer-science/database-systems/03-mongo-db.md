# MongoDB

MongoDB is a document database designed for ease of development and scaling. A record in MongoDB is a document, which is a data structure composed of field and value pairs. The values of fields may include other documents, arrays, and arrays of documents. MongoDB stores documents in collections. Collections are analogous to tables in relational databases.

## Retrieve Data

### Find

Read operations retrieve documents from a collection. The `find()` or `findOne()` method is called on the `Collection` object. The method accepts a query document that describes the documents to retrieve.

- The `find()` method returns a `Promise` that resolves a reference to a `Cursor` that contains matched documents.
- The `findOne()` method returns the matched document or `null`.

```ts
collection.find({
  name: 'Lemony Snicket',
  date: {
    $gte: new Date(new Date().setHours(00, 00, 00)),
    $lt: new Date(new Date().setHours(23, 59, 59)),
  },
});
```

### Sort

The `sort()` method could change the order of the result.

- To sort in ascending order, set the value of a field to `1`
- To sort in descending order, set the value of a field to `-1`

```ts
collection.find(query).sort({ length: -1 });
```

### Skip

The `skip()` method could omit documents from the beginning of the list of returned documents. Using `skip` without `sort` omits arbitrary documents.

```ts
collection.find(query).sort({ rating: -1 }).skip(2);
```

### Limit

The `limit()` method could cap the number of documents that can be returned from a read operation.

```ts
collection.find(query).sort({ rating: -1 }).limit(3);
```

### Projection

The `projection()` method could control the fields that appear in the documents returned by read operations. The two methods of projection are mutually exclusive.

- Explicitly include fields with a value of `1`
- Implicitly exclude fields with a value of `0`

The `_id` field is included by default because it' a unique identifier for each document.

```ts
collection.find(query).project({ name: 1 });
```

### Cursor

Read operations that return multiple documents do not immediately return all values matching the query. The cursor fetches documents in batches to reduce memory consumption. The cursor could be accessed with different paradigms.

- For each functional iteration: `await cursor.forEach(doc => console.log(doc));`
- Return an array of all documents: `const allValues = await cursor.toArray();`
- Asynchronous iteration: `for await (const doc of cursor)`

The cursor supports different utility methods.

- `count()`: Estimated count of the number of documents
- `rewind()`: Reset to its initial position
- `close()`: Free up the cursor's resources

## Write Operation

### Insert

MongoDB enforces the constraint that each document must contain a unique `_id` field.

- The `insertOne()` method inserts a single document and returns an `InsertOneResult` with `insertedId` property.
- The `insertMany()` method inserts multiple documents and returns an `InsertManyResult` with `ids` and `count` attributes.

```ts
collection.insertMany([
  { name: "Sicilian pizza", shape: "square" },
  { name: "New York pizza", shape: "round" },
]);
```

### Delete

- The `deleteOne()` method removes one existing document from a collection.
- The `deleteMany()` method removes multiple existing documents from a collection.

```ts
collection.deleteOne({
  pageViews: {
    $gt: 10,
    $lt: 32768
  },
});
```

### Update

To perform an update to one or more documents, create an update document that specifies the update operator and the fields and values that describe the change.

The update document contains one or more of the following update operators:

- `$set`: replaces the value of a field with a specified one
- `$inc`: increments or decrements field values
- `$rename`: rename fields
- `$unset`: remove fields
- `$mul`: multiplies a field by a specified number

```ts
const filter = { _id: 465 };
const updateDocument = {
  $set: {
    z: 42,
  },
};
const options = { upsert: true };

collection.updateOne(filter, updateDocument, options);
```

The `upsert` option controls whether to insert the document if there's no matched document in the collection.

### Replace

To perform a replacement operation, create a replacement document that consists of the fields and values to insert in the replace operation.

```ts
const filter = { _id: 465 };
const replacementDocument = { z: 42 };
const options = { upsert: true };

collection.replaceOne(filter, replacementDocument, options);
```

The `upsert` option controls whether to insert the document if there's no matched document in the collection.

## Specify a Query

Query documents contain one or more query operators that apply to specific fields which determine which documents to include in the result set.

### Literal Value

Literal values query data based on field-value pairs in a collection. Literal value queries are equivalent to the `$eq` comparison operator.

```ts
collection.find({ name: 'apples' });
```

### Comparison Operator

Comparison operators query data based on comparisons with values in a collection.

- `$gt`: greater than
- `$lt`: less than
- `$eq`: equal to
- `$ne`: not equal to

```ts
collection.find({
  qty: {
    $gt: 5,
    $lt: 10,
  },
});
```

### Logical Operator

Logical operators query data based on logic applied to the results of field-level operators. When a query document contains multiple elements, these elements are combined together with an implicit `$and` logical operator.

```ts
collection.find({
  $or: [
     { rating: { $eq: 5 }},
     { qty: { $gt: 4 }},
  ],
})
```

### Element Operator

Element operators query data based on the presence, absence, or type of a field.

```ts
collection.find({
  qty: {
    $exists: true,
  },
  name: {
    $exists: false,
  },
});
```

### Evaluation Operator

Evaluation operators query data based on the execution of higher level logic, like regular expression or text search.

```ts
collection.find({
  qty: {
    $mod: [3, 0],
  },
});
```

## Compound Operation

Compound operations combine read and write operations in a single atomic statement, so there's no chance of data changing in between a read and a subsequent write.

- `findOneAndDelete()`: match multiple documents and remove the first document
- `findOneAndUpdate()`: match multiple documents and update the first document
- `findOneAndReplace()`: match multiple documents and replace the first document

## Index

Index is a data structure that contains copies of parts of the data and supports the efficient execution of queries in MongoDB.

### Single Field and Compound Index

Single field index improves performance for queries that specify ascending or descending sort order on a single field of a document. (e.g. equality match or range query)

```ts
await collection.createIndex({ title: 1 });

await collection.find({ title: 'Batman' }).sort({ title: 1 });
```

Compound index improves performance for queries that specify ascending or descending sort order for multiple fields of a document.

```ts
await collection.createIndex({
  title: 1,
  genre: 1,
});

await collection.find({
  title: 'Batman',
  genre: 'Drama',
}).sort({
  title: 1,
  genre: 1,
});
```

### Unique Index

Unique index ensures that the indexed fields do not store duplicate values. By default, MongoDB creates a unique index on the `_id` field during the creation of a collection.

```ts
await collection.createIndex({
  theaterId: 1,
}, {
  unique: true,
});
```

## Aggregation

Aggregation operations process multiple documents and return computed results through aggregation pipelines.

An aggregation pipeline consists of one or more stages that process documents. Each stage performs an operation on the input documents.

### $match

The `$match` stage could pass the documents that match the specified conditions from a collection to the next pipeline stage. The `$match` stage could limit the total number of documents in the aggregation pipeline, which will optimize the performance.

```ts
collection.aggregate([
  {
    $match: {
      country: 'Spain',
      city: 'Salamanca',
    },
  },
]);
```

### $project

The `$project` stage could pass the requested fields in the documents to the next stage in the pipeline. The specified fields could be existing fields from the input documents or newly computed fields.

```ts
collection.aggregate([
  {
    $project: {
      country: 1,
      name: 1,
    },
  },
]);
```

### $group

The `$group` stage could group input documents by the specified `_id` expression and for each distinct grouping, outputs a document.

- `$sum`: the sum of numerical values
- `$min`: the lowest expression value
- `$max`: the highest expression value
- `$count`: the number of documents
- `$avg`: the average of numerical values
- `$first`: the value of the first document
- `$last`: the value of the last document
- `$push`: the array of expression values
- `$addToSet`: the array of unique expression values

```ts
collection.aggregrate([
  {
    $group: {
      _id: '$name',
      totalDocs: {
        $sum: 1,
      },
    },
  },
]);
```

### $out

The `$out` stage takes the document returned by the aggregation pipeline and writes them to a specified collection. The `$out` stage must be the last stage in the pipeline.

```ts
collection.aggregrate([
  {
    $group: {
      _id: '$name',
      totalDocs: {
        $sum: 1,
      },
    },
  },
  {
    $out: 'result',
  },
]);
```

### $unwind

The `$unwind` stage deconstructs an array field from the input documents to output a document for each element. Each output document is the input document with the value of the array field replaced by the element.

```ts
collection.aggregate([
  {
    $match: {
      country: 'Spain',
      city: 'Salamanca',
    },
  },
  {
    $unwind: '$students',
  }
]);
```

### $sort

The `$sort` stage sorts all input documents and returns them to the pipeline. `$sort` takes a document that specifies the fields to sort by and the respective sort order.

To achieve a consistent sort, add a field which contains exclusively unique values (e.g. `_id`) to the sort.

```ts
collection.aggregate([
  {
    $match: {
      country: 'Spain',
      city: 'Salamanca',
    },
  },
  {
    $sort: {
      'students.number': -1,
    },
  },
]);
```

### $limit

The `$limit` operator limits the number of documents passed to the next stage in the pipeline.

```ts
collection.aggregate([
  {
    $sort: {
      'students.number': -1,
    },
  },
  {
    $limit: 5,
  },
]);
```
