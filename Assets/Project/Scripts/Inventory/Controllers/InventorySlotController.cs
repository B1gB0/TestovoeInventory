using System;
using Project.Scripts.Inventory.Data;
using Project.Scripts.Inventory.ReadOnly;
using Project.Scripts.Inventory.View;
using UnityEngine;

namespace Project.Scripts.Inventory.Controllers
{
    public class InventorySlotController : IDisposable
    {
        private readonly InventorySlotView _view;
        private readonly IReadOnlyInventorySlot _slot;

        public InventorySlotController(IReadOnlyInventorySlot slot, InventorySlotView view,
            IconsOfItemsDictionaryData iconsOfItemsDictionaryData, InventoryService inventoryService)
        {
            _slot = slot;
            _view = view;
            _view.GetData(iconsOfItemsDictionaryData, inventoryService);

            slot.ItemIdChanged += OnSlotItemIdChanged;
            slot.ItemAmountChanged += OnSlotItemAmountChanged;
            slot.ItemIconChanged += OnSlotItemIconChanged;
            slot.ItemDescriptionChanged += OnSlotItemDescriptionChanged;
            slot.PositionChanged += OnSlotItemPositionChanged;
            slot.CapacityChanged += OnSlotCapacityChanged;

            OnSlotItemIdChanged(slot.ItemId);
            OnSlotItemAmountChanged(slot.Amount);
            OnSlotItemIconChanged(slot.IconName);
            OnSlotItemDescriptionChanged(slot.Description, slot.ItemCharacteristics, slot.ItemWeight, slot.ClassItem, 
                slot.Title, slot.Specialization);
            OnSlotItemPositionChanged(slot.Position);
            
            _view.SetCapacity(slot.Capacity);
        }
        
        public void Dispose()
        {
            _slot.ItemIdChanged -= OnSlotItemIdChanged;
            _slot.ItemAmountChanged -= OnSlotItemAmountChanged;
            _slot.ItemIconChanged -= OnSlotItemIconChanged;
            _slot.ItemDescriptionChanged -= OnSlotItemDescriptionChanged;
            _slot.PositionChanged -= OnSlotItemPositionChanged;
            _slot.CapacityChanged -= OnSlotCapacityChanged;
        }
        
        private void OnSlotItemIdChanged(string newItemId)
        {
            _view.SetId(newItemId);
        }

        private void OnSlotItemAmountChanged(int newAmount)
        {
            _view.SetAmount(newAmount);
        }

        private void OnSlotItemIconChanged(string newIconName)
        {
            if (string.IsNullOrEmpty(newIconName))
            {
                _view.HideIcon();
                return;
            }
            
            _view.SetIcon(newIconName);
        }

        private void OnSlotItemDescriptionChanged(string newDescription, int characteristics,
            float weight, string classItem, string title, string specialization)
        {
            _view.SetDescriptionData(newDescription, characteristics, weight, classItem, title, specialization);
        }

        private void OnSlotItemPositionChanged(Vector2Int position)
        {
            _view.SetPosition(position);
        }

        private void OnSlotCapacityChanged(int capacity)
        {
            _view.SetCapacity(capacity);
        }
    }
}