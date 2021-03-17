# Vue Interview

## Single Page Application

SPA 仅在 Web 页面初始化时加载相应的 HTML,JavaScript, CSS. 使用路由机制改变 HTML 的内容, 避免页面重复加载.

* 用户体验好, 服务器压力小, 前后端分离
* 初次加载较慢, 路由管理复杂, SEO 难度大

## v-show 与 v-if 有什么区别

* `v-show` 总是渲染元素, 调整 CSS 的 `display` 属性.
* `v-if` 是条件渲染.

## Class 与 Style 动态绑定

```markup
<div
  v-bind:class="{ active: isActive }"
>
</div>

<div
  v-bind:class="[isActive ? 'active' : '']"
>
</div>
```

## 单向数据流

父级 `prop` 更新会向下流动到子组件中, 但是反过来则不行. 这样会防止从子组件意外改变父级组件的状态, 从而导致应用的数据流向难以理解.

## computed 与 watch 的区别和运用的场景

* `computed`: 计算属性, 缓存计算结果.
* `watch`: 每当数据变化时执行回调函数.

## Vue 生命周期

* `beforeCreate`: 初始化 `Vue` 实例, `data`, `methods` 数据还未生效
* `created`: 初始化 `data` 与 `methods`, 可以操作这些数据或调用方法
* `beforeMount`: 执行模板中的指令, 最终模板字符串完成编译, 但尚未挂载到页面
* `mounted`: 将内存中编译好的模板挂载到页面上, 此时可以操作组件中的 DOM 节点
* `beforeUpdate`: 组件数据更新之前
* `update`: 组件数据更新之后
* `activated`: `keep-alive` 专属，组件被激活时调用
* `deactivated`: `keep-alive` 专属，组件被销毁时调用
* `beforeDestroy`: 组件销毁前, 尚未清除 `data` 与 `methods` 等属性
* `destroy`: 组件销毁后

### 父组件与子组件生命周期顺序

* 加载渲染过程: 父 `beforeCreate` -&gt; 父 `created` -&gt; 父 `beforeMount` -&gt; 子 `beforeCreate` -&gt; 子 `created` -&gt; 子 `beforeMount` -&gt; 子 `mounted` -&gt; 父 `mounted`
* 子组件更新过程: 父 `beforeUpdate` -&gt; 子 `beforeUpdate` -&gt; 子 `updated` -&gt; 父 `updated`

### 异步请求

异步请求可以在 `created`, `beforeMount`, `mounted` 中进行调用.

### 访问或操作 DOM

在钩子函数 `mounted` 被调用前, Vue 已经将编译好的模板挂载到页面上, 所以在 `mounted` 中可以访问操作 DOM.

### 父组件监听子组件生命周期

```markup
<Child
  @hook:mounted="doSomething"
>
</Child>
```

## v-model 原理

`v-model` 在内部为不同的输入元素使用不同的属性与事件.

* `text`, `textarea` 元素使用 `value` 属性和 `input` 事件
* `checkbox`, `radio`, `select` 使用 `checked` 属性和 `change` 事件

在自定义组件中, `v-model` 默认会使用 `value` 的 `prop` 和名为 `input` 的事件.

## 组件间通信方式

* `props`, `$emit` \(父子\)
* `ref`, `$parent/$children` \(父子\)
* `$attrs`, `$listeners` \(隔代\)
* Vuex \(父子, 隔代, 兄弟\)

## Router 路由模式

* `hash`: 使用 URL Hash 模拟完整 URL
* `history`: 依赖 HTML5 History API 与服务器配置
* `abstract`: 支持所有 JavaScript 运行环境 \(包括 Node.js 服务端\)

### hash 模式原理

* hash 值 \(`location.hash`\) 是 URL 中 `#` 后面的内容
* hash 值只是客户端的一种状态, 当向服务器端发出请求时, hash 部分不会被发送
* 可以使用 `a` 标签或修改 `location.hash` 改变 hash 值
* 可以使用 `hashchange` 事件来监听 hash 值的变化, 从而改变页面内容

### history 模式原理

* `history.pushState()`: 新增浏览器历史记录
* `history.repalceState()`: 替换浏览器历史记录
* 使用 `popstate` 事件来监听 URL 的变化, 从而改变页面内容

## Model–View–ViewModel \(MVVM\)

* View: 视图层 \(HTML, CSS\)
* Model: 数据模型 \(API 接口\)
* ViewModel: 视图数据层 \(ViewModel 封装出来的数据模型包括视图的状态与行为两部分, 而 Model 层的数据模型是只包含状态的\)

Vue 实现了双向绑定, 即 ViewModel 的内容会实时展现在 View 层. View 层展现的不是 Model 层的数据, 而是 ViewModel 的数据, 并由 ViewModel 负责与 Model 层交互.

```markup
<!-- View -->
<div id="app">
    <p>{{message}}</p>
    <button v-on:click="showMessage()">Click me</button>
</div>

<!-- ViewModel -->
<script>
const app = new Vue({
  el: '#app',
  data: {
    message: 'Hello Vue!',
  }
})
</script>

<!-- Model -->
<script>
const response = {
  "response": {
    "success": true
  }
}
</script>
```

## 数据双向绑定原理

双向绑定指 View 变化更新 Data, Data 变化更新 View. View 变化更新 Data 可以通过事件监听的方式来实现.

### Vue 2 实现双向绑定

1. 监听器: 遍历数据对象, 使用 `Object.defineProperty()` 对属性注入 Getter 与 Setter, 用于监听对象赋值.
2. 解析器: 解析 Template, 将 Template 中的变量都替换成数据. 渲染页面视图, 将每个指令对应的节点绑定更新函数, 添加监听数据的 Watcher. 一旦数据有变动, Watcher 收到通知并调用更新函数进行数据更新.
3. 订阅者: 订阅监听器中的属性值变化的消息, 当收到属性值变化的消息时, 触发解析器中对应的更新函数.
4. 订阅器: 订阅器采用 "发布-订阅" 设计模式, 统一管理监听器与订阅者.

### Proxy 与 Object.defineProperty

* `Proxy` 可以直接监听对象而非属性
* `Proxy` 可以直接监听数组的变化
* `Proxy` 不会直接修改原对象
* `Object.defineProperty` 兼容性好, 支持 IE9. `Proxy` 兼容性差, 难以 Polyfill.

### Vue.set

Vue 会在初始化实例时对属性执行 Getter/Setter 转化, 所以属性必须提前定义在 `data` 对象中. Vue 无法检测到添加或删除对象的属性, 也无法检测到使用 Index 对数组的直接赋值.

```javascript
this.$set(this.someObject, 'b', 2); // Object
this.$set(this.items, 1, 'value'); // Array
```

* 如果目标是数组, 直接使用数组的 `splice` 方法触发响应式.
* 如果目标是对象, 会先判读属性是否存在, 是否是响应式, 并注入 Getter/Setter.

## 项目优化

* `v-if` 与 `v-show` 区分使用场景
* `computed` 和 `watch` 区分使用场景
* `v-for` 遍历必须使用 `key`
* 长列表性能优化
* 事件的销毁
* 图片资源懒加载
* 路由懒加载
* 第三方插件的按需引入
* 优化无限列表性能
* 服务端渲染

