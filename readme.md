ApiLab
======

Evaluate .NET API / SOA schemes



Goal
====

Find the best scheme for running .NET services on separate machines
communicating over the network.



Scheme To-Do List
=================

Create an *ApiLab.Scheme.\<name\>* subdirectory and solution.

Import provided *ApiLab* projects into solution.

Publish the provided *NormalBakery* as a service.

Publish the provided *NormalButcher* as a service, supporting v1.0, v1.1, and
v2.0 clients at the same time.  May or may not use the provided
*ApiLab.Meats.Compat* backwards-compatibility library.

Implement an *IBurritoShop* service and publish it.  May or may not use the
provided *NormalBurritoShop* implementation, but must behave the same.  Must get
ingredients from the *NormalBakery* and *NormalButcher* services above.

Implement a web client that gets burritos from the *IBurritoShop* service.



Evaluation Criteria
===================


Network
-------

Services communicate over the network?

Network communication encrypted?

Network wire format is human-readable?

Compatible with standard authentication and authorisation mechanisms?


Service Implementations
-----------------------

Can re-use existing *NormalBurritoShop* implementation?

No "template" boilerplate?

Lines of code?


Deployment
----------

.Net Framework?

.Net Core?

