using Project.Scripts.GoogleImporter;
using Project.Scripts.Inventory.Data;
using Project.Scripts.Inventory.View;

namespace Project.Scripts.Inventory.Controllers
{
    public class EquipmentController
    {
        private const string PlayerOwner = "Player";
        
        private readonly EquipmentView _view;
        private readonly IconsOfItemsDictionaryData _icons;
        private readonly Equipment _equipment;
        private readonly InventoryService _inventoryService;
        private readonly IGameStateProvider _gameStateProvider;

        public EquipmentController(Equipment equipment, EquipmentView view, IconsOfItemsDictionaryData icons,
            InventoryService inventoryService, IGameStateProvider gameStateProvider)
        {
            _equipment = equipment;
            _view = view;
            _icons = icons;
            _inventoryService = inventoryService;
            _gameStateProvider = gameStateProvider;

            if (_equipment.BodyArmorId != null)
            {
                OnChangedBodyArmor(equipment.BodyArmorId, equipment.BodyCapacity, equipment.BodyArmorIconName, 
                    equipment.BodyDescription, equipment.BodyArmorCharacteristics, equipment.BodyWeight,
                    equipment.BodyClassItem, equipment.BodyTitle, equipment.BodySpecialization, equipment.BodyAmount);
            }

            if (_equipment.HeadArmorId != null)
            {
                OnChangedHeadArmor(equipment.HeadArmorId, equipment.HeadCapacity, equipment.HeadArmorIconName,
                    equipment.HeadDescription, equipment.HeadArmorCharacteristics, equipment.HeadWeight,
                    equipment.HeadClassItem, equipment.HeadTitle, equipment.HeadSpecialization, equipment.HeadAmount);
            }
        }

        public void OnChangedBodyArmor(string itemId, int itemSlotCapacity,
            string iconName, string description, int itemCharacteristics, float weight, string classItem,
            string title, string specialization, int amount)
        {
            if (_equipment.BodyArmorId != null)
            {
                ItemSettings item = _gameStateProvider.Items[_equipment.BodyArmorId];
            
                _inventoryService.AddItemsToInventory(PlayerOwner, item.Id, item.CellCapacity, item.IconName, 
                    item.Description, item.ItemCharacteristics, item.Weight, item.ClassItem, item.Title, 
                    item.Specialization, item.CellCapacity);
            }
            
            _equipment.GetDataOfBodyArmor(itemId, itemSlotCapacity, iconName, description, itemCharacteristics, weight,
                classItem, title, specialization, amount);
            _view.SetBodyArmor(itemCharacteristics, _icons.Icons[iconName]);
        }
        
        public void OnChangedHeadArmor(string itemId, int itemSlotCapacity,
            string iconName, string description, int itemCharacteristics, float weight, string classItem,
            string title, string specialization, int amount = 1)
        {
            if (_equipment.HeadArmorId != null)
            {
                ItemSettings item = _gameStateProvider.Items[_equipment.HeadArmorId];
            
                _inventoryService.AddItemsToInventory(PlayerOwner, item.Id, item.CellCapacity, item.IconName, 
                    item.Description, item.ItemCharacteristics, item.Weight, item.ClassItem, item.Title, 
                    item.Specialization, item.CellCapacity);
            }
            
            _equipment.GetDataOfHeadArmor(itemId, itemSlotCapacity, iconName, description, itemCharacteristics, weight,
                classItem, title, specialization, amount);
            _view.SetHeadArmor(itemCharacteristics, _icons.Icons[iconName]);
        }
    }
}