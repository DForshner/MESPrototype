using Core.CQRS;
using Domain.Warehouse;
using Domain.Warehouse.Domain;
using Warehouse.Messages.Commands;
using Warehouse.Messages.Events;

namespace Warehouse.Domain.CommandHandlers
{
    public class InventoryCommandHandler : 
        IHandle<CreateInventory> 
    {
        public InventoryCommandHandler()
        {
        }

        public void Handle(CreateInventory cmd)
        {
            AggregateRoot._eventPublisher.Publish(new InventoryCreated());
        }
    }
}
