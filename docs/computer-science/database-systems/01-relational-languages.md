# Relational Languages

## Relational Algebra

- **Select**: The select operation returns tuples that satisfy a given predicate.

$$\sigma_{\text{dept\_name }= \text{"Physics"} \wedge \text{salary} > 6000}$$

- **Project**: The project operation returns its argument relation, with certain attributes left out.

$$\Pi_{\text{ID, name, salary}}(\text{instructor})$$

- **Cartesian-product**: The cartesian-product operation combines information from any two relations. For relations $r_{1}(R_{1})$ and $r_{2}(R_{2})$, $r_{1} \times r_{2}$ is a relation $r(R)$ whose schema $R$ is the concatenation of the schemas $R_{1}$ and $R_{2}$.

$$r = \text{instructor} \times \text{teaches}$$

- **Join**: The join operation combines a selection and a cartesian-product operation. For relations $r(R)$ and $s(S)$, and let $\theta$ to be a predicate on attributes in the schema $R \cup S$, then $r \Join_{\theta} s = \sigma_{\theta} (r \times s)$.

$$\text{instructor} \Join_{\text{instructor.ID} = \text{teaches.ID}} \text{teaches}$$

- **Union**: The union operation returns all tuples that appear in either or both of the two relations. The input relations must have the same number of attributes and associated types.

$$\Pi_{\text{course\_id}} (\sigma_{\text{semester} = \text{"Fall"}}) \cup \Pi_{\text{course\_id}} (\sigma_{\text{semester} = \text{"Spring"}})$$

- **Intersection**: The intersection operation returns all tuples that appear in both of the two relations.

$$\Pi_{\text{course\_id}} (\sigma_{\text{semester} = \text{"Fall"}}) \cap \Pi_{\text{course\_id}} (\sigma_{\text{semester} = \text{"Spring"}})$$

- **Set-difference**: The set-difference operation returns tuples that are in one relation but not in another. $r - s$ returns a relation containing tuples in $r$ but not in $s$.

$$\Pi_{\text{course\_id}} (\sigma_{\text{semester} = \text{"Fall"}}) - \Pi_{\text{course\_id}} (\sigma_{\text{semester} = \text{"Spring"}})$$

- **Assignment**: The assignment operation assigns a relation to a temporary variable.

$$\text{course\_fall} \leftarrow \Pi_{\text{course\_id}} (\sigma_{\text{semester} = \text{"Fall"}})$$

- **Rename**: The rename operator assigns a name to a relation. $\rho_{x}(E)$ returns the result of expression $E$ under the name $x$. This operation could be used to give unique names to the different occurrences of the same relation.

$$\rho_{\text{i}}(instructor)$$

## SQL Query Langauge

### Data Definition

The SQL standard supports a variety of built-in types. Each type includes the `null` value.

#### String Data Types

- `CHAR(n)`: The fixed-length character string with user-specified length `n`
- `VARCHAR(n)`: The variable-length character string with user-specifed maximum length `n`
- `SET(val1, val2, ...)`: The string object that could have 0 or more values chosen fom a list of possible values.

#### Numeric Data Types

- `INT`: The integer
- `SMALLINT`: The subset of the integer type
- `FLOAT(n)`: The floating-point number with precision of at least `n` digits
- `DOUBLE`: Floating-point and double-precision floating-point numbers

#### Date and Time Data Types

- `DATE`: The date with format `YYYY-MM-DD`
- `TIMESTAMP`: The timestamp or the number of seconds since the Unix epoch time.
- `YEAR`: The year in four-digit format

### Schema Definition

The `create table` command defines an SQL relation. The definition specifies the name of attributes, the type and optional constraints of attributes, and optional integrity constraints.

```sql
CREATE TABLE IF NOT EXISTS department (
  dept_name VARCHAR(20) PRIMARY KEY,
  building_name VARCHAR(15) NOT NULL,
  budget NUMERIC(12, 2),
  PRIMARY KEY (dept_name),
  FOREIGN KEY (building_name) REFERENCES building
);
```

- `PRIMARY KEY`: The attributes `param1, param2` form the primary key of the relation. The primary-key attributes are required to be non-null and unique.
- `FOREIGN KEY(param1, param2, ...) references s`: The foreign key specification defines that the values of `param1, param2` must correspond to values of the primary key
atrributes in relation `s`.
- `NOT NULL`: The constraint specifies that the null value is not allowed for the specific attribute.
- `UNIQUE`: The values in this column have to be unique.
- `AUTOINCREMENT`: The integer value is automatically filled in and incremented with each row insertion.

The `ALTER TABLE` statement adds attriburtes to an existing relation. The new attributes is specified with `DEFAULT` or assigned `NULL` for all tuples in the relation.

```sql
ALTER TABLE relation ADD attribute domain DEFAULT default_value;

ALTER TABLE relation DROP attribute;
```

The `DELETE FROM` statement deletes all tuples in `r` but preserves the relation.

```sql
DELETE FROM relation;
```

The `DROP TABLE` statement deletes a relation from an SQL database.

```sql
DROP TABLE IF EXISTS relation;
```

### Insert, Update, and Delete Rows

The `INSERT` statement declares which table to write into, the columns of data that are filling, and one or more rows of data to insert.

```sql
INSERT INTO table_name (column_1, column_2, ...)
VALUES
  (value_or_expr, another_value_or_expr, …),
  (value_or_expr_2, another_value_or_expr_2, …),
  ...;
```

The `INSERT` statement takes multiple column-value pairs and applies the changes to each row that satisfies the constraint in the `WHERE` clause.

```sql
UPDATE table_name
SET
  column = value_or_expr,
  other_column = another_value_or_expr,
  ...
WHERE condition;
```

