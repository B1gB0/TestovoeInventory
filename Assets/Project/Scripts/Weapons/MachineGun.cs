namespace Project.Scripts.Weapons
{
    public class MachineGun : Weapon
    {
        public override void Shoot(Health.Health health, int damage, int armor)
        {
            health.TakeDamage(damage - armor);
        }
    }
}