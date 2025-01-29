using TMPro;
using UnityEngine;

namespace Project.Scripts.Inventory.View
{
    public class EquipmentView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _armorHeadText;
        [SerializeField] private TMP_Text _armorBodyText;

        public void SetData(string armorHeadText, string armorBodyText)
        {
            _armorHeadText.text = armorHeadText;
            _armorBodyText.text = armorBodyText;
        }
    }
}