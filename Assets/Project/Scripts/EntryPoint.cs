using System;
using Project.Scripts.Inventory;
using Project.Scripts.Inventory.Controllers;
using Project.Scripts.Inventory.Data;
using Project.Scripts.Inventory.View;
using Project.Scripts.Storage;
using Random = UnityEngine.Random;
using UnityEngine;

namespace Project.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        private const string PlayerOwner = "Player";

        private readonly JsonToFileStorageService _storageService = new();
        private readonly IconsDictionaryData _iconsDictionaryData = new ();

        [SerializeField] private ScreenView _screenView;
        [SerializeField] private IconsListImageData _iconsListSprites;

        private InventoryService _inventoryService;
        private ScreenController _screenController;
        private GameStateProvider _gameStateProvider;
        private string _currentOwnerId;

        private void Start()
        {
            _iconsDictionaryData.GetData(_iconsListSprites.Keys, _iconsListSprites.Icons);
            _gameStateProvider = new GameStateProvider(_storageService);
            
            _gameStateProvider.LoadGameState();

            _inventoryService = new InventoryService(_gameStateProvider);

            var gameState = _gameStateProvider.GameState;

            foreach (var inventoryData in gameState.Inventories)
            {
                _inventoryService.RegisterInventory(inventoryData);
            }
            
            _screenController = new ScreenController(_inventoryService, _screenView, _iconsDictionaryData);
            _screenController.OpenInventory(PlayerOwner);
            _currentOwnerId = PlayerOwner;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _screenController.OpenInventory(PlayerOwner);
                _currentOwnerId = PlayerOwner;
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                var randomIndex = Random.Range(0, _gameStateProvider.GameSettings.Items.Count);
                var itemSlotCapacity = _gameStateProvider.GameSettings.Items[randomIndex].CellCapacity;
                var randomItemId = _gameStateProvider.GameSettings.Items[randomIndex].Id;
                var randomIconName = _gameStateProvider.GameSettings.Items[randomIndex].IconName;
                var randomDescription = _gameStateProvider.GameSettings.Items[randomIndex].Description;
                var randomAmount = Random.Range(1, itemSlotCapacity);
                var result = _inventoryService.AddItemsToInventory(_currentOwnerId, randomItemId, itemSlotCapacity,
                    randomIconName, randomDescription, randomAmount);
                
                Debug.Log($"Item added {randomItemId}. Amount {result.ItemsAddedAmount}");
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                var randomIndex = Random.Range(0, _gameStateProvider.GameSettings.Items.Count);
                var randomItemId = _gameStateProvider.GameSettings.Items[randomIndex].Id;
                var randomAmount = Random.Range(1, 50);
                var result = _inventoryService.RemoveItemFromInventory(_currentOwnerId, randomItemId, randomAmount);
                
                Debug.Log($"Item removed {randomItemId}. Amount {result.ItemsRemovedAmount}, Success = {result.Success}");
            }
        }
    }
}