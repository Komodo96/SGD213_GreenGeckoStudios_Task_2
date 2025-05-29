using UnityEngine;

// Abstract base class for all weapon types.
// Handles shooting, ammo, reloading, and basic bullet firing logic.
public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected string weaponName;

    // Max ammo in the magazine
    protected int maxAmmo = 30;

    // Current ammo in the magazine
    protected int currentAmmo;

    // Ammo in reserve
    protected int reserveAmmo = 90;

    // Time between shots
    [SerializeField]
    protected float fireRate = 0.2f;

    // Damage per shot (could be used by bullet)
    [SerializeField]
    protected float damage = 10f;

    // Time to reload
    [SerializeField]
    private float reloadTime = 1.5f;

    // Prefab for the bullet to spawn
    [SerializeField]
    private GameObject bulletPrefab;

    // Force applied to bullet when shot
    [SerializeField]
    private float bulletForce = 500f;

    // Is the weapon currently reloading?
    protected bool isReloading = false;

    // Timer to enforce fire rate
    protected float nextTimeToFire = -1f;

    // Sets the weapon to be Fully Automatic or Semi Auto
    protected bool isFullAuto = false;

    // Position to spawn bullets from
    public Transform firePoint;

    // Optional visual effect when shooting
    public ParticleSystem muzzleFlash;

    // Public getters
    public float Damage => damage;
    public int CurrentAmmo => currentAmmo;
    public int ReserveAmmo => reserveAmmo;
    public string WeaponName => weaponName;

    public bool IsFullAuto => isFullAuto;

    // Fires a bullet if not reloading, enough time has passed, and there is ammo.
    public virtual void Shoot()
    {
        if (isReloading || Time.time < nextTimeToFire || currentAmmo <= 0)
            return;

        // Null checks for safety
        if (firePoint == null)
        {
            Debug.LogWarning($"FirePoint is not assigned on weapon: {weaponName}");
            return;
        }

        if (bulletPrefab == null)
        {
            Debug.LogWarning($"BulletPrefab is not assigned on weapon: {weaponName}");
            return;
        }

        nextTimeToFire = Time.time + fireRate;
        currentAmmo--;

        // Play muzzle flash effect if assigned
        //muzzleFlash?.Play();

        // Spawn the bullet prefab and apply force
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        }
    }

    // Begins the reload process if conditions are met.
    public virtual void Reload()
    {
        if (isReloading || currentAmmo == maxAmmo || reserveAmmo <= 0)
            return;

        isReloading = true;
        StartCoroutine(ReloadRoutine());
    }

    // Handles the reloading delay and ammo transfer.
    private System.Collections.IEnumerator ReloadRoutine()
    {
        yield return new WaitForSeconds(reloadTime);

        int neededAmmo = maxAmmo - currentAmmo;
        int ammoToLoad = Mathf.Min(neededAmmo, reserveAmmo);

        currentAmmo += ammoToLoad;
        reserveAmmo -= ammoToLoad;

        isReloading = false;
    }
}
