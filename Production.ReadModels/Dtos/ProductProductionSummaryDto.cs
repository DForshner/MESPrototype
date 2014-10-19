using Core.CQRS;
using Production.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.ReadModels.Dtos
{
    public class ProductProductionSummaryDto : Dto
    {
        public Guid ProductId { get; set; }
        public List<ProductionOrderCreated> Orders { get; set; }
        public int TotalOrdered { get; set; }
        public int RunningTotal { get; set; }

        public ProductProductionSummaryDto()
        {
            this.Orders = new List<ProductionOrderCreated>();
        }
    }
}
