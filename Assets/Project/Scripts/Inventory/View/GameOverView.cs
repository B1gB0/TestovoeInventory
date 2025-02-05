using UnityEngine;

namespace Project.Scripts.Inventory.View
{
    public class GameOverView : MonoBehaviour, IView
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}