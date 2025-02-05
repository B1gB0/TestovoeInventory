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

            OnChangedBodyArmor(equipment.BodyArmorId, equipment.BodyCapacity, equipment.BodyArmorIconName, 
                equipment.BodyDescription, equipment.BodyArmorCharacteristics, equipment.BodyWeight,
                equipment.BodyClassItem, equipment.BodyTitle, equipment.BodySpecialization, equipment.BodyAmount);
            
            OnChangedHeadArmor(equipment.HeadArmorId, equipment.HeadCapacity, equipment.HeadArmorIconName,
                equipment.HeadDescription, equipment.HeadArmorCharacteristics, equipment.HeadWeight,
                equipment.HeadClassItem, equipment.HeadTitle, equipment.HeadSpecialization, equipment.HeadAmount);
        }

        public void OnChangedBodyArmor(string itemId, int itemSlotCapacity,
            string iconName, string description, int itemCharacteristics, float weight, string classItem,
            string title, string specialization, int amount)
        {
            if(string.IsNullOrEmpty(itemId))
                return;

            if (_equipment.BodyArmorId != itemId)
            {
                _inventoryService.AddItemsToInventory(PlayerOwner, _equipment.BodyArmorId, _equipment.BodyCapacity,
                    _equipment.BodyArmorIconName, _equipment.BodyDescription, _equipment.BodyArmorCharacteristics, 
                    _equipment.BodyWeight, _equipment.BodyClassItem, _equipment.BodyTitle, _equipment.BodySpecialization,
                    _equipment.BodyAmount);
            }
            
            _equipment.GetDataOfBodyArmor(itemId, itemSlotCapacity, iconName, description, itemCharacteristics, weight,
                classItem, title, specialization, amount);
            _view.SetBodyArmor(itemCharacteristics, _icons.Icons[iconName]);
        }
        
        public void OnChangedHeadArmor(string itemId, int itemSlotCapacity,
            string iconName, string description, int itemCharacteristics, float weight, string classItem,
            string title, string specialization, int amount = 1)
        {
            if(string.IsNullOrEmpty(itemId))
                return;
            
            if (_equipment.HeadArmorId != itemId)
            {
                _inventoryService.AddItemsToInventory(PlayerOwner, _equipment.HeadArmorId, _equipment.HeadCapacity,
                    _equipment.HeadArmorIconName, _equipment.HeadDescription, _equipment.HeadArmorCharacteristics, 
                    _equipment.HeadWeight, _equipment.HeadClassItem, _equipment.HeadTitle, _equipment.HeadSpecialization,
                    _equipment.HeadAmount);
            }
            
            _equipment.GetDataOfHeadArmor(itemId, itemSlotCapacity, iconName, description, itemCharacteristics, weight,
                classItem, title, specialization, amount);
            _view.SetHeadArmor(itemCharacteristics, _icons.Icons[iconName]);
        }
    }
}