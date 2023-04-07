using Unity.VisualScripting;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private bool destroyOnCollide;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.attachedRigidbody.CompareTag(Constants.DAMAGEABLE))
        {
            DealDamage(collision.attachedRigidbody.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.DAMAGEABLE))
        {
            DealDamage(collision.gameObject);
        }
    }

    private void DealDamage(GameObject target)
    {
        target.GetComponent<Health>().TakeDamage(damage);
        if (destroyOnCollide)
        {
            Destroy(gameObject);
        }
    }
}
