# Stable Matching

## The Problem

Consider a set M of n men, and a set W of n women. We denote the set of all possible ordered pairs of the form (m, w). Each man ranks all the women in order of his preference and each women ranks all the men in order of her preference.

- Matching: Each member of M and each member of W apprears in at most one pair in S.
- Perfect matching: Each member of M and each member of W apprears in **exactly** one pair in S. (Everyone ends up matched to somebody.)
- Stable matching: A perfect matching with no instability. No man prefers a women over the one he is matched, where that other woman also prefers that man over the one she is matched. Each set of preference lists has at least one stable matching.

## The Algorithm

```
Initially all m and w are free
While there's a man m who is free and hasn't proposed to every women
  Choose such a man m
  Let w be the highest-ranked woman in m's preference list to whom m has not yet proposed
  If w is free
    (m, w) become engaged
  Else if w is engaged to m' and w prefers m to m'
    (m, w) become engaged
    m' becomes free
```

## Analyzing the Algorithm

1. w remains engaged from the point at which she receives her first proposal. The sequence of partners to which she is engaged gets better and better.

2. The sequence of women to whom m propses gets worse and worse.

3. The algorithm terminates after at most $n^{2}$ iterations of the while loop.

4. If m is free at some point in the execution of the algorithm, then there's a woman to whom he has not yet proposed.

5. The set S returned at termination is a perfect matching.

6. Consider an execution of the algorithm that returns a set of pairs S. The set S is a stable matching.

## Extension

All executions of the algorithm yeild the same matching that each man ends up with the best possible partner. If a woman w is a valid partner of a man m if there's a stable matching that contains the pair $(m, w)$. If no woman whom m ranks higher than w is a valid partner of his, w is the best valid partner. However, in the stable matching, each woman is paired with her worst valid partner.
