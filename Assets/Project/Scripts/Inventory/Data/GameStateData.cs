using System.Collections.Generic;
using Project.Scripts.GoogleImporter;

namespace Project.Scripts.Inventory.Data
{
    public class GameStateData
    {
        public List<InventoryGridData> Inventories;
        public EquipmentData EquipmentData;
        public PlayerData PlayerData;
        public EnemyData EnemyData;
    }
}