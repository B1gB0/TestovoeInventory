using System.Collections.Generic;
using Project.Scripts.Inventory.Data;
using Project.Scripts.Inventory.View;
using Project.Scripts.ReadOnly;

namespace Project.Scripts.Inventory.Controllers
{
    public class InventoryGridController
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

                    _slotControllers.Add(new InventorySlotController(slot, slotView, _iconsOfItemsDictionaryData));
                }
            }
            
            view.SetOwnerId(inventory.OwnerId);
        }
    }
}