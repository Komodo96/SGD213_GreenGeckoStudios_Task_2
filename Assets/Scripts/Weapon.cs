using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class Weapon : MonoBehaviour
{
    public string weaponName;
    public int maxAmmo = 30;
    public int currentAmmo;
    public int reserveAmmo = 90;
    public float fireRate = 0.2f;
    public float damage = 10f;
    public float reloadTime = 1.5f;
    public GameObject bulletPrefab;
    public float bulletForce = 500f;

    protected bool isReloading = false;
    protected float nextTimeToFire = 0f;

    public Transform firePoint;
    public ParticleSystem muzzleFlash;

    public virtual void Shoot()
    {
        if (isReloading || Time.time < nextTimeToFire || currentAmmo <= 0)
            return;

        nextTimeToFire = Time.time + fireRate;
        currentAmmo--;

        muzzleFlash?.Play();

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        }
    }

    public virtual void Reload()
    {
        if (isReloading || currentAmmo == maxAmmo || reserveAmmo <= 0)
            return;

        isReloading = true;
        // Start reload coroutine
        MonoBehaviour mb = firePoint.GetComponentInParent<MonoBehaviour>();
        mb.StartCoroutine(ReloadRoutine());
    }

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
