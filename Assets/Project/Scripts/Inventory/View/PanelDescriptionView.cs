using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Inventory.View
{
    public class PanelDescriptionView : MonoBehaviour, IView
    {
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private TMP_Text _characteristicsText;
        [SerializeField] private TMP_Text _weight;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Text _useButtonText;

        [SerializeField] private Image _iconItem;
        [SerializeField] private Image _iconCharacteristics;

        [field: SerializeField] public Button CloseButton { get; private set; }
        [field: SerializeField] public Button UseButton { get; private set; }
        [field: SerializeField] public Button DeleteButton { get; private set; }

        private void OnEnable()
        {
            CloseButton.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            CloseButton.onClick.RemoveListener(Hide);
        }

        public void Show(InventorySlotView slot, Sprite characteristicsSprite, string useButtonText)
        {
            SetData(slot.Description, slot.Sprite, slot.Characteristics, slot.Weight, characteristicsSprite,
                slot.Title, useButtonText);
            
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void SetData(string description, Sprite itemSprite, int characteristics, float weight,
            Sprite characteristicsSprite, string title, string useButtonText)
        {
            _iconItem.sprite = itemSprite;
            _iconCharacteristics.sprite = characteristicsSprite;
            _descriptionText.text = description;
            _characteristicsText.text = "+" + characteristics;
            _weight.text = weight.ToString();
            _title.text = title;
            _useButtonText.text = useButtonText;
        }
    }
}
