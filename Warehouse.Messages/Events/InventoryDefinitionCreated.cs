using Core.CQRS;
using System;

namespace Warehouse.Messages.Events
{
    public class InventoryDefinitionCreated : Event 
    {
        public readonly Guid Id;
        public readonly string Name;

        public InventoryDefinitionCreated(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
