using Core.CQRS;
using System;

namespace Warehouse.Messages.Commands
{
    public class DeactivateInventoryItem : Command, IVersioned
    {
        public Guid Id { get; private set; }
        public int Version { get; private set; }

        public DeactivateInventoryItem(Guid id, int concurrencyVersion)
        {
            Id = id;
            Version = concurrencyVersion;
        }
    }

    public class CreateInventoryItem : Command
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }

        public CreateInventoryItem(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    //public class RenameInventoryItem// : IConcurrencyAware
    //{
    //    public Guid Id { get; set; }
    //    public string ConcurrencyVersion { get; set; }
    //    public string NewName { get; set; }
    //}

    public class CheckInItemsToInventory : Command
    {
        public Guid Id { get; private set; }
        public int Count { get; private set; }

        public CheckInItemsToInventory(Guid id, int count)
        {
            Id = id;
            Count = count;
        }
    }

    //public class RemoveItemsFromInventory
    //{
    //    public Guid Id { get; set; }
    //    public int Count { get; set; }
    //}
}
