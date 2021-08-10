# Queries and Mutations

GraphQL is a query language for API and a server-side runtime for executing queries using a user-defined type system. A GraphQL service is created by defining types and fields on those types, then providing functions for each field on each type.

## Fields

The GraphQL query is about asking for specific fields on objects. The query has exactly the same shape as the result. If a field refers to an object, it's possible to make sub-selection of fields for that object.

Each field and nested object accept its own set of arguments

```graphql
query HeroNameAndFriends {
  human(id: "1000") {
    name
    height(unit: FOOT)
  }
}
```

Aliases could be used to rename the result of a field, since the result object fields match the name of the field in the query without arguments.

```graphql
query HeroNames {
  empireHero: hero(episode: EMPIRE) {
    name
  }
  jediHero: hero(episode: JEDI) {
    name
  }
}
```

## Fragments

Fragments is a set of fields that is a reusable unit in GraphQL.

```graphql
query HeroComparison {
  leftComparison: hero(episode: EMPIRE) {
    ...comparisonFields
  }
  rightComparison: hero(episode: JEDI) {
    ...comparisonFields
  }
}

fragment comparisonFields on Character {
  name
  appearsIn
  friends {
    name
  }
}
```

## Variables

GraphQL use variables to support dynamic values inside queries. The variable definition lists all of the variables, prefixed by $, followed by their type. All declared variables must be either scalars, enums, or input object types. Variable definitions can be optional or required.

```graphql
query HeroNameAndFriends($episode: Episode = JEDI) {
  hero(episode: $episode) {
    name
    friends {
      name
    }
  }
}
```

## Directives

```graphql
query Hero($episode: Episode, $withFriends: Boolean!) {
  hero(episode: $episode) {
    name
    friends @include(if: $withFriends) {
      name
    }
  }
}
```

- `@include(if: Boolean)`: Only include this field in the result if the argument is `true`.
- `@skip(if: Boolean)`: Skip this field if the argument is `true`.

## Mutations

Any operations that cause writes should be sent explicitly via a mutation. A mutation can contain multiple fields. However, while query fields are executed in parallel, mutation fields run in series, one after the other.

```graphql
mutation CreateReviewForEpisode($ep: Episode!, $review: ReviewInput!) {
  createReview(episode: $ep, review: $review) {
    stars
    commentary
  }
}
```
