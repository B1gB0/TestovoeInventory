using UnityEngine;

namespace Project.Scripts.Weapons
{
    public abstract class Weapon
    {
        public abstract void Shoot(Health.Health health, int damage, int armor);
    }
}