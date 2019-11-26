ApiLab
======

Proof-of-concept (micro?) [service oriented architecture](https://en.wikipedia.org/wiki/Service-oriented_architecture)
implementation using JSON-RPC on ASP.NET Core.



Prerequisites
=============

Semantic Versioning
-------------------

Especially the meaning of major, minor, and patch version number components with respect to backwards compatibility.

<https://semver.org/>


Domain Driven Design
--------------------

Especially what models and services are.

<https://en.wikipedia.org/wiki/Domain-driven_design#Building_blocks>



Definitions
===========

POCO Service
------------

A service accessible in-process, implemented as an `IFooService` + `FooService` pair of the sort you might register in
an IOC container as has been standard .NET practice for the past decade or two.


Network Service
---------------

A service accessible over the network.



Problem
=======

Implement a (micro?) service oriented architecture providing the most desired benefits at the lowest cost.



Desired Benefits
================

Versioning
----------

Enable components to evolve at different speeds, with faster-changing components maintaining backwards-compatibility for
slower-changing ones.

<https://github.com/microsoft/aspnet-api-versioning/wiki>



Costs
=====

Additions or Modifications to Service Code
------------------------------------------

One-time modification work required to a POCO service so it can be exposed as a network service.

Ongoing non-POCOness or non-standardness.

Ongoing additional complexity.

Ongoing pollution of domain service with infrastructure concerns.


Network Hosting Infrastructure Boilerplate
------------------------------------------

One-time setup of additional network hosting code.

Ongoing maintenance costs and risks of additional network hosting code.


Additions or Modifications to Client Code
-----------------------------------------

One-time setup of additional client code required to consume a service over the network instead of in-process.

Ongoing maintenance costs and risks of additional client code.



Overall Features
================

Communication over HTTP(S)

Human-readable, JSON wire format



Service Features
================

ASP.NET Core on .NET Core and .NET Framework.

Implementations are POCOs, zero additional code required to host on the network.

Synchronous and `async` operations.



Client Features
===============

.NET Core and .NET Framework.

Javascript.

Clients are dynamic, zero additional code required to consume a service over the
network.

Clients are async, .NET `async` and Javascript `Promise`.

.NET client supports batching (multiple operations per network request).

