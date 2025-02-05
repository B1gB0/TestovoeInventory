using System;

namespace Project.Scripts.Health
{
    public class HealthBar : Bar, IDisposable
    {
        private Health _health;

        public void Construct(Health health)
        {
            _health = health;
            _health.HealthChanged += OnChangedValues;
            
            Show();
        }

        private void OnChangedValues(float currentHealth, float maxHealth, float targetHealth)
        {
            SetValues(currentHealth, maxHealth, targetHealth);
        }

        public void Dispose()
        {
            _health.HealthChanged -= OnChangedValues;
        }
    }
}