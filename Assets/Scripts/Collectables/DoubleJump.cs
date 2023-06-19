using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.attachedRigidbody.GetComponentInChildren<PlayerMovement>();
        player.EnableDoubleJump();
        Destroy(gameObject);
    }
}
