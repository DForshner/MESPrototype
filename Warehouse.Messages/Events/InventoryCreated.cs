using Core.CQRS;
using System;

namespace Warehouse.Messages.Events
{
    public class InventoryCreated : Event
    {
        public InventoryCreated()
        {
        }
    }
}
