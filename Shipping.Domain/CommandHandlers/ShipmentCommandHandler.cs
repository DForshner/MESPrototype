using Core.CQRS;
using Shipping.Domain.AggregateRoots;
using Shipping.Messages.Commands;

namespace Shipping.Domain.CommandHandlers
{
    public class ShipmentCommandHandler : IHandle<CreateShipment>, IHandle<CompleteShipment>
    {
        private readonly IRepository<Shipment> _repo;

        public ShipmentCommandHandler(IRepository<Shipment> repo)
        {
            this._repo = repo; 
        }

        public void Handle(CreateShipment command)
        {
            var shipment = new Shipment(command.ShipmentId);
            _repo.Save(shipment, -1);
        }

        public void Handle(CompleteShipment command)
        {
            var shipment = _repo.GetById(command.ShipmentId);
            shipment.Ship();
            _repo.Save(shipment, command.OriginalVersion);
        }
    }
}
