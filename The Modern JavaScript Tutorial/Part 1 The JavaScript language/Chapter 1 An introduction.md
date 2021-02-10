# Chapter 1: An introduction

## 1.1 An Introduction to JavaScript

### What is JavaScript?

JavaScript was initially created to "make web pages alive". They can be written right in a web page's HTML and run automatically as the page loads. The browser has an embedded engine sometimes called a "JavaScript virtual machine". (V8, SpiderMondy, ChakraCore, etc.)

### How do engines work?

1. The engine (embedded if it's a browser) reads ("parses") the script.
2. Then it converts ("compiles") the script to the machine language.
3. And then the machine code runs, pretty fast. (The engine applies optimizations at each step of the process.)

### What can in-browser JavaScript do?

JavaScript does not provide low-level access to memory or CPU.
JavaScript's capabilities greatly depend on the environment it's running in. (Node.js supports reading and writing arbitrary files, performing network requests, etc.)

In-browser JavaScript can do everything related to webpage manipulation, interaction with the user, and the webserver. (Add or change the HTML, react to user actions, send requests over the network, get and set cookies, save data into the local storage, etc.)

### What Can't in-browser JavaScript do?

JavaScript's abilities in the browser are limited for the sake of the user's safety to prevent evil webpages from harming the user's privacy and data.

- Read or write files on the disk, or execute programs.
- Different tabs/windows generally do not know about each other if they come from different sites. (Same Origin Policy)
- Its ability to receive data from other sites/domains is crippled.

### What makes JavaScript unique?

- Full integration with HTML/CSS.
- Simple things are done simply.
- Support by all major browsers and enabled by default.
- Allows to create servers, mobile applications, etc.

### Languages "over" JavaScript

A plethora of new languages appeared, which are transpiled (converted) to JavaScript before they run in the browser.

- **CoffeeScript**: a "syntactic sugar" for JavaScript
- **TypeScript**: strict data typing
- **Flow**: data typing
- **Dart**: has its own engine that runs in non-browser environments

## 1.2 Manuals and specifications

### Specification

The ECMA-262 specification contains the most in-depth, detailed and formalized information about JavaScript. It defines the language.

To read about new bleeding-edge features: https://github.com/tc39/proposals

### Manuals

- MDN (Mozilla) JavaScript Reference
- MSDN 
Compatibility tables

- http://caniuse.com: per-feature tables of support
- https://kangax.github.io/compat-table: a table with language features and engines that support those or don't support.

## 1.3 Code editors

### IDE

- Visual Studio Code
- WebStorm
Lightweight editors

- Atom
- Sublime Text
- Notepad++
- Vim or Emacs

## 1.4 Developer console

Developer tools allow us to see errors, run commands, examine variables, and much more.

They can be opened with `F12` for most browsers on Windows. Chrome for Mac needs `Cmd+Opt+J`, Safari: `Cmd+Opt+C` (need to enable first).
