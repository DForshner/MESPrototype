using Core.CQRS;
using Domain.Warehouse;
using Domain.Warehouse.Domain;
using Warehouse.Messages.Commands;

namespace Warehouse.Domain.CommandHandlers
{
    public class LocationCommandHandler : 
        IHandle<CreateLocation>, 
        IHandle<CreateInventoryDefinition>
    {
        private readonly IRepository<Location> _repo;

        public LocationCommandHandler(IRepository<Location> repo)
        {
            this._repo = repo; 
        }

        public void Handle(CreateLocation cmd)
        {
            var location = new Location(cmd.LocationId, cmd.Name);
            //_repo.Save(location, -1);
        }

        public void Handle(CreateInventoryDefinition cmd)
        {
            var def = new InventoryDefinition(cmd.InventoryDefinitionId, cmd.Name);
        }
    }
}
