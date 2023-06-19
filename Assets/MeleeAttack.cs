using UnityEngine;
using UnityEngine.UI;

public class MeleeAttack : MonoBehaviour
{

    [SerializeField] private float damage;
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private LayerMask layerToAttack;
    [SerializeField] private float attackRadius;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float timeBetweenAttacks;

    [SerializeField] private Image statusImage;
    
    bool attackReload = false;
    float attackReloadTimer = 0f;
    public void TryAttack()
    {
        if (!attackReload)
        {
            playerAnimator.SetTrigger(Constants.SLASH);
            attackReload = true;
        }
    }

    private void Update()
    {
        if (attackReload)
        {
            attackReloadTimer += Time.deltaTime;
            statusImage.fillAmount = attackReloadTimer / timeBetweenAttacks;
            if (attackReloadTimer >= timeBetweenAttacks)
            {
                attackReload = false;
                attackReloadTimer= 0f;
                statusImage.fillAmount = 1;
            }
        }
    }


    public void Slash()
    {

        Collider2D result = Physics2D.OverlapCircle(attackPoint.transform.position, attackRadius, layerToAttack);
        if (result != null)
        {
            if (result.gameObject.CompareTag(Constants.RED_BARREL))
            {
                var barrel = result.gameObject.GetComponent<ExplosiveBarrel>();
                if (barrel != null)
                {
                    barrel.StartExplosionTimer();
                }
            } else
            {
                Health enemyHealth = result.gameObject.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                }
            }
        }

        playerMovement.EndAttack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRadius);
    }
}
