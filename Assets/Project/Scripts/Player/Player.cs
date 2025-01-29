using System;
using System.Collections.Generic;
using Project.Scripts.Health;
using Project.Scripts.Inventory.Data;
using Project.Scripts.Weapons;
using UnityEngine;

namespace Project.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        private const string Gun = "Gun";

        private readonly Dictionary<string, Weapon> _weapons = new()
        {
            ["Gun"] = new Gun(),
            ["MachineGun"] = new MachineGun()
        };
        
        [SerializeField] private Health.Health _health;
        [SerializeField] private HealthBar _healthBar;

        private IGameStateSaver _saver;
        private PlayerData _data;
        private int _headArmor;
        private int _bodyArmor;
        
        public Weapon CurrentWeapon { get; private set; }

        private void OnEnable()
        {
            _health.TargetHealthChanged += GetHealthInData;
        }

        private void Start()
        {
            _healthBar.GetHealth(_health);
            SetCurrentWeapon(Gun);
        }

        public void SetCurrentWeapon(string key)
        {
            CurrentWeapon = _weapons[key];
        }
        
        public void GetGameStateProvider(IGameStateSaver saver)
        {
            _saver = saver;
        }

        public void GetDataFromGameState(PlayerData data)
        {
            _data = data;

            //_headArmor = _data.headArmor;
            //_bodyArmor = _data.bodyArmor;
            //_health.SetHealthValue(_data.health);
        }

        public void SetArmor()
        {
            
        }

        private void GetHealthInData(float health)
        {
            //_data.health = health;
            //_saver.SaveGameState();
        }
    }
}
