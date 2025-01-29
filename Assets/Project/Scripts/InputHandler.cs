using Project.Scripts.Inventory.Controllers;
using Project.Scripts.Inventory.View;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Scripts
{
    public class InputHandler : MonoBehaviour
    {
        private PanelDescriptionController _panelDescriptionController;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void GetPanelDescriptionController(PanelDescriptionController panelDescriptionController)
        {
            _panelDescriptionController = panelDescriptionController;
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            if(!context.started) return;

            var rayCastHit = Physics2D.GetRayIntersection(_camera.ScreenPointToRay(
                Mouse.current.position.ReadValue()));

            if(!rayCastHit.collider) return;
            
            Debug.Log(rayCastHit.collider);

            if(rayCastHit.collider.TryGetComponent(out InventorySlotView slot))
            {
                _panelDescriptionController.OnShowView(slot);
            }
        }
    }
}