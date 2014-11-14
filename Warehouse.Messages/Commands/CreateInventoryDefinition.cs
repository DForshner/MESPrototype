using Core.CQRS;
using System;

namespace Warehouse.Messages.Commands
{
    public class CreateInventoryDefinition : Command
    {
        public readonly Guid InventoryDefinitionId;
        public readonly string Name;

        public CreateInventoryDefinition(Guid inventorydefinitionId, string name)
        {
            this.InventoryDefinitionId = inventorydefinitionId;
            this.Name = name;
        }
    }
}
