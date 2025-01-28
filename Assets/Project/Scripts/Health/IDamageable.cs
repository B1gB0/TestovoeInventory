using System;

namespace Project.Scripts.Health
{
    public interface IDamageable
    {
        event Action Die;

        void TakeDamage(float damage);
    }
}