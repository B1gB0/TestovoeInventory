using Project.Scripts.Health;
using UnityEngine;

namespace Project.Scripts
{
    public class ViewFactory : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBarPlayerTemplate;
        [SerializeField] private HealthBar _healthBarEnemyTemplate;
        
        public HealthBar CreatePlayerHealthBar(Health.Health health)
        {
            HealthBar healthBar = Instantiate(_healthBarPlayerTemplate);
            healthBar.Construct(health);
            return healthBar;
        }
        
        public HealthBar CreateEnemyHealthBar(Health.Health health)
        {
            HealthBar healthBar = Instantiate(_healthBarEnemyTemplate);
            healthBar.Construct(health);
            return healthBar;
        }
    }
}
