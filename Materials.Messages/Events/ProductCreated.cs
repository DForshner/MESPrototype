using Core.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials.Messages.Events
{
    public class ProductCreated : Event 
    {
        public readonly Guid ProductId;

        public ProductCreated(Guid productId)
        {
            this.ProductId = productId;
        }
    }
}
