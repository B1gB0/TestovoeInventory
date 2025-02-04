using System;

namespace Project.Scripts.Inventory.ReadOnly
{
    public interface IReadOnlyInventorySlot
    {
        public event Action<string> ItemIdChanged;
        public event Action<int> ItemAmountChanged;
        public event Action<string> ItemIconChanged; 
        public event Action<string, int, float, string, string, string> ItemDescriptionChanged; 

        public string ItemId { get; }
        public int Amount { get; }
        public string IconName { get; }
        public string Description { get; }
        public int ItemCharacteristics { get; }
        public float ItemWeight { get; }
        public string ClassItem { get; }
        public string Title { get; }
        public string Specialization { get; }
        public int Capacity { get; }
        public bool IsEmpty { get; }
    }
}