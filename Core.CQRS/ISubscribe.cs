﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CQRS
{
    public interface ISubscribe<T> where T : Event
    {
        void Notify(T eventThatOccured);
    }
}
