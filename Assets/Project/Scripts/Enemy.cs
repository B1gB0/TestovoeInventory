using UnityEngine;

namespace Project.Scripts
{
    public class Enemy : MonoBehaviour
    {
        private const float MaxHealth = 100;
        private const float damage = 15;
        
        private EnemyData _data;
        private int _counter;

        [field: SerializeField] public Health.Health Health { get; private set; }

        private void Start()
        {
            Health.TargetHealthChanged += GetHealthInData;
            Health.Die += SetMaxHealth;
        }

        public void AttackPlayer(Player player)
        {
            if (_counter == 0)
            {
                player.Health.TakeDamage(damage - player.BodyArmor);
                _counter++;
            }
            else
            {
                player.Health.TakeDamage(damage - player.HeadArmor);
                _counter--;
            }
        }

        public void GetDataFromGameState(EnemyData data)
        {
            _data = data;

            if (_data.Health <= 0)
            {
                _data.Health = MaxHealth;
                Health.SetHealthValue(MaxHealth);
            }
            else
            {
                Health.SetHealthValue(_data.Health);
            }
        }

        private void SetMaxHealth()
        {
            GetHealthInData(MaxHealth);
        }

        private void GetHealthInData(float health)
        {
            _data.Health = health;
            Health.SetHealthValue(health);
        }
        
        private void OnDestroy()
        {
            Health.TargetHealthChanged -= GetHealthInData;
            Health.Die -= SetMaxHealth;
        }
    }
}