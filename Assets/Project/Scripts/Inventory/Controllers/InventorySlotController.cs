using Project.Scripts.Inventory.Data;
using Project.Scripts.Inventory.ReadOnly;
using Project.Scripts.Inventory.View;

namespace Project.Scripts.Inventory.Controllers
{
    public class InventorySlotController
    {
        private readonly InventorySlotView _view;

        public InventorySlotController(IReadOnlyInventorySlot slot, InventorySlotView view,
            IconsOfItemsDictionaryData iconsOfItemsDictionaryData)
        {
            _view = view;
            _view.GetData(iconsOfItemsDictionaryData);

            slot.ItemIdChanged += OnSlotItemIdChanged;
            slot.ItemAmountChanged += OnSlotItemAmountChanged;
            slot.ItemIconChanged += OnSlotItemIconChanged;
            slot.ItemDescriptionChanged += OnSlotItemDescription;

            OnSlotItemIdChanged(slot.ItemId);
            OnSlotItemAmountChanged(slot.Amount);
            OnSlotItemIconChanged(slot.IconName);
            OnSlotItemDescription(slot.Description, slot.ItemCharacteristics, slot.ItemWeight, slot.ClassItem, 
                slot.Title, slot.Specialization);
            
            _view.SetCapacity(slot.Capacity);
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

        private void OnSlotItemDescription(string newDescription, int characteristics,
            float weight, string classItem, string title, string specialization)
        {
            _view.SetDescriptionData(newDescription, characteristics, weight, classItem, title, specialization);
        }
    }
}