using Core.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Warehouse
{
    internal class InventoryItem : AggregateRoot
    {
        private bool _active;
        private Guid _id;

        public InventoryItem()
        {

        }

        public InventoryItem(Guid id, string name)
        {

        }

        public override Guid Id
        {
            get { return _id; }
        }
    }
}
