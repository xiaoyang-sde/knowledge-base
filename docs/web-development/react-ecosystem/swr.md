# SWR

## stale-while-revalidate

The `Cache-Control` response header that contains `stale-while-revalidate` should also contain `max-age`, and the number of seconds specified via `max-age` is what determines staleness.

Any cached response newer than `max-age` is considered fresh, and older cached responses are stale.

- If the locally cached response is still fresh, then it can be used as-is to fulfill a browser's request.
- If the cached response is stale and still in the window of time covered by the `stale-while-revalidate` setting, the stale response is used to fulfill the request, and a revalidation request is sent and the cache is then updated.
- If the cached response is stale but falls outside the window of time, then the browser will directly retrieve a response from the network and update the cache.

```text
Cache-Control: max-age=1, stale-while-revalidate=59
```

## useSWR

```tsx
import useSWR from 'swr';

const fetcher = (...args) => fetch(...args).then(res => res.json());

const useUser = (id) => {
  const { data, error } = useSWR(`/api/user/${id}`, fetcher);

  return {
    user: data,
    isLoading: !error && !data,
    isError: error
  };
};

const Profile = () => {
  const { data, error } = useUser(id);

  if (error) return <div>failed to load</div>;
  if (!data) return <div>loading...</div>;

  return <div>hello {data.name}!</div>;
};
```

## Prefetching Data

For top level requests, `rel="preload"` is highly recommended. It will prefetch the data when the HTML loads, even before JavaScript starts to download.

```html
<head>
  <link rel="preload" href="/api/data" as="fetch" crossorigin="anonymous">
</head>
```

## Performance

### Deduplication

```tsx
const App = () => (
  <Profile />
  <Profile />
  <Profile />
);
```

Each `<Profile>` component has a `useSWR` hook inside. Since they have the same SWR key and are rendered at the almost same time, only 1 network request will be made.

### Deep Comparison

SWR deep compares data changes by default. If the data value isn’t changed, a re-render will not be triggered. The comparison could be  customized by the `compare` option.

### Dependency Collection

`useSWR` returns 3 stateful values: `data`, `error` and `isValidating`, each one can be updated independently. SWR only updates the states that are used by the component.

### Tree Shaking

The SWR package is tree-shakeable and side-effect free.
