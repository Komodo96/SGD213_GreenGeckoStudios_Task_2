using UnityEngine;
using UnityEngine.UI; // For UI elements
using TMPro;

public class GrenadeThrower : MonoBehaviour
{
    [Header("Grenade Settings")]
    public GameObject grenadePrefab;
    public Transform throwPoint;
    public float throwForce = 15f;
    public float upwardForce = 2f;

    [Header("Explosion Settings")]
    public float grenadeLifetime = 3f; // Time before explosion

    [Header("Grenade Count UI")]
    public int maxGrenades = 3;
    private int currentGrenades;
    public TextMeshProUGUI grenadeCountText;

    void Start()
    {
        currentGrenades = maxGrenades;
        UpdateGrenadeUI();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2") && currentGrenades > 0)
        {
            ThrowGrenade();
        }
    }

    void ThrowGrenade()
    {
        if (grenadePrefab == null || throwPoint == null) return;

        GameObject grenade = Instantiate(grenadePrefab, throwPoint.position, throwPoint.rotation);

        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 forceDirection = throwPoint.forward * throwForce + throwPoint.up * upwardForce;
            rb.AddForce(forceDirection, ForceMode.VelocityChange);
        }

        currentGrenades--;
        UpdateGrenadeUI();

        StartCoroutine(ExplodeAfterDelay(grenade, grenadeLifetime));
    }

    System.Collections.IEnumerator ExplodeAfterDelay(GameObject grenade, float delay)
    {
        yield return new WaitForSeconds(delay);

        Debug.Log("BOOM! Grenade exploded!");
        Destroy(grenade);
    }

    void UpdateGrenadeUI()
    {
        if (grenadeCountText != null)
        {
            grenadeCountText.text = "Grenades: " + currentGrenades;
        }
    }

    // Optional: Call this from pickups to replenish grenades
    public void AddGrenade(int amount)
    {
        currentGrenades = Mathf.Min(currentGrenades + amount, maxGrenades);
        UpdateGrenadeUI();
    }
}
