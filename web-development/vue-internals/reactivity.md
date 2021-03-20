# Reactivity in Depth (Vue 2)

Vue 内部通过 `Object.defineProperty` 方法进行属性拦截, 将 `data` 对象的数据转化为 `getter` 与  `setter`, 当数据变化时通知视图更新.

## MVVM 数据双向绑定

双向绑定: Model 变化更新 View, View 变化更新 Model. 例如 `input` 内容变化时, `data` 的数据同步变化, 或 `data` 的数据更新时, `p` 中的文字同步变化.

- View 变化更新 Model 可以通过事件监听的方式来实现.
- Model 变化更新 View 是本文讨论的重点内容.

数据双向绑定步骤:

1. 实现监听器 Observer, 劫持并监听所有属性, 当属性发生变化时通知 Watcher.
2. 实现订阅器 Dep, 收集订阅者, 对 Observer 与 Watcher 进行统一管理.
3. 实现订阅者 Watcher, 收到属性变化时执行相应的方法, 更新视图.
4. 实现解析器 Compiler, 解析每个节点的指令, 初始化模版数据与 Watcher.

## 监听器 Observer 实现

监听器 Observer 让 `data` 对象变得可以观测. Vue 2 使用 `Object.defineProperty()` 劫持每个属性的 `getter` 与 `setter`, 让数据被读写时能够通知 Watcher.

### Object.defineProperty() 定义对象

```js
const person = Object.defineProperty({}, 'name', {
  get() {
    console.log('Read');
    return this._name;
  },

  set(newVal) {
    console.log('Write');
    this._name = newVal;
  }
});
```

### 劫持所有属性

当 `data` 的属性较多时, 我们可以遍历 Object, 劫持所有属性.

```js
const observable = (obj) => {
  if (!obj || typeof obj !== 'object') {
    return;
  }
  Object.keys(obj).forEach((key) => defineReactive(obj, key, obj[key]));
  return obj;
}

const defineReactive = (obj, key, val) => {
  Object.defineProperty(obj, key, {
    get() {
      console.log(`Read ${key}`);
      return val;
    },

    set(newVal) {
      console.log(`Write ${key}`);
      val = newVal;
    }
  });
}
```

我们可以使用 Object Literal 创建对象, 并使用 `observable` 方法完成劫持.

```js
const person = observable({
  name: 'Nachenberg',
  age: 15
});
```
