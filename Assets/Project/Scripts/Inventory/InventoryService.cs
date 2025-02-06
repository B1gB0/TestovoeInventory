using System.Collections.Generic;
using Project.Scripts.Inventory.Data;
using Project.Scripts.ReadOnly;
using UnityEngine;

namespace Project.Scripts.Inventory
{
    public class InventoryService
    {
        private readonly Dictionary<string, InventoryGrid> _inventoriesMap = new();

        public InventoryGrid RegisterInventory(InventoryGridData inventoryData)
        {
            var inventory = new InventoryGrid(inventoryData);
            _inventoriesMap[inventory.OwnerId] = inventory;

            return inventory;
        }

        public void SwitchInventoryItems(string ownerId, Vector2Int slotCoordsA, Vector2Int slotCoordsB)
        {
            var inventory = _inventoriesMap[ownerId];
            inventory.SwitchSlots(slotCoordsA, slotCoordsB);
        }

        public AddItemsToInventoryGridResult AddItemsToInventory(string ownerId, string itemId, int itemSlotCapacity,
            string iconName, string description, int itemCharacteristics, float weight, string classItem,
            string title, string specialization, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            var result = inventory.AddItem(itemId, itemSlotCapacity, iconName, description, itemCharacteristics,
                weight, classItem, title, specialization, amount);

            return result;
        }

        public AddItemsToInventoryGridResult AddItemsToInventory(
            string ownerId, Vector2Int slotCoords, string itemId, int itemSlotCapacity, string iconName,
            string description, int itemCharacteristics, float weight, string classItem, string title, 
            string specialization, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            var result = inventory.AddItem(slotCoords, itemId, itemSlotCapacity, iconName, description, itemCharacteristics, 
                weight,  classItem, title, specialization, amount);

            return result;
        }

        public RemoveItemsFromInventoryGridResult RemoveItemFromInventory(string ownerId, string itemId, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            var result = inventory.RemoveItem(itemId, amount);

            return result;
        }

        public RemoveItemsFromInventoryGridResult RemoveItemFromInventory(
            string ownerId, Vector2Int slotCoords, string itemId, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            var result = inventory.RemoveItem(slotCoords, itemId, amount);

            return result;
        }
        
        public bool Has(string ownerId, string itemId, int amount)
        {
            var inventory = _inventoriesMap[ownerId];
            return inventory.Has(itemId, amount);
        }

        public IReadOnlyInventoryGrid GetInventory(string ownerId)
        {
            return _inventoriesMap[ownerId];
        }
    }
}