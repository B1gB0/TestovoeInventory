using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Inventory.View
{
    public class PanelDescription : MonoBehaviour, IView
    {
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private Image _icon;

        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _useButton;
        [SerializeField] private Button _deleteButton;
        
        private void OnEnable()
        {
            _closeButton.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(Hide);
        }
        
        public void Show(string description, Sprite sprite)
        {
            _icon.sprite = sprite;
            _descriptionText.text = description;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
