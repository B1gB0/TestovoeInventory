using System;
using Project.Scripts.Inventory.Data;
using Project.Scripts.Inventory.View;
using UnityEngine;

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
        private const string Body = nameof(Body);
        private const string Head = nameof(Head);
        
        private readonly PanelDescriptionView _panelDescriptionView;
        private readonly IconsOfItemsDictionaryData _iconsData;
        private readonly InventoryService _inventoryService;
        private readonly Player.Player _player;
        private readonly EquipmentView _equipmentView;
        
        private InventorySlotView _slot;
        private string _useButtonText;

        public PanelDescriptionController(PanelDescriptionView panelDescriptionView,
            IconsOfItemsDictionaryData iconsData, InventoryService inventoryService, Player.Player player,
            EquipmentView equipmentView)
        {
            _panelDescriptionView = panelDescriptionView;
            _iconsData = iconsData;
            _inventoryService = inventoryService;
            _player = player;
            _equipmentView = equipmentView;
        }

        public void OnShowView(InventorySlotView slot)
        {
            if(slot.ItemId == null)
                return;

            _slot = slot;
            Debug.Log(slot.ClassItem);
            Debug.Log(_slot.ClassItem);

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
            
            _panelDescriptionView.CloseButton.onClick.AddListener(Dispose);
            _panelDescriptionView.DeleteButton.onClick.AddListener(OnDeleteButtonClicked);
            _panelDescriptionView.Show(slot, _iconsData.Icons[slot.ClassItem], _useButtonText);
        }

        private void OnEquipButtonClicked()
        {
            Debug.Log(_slot.Specialization);
            if (_slot.Specialization == Body)
            {
                Debug.Log(_slot.Specialization);
                _player.SetBodyArmor(_slot.Characteristics);
                _equipmentView.SetBodyArmor(_slot.Characteristics, _iconsData.Icons[_slot.IconName]);
            }
            else if(_slot.Specialization == Head)
            {
                Debug.Log(_slot.Specialization);
                _player.SetHeadArmor(_slot.Characteristics);
                _equipmentView.SetHeadArmor(_slot.Characteristics, _iconsData.Icons[_slot.IconName]);
            }
        }

        private void OnHealButtonClicked()
        {
            _player.Health.AddHealth(_slot.Characteristics);
        }

        private void OnBuyButtonClicked()
        {
            _inventoryService.AddItemsToInventory(PlayerOwner, _slot.ItemId, _slot.Capacity, _slot.IconName, 
                _slot.Description, _slot.Characteristics, _slot.Weight, _slot.ClassItem, _slot.Title, 
                _slot.Specialization, _slot.Capacity);
        }
        
        private void OnDeleteButtonClicked()
        {
            _inventoryService.RemoveItemFromInventory(PlayerOwner, _slot.ItemId, _slot.Amount);
        }

        public void Dispose()
        {
            switch (_slot.ClassItem)
            {
                case Armor:
                    _panelDescriptionView.UseButton.onClick.RemoveListener(OnEquipButtonClicked);
                    break;
                case Medicine:
                    _panelDescriptionView.UseButton.onClick.RemoveListener(OnHealButtonClicked);
                    break;
                case Ammo:
                    _panelDescriptionView.UseButton.onClick.RemoveListener(OnBuyButtonClicked);
                    break;
            }


            _panelDescriptionView.CloseButton.onClick.RemoveListener(Dispose);
            _panelDescriptionView.DeleteButton.onClick.RemoveListener(OnDeleteButtonClicked);
        }
    }
}