# Composing Methods

Much of refactoring is devoted to correctly composing methods. The refactoring techniques in this group streamline methods, remove code duplication, and pave the way for future improvements.

## Extract Method

Create a new method and name it in a way that makes its purpose self-evident.

```ts
printOwing(): void {
  printBanner();

  // Print details.
  console.log("name: " + name);
  console.log("amount: " + getOutstanding());
}
```

```ts
printOwing(): void {
  printBanner();
  printDetails(getOutstanding());
}

printDetails(outstanding: number): void {
  console.log("name: " + name);
  console.log("amount: " + outstanding);
}
```

## Inline Method

When a method body is more obvious than the method itself, use this technique.

Make sure that the method isn’t redefined in subclasses. If the method is redefined, refrain from this technique.

```ts
class PizzaDelivery {
  // ...
  getRating(): number {
    return moreThanFiveLateDeliveries() ? 2 : 1;
  }
  moreThanFiveLateDeliveries(): boolean {
    return numberOfLateDeliveries > 5;
  }
}
```

```ts
class PizzaDelivery {
  // ...
  getRating(): number {
    return numberOfLateDeliveries > 5 ? 2 : 1;
  }
}
```

## Extract Variable

You have an expression that’s hard to understand.

- Condition of the if() operator or a part of the ?: operator in C-based languages
- A long arithmetic expression without intermediate results
- Long multipart lines

```ts
renderBanner(): void {
  if ((platform.toUpperCase().indexOf("MAC") > -1) &&
       (browser.toUpperCase().indexOf("IE") > -1) &&
        wasInitialized() && resize > 0 )
  {
    // do something
  }
}
```

```ts
renderBanner(): void {
  const isMacOs = platform.toUpperCase().indexOf("MAC") > -1;
  const isIE = browser.toUpperCase().indexOf("IE") > -1;
  const wasResized = resize > 0;

  if (isMacOs && isIE && wasInitialized() && wasResized) {
    // do something
  }
}
```

## Inline Temp

You have a temporary variable that’s assigned the result of a simple expression and nothing more.

```ts
hasDiscount(order: Order): boolean {
  let basePrice: number = order.basePrice();
  return basePrice > 1000;
}
```

```ts
hasDiscount(order: Order): boolean {
  return order.basePrice() > 1000;
}
```

## Replace Temp with Query

You place the result of an expression in a local variable for later use in your code.

```ts
calculateTotal(): number {
  let basePrice = quantity * itemPrice;
  if (basePrice > 1000) {
    return basePrice * 0.95;
  }
  else {
    return basePrice * 0.98;
  }
}
```

```ts
calculateTotal(): number {
  if (basePrice() > 1000) {
    return basePrice() * 0.95;
  }
  else {
    return basePrice() * 0.98;
  }
}

basePrice(): number {
  return quantity * itemPrice;
}
```

## Substitute Algorithm

So you want to replace an existing algorithm with a new one? Gradual refactoring isn’t the only method for improving a program. Sometimes a method is so cluttered with issues that it’s easier to tear down the method and start fresh.

```ts
foundPerson(people: string[]): string{
  for (let person of people) {
    if (person.equals("Don")){
      return "Don";
    }
    if (person.equals("John")){
      return "John";
    }
    if (person.equals("Kent")){
      return "Kent";
    }
  }
  return "";
}
```

```ts
foundPerson(people: string[]): string{
  let candidates = ["Don", "John", "Kent"];
  for (let person of people) {
    if (candidates.includes(person)) {
      return person;
    }
  }
  return "";
}
```

## Split Temporary Variable

You have a local variable that’s used to store various intermediate values inside a method (except for cycle variables).

Each component of the program code should be responsible for one and one thing only, which makes it much easier to maintain the code.

```ts
let temp = 2 * (height + width);
console.log(temp);
temp = height * width;
console.log(temp);
```

```ts
const perimeter = 2 * (height + width);
console.log(perimeter);
const area = height * width;
console.log(area);
```

## Remove Assignments to Parameters

- If a parameter is passed via reference, then after the parameter value is changed inside the method, this value is passed to the argument that requested calling this method.

- Multiple assignments of different values to a single parameter make it difficult for you to know what data should be contained in the parameter at any particular point in time.

```ts
discount(inputVal: number, quantity: number): number {
  if (inputVal > 50) {
    inputVal -= 2;
  }
  // ...
}
```

```ts
discount(inputVal: number, quantity: number): number {
  let result = inputVal;
  if (inputVal > 50) {
    result -= 2;
  }
  // ...
}
```

## Replace Method with Method Object

You have a long method in which the local variables are so intertwined that you can’t apply Extract Method. The first step is to isolate the entire method into a separate class and turn its local variables into fields of the class.

Isolating a long method in its own class allows stopping a method from ballooning in size. This also allows splitting it into submethods within the class, without polluting the original class with utility methods.

```ts
class Order {
  // ...
  price(): number {
    let primaryBasePrice;
    let secondaryBasePrice;
    let tertiaryBasePrice;
    // Perform long computation.
  }
}
```

```ts
class Order {
  // ...
  price(): number {
    return new PriceCalculator(this).compute();
  }
}

class PriceCalculator {
  private _primaryBasePrice: number;
  private _secondaryBasePrice: number;
  private _tertiaryBasePrice: number;
  
  constructor(order: Order) {
    // Copy relevant information from the
    // order object.
  }
  
  compute(): number {
    // Perform long computation.
  }
}
```

