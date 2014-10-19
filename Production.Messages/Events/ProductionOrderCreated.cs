using Core.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Messages.Events
{
    public class ProductionOrderCreated : Event
    {
        public readonly Guid Id;
        public readonly Guid ProductId;
        public readonly int Total;
        public readonly DateTime Created;

        public ProductionOrderCreated(Guid id, Guid productId, int orderTotal, DateTime created)
        {
            this.Id = id;
            this.ProductId = productId;
            this.Total = orderTotal;
            this.Created = created;
        }
    }
}
