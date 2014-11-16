using Core.CQRS;

namespace Core.Messages.Events
{
    public class ManagerStarted : Event 
    {
        public readonly string Name;

        public ManagerStarted(string name)
        {
            this.Name = name;
        }
    }
}
