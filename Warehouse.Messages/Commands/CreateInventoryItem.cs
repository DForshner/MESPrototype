using Core.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Messages.Commands
{
    class CreateIventoryItem : Command
    {
        public readonly Guid IventoryItemId;
        public readonly string Name;

        public CreateIventoryItem(Guid inventoryItem, string name)
        {
            this.IventoryItemId = inventoryItem;
            this.Name = name;
        }
    }
}
