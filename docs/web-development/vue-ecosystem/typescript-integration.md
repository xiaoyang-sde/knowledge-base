# TypeScript Integration

## Define Vue Components

`defineComponent` global method couold be used to let TypeScript properly infer types inside Vue component options.

```vue
<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
  // type inference enabled
})
</script>
```

## Composition API

TypeScript will infer types from `props` component option.

```ts
import { defineComponent } from 'vue'

const Component = defineComponent({
  props: {
    message: {
      type: String,
    },
  },

  setup(props) {
    ...
  }
});
```

### Typing `ref`

`ref` infers the type from the initial value. To specify complex types for a ref's inner value, pass a generic argument.

```ts
const year = ref<string | number>('2020');
```

To create a template ref, use `InstanceType` to get type information of a specific component.

```ts
const modal = ref<InstanceType<typeof MyComponent>>();
```

### Typing `reactive`

```ts
interface Book {
  title: string;
  year?: number;
}

const book = reactive<Book>({ title: 'Vue 3 Guide' });
```

### Typing `computed`

Computed values will infer the type from returned value:

```ts
const doubleCount = computed(() => count.value * 2);
```

### Typing Event Handlers

When dealing with native DOM events, it might be useful to type the argument we pass to the handler correctly. The solution is to cast the `event.target` with a correct type.

```ts
const handleChange = (evt: Event) => {
  console.log((evt.target as HTMLInputElement).value)
};
```
