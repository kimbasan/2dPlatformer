using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private bool destroyOnCollide;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag(Constants.DAMAGEABLE))
        {
            Debug.Log("Adding damage!");
            DealDamage(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.DAMAGEABLE))
        {
            Debug.Log("Adding damage!");
            DealDamage(collision.gameObject);
        }
    }

    private void DealDamage(GameObject target)
    {
        var health = target.GetComponent<PlayerHealth>();
        if (health == null)
        {
            health = target.GetComponentInParent<PlayerHealth>();
        }
        if (health != null && enabled)
        {
            health.AddDamage(damage, transform.position);
            if (destroyOnCollide)
            {
                gameObject.SetActive(false);
            }
        } else
        {
            var enemyHealth = target.GetComponent<Health>();
            if (enemyHealth!= null && enabled)
            {
                enemyHealth.TakeDamage(damage);
                if (destroyOnCollide)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
