using Core.CQRS;
using System;
using Warehouse.Messages.Events;

namespace Domain.Warehouse.Domain
{
    public class Location : AggregateRoot 
    {
        private readonly Guid _id;
        private readonly string _name;

        public Location(Guid id, string name)
        {
            this._id= id;
            this._name = name;

            EventPublisher.Publish(new LocationCreated(_id, _name));
        }

        public override Guid Id { get { return _id; } }
    }
}