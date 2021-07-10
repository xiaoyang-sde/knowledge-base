# Composition API

## Basics

### Reactive Variables with `ref`

`ref` takes the argument and returns it wrapped within an object with a `value` property, which can then be used to access or mutate the value of the reactive variable.

```ts
import { ref } from 'vue';

const counter = ref(0);
counter.value++;
```

### Reacting to Changes with `watch`

The `watch` function accepts 3 arguments:

1. Reactive Reference or getter function to be watched
2. Callback function
3. Optional configuration options

```ts
import { ref, watch } from 'vue';

const counter = ref(0);
watch(counter, (newValue, oldValue) => {
  console.log('The new counter value is: ' + counter.value)
});
```

### Standalone `computed` properties

The `computed` function accepts a getter-like callback and returns a read-only reactive reference.

```ts
import { ref, computed } from 'vue';

const counter = ref(0);
const twiceTheCounter = computed(() => counter.value * 2);
```

## Setup

The `setup` function takes two arguments: `props` and `context`.

- `props` are reactive and will be updated when new props are passed in.

```ts
import { toRefs } from 'vue';

setup(props, { attrs, slots, emit }) {
  const { title } = toRefs(props);
  console.log(title.value);
  ...
};
```

- `context` is a normal JavaScript object that exposes three component properties.
  - `attrs`: attributes (Non-reactive object)
  - `slots`: slots (Non-reactive object)
  - `emit`: emit events (Method)

### Accessing Component Properties

When `setup` is executed, the component instance has not been created yet. Therefore, these component options are not accessable: `data`, `computed`, `methods`.

If `setup` returns an object, the properties on the object can be accessed in the component's template, as well as the properties of the `props` passed into `setup`.

### Usage of `this`

Inside `setup`, `this` won't be a reference to the current active instance. Since `setup` is called before other component options are resolved, `this` inside `setup` will behave quite differently from `this` in other options.

## Lifecycle Hooks

The component's lifecycle hook could be accessed by prefixing  the lifecycle hook with 'on'. However, `beforeCreate` and `created` hooks should be directly written in the `setup` function.

```ts
import { ref, onMounted } from 'vue';

setup (props) {
  const repositories = ref([]);
  const getUserRepositories = async () => {
    repositories.value = await fetchUserRepositories(props.user);
  };
  onMounted(getUserRepositories);
};
```

## Template Refs

The concept of reactive refs and template refs are unified. To obtain a reference to an in-template element or component instance, a ref could be declared and returned from `setup`.

```vue
<template>
  <div ref="root">This is a root element</div>
</template>

<script>
import { ref, onMounted } from 'vue';

export default {
  setup() {
    const root = ref(null);

    onMounted(() => {
      // the DOM element will be assigned to the ref after initial render
      console.log(root.value); // <div>This is a root element</div>
    });

    return {
      root,
    }
  }
};
</script>
```

If a VNode's `ref` key corresponds to a ref on the render context, the VNode's corresponding element or component instance will be assigned to the value of that ref.
