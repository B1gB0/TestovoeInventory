using System;
using Project.Scripts.Inventory.Data;
using Project.Scripts.Inventory.ReadOnly;
using UnityEngine;

namespace Project.Scripts.Inventory
{
    public class InventorySlot : IReadOnlyInventorySlot
    {
        private readonly InventorySlotData _data;
        
        public InventorySlot(InventorySlotData data)
        {
            _data = data;
            
            GetItemId(_data.ItemId, _data.IconName, _data.Description);
            GetAmount(_data.Amount);
        }
        
        public event Action<string> ItemIdChanged;
        public event Action<int> ItemAmountChanged;
        public event Action<string> ItemIconChanged;
        public event Action<string> ItemDescriptionChanged;

        public string ItemId { get; private set; }

        public int Amount { get; private set; }

        public string IconName { get; private set; }
        public string Description { get; private set; }

        public int Capacity => _data.Capacity;

        public bool IsEmpty => Amount == 0 && string.IsNullOrEmpty(ItemId);

        public void GetItemId(string itemId, string iconName, string description)
        {
            ItemId = itemId;
            IconName = iconName;
            Description = description;
            Debug.Log(description);
            Debug.Log(Description);

            if (_data.ItemId != ItemId)
            {
                _data.ItemId = ItemId;
                _data.IconName = IconName;
                _data.Description = Description;
            }
            
            ItemIconChanged?.Invoke(iconName);
            ItemIdChanged?.Invoke(itemId);
            ItemDescriptionChanged?.Invoke(description);
        }

        public void GetAmount(int amount)
        {
            Amount = amount;

            if (_data.Amount != Amount)
            {
                _data.Amount = Amount;
            }

            ItemAmountChanged?.Invoke(amount);
        }
    }
}