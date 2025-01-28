namespace Project.Scripts.Inventory
{
    public struct RemoveItemsFromInventoryGridResult
    {
        public readonly string InventoryOwnerId;
        public readonly int ItemsRemovedAmount;
        public readonly bool Success;

        public RemoveItemsFromInventoryGridResult(string inventoryOwnerId, int itemsRemovedAmount, bool success)
        {
            InventoryOwnerId = inventoryOwnerId;
            ItemsRemovedAmount = itemsRemovedAmount;
            Success = success;
        }
    }
}