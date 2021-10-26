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

The SQL standard supports a variety of built-in types. Each type includes the `NULL` value.

The `CAST(e AS t)` expression converts an expression `e` to the type `t`. (e.g. `CAST(ID AS NUMERIC(5))`)

The `COALESCE(e_1, e_2, ...)` expression returns the first non-null value in `e_1, e_2, ...`.

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
- `TIME`: The time of day in hours, minutes, and seconds.
- `TIMESTAMP`: The timestamp or the number of seconds since the Unix epoch time.
- `YEAR`: The year in four-digit format

### Schema Definition

The `CREATE TABLE` command defines an SQL relation. The definition specifies the name of attributes, the type and optional constraints of attributes, and optional integrity constraints.

```sql
CREATE TABLE IF NOT EXISTS department (
  dept_name VARCHAR(20) PRIMARY KEY,
  building_name VARCHAR(15) NOT NULL,
  budget NUMERIC(12, 2) DEFAULT 0,
  PRIMARY KEY (dept_name),
  FOREIGN KEY (building_name) REFERENCES building
);
```

Integrity constraints ensure that changes made to the database by authorized users do not result in a loss of data consistency.

- `PRIMARY KEY`: The attributes `param1, param2` form the primary key of the relation. The primary-key attributes are required to be non-null and unique.
- `FOREIGN KEY(param1, param2, ...) REFERENCES s`: The foreign key specification defines that the values of `param1, param2` must correspond to values of the primary key
atrributes in relation `s`.
- `NOT NULL`: The constraint specifies that the null value is not allowed for the specific attribute.
- `UNIQUE`: The values in this column have to be unique.
- `CHECK(predicate)`: The value in this column must satisfies the predicate. (e.g. `CHECK(budget > 0)`)
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

### Multi-table Queries with Join

The `JOIN` clause combines row data across two separate tables using the unique key.

- The `JOIN` or `INNER JOIN` clause matches rows from th first table and the second table which have the same key to create a result row with the combined columns.
- The `LEFT JOIN` or `LEFT OUTER JOIN` clause includes rows from the first table regardless of whether a matching row is found in the second table.
- The `RIGHT JOIN` or `RIGHT OUTER JOIN` clause includes rows from the second table regardless of whether a matching row is found in the first table.
- The `FULL JOIN` or `FULL OUTER JOIN` clause inncludes all rows from both table regardless of whether a matching row exists in the other table.

```sql
SELECT column1, column2, ...
FROM table_1
INNER/LEFT/RIGHT/FULL JOIN another_table ON table_1.id = table_2.id;
```

The `JOIN` clause supports several join conditions that specifies when to combine two tuples.

- The `NATURAL` condition joins two tuples if all of their columns with the same name have the same value.
- The `USING (column_1, column_2, ...)` condition joins two tuples if `column_1, column_2, ...` have the same value.
- The `ON condtion_1 AND/OR condition_2 ...` condition joins two tuples if they matches `condtion_1 AND/OR condition_2 ...`.

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

The subquery expression could appear at any place in a query that expects a relation.

The `IN` clause returns true if the item exists in the result of the subquery.

```sql
SELECT DISTINCT course_id FROM section
WHERE course_id IN (
  'CS 143', 'CS 161', 'CS 181',
);

SELECT DISTINCT course_id FROM section
WHERE course_id IN (
  SELECT course_id FROM section
  WHERE semester = 'Spring 2018'
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

The subquery expression could also be used in the `FROM` clause.

```sql
SELECT dept_name, avg_salary
FROM (
  SELECT dept_name, AVG(salary)
  FROM instructor
  GROUP BY dept_name
  AS dept_avg(dept_name, avg_salary)
)
WHERE avg_salary > 42000;
```

The `WITH` clause defines a temporary relation that is available to the query in which the clause occurs. The temporary relation could be used multiple times.

```sql
WITH max_budget(value) AS (
  SELECT MAX(budget)
  FROM department
)
SELECT budget
FROM department, max_budget
WHERE  deptartment.budget = max_budget.value;
```

The scalar subquery (subquery expression with result that only contains a single value) could be used in `SELECT`, `WHERE`, and `HAVING` clause as an expression.

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

### Views

The view in SQL is a virtual relation to be defined by a query, and is executed whenever the virtual relation is used.

```sql
CREATE VIEW view_name AS query_expression;

CREATE VIEW view_name(column_1, column_2, ...) AS query_expression;
```

If a view meets the following conditions, it supports tthe `UPDATE`, `INSERT`, and `DELETE` operations.

- The `FROM` clause has only one database relation.
- The `SELECT` clause contains only attribute names of the relation and does not have any expressions, aggregates, or distinct specification.
- Any attribute not listed in the select clause does not have a `NOT NULL` constraint and is not part of a primary key.
- The query does not have a `GROUP BY` or `HAVING` clause.

### Transactions

The transaction consists of a sequence of query or update statements. Each transaction must be terminated by a `COMMIT` or `ROLLBACK` statement. Each transaction is atomic, thus either all or none of the effects of the transaction are reflected in the database.

Each single SQL statement is taken to be a transaction on its own, and it gets committed after it is executed. The transaction could also be defined with the `BEGIN` clause.

```sql
BEGIN
sql_statement_1
sql_statement_2
...
COMMIT/ROLLBACK
```

### Index

The index on an attribute of a relation is a data structure that allows the database system to find those tuples in the relation that have a specified value for that attribute efficiently, without scanning through all the tuples of the relation.

```sql
CREATE INDEX index_name
ON relation_name (
  search_key_1,
  search_key_2,
  ...
);

DROP INDEX index_name;
```

### Integrity Constraints

