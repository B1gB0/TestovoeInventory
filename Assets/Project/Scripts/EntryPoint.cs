using Project.Scripts.Health;
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
        
        private const string GunBullets = "GunBullets";
        private const string MachineGunBullets = "MachineGunBullets";
        private const string Gun = nameof(Gun);
        private const string MachineGun = nameof(MachineGun);

        private const int CountBulletsForGunShot = 1;
        private const int CountBulletsForMachineGunShot = 3;

        private readonly JsonToFileStorageService _storageService = new();
        private readonly IconsOfItemsDictionaryData _iconsOfItemsDictionaryData = new ();

        [SerializeField] private ViewFactory _viewFactory;
        [SerializeField] private PanelDescriptionView _panelDescriptionView;
        [SerializeField] private EquipmentView _equipmentView;
        [SerializeField] private ScreenView _screenView;
        [SerializeField] private GameOverView _gameOverView;

        [SerializeField] private IconsListImageData _iconsListSprites;
        [SerializeField] private InputHandler _inputHandler;
        
        [SerializeField] private Player.Player _player;
        [SerializeField] private Enemy.Enemy _enemy;

        [SerializeField] private Button _rebornButton;
        [SerializeField] private Button _gunButton;
        [SerializeField] private Button _machineGunButton;
        [SerializeField] private Button _fireButton;

        private InventoryService _inventoryService;
        private ScreenController _screenController;
        private PanelDescriptionController _panelDescriptionController;
        private GameStateProvider _gameStateProvider;
        private HealthBar _healthBarEnemy;
        private HealthBar _healthBarPlayer;
        private string _currentOwnerId;

        private void OnEnable()
        {
            _rebornButton.onClick.AddListener(_player.SetMaxHealth);
            _rebornButton.onClick.AddListener(_gameOverView.Hide);
            _fireButton.onClick.AddListener(OnFire);
            _gunButton.onClick.AddListener(OnSetGun);
            _machineGunButton.onClick.AddListener(OnSetMachineGun);
        }

        private void OnDisable()
        {
            _rebornButton.onClick.RemoveListener(_player.SetMaxHealth);
            _rebornButton.onClick.RemoveListener(_gameOverView.Hide);
            _gunButton.onClick.RemoveListener(OnSetGun);
            _machineGunButton.onClick.RemoveListener(OnSetMachineGun);
            _panelDescriptionController.Dispose();
        }

        private void Start() 
        {
            _gameStateProvider = new GameStateProvider(_storageService);
            _gameStateProvider.LoadGameState();

            var gameState = _gameStateProvider.GameState;

            _inventoryService = new InventoryService(_gameStateProvider);
            
            _iconsOfItemsDictionaryData.GetDataOfSprites(_iconsListSprites.Keys, _iconsListSprites.Icons);

            Equipment equipment = new Equipment(gameState.EquipmentData);
            
            _panelDescriptionController = new PanelDescriptionController(_panelDescriptionView,
                _iconsOfItemsDictionaryData, _inventoryService, _player, _equipmentView, equipment, _gameStateProvider);

            _inputHandler.GetPanelDescriptionController(_panelDescriptionController);

            _player.GetDataFromGameState(gameState.PlayerData);
            _enemy.GetDataFromGameState(gameState.EnemyData);

            foreach (var inventoryData in gameState.Inventories)
            {
                _inventoryService.RegisterInventory(inventoryData);
            }
            
            _screenController = new ScreenController(_inventoryService, _screenView, _iconsOfItemsDictionaryData);
            _screenController.OpenInventory(PlayerOwner);
            _currentOwnerId = PlayerOwner;

            _healthBarPlayer = _viewFactory.CreatePlayerHealthBar(_player.Health);
            _healthBarEnemy = _viewFactory.CreateEnemyHealthBar(_enemy.Health);
            
            _healthBarPlayer.transform.SetParent(_screenView.transform, false);
            _healthBarEnemy.transform.SetParent(_screenView.transform, false);

            _enemy.Health.Die += GetRandomItem;
            _player.Health.Die += _gameOverView.Show;
        }

        private void OnFire()
        {
            if (_player.CurrentWeapon.Type == Weapons.Weapons.Gun)
            {
                var result = _inventoryService.RemoveItemFromInventory(_currentOwnerId, GunBullets, 
                    CountBulletsForGunShot);

                if (result.Success)
                {
                    _player.Fire(_enemy);
                    _enemy.AttackPlayer(_player);
                }
            }
            else if (_player.CurrentWeapon.Type == Weapons.Weapons.MachineGun)
            {
                var result = _inventoryService.RemoveItemFromInventory(_currentOwnerId, MachineGunBullets, 
                    CountBulletsForMachineGunShot);

                if (result.Success)
                {
                    _player.Fire(_enemy);
                    _enemy.AttackPlayer(_player);
                }
            }
        }

        private void GetRandomItem()
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

        private void OnSetGun()
        {
            _player.SetCurrentWeapon(Gun);
        }
        
        private void OnSetMachineGun()
        {
            _player.SetCurrentWeapon(MachineGun);
        }

        private void OnDestroy()
        {
            _enemy.Health.Die -= GetRandomItem;
            _player.Health.Die -= _gameOverView.Show;
            _gameStateProvider.SaveGameState();
            _healthBarPlayer.Dispose();
            _healthBarEnemy.Dispose();
        }
    }
}