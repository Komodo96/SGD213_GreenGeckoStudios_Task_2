using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    // Array to hold all available weapons for the player
    public Weapon[] weapons;

    // Index of the currently active weapon in the weapons array
    private int currentWeaponIndex = 0;

    // Reference to the currently active weapon instance
    [SerializeField]
    private Weapon currentWeapon;

    // UI Text component to display ammo count
    public TextMeshProUGUI ammoText;

    void Start()
    {
        // Safety check to ensure weapons array is assigned and not empty
        if (weapons == null || weapons.Length == 0)
        {
            Debug.LogWarning("No weapons assigned in WeaponManager!");
            return;
        }

        // Activate the first weapon by default on game start
        SwitchWeapon(0);
        UpdateAmmoUI();
    }

    void Update()
    {
        // Checks for number key presses to switch weapons
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(0);
            UpdateAmmoUI();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(1);
            UpdateAmmoUI();
        }

        if (currentWeapon != null)
        {
            if (currentWeapon.IsFullAuto)
            {
                // Fire while holding mouse button down
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    currentWeapon.Shoot();
                    UpdateAmmoUI();
                }
            }
            else
            {
                // Semi-auto fire only on mouse button down
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    currentWeapon.Shoot();
                    UpdateAmmoUI();
                }
            }
        }

        // Checks for the reload key to reload the current weapon
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentWeapon?.Reload();
            // Update ammo display after reload starts
            UpdateAmmoUI(); 
        }
    }

    // Switches the current weapon to the one at the given index
    public void SwitchWeapon(int index)
    {
        // Validate weapons array and index bounds
        if (weapons == null || weapons.Length == 0) return;
        if (index < 0 || index >= weapons.Length) return;

        // Deactivate all weapons to ensure only one is active
        foreach (Weapon w in weapons)
            w.gameObject.SetActive(false);

        // Update current weapon index and reference
        currentWeaponIndex = index;
        currentWeapon = weapons[index];

        // Activate the selected weapon
        currentWeapon.gameObject.SetActive(true);
    }

    // Updates the ammo count displayed in the UI
    void UpdateAmmoUI()
    {
        if (ammoText && currentWeapon != null)
        {
            // Display current ammo and reserve ammo 
            ammoText.text = $"{currentWeapon.CurrentAmmo} / {currentWeapon.ReserveAmmo}";
        }
    }
}
