using Core.CQRS;
using System;

namespace Warehouse.Messages.Commands
{
    public class TransferInventory : Command
    {
        public readonly Guid InventoryDefinitionId;
        public readonly Guid OldLocationId;
        public readonly Guid NewLocationId;
        public readonly int Quantity;

        public TransferInventory(Guid inventoryDefinitionId, Guid oldLocationId, Guid newLocationId, int qtyToCreate)
        {
            this.InventoryDefinitionId = inventoryDefinitionId;
            this.OldLocationId = oldLocationId;
            this.NewLocationId = newLocationId;
            this.Quantity = qtyToCreate;
        }
    }
}