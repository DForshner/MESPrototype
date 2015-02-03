MESPrototype
============

A demo MES I'm using to explore using DDD, CQRS, Event Sourcing, and Functional Reactive Programming concepts.

TODO:
- [ ] Everything!  Nothing here is working.

WebPortal Solution
- Web portal to view and control system

Managers Solution:
- Starts multiple processes that handle domain logic.
- Communicate via a service bus.

Simulation Solution:
- Simulates production activity.

Dependencies
- The Core and Messaging projects should provide the common dependencies.
- Nothing should depend on the web and infrastructure projects.
