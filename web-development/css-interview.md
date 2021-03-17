# CSS Interview

## Flex 布局

### Flex 容器

将容器设置 `display: flex` 即可启用 Flexbox 布局.

- `flex-direction`: `row || column || row-reverse || column-reverse`, Main Axis 排列方向
- `flex-wrap`: `wrap || nowrap || wrap-reverse`, 项目是否换行显示
- `flex-flow`: `row wrap` 等组合属性
- `justify-content`: `flex-start || flex-end || center || space-between || space-around`, Main Axis 对齐方式
- `align-items`: `flex-start || flex-end || center || stretch || baseline`, Cross Axis 对齐方式
- `align-content`: `flex-start || flex-end || center || stretch`, 多行 Cross Axis 对齐方式

### Flex 项目

- `order`: 设置该项目的排列顺序
- `flex-grow`: 设置该项目如何占据剩余空间
- `flex-shrink`: 设置该项目在没有额外空间时如何缩小
- `flex-basis`: 设置该项目的初始大小
- `align-self`: 设置该项目在 Cross Axis 的对齐方式

## em 与 rem

- `em` 相对于父级元素
- `rem` 相对于根元素 `<html>`

## 突破 Chrome 最小字体 12px 限制

使用 `transform` 属性调整文字大小.

```css
.mini-font{
  font-size: 12px;
  -webkit-transform-origin-x: 0;
  -webkit-transform: scale(0.90);
}
```

## 元素居中

### 水平居中

- 行内元素: `text-align: center`
- 确定宽度的块级元素: `margin: 0 auto`
- 不确定宽度的块级元素: 子元素设置 `display: inline-block`, 同时父元素设置 `text-align: center`
- Flex 布局: `justify-content: center`

### 垂直居中

- 单行文本: 将元素的 `line-height` 设置为元素高度
- 多行文本: 父元素设置 `display: table`, 子元素设置 `display: table-cell`, `vertical-align: middle`
- 块级元素 (Flex 布局): `align-items: center`
- 块级元素 (绝对定位和负边距法)

```css
.child {
  position: absolute;
  top: 50%;
  left: 50%;
  height: 50%;
  width: 50%;
  margin: -25% 0 0 -25%;
}
```

- 块级元素 (绝对定位和负边距法)

尝试将子元素拉伸到容器边界, 但由于设置了高度与宽度, 无法拉伸, 并使用 `margin: auto` 将元素固定到容器中央.

```css
#child {
  position: absolute;
  top: 0;
  bottom: 0;
  right: 0;
  left: 0;
  margin: auto;
}
```

- 块级元素 (绝对定位与 transform)

```css
#child {
  position: absolute;
  top: 50%;
  transform: translate(0, -50%);
}
```
