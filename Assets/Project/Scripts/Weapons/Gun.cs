namespace Project.Scripts.Weapons
{
    public class Gun : Weapon
    {
        public override void Shoot(Health.Health health, int damage, int armor)
        {
            health.TakeDamage(damage - armor);
        }
    }
}