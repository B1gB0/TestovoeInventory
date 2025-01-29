using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Inventory.Data
{
    public class IconsOfItemsDictionaryData
    {
        public Dictionary<string, Sprite> Icons { get; private set; } = new();

        public void GetDataOfSprites(List<string> keys, List<Sprite> icons)
        {
            for (int i = 0; i < icons.Count; i++)
            {
                Icons.Add(keys[i], icons[i]);
            }
        }
    }
}