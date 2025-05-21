public class Pistol : Weapon
{
    private void Awake()
    {
        weaponName = "Pistol";
        maxAmmo = 15;
        currentAmmo = 15;
        reserveAmmo = 45;
        fireRate = 0.5f;
        damage = 20f;
    }
}
