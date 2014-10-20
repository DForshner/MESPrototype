MESPrototype
============

This is a toy MES/MOM that explores using DDD, CQRS, and event sourcing.

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