﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CQRS
{
    public interface IRepository<T> where T : AggregateRoot
    {
        void Save(AggregateRoot aggregate, int expectedVersion);
        T GetById(Guid id);
    }
}
