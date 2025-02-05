﻿using Project.Scripts.Inventory.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Scripts.Inventory.View
{
    public class InventorySlotView : MonoBehaviour, IDropHandler
    {
        private const int Null = 0;
        private const int MinValue = 1;

        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _amountText;
        [SerializeField] private Image _icon;

        private IconsOfItemsDictionaryData _iconsOfItemsData;
        private InventoryService _inventoryService;

        public string Description { get; private set; }
        public string ItemId { get; private set; }
        public string Title { get; private set; }
        public string ClassItem { get; private set; }
        public int Characteristics { get; private set; }
        public float Weight { get; private set; }
        public int Capacity { get; private set; }
        public int Amount { get; private set; }
        public string Specialization { get; private set; }
        public string IconName { get; private set; }
        public Vector2Int Position { get; private set; }
        public Sprite Sprite => _icon.sprite;

        public void OnDrop(PointerEventData eventData)
        {
            var otherItemTransform = eventData.pointerDrag.transform;
            
            ItemView item = eventData.pointerDrag.GetComponent<ItemView>();
            item.GetInventoryService(_inventoryService);
            item.GetSecondSlotPosition(Position);
            
            otherItemTransform.localPosition = Vector3.zero;
        }

        public void GetData(IconsOfItemsDictionaryData iconsOfItemsData, InventoryService inventoryService)
        {
            _iconsOfItemsData = iconsOfItemsData;
            _inventoryService = inventoryService;
        }

        public void SetId(string id)
        {
            ItemId = id;
            _titleText.text = id;
        }

        public void SetAmount(int amount)
        {
            Amount = amount;
            _amountText.text = amount is Null or MinValue ? "" : amount.ToString();
        }

        public void SetIcon(string iconName)
        {
            IconName = iconName;
            _icon.sprite = _iconsOfItemsData.Icons[iconName];
            _icon.gameObject.SetActive(true);
        }

        public void SetDescriptionData(string description, int itemValue, float itemWeight, string classItem, 
            string title, string specialization)
        {
            Description = description;
            Characteristics = itemValue;
            Weight = itemWeight;
            ClassItem = classItem;
            Title = title;
            Specialization = specialization;
        }

        public void SetCapacity(int capacity)
        {
            Capacity = capacity;
        }

        public void HideIcon()
        {
            _icon.gameObject.SetActive(false);
        }

        public void SetPosition(Vector2Int position)
        {
            Position = position;
        }
    }
}