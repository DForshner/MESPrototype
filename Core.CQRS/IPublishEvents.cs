﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CQRS
{
    public interface IPublishEvents
    {
        void Publish<T>(T eventToPublish) where T : Event;
    }
}
