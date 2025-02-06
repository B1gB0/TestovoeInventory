using System;
using System.Collections.Generic;
using Project.Scripts.Inventory.Data;
using Project.Scripts.Inventory.ReadOnly;
using UnityEngine;
using Project.Scripts.ReadOnly;

namespace Project.Scripts.Inventory
{
    public class InventoryGrid : IReadOnlyInventoryGrid
    {
        private readonly InventoryGridData _data;
        private readonly Dictionary<Vector2Int, InventorySlot> _mapSlots = new ();

        public InventoryGrid(InventoryGridData data)
        {
            _data = data;

            var size = data.Size;

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    var index = i * size.y + j;
                    var slotData = data.Slots[index];
                    var slot = new InventorySlot(slotData);
                    var position = new Vector2Int(i, j);

                    _mapSlots[position] = slot;
                    slot.GetPosition(position);
                }
            }
        }
        
        public event Action<string, int> ItemsAdded;
        public event Action<string, int> ItemsRemoved;
        public event Action<Vector2Int> SizeChanged;

        public string OwnerId => _data.OwnerId;
        public Vector2Int Size => _data.Size;
        
        public AddItemsToInventoryGridResult AddItem(string itemId, int itemSlotCapacity, string iconName,
            string description, int characteristics, float weight, string classItem, string title,
            string specialization, int amount = 1)
        {
            var remainingAmount = amount;
            
            var itemsAddedToSlotsWithSameItemsAmount = AddToSlotsWithSameItems(itemId, remainingAmount, 
                itemSlotCapacity, out remainingAmount);

            if (remainingAmount <= 0)
            {
                return new AddItemsToInventoryGridResult(OwnerId, amount,
                    itemsAddedToSlotsWithSameItemsAmount);
            }

            var itemsAddedToAvailableSlotsAmount = AddToFirstAvailableSlots(itemId, remainingAmount, 
                itemSlotCapacity, iconName, description, characteristics, weight, classItem, title, specialization,
                out remainingAmount);
            
            var totalAddedItemsAmount = itemsAddedToSlotsWithSameItemsAmount + itemsAddedToAvailableSlotsAmount;

            return new AddItemsToInventoryGridResult(OwnerId, amount, totalAddedItemsAmount);
        }

        public AddItemsToInventoryGridResult AddItem(Vector2Int slotCoords, string itemId, int itemSlotCapacity,
            string iconName, string description, int characteristics, float weight, string classItem, string title,
            string specialization, int amount = 1)
        {
            var slot = _mapSlots[slotCoords];
            var newValue = slot.Amount + amount;
            var itemsAddedAmount = 0;

            if (slot.IsEmpty)
            {
                slot.GetItemId(itemId, iconName, description, characteristics, weight, classItem, title, specialization);
            }

            if (newValue > itemSlotCapacity)
            {
                var remainingItems = newValue - itemSlotCapacity;
                var itemsToAddAmount = itemSlotCapacity - slot.Amount;
                itemsAddedAmount += itemsToAddAmount;
                slot.GetAmount(itemSlotCapacity);

                var result = AddItem(itemId, itemSlotCapacity, iconName, description, characteristics,
                    weight, classItem, title, specialization, remainingItems);
                
                itemsAddedAmount += result.ItemsAddedAmount;
            }
            else
            {
                itemsAddedAmount = amount;
                slot.GetAmount(itemSlotCapacity);
            }

            return new AddItemsToInventoryGridResult(OwnerId, amount, itemsAddedAmount);
        }

        public RemoveItemsFromInventoryGridResult RemoveItem(string itemId, int amount = 1)
        {
            if (!Has(itemId, amount))
            {
                return new RemoveItemsFromInventoryGridResult(OwnerId, amount, false);
            }

            var amountToRemove = amount;

            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    var slotCoords = new Vector2Int(i, j);
                    var slot = _mapSlots[slotCoords];

                    if (slot.ItemId != itemId)
                    {
                        continue;
                    }

                    if (amountToRemove > slot.Amount)
                    {
                        amountToRemove -= slot.Amount;

                        RemoveItem(slotCoords, itemId, slot.Amount);
                    }
                    else
                    {
                        RemoveItem(slotCoords, itemId, amountToRemove);
                        
                        ItemsRemoved?.Invoke(itemId, amount);

                        return new RemoveItemsFromInventoryGridResult(OwnerId, amount, true);
                    }
                }
            }

            throw new Exception("Something went wrong with remove items");
        }
        
        public RemoveItemsFromInventoryGridResult RemoveItem(Vector2Int slotCoords, string itemId, int amount = 1)
        {
            var slot = _mapSlots[slotCoords];

            if (slot.IsEmpty || slot.ItemId != itemId || slot.Amount < amount)
            {
                return new RemoveItemsFromInventoryGridResult(OwnerId, amount, false);
            }

            var newAmount = slot.Amount - amount;
            slot.GetAmount(newAmount);

            if (slot.Amount == 0)
            {
                slot.GetItemId(null, null, null, 0, 0, null, null,
                    null);
            }
            
            ItemsRemoved?.Invoke(itemId, amount);

            return new RemoveItemsFromInventoryGridResult(OwnerId, amount, true);
        }

        public int GetAmount(string itemId)
        {
            var amount = 0;
            var slots = _data.Slots;

            foreach (var slot in slots)
            {
                if (slot.ItemId == itemId)
                {
                    amount += slot.Amount;
                }
            }

            return amount;
        }

        public bool Has(string itemId, int amount)
        {
            var amountExist = GetAmount(itemId);

            return amountExist >= amount;
        }

        public void SwitchSlots(Vector2Int slotCoordsA, Vector2Int slotCoordsB)
        {
            var slotA = _mapSlots[slotCoordsA];
            var slotB = _mapSlots[slotCoordsB];
            var tempSlotItemId = slotA.ItemId;
            var tempSlotAmount = slotA.Amount;
            var tempSlotIcon = slotA.IconName;
            var tempSlotDescription = slotA.Description;
            var tempSlotItemValue = slotA.ItemCharacteristics;
            var tempSlotItemWeight = slotA.ItemWeight;
            var tempSlotItemClass = slotA.ClassItem;
            var tempSlotItemTitle = slotA.Title;
            var tempSlotItemSpecialization = slotA.Specialization;
            
            slotA.GetItemId(slotB.ItemId, slotB.IconName, slotB.Description, slotB.ItemCharacteristics,
                slotB.ItemWeight, slotB.ClassItem, slotB.Title, slotB.Specialization);
            slotA.GetAmount(slotB.Amount);
            slotB.GetItemId(tempSlotItemId, tempSlotIcon, tempSlotDescription, tempSlotItemValue,
                tempSlotItemWeight, tempSlotItemClass, tempSlotItemTitle, tempSlotItemSpecialization);
            slotB.GetAmount(tempSlotAmount);
        }

        public IReadOnlyInventorySlot[,] GetSlots()
        {
            var array = new IReadOnlyInventorySlot[Size.x, Size.y];

            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    var position = new Vector2Int(i, j);
                    array[i, j] = _mapSlots[position];
                }
            }

            return array;
        }

        private int AddToSlotsWithSameItems(string itemId, int amount, int itemSlotCapacity, out int remainingAmount)
        {
            var itemsAddedAmount = 0;
            remainingAmount = amount;

            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    var coords = new Vector2Int(i, j);
                    var slot = _mapSlots[coords];

                    if (slot.IsEmpty)
                    {
                        continue;
                    }

                    if (slot.Amount >= itemSlotCapacity)
                    {
                        continue;
                    }

                    if (slot.ItemId != itemId)
                    {
                        continue;
                    }

                    var newValue = slot.Amount + remainingAmount;

                    if (newValue > itemSlotCapacity)
                    {
                        Debug.Log(itemSlotCapacity);
                        remainingAmount = newValue - itemSlotCapacity;
                        var itemsToAddAmount = itemSlotCapacity - slot.Amount;
                        itemsAddedAmount += itemsToAddAmount;
                        Debug.Log(itemSlotCapacity);
                        slot.GetAmount(itemSlotCapacity);

                        if (remainingAmount == 0)
                        {
                            return itemsAddedAmount;
                        }
                    }
                    else
                    {
                        itemsAddedAmount += remainingAmount;
                        slot.GetAmount(newValue);
                        remainingAmount = 0;

                        return itemsAddedAmount;
                    }
                }
            }
            
            ItemsAdded?.Invoke(itemId, amount);

            return itemsAddedAmount;
        }
        
        private int AddToFirstAvailableSlots(string itemId, int amount, int itemSlotCapacity, string iconName,
            string description, int characteristics, float weight, string classItem, string title,
            string specialization, out int remainingAmount)
        {
            var itemsAddedAmount = 0;
            remainingAmount = amount;

            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    var coords = new Vector2Int(i, j);
                    var slot = _mapSlots[coords];
                    
                    if (!slot.IsEmpty)
                    {
                        continue;
                    }

                    slot.GetItemId(itemId, iconName, description, characteristics, weight, classItem, title, specialization);
                    var newValue = remainingAmount;

                    if (newValue > itemSlotCapacity)
                    {
                        remainingAmount = newValue - itemSlotCapacity;
                        var itemsToAddAmount = itemSlotCapacity;
                        itemsAddedAmount += itemsToAddAmount;
                        slot.GetAmount(itemSlotCapacity);
                    }
                    else
                    {
                        itemsAddedAmount += remainingAmount;
                        slot.GetAmount(newValue);
                        remainingAmount = 0;

                        return itemsAddedAmount;
                    }
                }
            }
            
            ItemsAdded?.Invoke(itemId, amount);

            return itemsAddedAmount;
        }
    }
}