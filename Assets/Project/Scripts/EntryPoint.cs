using Project.Scripts.Inventory;
using Project.Scripts.Inventory.Controllers;
using Project.Scripts.Inventory.Data;
using Project.Scripts.Inventory.View;
using Project.Scripts.Storage;
using Random = UnityEngine.Random;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        private const string PlayerOwner = "Player";
        private const string Gun = nameof(Gun);
        private const string MachineGun = nameof(MachineGun);

        private readonly JsonToFileStorageService _storageService = new();
        private readonly IconsOfItemsDictionaryData _iconsOfItemsDictionaryData = new ();

        [SerializeField] private PanelDescriptionView _panelDescriptionView;
        [SerializeField] private ScreenView _screenView;
        [SerializeField] private IconsListImageData _iconsListSprites;
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private Player.Player _player;

        [SerializeField] private Button _gunButton;
        [SerializeField] private Button _machineGunButton;
        [SerializeField] private Button _fireButton;

        private InventoryService _inventoryService;
        private ScreenController _screenController;
        private GameStateProvider _gameStateProvider;
        private string _currentOwnerId;

        private void OnEnable()
        {
            //_fireButton.onClick.AddListener(_player.CurrentWeapon.Shoot);
            _gunButton.onClick.AddListener(OnSetGun);
            _machineGunButton.onClick.AddListener(OnSetMachineGun);
        }

        private void OnDisable()
        {
            _gunButton.onClick.RemoveListener(OnSetGun);
            _machineGunButton.onClick.RemoveListener(OnSetMachineGun);
        }

        private void Start()
        {
            _iconsOfItemsDictionaryData.GetDataOfSprites(_iconsListSprites.Keys, _iconsListSprites.Icons);

            _gameStateProvider = new GameStateProvider(_storageService);
            
            _gameStateProvider.LoadGameState();

            _inventoryService = new InventoryService(_gameStateProvider);
            
            PanelDescriptionController panelDescriptionController =
                new PanelDescriptionController(_panelDescriptionView, _iconsOfItemsDictionaryData, _inventoryService);

            _inputHandler.GetPanelDescriptionController(panelDescriptionController);
            
            var gameState = _gameStateProvider.GameState;
            
            _player.GetDataFromGameState(gameState.PlayerData);

            foreach (var inventoryData in gameState.Inventories)
            {
                _inventoryService.RegisterInventory(inventoryData);
            }
            
            _screenController = new ScreenController(_inventoryService, _screenView, _iconsOfItemsDictionaryData);
            _screenController.OpenInventory(PlayerOwner);
            _currentOwnerId = PlayerOwner;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                var randomIndex = Random.Range(0, _gameStateProvider.GameSettings.Items.Count);
                var itemSlotCapacity = _gameStateProvider.GameSettings.Items[randomIndex].CellCapacity;
                var randomItemId = _gameStateProvider.GameSettings.Items[randomIndex].Id;
                var randomIconName = _gameStateProvider.GameSettings.Items[randomIndex].IconName;
                var randomDescription = _gameStateProvider.GameSettings.Items[randomIndex].Description;
                var randomItemCharacteristics = _gameStateProvider.GameSettings.Items[randomIndex].ItemCharacteristics;
                var randomItemWeight = _gameStateProvider.GameSettings.Items[randomIndex].Weight;
                var randomItemClassItem = _gameStateProvider.GameSettings.Items[randomIndex].ClassItem;
                var randomItemTitle = _gameStateProvider.GameSettings.Items[randomIndex].Title;
                var randomItemSpecialization = _gameStateProvider.GameSettings.Items[randomIndex].Specialization;
                var randomAmount = Random.Range(1, itemSlotCapacity);
                
                Debug.Log(randomItemSpecialization);
                
                var result = _inventoryService.AddItemsToInventory(_currentOwnerId, randomItemId, itemSlotCapacity,
                    randomIconName, randomDescription, randomItemCharacteristics, randomItemWeight,
                    randomItemClassItem, randomItemTitle, randomItemSpecialization, randomAmount);
                
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

        private void OnSetGun()
        {
            _player.SetCurrentWeapon(Gun);
        }
        
        private void OnSetMachineGun()
        {
            _player.SetCurrentWeapon(MachineGun);
        }
    }
}