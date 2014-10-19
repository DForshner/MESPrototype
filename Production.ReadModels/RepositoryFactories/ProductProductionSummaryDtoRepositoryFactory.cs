using Production.ReadModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.ReadModels.RepositoryFactories
{
    class ProductProductionSummaryDtoRepositoryFactory
    {
        public static Func<IProductProductionSummaryDtoRepository> RepositoryBuilder = Default;

        private static IProductProductionSummaryDtoRepository Default()
        {
            throw new Exception("");
        }

        public IProductProductionSummaryDtoRepository BuildRepository()
        {
            return RepositoryBuilder();
        }
    }
}
