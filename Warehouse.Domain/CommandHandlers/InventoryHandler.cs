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
        private readonly IRepository<InventoryItem> _repository;

        public InventoryCommandHandler(IRepository<InventoryItem> repository)
        {
            _repository = repository;
        }

        public void Handle(CreateInventoryItem message)
        {
            // TODO: Instead of new-ing inventory item here we should get a call an 
            // existing aggregate and have it crate the inventory item for us.
            // See Udi post on don't create aggregate roots.
            var item = new InventoryItem(message.Id, message.Name);
            _repository.Save(item, null);
        }

        public void Handle(DeactivateInventoryItem message)
        {
            var item = _repository.GetById(message.Id);
            item.Deactivate();
            _repository.Save(item, message.Version);
        }

        public void Handle(RemoveItemsFromInventory message)
        {
            var item = _repository.GetById(message.Id);
            item.Remove(message.Count);
            _repository.Save(item, null);
        }

        public void Handle(CheckInItemsToInventory message)
        {
            var item = _repository.GetById(message.Id);
            item.CheckIn(message.Count);
            _repository.Save(item, null);
        }

        public void Handle(RenameInventoryItem message)
        {
            var item = _repository.GetById(message.InventoryItemId);
            item.ChangeName(message.NewName);
            _repository.Save(item, message.Version);
        }

        public void Handle(CreateInventory cmd)
        {
            AggregateRoot._eventPublisher.Publish(new InventoryCreated());
        }
    }
}