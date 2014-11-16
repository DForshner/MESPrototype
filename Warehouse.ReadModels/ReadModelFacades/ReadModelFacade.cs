using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.ReadModels.Dtos;

namespace Warehouse.ReadModels.ReadModelFacades
{
    public class ReadModelFacade : IReadModelFacade
    {
        public IEnumerable<InventoryItemListDto> GetInventoryItems()
        {
            return FakeDatabase.list;
        }

        public InventoryItemDetailsDto GetInventoryItemDetails(Guid id)
        {
            return FakeDatabase.details.ContainsKey(id) ?
                FakeDatabase.details[id] :
                null;
        }
    }
}
