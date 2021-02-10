# Compression, Complexity, and P=NP?

## Model 1 vs. Model 2 for Compression

A Flaw in Compression Model 1: Source code for decompression algorithm itself might be highly complex.

### Question 1: Comprehensible Compression

Is there a "comprehensible" compression takes as input a target bitstream B, and outputs useful, readable Java code?

### Question 2: Optimal Compression

Is there an optimal compression takes as input a target bitstream B, and outputs the shortest possible Java program that outputs this bitstream?

## Kolmogorov Complexity

The Java-Kolmogorov complexity K_J (B) is the length of the shortest Java program (in bytes) that generates B.

Fact #1: Kolmogorov Complexity is effectively independent of language. For any bit stream, the Java-Kolmogorov Complexity is no more than a constant factor larger than the Python-Kolmogorov Complexity.

Could write a Python interpreter in Java and then runs the Python program.

It means that most bitstreams are fundamentally incompressible no matter what programming language is used for the compression algorithm.

## Space/Time Bounded Compression

An optimal compression algorithm takes as input a target bitstream B, and outputs the shortest possible Java program that outputs this bitstream.

No “optimal compression” algorithm exists.

### Space Bounded Compression

Takes two inputs, a target bitstream B and a size S, and outputs a Java program of `length ≤ S` that outputs B.

Does not exist, since it could be used to find K_J (B) by binary searching on S.

### Space/Time Bounded Compression

Takes three inputs, a target bitstream B, a size S, a maximum number of lines of bytecode executed T, and outputs a Java program of `length ≤ S` that outputs B in fewer than T executed lines of bytecode.

For each possible program p of length S or less:
* If p compiles, run program p until either:
* p terminates.
* We output B.
* T lines of bytecode are executed.
Runtime: `O(T x 2 ^ S)`.

### Efficient Space/Time Bounded Compression

* Need to make a more precise definition of what we mean by “efficient”.
* Closely related to an important puzzle in computer science: Does P = NP?

## P = NP?

An efficient solution to these three problems (3SAT, Independent Set, and Longest Path) would also give an efficient space/time bounded compression algorithm.

Space/time bounded compression reduces to 3SAT, Independent Set, and Longest Path.

### Definition

Two important classes of yes/no problems:
P: Efficiently solvable problems.
NP: Problems with solutions that are efficiently verifiable.

Examples of problems in P:
* Is this array sorted?
* Does this array have duplicates?

Examples of problems in NP:
* Is there a solution to this 3SAT problem?
* In graph G, does there exist a path from s to t of weight > k?

Any decision problem for which a yes answer can be efficiently verified can be transformed into a 3SAT problem. This transformation is also "efficient" (polynomial time).
