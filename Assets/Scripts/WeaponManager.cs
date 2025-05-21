using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class WeaponManager : MonoBehaviour
{
    public Weapon[] weapons;
    private int currentWeaponIndex = 0;
    private Weapon currentWeapon;

    public TextMeshProUGUI ammoText;

    void Start()
    {
        SwitchWeapon(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchWeapon(1);

        if (Input.GetButton("Fire1")) currentWeapon?.Shoot();

        if (Input.GetKeyDown(KeyCode.R)) currentWeapon?.Reload();

        UpdateAmmoUI();
    }

    void SwitchWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length) return;

        foreach (Weapon w in weapons)
            w.gameObject.SetActive(false);

        currentWeaponIndex = index;
        currentWeapon = weapons[index];
        currentWeapon.gameObject.SetActive(true);
    }

    void UpdateAmmoUI()
    {
        if (ammoText && currentWeapon != null)
        {
            ammoText.text = $"{currentWeapon.currentAmmo} / {currentWeapon.reserveAmmo}";
        }
    }
}
