using TMPro;
using UnityEngine;

namespace Project.Scripts.Inventory.View
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private InventorySlotView[] _slots;
        [SerializeField] private TMP_Text _textOwner;

        public void SetOwnerId(string ownerId)
        {
            _textOwner.text = ownerId;
        }

        public InventorySlotView GetInventorySlotView(int index)
        {
            return _slots[index];
        }
    }
}