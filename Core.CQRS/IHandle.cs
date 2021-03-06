﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CQRS
{
    public interface IHandle<T> where T : Command 
    {
        void Handle(T commandToHandle);
    }
}
