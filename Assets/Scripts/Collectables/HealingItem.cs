using UnityEngine;

public class HealingItem : MonoBehaviour
{
    [SerializeField] private int healthPoints;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.attachedRigidbody.GetComponent<Health>();
        if (health != null)
        {
            health.AddHealth(healthPoints);
            Debug.Log("Health Added");
            Destroy(gameObject);
        }
    }
}
