using Core.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FakePersistance
{
    public class Repository<T> : IRepository<T> where T : AggregateRoot
    {
        public void Save(T aggregate, int expectedVersion)
        {
            throw new NotImplementedException();
        }

        public T GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
