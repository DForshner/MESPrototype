using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Messages.Commands
{
    public class InventoryItemCreated
    {
        public readonly Guid Id;
        public readonly string Name;
        public InventoryItemCreated(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
