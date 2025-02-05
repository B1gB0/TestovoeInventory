using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Scripts.Inventory.View
{
    public class ItemView : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private Canvas _canvas;
        private CanvasGroup _canvasGroup;
        
        public bool IsDragging { get; private set; }
        
        // private void Start()
        // {
        //     _rectTransform = GetComponent<RectTransform>();
        //     _canvas = GetComponentInParent<Canvas>();
        //     _canvasGroup = GetComponent<CanvasGroup>();
        // }
        
        // public void OnBeginDrag(PointerEventData eventData)
        // {
        //     IsDragging = true;
        //     
        //     var slotTransform = _rectTransform.parent;
        //     slotTransform.SetAsLastSibling();
        //     _canvasGroup.blocksRaycasts = false;
        // }
        //
        // public void OnDrag(PointerEventData eventData)
        // {
        //     _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        // }
        //
        // public void OnEndDrag(PointerEventData eventData)
        // {
        //     IsDragging = false;
        //     
        //     transform.localPosition = Vector3.zero;
        //     _canvasGroup.blocksRaycasts = true;
        // }    
    }
}