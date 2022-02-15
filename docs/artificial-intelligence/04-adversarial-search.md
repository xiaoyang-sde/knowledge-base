# Adversarial Search

## Games

The adversarial search problems, or games, are search problems with competitive environments, in which the agent's goals are in conflict. Each game could be defined with the following elements. The initial state, $\text{ACTIONS}$ function, and $\text{RESULT}$ function define the game tree for the game

- $S_0$: the **initial state**, which specifies how the game is set up at the start
- $\text{PLAYER}(s)$: defines which player has the move in a state
- $\text{ACTIONS}(s)$: defines the set of legal moves in a state
- $\text{RESULT}(s, a)$: the **transition model**, which defines the result of a move
- $\text{TERMINAL-TEST}(s)$: the **terminal test**, which is `true` when the game is over and `false` otherwise
- $\text{UTILITY}(s, p)$: the **utility function**, which defines the final numeric value for a game that ends in terminal state $s$ for a player $p$

## Optimal Decisions in Games

Given a game tree, the optimal strategy could be determined from the **minimax value** of each node, or $\text{MINIMAX}(n)$. The minimax value of a node is the utility for $\text{MAX}$ of being in the corresponding state, assuming that both players play optimally from there to the end of the game.

$$
\text{MINIMAX}(s) =
\begin{cases}
  \text{UTILITY}(s) & \text{if TERMINAL-TEST}(s)\\
  \max_{a \in \text{ACTIONS}(s)} \text{MINIMAX}(\text{RESULT}(s, a)) & \text{\text{if PLAYER}(s) = \text{MAX}}\\
  \min_{a \in \text{ACTIONS}(s)} \text{MINIMAX}(\text{RESULT}(s, a)) & \text{\text{if PLAYER}(s) = \text{MIN}}
\end{cases}
$$

The **minimax** algorithm computes the minimax decision from the current state. The algorithm uses a simple recursive computation of minimax values of each successor state, and then the mimimax values are backed up through the tree. The algorithm is complete if the tree is finite and optimal if the opponent plays optimally.

- Time complexity: $O(b^m)$
- Space complexity: $O(bm)$

## Alpha-Beta Pruning

The **alpha-beta pruning** algorithm computes the correct minimax decision without looking at every node in the game tree. The algorithm updates the values of $\alpha$ and $\beta$ and prunes the remaining branches at a node when the current node is known to be worse than the current $\alpha$ or $\beta$ value.

- $\alpha$: the best value to $\text{MAX}$ found so far of the current path
- $\beta$: the best value to $\text{MIN}$ found so far of the current path

The effectiveness of alpha-beta pruning is dependent on the order in which the states are examined. If the algorithm always picks the best node, the time complexity is $O(b^{m/2})$.

## Imperfect Real-Time Decision

To find the solution under time limits, the algorithm could cutoff the search earlier and apply a heuristic evaluation function to states in the search to turn non-terminal nodes into terminal leaves. The $\text{EVAL}(s)$ heuristic function estimates the position's utility, and $\text{CUTOFF-TEST}(s, d)$ decides when to apply $\text{EVAL}$ based on the state or depth limit.

$$
\text{H-MINIMAX}(s, d) =
\begin{cases}
  \text{EVAL}(s) & \text{if CUTOFF-Test}(s, d)\\
  \max_{a \in \text{Actions}(s)} \text{H-MINIMAX}(\text{RESULT}(s, a), d + 1) & \text{\text{if PLAYER}(s) = \text{MAX}}\\
  \min_{a \in \text{Actions}(s)} \text{H-MINIMAX}(\text{RESULT}(s, a), d + 1) & \text{\text{if PLAYER}(s) = \text{MIN}}
\end{cases}
$$

The evaluation function, or the **weighted linear function**, returns an estimate of the expected utility of the game from a given position. $w_i$ is a weight and $f_i$ is a feature of the position.

$$\text{EVAL}(s) = \omega_1 f_1 (s) + \omega_2 f_2 (s) + \dots + \omega_n f_n (s) = \sum_{i = 1}^{n} \omega_i f_i (s)$$

## Stochastic Games

Stochastic games contains unpredictable external events. The game tree of a stochastic game includes chance nodes in addition to $\text{MAX}$ and $\text{MIN}$ nodes. The **minimax** value of deterministic games could be generalized to an **expecti-minimax** value for games with chance nodes. The expecti-minimax value of a chance node is the sum of the value over all outcomes, weighted by the probability of each chance action $r$.

$$
\text{EXPECTI-MINIMAX}(s) =
\begin{cases}
  \text{UTILITY}(s) & \text{if TERMINAL-TEST}(s)\\
  \max_{a \in \text{ACTIONS}(s)} \text{EXPECTI-MINIMAX}(\text{RESULT}(s, a)) & \text{\text{if PLAYER}(s) = \text{MAX}}\\
  \min_{a \in \text{ACTIONS}(s)} \text{EXPECTI-MINIMAX}(\text{RESULT}(s, a)) & \text{\text{if PLAYER}(s) = \text{MIN}}\\
  \sum_{r} P(r) \text{EXPECTI-MINIMAX}(\text{RESULT}(s, r)) & \text{\text{if PLAYER}(s) = \text{CHANCE}}
\end{cases}
$$
