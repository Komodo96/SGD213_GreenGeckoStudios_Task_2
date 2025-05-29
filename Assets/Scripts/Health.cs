using UnityEngine;
using UnityEngine.UI;
using System;

// Implements the IHealth interface so this component can take damage generically
public class Health : MonoBehaviour, IHealth
{
    [SerializeField]
    private float maxHealth = 100f;

    [SerializeField]
    private float currentHealth;

    [SerializeField]
    private bool isPlayer = false;

    // UI health bar
    [SerializeField]
    private Slider healthSlider;

    // Event invoked when health reaches zero (death)
    public event Action OnDeath;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    // Call this to apply damage
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        UpdateHealthUI();

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Update()
    {
        // For testing: only player can take damage on pressing T once
        if (isPlayer && Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10f);
        }
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
    }

    private void Die()
    {
        // Fire death event, so other scripts can respond (animations, sounds, etc.)
        OnDeath?.Invoke();

        if (isPlayer)
        {
            Debug.Log("Player Died!");
            // TODO: Disable movement, show game over screen, etc.
        }
        else
        {
            Debug.Log($"{gameObject.name} Died!");
            Destroy(gameObject);
        }
    }
}
