using System;
using UnityEngine;

namespace Project.Scripts.Inventory.ReadOnly
{
    public interface IReadOnlyInventoryGrid : IReadOnlyInventory
    {
        public event Action<Vector2Int> SizeChanged;
        
        public Vector2Int Size { get; }

        public IReadOnlyInventorySlot[,] GetSlots();
    }
}