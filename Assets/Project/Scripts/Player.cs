using System.Collections.Generic;
using Project.Scripts.Inventory.Data;
using Project.Scripts.Weapons;
using UnityEngine;

namespace Project.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        private const string Gun = "Gun";
        private const float MaxHealth = 100;

        private readonly Dictionary<string, Weapon> _weapons = new()
        {
            ["Gun"] = new Gun(Weapons.Weapons.Gun),
            ["MachineGun"] = new MachineGun(Weapons.Weapons.MachineGun)
        };
        
        private PlayerData _data;
        private int _counterShots;
        
        public int HeadArmor { get; private set; }
        public int BodyArmor { get; private set; }
        
        [field: SerializeField] public Health.Health Health { get; private set; }
        
        public Weapon CurrentWeapon { get; private set; }

        private void Start()
        {
            Health.TargetHealthChanged += GetHealthInData;
            
            SetCurrentWeapon(Gun);
        }
        public void SetMaxHealth()
        {
            GetHealthInData(MaxHealth);
        }

        public void Fire(Enemy.Enemy enemy)
        {
            CurrentWeapon.Shoot(enemy.Health);
        }

        public void SetCurrentWeapon(string key)
        {
            CurrentWeapon = _weapons[key];
        }

        public void GetDataFromGameState(PlayerData data)
        {
            _data = data;

            HeadArmor = data.HeadArmor;
            BodyArmor = data.HeadArmor;
            
            if (_data.Health <= 0)
            {
                GetHealthInData(MaxHealth);
            }

            if (_data.Health != Health.TargetHealth)
            {
                Health.SetHealthValue(_data.Health);
            }
        }
        
        public void SetHeadArmor(int headArmor)
        {
            _data.HeadArmor = headArmor;
            HeadArmor = headArmor;
        }

        public void SetBodyArmor(int bodyArmor)
        {
            _data.BodyArmor = bodyArmor;
            BodyArmor = bodyArmor;
        }

        private void GetHealthInData(float health)
        {
            _data.Health = health;
            Health.SetHealthValue(health);
        }

        private void OnDestroy()
        {
            Health.TargetHealthChanged -= GetHealthInData;
        }
    }
}
