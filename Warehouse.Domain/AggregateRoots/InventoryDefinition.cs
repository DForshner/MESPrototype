using Core.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Messages.Events;

namespace Domain.Warehouse
{
    internal class InventoryDefinition : AggregateRoot
    {
        private Guid _id;
        private string _name;

        public InventoryDefinition()
        {

        }

        public InventoryDefinition(Guid id, string name)
        {
            _id = id;
            _name = name;

            EventPublisher.Publish(new InventoryDefinitionCreated(_id, _name));
        }

        public override Guid Id
        {
            get { return _id; }
        }
    }
}
