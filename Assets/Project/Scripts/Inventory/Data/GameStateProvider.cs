using System.Collections.Generic;
using Project.Scripts.GoogleImporter;
using Project.Scripts.Storage;
using UnityEngine;

namespace Project.Scripts.Inventory.Data
{
    public class GameStateProvider : IGameStateProvider, IGameStateSaver
    {
        private const string GameStateKey = "GameState";
        private const string GameSettingsKey = "GameSettings";

        private const int Columns = 6;
        private const int Rows = 5;
        
        private readonly IStorageService _storageService;

        public GameStateProvider(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public GameStateData GameState { get; private set; }
        public GameSettings GameSettings { get; private set; }
        public Dictionary<string, ItemSettings> Items { get; private set; } = new();

        public void SaveGameState()
        {
            _storageService.Save(GameStateKey, Application.persistentDataPath, GameState);
        }

        public void LoadGameState()
        {
            _storageService.Load<GameStateData>(GameStateKey, Application.persistentDataPath, data =>
            {
                GameState = data;
            });
            
            _storageService.Load<GameSettings>(GameSettingsKey, Application.persistentDataPath, data =>
            {
                GameSettings = data;

                if (GameSettings != null)
                {
                    Debug.Log(GameSettings != null);
                }
            });

            if (GameState != null)
                return;

            GameState = InitFromSettings();
            SaveGameState();
        }

        private GameStateData InitFromSettings()
        {
            var gameState = new GameStateData
            {
                Inventories = new List<InventoryGridData>
                {
                    CreateTestInventory("Player")
                },
                
                EquipmentData = new EquipmentData(),
                PlayerData = new PlayerData(),
                EnemyData = new EnemyData()
            };

            return gameState;
        }
        
        private InventoryGridData CreateTestInventory(string ownerId)
        {
            var size = new Vector2Int(Columns, Rows);
            var createdInventorySlots = new List<InventorySlotData>();
            var length = size.x * size.y;

            var items = CreateAllItems();
            
            for (int i = 0; i < items.Count; i++)
            {
                createdInventorySlots.Add(items[i]);
            }

            for (int i = items.Count; i < length; i++)
            {
                createdInventorySlots.Add(new InventorySlotData());
            }

            var createdInventoryData = new InventoryGridData()
            {
                OwnerId = ownerId,
                Size = size,
                Slots = createdInventorySlots
            };

            return createdInventoryData;
        }

        public void GetItemsFromGameSettings()
        {
            for (int i = 0; i < GameSettings.Items.Count; i++)
            {
                Debug.Log(GameSettings != null);
                Items.Add(GameSettings.Items[i].Id, GameSettings.Items[i]);
            }
        }

        private List<InventorySlotData> CreateAllItems()
        {
            List<InventorySlotData> items = new List<InventorySlotData>();
            
             for (int i = 0; i < GameSettings.Items.Count; i++)
             {
                 items.Add(new InventorySlotData()
                 {
                     ItemId = GameSettings.Items[i].Id,
                     Amount = GameSettings.Items[i].CellCapacity,
                     Capacity = GameSettings.Items[i].CellCapacity,
                     IconName = GameSettings.Items[i].IconName,
                     Description = GameSettings.Items[i].Description,
                     ItemCharacteristics = GameSettings.Items[i].ItemCharacteristics,
                     Weight = GameSettings.Items[i].Weight,
                     ClassItem = GameSettings.Items[i].ClassItem,
                     Title = GameSettings.Items[i].Title,
                     Specialization = GameSettings.Items[i].Specialization
                 });
            
                 Debug.Log(GameSettings.Items[i].Specialization);
            }

            return items;
        }
    }
}