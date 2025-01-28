using Project.Scripts.Inventory.Data;
using Project.Scripts.Inventory.ReadOnly;
using Project.Scripts.Inventory.View;

namespace Project.Scripts.Inventory.Controllers
{
    public class InventorySlotController
    {
        private const int MinValue = 1;
        private const int Null = 0;

        private readonly InventorySlotView _view;

        public InventorySlotController(IReadOnlyInventorySlot slot, InventorySlotView view,
            IconsDictionaryData iconsDictionaryData)
        {
            _view = view;
            _view.GetData(iconsDictionaryData);

            slot.ItemIdChanged += OnSlotItemIdChanged;
            slot.ItemAmountChanged += OnSlotItemAmountChanged;
            slot.ItemIconChanged += OnSlotItemIconChanged;
            slot.ItemDescriptionChanged += OnSlotItemDescription;

            OnSlotItemIdChanged(slot.ItemId);
            OnSlotItemAmountChanged(slot.Amount);
            OnSlotItemIconChanged(slot.IconName);
            OnSlotItemDescription(slot.Description);
        }
        
        private void OnSlotItemIdChanged(string newItemId)
        {
            _view.SetTitle(newItemId);
        }

        private void OnSlotItemAmountChanged(int newAmount)
        {
            if (newAmount > MinValue)
            {
                _view.SetAmount(newAmount);
            }
            else
            {
                _view.SetAmount(Null);
            }
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

        private void OnSlotItemDescription(string newDescription)
        {
            _view.SetDescription(newDescription);
        }
    }
}