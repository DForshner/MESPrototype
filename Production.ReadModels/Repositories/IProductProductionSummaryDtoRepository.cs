using Production.ReadModels.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.ReadModels.Repositories
{
    public interface IProductProductionSummaryDtoRepository
    {
        void Save(ProductProductionSummaryDto dtoToSave);
        ProductProductionSummaryDto GetId(Guid id);
        ProductProductionSummaryDto GetByProductId(Guid id);
    }
}
