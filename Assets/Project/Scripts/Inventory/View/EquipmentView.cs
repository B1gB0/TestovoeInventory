using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Inventory.View
{
    public class EquipmentView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _armorHeadText;
        [SerializeField] private TMP_Text _armorBodyText;
        
        [SerializeField] private Image _headItemIcon;
        [SerializeField] private Image _bodyItemIcon;

        public void SetHeadArmor(int armorHead, Sprite headItem)
        {
            _headItemIcon.gameObject.SetActive(true);
            _armorHeadText.text = armorHead.ToString();
            _headItemIcon.sprite = headItem;
        }

        public void SetBodyArmor(int armorBody, Sprite bodyItem)
        {
            _bodyItemIcon.gameObject.SetActive(true);
            _armorBodyText.text = armorBody.ToString();
            _bodyItemIcon.sprite = bodyItem;
        }
    }
}