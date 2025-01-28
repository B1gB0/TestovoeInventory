namespace Project.Scripts.Weapons
{
    public class Gun : Weapon
    {
        public override void Shoot(Health.Health health, int damage)
        {
            health.TakeDamage(damage);
        }
    }
}