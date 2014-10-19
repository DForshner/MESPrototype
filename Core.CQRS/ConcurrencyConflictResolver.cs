using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CQRS
{
    public class ConcurrencyConflictResolver : IConcurrencyConflictResolver
    {
        private readonly Dictionary<Type, List<Type>> _conflicts = new Dictionary<Type, List<Type>>();

        public bool ConflictsWith(Type eventToCheck, IEnumerable<Type> previousEvents)
        {
            Debug.Assert(eventToCheck.IsSubclassOf(typeof(Event)), "Conflicts can only be registered with the Event type.");
            Debug.Assert(previousEvents.All(x => x.IsSubclassOf(typeof(Event))), "Conflicts can only be registered with the Event type.");

            // If type hasn't been registered assume worst case
            if (!_conflicts.ContainsKey(eventToCheck))
                return true;

            return previousEvents.Any(pe => 
                _conflicts[eventToCheck].Any(c => c == pe ));
        }

        public void RegisterConflict(Type eventType, IEnumerable<Type> conflictsWith)
        {
            Debug.Assert(eventType.IsSubclassOf(typeof(Event)), "Conflicts can only be registered with the Event type.");
            Debug.Assert(conflictsWith.All(x => x.IsSubclassOf(typeof(Event))), "Conflicts can only be registered with the Event type.");

            if (_conflicts.ContainsKey(eventType))
                throw new Exception("Conflicts have already been registered for this event type.");

            var conflictsToAdd = (conflictsWith != null) ? new List<Type>(conflictsWith) : new List<Type>();

            _conflicts.Add(eventType, conflictsToAdd);
        }
    }
}
