# An Overview of JavaScript Testing in 2020

Article Source: [https://medium.com/welldone-software/an-overview-of-javascript-testing-7ce7298b9870](https://medium.com/welldone-software/an-overview-of-javascript-testing-7ce7298b9870)

## Test Types

- **Unit Tests**: Testing of individual units like functions or classes by supplying input and making sure the output is as expected.
- **Integration Tests**: Testing processes across several units to achieve their goals, including their side effects.
- **End-to-end Tests**: Testing how scenarios function on the product itself, by controlling the browser or the website.

## Running Tests

- Tests can run in the browser by creating an HTML page with the test libraries.
- Tests can run in a headless browser.
- Tests can also be executed in Node.js by simply importing the test files and dependent libraries.

## Test Tools Types

- Test launchers: Launch a list of tests based on a configuration you provide. (Enviorment, plugin, output format, etc.)
- Testing structure: The organization of your tests. (BDD structure)
- Assertion functions: Make sure that tested variables contain the expected value.
- Spies: Attach themselves to functions to provide us extra information about them.
- Stubbing or Dubbing: Replaces selected methods of existing modules with user-supplied functions in order to ensure expected behavior during the test.
- Mocks or Fakes: Used to fake certain modules or behaviors to test different parts of a processes.
- Snapshot Testing: Allows you to compare a data structure to what it was in older releases.
- Browsers Controllers: Web drivers, inject scripts, or use native browser API to access DOM, network, cookies, etc.

To start testing you need to choose a testing structure that suits you, choose the style of assertion functions you like, and decide how do you want to run the tests. Some frameworks like Jest, Jasmine, TestCafe, and Cypress provide all of these out of the box.

It's recommended to create two different processes. One for running unit and integration tests and another one for Functional Tests. Unit and integration tests can run on the fly, as you code, by using a "watch mode".

## List of General Prominent Testing Tools

- jsdom: JavaScript implementation of the WHATWG DOM and HTML standards.
- Storybook: Write components in special “stories” which enable developing and interacting with components in isolation.
- Istanbul: Tell you how much of your code is covered with unit tests.
- Karma: Hosts a test server with a special web page to run your tests in the page’s environment.
- Chai: The most popular assertion library.

- Jest: The testing framework created and maintained by Facebook.
- Mocha: It is used with third party assertions, mocking, and spying tools (usually Sinon and Chai).
