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

        Debug.Log($"[BULLET] Collided with: {hitObject.name}, Layer: {LayerMask.LayerToName(hitObject.layer)}, Tag: {hitObject.tag}");

        if (HasPlayerTag(hitObject))
        {
            Debug.Log("Hit player or its child - ignoring damage.");
            return;
        }

        IHealth healthComponent = collision.collider.GetComponentInParent<IHealth>();
        if (healthComponent != null)
        {
            Debug.Log($"[BULLET] Dealing {damage} damage to: {hitObject.name}");
            healthComponent.TakeDamage(damage);
        }
        else
        {
            Debug.Log("[BULLET] No IHealth component found on hit object or its parents.");
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