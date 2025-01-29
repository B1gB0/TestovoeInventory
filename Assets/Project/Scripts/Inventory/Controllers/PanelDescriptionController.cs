using Project.Scripts.Inventory.Data;
using Project.Scripts.Inventory.View;

namespace Project.Scripts.Inventory.Controllers
{
    public class PanelDescriptionController
    {
        private const string Armor = "Armor";
        private const string Ammo = "Ammo";
        private const string Medicine = "Medicine";
        private const string Equip = "Экипировать";
        private const string Heal = "Лечить";
        private const string Buy = "Купить";
        
        private readonly PanelDescriptionView _panelDescriptionView;
        private readonly IconsOfItemsDictionaryData _iconsData;

        private string _useButtonText;

        public PanelDescriptionController(PanelDescriptionView panelDescriptionView,
            IconsOfItemsDictionaryData iconsData, InventoryService inventoryService)
        {
            _panelDescriptionView = panelDescriptionView;
            _iconsData = iconsData;
            
            _panelDescriptionView.GetInventoryService(inventoryService);
        }

        public void OnShowView(InventorySlotView slot)
        {
            if(slot.ItemId == null)
                return;

            _useButtonText = slot.ClassItem switch
            {
                Armor => Equip,
                Medicine => Heal,
                Ammo => Buy,
                _ => _useButtonText
            };

            _panelDescriptionView.Show(slot, _iconsData.Icons[slot.ClassItem], _useButtonText);
        }
    }
}