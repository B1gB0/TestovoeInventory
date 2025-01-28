namespace Project.Scripts.Weapons
{
    public class MachineGun : Weapon
    {
        public override void Shoot(Health.Health health, int damage)
        {
            health.TakeDamage(damage);
        }
    }
}