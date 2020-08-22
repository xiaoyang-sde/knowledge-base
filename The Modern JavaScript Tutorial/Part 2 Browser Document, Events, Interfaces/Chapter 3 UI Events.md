# Chapter 3 UI Events

## 3.2 Moving the mouse: mouseover/out, mouseenter/leave

### Events mouseover/mouseout, relatedTarget

- `mouseover`: a mouse pointer comes over an element.
- `mouseout`: a mouse pointer leaves the element.
- - `event.relatedTarget`: the element from which the mouse came or left for.

The relatedTarget property can be `null`.

### Skipping elements

If the visitor is moving the mouse very fast then some DOM-elements may be skipped.

### Mouseout when leaving for a child

`mouseout` triggers when the pointer moves from an element to its descendant. According to the browser logic, the mouse cursor may be only over a single element at any time – the most nested one and top by z-index. The event also may bubble to the parent.

### Events mouseenter and mouseleave

They trigger when the mouse pointer enters/leaves the element.

- Transitions inside the element, to/from descendants, are not counted.
- Events `mouseenter`/`mouseleave` do not bubble.

## 3.4 Pointer events

### The brief history

- Long ago, there were only mouse events.
- Touch events were introduced, such as `touchstart`, `touchend`, `touchmove`.
- The new standard Pointer Events was introduced. It provides a single set of events for all kinds of pointing devices.

### Pointer event types

- `pointerdown`
- `pointerup`
- `pointermove`
- `pointerover`
- `pointerout`
- `pointerenter`
- `pointerleave`
- `pointercancel`
- `gotpointercapture`
- `lostpointercapture`

We can replace `mouse<event>` events with `pointer<event>` in our code and expect things to continue working fine with mouse.

### Pointer event properties

- `pointerId` – the unique identifier of the pointer causing the event. (Allows us to handle multiple pointers)
- `pointerType` – the pointing device type. (`mouse`, `pen`, or `touch`)
- `isPrimary` – `true` for the primary pointer (the first finger)

- `width`/`height` - the width/height of the area where the pointer touches the device. (`1` for mouse)
- `pressure`: the pressure of the pointer tip, in range from 0 to 1 (default: 0.5 for pressed and 0)
- `tangentialPressure`: the normalized tangential pressure
- `tiltX`, `tiltY`, `twist` – pen-specific properties that describe how the pen is positioned relative the surface.

### Multi-touch

The `pointerId` is assigned not to the whole device, but for each touching finger.

The events associated with the first finger always have `isPrimary=true`.

### Event: pointercancel

The `pointercancel` event fires when there’s an ongoing pointer interaction, and then something happens that causes it to be aborted.

- The pointer device hardware was disabled.
- The device orientation changed (tablet rotated).
- The browser decided to handle the interaction on its own, considering it a mouse gesture or zoom-and-pan action or something else.

Prevent default browser actions to avoid `pointercancel`. Set `touch-action: none` in CSS.

### Pointer capturing

We can “bind” all events with a particular 1 to a given element.

- `elem.setPointerCapture(pointerId)` – binds the given `pointerId` to `elem`.
- `elem.releasePointerCapture(pointerId)` – unbinds the given `pointerId` from `elem`.

Pointer capturing is used to simplify drag’n’drop kind of interactions.

## 3.5 Keyboard: keydown and keyup

The `keydown` events happens when a key is pressed down, and then `keyup` – when it’s released.

### event.code and event.key

- `key`: the character (different between languages)
- `code`: physical key code (`Key<letter>`, `Digit<number>`, `Enter`, etc.)

### Default actions

- A character appears on the screen (the most obvious outcome).
- A character is deleted (`Delete` key).
- The page is scrolled (`PageDown` key).
- The browser opens the “Save Page” dialog (`Ctrl+S`)

In the past, there was a `keypress` event, and also `keyCode`, `charCode`, which properties of the event object. They are deprecated and no longer need to be used.

## 3.6 Scrolling

- Show/hide additional controls or information depending on where in the document the user is.
- Load more data when the user scrolls down till the end of the page.

### Prevent scrolling

We can’t prevent scrolling by using event.`preventDefault()` in `onscroll` listener, because it triggers after the scroll has already happened.

But we can prevent scrolling by event.`preventDefault()` on an event that causes the scroll.

There are many ways to initiate a scroll, so it’s more reliable to use CSS, `overflow` property.