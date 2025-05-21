public class AssaultRifle : Weapon
{
    private void Awake()
    {
        weaponName = "Assault Rifle";
        maxAmmo = 30;
        currentAmmo = 30;
        reserveAmmo = 90;
        fireRate = 0.1f;
        damage = 10f;
    }
}
