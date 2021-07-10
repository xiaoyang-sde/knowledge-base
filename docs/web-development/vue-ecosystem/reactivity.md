# Reactivity

## Fundamentals

### Reactive State

The `reactive` function creates a reactive state from a JavaScript object. The reactive conversion affects all nested properties of the passed object.

```ts
import { reactive } from 'vue'

const state = reactive({
  count: 0
});
```

### Standalone Reactive Values

The `ref` function creates a reactive and mutable object that serves as a reactive reference to the internal value. The object contains the only one property named `value`.

```ts
import { ref } from 'vue';

const count = ref(0);
```

#### Ref Unwrapping

When a ref is returned as a property on the render context and accessed in the template, it automatically shallow unwraps the inner value. It only happens inside a reactive `Object`, not in `Arraay` or a native collection type.

#### Access in Reactive Objects

When a `ref` is accessed or mutated as a property of a reactive object, it automatically unwraps to the inner value so it behaves like a normal property.

```ts
const count = ref(0);
const state = reactive({
  count,
});
state.count = 1;
```

If a new `ref` is assigned to a property linked to an existing `ref`, it will replace the old `ref`.

### Destructuring Reactive State

When destructing a reactive object, the reactive object should be convert to a set of refs. These refs will retain the reactive connection to the source object.

```ts
import { reactive, toRefs } from 'vue';

const book = reactive({
  author: 'Vue Team',
  title: 'Vue 3 Guide',
});
const { author, title } = toRefs(book);
```

### Readonly Reactive Objects

The `readonly` function creates a readonly proxy to the original object.

```ts
import { reactive, readonly } from 'vue';

const original = reactive({ count: 0 });
const copy = readonly(original);
```
