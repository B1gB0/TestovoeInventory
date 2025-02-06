using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Scripts.Inventory.View
{
    public class ItemView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private const string PlayerOwner = "Player";
        
        private RectTransform _rectTransform;
        private Canvas _canvas;
        private CanvasGroup _canvasGroup;
        private InventoryService _inventoryService;

        private Vector2Int _firstSlot;
        private Vector2Int _secondSlot;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvas = GetComponentInParent<Canvas>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            InventorySlotView slot = eventData.pointerDrag.GetComponentInParent<InventorySlotView>();
            _firstSlot = slot.Position;
            Debug.Log(_firstSlot);
        
            var slotTransform = _rectTransform.parent;
            slotTransform.SetAsLastSibling();
            _canvasGroup.blocksRaycasts = false;
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            
            transform.localPosition = Vector3.zero;
            _inventoryService.SwitchInventoryItems(PlayerOwner,_firstSlot, _secondSlot);
            _canvasGroup.blocksRaycasts = true;
        }

        public void GetInventoryService(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public void GetSecondSlotPosition(Vector2Int position)
        {
            _secondSlot = position;
        }
    }
}