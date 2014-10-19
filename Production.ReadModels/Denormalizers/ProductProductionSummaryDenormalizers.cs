using Core.CQRS;
using Materials.Messages.Events;
using Production.Messages.Events;
using Production.ReadModels.Dtos;
using Production.ReadModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.ReadModels.Denormalizers
{
    public class ProductProductionSummaryDenormalizer : IHandle<ProductionOrderCreated>, IHandle<ProductCreated>
    {
        private readonly IProductProductionSummaryDtoRepository _repo;

        private const int NUM_ORDERS_TO_TRACK = 10;

        public ProductProductionSummaryDenormalizer(IProductProductionSummaryDtoRepository repo)
        {
            this._repo = repo;
        }

        public void Handle(ProductCreated evt)
        {
            var dto = new ProductProductionSummaryDto
            {
                Id = new Guid(),
                ProductId = evt.ProductId
            };
            _repo.Save(dto);
        }

        public void Handle(ProductionOrderCreated evt)
        {
            var dto = _repo.GetByProductId(evt.ProductId);

            dto.Orders.Add(evt);
            dto.TotalOrdered += evt.Total;
            dto.RunningTotal += evt.Total;

            if (dto.Orders.Count > NUM_ORDERS_TO_TRACK)
            {
                dto.RunningTotal -= dto.Orders[0].Total;
                dto.Orders.RemoveAt(0);
            }
        }
    }
}
