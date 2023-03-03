# Intelligent Agents

## Agents and Environments

The **agent** is defined as an object that could perceive its environment through sensors and act upon the environment through actuators. The agent's perceptual inputs at a given instance is referred to **percept**. The agent's behavior is described by the **agent function** that maps the perception sequence to an action, and the function is implemented by the **agent program**.

For each possible percept sequence, the **rational agent** should select an action that is expected to maximize its performance measure, given the evidence provided by the percept sequence and the built-in knowledge the agent has. The rational agent should gather information and learn from what it perceives.

The task environment could be specified with the performance, environment, actuators, and sensors. The task environments could be categorized with several number of dimensions: fully or partially observable, single or multi agents, deterministic or stochastic, episodic or sequential, static or dynamic, discrete or continuous, know or unknown.

## The Structure of Agents

The agent program takes the **current percept** as input from the sensors, persist the percept into the percept sequence, and returns an action to the actuators. The table-driven approach (`dict[percept_sequence, action]`) to agent construction is failed because the table size is not feasible. The agent could improve its performance through **learning**, a process of modification of the agent to bring its internal components into closer agreement with the feedback information.

- **Simple reflex agent**: The agent selects actions based on the current percept, ignoring the rest of the percept history. The agent will work only if the environment is fully observable, or the correct decision could be made based on the current percept.
- **Model-based reflex agent**: The agent selects actions based on its internal state that depends on the percept history to reflect the unobserved aspects of the current state. The agent relies on a model that defines how the world evolves and how the agent's action affect the world to update its internal state.
- **Goal-based agent**: The agent selects action based on the goal that describes the desirable situation.
- **Utility-based agent**: The agent selects action based on the utility function (internalization of the performance measure). The utility function specifies the appropriate tradeoff when there are conflicting goals.
