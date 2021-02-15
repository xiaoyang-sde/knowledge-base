# Chapter 4: Forms, controls

## 4.1 Form properties and methods

### Navigation: form and elements

Document forms are members of the special collection `document.forms`.

```javascript
document.forms.my - the form with name="my"
document.forms[0] - the first form in the document
```

Any element is available in the named collection `form.elements`. `form.elements[name]` is a collection.

```javascript
let elem = form.elements.age;
alert(elem[0]);
```

### Backreference: element.form

For any element, the form is available as `element.form`.

### Form elements

#### input and textarea

We can access their value as `input.value` \(string\) or `input.checked` \(boolean\) for checkboxes.

#### select and option

* `select.options` – the collection of `<option>` subelements.
* `select.value` – the value of the currently selected `<option>`.
* `select.selectedIndex` – the number of the currently selected `<option>`.

#### new Option

```javascript
option = new Option(text, value, defaultSelected, selected);
```

* `defaultSelected` - set HTML-attribute, that we can get using `option.getAttribute('selected')`.
* `selected` - if true, then the option is selected.

## 4.2 Focusing: focus/blur

There’s an `autofocus` HTML attribute that puts the focus into an element by default when a page loads.

### Events focus/blur

The `focus` event is called on focusing, and `blur` – when the element loses the focus.

### Methods focus/blur

Methods `elem.focus()` and `elem.blur()` set/unset the focus on the element.

`onblur` works after the element lost the focus, so we can't use `preventDefault()`.

### Allow focusing on any element: tabindex

`focus`/`blur` support is guaranteed for elements that a visitor can interact with: `<button>`, `<input>`, `<select>`, `<a>` and so on.

Any element becomes focusable if it has `tabindex`. The switch order is: elements with `tabindex` from 1 and above go first, and then elements without `tabindex`.

* `tabindex="0"` puts an element among those without `tabindex`.
* `tabindex="-1"` - the Tab key ignores such elements, but method elem.focus\(\) works.

### Delegation: focusin/focusout

Events `focus` and `blur` do not bubble. However, they propagate down on the capturing phase.

`focusin` and `focusout` events exactly do the same as `focus`/`blur`, but they bubble. They must be assigned using `elem.addEventListener`.

## 4.3 Events: change, input, cut, copy, paste

### Event: change

The `change` event triggers when the element has finished changing.

* `input`: it triggers after losing focus.
* `select`, `input type=checkbox/radio`: it triggers right after the selection changes.

### Event: input

The `input` event triggers every time after a value is modified by the user.

The `input` event occurs after the value is modified.

### Events: cut, copy, paste

We can use `event.preventDefault()` to abort the action, then nothing gets copied/pasted.

## 4.4 Forms: event and method submit

The `submit` event triggers when the form is submitted. It is usually used to validate the form before sending it to the server or to abort the submission and process it in JavaScript.

### Event: submit

To submit a form:

* Click `<input type="submit">` or `<input type="image">`.
* Press `Enter` on an input field.

When a form is sent using `Enter` on an input field, a `click` event triggers on the `<input type="submit">`.

### Method: submit

To submit a form to the server manually, we can call `form.submit()`. The `submit` event is not generated.

