using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public bool isPlayer = false;

    // UI
    public Slider healthSlider; // Unity UI Slider for health bar

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
    }

    private void Die()
    {
        if (isPlayer)
        {
            Debug.Log("Player Died!");
            // Disable movement, show game over screen, etc.
        }
        else
        {
            Debug.Log($"{gameObject.name} Died!");
            Destroy(gameObject);
        }
    }
}
