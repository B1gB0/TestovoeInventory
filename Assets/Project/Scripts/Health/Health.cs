using System;
using System.Collections;
using UnityEngine;

namespace Project.Scripts.Health
{
    public class Health : MonoBehaviour, IDamageable
    {
        private const float RecoveryRate = 10f;
        
        [SerializeField] private float _value;

        private ParticleSystem _hitEffect;
        private Coroutine _coroutine;
        private float _currentHealth;

        public event Action Die;
        public event Action<float, float, float> HealthChanged;
        public event Action<float> TargetHealthChanged;

        public float MaxHealth { get; private set; }
        public float TargetHealth { get; private set; }

        private void Start()
        {
            MaxHealth = _value;
            _currentHealth = _value;
            
            HealthChanged?.Invoke(_currentHealth, TargetHealth, MaxHealth);
        }

        public void TakeDamage(float damage)
        {
            TargetHealth -= damage;
            
            TargetHealthChanged?.Invoke(TargetHealth);

            OnChangeHealth();

            if (TargetHealth < 0f)
                TargetHealth = 0f;

            if (TargetHealth == 0)
                Die?.Invoke();
        }

        public void AddHealth(float healthValue)
        {
            TargetHealth += healthValue;
            
            TargetHealthChanged?.Invoke(TargetHealth);

            OnChangeHealth();

            if (TargetHealth > MaxHealth)
                TargetHealth = MaxHealth;
        }

        public void SetHealthValue(float value)
        {
            TargetHealth = value;

            OnChangeHealth();
        }

        private void OnChangeHealth()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(ChangeHealth());
        }

        private IEnumerator ChangeHealth()
        {
            while (_currentHealth != TargetHealth)
            {
                _currentHealth = Mathf.MoveTowards(_currentHealth, TargetHealth, RecoveryRate * Time.deltaTime);
                HealthChanged?.Invoke(_currentHealth, MaxHealth, TargetHealth);

                yield return null;
            }
        }
    }
}
