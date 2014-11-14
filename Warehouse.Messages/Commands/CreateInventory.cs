using Core.CQRS;
using System;

namespace Warehouse.Messages.Commands
{
    public class CreateInventory : Command
    {
        public readonly Guid InventoryDefinitionId;
        public readonly Guid LocationId;
        public readonly int Quantity;

        public CreateInventory(Guid inventoryDefinitionId, Guid locationId, int qtyToCreate)
        {
            this.InventoryDefinitionId = inventoryDefinitionId;
            this.LocationId = locationId;
            this.Quantity = qtyToCreate;
        }
    }
}