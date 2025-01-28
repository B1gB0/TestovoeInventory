using Project.Scripts.Inventory.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Inventory.View
{
    public class InventorySlotView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _amountText;
        [SerializeField] private Image _icon;
        
        private IconsDictionaryData _iconsData;
        
        public string ItemDescription { get; private set; }

        public Sprite Sprite => _icon.sprite;

        public void GetData(IconsDictionaryData iconsData)
        {
            _iconsData = iconsData;
        }

        public void SetTitle(string title)
        {
            _titleText.text = title;
        }

        public void SetAmount(int amount)
        {
            _amountText.text = amount == 0 ? "" : amount.ToString();
        }

        public void SetIcon(string iconName)
        {
            _icon.sprite = _iconsData.Icons[iconName];
            _icon.gameObject.SetActive(true);
        }

        public void SetDescription(string description)
        {
            ItemDescription = description;
        }

        public void HideIcon()
        {
            _icon.gameObject.SetActive(false);
        }
    }
}