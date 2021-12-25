# Database Design

## The Entity-Relationship (E-R) Data Model

The entity-relationship (E-R) data model was developed to facilitate database design by allowing specification of an enterprise schema that represents the overall logical structure of a database.

### Entity Set

The **entity** is a "thing" or "object" in the real world that is distinguishable from all other objects. Each entity has a set of attributes with values. Each entity is represented by a set of **attributes** and **values**.

The **entity set** is a set of entities of the same type that share the same properties, or attributes. The **extension** of the entity set is the actual collection of entities belonging to the entity set.

### Relationship Set

The **relationship** is an association among several entities, which could multiple descriptive attributes.

The **relationship set** is a set of relationships of the same type. The **relationship instance** represents an association between the named entities in the real-world enterprise that is being modeled. The number of entity sets in a relationship set is the degree of the relationship set.

The **role** of each entity set in a relationship set defines its role in the relation. It is represented by a laebl on the edge.

### Complex Attribute

For each attribute, there is a set of permitted values, called the **domain**, or value set, of that attribute. The attribute will take a `null` value if the entity doesn't have a value for it.

- Composite attribute: The value of the attribute could be divied into subparts.
- Multivalued attribute: The attribute could have multiple values.
- Derived attribute: The value of the attribute could be derived from the values of other related attributes or entities.

### Mapping Cardinality

Mapping cardinalities, or cardinality ratios, express the number of entities to which another entity can be associated via a relationship set.

- One-to-one: An entity in A is associated with at most one entity in B, and an entity in B is associated with at most one entity in A. It is represented by two directed line to both entities.
- One-to-many: An entity in A is associated with any number of entities in B, and an entity in B is associated with at most one entity in A. It is represented by a directed line to the entity on the "one" side and an undirected line to the entity on the "many" side.
- Many-to-many: An entity in A is associated with any number of entities in B, and an entity in B is associated with any number of entities in A. It is represented by two undirected line to both entities.

The **total** participation requires that each entity participates in the relationship at least once. It is represented by a double line in the E-R model.

The general cardinality notation limits the number of participation of each entity in the relation between `a` through `b` times. It is represented by a label `a...b` on the edge.

### Primary Key

The primary key for an entity is a set of attributes that suffice to distinguish entites from each other.

The primary key for a relation depends on its mapping cardinality.

- One-to-one: The primary key of either one of the participating entity set
- One-to-many: The primary key of the participating entity set in the "many" side
- Non-binary relationship: The primary key of all participating entity set if no cardinality constraints are present

The **weak entity set** is one whose existence is depndent on another entity set, which is its identifying entity set. The primary key of the identifying entity and discriminator attributes could uniquely identify a weak entity. The weak entity set is represented by a double rectangle with the discriminator being underlined with a dashed line.

The relationship associating the weak entity set with the identifying entity set is the identifying relationship. It is represented by a double diamond.

### Superclass and Subclass

The entity set could include subgroupings of entites that are distinct in some way from other entities in the set. The attributes and relationship participation of the superclass are inherited by the subclass. The ISA relationship is represented by hollow arrows from the subclass to the superclass.

- Specialization: The refinement from an initial entity set into successive levels of entity subgroupings (from superclass to subclass)
- Generalization: The synthesization from multiple entity sets into a higher-level entity set on the basis of common features (from subclass to superclass)

The entity could belong to at most one (overlapping specialization) or multiple (disjoint specialization) subclass.

### E-R Diagram and Relational Schema

#### Strong Entity Set

The strong entity set with simple descriptive attributes could be represented with a schema with distinct attributes. Each tuple in the relation on the schema corresponds to an entity of the entity set. The primary key of the entity set is the primary key of the schema.

- Composite attribute will be converted to separate attribute for each of the component attributes in the schema
- Multivalued attribute will be converted to a new relation schema
- Derived attribute won't be explicitly represented in the schema

#### Weak Entity Set

The weak entity set could be represented with a schema that contains the primary key of its identifiying entity set and the attributes of the weak entity set. The primary key of the identifying entity set and the discriminator of the weak entity set are the primary key of the schema.

#### Relationship Set

The relationship set could be represented with a schema that contains the primary key of its participating entities and its descriptive attributes.

The schema converted from a relationship set linking a weak entity set to the strong entity set is redundant.

#### Generalization

- Create a schema for the superclass. For each subclass, create a schema that includes the attributes of the subclass and the primary key of the superclass.
- For each subclass, create a schema that includes the attributes of the subclass and the attributes of the superclass.

## Normalization

The basic idea of normalization uses functional dependencies to decompose table and remove redundancies in the relational database design.

### Functional Dependency

$u[x]$ denotes the values for the attributes $X$ of the tuple $u$. The functional dependeny $X \rightarrow Y$ means that for any $u_1, u_2 \in R$, if $u_1[X] = u_2[X]$, then $u_1[Y] = u_2[Y]$.

- Trivial FD: $X \rightarrow Y$ is trivial if $Y \subseteq X$.
- Non-trivial FD: $X \rightarrow Y$ is non-trivial if $Y \not\subseteq X$.
- Completely non-trivial FD: $X \rightarrow Y$ is completely non-trivial if $X \cap Y = \emptyset$.

### Logical Implication and Closure

The closure of functional dependency set $F$ is denoted as $F^+$, which contains all functional dependencies that are logically implied by $F$.

The closure of attribute set $X$ is denoted as $X^+$, which contains all attributes that are functionally determined by $X$.

$X$ is a key of $R$ if and only if $X^+ = R$ or $X \rightarrow \text{all attributes of R}$ and $X$ is minimal.

### Decomposition

- General decomposition: The relation $R (A_1, \dots, A_n)$ is decomposed into $R_1 (A_1, \dots, A_i), R_2 (A_j, \dots, A_n)$ that ${A_1, \dots, A_n} = {A_1, \dots, A_i} \cup {A_j, \dots, A_n}$
- Lossless-Join decomposition: The relation $R (A_1, \dots, A_n)$ is decomposed into $R_1$ and $R_2$ and $R = R_1 \Join R_2$. The decomposition is lossless if $R_1 \cap R_2$ is a key of either $R_1$ or $R_2$.

### Boyce-Codd Normal Form (BCNF)

The relation $R$ is in Boyce-Codd Normal Form with regard to the set of functional dependencies $F$ if and only if for every non-trivial functional dependency $X \rightarrow Y \in F^+$, $X$ contains a key of $R$.

If a relation $R$ violates the BCNF condition, it should be decomposed until all decomposed relations are in BCNF. For the functional dependency $X$, compute $X^+$ and decompose $R$ into $R_1 (X^+)$ and $R_2 (X, Z)$ where $Z$ is all attributes in $R$ except $X^+$.
