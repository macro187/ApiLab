ApiLab
======

Evaluate .NET API / SOA schemes



Goal
====

Find the best scheme for running .NET services on separate machines
communicating over the network.



Scheme To-Do List
=================

Publish the provided *NormalBakery* as a service.

Publish the provided *NormalButcher* as a service.

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


Service Implementations
-----------------------

Able to re-use existing *NormalBurritoShop* implementation?

No "template" boilerplate?

Lines of code?


Deployment
----------

.Net Framework?

.Net Core?

