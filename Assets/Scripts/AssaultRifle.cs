public class AssaultRifle : Weapon
{
    private void Awake()
    {
        weaponName = "Assault Rifle";
        maxAmmo = 30;
        currentAmmo = 30;
        reserveAmmo = 90;
        fireRate = 0.2f; // faster fire rate for full-auto
        damage = 10f;
        isFullAuto = true; // THIS enables hold-to-shoot
    }
}
