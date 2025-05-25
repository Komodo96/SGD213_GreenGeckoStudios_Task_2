using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 10f;
    public float lifeTime = 2f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Give the bullet an initial velocity forward
            rb.velocity = transform.forward * speed;
        }

        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject hitObject = collision.gameObject;

        // Check tag on hit object or any of its parents
        if (HasPlayerTag(hitObject))
        {
            Debug.Log("Hit player or its child - ignoring damage.");
            return;
        }

        Debug.Log($"Bullet hit: {hitObject.name}, Tag: {hitObject.tag}");

        Health health = collision.collider.GetComponentInParent<Health>();
        if (health != null)
        {
            Debug.Log($"Dealing damage to: {health.gameObject.name}");
            health.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

    private bool HasPlayerTag(GameObject obj)
    {
        while (obj != null)
        {
            if (obj.CompareTag("Player"))
                return true;
            if (obj.transform.parent != null)
                obj = obj.transform.parent.gameObject;
            else
                obj = null;
        }
        return false;
    }
}