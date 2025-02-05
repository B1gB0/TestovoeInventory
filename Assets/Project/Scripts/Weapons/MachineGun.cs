namespace Project.Scripts.Weapons
{
    public class MachineGun : Weapon
    {
        private const int Damage = 9;
        
        public MachineGun(Weapons type) : base(type) { }
        
        public override void Shoot(Health.Health health)
        {
            health.TakeDamage(Damage);
        }

    }
}