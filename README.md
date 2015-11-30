
## What Is This?

Radish is currently a simple coding exercise to test a [Dependency Injector](https://en.m.wikipedia.org/wiki/Dependency_injection) implementation that uses reflection.

## Why Should I Care?

You shouldn't, unless you're interested in how dependency injection works.

## What's Radish's Future?

There are a few possible long-term goals that are being considered:

* Become a usable implementation for structuremap
* Create a new project for creating unit/integration testing that will consume this project. 
* Continue as an exercise in testing Dependency Injection implementations

## What's In The Foreseeable Future?

* Remove `Radish` from anything that is not a namespace
* Hide helper classes and make API layer more obvious
* Better tests and more test coverage

###### More Functionality

* Supply an `Assembly` whose classes will be added to the registry
* Get a specific instance using an identifier
* Error handling
* Flesh out more scopes
