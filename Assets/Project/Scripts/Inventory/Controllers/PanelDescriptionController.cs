using System;
using Project.Scripts.GoogleImporter;
using Project.Scripts.Inventory.Data;
using Project.Scripts.Inventory.View;

namespace Project.Scripts.Inventory.Controllers
{
    public class PanelDescriptionController : IDisposable
    {
        private const string PlayerOwner = "Player";
        private const string Armor = "Armor";
        private const string Ammo = "Ammo";
        private const string Medicine = "Medicine";
        private const string Equip = "Экипировать";
        private const string Heal = "Лечить";
        private const string Buy = "Купить";

        private const int Amount = 1;
        
        private const string Body = nameof(Body);
        private const string Head = nameof(Head);
        
        private readonly PanelDescriptionView _panelDescriptionView;
        private readonly IconsOfItemsDictionaryData _iconsData;
        private readonly InventoryService _inventoryService;
        private readonly Player.Player _player;
        private readonly EquipmentController _equipmentController;
        private readonly IGameStateProvider _gameStateProvider;

        private InventorySlotView _slot;
        private string _useButtonText;

        public PanelDescriptionController(PanelDescriptionView panelDescriptionView,
            IconsOfItemsDictionaryData iconsData, InventoryService inventoryService, Player.Player player,
            EquipmentView equipmentView, Equipment equipment, IGameStateProvider gameStateProvider)
        {
            _panelDescriptionView = panelDescriptionView;
            _iconsData = iconsData;
            _inventoryService = inventoryService;
            _player = player;
            
            _equipmentController = new EquipmentController(equipment, equipmentView, iconsData, inventoryService,
                gameStateProvider);
            _gameStateProvider = gameStateProvider;
        }

        public void OnShowView(InventorySlotView slot)
        {
            if(slot.ItemId == null)
                return;
            
            Dispose();

            _slot = slot;

            switch (slot.ClassItem)
            {
                case Armor:
                    _useButtonText = Equip;
                    _panelDescriptionView.UseButton.onClick.AddListener(OnEquipButtonClicked);
                    break;
                case Medicine:
                    _useButtonText = Heal;
                    _panelDescriptionView.UseButton.onClick.AddListener(OnHealButtonClicked);
                    break;
                case Ammo:
                    _useButtonText = Buy;
                    _panelDescriptionView.UseButton.onClick.AddListener(OnBuyButtonClicked);
                    break;
            }
            
            _panelDescriptionView.DeleteButton.onClick.AddListener(OnDeleteButtonClicked);
            _panelDescriptionView.Show(slot, _iconsData.Icons[slot.ClassItem], _useButtonText);
        }
        
        public void Dispose()
        {
            _panelDescriptionView.UseButton.onClick.RemoveListener(OnEquipButtonClicked);
            _panelDescriptionView.UseButton.onClick.RemoveListener(OnHealButtonClicked);
            _panelDescriptionView.UseButton.onClick.RemoveListener(OnBuyButtonClicked);
            _panelDescriptionView.DeleteButton.onClick.RemoveListener(OnDeleteButtonClicked);
        }

        private void OnEquipButtonClicked()
        {
            if (_slot.Specialization == Body)
            {
                _player.SetBodyArmor(_slot.Characteristics);
                
                _equipmentController.OnChangedBodyArmor(_slot.ItemId, _slot.Capacity, _slot.IconName, 
                    _slot.Description, _slot.Characteristics, _slot.Weight, _slot.ClassItem, _slot.Title, 
                    _slot.Specialization, _slot.Capacity);
                
                RemoveItem();
            }
            else if(_slot.Specialization == Head)
            {
                _player.SetHeadArmor(_slot.Characteristics);
                
                _equipmentController.OnChangedHeadArmor(_slot.ItemId, _slot.Capacity, _slot.IconName, 
                    _slot.Description, _slot.Characteristics, _slot.Weight, _slot.ClassItem, _slot.Title, 
                    _slot.Specialization, _slot.Capacity);
                
                RemoveItem();
            }
        }

        private void OnHealButtonClicked()
        {
            _player.Health.AddHealth(_slot.Characteristics);
            RemoveItemInPosition(Amount);
        }

        private void OnBuyButtonClicked()
        {
            ItemSettings item = _gameStateProvider.Items[_slot.ItemId];
            
            _inventoryService.AddItemsToInventory(PlayerOwner, item.Id, item.CellCapacity, item.IconName, 
                item.Description, item.ItemCharacteristics, item.Weight, item.ClassItem, item.Title, 
                item.Specialization, item.CellCapacity);
        }
        
        private void OnDeleteButtonClicked()
        {
            RemoveItemInPosition(_slot.Amount);
        }

        private void RemoveItem()
        {
            _inventoryService.RemoveItemFromInventory(PlayerOwner, _slot.ItemId, _slot.Amount);
        }

        private void RemoveItemInPosition(int amount)
        {
            _inventoryService.RemoveItemFromInventory(PlayerOwner, _slot.Position, _slot.ItemId,  amount);
        }
    }
}