using Project.Scripts.Inventory.Data;
using Project.Scripts.Inventory.View;

namespace Project.Scripts.Inventory.Controllers
{
    public class ScreenController
    {
        private readonly InventoryService _inventoryService;
        private readonly ScreenView _view;
        private readonly IconsDictionaryData _iconsDictionaryData;

        private InventoryGridController _currentInventoryController;

        public ScreenController(InventoryService inventoryService, ScreenView view,
            IconsDictionaryData iconsDictionaryData)
        {
            _inventoryService = inventoryService;
            _iconsDictionaryData = iconsDictionaryData;
            _view = view;
        }

        public void OpenInventory(string ownerId)
        {
            var inventory = _inventoryService.GetInventory(ownerId);
            var inventoryView = _view.InventoryView;

            _currentInventoryController = new InventoryGridController(inventory, inventoryView, _iconsDictionaryData);
        }
    }
}