using UnityEngine;

public class ArrowsAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.attachedRigidbody.GetComponentInChildren<PlayerMovement>();
        player.EnableArrowsAttack();
        Destroy(gameObject);
    }
}
