using UnityEngine;

namespace Project.Scripts.Weapons
{
    public abstract class Weapon
    {
        public Weapon(Weapons type)
        {
            Type = type;
        }
        
        public Weapons Type { get; }
        
        public abstract void Shoot(Health.Health health);
    }
}