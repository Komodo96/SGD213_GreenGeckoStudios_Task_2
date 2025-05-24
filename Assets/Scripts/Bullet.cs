using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 10f;
    public float lifeTime = 2f;

    private void Start()
    {
        // Destroy bullet after X seconds
        Destroy(gameObject, lifeTime); 
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Target target))
        {
            target.TakeDamage(damage);
        }

        // Destroy bullet on impact
        Destroy(gameObject); 
    }
}