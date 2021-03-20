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

## 监听器 Observer

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

## 订阅器 Dep

当我们监听到数据的读写操作后, 我们将通知依赖于该数据的视图进行数据更新. 在 Vue 中, 每个数据都是发布者, 而每个数据对应的依赖对象是订阅者.

### 设计模式: 发布订阅

发布订阅模式定义对象间的一对多的依赖关系, 当一个对象的状态改变时, 所有依赖于它的对象都将收到通知.

### 代码实现

我们需要创建一个订阅器 Dep 来收集所有依赖 (订阅者), 并且当数据变化时执行对应订阅者的更新函数. `watcher` 静态属性是全局唯一的 订阅者.

```js
class Dep {
  static watcher = null;
  static initialized = false;
  subs = [];

  addSub(sub) {
    this.subs.push(sub);
  }

  notify() {
    this.subs.forEach((sub) => sub.update());
  }
}
```

修改 `defineReactive` 函数, 将其接入订阅器.

```js
const defineReactive = (obj, key, val) => {
  const dep = new Dep();
  Object.defineProperty(obj, key, {
    get() {
      if (!Dep.initialized) {
        dep.addSub(Dep.watcher);
        Dep.initialized = true;
      }
      return val;
    },

    set(newVal) {
      if (newVal === val) {
        return;
      }
      val = newVal;
      dep.notify();
    }
  });
}
```

## 订阅者 Watcher

订阅者 Watcher 在初始化的时候需要将自己添加进订阅器 Dep 中. 通过遍历 `data` 对象并获取属性值, 我们触发所有 `get` 函数来执行添加订阅者的操作.

在订阅者初始化时, 我们将对应订阅器上的 `initialized` 属性设置为 `false`, 并访问属性强行执行 `get` 函数, 将订阅者添加到订阅器中, 随后将 `initialized` 属性设置为 `true`, 避免后续调用 `get` 函数时重新触发订阅操作.

### 代码实现

- `vm`: Vue 实例对象
- `exp`: Text interpolation (`{{ }}`) 或 `v-bind` 等指令中的属性值.
- `cb`: 订阅者绑定的更新函数.

```js
class Watcher {
  constructor(vm, exp, cb) {
    this.vm = vm;
    this.exp = exp;
    this.cb = cb;

    Dep.initialized = false;
    Dep.watcher = this;
    this.value = this.vm.data[this.exp];
  }

  update() {
    const value = this.vm.data[this.exp];
    const oldValue = this.value;
    if (value !== oldValue) {
      this.value = value;
      this.cb.call(this.vm, value, oldValue);
    }
  }
}
```
