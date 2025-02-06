using System;
using UnityEngine;

namespace Project.Scripts.Inventory.ReadOnly
{
    public interface IReadOnlyInventorySlot
    {
        public event Action<string> ItemIdChanged;
        public event Action<int> ItemAmountChanged;
        public event Action<string> ItemIconChanged; 
        public event Action<string, int, float, string, string, string> ItemDescriptionChanged; 
        public event Action<Vector2Int> PositionChanged;
        public event Action<int> CapacityChanged;

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
        public Vector2Int Position { get; }
        public bool IsEmpty { get; }
    }
}