using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Inventory.View
{
    public class PanelDescriptionView : MonoBehaviour, IView
    {
        private const string PlayerOwner = "Player";
        
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private TMP_Text _characteristicsText;
        [SerializeField] private TMP_Text _weight;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Text _useButtonText;

        [SerializeField] private Image _iconItem;
        [SerializeField] private Image _iconCharacteristics;

        [SerializeField] private Button _closeButton;
        
        [field: SerializeField] public Button _useButton { get; private set; }
        [field: SerializeField] public Button _deleteButton { get; private set; }

        private InventoryService _inventoryService;
        private InventorySlotView _slot;

        private void OnEnable()
        {
            _deleteButton.onClick.AddListener(OnDeleteButtonClicked);
            _closeButton.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            _deleteButton.onClick.RemoveListener(OnDeleteButtonClicked);
            _closeButton.onClick.RemoveListener(Hide);
        }

        public void GetInventoryService(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public void Show(InventorySlotView slot, Sprite characteristicsSprite, string useButtonText)
        {
            _slot = slot;
            
            SetData(slot.Description, slot.Sprite, slot.Characteristics, slot.Weight, characteristicsSprite,
                slot.Title, useButtonText);
            
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnDeleteButtonClicked()
        {
            Debug.Log(_slot.Amount);
            _inventoryService.RemoveItemFromInventory(PlayerOwner, _slot.ItemId, _slot.Amount);
        }

        private void SetData(string description, Sprite itemSprite, string characteristics, string weight,
            Sprite characteristicsSprite, string title, string useButtonText)
        {
            _iconItem.sprite = itemSprite;
            _iconCharacteristics.sprite = characteristicsSprite;
            _descriptionText.text = description;
            _characteristicsText.text = "+" + characteristics;
            _weight.text = weight;
            _title.text = title;
            _useButtonText.text = useButtonText;
        }
    }
}
