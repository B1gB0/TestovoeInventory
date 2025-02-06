using System;
using System.Collections.Generic;
using Project.Scripts.Inventory.Data;
using Project.Scripts.Inventory.ReadOnly;
using Project.Scripts.Inventory.View;

namespace Project.Scripts.Inventory.Controllers
{
    public class InventoryGridController : IDisposable
    {
        private readonly IconsOfItemsDictionaryData _iconsOfItemsDictionaryData;
        private readonly List<InventorySlotController> _slotControllers = new();

        public InventoryGridController(IReadOnlyInventoryGrid inventory, InventoryView view,
            IconsOfItemsDictionaryData iconsOfItemsDictionaryData, InventoryService inventoryService)
        {
            _iconsOfItemsDictionaryData = iconsOfItemsDictionaryData;

            var size = inventory.Size;
            var slots = inventory.GetSlots();
            var lineLength = size.y;

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    var index = i * lineLength + j;
                    var slotView = view.GetInventorySlotView(index);
                    var slot = slots[i, j];

                    _slotControllers.Add(new InventorySlotController(slot, slotView,
                        _iconsOfItemsDictionaryData, inventoryService));
                }
            }
            
            view.SetOwnerId(inventory.OwnerId);
        }

        public void Dispose()
        {
            foreach (var slotController in _slotControllers)
            {
                slotController.Dispose();
            }
        }
    }
}