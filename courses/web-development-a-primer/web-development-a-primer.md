# Web Development: A Primer

This course is for anyone who knows the basics of programming and wants to get into web development in some capacity but is not sure where to start.

## Networks

### Network layers

* Application Layer: process-to-process communication across the - Internet \(HTTP\)
* Transport Layer: connect applications on the Internet \(TCP\)
* Network Layer: connect any two machines on the Internet
* Data Link Layer: transmit data from any given machine to a device
* Physical Layer: physical wiring and circuits

## Databases

### Databases in Web Development

* Static websites are ones that only display information such as text or pictures on their web pages that users cannot interact with.
* Dynamic websites allow for user interaction, and that is where Databases come into the picture.

DBMS, short for Database Management System, is essentially a sort of nuance added to web development that allows data to be separated from the logic of the application and stored separately to be retrieved and processed when required.

* SQL: use predefined schemas to determine the structure of your data before you work with it; all of your data must follow the same structure \(MySQL, PostgreSQL, MariaDB\)
* NoSQL: use a dynamic schema for unstructured data, and data is stored in many ways, including key-value pairs, documents and, even graphs; grow dynamically and the structure of data can vary from database to database \(Redis, Apache CouchDB, MongoDB\)

**SQL Management Systems**

* MySQL: use basic SQL queries to create tables and handle data
* PostgreSQL: the default database for macOS and provides efficient data management across platforms; define their own data types and even custom plugins
* MariaDB: include a wide selection of storage engines so it can easily work with data from other relational data management systems

**Caching**: selecting commonly requested data components and storing them in immediate memory to avoid extraneous database calls; NoSQL databases have gained popularity in recent times because of their ability to automatically cache data to enhance performance.

NoSQL databases use data structures to store entries, and SQL databases use tabular form to store entries.

**NoSQL Management Systems**

* MongoDB: use a document-based storage system that stores key-value pairs; distributable across locations; highly flexible
* Apache CouchDB: intuitive document storage model with a powerful query engine to allow users to store their data safely on personal servers or with any leading cloud provider
* Redis: an open-source in-memory data structure store that is often used as a database

## Web Caching

Caches are used at each level starting from the server itself all the way to users’ browsers. Web caching works by caching HTTP responses for certain requests according to a probabilistic analysis of requests that are frequently observed on the server.

* Low network traffic
* Improve responsiveness of web applications
* Sustain higher loads of data since a significant chunk of the data can be stored within caches
* Data that has been stored in caches can still be served to end-users when the server is down

Memcached is an open-source, high-performance, distributed memory object caching system that is intended to speed up dynamic web applications by alleviating database load.

## The Front End

The source code of a website can be broken down into two parts: client-side and server-side.

Client-side languages: HTML, CSS, and JavaScript. Server-side languages: JavaScript, Python, PHP, Ruby, and C\#.

Server-side programming includes:

* Delivering and storing information efficiently
* Providing a custom experience for each user
* Controlling access to content
* Storing session/state information
* Notifying and communicating
* Analyzing data

### CSS Preprocessors

A CSS preprocessor is a scripting language that extends CSS and then compiles it into regular CSS. SASS and Less \(Syntactically Awesome Style Sheets and Leaner Style Sheets\) are language extensions for CSS that allow you to use more conventional programming constructs such as variables, conditionals, and loops to generate CSS.

### JavaScript DOM

HTML structure can be mapped onto a tree. The DOM stores these HTML elements as objects that can then be manipulated in various ways through JavaScript.

## The Back End

A web server is a system that delivers content and services to users over the Internet.

**Microservice**: Virtual machines can allow us to have several ‘machines’ on one server, each with different operating systems.

* Highly maintainable and testable
* Loosely coupled
* Independently deployable
* Organized around business capabilities.

### NodeJS

Node is a runtime environment that can run JavaScript outside of browsers.

As an asynchronous event-driven JavaScript runtime, Node is designed to build scalable network applications.

A sample NodeJS server:

```javascript
const http = require('http');
const hostname = '0.0.0.0';
const port = 3000;
const server = http.createServer((req, res) => {
  res.statusCode = 200;
  res.setHeader('Content-Type', 'text/plain');
  res.end('Hello World\n');
});
server.listen(port, hostname, () => {
  console.log(`Server running at http://${hostname}:${port}/`);
});
```

## Web Development Frameworks

### MVC architecture

Model: store all information about the content and structure of an application View: the application’s front-end, or what is more commonly known as the user interface Controller: receive user input from the View, processes it, and mediates required changes between the two.

### Front-end \(client-side\) frameworks

* Angular JS: highly expressive, readable, and quick to develop
* Bootstrap: ease up the process of adding CSS to HTML
* React.js: build the View layer of an MVC application
* Backbone: structure your JavaScript code in an MVC \(Model, View, Controller\) form
* Semantic-UI: use natural language, and the code is, therefore, largely self-explanatory, thus making it highly desirable for beginners, particularly those with little or no coding experience

### Back-end \(server-side\) frameworks

* Express \(JavaScript\): de facto standard server framework for Node.js
* Symfony \(PHP\): a set of reusable components
* Django \(Python\): a high-level Python web framework that enables the rapid development of secure and maintainable websites
* Ruby on Rails \(Ruby\): provide default structures for everything that goes into each component, including databases, several common web services as well as web pages themselves
* ASP .NET \(C\#\): simplify the process of creating websites based on HTML5, CSS, and JavaScript

### Isomorphic \(client-server\) frameworks

Avoid the cumbersome process of developing the front end and the back end separately and then integrating them. Developers could write their entire application in a single framework without having to worry about integrating multiple components written in different frameworks.

* Meteor JS: an open-source client-server JavaScript framework that has been written in Node.js
* Lazo.js
* Rendr: run Backbone.js applications on both the client and the server

### Software Stacks

A combination of tools that are used together to create software.

* LAMP: Linux, Apache, MySQL, PHP
* MEAN Stack: MongoDB, Express JS, Angular JS, Node JS
* MERN Stack: MongoDB, Express JS, React JS, Node JS

## Principles of Software Engineer

### Software processes

A set of related activities that leads to the production of a software product, such as a website.

* A **plan-driven process** is one where all of the process activities are planned in advance, and progress is measured against this plan.
* An **agile process** is one where planning is incremental, and it is easier to change the process in accordance with changing customer requirements.

### Waterfall Model

Represent the fundamental activities of specification, development, validation, and evolution as separate, sequential process phases.

* Analyse and define requirements
* Design software
* Implement and test units
* Integrate units and test system
* Operation and maintenance

### Incremental Development

Develop an initial version of your website, open it to feedback, and then incrementally develop newer versions based on this feedback.

### Reuse-oriented Software Engineering

Rely on reusable software components and an integrating framework for the composition of the components.

### Test driven development

An approach to program development in which you interleave both testing and code development.

### Automated testing

* Selenium: automate browsers
* Jest: an integrated, “zero-configuration” JavaScript testing tool
* PyUnit: Python unit testing framework
* JUnit: the Java equivalent of PyUnit

