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
