using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private PlayerInput playerInput;
    
    public event EventHandler PlayerHit;
    void Awake()
    {
        playerHealth.Death += PlayerHealth_Death;
    }

    public void AddDamage(float damage)
    {
        playerHealth.TakeDamage(damage);
    }

    public void AddDamage(float damage, Vector2 pushback)
    {
        playerHealth.TakeDamage(damage);

        if (PlayerHit!= null)
        {
            PlayerHit.Invoke(this, new HitEventArgs(pushback));
        }
    }

    private void PlayerHealth_Death(object sender, System.EventArgs e)
    {
        playerAnimator.SetBool(Constants.IS_DEAD, true);
        playerInput.enabled = false;
    }
}

public class HitEventArgs : EventArgs
{
    public Vector2 hitPosition;

    public HitEventArgs(Vector2 hitPosition)
    {
        this.hitPosition = hitPosition;
    }
}
