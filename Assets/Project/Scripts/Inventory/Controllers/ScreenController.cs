using Project.Scripts.Inventory.Data;
using Project.Scripts.Inventory.View;

namespace Project.Scripts.Inventory.Controllers
{
    public class ScreenController
    {
        private readonly InventoryService _inventoryService;
        private readonly ScreenView _view;
        private readonly IconsOfItemsDictionaryData _iconsOfItemsDictionaryData;

        private InventoryGridController _currentInventoryController;

        public ScreenController(InventoryService inventoryService, ScreenView view,
            IconsOfItemsDictionaryData iconsOfItemsDictionaryData)
        {
            _inventoryService = inventoryService;
            _iconsOfItemsDictionaryData = iconsOfItemsDictionaryData;
            _view = view;
        }

        public void OpenInventory(string ownerId)
        {
            var inventory = _inventoryService.GetInventory(ownerId);
            var inventoryView = _view.InventoryView;

            _currentInventoryController = new InventoryGridController(inventory, inventoryView,
                _iconsOfItemsDictionaryData, _inventoryService);
        }
    }
}