The `DELETE` statement describes the table to act on, and the rows of the table to delete through the `WHERE` clause.

```sql
DELETE FROM table_name WHERE condition;
```

### Basic Query Structure

The `SELECT` statment retrieves data from a database. The data returned is stored in a result table. The `DISTINCT` is used to discard duplicate value.

```sql
SELECT column1, column2, ... FROM table_name;

SELECT DISTINCT column1, column2, ... FROM table_name;

SELECT * FROM table_name;
```

The `WHERE` clause specifies the constraints to filter the result. The constraints could be constrcuted with `AND` or `OR` logical keywords.

```sql
SELECT column1, column2, ...
FROM table_name
WHERE condition
  AND/OR another_condition
  AND/OR ...;
```

- `IS NULL`: The value is `NULL`
- `IS NOT NULL`: The value isn't `NULL`
- `BETWEEN ... AND ...`: Number is within range of two values (inclusive)
- `NOT BETWEEN ... AND ...`: Number isn't within range of two values (inclusive)
- `IN (...)`: Number or string exists in a list
- `NOT IN (...)`: Number or string doesn't in a list
- `LIKE`: Case insensitive exact string comparison
- `NOT LIKE`: Case insensitive exact string inequality comparison
- `%`: Match a sequence of zero or more characters
- `-`: Match a single character

The `ORDER BY` clause sorts the result alphanumerically by a given column in ascending or descending order.

The `LIMIT` clause limits the number of rows to return. The `OFFSET` clause specifies where to begin counting the rows.

```sql
SELECT column1, column2, ...
FROM table_name
WHERE condition(s)
ORDER BY column ASC/DESC
LIMIT num_limit OFFSET num_offset;
```

The `AS` keyword gives a descriptive alias to the result data.

```sql
SELECT column AS better_column_name, ...
FROM a_long_widgets_table_name AS widgets
INNER JOIN widget_sales ON widgets.id = widget_sales.widget_id;
```

### Set Operations

The `UNION` operation combines the result of two queries and eliminates duplicates. The `UNION ALL` operation retains all duplicates.

The `INTERSECT` operation selects the intersected result of two queries and eliminates duplicates. The `INTERSECT ALL` operation retains all duplicates.

The `EXCEPT` operation selects the items in the first query but not in the second query, and eliminates duplicates. The `INTERSECT ALL` operation retains all duplicates.

```sql
(
  SELECT column_1 FROM table_name_1
) UNION/INTERSECT/EXCEPT (
  SELECT column_2 FROM table_name_2
);
```

### Multi-table Query with Join

The `JOIN` clause combines row data across two separate tables using the unique key.

- The `INNER JOIN` clause matches rows from th first table and the second table which have the same key to create a result row with the combined columns.
- The `LEFT JOIN` clause includes rows from the first table regardless of whether a matching row is found in the second table.
- The `RIGHT JOIN` clause includes rows from the second table regardless of whether a matching row is found in the first table.
- The `FULL JOIN` clause inncludes all rows from both table regardless of whether a matching row exists in the other table.

```sql
SELECT column1, column2, ...
FROM table_1
INNER/LEFT/RIGHT/FULL JOIN another_table ON table_1.id = table_2.id;
```

### Queries with Aggregates

The aggregate expressions summarizes information about a group of rows of data.

```sql
SELECT AGG_FUNC(column_or_expression) AS aggregate_description, ...
FROM table_name
WHERE constraint_expression
GROUP BY column
HAVING group_condition;
```

- `COUNT(column)`: The count of number of non-NULL value in the specified column
- `MIN(column)`: The smallest numerical value in the specified column
- `MAX(column)`: The largest numerical value in the specified column
- `AVG(column)`: The average numerical value in the specified column
- `SUM(column)`: The sum of all numerical values in the specified column

The `HAVING` clause filters the grouped rows in the result set. The constraints are written the same way as the `WHERE` clause constraints.

### Nested Subqueries

The result of the the subquery could appear at any place in a query that expects a relation.

The `IN` clause returns true if the item exists in the result of the subquery.

```sql
SELECT DISTINCT course_id FROM section
WHERE course_id IN (
  'CS 143', 'CS 161', 'CS 181',
);

SELECT DISTINCT course_id FROM section
WHERE course_id IN (
  SELECT course_id FROM section
  WHERE semester = 'Spring 2018')
);
```

The `SOME` clause returns `true` if the condition satisfies for at least one item in the result of the subquery.

The `ALL` clause returns `true` if the condition satisfies for all items in the result of the subquery..

```sql
SELECT DISTINCT salary FROM employee
WHERE salary > SOME/ALL (
  SELECT salary FROM employee
  WHERE employee_id = '1')
);
```

The `EXISTS` clause returns `true` if the result of the subquery is not empty.

The `UNIQUE` clause returns `true` if the result of the subquery does not contain duplicate tuples.

### Case Condition

The `CASE` statement goes through conditions and returns a value when the first condition is met.

```sql
CASE
  WHEN condition_1 THEN result_1
  WHEN condition_2 THEN result_2
  ...
  ELSE default_result
END;
```

### Modification of the Database

The `DELETE` clause removes selected tuples from a table.

```sql
DELETE FROM table_name
WHERE condition;
```

The `INSERTION` clause inserts tuples into a table. The attribute values for inserted tuples must be members of the corresponding attribute's domain.

```sql
INSERT INTO table_name (
  column_1,
  column_2,
  ...
) VALUES (
  value_1,
  value_2,
  ...
);
```

The `UPDATE` clause changes a value in a table.

```sql
UPDATE table_name
SET column_1 = value_1
WHERE condition;
```
