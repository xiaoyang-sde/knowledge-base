# Chapter 7: Object properties configuration

## 7.1 Property flags and descriptors

### Property flags

- `writable` – if `true`, the value can be changed, otherwise it’s read-only.
- `enumerable` – if `true`, then listed in loops, otherwise not listed.
- `configurable` – if `true`, the property can be deleted and these attributes can be modified, otherwise not.

### Get the flags

```js
let descriptor = Object.getOwnPropertyDescriptor(obj, propertyName);
```

```js
let user = {
  name: "John"
};

let descriptor = Object.getOwnPropertyDescriptor(user, 'name');

/* property descriptor:
{
  "value": "John",
  "writable": true,
  "enumerable": true,
  "configurable": true
}
*/
```

### Change the flags

```js
Object.defineProperty(obj, propertyName, descriptor);
```

If the property exists, `defineProperty` updates its flags. Otherwise, it creates the property with the given value and flags. If a flag is not supplied, it is assumed false.

Making a property non-configurable is a one-way road. We cannot change it back with `defineProperty`.

### Multiple properties

Define many properties at once:

```js
Object.defineProperties(obj, {
  prop1: descriptor1,
  prop2: descriptor2
  // ...
});
```

Get all property descriptors at once:

```js
Object.getOwnPropertyDescriptors(obj));
```

If we want a better clone, `Object.defineProperties` is preferred.

## 7.2 Property getters and setters

### Getters and setters

```js
let obj = {
  get propName() {
    // getter, the code executed on getting obj.propName
  },

  set propName(value) {
    // setter, the code executed on setting obj.propName = value
  }
};
```

For example:

```js
let user = {
  name: "John",
  surname: "Smith",

  get fullName() {
    return `${this.name} ${this.surname}`;
  }

  set fullName(value) {
    [this.name, this.surname] = value.split(" ");
  }
};

user.fullName = "Alice Cooper";
alert(user.surname); // Cooper
```

### Accessor descriptors

For accessor properties, there is no `value` or `writable`, but instead there are `get` and `set` functions.

To create an accessor `fullName` with `defineProperty`:

```js
Object.defineProperty(user, 'fullName', {
  get() {
    return `${this.name} ${this.surname}`;
  },

  set(value) {
    [this.name, this.surname] = value.split(" ");
  }
});
```

### Smarter getters/setters

Getters/setters can be used as wrappers over "real" property values to gain more control over operations with them.

```js
set name(value) {
    if (value.length < 4) {
      alert("Name is too short, need at least 4 characters");
      return;
    }
    this._name = value;
}
```
