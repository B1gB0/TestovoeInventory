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

            GetItemId(_data.ItemId, _data.IconName, _data.Description, _data.ItemCharacteristics, _data.Weight,
                _data.ClassItem, _data.Title, _data.Specialization);
            GetAmount(_data.Amount);
        }
        
        public event Action<string> ItemIdChanged;
        public event Action<int> ItemAmountChanged;
        public event Action<string> ItemIconChanged;
        public event Action<string, string, string, string, string, string> ItemDescriptionChanged;

        public string ItemId { get; private set; }
        public int Amount { get; private set; }
        public string IconName { get; private set; }
        public string Description { get; private set; }
        public string ItemCharacteristics { get; private set; }
        public string ItemWeight { get; private set; }
        public string ClassItem { get; private set; }
        public string Title { get; private set; }
        public string Specialization { get; private set; }

        public int Capacity => _data.Capacity;
        public bool IsEmpty => Amount == 0 && string.IsNullOrEmpty(ItemId);

        public void GetItemId(string itemId, string iconName, string description, string itemCharacteristics,
            string weight, string classItem, string title, string specialization)
        {
            ItemId = itemId;
            IconName = iconName;
            Description = description;
            ItemCharacteristics = itemCharacteristics;
            ItemWeight = weight;
            ClassItem = classItem;
            Title = title;
            Specialization = specialization;

            if (_data.ItemId != ItemId)
            {
                _data.ItemId = ItemId;
                _data.IconName = IconName;
                _data.Description = Description;
                _data.ItemCharacteristics = ItemCharacteristics;
                _data.Weight = ItemWeight;
                _data.ClassItem = ClassItem;
                _data.Title = Title;
                _data.Specialization = Specialization;
            }

            ItemIconChanged?.Invoke(iconName);
            ItemIdChanged?.Invoke(itemId);
            ItemDescriptionChanged?.Invoke(description, itemCharacteristics, weight, classItem, title, specialization);
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