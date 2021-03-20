# Virtual DOM

Article Source: [深入剖析: Vue 核心之虚拟 DOM](https://juejin.cn/post/6844903895467032589)

## 真实 DOM 解析流程 \(WebKit 渲染引擎\)

1. 构建 DOM 树: 用 HTML 分析器解析 HTML 元素, 构建 DOM 树.
2. 生成 CSSOM 样式表: 用 CSS 分析器解析 CSS 文件与元素上的 `inline` 样式, 生成页面的样式表.
3. 构建 Render 树: 关联 DOM 树与样式表 \(Attachment\), 构建 Render 树. 调用每个节点的 `attach`方法, 返回 Render 对象, 用于构建此树.
4. 确定节点坐标 \(Reflow\): 根据 Render 树结构, 确定每个节点的坐标与大小. \(生成 Box Model\)
5. 绘制页面: 根据 Render 树与坐标, 调用节点的 `paint` 方法, 绘制节点.

### 解析顺序

* DOM 树的构建与 HTML 加载同时进行.
* Render 树, DOM 树, 样式表构建同时进行.
* CSS 从右向左逆向解析, 嵌套标签越多, 解析越慢.
* 原生 JavaScript 操作 DOM 时, 浏览器会重新构建 DOM 树. 例如需要更新 10 个节点, 浏览器收到首个请求时并不知道还有 9 次更新操作, 所以会重复执行 10 次. 操作 DOM \(Repaint, Reflow\) 非常消耗计算资源.

## Virtual DOM 算法实现

Virtual DOM 使用 JavaScript 对象模拟 DOM. 页面更新首先将反映在此对象上, 等待更新完成后, 再将最终的对象映射成真实 DOM, 交给浏览器进行绘制.

例如需要更新 10 个节点, 虚拟 DOM 将 10 次更新的 `diff` 储存在 JavaScript 对象中, 随后再将该对象一次性 `attach` 到 DOM 树上, 避免大量无意义的计算.

### JavaScript 对象模拟 DOM 树

```javascript
function Element(tag, props, children) {
  this.tag = tag;
  this.props = props;
  this.children = children || [];

  // Use key as the unique identifier of the element
  if (props.key) {
    this.key = props.key;
  }

  // Calculate the count of children
  this.count = 0;
  children.forEach((child, index) => {
    if (child instance of Element) {
      this.count += child.count;
    }
    this.count += 1;
  });
}

function createElement(tag, props, children) {
  return new Element(tag, props, children);
}

export default createElement;
```

```javascript
import Element from 'VirtualDOM';

const ul = Element('div', { id: 'virtual-dom' }, [
  Element('p', {}, ['Virtual DOM']),
  Element('ul', { id: 'list' }, [
    Element('li', { class: 'item' }, ['Item 1']),
    Element('li', { class: 'item' }, ['Item 2']),
    Element('li', { class: 'item' }, ['Item 3'])
  ]),
  Element('div',{},['Hello World'])
]);
```

#### 渲染 DOM 对象

根据 `tag` 属性构造真实 DOM 节点并设置该节点属性, 最终通过递归构建子节点:

```javascript
Element.prototype.render = function () {
  const el = document.createElement(this.tag);
  const props = this.props;
  for (const [name, value] of Object.entires(props)) {
    el.setAttribute(name, value);
  }

  const children = this.children;
  children.forEach((child) => {
    childEl = (child instanceof Element) ?
              child.render() :
              document.createTextNode(child);
    el.appendChild(childEl);
  });

  return el;
};
```

将构建好的节点添加到 DOM 树中:

```javascript
const root = ul.render();
document.body.appendChild(root);
```

### diff 算法 - 比较两棵 Virtual DOM 树的差异

当完全比较两颗树时, `diff` 算法的时间复杂度为 `O(n^3)`. 在前端开发过程中, 很少会发生跨层级的元素移动, 所以 Virtual DOM 只会比较同一层级的元素, 将复杂度降为 `O(n)`.

#### diff 类型

* Replace: 替换节点
* Reorder: 修改子节点的顺序
* Props: 修改节点属性 \(例如添加 `class`\)
* Text: 修改 Text 节点的内容

#### Depth-first Search

使用 Depth-first Search 将所有节点与新的树中对应节点进行对比, 将差异记录到指定对象中.

```javascript
const dfs = (oldNode, newNode, index, patches) => {
  const patch = [];

  if (
    typeof oldNode === 'string' &&
    typeof newNode === 'string' &&
    oldNode !== newNode
  ) {
    // The text in the node has been changed
    patch.push({
      type: patch.TEXT,
      content: newNode
    });
  } else if (
    oldNode.tag === newNode?.tag &&
    oldNode.key === newNode?.key
  ) {
    // The nodes are the same, but the props have been changed
    const propsPatches = diffProps(oldNode, newNode);
    patch.push({
      type: patch.PROPS,
      props: propsPatches
    });

    // Compare the children of the current node
    diffChildren(
      oldNode.children,
      newNode.children,
      index,
      patches,
      currentPatch
    );
  } else if (newNode !== null) {
    // The nodes are different, so the oldNode should be replaced
    patch.push({
      type: patch.REPLACE,
      props: newNode
    });
  }

  patches[index] = patch;
};

const diff = (oldTree, newTree) => {
  const index = 0;
  const patches = {};
  dfs(oldTree, newTree, index, patches);
  return patches;
};
```

#### 列表对比算法

当子节点重新排序时, 如果按照同层级进行顺序对比, 它们都会被替换 \(`REPLACE`\) 掉. Levenshtein Distance 算法可以解决此问题. 通过 Dynamic Programming 求解, 时间复杂度为 `O(M*N)`.

代码实现: [list-diff](https://github.com/livoras/list-diff)

### patch - 更新实际 DOM 树

由于 Virtual DOM 树的与实际 DOM 树结构相同, 我们可以使用 Depth-first Search 遍历 DOM 树, 并根据 Patch 的内容, 修改实际 DOM 节点.

#### 更新指定节点

```javascript
const applyPatches = (node, patches) => {
  patches.forEach(patch => {
    switch (patch.type) {
      case REPLACE:
        var newNode = (typeof patch.node === 'string')
          ? document.createTextNode(patch.node)
          : patch.node.render();
        node.parentNode.replaceChild(newNode, node);
        break;
      case REORDER:
        reorderChildren(node, patch.moves);
        break;
      case PROPS:
        setProps(node, patch.props);
        break;
      case TEXT:
        node.textContent = patch.content;
        break;
    }
  })
};
```

#### Depth-first Search 遍历 DOM 树

```javascript
const patch = (node, patches) => {
  dfsPatch(node, patches, { index: 0 });
};

const dfsPatch = (node, patches, walker) => {
  const currentPatches = patches[walker.index];
  for (const child of node.childNodes) {
    walker.index += 1;
    dfsPatch(child, patches, walker);
  }
  applyPatches(node, patches);
};
```

## Vue Virtual DOM 解析

### VNode 模拟 DOM 树

Vue 借鉴 [snabbdom](https://github.com/snabbdom/snabbdom), 使用 `VNode` 模拟 DOM 树的节点.

```javascript
export default class VNode {
  tag: string | void;
  data: VNodeData | void;
  children: ?Array<VNode>;
  text: string | void;
  elm: Node | void;
  context: Component | void;
  key: string | number | void;
  ...
}
```

* `tag`: HTML 标签 \(`a`, `p`, etc.\)
* `data`: `class`, `style`, `attribute`, etc.
* `children`: 子节点
* `text`: 文本属性
* `elm`: 对应的真实 DOM 节点
* `key`: 提高 diff 的效率

### 创建 VNode

#### 初始化 Vue

```javascript
function Vue (options) {
  this._init(options)
}
```

#### 挂载实例

```javascript
Vue.prototype.$mount = function (
  el?: string | Element,
  hydrating?: boolean
): Component {
  el = el && inBrowser ? query(el) : undefined
  return mountComponent(this, el, hydrating)
}
```

`mountComponent` 实例化一个渲染 `Watcher`, 并传入一个 `updateComponent` 回调函数. 此回调函数调用 `vm._render` 方法生成 VNode 并使用 `vm._update` 更新 DOM.

```javascript
export function mountComponent (
  vm: Component,
  el: ?Element,
  hydrating?: boolean
): Component {
  vm.$el = el
  ...
  let updateComponent = () => {
    const vnode = vm._render()
    vm._update(vnode, hydrating)
  }

  new Watcher(vm, updateComponent, noop, {
    before () {
      if (vm._isMounted && !vm._isDestroyed) {
        callHook(vm, 'beforeUpdate')
      }
    }
  }, true /* isRenderWatcher */)
  hydrating = false

  return vm
}
```

#### 渲染 VNode

`_render` 方法将实例渲染成 VNode.

```javascript
Vue.prototype._render = function (): VNode {
  const vm: Component = this
  const { render, _parentVnode } = vm.$options
  let vnode
  try {
    ...

    currentRenderingInstance = vm
    // Call createElement method to generate the VNode
    vnode = render.call(vm._renderProxy, vm.$createElement)
  } catch (e) {
    handleError(e, vm, `render`){}
  }

  vnode.parent = _parentVnode
  return vnode
}
```

`_createElement` 方法创建 VNode.

* `context`: Context of VNode \(Component\)
* `tag`: VNode 标签 \(String 或 Component\)
* `data`: VNode 数据
* `children`: VNode 子节点

```javascript
export function _createElement (
  context: Component,
  tag?: string | Class<Component> | Function | Object,
  data?: VNodeData,
  children?: any,
  normalizationType?: number
): VNode | Array<VNode> {
  ...
  let vnode, ns

  if (typeof tag === 'string') {
    let Ctor
    ns = (context.$vnode && context.$vnode.ns) || config.getTagNamespace(tag)
    if (config.isReservedTag(tag)) {
      vnode = new VNode(
        config.parsePlatformTagName(tag), data, children,
        undefined, undefined, context
      )
    } else if ((!data || !data.pre) && isDef(Ctor = resolveAsset(context.$options, 'components', tag))) {
      vnode = createComponent(Ctor, data, context, children, tag)
    } else {
      vnode = new VNode(
        tag, data, children,
        undefined, undefined, context
      )
    }
  } else {
    vnode = createComponent(tag, data, context, children)
  }

  ...
  return vnode
}
```

### VNode diff 算法

Vue 实例化 `watcher` 并将其添加到模板中所绑定的变量的依赖中. 当 `model` 中响应式数据发生变化, `dep` 数组将调用 `dep.notify()` 方法遍历所有依赖并更新视图 \(调用 `updateComponent` 方法\)

`vm._update` 方法将更新视图. `vnode` 参数是刚创建的 VNode.

```javascript
Vue.prototype._update = function (vnode: VNode, hydrating?: boolean) {
  const vm: Component = this
  const prevEl = vm.$el
  const prevVnode = vm._vnode
  const restoreActiveInstance = setActiveInstance(vm)
  vm._vnode = vnode

  // Compare prevVnode and vnode
  vm.$el = vm.__patch__(prevVnode, vnode)

  ...
}
```

`vm.__patch__` 方法将 `prevVnode` 与 `vnode` 进行 diff 操作, 并根据需要记录 Patch, 然后生成新的 DOM 节点来完成视图更新.

```javascript
function patch (oldVnode, vnode, hydrating, removeOnly) {
  if (isUndef(oldVnode)) {
    // Create new node if oldVnode doesn't exist
    isInitialPatch = true
    createElm(vnode, insertedVnodeQueue)
  } else {
    // Compare oldVnode and vnode
    const isRealElement = isDef(oldVnode.nodeType)
    if (!isRealElement && sameVnode(oldVnode, vnode)) {
      // Patch existing root node
      patchVnode(oldVnode, vnode, insertedVnodeQueue, null, null, removeOnly)
    }
  }
    ...
}
```

当 `oldVnode` 不存在时, 创建新的节点. 如果存在则将 `oldVnode` 与 `vnode` 进行 diff 与 patch. patch 过程中调用 `sameVnode` 方法比较两个 VNode 的属性, 判断是否局部更新. 如果两个 VNode 属性相同, 则发生了、局部更新, 将两个 VNode 进行 diff. 如果两个 VNode 属性不同则跳过 diff 过程, 并创建新的真实 DOM 节点来替换旧节点.

```javascript
function sameVnode (a, b) {
  return (
    a.key === b.key &&
    a.tag === b.tag &&
    a.isComment === b.isComment &&
    isDef(a.data) === isDef(b.data) &&
    sameInputType(a, b)
  )
}
```

`patchVnode` 方法对两个 VNode 进行 diff.

* 对文本节点更新时, 如果文本不同则直接替换
* 当 VNode 没有文本节点时, 开始 diff 子节点
* 如果 `oldCh` 与 `ch` 都存在且不相同, 调用 `updateChildren` 对子节点进行 diff
* 如果 `oldCh` 不存在, 清空 `oldVnode` 的文本节点, 并使用 `addVnodes` 方法将 `ch` 添加到 `elm` \(真实 DOM 节点\)
* 如果 `oldCh` 存在, `ch` 不存在, 删除 `elm` 的 `oldChild` 子节点
* 如果 `oldVnode` 有文本节点, `vnode` 没有, 则清空这个文本节点

```javascript
function patchVnode (oldVnode, vnode, insertedVnodeQueue, ownerArray, index, removeOnly) {
  const elm = vnode.elm = oldVnode.elm
  const oldCh = oldVnode.children
  const ch = vnode.children

  if (isUndef(vnode.text)) {
    if (isDef(oldCh) && isDef(ch)) {
      if (oldCh !== ch) updateChildren(elm, oldCh, ch, insertedVnodeQueue, removeOnly)
    } else if (isDef(ch)) {
      if (process.env.NODE_ENV !== 'production') {
        checkDuplicateKeys(ch)
      }
      if (isDef(oldVnode.text)) nodeOps.setTextContent(elm, '')
      addVnodes(elm, null, ch, 0, ch.length - 1, insertedVnodeQueue)
    } else if (isDef(oldCh)) {
      removeVnodes(elm, oldCh, 0, oldCh.length - 1)
    } else if (isDef(oldVnode.text)) {
      nodeOps.setTextContent(elm, '')
    }
  } else if (oldVnode.text !== vnode.text) {
    nodeOps.setTextContent(elm, vnode.text)
  }
}
```

### 子节点 diff 算法

`updateChildren` 方法是 diff 的最重要环节. 在开始遍历前, 首先给 `oldCh` 与 `newCh` 分配一个 `startIndex` 与 `endIndex` 作为遍历的索引. 当 `oldCh` 或 `newCh` 遍历完成后 (`startIndex >= endIndex`), 停止 diff 过程.

```js
function updateChildren (parentElm, oldCh, newCh, insertedVnodeQueue, removeOnly) {
  let oldStartIdx = 0
  let newStartIdx = 0
  let oldEndIdx = oldCh.length - 1
  let oldStartVnode = oldCh[0]
  let oldEndVnode = oldCh[oldEndIdx]
  let newEndIdx = newCh.length - 1
  let newStartVnode = newCh[0]
  let newEndVnode = newCh[newEndIdx]
  let oldKeyToIdx, idxInOld, vnodeToMove, refElm

  while (oldStartIdx <= oldEndIdx && newStartIdx <= newEndIdx) {
    ...
  }
}
```

### patch (nodeOps)

在 `diff` 算法中, Vue 使用 `nodeOps` 封装的方法操作真实 DOM 结构.

```js
export function createElementNS (namespace: string, tagName: string): Element {
  return document.createElementNS(namespaceMap[namespace], tagName)
}

export function createTextNode (text: string): Text {
  return document.createTextNode(text)
}

export function createComment (text: string): Comment {
  return document.createComment(text)
}

export function insertBefore (parentNode: Node, newNode: Node, referenceNode: Node) {
  parentNode.insertBefore(newNode, referenceNode)
}

export function removeChild (node: Node, child: Node) {
  node.removeChild(child)
}
```
