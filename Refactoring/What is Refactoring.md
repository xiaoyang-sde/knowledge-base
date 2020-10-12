# Refactoring

Refactoring is a systematic process of improving code without creating new functionality that can transform a mess into clean code and simple design.

- Clean Code: Clean code is code that is easy to read, understand and maintain. Clean code makes software development predictable and increases the quality of a resulting product.
- Dirty Code: Dirty code is result of inexperience multiplied by tight deadlines, mismanagement, and nasty shortcuts taken during the development process.
- Code Smells: Code smells are indicators of problems that can be addressed during refactoring. Code smells are easy to spot and fix, but they may be just symptoms of a deeper problem with code.
- Refactoring Techniques: Refactoring techniques describe actual refactoring steps. Most refactoring techniques have their pros and cons. Therefore, each refactoring should be properly motivated and applied with caution.

## Clean code

The main purpose of refactoring is to fight technical debt. It transforms a mess into clean code and simple design.

- Clean code passes all tests.
- Clean code doesn’t contain duplication, maintaining a minimal number of classes and other moving parts.
- Clean code is obvious for other programmers, and also easier and cheaper to maintain.

## Technical debt

You can temporarily speed up without writing tests for new features, but this will gradually slow your progress every day until you eventually pay off the debt by writing tests.

- Business pressure
- Lack of understanding of the consequences of technical debt
- Failing to combat the strict coherence of components
- Lack of tests
- Lack of documentation
- Lack of interaction between team members
- Long-term simultaneous development in several branches
- Delayed refactoring

## When to refactor

- When you’re doing something for the first time, just get it done.
- When you’re doing something similar for the second time, cringe at having to repeat but do the same thing anyway.
- When you’re doing something for the third time, start refactoring.

- When adding a feature
- When fixing a bug
- During a code review

## How to refactor

Refactoring should be done as a series of small changes, each of which makes the existing code slightly better while still leaving the program in working order.

- The code should become cleaner.
- New functionality shouldn’t be created during refactoring.
- All existing tests must pass after refactoring.
