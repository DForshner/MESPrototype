using Core.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Messages.Commands
{
    public class RemoveItemsFromInventory : Command
    {
        public Guid Id { get; set; }
        public int Count { get; set; }

        public RemoveItemsFromInventory(Guid id, int count)
        {
            Id = id;
            Count = count;
        }
    }
}