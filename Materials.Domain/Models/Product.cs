using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials.Domain.Models
{
    internal class Product 
    {
        public readonly Guid ProductId;

        public Product(Guid productId)
        {
            this.ProductId = productId;
        }
    }
}
