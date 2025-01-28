using System;

namespace Project.Scripts.ReadOnly
{
    public interface IReadOnlyInventory
    {
        public event Action<string, int> ItemsAdded;
        public event Action<string, int> ItemsRemoved;
        
        public string OwnerId { get; }
        
        public int GetAmount(string itemId);
        
        public bool Has(string itemId, int amount);
    }
}