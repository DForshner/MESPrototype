using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CQRS
{
    public interface IConcurrencyConflictResolver
    {
        bool ConflictsWith(Type eventToCheck, IEnumerable<Type> previousEvents);
        void RegisterConflict(Type eventType, IEnumerable<Type> conflictsWith);
    }
}
