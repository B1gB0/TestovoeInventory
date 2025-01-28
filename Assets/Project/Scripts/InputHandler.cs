using Project.Scripts.Inventory.View;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Scripts
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private PanelDescription _panelDescription;
        
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            Debug.Log("ЛКМ?");
            if(!context.started) return;
            
            Debug.Log("лкм");

            var rayCastHit = Physics2D.GetRayIntersection(_camera.ScreenPointToRay(
                Mouse.current.position.ReadValue()));
            
            Debug.Log("до столкновения");
            
            if(!rayCastHit.collider) return;
            
            Debug.Log(rayCastHit.collider);
            Debug.Log("столкновение");
            
            if(rayCastHit.collider.TryGetComponent(out InventorySlotView slot))
            {
                _panelDescription.Show(slot.ItemDescription, slot.Sprite);
            }
        }
    }
}