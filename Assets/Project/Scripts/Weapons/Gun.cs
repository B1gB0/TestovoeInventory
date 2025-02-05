namespace Project.Scripts.Weapons
{
    public class Gun : Weapon
    {
        private const int Damage = 5;
        
        public Gun(Weapons type) : base(type) { }
        
        public override void Shoot(Health.Health health)
        {
            health.TakeDamage(Damage);
        }
    }
}