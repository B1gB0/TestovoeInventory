using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Inventory.Data
{
    [Serializable]
    public class InventoryGridData
    {
        public string OwnerId;
        public List<InventorySlotData> Slots;
        public Vector2Int Size;
    }
}