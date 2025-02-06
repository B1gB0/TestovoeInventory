using Project.Scripts.Inventory.Controllers;
using Project.Scripts.Inventory.View;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Scripts
{
    public class InputHandler : MonoBehaviour, IPointerClickHandler
    {
        private PanelDescriptionController _panelDescriptionController;

        public void GetPanelDescriptionController(PanelDescriptionController panelDescriptionController)
        {
            _panelDescriptionController = panelDescriptionController;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            InventorySlotView slot = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<InventorySlotView>();

            if (slot != null)
            {
                _panelDescriptionController.OnShowView(slot);
            }
        }
    }
}