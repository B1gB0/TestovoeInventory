using System;
using Project.Scripts.Inventory.ReadOnly;
using UnityEngine;

namespace Project.Scripts.ReadOnly
{
    public interface IReadOnlyInventoryGrid : IReadOnlyInventory
    {
        public event Action<Vector2Int> SizeChanged;
        
        public Vector2Int Size { get; }

        public IReadOnlyInventorySlot[,] GetSlots();
    }
}