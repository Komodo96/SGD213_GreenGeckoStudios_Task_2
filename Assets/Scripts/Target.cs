//using UnityEngine;
//using System;

//public class Target : MonoBehaviour
//{
//    public float maxHealth = 100f;
//    private float currentHealth;

//    public event Action OnDeath;

//    private void Awake()
//    {
//        currentHealth = maxHealth;
//    }

//    public void TakeDamage(float amount)
//    {
//        currentHealth -= amount;
//        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

//        if (currentHealth <= 0f)
//        {
//            Die();
//        }
//    }

//    void Die()
//    {
//        OnDeath?.Invoke();
//        // Can add death animation or effects here
//        Destroy(gameObject);
//    }
//}
