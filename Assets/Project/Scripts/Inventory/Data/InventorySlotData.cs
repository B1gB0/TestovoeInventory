using System;

namespace Project.Scripts.Inventory.Data
{
    [Serializable]
    public class InventorySlotData
    {
        public string ItemId;
        public int Amount;
        public int Capacity;
        public string IconName;
        public string Description;
    }
}
