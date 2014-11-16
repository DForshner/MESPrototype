using Core.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Messages.Commands
{
    public class RenameInventoryItem : Command, IVersioned
    {
        public readonly Guid InventoryItemId;
        public readonly string NewName;
        public int Version {get; private set;}

        public RenameInventoryItem(Guid inventoryItemId, string newName, int originalVersion)
        {
            InventoryItemId = inventoryItemId;
            NewName = newName;
            Version = originalVersion;
        }
    }
}