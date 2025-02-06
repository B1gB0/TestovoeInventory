using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Inventory.Data
{
    [CreateAssetMenu(menuName = "IconsOfItemsDictionaryData")]
    public class IconsListImageData : ScriptableObject
    {
        [field: SerializeField] public List<Sprite> Icons { get; private set; }
        [field: SerializeField] public List<string> Keys { get; private set; }
    }
}