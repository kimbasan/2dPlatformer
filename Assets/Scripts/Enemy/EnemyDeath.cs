using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject parent;
    [SerializeField] private DamageDealer damageDealer;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] private EnemyController controller;
    void Awake()
    {
        health.Death += Health_Death;
    }

    private void Health_Death(object sender, System.EventArgs e)
    {
        animator.SetBool(Constants.IS_ALIVE, false);
        damageDealer.enabled = false;
        rb.gravityScale = 1.0f;
        rb.velocity = new Vector2(0, rb.velocity.y);
        if (controller!= null)
        {
            controller.enabled = false;
        }
    }

    public void OnDeath()
    {
        Destroy(parent);
    }
}
